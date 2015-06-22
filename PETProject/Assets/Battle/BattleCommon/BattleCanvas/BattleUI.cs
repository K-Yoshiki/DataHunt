using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// バトルシーンのUI管理
/// </summary>
public class BattleUI : MonoSingleton<BattleUI>
{
	/// <summary>
	/// フェーズ表示
	/// </summary>
	[SerializeField]
	PhaseLabelController phaseLabelCtrl;

	/// <summary>
	/// ボスHP表示
	/// </summary>
	[SerializeField]
	BossHPScreen bossHpScreen;

	/// <summary>
	/// ゲームオーバー表示
	/// </summary>
	[SerializeField]
	GameOverScreen gameOverScreen;

	/// <summary>
	/// リザルト表示
	/// </summary>
	[SerializeField]
	ResultScreen resultScreen;

	/// <summary>
	/// ダメージ用のパネル
	/// </summary>
	[SerializeField]
	GameObject damagePanel;


	/// <summary>
	/// ゲームオーバー画面の表示
	/// </summary>
	public void ShowGameOver(Action<bool> pressButton, byte continueCount)
	{
		gameOverScreen.Show(pressButton, continueCount);
	}
	
	/// <summary>
	/// 通常のフェーズラベルを表示する
	/// </summary>
	public void ShowNormalPhaseLabel(int phaseNum, Action callback)
	{
		phaseLabelCtrl.NormalPhaseLabel(phaseNum, callback);
	}

	/// <summary>
	/// ボス専用のフェーズラベルを表示する
	/// </summary>
	public void ShowBossPhaseLabel(Action callback)
	{
		phaseLabelCtrl.BossPhaseLabel(callback);
	}

	/// <summary>
	/// ボスのHPを表示する
	/// </summary>
	/// <param name="boss">Boss.</param>
	public void ShowBossHP(Boss boss)
	{
		bossHpScreen.ShowBossHP(boss);
	}

	/// <summary>
	/// ボスのHP表示を閉じる
	/// </summary>
	public void CloseBossHP()
	{
		bossHpScreen.CloseBossHP();
	}

	/// <summary>
	/// リザルト画面の表示
	/// </summary>
	public void ShowResult(string stageName, string levelName, Action callback)
	{
		resultScreen.ShowResult(stageName, levelName, callback);
	}

	/// <summary>
	/// プレイヤーのダメージ表現
	/// </summary>
	public void PlayerDamage()
	{
		damagePanel.SetActive(true);
	}
}
