using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(BattleData))]
public class BattleDataDrawer : Editor
{
	// Battleデータのキャッシュ
	BattleData _data;
	BattleData Data
	{
		get { 
			if (_data == null)
				_data = target as BattleData;
			return _data;
		}
	}

	public override void OnInspectorGUI()
	{
		DrawPhaseCount();

		EditButton();
	}

	void Horizontal(Action horizontalView)
	{
		EditorGUILayout.BeginHorizontal();
		horizontalView.Invoke();
		EditorGUILayout.EndHorizontal();
	}
	
	void DrawPhaseCount()
	{
		DrawInfoLabel("Phase Count", Data.PhaseList.Count.ToString());
	}

	string NoValueLabel(string value)
	{
		if (String.IsNullOrEmpty(value))
		{
			return "[ No Data ]";
		}
		return value;
	}

	void DrawInfoLabel(string title, string value)
	{
		Horizontal(delegate {
			EditorGUILayout.LabelField(title);
			EditorGUILayout.LabelField(value);
		});
	}

	void EditButton()
	{
		if (GUILayout.Button("BattleData Edit", GUILayout.Height(25)))
		{
			BattleDataEditWindow.Open(Data);
		}
	}
}