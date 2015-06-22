using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 取得パーツの表示用ビュー管理
/// </summary>
public class GetPartsView : MonoBehaviour
{
	public float rotateSpeed;

	[SerializeField]
	Transform[] partsViewPositions;

	void Start()
	{
		ShowGetParts();
	}

	void Update()
	{
		foreach (var tf in partsViewPositions)
		{
			tf.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
		}
	}

	// ゲットしたパーツを順に表示していく
	void ShowGetParts()
	{
		int index = 0;
		Dictionary<int, PlayerParts> partsPrefabs = LoadPrefabs();
		foreach (var partsPrefab in partsPrefabs.Values)
		{
			PartsInstantiate(index, partsPrefab);
			++index;
			if (index >= 15) break;
		}
	}

	void PartsInstantiate(int index, PlayerParts prefab)
	{
		Transform parent = partsViewPositions[index];
		PlayerParts parts = (PlayerParts)Instantiate(prefab);
		parts.transform.SetParent(parent);
		parts.transform.localPosition = Vector3.zero;
		parts.transform.localRotation = Quaternion.identity;
		parts.transform.localScale = Vector3.one;

		SetLayer("UIObject", parts.transform);
	}

	void SetLayer(string layerName, Transform obj)
	{
		obj.gameObject.layer = LayerMask.NameToLayer(layerName);
		for (int i = 0; i < obj.childCount; ++i)
		{
			SetLayer(layerName, obj.GetChild(i));
		}
	}

	// ゲットしたパーツのプレハブをロードする
	Dictionary<int, PlayerParts> LoadPrefabs()
	{
		Dictionary<int, PlayerParts> dict = new Dictionary<int, PlayerParts>();
		List<int> getIDs = BattleRecord.Instance.GetedItemIDList;
		foreach (var id in getIDs)
		{
			if (dict.ContainsKey(id) == false)
			{
				dict[id] = PartsLoader.Load(id);
			}
		}
		return dict;
	}

}
