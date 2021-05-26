using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace LogisticsSubnets
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("DSPGAME.exe")]
    public class ModEntry : BaseUnityPlugin
    {

        public static ManualLogSource _logger;
        private const string PluginGuid = "Captncraig.LogisticsSubnets";
        private const string PluginName = "Logistics Subnets";
        private const string PluginVersion = "1.0.1.0";

        public void Awake()
        {
            SubnetPatches._logger = base.Logger;
            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll(typeof(SubnetPatches));
        }
        public void Start()
        {
            _logger = base.Logger;
        }
    }
}
