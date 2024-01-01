using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BetterSpec.Patches
{

    [HarmonyPatch(typeof(HUDManager))]
    internal class SpecInfoHud
    {
        private static TextMeshProUGUI specInfo;
        public static HUDManager hud = HUDManager.Instance;
        public int num2 = 0;
        int num4 = 0;


        public string itemsInside()
        {
            System.Random random = new System.Random(StartOfRound.Instance.randomMapSeed + 91);
            
            int num3 = 0;
            GrabbableObject[] objectsOfType = UnityEngine.Object.FindObjectsOfType<GrabbableObject>();
            for (int index = 0; index < objectsOfType.Length; ++index)
            {
                if (objectsOfType[index].itemProperties.isScrap && objectsOfType[index].isInFactory && !objectsOfType[index].name.Equals("Key"))
                {
                    num4 += objectsOfType[index].itemProperties.maxValue - objectsOfType[index].itemProperties.minValue;
                    num3 += Mathf.Clamp(random.Next(objectsOfType[index].itemProperties.minValue, objectsOfType[index].itemProperties.maxValue), objectsOfType[index].scrapValue - 6 * index, objectsOfType[index].scrapValue + 9 * index);
                    ++num2;
                }
            }
            return num2.ToString();
        }

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        static void modifyWeightText()
        {
            if (!GameNetworkManager.Instance.gameHasStarted)
            {
                specInfo.alpha = 0f;
                return;
            }

            if (specInfo == null)
            {
                return;
            }
            if (!GameNetworkManager.Instance.localPlayerController.isPlayerDead)
            {
                SpecInfoHud specInfoInstance = new SpecInfoHud();
                specInfoInstance.itemsInside();
                int alive = RoundManager.Instance.numberOfEnemiesInScene;
                specInfo.alpha = 0f;
                specInfo.text = "Items Remaining Inside: " + specInfoInstance.num2.ToString() + "\nValue Remaining: $" + specInfoInstance.num4.ToString();
            }

                if (GameNetworkManager.Instance.localPlayerController.isPlayerDead)
                {
                    specInfo.alpha = 1f;
                }
        }

        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        static void createSpecElm(ref HUDManager __instance)
        {
            SpecInfoHud specInfoInstance = new SpecInfoHud();
            specInfoInstance.itemsInside();
            GameObject spec = new GameObject("SpectatorInfo");
            spec.AddComponent<RectTransform>();
            TextMeshProUGUI specBoxTemp = spec.AddComponent<TextMeshProUGUI>();
            RectTransform rectTransform = specBoxTemp.rectTransform;
            rectTransform.SetParent(__instance.PTTIcon.transform, false);
            rectTransform.anchoredPosition = new Vector2(400f, -90f);
            specBoxTemp.font = __instance.controlTipLines[0].font;
            specBoxTemp.fontSize = 9f;
            specBoxTemp.enabled = true;
            specBoxTemp.color = Color.green;
            specBoxTemp.text = "Items Remaining Inside: " + specInfoInstance.num2.ToString() + "\nValue Remaining: $" + specInfoInstance.num4.ToString();
            specBoxTemp.overflowMode = (TextOverflowModes)0;
            specInfo = specBoxTemp;
        }



    }
}
