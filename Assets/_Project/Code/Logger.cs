using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace _Project.Code
{
    public static class Logger
    {
        public static void Log(
            object message,
            Object context = null,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "")
        {
            Debug.Log(Format(message, filePath, memberName), context);
        }

        public static void LogWarning(
            object message,
            Object context = null,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "")
        {
            Debug.LogWarning(Format(message, filePath, memberName), context);
        }

        public static void LogError(
            object message,
            Object context = null,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "")
        {
            Debug.LogError(Format(message, filePath, memberName), context);
        }

        public static void LogException(
            System.Exception exception,
            Object context = null,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "")
        {
            Debug.LogError(Format(exception, filePath, memberName), context);
        }

        private static string Format(
            object message,
            string filePath,
            string memberName)
        {
            string className = Path.GetFileNameWithoutExtension(filePath);
            return $"[{className}] {message}";
        }
    }
}