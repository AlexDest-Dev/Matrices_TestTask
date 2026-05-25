using System.Collections.Generic;
using _Project.Code.Core;
using UnityEngine;

namespace _Project.Code.OffsetsSerializing
{
    public class OffsetsWriter
    {
        private JsonWriter _jsonWriter;
        
        public OffsetsWriter(JsonWriter jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        public void WriteOffsets(List<Matrix4x4> offsets)
        {
            var serializedMatrices = new List<SerializedMatrix>();
            foreach (var offset in offsets)
            {
                serializedMatrices.Add(new SerializedMatrix(offset));
            }
            Logger.Log($"WriteOffsets: {serializedMatrices.Count}");
            _jsonWriter.Write(serializedMatrices);
        }
    }
}