using GameNetcodeStuff;
using HarmonyLib;
using NetcodeTutorial.Managers;

namespace NetcodeTutorial.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatcher
    {

        [HarmonyPrefix]
        [HarmonyPatch("KillPlayer")]
        static void NotifyPlayersOfDeath(PlayerControllerB __instance)
        {
            if (!__instance.IsOwner)
            {
                return;
            }
            if (__instance.isPlayerDead)
            {
                return;
            }
            if (!__instance.AllowPlayerDeath())
            {
                return;
            }
            if(__instance.IsHost || __instance.IsServer)
            {
                NetworkManagerMyMod.instance.DeathNotificationClientRpc(__instance.playerClientId);
            }
            else
            {
                NetworkManagerMyMod.instance.RequestDeathNotificationServerRpc(__instance.playerClientId);
            }
        }
    }
}
