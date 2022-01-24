using HarmonyLib;
using BepInEx.Logging;

namespace FreeStuff
{
    public static class Patches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CargoTraffic), "UpdateSplitter")]
        public static bool CargoTraffic_UpdateSplitter_Prefix(CargoTraffic __instance, int id, int input0, int input1, int input2, int output0, int output1, int output2, int filter)
        {
            if (input0 == 0 && output0 != 0 && filter != 0)
            {
                var outputPath = __instance.GetCargoPath(__instance.beltPool[output0].segPathId);
                if (outputPath != null && outputPath.TestBlankAtHead() == 0)
                {
                    outputPath.TryInsertItemAtHead(filter);
                }
            }
            return true;
        }
    }
}
