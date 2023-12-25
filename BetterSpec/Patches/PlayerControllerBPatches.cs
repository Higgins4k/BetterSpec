using GameNetcodeStuff;
using HarmonyLib;
using System;
using UnityEngine;

namespace BetterSpec.Patches
{
    
    internal class HUDManagerPatches
    {
        [HarmonyPatch(typeof(HUDManager), "Update")]
        [HarmonyPrefix]
        static void specClockPatch(HUDManager __instance)
        {
            if (HUDManager.Instance == null)
            {
                return;
            }

            if (GameNetworkManager.Instance.localPlayerController.isPlayerDead)
            {
                if (HUDManager.Instance.Clock != null)
                {
                    HUDManager.Instance.HideHUD(false);
                    HUDManager.Instance.SetClockVisible(true);
                    HUDManager.Instance.Clock.targetAlpha = 1f;
                    HUDManager.Instance.weightCounter.text = "";
                    HUDManager.Instance.Inventory.targetAlpha = 0f;
                    HUDManager.Instance.PlayerInfo.targetAlpha = 0f;


                }
                else
                {
                    Debug.LogError("Clock in HUDManager is null!");
                }
            }
        }
    }
}
