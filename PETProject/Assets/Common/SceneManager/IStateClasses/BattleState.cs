using UnityEngine;
using System.Collections;

public class BattleState : IState
{
	public void Initialize()
	{
		AppUtils.Sound.Instance.StopBGM(0.5f);
		if(Application.loadedLevelName != "Battle")
		{
			string text = "";
			BattleData data = SceneManager.Instance.GetSceneData<BattleData>();
			if (data != null)
			{
				text = string.Format("{0}\n{1}", data.StageName, data.LevelName);
			}
			Fade.Instance.FadeSceneLoad("Battle", text, Color.black, 1.0f, 1.0f, BattleStart);
		}
		else
		{
			// debug battle
			BattleStart();
		}
		Debug.Log("BattleState : Initialize");
	}

	void BattleStart()
	{
		BattleManager.Instance.StartBattle();
	}

	public void Update()
	{

	}

	public void Exit()
	{

	}
}