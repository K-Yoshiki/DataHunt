using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using AppUtilsEditor;

public class BattleDataEditWindow : EditorWindow
{
	public BattleData data;
	Vector2 phaseListScroll;
	Vector2 spawnListScroll;
	SpawnListCache selectedSpawnList;

	public static void Open(BattleData battleData)
	{
		var window = EditorWindowUtility.ShowWindow<BattleDataEditWindow>(800, 420, true, "BattleDataEdit");
		window.data = battleData;
		window.LockResize();
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		DrawLeft();
		DrawRight();
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		EditorUtility.SetDirty(data);
	}

	void ValueEdit(string title, Action editView)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(title);
		editView.Invoke();
		EditorGUILayout.EndHorizontal();
	}

	#region Left Editor (PhaseList)
	/// <summary>
	/// エディタウィンドウ左側
	/// </summary>
	void DrawLeft()
	{
		EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));
		EditorGUILayout.Space();
		data.isTutorial = EditorGUILayout.Toggle("Tutorial Stage", data.isTutorial);
		data.StageBGM = EditorGUILayout.ObjectField("Stage BGM", data.StageBGM, typeof(AudioClip), false) as AudioClip;
		data.BossBGM = EditorGUILayout.ObjectField("Boss BGM", data.BossBGM, typeof(AudioClip), false) as AudioClip;
		ValueEdit("Field Drop Table", delegate {
			if (GUILayout.Button("Edit"))
				DropTableEditWindow.Open(data.FieldDropTable, "Field Drop Table Edit");
		});
		ValueEdit("Boss Drop Table", delegate {
			if (GUILayout.Button("Edit"))
				DropTableEditWindow.Open(data.BossDropTable, "Boss Drop Table Edit");
		});
		EditorGUILayout.Space();
		DrawPhaseEditor();
		EditorGUILayout.EndVertical();
	}

	/// <summary>
	/// フェーズの編集部分
	/// </summary>
	void DrawPhaseEditor()
	{
		DrawPhaseListTitle();
		phaseListScroll = EditorGUILayout.BeginScrollView(phaseListScroll);
		DrawPhaseListEdit();
		EditorGUILayout.EndScrollView();
	}

	/// <summary>
	/// フェーズのリストタイトルと追加用ボタン
	/// </summary>
	void DrawPhaseListTitle()
	{
		// Title and Phase add button
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Phase List : " + data.PhaseList.Count + " Phase");
		if (GUILayout.Button("+"))
			data.PhaseList.Add(new PhaseData());
		EditorGUILayout.EndHorizontal();
	}

	void DrawPhaseListEdit()
	{
		int index = 0;
		PhaseData deletePhase = null;
		
		// Phase Loop
		data.PhaseList.ForEach(delegate(PhaseData phase)
		{
			GUILayout.BeginVertical(EditorStyles.textArea);
			GUILayout.Space(2);
			
			// Phase index and Delete button
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(string.Format("Phase[{0}]", index));
			if (GUILayout.Button("Delete"))
			{
				string message = "This phase is deleting ?" + "\n" + "\"Phase " + index.ToString() + "\"";
				if (EditorUtility.DisplayDialog("Delete Dialog", message, "OK", "Cancel"))
				{
					deletePhase = phase;
					selectedSpawnList = null;
				}
			}
			EditorGUILayout.EndHorizontal();
			
			// Rail edit
			phase.Rails = (byte)EditorGUILayout.IntSlider("Rails", phase.Rails, 1, 7);
			
			// Enemy spawn setting button
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("EnemyCount : " + phase.EnemySpawnList.Count);
			if (GUILayout.Button("Edit EnemySpawnList >>"))
			{
				selectedSpawnList = new SpawnListCache(index, phase.EnemySpawnList);
			}
			EditorGUILayout.EndHorizontal();

			GUILayout.Space(2);
			GUILayout.EndVertical();
			GUILayout.Space(5);
			index++;
		});

		if (deletePhase != null)
			data.PhaseList.Remove(deletePhase);
	}
	#endregion

	#region Right Editor (SpawnList)
	class SpawnListCache
	{
		public int index;
		public List<EnemySpawnData> spawnList;

		public SpawnListCache(int index, List<EnemySpawnData> list)
		{
			this.index = index;
			this.spawnList = list;
		}
	}

	void DrawRight()
	{
		EditorGUILayout.BeginVertical(GUILayout.Width(position.width * 0.5f));
		EditorGUILayout.Space();
		if (selectedSpawnList != null)
		{
			DrawSpawnDataTitle();
			spawnListScroll = EditorGUILayout.BeginScrollView(spawnListScroll);
			DrawSpawnDataEdit();
			EditorGUILayout.EndScrollView();
		}
		EditorGUILayout.EndVertical();
	}

	void DrawSpawnDataTitle()
	{
		// Title and Add buton
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(string.Format("Spawn Enemy Edit : Phase [{0}]", selectedSpawnList.index));
		if (GUILayout.Button("+"))
			selectedSpawnList.spawnList.Add(new EnemySpawnData());
		EditorGUILayout.EndHorizontal();
	}

	void DrawSpawnDataEdit()
	{
		// Spawn List
		int index = 0;
		EnemySpawnData deleteData = null;
		selectedSpawnList.spawnList.ForEach(delegate(EnemySpawnData spawnData)
		{
			GUILayout.BeginVertical(EditorStyles.textArea);

			// Draw numder And Delete button
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(string.Format("Enemy [{0}]", index));
			if (GUILayout.Button("Delete"))
			{
				if (EditorUtility.DisplayDialog("Delete Dialog", "This Enemy Data Deleting?", "YES", "Cancel"))
					deleteData = spawnData;
			}
			EditorGUILayout.EndHorizontal();

			// Draw EnemyData
			DrawSpawnData(spawnData);

			GUILayout.EndVertical();
			++index;
		});

		// Remove SpawnData
		if (deleteData != null)
			selectedSpawnList.spawnList.Remove(deleteData);
	}

	void DrawSpawnData(EnemySpawnData spawnData)
	{
		spawnData.SpawnTime = EditorGUILayout.FloatField("Spawn Time", spawnData.SpawnTime);
		spawnData.DefRail = EditorGUILayout.IntSlider("Default Rail", spawnData.DefRail, 0, 5);
		spawnData.DefAngle = EditorGUILayout.Slider("Default Angle", spawnData.DefAngle, 0, 360);
		spawnData.MoveSpeed = EditorGUILayout.FloatField("Move Speed", spawnData.MoveSpeed);
		spawnData.EnemyType = (EnemyType)EditorGUILayout.EnumPopup("Enemy Type", spawnData.EnemyType);

		if (spawnData.EnemyType == EnemyType.Medium)
		{
			DrawAddEnemyDataButton(spawnData.EnemyList);
			for (int i = 0; i < spawnData.EnemyList.Count; ++i)
			{
				spawnData.EnemyList[i] = DrawEnemyData(spawnData.EnemyList[i], i);
			}
		}
		else
		{
			if (spawnData.EnemyList.Count == 0)
			{
				spawnData.EnemyList.Add(null);
			}
			else if (spawnData.EnemyList.Count > 1)
			{
				spawnData.EnemyList.RemoveRange(1, spawnData.EnemyList.Count - 1);
			}
			spawnData.EnemyList[0] = DrawEnemyData(spawnData.EnemyList[0]);
		}
	}

	void DrawAddEnemyDataButton(List<EnemyData> enemyList)
	{
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Enemy Data Add", EditorStyles.miniButtonLeft, GUILayout.Height(20)))
		{
			enemyList.Add(null);
		}
		EditorGUI.BeginDisabledGroup(enemyList.Count == 1);
		if (GUILayout.Button("Enemy Data Last Remove", EditorStyles.miniButtonRight, GUILayout.Height(20)))
		{
			enemyList.RemoveAt(enemyList.Count - 1);
		}
		EditorGUI.EndDisabledGroup();
		EditorGUILayout.EndHorizontal();
	}

	EnemyData DrawEnemyData(EnemyData data, int index = 0)
	{
		return EditorGUILayout.ObjectField("Enemy Data", data, typeof(EnemyData), false) as EnemyData;
	}
	#endregion
}