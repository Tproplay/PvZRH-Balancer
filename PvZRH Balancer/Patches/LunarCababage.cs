/*
 * Lunar Cabbage (955)
 * 
 * https://discord.com/channels/1529901206422425772/1554910835032334440/1556323887988482128
 * Removed the Attack damage cap of Basic Attack which was previously capped at 500.
 */

using HarmonyLib;
#if MELONLOADER
using Il2Cpp;
#endif

namespace PvZRH_Balancer.Patches
{
    public static class LunarCabbagePatch
    {
        
        public static class NoAttackDamageCapPatch
        {
            /*
             * Whenever Lunar Chabbage fires a bullet, it gets passed into the Unique Effect method
             * to apply bonus damage based on the plant's light level.
             * The bonus damage is capped at 500, so we check if the plant could had applied
             * more damage if it wansn't capped.
             */
            [HarmonyPatch(typeof(LunarCabbage), nameof(LunarCabbage.UniqueEffect))]
            public static class LunarCabbage_UniqueEffect
            {
                [HarmonyPostfix]
                public static void Postfix(LunarCabbage __instance, Bullet bullet)
                {
                    if (bullet == null) return;

                    // Calculate the damage bonus to add
                    // The method has already applied upto 500 bonus damage so we only add
                    // the extra damage above 500 
                    int BonusDamage = Math.Max(0, __instance.currentLightLevel * 100 - 500);

                    // We want to skip if no damage to add
                    if (BonusDamage == 0) return;

                    // Ultimate Buff 22 grants 3x the base damage to this plant
                    int multiplier = Lawnf.TravelUltimate((UltiBuff)22) ? 3 : 1;

                    // Add the damage
                    bullet.Damage += BonusDamage * multiplier;
                }
            }
        }
    }
}