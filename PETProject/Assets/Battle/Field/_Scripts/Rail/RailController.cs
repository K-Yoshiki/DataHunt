using UnityEngine;
using System.Collections;


/// <summary>
/// フィールドレール操作クラス
/// </summary>
public class RailController : MonoBehaviour
{
	[SerializeField]
	public GameObject[] railPrefabs = new GameObject[0];
	private Transform[] railList;
	public short RailCount { get; private set; }

	/// <summary>
	/// 現在のフィールドのレール数をセット
	/// </summary>
	/// <param name="rails">Rails.</param>
	public void SetRails(short railCount)
	{
		RailSet();
		
		railCount = (short)Mathf.Max(0, Mathf.Min(railCount, railPrefabs.Length));
		
		int diactivedRailCount = railPrefabs.Length - railCount;
		diactivedRailCount = (short)Mathf.Min(diactivedRailCount, railPrefabs.Length);
		
		// Set Enable
		for (int i = 0; i < railCount; ++i)
		{
			railList[i].gameObject.SetActive(true);
		}
		// Set Disable
		for (int i = 0; i < diactivedRailCount; ++i)
		{
			railList[railList.Length - i - 1].gameObject.SetActive(false);
		}
		
		this.RailCount = railCount;
	}
	
	/// <summary>
	/// 現在のレール数を取得する
	/// </summary>
	/// <value>The get rings.</value>
	public short GetRings
	{
		get{ return this.RailCount; }
	}
	
	void RailSet()
	{
		if (railList != null && railList.Length > 0)
			return;
			
		railList = new Transform[railPrefabs.Length];
		
		for (short i = 0; i < railPrefabs.Length; ++i)
		{
			railList[i] = (Instantiate(railPrefabs[i]) as GameObject).transform;
			railList[i].SetParent(this.transform);
			railList[i].transform.position = transform.position;
		}
	}
}
