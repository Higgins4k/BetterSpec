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

            if (!GameNetworkManager.Instance.gameHasStarted)
            {
                return;
            }

            if (__instance == null)
            {
                Debug.LogError("HUDManagerPatches: __instance is null!");
                return;
            }

            if (HUDManager.Instance == null)
            {
                Debug.LogError("HUDManagerPatches: HUDManager.Instance is null!");
                return;
            }

            if (HUDManager.Instance.Clock == null)
            {
                Debug.LogError("HUDManagerPatches: Clock in HUDManager is null!");
                return;
            }

            if (GameNetworkManager.Instance == null)
            {
                Debug.LogError("HUDManagerPatches: GameNetworkManager.Instance is null!");
                return;
            }

            if (GameNetworkManager.Instance.localPlayerController == null)
            {
                Debug.LogError("HUDManagerPatches: localPlayerController is null!");
                return;
            }

            if (GameNetworkManager.Instance.localPlayerController.isPlayerDead)
            {
                if (HUDManager.Instance.Clock != null)
                {
                    HUDManager.Instance.HideHUD(false);
                    HUDManager.Instance.SetClockVisible(true);
                    HUDManager.Instance.Clock.targetAlpha = 1f;
                    HUDManager.Instance.Inventory.targetAlpha = 0f;
                    HUDManager.Instance.PlayerInfo.targetAlpha = 0f;


                }
                else
                {
                    Debug.LogError("Clock in HUDManager is null!");
                }
            }
            else
            {
                HUDManager.Instance.PlayerInfo.targetAlpha = 1f;
                HUDManager.Instance.Inventory.targetAlpha = 1f;
            }


        }
    }
}
