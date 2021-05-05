

namespace  urlopener
{
#if UNITY_EDITOR

    using UnityEngine;

    public class URLOpener
    {
        public static void OpenURL(string score)
        {
            Debug.Log($"Clicked to open URL: {score}");
        }
    }

#elif UNITY_WEBGL

    using System.Runtime.InteropServices;

    public class URLOpener
    {
        [DllImport("__Internal")]
        public static extern void OpenURL(string score);
    }

#else

    public class URLOpener
    {
        public static void OpenURL(string score)
        {
        }
    }
#endif
}
