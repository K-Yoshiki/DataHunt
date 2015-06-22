using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// 現在のフェーズ表示のアニメーション管理コントローラー
/// </summary>
public class PhaseLabelController : MonoBehaviour
{
	[SerializeField]
	NormalPhase normalPhaseLabel;

	[SerializeField]
	PhaseLabel bossPhaseLabel;

	/// <summary>
	/// 通常のフェーズラベルを表示する
	/// </summary>
	public void NormalPhaseLabel(int phaseNum, Action callback)
	{
		NormalPhase label = Instantiate(normalPhaseLabel) as NormalPhase;
		label.transform.SetParent(this.transform);
		label.transform.localPosition = Vector3.zero;
		label.transform.localScale = Vector3.one;
		label.Set(phaseNum);
		label.OnCallBack += callback;
	}

	/// <summary>
	/// ボス専用のフェーズラベルを表示する
	/// </summary>
	public void BossPhaseLabel(Action callback)
	{
		PhaseLabel label = Instantiate(bossPhaseLabel) as PhaseLabel;
		label.transform.SetParent(this.transform);
		label.transform.localPosition = Vector3.zero;
		label.transform.localScale = Vector3.one;
		label.OnCallBack += callback;
	}	
}