/*
 * SuperNutShooter (1423) 
 * 
 * https://discord.com/channels/1529901206422425772/1554910835032334440/1555883120505397418
 * base damage: 30*6 -> 100*6
 * giant wallnuts 20 -> 100x base damage
 */

using HarmonyLib;
#if MELONLOADER
using Il2Cpp;
#endif

namespace PvZRH_Balancer.Patches
{
    public static class SuperNutShooterPatch
    {
        /*
         * We simply check if any of the parameters are greater than 50,
         * then we simply increase its values to a high number so the
         * game thinks this plant has reached the threashold
         */
        [HarmonyPatch(typeof(SuperNutShooter), nameof(SuperNutShooter.Awake))]
        public static class SuperNutShooter_Awake
        {
            [HarmonyPrefix]
            public static void Prefix(SuperNutShooter __instance)
            {
                __instance.attackDamage = 100;

            }
        }

        /*
         * For increasing the damage of spawned Big Wall-nut,
         * We Postfix Big Wall-nut and multiply its damage 5 times
         */
        [ThreadStatic] static bool isShoot2;

        [HarmonyPatch(typeof(SuperNutShooter), nameof(SuperNutShooter.Shoot2))]
        public static class SuperNutShooter_Shoot2
        {
            [HarmonyPrefix]
            public static void Prefix()
            {
                isShoot2 = true;
            }

            [HarmonyPostfix]
            public static void Postfix()
            {
                isShoot2 = false;
            }
        }

        [HarmonyPatch(typeof(BigWallNut), nameof(BigWallNut.ModifyDamage))]
        public static class BigWallNut_ModifyDamage
        {

            [HarmonyPostfix]
            public static void Postfix(BigWallNut __instance)
            {
                if (!isShoot2) return;

                __instance.damageAdder[PlantDamageAdder.Update] *= 5;
            }
        }
    }
}
