using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace FreeStuff
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("DSPGAME.exe")]
    public class ModEntry : BaseUnityPlugin
    {

        public static ManualLogSource _logger;
        private const string PluginGuid = "Captncraig.FreeStuff";
        private const string PluginName = "Free Stuff";
        private const string PluginVersion = "1.0.1.0";

        public void Awake()
        {
            Patches._logger = base.Logger;
            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll(typeof(Patches));
        }
        public void Start()
        {
            _logger = base.Logger;
        }
    }
}
