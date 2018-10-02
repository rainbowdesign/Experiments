using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using UnityEngine;

[HarmonyPatch(typeof(GlassForgeConfig), "ConfigureBuildingTemplate", null)]
public static class MercuryMod
{
	public static void Postfix()
	{

		ElementLoader.FindElementByHash(SimHashes.SolidMercury).highTemp = 1000f;
		ElementLoader.FindElementByHash(SimHashes.Mercury).lowTemp = 10f;
		ElementLoader.FindElementByHash(SimHashes.Mercury).highTemp = 2400f;
		ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
	{
		new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Tungsten).tag, 50f),
		new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.MaficRock).tag, 50f),
		new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Obsidian).tag, 100f)
	};
		ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
		{
	  new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Mercury).tag, 200f)
		};
		string obsolete_id = ComplexRecipeManager.MakeObsoleteRecipeID("GlassForge", ingredients[0].material);
		string str = ComplexRecipeManager.MakeRecipeID("GlassForge", (IList<ComplexRecipe.RecipeElement>)ingredients, (IList<ComplexRecipe.RecipeElement>)results);
		new ComplexRecipe(str, ingredients, results)
		{
			time = 40f,
			useResultAsDescription = true,
			description = string.Format((string)STRINGS.BUILDINGS.PREFABS.GLASSFORGE.RECIPE_DESCRIPTION, (object)ElementLoader.GetElement(results[0].material).name, (object)ElementLoader.GetElement(ingredients[0].material).name)

		}.fabricators = new List<Tag>()
	{
	  TagManager.Create("GlassForge")
	};
		ComplexRecipeManager.Get().AddObsoleteIDMapping(obsolete_id, str);
	}
}