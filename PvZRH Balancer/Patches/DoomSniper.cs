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
        public static class LessChargeThresholdPatch
        {
            /*
             * We simply increase the number of charges you get to make it equivalant to
             * having the threshold of 50 charges
             */
            [HarmonyPatch(typeof(DoomSniper), nameof(DoomSniper.Shoot1))]
            public static class DoomSniper_Shoot1
            {
                [HarmonyPrefix]
                public static void Prefix(DoomSniper __instance)
                {
                    __instance.craze += 1; // craze is frenzy charges count, it resets at 100 stacks
                    __instance.onShootTimes += 5; // doom charges, it doesn't resets
                }
            }
        }
    }
}
