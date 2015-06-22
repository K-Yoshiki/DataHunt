using UnityEngine;
using System.Collections;


/// <summary>
/// ホーム画面の選択リング制御
/// </summary>
public class SelectableRing : MonoBehaviour
{
	public float autoRotBaseSpeed = 0.1f;
	public int selectableCount = 3;

	bool isCatch;
	float cellSize;
	float[] cellCentorAngles;
	int selectedIndex;
	
	float NowAngle
	{
		get { return this.transform.localEulerAngles.y; }
		set { this.transform.localEulerAngles = Vector3.up * value; }
	}

	void Start()
	{
		isCatch = false;
		NowAngle = this.transform.localEulerAngles.y;
		selectedIndex = 0;

		cellSize = 360f / Mathf.Max(1, selectableCount);
		cellCentorAngles = new float[selectableCount];
		cellCentorAngles[0] = NowAngle;
		for (int i = 1; i < selectableCount; ++i)
		{
			cellCentorAngles[i] = cellCentorAngles[i - 1] + cellSize;
		}
	}

	void Update()
	{
		if (isCatch == false)
		{
			float dist = Mathf.DeltaAngle(NowAngle, cellCentorAngles[selectedIndex]);
			NowAngle += autoRotBaseSpeed * dist * Time.deltaTime;
		}
	}

	/// <summary>
	/// 自動回転の目標値を設定する
	/// </summary>
	void SetTargetAngle()
	{
		float dist = Mathf.DeltaAngle(NowAngle, cellCentorAngles[selectedIndex]);
		float sign = Mathf.Sign(dist);
		if (Mathf.Abs(dist) > cellSize * 0.25f)
		{
			if (sign < 0f)
			{
				// Plus
				++selectedIndex;
				selectedIndex = Mathf.FloorToInt(Mathf.Repeat(selectedIndex, cellCentorAngles.Length));
			}
			else
			{
				// Minus
				--selectedIndex;
				selectedIndex = Mathf.FloorToInt(Mathf.Repeat(selectedIndex, cellCentorAngles.Length));
			}
		}
	}
	
	/// <summary>
	/// 指がリングに触れた場合
	/// </summary>
	public void Catch()
	{
		isCatch = true;
	}

	/// <summary>
	/// 指がリングから離されたとき
	/// </summary>
	public void Release()
	{
		isCatch = false;
		SetTargetAngle();
	}

	/// <summary>
	/// リングを指定の方向と移動量でまわす
	/// </summary>
	/// <param name="value">Value.</param>
	public void Rotate(float value)
	{
		if (isCatch)
			NowAngle += value * Time.deltaTime;
	}
}