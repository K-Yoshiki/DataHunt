using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButton : MonoBehaviour {

	StageNamePackage stageName;
	int selfIndex;

	public GameObject subMissionPanel;
	public Text levelNameLabel;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Initialize(StageNamePackage stageName, int selfIndex)
	{
		this.stageName = stageName;
		this.selfIndex = selfIndex;
		levelNameLabel.text = stageName.levelNames[selfIndex].levelName;
	}

	public void DestroyLevelButton()
	{
		foreach ( Transform n in subMissionPanel.transform )
		{
			GameObject.Destroy(n.gameObject);
		}
	}

	public void OnClickLevelButton()
	{
		var level = stageName.levelNames[selfIndex];
		BattleData data = BattleDataLoader.GetBattleData(level.prefabName, stageName.stageName, level.levelName);
		SceneManager.Instance.SetSceneData(data);
		if (data.isTutorial)
			SceneManager.Instance.SetState(SceneState.Loading);
		else
			SceneManager.Instance.SetState(SceneState.Battle);
	}
}
