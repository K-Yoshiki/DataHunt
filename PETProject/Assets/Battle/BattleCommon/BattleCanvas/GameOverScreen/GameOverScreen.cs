using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using AppUtils;


/// <summary>
/// ゲームオーバー画面
/// </summary>
public class GameOverScreen : MonoBehaviour
{
	/// <summary>
	/// コンティニュー表示テキスト
	/// </summary>
	[SerializeField]
	Text continueText;

	/// <summary>
	/// ポジティブボタン
	/// </summary>
	[SerializeField]
	GameOverButton positiveButton;

	/// <summary>
	/// ネガティブボタン
	/// </summary>
	[SerializeField]
	GameOverButton negativeButton;

	/// <summary>
	/// ゲームオーバー音
	/// </summary>
	[SerializeField]
	AudioClip gameOverSound;

	/// <summary>
	/// 表示時のイベント
	/// </summary>
	[SerializeField]
	UnityEvent showEvents;

	/// <summary>
	/// 非表示時のイベント
	/// </summary>
	[SerializeField]
	UnityEvent hideEvents;
	
	/// <summary>
	/// 今ゲームオーバーが開かれているかどうか
	/// </summary>
	bool isShow = false;

	/// <summary>
	/// ボタンが押された場合のイベント
	/// </summary>
	Action<bool> OnPressEvent = delegate{};


	/// <summary>
	/// 表示
	/// </summary>
	/// <param name="pressCallback">Press callback.</param>
	/// <param name="continueCount">Continue count.</param>
	public void Show(Action<bool> pressCallback, byte continueCount)
	{
		if (isShow) return;
		isShow = true;
		OnPressEvent = pressCallback;
		continueText.text = continueCount.ToString();
		positiveButton.SetButtonActive(continueCount > 0);
		negativeButton.SetIntaractive(true);
		PlaySound();
		showEvents.Invoke();
	}

	/// <summary>
	/// 非表示
	/// </summary>
	public void Hide()
	{
		hideEvents.Invoke();
		isShow = false;
		OnPressEvent = null;
	}

	/// <summary>
	/// コンティニュー：YES が押された場合
	/// </summary>
	public void PressPositive()
	{
		negativeButton.SetIntaractive(false);
		OnPressEvent(true);
		OnPressEvent = null;
		continueText.text = (int.Parse(continueText.text) - 1).ToString();
		BattleSound.Instance.PlayBattleBGM();
		Hide();
	}

	/// <summary>
	/// コンティニュー：NO が押された場合
	/// </summary>
	public void PressNegative()
	{
		positiveButton.SetIntaractive(false);
		OnPressEvent(false);
		OnPressEvent = null;
	}
	
	void PlaySound()
	{
		Sound.Instance.StopBGM();
		if (gameOverSound != null)
			Sound.Instance.PlaySE(gameOverSound);
	}
}
