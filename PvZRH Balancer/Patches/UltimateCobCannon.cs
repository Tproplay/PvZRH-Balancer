/*
 * https://discord.com/channels/1529901206422425772/1554910835032334440/1555292366376669306
 * 
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
        /*
         * The Ultimate CobCannon deals screen wide 30% hp based damage by using
         * the BoardAction.SetDoom function with effect = 2 parameter passed
         * 
         * The SetDoom function calls the Zombie.Charred function to deal percentage
         * based damage
         * 
         * We use the setDoomBaseDamage to track if the charred damage was called
         * by Ultimate CobCannon's bullet.
         */

        [ThreadStatic] private static int setDoomBaseDamage = -1;

        [HarmonyPatch(typeof(BoardAction))]
        public static class BoardAction_SetDoom
        {
            [HarmonyPatch(nameof(BoardAction.SetDoom))]
            [HarmonyPrefix]
            public static void Prefix(int effect, int damage)
            {
                if (Lawnf.TravelUltimate((UltiBuff)14) // Do 4x damage only if the buff is active
                    && effect == 2)                    // effect 2 is Ultimate CobCannon's effect
                {
                    setDoomBaseDamage = damage;
                }
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
                // The damage pased in this functions is:
                // damage = (Zombie.theFirstArmorHealth) * 0.3 + Doom_damage 

                if (setDoomBaseDamage >= 0)
                {
                    damage = (damage - setDoomBaseDamage) * 4 + setDoomBaseDamage;
                }
            }
        }
    }
}