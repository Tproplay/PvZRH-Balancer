using System.Reflection;

#if MELONLOADER
using MelonLoader;
#endif

namespace PvZRH_Balancer;

public class Main
{
    public static Main? Instance { get; private set; }
    public static HarmonyLib.Harmony? HarmonyInstance { get; private set; }
    public static void Initialize()
    {
        Instance = new Main();

        HarmonyInstance = new HarmonyLib.Harmony("com.tproplay.balancer");

        Assembly currentAssembly = typeof(Main).Assembly;

        HarmonyPatchInfo patchInfo = HarmonyManager.HarmonyPatchAll(currentAssembly, HarmonyInstance);

#if MELONLOADER
        if (patchInfo.HasFailures)
        {
            MelonLogger.Warning($"[Harmony] {patchInfo.FailCount} patch classes failed to apply:");
            for (int i = 0; i < patchInfo.Failures.Count; i++)
            {
                var failure = patchInfo.Failures[i];
                MelonLogger.Error($"[Harmony] -> Failure #{i + 1} on '{failure.ClassName}':\nException: {failure.Exception.GetType().Name} - {failure.Exception.Message}\nStack: {failure.Exception.StackTrace}");
            }
        }
#endif
    }

    public void OnUpdate()
    {

    }
}
