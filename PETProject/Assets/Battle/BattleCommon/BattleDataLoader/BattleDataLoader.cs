using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// バトルデータ読込クラス
/// </summary>
public static class BattleDataLoader
{
	/// <summary>
	/// 指定された情報で, バトルデータを読み出します
	/// </summary>
	/// <param name="prefabName">バトルデータのプレハブ名を指定します.</param>
	/// <param name="stageName">バトルデータに格納するステージ名を指定します.</param>
	/// <param name="levelName">バトルデータに格納するレベル名を指定します.</param>
	public static BattleData GetBattleData(string prefabName, string stageName, string levelName)
	{
		string path = "BattleData/DataPrefabs/" + prefabName;
		BattleData data = Resources.Load<BattleData>(path);
		
		if (data != null)
		{
			data.StageName = stageName;
			data.LevelName = levelName;
		}
		return data;
	}

	/// <summary>
	/// 全てのステージ名とレベル名のリストを取得します
	/// </summary>
	/// <returns>The stage name package.</returns>
	public static List<StageNamePackage> GetStageNameList()
	{
		string path = "BattleData/BattleNames";
		BattleNames loader = Resources.Load<BattleNames>(path);
				
		if (loader != null)
		{
			return loader.stageNameList;
		}
		return null;
	}
}