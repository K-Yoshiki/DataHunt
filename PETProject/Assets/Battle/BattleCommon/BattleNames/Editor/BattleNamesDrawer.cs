using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CustomEditor(typeof(BattleNames))]
public class BattleNamesDrawer : Editor
{
	BattleNames _names;
	BattleNames Names
	{
		get {
			if (_names == null)
				_names = target as BattleNames;
			return _names;
		}
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.Space();
		if (GUILayout.Button("Battle Names Edit", GUILayout.Height(25)))
		{
			BattleNamesEditWindow.Open(Names);
		}
	}
}
