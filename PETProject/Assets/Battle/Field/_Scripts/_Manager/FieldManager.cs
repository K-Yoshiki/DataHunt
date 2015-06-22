using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// フィールド構築、管理マネージャー
/// </summary>
public class FieldManager : MonoSingleton<FieldManager>
{
	// 現在の半径集
	// 5, 7.5, 10, 13, 16, 19, 22, 25(emptyRail)
	[SerializeField]
	List<float> radiuses = new List<float>();
	
	[SerializeField]
	RailController railController;
	
	[SerializeField]
	RailAnimation railAnimation;
	
	[SerializeField]
	SkyboxController skyboxPrefab;
	SkyboxController skyboxController;

	[SerializeField]
	CenterHole centerHole;


	/// <summary>
	/// フィールド管理クラスの初期化
	/// </summary>
	public void Initialize()
	{
		radiuses.Sort();
		
		Transform temp = skyboxPrefab.transform;
		skyboxController = Instantiate(skyboxPrefab, temp.position, temp.rotation) as SkyboxController;
		centerHole = Instantiate(centerHole) as CenterHole;
	}
	
	/// <summary>
	/// 指定レールの半径の大きさを取得します
	/// </summary>
	/// <returns>The radius.</returns>
	/// <param name="railNum">Rail number.</param>
	public float GetRadius(int railNum)
	{
		if (railNum <= -1)
			return 0;
		if (railNum > radiuses.Count - 1)
			return radiuses[radiuses.Count - 1];

		return radiuses[railNum];
	}

	/// <summary>
	/// 現在のレール数を取得します
	/// </summary>
	/// <returns>The count.</returns>
	public int RailCount()
	{
		return railController.GetRings;
	}

	/// <summary>
	/// 指定レール数をフィールドに設置します
	/// </summary>
	/// <param name="rails">Rails.</param>
	public void SetRails(int rails)
	{
		railController.SetRails((short)rails);
	}

	/// <summary>
	/// 次のフィールドの準備
	/// </summary>
	/// <param name="rails">Rails.</param>
	/// <param name="phaseNum">Phase number.</param>
	/// <param name="endCallback">End callback.</param>
	public void NextField(int rails, int phaseNum, Action endCallback)
	{
		// フィールド構築終了時の処理を作成
		Action endAnim = delegate {
			EndAnim(rails, endCallback);
		};

		// フェーズのラベル表示の処理を作成
		Action labelShow = delegate {
			BattleUI.Instance.ShowNormalPhaseLabel(phaseNum, endAnim);
		};

		// フィールド構築の開始
		StartAnim(labelShow);
	}

	/// <summary>
	/// ボスフィールドの準備
	/// </summary>
	/// <param name="rails">Rails.</param>
	/// <param name="endCallback">End callback.</param>
	public void NextFieldBoss(int rails, Action endCallback)
	{
		Action endAnim = delegate {
			centerHole.HoleLost();
			EndAnim(rails, endCallback);
		};

		Action labelShow = delegate {
			BattleUI.Instance.ShowBossPhaseLabel(endAnim);
		};

		StartAnim(labelShow);
	}

	/// <summary>
	/// ゲームオーバー時のスクロールアニメーション
	/// </summary>
	public void GameOverAnim()
	{
		// Skyboxのスクロールアニメーションの開始
		skyboxController.StartFallAnimation();
		
		// ステージリングの上昇アニメーション
		railAnimation.FallStart();

		// センターホールのアニメーション
		centerHole.StartAnim();
	}

	/// <summary>
	/// 中央ホールをオープン状態に
	/// </summary>
	public void OpenCenterHole()
	{
		centerHole.ResetColor(1.0f);
	}

	/// <summary>
	/// レール数を0にする
	/// </summary>
	public void RailReset()
	{
		railController.SetRails(0);
	}

	/// <summary>
	/// フィールド移行アニメーションの開始
	/// </summary>
	/// <param name="labelShow">Label show.</param>
	void StartAnim(Action labelShow)
	{
		// Skyboxのスクロールアニメーションの開始
		skyboxController.StartFallAnimation();
		
		// ステージリングの上昇アニメーション
		railAnimation.FallStart();

		// センターホールのアニメーション
		centerHole.StartAnim();

		// フェーズラベルの表示
		labelShow();
	}

	/// <summary>
	/// フィールド移行アニメーションの終了
	/// </summary>
	/// <param name="rails">Rails.</param>
	/// <param name="endCallBack">End call back.</param>
	void EndAnim(int rails, Action endCallBack)
	{
		Action animStop = delegate {
			skyboxController.EndFallAnimation();
			centerHole.StopAnim();
			endCallBack();
		};

		centerHole.ChangeColor(0.25f);
		railController.SetRails((short)rails);
		PlayerController.Instance.SetStartRail();
		railAnimation.FallEnd(animStop);
	}
}

/// <summary>
/// レール移動時の方向
/// </summary>
public enum RailVec
{
	/// <summary>
	/// 内側
	/// </summary>
	Inner,

	/// <summary>
	/// 外側
	/// </summary>
	Outer
}