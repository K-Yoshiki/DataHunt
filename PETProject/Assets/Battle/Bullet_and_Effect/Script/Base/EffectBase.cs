using UnityEngine;
using System.Collections;


/// <summary>
/// エフェクトのベース.
/// 基本そのまま使用出来る場合はそのまま使用し, エフェクトに追加の機能を入れたい場合はこのクラスを継承してください
/// </summary>
public class EffectBase : MonoBehaviour
{
	/// <summary>
	/// 別プログラム側で任意に削除したい場合はInspectorでTrueに
	/// </summary>
	public bool loop = false;

	/// <summary>
	/// エフェクトの寿命
	/// </summary>
	public float duration;
	
	void Start()
	{
		if (loop == false)
			Destroy(this.gameObject, duration);
	}
}
