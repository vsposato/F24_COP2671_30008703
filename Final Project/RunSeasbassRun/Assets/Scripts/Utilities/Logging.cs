using System;
using UnityEngine;

namespace Utilities
{
    public static class Logging
    {
        /// <summary>
        ///  Set this to true either by code or in the inspector to print trace log messages
        /// </summary>
        private static readonly bool PrintTrace = PlayerPrefs.GetInt("Debug", 0) != 0;

        public static void PrintLog(string str, params object[] args)
        {
            Print(UnityEngine.Debug.Log, str, args);
        }

        public static void PrintWarn(string str, params object[] args)
        {
            Print(UnityEngine.Debug.LogWarning, str, args);
        }

        public static void PrintError(string str, params object[] args)
        {
            Print(UnityEngine.Debug.LogError, str, args);
        }

        private static void Print(Action<string> call, string str, params object[] args)
        {
            if (PrintTrace)
            {
                call(string.Format(
                        "<b>[{0}] {1} </b>",
                        Time.frameCount,
                        string.Format(str, args)
                    )
                );
            }
        }
    }
}