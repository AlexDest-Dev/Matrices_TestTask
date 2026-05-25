using System.Collections.Generic;
using System.IO;
using _Project.Code.Core;
using UnityEngine;

namespace _Project.Code
{
    public class ModelProvider
    {
        private readonly JsonReader _jsonReader;
        private readonly List<Matrix4x4> _spaceMatrices;
        private readonly List<Matrix4x4> _modelMatrices;

        public IReadOnlyList<Matrix4x4> ModelMatrices => _modelMatrices;
        public IReadOnlyList<Matrix4x4> SpaceMatrices => _spaceMatrices;

        public ModelProvider(JsonReader jsonReader)
        {
            _jsonReader = jsonReader;
            _modelMatrices = _jsonReader.Read<List<Matrix4x4>>(Path.Combine(Application.streamingAssetsPath, "model.json"));
            _spaceMatrices = _jsonReader.Read<List<Matrix4x4>>(Path.Combine(Application.streamingAssetsPath, "space.json"));
        }
    }
}