using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace FreeStuff
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("DSPGAME.exe")]
    public class ModEntry : BaseUnityPlugin
    {
        private const string PluginGuid = "Captncraig.FreeStuff";
        private const string PluginName = "Free Stuff";
        private const string PluginVersion = "1.0.2.0";

        public void Awake()
        {
            Harmony harmony = new Harmony(PluginGuid);
            harmony.PatchAll(typeof(Patches));
        }
    }
}
