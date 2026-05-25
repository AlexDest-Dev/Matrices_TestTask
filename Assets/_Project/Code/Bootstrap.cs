using System.Collections.Generic;
using System.Threading;
using _Project.Code.OffsetsSerializing;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Code
{
    public class Bootstrap : MonoBehaviour
    {
        private OffsetsVisualiser.OffsetsVisualiser _offsetsVisualiser;
        private OffsetsCalculator.OffsetsCalculator _offsetsCalculator;
        private OffsetsWriter _offsetsWriter;

        private CancellationTokenSource cts;

        [Inject]
        private void Construct(OffsetsCalculator.OffsetsCalculator offsetsCalculator,
            OffsetsVisualiser.OffsetsVisualiser offsetsVisualiser, OffsetsWriter offsetsWriter)
        {
            _offsetsCalculator = offsetsCalculator;
            _offsetsVisualiser = offsetsVisualiser;
            _offsetsWriter = offsetsWriter;
        }

        public void Start()
        {
            ShowOffsets();
        }


        [ContextMenu("ShowOffsets")]
        private void ShowOffsets()
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            AsyncShowOffsets(cts.Token).Forget();
        }

        private async UniTaskVoid AsyncShowOffsets(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var calculatedOffsets = await _offsetsCalculator.CalculateOffsets(ct);
            _offsetsWriter.WriteOffsets(calculatedOffsets);
            await _offsetsVisualiser.ShowOffsets(calculatedOffsets, ct);
        }

        private void OnDestroy()
        {
            cts?.Cancel();
        }
    }
}