using UnityEngine;
using System.Collections;
using AppUtils;


/// <summary>
/// バトルのサウンド再生管理
/// </summary>
public class BattleSound : MonoSingleton<BattleSound>
{
	AudioClip battleBGM;
	AudioClip bossBGM;

	/// <summary>
	/// 音声データのセット
	/// </summary>
	/// <param name="battleBGM">Battle background.</param>
	/// <param name="bossBGM">Boss background.</param>
	public void SetData(AudioClip battleBGM, AudioClip bossBGM)
	{
		this.battleBGM = battleBGM;
		this.bossBGM = bossBGM;
	}

	/// <summary>
	/// 通常フェーズBGMを再生する
	/// </summary>
	public void PlayBattleBGM()
	{
		if (battleBGM != null)
			Sound.Instance.PlayBGM(battleBGM);
	}

	/// <summary>
	/// ボスフェーズBGMを再生する
	/// </summary>
	public void PlayBossBGM()
	{
		if (bossBGM != null)
			Sound.Instance.PlayBGM(bossBGM);
	}
}
