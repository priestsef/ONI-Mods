using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Harmony;
using TUNING;

namespace UniversalCooler
{
    public class UniversalCoolerConfigMod
    {

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class UniversalCoolerPatch
        {
            public static LocString NAME = "Universal Cooler";
            public static LocString DESC = "This building will help you " + STRINGS.UI.FormatAsLink("cool", "TEMPERATURE") + " all the items down.";
            public static LocString EFFECT = "Universal Cooler is provided to you by " + STRINGS.UI.FormatAsLink("Korriban Dynamics", "KORRIBANDYNAMICS") + ".";

            private static void Prefix()
            {
                Strings.Add("STRINGS.BUILDINGS.PREFABS.UNIVERSALCOOLER.NAME", NAME);
                Strings.Add("STRINGS.BUILDINGS.PREFABS.UNIVERSALCOOLER.DESC", DESC);
                Strings.Add("STRINGS.BUILDINGS.PREFABS.UNIVERSALCOOLER.EFFECT", EFFECT);

                List<string> category = (List<string>)BUILDINGS.PLANORDER.First(po => po.category == new HashedString("Utilities")).data;
                category.Add(UniversalCoolerConfig.ID);
            }

         private static void Postfix()
            {
                object obj = Activator.CreateInstance(typeof(UniversalCoolerConfig));
                BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class UniversalCoolerDbPatch
        {
            private static void Prefix()
            {
                List<string> ls = new List<string>(Techs.TECH_GROUPING["HighTempForging"]){UniversalCoolerConfig.ID};
                Techs.TECH_GROUPING["HighTempForging"] = ls.ToArray();                
            }
        }

    }
}
