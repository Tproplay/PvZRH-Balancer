/*
 * https://discord.com/channels/1529901206422425772/1554910835032334440/1555292366376669306
 * The damage x4 modifier only affects the "main blast" damage, not the additional one.
 * Make it affect the additional damage, meaning 120% of all zombie hp across the screen.
 * This will NOT make it overpowered, only potentially viable. It's so bad rn.
 */

using HarmonyLib;

#if MELONLOADER
using Il2Cpp;
#endif

namespace PvZRH_Balancer.Patches
{
    public static class UltimateCannonPatch
    {
        [ThreadStatic] private static int setDoomBaseDamage = -1;

        [HarmonyPatch(typeof(BoardAction))]
        public static class BoardAction_SetDoom
        {
            [HarmonyPatch(nameof(BoardAction.SetDoom))]
            [HarmonyPrefix]
            public static void Prefix(int effect, int damage)
            {
                if (effect == 2) setDoomBaseDamage = damage;
            }

            [HarmonyPatch(nameof(BoardAction.SetDoom))]
            [HarmonyPostfix]
            public static void Postfix() => setDoomBaseDamage = -1;
        }

        // Scale the 30% Max HP portion to 120%
        [HarmonyPatch(typeof(Zombie))]
        public static class Zombie_Charred
        {
            [HarmonyPatch(nameof(Zombie.Charred))]
            [HarmonyPrefix]
            public static void Prefix(ref int damage)
            {
                if (setDoomBaseDamage >= 0 && damage > setDoomBaseDamage)
                {
                    damage = (damage - setDoomBaseDamage) * 4 + setDoomBaseDamage;
                }
            }
        }
    }
}