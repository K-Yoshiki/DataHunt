using UnityEngine;
using System.Collections;


public class GameOverPhase : IState
{
	BattlePhaseCycle phaseCycle;

	public GameOverPhase(BattlePhaseCycle phaseCycle)
	{
		this.phaseCycle = phaseCycle;
	}

	public void Initialize()
	{
		PlayerController.Instance.ControlState(false);
		FieldManager.Instance.GameOverAnim();
		BattleUI.Instance.ShowGameOver(PressGameOverButton, phaseCycle.ContinueCount);
	}

	public void Update()
	{

	}

	public void Exit()
	{
		FieldManager.Instance.RailReset();
	}

	/// <summary>
	/// ゲームオーバーボタンの結果を受け取る
	/// </summary>
	/// <param name="isPositive">If set to <c>true</c> is positive.</param>
	void PressGameOverButton(bool isPositive)
	{
		if (isPositive)
		{
			phaseCycle.ContinuePhase();
		}
		else
		{
			SceneManager.Instance.SetState(SceneState.Home);
		}
	}
}
