using UnityEngine;
using System.Collections;


/// <summary>
/// バトルフェーズのエネミーを全滅させた場合
/// </summary>
public class BattleEndPhase : IState
{
	BattlePhaseCycle phaseCycle;
	EnemiesManager enemiesManager;

	public BattleEndPhase(BattlePhaseCycle phaseCycle, EnemiesManager enemiesManager)
	{
		this.phaseCycle = phaseCycle;
		this.enemiesManager = enemiesManager;
	}

	public void Initialize()
	{
		FieldManager.Instance.OpenCenterHole();
	}

	public void Update()
	{
		if (PlayerController.Instance.NowRail < 0)
		{
			enemiesManager.PhaseEnd();
			phaseCycle.NextPhase();
		}
	}

	public void Exit()
	{

	}
}
