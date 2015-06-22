using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using AppUtilsEditor;


public class ItemTableEditor : EditorWindow
{
	public ItemTable _itemTable;

	int addID;
	ItemType addType;

	bool onSearchID;
	int searchID;

	bool onSearchType;
	ItemType searchType;

	Vector2 scroll;

	[MenuItem("Debug/Item Table Edit")]
	public static void Open()
	{
		var window = EditorWindowUtility.ShowWindow<ItemTableEditor>(800, 420, true, "ItemTableEdit");
		window._itemTable = ItemDataLoader.Table;
		window._itemTable.Initialize();
		window.LockResize();
	}
	
	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.BeginVertical(GUILayout.MaxWidth(position.width * 0.5f));
		ItemParticles();
		ItemAddButton();
		SearchSetting();
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical(GUILayout.MaxWidth(position.width * 0.5f));
		ItemList(_itemTable.tempTable);
		EditorGUILayout.EndVertical();

		EditorGUILayout.EndHorizontal();

		_itemTable.TableToList();
		EditorUtility.SetDirty(_itemTable);
	}

	void ItemParticles()
	{
		GUILayout.Space(2f);
		EditorGUILayout.BeginVertical(EditorStyles.textArea);
		EditorGUILayout.LabelField("Item Particles");
		_itemTable.partsItemParticle = (ItemParticle)EditorGUILayout.ObjectField("PartsItemParticle", _itemTable.partsItemParticle, typeof(ItemParticle), false);
		EditorGUILayout.EndVertical();
	}

	/// <summary>
	/// Itemの追加UI
	/// </summary>
	void ItemAddButton()
	{
		EditorGUILayout.BeginVertical(EditorStyles.textArea);
		EditorGUILayout.LabelField("Add Item");
		addID = EditorGUILayout.IntField("Item ID", addID);
		addType = (ItemType)EditorGUILayout.EnumPopup("Item Type", addType);
		if (GUILayout.Button("Add Item"))
		{
			if (_itemTable.tempTable.ContainsKey(addID) == false)
			{
				_itemTable.tempTable[addID] = new ItemData(addType) { itemID = addID };
			}
		}
		EditorGUILayout.EndVertical();
	}

	/// <summary>
	/// IDとTypeでサーチする
	/// </summary>
	void SearchSetting()
	{
		EditorGUILayout.BeginVertical(EditorStyles.textArea);
		EditorGUILayout.LabelField("Item Data Search");

		onSearchID = EditorGUILayout.BeginToggleGroup("ID Search On", onSearchID);
		searchID = EditorGUILayout.IntField("Search ID : ", searchID);
		EditorGUILayout.EndToggleGroup();

		onSearchType = EditorGUILayout.BeginToggleGroup("Type Search On", onSearchType);
		searchType = (ItemType)EditorGUILayout.EnumPopup("Search Type : ", searchType);
		EditorGUILayout.EndToggleGroup();

		EditorGUILayout.EndVertical();
	}

	void ItemList(Dictionary<int, ItemData> dict)
	{
		scroll = EditorGUILayout.BeginScrollView(scroll);
		foreach(var item in dict.Values)
		{
			if (DrawItem(item)) break;
		}
		EditorGUILayout.EndScrollView();
	}

	bool DrawItem(ItemData item)
	{
		bool result = false;
		if (onSearchID)
		{
			if (item.itemID != searchID)
				return result;
		}
		if (onSearchType)
		{
			if (item.itemType != searchType)
				return result;
		}

		EditorGUILayout.BeginVertical(EditorStyles.textArea);
		GUILayout.Space(2f);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("ID : " + item.itemID.ToString());
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("×"))
		{
			if (EditorUtility.DisplayDialog("This Item Delete?", "", "OK", "Cancel"))
			{
				result = true;
				_itemTable.RemoveItem(item);
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Type : " + item.itemType.ToString());
		item.itemValue = EditorGUILayout.IntField("ItemValue", item.itemValue);

		GUILayout.Space(2f);
		EditorGUILayout.EndVertical();
		return result;
	}
}