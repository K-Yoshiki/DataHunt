using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemTable : ScriptableObject
{
	public ItemParticle partsItemParticle;
	public List<ItemData> itemList;
	public Dictionary<int, ItemData> tempTable;
	
	public void Initialize()
	{
		itemList = itemList ?? new List<ItemData>();
		tempTable = tempTable ?? new Dictionary<int, ItemData>();
		ListToTable();
	}
	
	/// <summary>
	/// アイテム取得演出のパーティクル呼び出し
	/// </summary>
	/// <returns>The particle prefab.</returns>
	/// <param name="item">Item.</param>
	public void PartcileSpawn(ItemType itemType, Vector3 pos)
	{
		if (itemType == ItemType.Parts)
		{
			Instantiate(partsItemParticle, pos, Quaternion.identity);
		}
	}
	
	/// <summary>
	/// アイテムのデータをidから取得する.
	/// </summary>
	/// <returns>アイテムデータを返します。指定IDがない場合はNullを返します.</returns>
	/// <param name="id">Item ID.</param>
	public ItemData GetItemData(int id)
	{
		if (tempTable.ContainsKey(id))
		{
			return tempTable[id];
		}
		return null;
	}
	
	/// <summary>
	/// アイテムのデータをセットする.
	/// </summary>
	/// <param name="id">ItemID</param>
	/// <param name="item">ItemBase Data</param>
	public void SetItemData(ItemData item)
	{
		tempTable[item.itemID] = item;
	}
	
	/// <summary>
	/// アイテムのデータを削除する
	/// </summary>
	/// <param name="item">Item.</param>
	public void RemoveItem(ItemData item)
	{
		tempTable.Remove(item.itemID);
	}
	
	/// <summary>
	/// テーブルからリストへ保存する
	/// </summary>
	public void TableToList()
	{
		itemList.Clear();
		foreach (var pair in tempTable)
		{
			itemList.Add(pair.Value);
		}
	}
	
	/// <summary>
	/// リストからテーブルへ復元する
	/// </summary>
	public void ListToTable()
	{
		tempTable.Clear();
		foreach(var item in itemList)
		{
			tempTable[item.itemID] = item;
		}
	}
}
