using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace NetcodeTutorial.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatcher
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        static void spawnNetManager(StartOfRound __instance)
        {
            if(__instance.IsHost)
            {
                GameObject go = GameObject.Instantiate(Plugin.instance.netManagerPrefab);
                go.GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}
