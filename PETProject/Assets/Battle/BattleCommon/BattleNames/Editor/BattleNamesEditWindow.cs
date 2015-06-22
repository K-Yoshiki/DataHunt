using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtilsEditor;

public class BattleNamesEditWindow : EditorWindow
{
	public BattleNames names;
	Vector2 leftScroll;
	Vector2 rightScroll;
	StageNamePackage selected;

	public static void Open(BattleNames names)
	{
		var window = EditorWindowUtility.ShowWindow<BattleNamesEditWindow>(640, 320, true);
		window.names = names;
		window.LockResize();
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();

		// Left Menu
		EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));
		DrawStageNameList(names.stageNameList);
		EditorGUILayout.EndVertical();

		// Right Menu
		EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));
		if (selected != null)
			DrawLevelNameList(selected.levelNames, selected.stageName);
		EditorGUILayout.EndVertical();

		EditorGUILayout.EndHorizontal();

		EditorUtility.SetDirty(names);
	}

	// ステージ名リストの描画
	void DrawStageNameList(List<StageNamePackage> list)
	{
		EditorGUILayout.LabelField("Stage Names");

		if (GUILayout.Button("+"))
		{
			list.Add(new StageNamePackage());
		}

		leftScroll = EditorGUILayout.BeginScrollView(leftScroll);
		for (int i = 0; i < list.Count; ++i)
		{
			string title = list[i].stageName;
			title = string.Format("{0}. {1}", i, string.IsNullOrEmpty(title) ? "[NoName]" : title);
			if (DrawStageName(title, list, list[i]))
			{
				selected = null;
				break;
			}
		}
		EditorGUILayout.EndScrollView();
	}

	// ステージ名アイテムの描画
	bool DrawStageName(string title, List<StageNamePackage> list, StageNamePackage item)
	{
		bool isMove = false;

		GUILayout.BeginVertical(EditorStyles.textArea);
		GUILayout.Space(4f);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
		isMove = DrawListControl(list, item);
		EditorGUILayout.EndHorizontal();

		item.stageName = EditorGUILayout.TextField("Stage Name", item.stageName);
		if (GUILayout.Button("Edit Level Names >>"))
		{
			selected = item;
		}
		GUILayout.EndVertical();
		GUILayout.Space(2f);

		return isMove;
	}

	// レベル名リストの描画
	void DrawLevelNameList(List<LevelNamePackage> list, string stageName)
	{
		EditorGUILayout.LabelField(stageName + " in Level Names List");

		if (GUILayout.Button("+"))
		{
			list.Add(new LevelNamePackage());
		}

		rightScroll = EditorGUILayout.BeginScrollView(rightScroll);
		for (int i = 0; i < list.Count; ++i)
		{
			string title = list[i].levelName;
			title = string.Format("{0}. {1}", i, string.IsNullOrEmpty(title) ? "[No Name Level]" : title);
			if (DrawLevelName(title,  list, list[i]))
				break;
		}
		EditorGUILayout.EndScrollView();
	}

	// レベル名アイテムの描画
	bool DrawLevelName(string title, List<LevelNamePackage> list, LevelNamePackage item)
	{
		bool isMove = false;
		GUILayout.BeginVertical(EditorStyles.textArea);
		GUILayout.Space(4f);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(title);
		isMove = DrawListControl(list, item);
		EditorGUILayout.EndHorizontal();

		item.levelName = EditorGUILayout.TextField("Level Name", item.levelName);
		item.prefabName = EditorGUILayout.TextField("Data Prefab Name", item.prefabName);

		GUILayout.EndVertical();
		GUILayout.Space(2f);
		return isMove;
	}

	/// <summary>
	/// リストアイテムコントロール.
	/// </summary>
	/// <returns>アイテムのインデックスに変化があった場合trueを返します.</returns>
	bool DrawListControl<T>(List<T> list, T selectItem)
	{
		bool result = false;
		EditorGUILayout.BeginHorizontal();
		if (DrawUpButton<T>(list, selectItem))
			result = true;
		if (DrawDownButton<T>(list, selectItem))
			result = true;
		if (DrawRemoveItem<T>(list, selectItem))
			result = true;
		EditorGUILayout.EndHorizontal();
		return result;
	}

	bool DrawUpButton<T>(List<T> list, T selectItem)
	{
		int index = list.IndexOf(selectItem);
		EditorGUI.BeginDisabledGroup(index == 0);
		if (GUILayout.Button("▲", EditorStyles.miniButtonLeft))
		{
			T upItem = list[index - 1];
			list[index - 1] = selectItem;
			list[index] = upItem;
			return true;
		}
		EditorGUI.EndDisabledGroup();
		return false;
	}

	bool DrawDownButton<T>(List<T> list, T selectItem)
	{
		int index = list.IndexOf(selectItem);
		EditorGUI.BeginDisabledGroup(index == list.Count - 1);
		if (GUILayout.Button("▼", EditorStyles.miniButtonMid))
		{
			T downItem = list[index + 1];
			list[index + 1] = selectItem;
			list[index] = downItem;
			return true;
		}
		EditorGUI.EndDisabledGroup();
		return false;
	}

	bool DrawRemoveItem<T>(List<T> list, T selectItem)
	{
		if (GUILayout.Button("×", EditorStyles.miniButtonRight))
		{
			if (EditorUtility.DisplayDialog("Delete Selected Item?", "", "Delete", "Cancel"))
			{
				list.Remove(selectItem);
				return true;
			}
		}
		return false;
	}
}
