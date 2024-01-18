using BepInEx;
using BepInEx.Configuration;
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
        private const string modVersion = "1.1.3";
        private readonly Harmony harmony = new Harmony(modGUID);
        public static MainBetterSpec instance;

        internal ManualLogSource pnt;

        public ConfigEntry<bool> seeItemsRemaining;
        public ConfigEntry<bool> seeValueRemaining;
        public ConfigEntry<bool> seeConfigNotice;

        void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }

            BindConfiguration();

            pnt = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            pnt.LogInfo("BetterSpec Enabled");
            pnt.LogInfo("Make sure to review BetterSpec");

            harmony.PatchAll(typeof(MainBetterSpec));
            harmony.PatchAll(typeof(HUDManagerPatches));
            harmony.PatchAll(typeof(SpecInfoHud));


        }

        private void BindConfiguration()
        {
            seeValueRemaining = Config.Bind("General", "See Value Remaining", false, "Show Value Remaining when spectating");
            seeItemsRemaining = Config.Bind("General", "See Items Remaining Inside", false, "Show Items Remaining when spectating");
            seeConfigNotice = Config.Bind("General", "Config Notice Display", true, "Shows the messasge informing of new Config");
        }

    }
}
