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
         * Modify GetPlantData to return 100 attack damage for SuperNutShooter
         * And return 5x damage for WallNut if its spawned while SuperNutShooter is shooting
         */
        [HarmonyPatch(typeof(PlantDataManager), nameof(PlantDataManager.GetPlantData))]
        public static class SuperNutShooter_Awake
        {
            [HarmonyPostfix]
            public static void Postfix(PlantType plantType, ref PlantDataManager.PlantData __result)
            {
                if (plantType == PlantType.SuperNutShooter)
                {
                    __result.attackDamage = 100;
                }

                if (isShoot2)
                {
                    if (plantType == PlantType.BigWallNut)
                    {
                        PlantDataManager.PlantData plantData = new(__result);
                        plantData.attackDamage = 3000;
                        __result = plantData;
                    }
                }
            }
        }

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
    }
}
