using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// Battle Phase
/// </summary>
public class BattlePhase : IState
{
	PhaseData phaseData;
	BattlePhaseCycle phaseCycle;
	EnemiesManager enemiesManager;
	PhaseType phaseType;


	public BattlePhase(PhaseData phaseData, BattlePhaseCycle phaseCycle, EnemiesManager enemiesManager, PhaseType phaseType)
	{
		this.phaseData = phaseData;
		this.phaseCycle = phaseCycle;
		this.enemiesManager = enemiesManager;
		this.phaseType = phaseType;
	}
	
	public void Initialize()
	{
		enemiesManager.Initialize(phaseData.EnemySpawnList);
	}
	
	public void Update()
	{
		// プレイヤーが場外に出されたら
		if (PlayerController.Instance.IsDead)
		{
			// フィールドにいるエネミーを全て除去
			enemiesManager.AllEnemyDelete();
			enemiesManager.PhaseEnd();
			// ゲームオーバーフェーズに移行
			phaseCycle.SetPhase(new GameOverPhase(phaseCycle));
			return;
		}

		// 次のフェーズにいける状態になったら
		if (BattleManager.Instance.CanNextPhase())
		{
			if (phaseType != PhaseType.Boss)
			{
				phaseCycle.SetPhase(new BattleEndPhase(phaseCycle, enemiesManager));
				return;
			}
			phaseCycle.NextPhase();
		}
	}
	
	public void Exit()
	{
		//TODO: ここでフィールド上にある全てのエンティティを自滅させる
		
	}
}