using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogisticsSubnets
{
	public static class SubnetPatches
	{
		public static ManualLogSource _logger;

		private static Regex reg = new Regex(@"\[([^\]]*)\]$");

		[HarmonyPrefix]
		[HarmonyPatch(typeof(StationComponent), "RematchLocalPairs", typeof(StationComponent[]), typeof(int), typeof(int), typeof(int)) ]
		public static bool RematchLocalPairs_Prefix(StationComponent __instance, ref StationComponent[] stationPool)
		{
			stationPool = Clean(__instance.name, stationPool);
			return true;
		}

		private static StationComponent[] Clean (string name, StationComponent[] stationPool)
        {
			var a = new StationComponent[stationPool.Length];
			for (var i = 0; i<stationPool.Length; i++)
            {
				var s = stationPool[i];
				if (s != null && Subnet(s.name) == Subnet(name))
                {
					a[i] = s;
                }
                else
                {
					a[i] = null;
                }
            }
			return a;
        }

		private static string Subnet(string name)
        {
			if (name == null) return "";
			var match = reg.Match(name);
			var tag = match.Success? match.Groups[1].ToString() : "";
			return tag;
        }

		[HarmonyPrefix]
		[HarmonyPatch(typeof(StationComponent), "RematchRemotePairs", typeof(StationComponent[]), typeof(int), typeof(int), typeof(int))]
		public static bool RematchRemotePairs_Prefix(StationComponent __instance, ref StationComponent[] gStationPool)
		{
			gStationPool = Clean(__instance.name, gStationPool);
			return true;
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(UIStationWindow), "OnNameInputSubmit")]
		public static void OnNameInputSubmit_Postfix(UIStationWindow __instance)
		{
			var planet = __instance.transport;
			planet.RefreshTraffic(__instance.stationId);
			var galaxy = planet.gameData.galacticTransport;
			galaxy.RefreshTraffic(__instance.stationId);
		}
	}
}
