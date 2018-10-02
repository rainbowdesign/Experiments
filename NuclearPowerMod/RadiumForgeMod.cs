using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Harmony;
using UnityEngine;

[HarmonyPatch(typeof(GlassForgeConfig), "ConfigureBuildingTemplate", null)]
public static class RadiumForge
{
	public static void Postfix()
	{

		ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[3]
	{
		new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Steel).tag, 25f),
		new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Glass).tag, 25f),
		new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.SolidNaphtha).tag, 100f)
	};
		ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
		{
	  new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.LiquidPropane).tag, 100f)
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