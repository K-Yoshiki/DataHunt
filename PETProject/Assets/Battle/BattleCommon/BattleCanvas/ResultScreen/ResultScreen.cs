using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


/// <summary>
/// リザルト画面
/// </summary>
public class ResultScreen : MonoBehaviour
{
	/// <summary>
	/// クリア画面のパーティクル
	/// </summary>
	[SerializeField]
	EffectBase clearParticle;

	/// <summary>
	/// ステージ名のラベル
	/// </summary>
	[SerializeField]
	Text stageNameLabel;

	/// <summary>
	/// レベル名のラベル
	/// </summary>
	[SerializeField]
	Text levelNameLabel;

	/// <summary>
	/// 
	/// </summary>
	[SerializeField]
	GetPartsView getPartsViewPrefab;

	/// <summary>
	/// リザルト画面のBGM
	/// </summary>
	[SerializeField]
	AudioClip resultSound;

	/// <summary>
	/// ボタン用のサウンド
	/// </summary>
	[SerializeField]
	AudioClip buttonSound;

	/// <summary>
	/// リザルト動作後のコールバック
	/// </summary>
	Action callback;


	/// <summary>
	/// リザルト画面の表示
	/// </summary>
	public void ShowResult(string stageName, string levelName, Action callback)
	{
		this.callback = callback;
		GenerateParticle();
		stageNameLabel.text = stageName;
		levelNameLabel.text = levelName;
		this.gameObject.SetActive(true);
		SoundPlay();
	}

	/// <summary>
	/// 取得したパーツの表示
	/// </summary>
	public void ShowGetParts()
	{
		Instantiate(getPartsViewPrefab);
	}

	public void End()
	{
		if (callback != null)
		{
			callback();
			ButtonSound();
			callback = null;
		}
	}

	// パーティクル生成
	void GenerateParticle()
	{
		clearParticle = Instantiate(clearParticle) as EffectBase;
	}

	// BGMの再生
	void SoundPlay()
	{
		if (resultSound != null)
			AppUtils.Sound.Instance.PlayBGM(resultSound, 0.1f);
	}

	void ButtonSound()
	{
		if (buttonSound != null)
			AppUtils.Sound.Instance.PlaySE(buttonSound);
	}
}
