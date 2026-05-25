using _Project.Code.Core;
using _Project.Code.OffsetsSerializing;
using Zenject;

namespace _Project.Code
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCore();
            Container.Bind<OffsetsCalculator.OffsetsCalculator>().AsSingle();
            Container.Bind<OffsetsWriter>().AsSingle();
        }

        private void BindCore()
        {
            Container.Bind<JsonReader>().AsSingle();
            Container.Bind<ModelProvider>().AsSingle();
            Container.Bind<JsonWriter>().AsSingle();
        }
    }
}