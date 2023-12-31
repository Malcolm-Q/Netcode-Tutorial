using GameNetcodeStuff;
using Unity.Netcode;

namespace NetcodeTutorial.Managers
{
    public class NetworkManagerMyMod : NetworkBehaviour
    {
        public static NetworkManagerMyMod instance;

        void Awake()
        {
            instance = this;
        }


        [ServerRpc(RequireOwnership = false)]
        public void RequestDeathNotificationServerRpc(ulong id)
        {
            DeathNotificationClientRpc(id);
        }

        [ClientRpc]
        public void DeathNotificationClientRpc(ulong id)
        {
            string name = StartOfRound.Instance.allPlayerObjects[(int)id].GetComponent<PlayerControllerB>().playerUsername;
            HUDManager.Instance.DisplayTip("Player Has Died", $"{name} is now dead");
        }
    }
}
