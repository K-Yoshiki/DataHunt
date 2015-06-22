using UnityEngine;
using System.Collections.Generic;


public class BattleNames : MonoBehaviour
{
	public List<StageNamePackage> stageNameList;

	public BattleNames()
	{
		stageNameList = stageNameList ?? new List<StageNamePackage>();
	}
}

/// <summary>
/// ステージ名とレベル名のデータセット
/// </summary>
[System.Serializable]
public class StageNamePackage
{
	public string stageName;
	public List<LevelNamePackage> levelNames;
	
	public StageNamePackage()
	{
		stageName = "";
		levelNames = levelNames ?? new List<LevelNamePackage>();
	}
}

/// <summary>
/// レベル名とプレハブ名のデータセット
/// </summary>
[System.Serializable]
public class LevelNamePackage
{
	public string levelName;
	public string prefabName;

	public LevelNamePackage()
	{
		levelName = "";
		prefabName = "";
	}
}