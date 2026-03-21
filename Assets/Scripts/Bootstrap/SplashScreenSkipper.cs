using UnityEngine;
using UnityEngine.Rendering;

namespace Bootstrap
{
    internal sealed class SplashScreenSkipper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void SkipSplashScreen() =>
            SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
    }
}