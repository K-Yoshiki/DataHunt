using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// Player Core Controller
/// </summary>
public class RailChanger : MonoBehaviour
{
	public float changeTime = 0.5f;
	private bool isRailChanging;

	public Action<int> SetRail;
	
	void Awake()
	{
		isRailChanging = false;
	}
	
	/// <summary>
	/// 初期状態のセット
	/// </summary>
	/// <param name="nowRail">初期状態のレールインデックス</param>
	public void DefaultSet(int nowRail)
	{
		Vector3 pos = transform.localPosition;
		pos.z = -FieldManager.Instance.GetRadius(nowRail);
		this.transform.localPosition = pos;
	}

	/// <summary>
	/// ダメージを受けた際のレール変更
	/// </summary>
	/// <returns>ダメージ処理が成功したかどうか</returns>
	public bool DamageOutRail()
	{
		int oldRailNum = PlayerController.Instance.NowRail;
		SetRail(ChangeRail(PlayerController.Instance.NowRail, RailVec.Outer, true));
		return oldRailNum != PlayerController.Instance.NowRail;
	}
	
	/// <summary>
	/// レールの変更
	/// </summary>
	/// <returns>変更後のレールインデックス</returns>
	/// <param name="nowRail">現在のレールインデックス</param>
	/// <param name="changeDir">動く方向</param>
	public int ChangeRail(int nowRail, RailVec changeDir, bool isDamage = false)
	{
		if (isRailChanging)
			return nowRail;
		
		isRailChanging = true;
		
		// Create iTween Hash
		Hashtable hash = iTween.Hash(
			"z", -GetRail(ref nowRail, changeDir, isDamage),
			"islocal", true,
			"time", changeTime,
			"easetype", iTween.EaseType.easeOutSine,
			"oncomplete", "RailChangeEnd"
		);
		
		// iTween Move Start
		iTween.MoveTo(this.gameObject, hash);
		
		return nowRail;
	}


	void RailChangeEnd()
	{
		isRailChanging = false;
	}
	
	float GetRail(ref int nowRail, RailVec changeDir, bool isDamage)
	{
		if (changeDir == RailVec.Inner)
		{
			NowRailAdd(ref nowRail, -1, isDamage);
			return FieldManager.Instance.GetRadius(nowRail);
		}
		else if (changeDir == RailVec.Outer)
		{
			NowRailAdd(ref nowRail, 1, isDamage);
			return FieldManager.Instance.GetRadius(nowRail);
		}
		else
		{
			return 0;
		}
	}
	
	void NowRailAdd(ref int nowRail, int value, bool isDamage)
	{
		if (BattleManager.Instance.CanNextPhase() && value < 0)
		{
			nowRail = -1;
			return;
		}

		int min = BattleManager.Instance.CanNextPhase() ? -1 : 0;
		int max = FieldManager.Instance.RailCount() - (isDamage ? 0 : 1);

		nowRail += value;
		nowRail = Mathf.Max(min, Mathf.Min(nowRail, max));
	}
}
