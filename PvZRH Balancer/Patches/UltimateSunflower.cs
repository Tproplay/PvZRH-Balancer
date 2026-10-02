/*
 * Ultimate Sunflower (954) 
 * 
 * https://discord.com/channels/1529901206422425772/1554910835032334440/1555294407970791434
 * Energy cap removed instead of 3x the cap
 */

using HarmonyLib;
#if MELONLOADER
using Il2Cpp;
#endif

namespace PvZRH_Balancer.Patches
{
    public static class UltimateSunflowerPatch
    {
        /*
         * We simply change the game's logic and instead use our custom one
         * which makes the cap very high so that it can be considered unlimited
         */
        [HarmonyPatch(typeof(UltimateSunflower), nameof(UltimateSunflower.SunAction))]
        public static class UltimateSunflower_SunAction
        {
            [HarmonyPrefix]
            public static bool CustomSunAction(UltimateSunflower __instance, int value)
            {
                if (__instance.blink.activeSelf)
                {
                    __instance.attributeCount += value * 2;
                }
                else
                {
                    __instance.attributeCount += value;

                    if (__instance.attributeCount >= __instance.attackDamage)
                    {
                        int damage = __instance.attackDamage;
                        __instance.attributeCount -= __instance.attackDamage;
                        __instance.Shoot(damage);
                    }
                }

                // Capacity is 4x or unlimited the attackDamage
                bool hasBuff = Lawnf.TravelUltimate((UltiBuff)39);
                int maxCap = hasBuff ? 2_000_000_000 : __instance.attackDamage * 4;

                if (__instance.attributeCount > maxCap)
                {
                    __instance.attributeCount = maxCap;
                    __instance.board.GetSun(hasBuff ? 45f : 15f, true);
                }

                __instance.UpdateText();
                return false;
            }
        }
    }
}
