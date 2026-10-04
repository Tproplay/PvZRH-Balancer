/*
 * Doom Sniper (1272) 
 * 
 * https://discord.com/channels/1529901206422425772/1554910835032334440/1555882566144495696
 * 300 doom charges -> 50
 * 100 frenzy charges -> 50
 */

using HarmonyLib;
#if MELONLOADER
using Il2Cpp;
#endif

namespace PvZRH_Balancer.Patches
{
    public static class DoomSniperPatch
    {
        /*
         * We simply check if any of the parameters are greater than 50,
         * then we simply increase its values to a high number so the
         * game thinks this plant has reached the threashold
         */
        [HarmonyPatch(typeof(DoomSniper), nameof(DoomSniper.Shoot1))]
        public static class DoomSniper_Shoot1
        {
            [HarmonyPrefix]
            public static void Prefix(DoomSniper __instance)
            {
                if (__instance.craze >= 51) __instance.craze = 301;
                if (__instance.onShootTimes >= 51) __instance.onShootTimes = 301;

            }
        }
    }
}
