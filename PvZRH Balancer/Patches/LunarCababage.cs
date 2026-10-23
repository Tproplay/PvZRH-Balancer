/*
 * Lunar Cabbage (955)
 * 
 * https://discord.com/channels/1529901206422425772/1554910835032334440/1556323887988482128
 * Removed the Attack damage cap.
 */

using HarmonyLib;
#if MELONLOADER
using Il2Cpp;
#endif

namespace PvZRH_Balancer.Patches
{
    public static class LunarCabbagePatch
    {
        [HarmonyPatch(typeof(LunarCabbage), nameof(LunarCabbage.UniqueEffect))]
        public static class LunarCabbage_UniqueEffect
        {
            [HarmonyPostfix]
            public static void Postfix(LunarCabbage __instance, Bullet bullet)
            {
                if (bullet == null) return;

                // Calculate the damage bonus to add
                int BonusDamage = Math.Max(0, __instance.currentLightLevel * 100 - 500);
                int multiplier = Lawnf.TravelUltimate((UltiBuff)22) ? 3 : 1;

                // Add the damage
                bullet.Damage += BonusDamage * multiplier;
            }
        }
    }
}