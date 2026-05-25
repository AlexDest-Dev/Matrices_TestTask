using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.OffsetsVisualiser
{
    public class OffsetAnimator
    {
        private Transform _animationRoot;
        private Matrix4x4 _offset;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private float _animationTime;

        public OffsetAnimator(Transform animationRoot, Matrix4x4 offset, float animationTime)
        {
            _animationRoot = animationRoot;
            _offset = offset;

            _initialPosition = animationRoot.position;
            _initialRotation = animationRoot.rotation;

            _animationTime = animationTime;
        }

        public async UniTask Animate(CancellationToken ct)
        {
            _animationRoot.position = _initialPosition;
            _animationRoot.rotation = _initialRotation;

            var newPosition = _offset.GetPosition();
            var newRotation = _offset.rotation;

            var time = 0f;
            while (time < _animationTime)
            {
                if (ct.IsCancellationRequested)
                    throw new OperationCanceledException();
                _animationRoot.position = Vector3.Lerp(_initialPosition, newPosition, time / _animationTime);
                _animationRoot.rotation = Quaternion.Lerp(_initialRotation, newRotation, time / _animationTime);
                time += Time.deltaTime;
                await UniTask.Yield();
            }
        }
    }
}