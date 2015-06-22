using UnityEngine;
using System;
using System.Collections;
using AppUtils;


/// <summary>
/// Moving Phase
/// </summary>
public class ChangePhase : IState
{
	PhaseData phaseData;
	BattlePhaseCycle phaseCycle;
	byte phaseNum;
	PhaseType phaseType;

	public ChangePhase(PhaseData phaseData, BattlePhaseCycle phaseCycle, PhaseType phaseType, byte phaseNum)
	{
		this.phaseData = phaseData;
		this.phaseCycle = phaseCycle;
		this.phaseType = phaseType;
		this.phaseNum = phaseNum;
	}

	public void Initialize()
	{
		// プレイヤーのタッチ操作判定を切る
		PlayerController.Instance.ControlState(false);
		PlayerController.Instance.ClearBullet();

		// 次のフェーズ場への移動処理
		if (phaseType != PhaseType.Boss)
		{
			BattleSound.Instance.PlayBattleBGM();
			FieldManager.Instance.NextField(phaseData.Rails, phaseNum, ChangePhaseEnd);
		}
		else
		{
			Sound.Instance.StopBGM(0.5f);
			FieldManager.Instance.NextFieldBoss(phaseData.Rails, ChangePhaseEnd);
		}
	}

	/// <summary>
	/// フィールド移行フェーズの終了処理
	/// </summary>
	void ChangePhaseEnd()
	{
		PlayerController.Instance.ControlState(true);
		phaseCycle.NextPhase();
	}

	public void Update()
	{
		
	}

	public void Exit()
	{
		if (phaseType == PhaseType.Boss)
			BattleSound.Instance.PlayBossBGM();
	}	
}