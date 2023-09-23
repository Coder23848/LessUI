using BepInEx;
using MoreSlugcats;
using UnityEngine;

namespace LessUI
{
    [BepInPlugin("com.coder23848.lessui", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
#pragma warning disable IDE0051 // Visual Studio is whiny
        private void OnEnable()
#pragma warning restore IDE0051
        {
            // Plugin startup logic
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.Menu.SleepAndDeathScreen.GetDataFromGame += SleepAndDeathScreen_GetDataFromGame;
        }

        private void SleepAndDeathScreen_GetDataFromGame(On.Menu.SleepAndDeathScreen.orig_GetDataFromGame orig, Menu.SleepAndDeathScreen self, Menu.KarmaLadderScreen.SleepDeathScreenDataPackage package)
        {
            orig(self, package);
            // Delete kill feed
            if (PluginOptions.RemoveKillFeed.Value && package.characterStats.name != SlugcatStats.Name.Red)
            {
                self.pages[0].RemoveSubObject(self.killsDisplay);
                self.killsDisplay = null;
            }
            // Delete token tracker
            if (PluginOptions.RemoveTokenTracker.Value)
            {
                for (int i = 0; i < self.pages[0].subObjects.Count; i++)
                {
                    if (self.pages[0].subObjects[i] is CollectiblesTracker ct)
                    {
                        self.pages[0].RemoveSubObject(ct);
                    }
                }
            }
        }

        private void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);
            Debug.Log("Less UI config setup: " + MachineConnector.SetRegisteredOI(PluginInfo.PLUGIN_GUID, PluginOptions.Instance));
        }
    }
}