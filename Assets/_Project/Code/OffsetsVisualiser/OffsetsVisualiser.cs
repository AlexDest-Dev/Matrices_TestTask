using System.Collections.Generic;
using System.Threading;
using _Project.Code.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Code.OffsetsVisualiser
{
    public class OffsetsVisualiser : MonoBehaviour
    {
        private ModelProvider _modelProvider;
        private List<Matrix4x4> _foundOffsets;

        [SerializeField] private Transform _modelRoot;
        [SerializeField] private Transform _spaceRoot;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _modelInOffsetMaterial;

        private List<GameObject> views = new();

        [Inject]
        public void Construct(ModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
            InitViews();
        }

        public async UniTask ShowOffsets(List<Matrix4x4> offsets, CancellationToken ct)
        {
            Logger.Log($"Started showing offsets. Total {offsets.Count}");
            SetDefaultValuesForModel();
            for (var i = 0; i < offsets.Count; i++)
            {
                ct.ThrowIfCancellationRequested();
                var offset = offsets[i];
                Logger.Log($"Show offset {i + 1} from {offsets.Count}");
                var offsetAnimator = new OffsetAnimator(_modelRoot, offset, 3f);
                await offsetAnimator.Animate(ct);
                await UniTask.WaitForSeconds(1f, cancellationToken: ct);
                SetDefaultValuesForModel();
                await UniTask.WaitForSeconds(1f, cancellationToken: ct);
            }
        }

        private void SetDefaultValuesForModel()
        {
            _modelRoot.rotation = Quaternion.identity;
            _modelRoot.position = Vector3.zero;
        }

        private void InitViews()
        {
            Logger.Log("Initialize views");
            foreach (var matrix in _modelProvider.SpaceMatrices)
                CreatePrimitive(matrix, _spaceRoot, _defaultMaterial);

            foreach (var matrix in _modelProvider.ModelMatrices)
                CreatePrimitive(matrix, _modelRoot, _modelInOffsetMaterial);
        }

        private void CreatePrimitive(Matrix4x4 matrix, Transform root, Material material)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(root);
            cube.GetComponent<Renderer>().material = material;
            ApplyMatrix(cube.transform, matrix);
            views.Add(cube);
        }

        private static void ApplyMatrix(Transform objectTransform, Matrix4x4 matrix)
        {
            objectTransform.position = matrix.GetColumn(3);
            objectTransform.rotation = matrix.rotation;
            objectTransform.localScale = matrix.lossyScale;
        }
    }
}