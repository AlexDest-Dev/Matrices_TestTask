using UnityEngine;
using Zenject;

namespace _Project.Code.OffsetsVisualiser
{
    public class OffsetsVisualiserInstaller : MonoInstaller
    {
        [SerializeField] private OffsetsVisualiser _offsetsVisualiser;
        public override void InstallBindings()
        {
            Container.Bind<OffsetsVisualiser>().FromInstance(_offsetsVisualiser).AsSingle();
        }
    }
}