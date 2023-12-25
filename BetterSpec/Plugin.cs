using BepInEx;
using BepInEx.Logging;
using BetterSpec.Patches;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSpec
{
    [BepInPlugin(modGUID, modName, modVersion)]

    public class MainBetterSpec : BaseUnityPlugin
    {


        private const string modGUID = "zg.BetterSpec";
        private const string modName = "BetterSpec";
        private const string modVersion = "1.0.6";
        private readonly Harmony harmony = new Harmony(modGUID);
        private static MainBetterSpec instance;

        internal ManualLogSource pnt;



        void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }

            pnt = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            pnt.LogInfo("BetterSpec Enabled");
            pnt.LogInfo("Make sure to review BetterSpec");

            harmony.PatchAll(typeof(MainBetterSpec));
            harmony.PatchAll(typeof(HUDManagerPatches));
        }

    }
}
