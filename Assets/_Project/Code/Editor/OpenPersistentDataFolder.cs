using UnityEditor;
using UnityEngine;

namespace _Project.Code.Editor
{
    public static class OpenPersistentDataFolder
    {
        [MenuItem("Tools/Show Persistent Data")]
        private static void OpenPersistentData()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }
}