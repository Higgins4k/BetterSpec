using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace BetterSpec.Patches
{
    
    internal class HUDManagerPatches
    {
        [HarmonyPatch(typeof(HUDManager), "Update")]
        [HarmonyPrefix]
        static void specClockPatch(HUDManager __instance)
        {
            if (__instance == null)
            {
                return;
            }

            if (GameNetworkManager.Instance.localPlayerController.isPlayerDead)
            {
                if (__instance.Clock != null)
                {
                    HUDManager.Instance.HideHUD(false);
                    HUDManager.Instance.SetClockVisible(true);
                    HUDManager.Instance.Clock.targetAlpha = 1f;
                   

                }
                else
                {
                    Debug.LogError("Clock in HUDManager is null!");
                }
            }
        }
    }
}
