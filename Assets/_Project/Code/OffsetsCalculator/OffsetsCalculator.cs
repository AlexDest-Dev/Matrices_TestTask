using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.OffsetsCalculator
{
    public class OffsetsCalculator
    {
        // Точность сравнения матриц
        // Если использовать станадртное сравнение матриц - оно имеет высокую точность(, а предоставленные данные имеют относительно высокое расхождение
        // С помощью Epsilon и кастомного компаратора мы снижаем точность сравнения
        // 0.00001f -> 16 оффсетов
        // 0.0001f и больше -> 20 оффсетов
        private const float Epsilon = 0.0001f;

        private readonly ModelProvider _modelProvider;
        private readonly MatrixComparator _matrixComparator;

        public OffsetsCalculator(ModelProvider modelProvider)
        {
            _modelProvider = modelProvider;
            _matrixComparator = new MatrixComparator(Epsilon);
        }

        public async UniTask<List<Matrix4x4>> CalculateOffsets(CancellationToken ct = default)
        {
            Logger.Log("Start calculating offsets");
            var result = await UniTask.RunOnThreadPool(() =>
            {
                var foundOffsets = new List<Matrix4x4>();
                // Оптимизация - достаточно использовать любую модель как опорную для вычисления тестового оффсета,
                // так как оффсет должен подходить для каждой модели
                var inverseAnchorModel = _modelProvider.ModelMatrices[0].inverse;
                foreach (var spaceMatrix in _modelProvider.SpaceMatrices)
                {
                    ct.ThrowIfCancellationRequested();
                    var offset = spaceMatrix * inverseAnchorModel;
                    if (IsOffsetValid(offset))
                        foundOffsets.Add(offset);
                }

                return foundOffsets;
            }, cancellationToken: ct);

            Logger.Log($"Found offsets: {result.Count}");
            return result;
        }

        private bool IsOffsetValid(Matrix4x4 offset)
        {
            for (var i = 1; i < _modelProvider.ModelMatrices.Count; i++)
            {
                var modelMatrix = _modelProvider.ModelMatrices[i];
                var modifiedModel = offset * modelMatrix;
                if (!IsMatrixInSpace(_matrixComparator, modifiedModel))
                    return false;
            }

            return true;
        }

        private bool IsMatrixInSpace(MatrixComparator matrixComparer, Matrix4x4 checkSpaceMatrix)
        {
            foreach (var spaceMatrix in _modelProvider.SpaceMatrices)
            {
                if (matrixComparer.Equals(spaceMatrix, checkSpaceMatrix))
                    return true;
            }

            return false;
        }
    }
}