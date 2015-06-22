using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class TestLoadBattleNames : MonoBehaviour
{
	public Text label;
	public Text label2;
	List<StageNamePackage> names;
	LevelNamePackage levelName;

	// Use this for initialization
	void Start()
	{
		label.text = "";
		//label2.text = "";
		names = BattleDataLoader.GetStageNameList();

		foreach(var stageNamePack in names)
		{
			label.text += stageNamePack.stageName + "\n";

			StageName(stageNamePack);
		}
	}


	// Button
	void StageName(StageNamePackage stageNamePack)
	{
//		StageNamePackage pack = stageNamePack;

		foreach (var level in stageNamePack.levelNames)
		{
			LevelName(level);
//			BattleData data = BattleDataLoader.GetBattleData(level.prefabName, stageNamePack.stageName, level.levelName);
//			SceneManager.Instance.SetSceneData(data);
//			SceneManager.Instance.SetState(SceneState.Battle);
		}
	}

	void LevelName(LevelNamePackage levelNamePack)
	{
		label.text += "  " + levelNamePack.levelName + "\n";
		Debug.Log(levelNamePack.levelName);
	}
}
