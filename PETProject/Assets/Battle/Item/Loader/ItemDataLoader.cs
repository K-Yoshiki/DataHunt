using UnityEngine;
using System.Collections;


public static class ItemDataLoader
{
	static ItemTable _cacheTable;
	public static ItemTable Table
	{
		get {
			if (_cacheTable == null)
			{
				_cacheTable = Resources.Load<ItemTable>("ItemTable");
				_cacheTable.Initialize();
			}
			return _cacheTable;
		}
	}

	/// <summary>
	/// アイテムデータの取得
	/// </summary>
	/// <returns>The item.</returns>
	public static ItemData LoadItem(int itemID)
	{
		return Table.GetItemData(itemID);
	}
}
