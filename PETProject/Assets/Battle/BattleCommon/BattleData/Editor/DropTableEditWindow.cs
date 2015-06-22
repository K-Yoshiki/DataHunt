using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using AppUtilsEditor;


/// <summary>
/// アイテムのドロップテーブル
/// </summary>
public class DropTableEditWindow : EditorWindow
{
	public DropTable _dropTable;
	Vector2 scroll;

	public static void Open(DropTable dropTable, string windowTitle)
	{
		var window = EditorWindowUtility.ShowWindow<DropTableEditWindow>(400, 420, true, windowTitle);
		window._dropTable = dropTable;
		window.LockResize();
	}

	void OnGUI()
	{
		DropPercent();
		DrawAddDataButton();
		DropDataList(_dropTable.dropDataTable);
	}

	// ドロップテーブルのパーセントテージを設定欄の表示
	void DropPercent()
	{
		_dropTable.dropPercent = EditorGUILayout.Slider("Drop Percent", _dropTable.dropPercent, 0f, 100f);
	}

	// ドロップデータの追加ボタンを表示する
	void DrawAddDataButton()
	{
		if (GUILayout.Button("Add Drop Data"))
		{
			_dropTable.dropDataTable.Add(new DropData());
		}
	}

	// ドロップアイテムの抽選リストの設定欄の表示
	void DropDataList(List<DropData> list)
	{
		EditorGUILayout.LabelField("Drop Data");
		scroll = EditorGUILayout.BeginScrollView(scroll);
		foreach(var data in list)
		{
			if (DrawDropData(data))
			{
				list.Remove(data);
				break;
			}
		}
		EditorGUILayout.EndScrollView();
	}

	// ドロップアイテムの設定欄表示
	bool DrawDropData(DropData data)
	{
		bool result = false;
		EditorGUILayout.BeginVertical(EditorStyles.textArea);
		EditorGUILayout.BeginHorizontal();
		data.itemID = EditorGUILayout.IntField("Item ID", data.itemID);
		if (GUILayout.Button("×"))
		{
			if (EditorUtility.DisplayDialog("This Item Delete?", "", "OK", "Cancel"))
			{
				result = true;
			}
		}
		EditorGUILayout.EndHorizontal();
		data.selectPercent = EditorGUILayout.Slider("Drop Percent", data.selectPercent, 0f, 100f);
		EditorGUILayout.EndVertical();
		return result;
	}
}
