using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using UnityEngine;

namespace RadiumFuelMod
{

	[HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
	internal class RadiumFuelMod_GeneratedBuildings_LoadGeneratedBuildings
	{
		private static void Prefix()
		{
			Debug.Log(" === GeneratedBuildings Prefix === " + RadiumFuelConfig.ID);
			Strings.Add("STRINGS.BUILDINGS.PREFABS.RADIUMFUELCELL.NAME", "Radium Fuel Cell");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.RADIUMFUELCELL.DESC", "The Radium Fuel Cell provides heat over time.");
			Strings.Add("STRINGS.BUILDINGS.PREFABS.RADIUMFUELCELL.EFFECT", "Place the Radium Fuel Cell to supply a steam engine with heat.");

			List<string> ls = new List<string>((string[])TUNING.BUILDINGS.PLANORDER[10].data);
			ls.Add(RadiumFuelConfig.ID);
			TUNING.BUILDINGS.PLANORDER[10].data = (string[])ls.ToArray();

			TUNING.BUILDINGS.COMPONENT_DESCRIPTION_ORDER.Add(RadiumFuelConfig.ID);


		}
		private static void Postfix()
		{

			Debug.Log(" === GeneratedBuildings Postfix === " + RadiumFuelConfig.ID);
			object obj = Activator.CreateInstance(typeof(RadiumFuelConfig));
			BuildingConfigManager.Instance.RegisterBuilding(obj as IBuildingConfig);
			Db.Get().Diseases.Add(new Klei.AI.Radioactive());
		}
	}

	[HarmonyPatch(typeof(Db), "Initialize")]
	internal class RadiumFuelMod_Db_Initialize
	{
		private static void Prefix(Db __instance)
		{
			List<string> ls = new List<string>((string[])Database.Techs.TECH_GROUPING["RenewableEnergy"]);
			ls.Add(RadiumFuelConfig.ID);
			Database.Techs.TECH_GROUPING["RenewableEnergy"] = (string[])ls.ToArray();

			//Database.Techs.TECH_GROUPING["TemperatureModulation"].Add("InsulatedPressureDoor");
		}
	}
}