#if UNITY_EDITOR
#define DEBUG
#endif

using System;
using UnityEngine;

public static class Debug
{
    public static bool IsDebugBuild { get { return UnityEngine.Debug.isDebugBuild; } }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object message)
       => UnityEngine.Debug.Log(message);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(object message, UnityEngine.Object context)
       => UnityEngine.Debug.Log(message, context);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object message)
      => UnityEngine.Debug.LogError(message);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogError(object message, UnityEngine.Object context)
       => UnityEngine.Debug.LogError(message, context);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message)
       => UnityEngine.Debug.LogWarning(message.ToString());

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void LogWarning(object message, UnityEngine.Object context)
      => UnityEngine.Debug.LogWarning(message.ToString(), context);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color = default, float duration = 0.0f, bool depthTest = true)
       => UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color = default, float duration = 0.0f, bool depthTest = true)
       => UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Assert(bool bCondition)
    {
        if (!bCondition)
        {
            throw new Exception();
        }
    }
}
