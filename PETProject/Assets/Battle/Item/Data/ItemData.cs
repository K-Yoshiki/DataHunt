using UnityEngine;
using System.Collections;

/// <summary>
/// アイテム情報の基底クラス
/// </summary>
[System.Serializable]
public class ItemData
{
	/// <summary>
	/// アイテムのID値
	/// </summary>
	public int itemID;

	/// <summary>
	/// アイテム毎の値
	/// </summary>
	public int itemValue;

	/// <summary>
	/// アイテムのタイプ
	/// </summary>
	public ItemType itemType;

	/// <summary>
	/// アイテムの初期化
	/// </summary>
	/// <param name="type">Type.</param>
	public ItemData(ItemType type)
	{
		this.itemType = type;
	}

	/// <summary>
	/// ユーザーデータへの保存
	/// </summary>
	public void OnSaveInUser(UserData userData)
	{
		if (itemType == ItemType.Parts)
		{
			SavePartsData(userData);
		}
		else if (itemType == ItemType.Gold)
		{
			SaveGoldData(userData);
		}
	}

	/// <summary>
	/// パーツアイテムのセーブ
	/// </summary>
	/// <param name="userData">User data.</param>
	void SavePartsData(UserData userData)
	{
		userData.personalParts.AddParts(itemValue);
	}

	/// <summary>
	/// ゴールドの追加
	/// </summary>
	/// <param name="userData">User data.</param>
	void SaveGoldData(UserData userData)
	{
		userData.playerData.gold += (uint)Mathf.Abs(itemValue);
	}
}


/// <summary>
/// アイテムの分類
/// </summary>
public enum ItemType : byte
{
	Parts,
	Gold,
}