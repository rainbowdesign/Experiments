﻿using TUNING;
using UnityEngine;

public class RadiumFuelConfig : IBuildingConfig
{
	private static readonly CellOffset[] overrideOffsets = new CellOffset[4]
	{
	new CellOffset(-1, -1),
	new CellOffset(1, -1),
	new CellOffset(-1, 1),
	new CellOffset(1, 1)
	};
	public const string ID = "RadiumFuelCell";

	public override BuildingDef CreateBuildingDef()
	{
		string id = "RadiumFuelCell";
		int width = 1;
		int height = 1;
		string anim = "thermalblock_kanim";
		int hitpoints = 30;
		float construction_time = 120f;
		float[] tieR5 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] anyBuildable = new string[1]	{	  "Radium"	}; 
		float melting_point = 8000f;
		BuildLocationRule build_location_rule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tieR5, anyBuildable, melting_point, build_location_rule, DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.ViewMode = SimViewMode.TemperatureMap;
		buildingDef.DefaultAnimState = "off";
		buildingDef.ObjectLayer = ObjectLayer.Backwall;
		buildingDef.SceneLayer = Grid.SceneLayer.TempShiftPlate;
		return buildingDef;
	}

	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		//go.AddComponent<ZoneTile>();
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.DoPostConfigure(go);
		go.GetComponent<KPrefabID>().prefabSpawnFn += (KPrefabID.PrefabFn)(game_object =>
		{
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(game_object);
			StructureTemperatureData data = GameComps.StructureTemperatures.GetData(handle);
			int cell = Grid.PosToCell(game_object);
			data.OverrideExtents(new Extents(cell, RadiumFuelConfig.overrideOffsets));
			GameComps.StructureTemperatures.SetData(handle, data);
		});
	}
}