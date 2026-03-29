using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Hệ thống Quản trị Cảnh báo (Logger) tiêu chuẩn.
    /// Cho phép bật/tắt toàn bộ Log của Game để tối ưu hiệu năng CPU và RAM khi xuất xưởng (Build Production).
    /// </summary>
    public static class GameLogger
    {
        [Tooltip("Tắt biến này khi Build phát hành để đỡ tốn tài nguyên của người chơi.")]
        public static bool EnableLogs = true;

        public static void Log(object message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (EnableLogs)
            {
                Debug.Log($"<color=white>[GameLog]</color> {message}");
            }
#endif
        }

        public static void LogWarning(object message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (EnableLogs)
            {
                Debug.LogWarning($"<color=yellow>[GameWarning]</color> {message}");
            }
#endif
        }

        public static void LogError(object message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (EnableLogs)
            {
                Debug.LogError($"<color=red>[GameError]</color> {message}");
            }
#endif
        }
    }
}
