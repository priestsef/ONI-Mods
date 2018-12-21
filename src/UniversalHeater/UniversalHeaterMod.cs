using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Harmony;
using TUNING;

namespace UniversalHeater
{
    public class UniversalHeaterConfigMod
    {

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class UniversalHeaterPatch
        {
            public static LocString NAME = "Universal Heater";
            public static LocString DESC = "This building will help you " + STRINGS.UI.FormatAsLink("heat", "TEMPERATURE") + " all the items up.";
            public static LocString EFFECT = "Universal Heater is provided to you by " + STRINGS.UI.FormatAsLink("Korriban Dynamics", "KORRIBANDYNAMICS") + ".";

            private static void Prefix()
            {
                Strings.Add("STRINGS.BUILDINGS.PREFABS.UNIVERSALHEATER.NAME", NAME);
                Strings.Add("STRINGS.BUILDINGS.PREFABS.UNIVERSALHEATER.DESC", DESC);
                Strings.Add("STRINGS.BUILDINGS.PREFABS.UNIVERSALHEATER.EFFECT", EFFECT);

                List<string> category = (List<string>)BUILDINGS.PLANORDER.First(po => po.category == new HashedString("Utilities")).data;
                category.Add(UniversalHeaterConfig.ID);
            }

         private static void Postfix()
            {
                object obj = Activator.CreateInstance(typeof(UniversalHeaterConfig));
                BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class UniversalHeaterDbPatch
        {
            private static void Prefix()
            {
                List<string> ls = new List<string>(Techs.TECH_GROUPING["HighTempForging"]){UniversalHeaterConfig.ID};
                Techs.TECH_GROUPING["HighTempForging"] = ls.ToArray();                
            }
        }

    }
}
