using BepInEx;
using HarmonyLib;
using NetcodeTutorial.Managers;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace NetcodeTutorial
{
    [BepInPlugin(GUID,NAME,VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        const string GUID = "malco.networkTutorial";
        const string NAME = "Network Tutorial";
        const string VERSION = "0.0.1";

        private readonly Harmony harmony = new Harmony(GUID);

        public static Plugin instance;

        public GameObject netManagerPrefab;


        void Awake()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                foreach (var method in methods)
                {
                    var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                    if (attributes.Length > 0)
                    {
                        method.Invoke(null, null);
                    }
                }
            }
            instance = this;

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "netcodemod");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            netManagerPrefab = bundle.LoadAsset<GameObject>("Assets/NetcodeTutorial/NetworkManagerMyMod.prefab");
            netManagerPrefab.AddComponent<NetworkManagerMyMod>();

            harmony.PatchAll();
            Logger.LogInfo("Patched network Tutorial");
        }
    }
}
