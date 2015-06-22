using UnityEngine;
using System.Collections;


/// <summary>
/// リザルト表示
/// </summary>
public class ResultPhase : IState
{
	string stageName;
	string levelName;

	public ResultPhase(BattleData data)
	{
		stageName = data.StageName;
		levelName = data.LevelName;
	}
	
	public void Initialize()
	{
		// プレイヤーのタッチ操作判定を切る
		PlayerController.Instance.ControlState(false);

		// リザルトの表示
		BattleUI.Instance.ShowResult(stageName, levelName, delegate{
			AppUtils.Sound.Instance.StopBGM(1f);
			SceneManager.Instance.SetState(SceneState.Home);
		});

		// データの保管
		BattleRecord.Instance.SaveRecord();
	}

	public void Update(){}

	public void Exit(){}
}
