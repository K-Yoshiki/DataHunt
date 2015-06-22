/*
 * Attached Tween ver3.0
 * Ahthor: Yamamoto Koji
 */
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


/// <summary>
/// AttachedTweenクラスのInspector改変
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(AttachedTween))]
public class AttachedTweenEditor : Editor
{
	SerializedProperty m_BaseSet = null;
	SerializedProperty m_ActionParam = null;
	SerializedProperty m_CallBackFunc = null;
	static bool showCallbackEvent;
	static bool isBackward;

	void OnEnable()
	{
		// Get SerializedProperties
		SerializedProperty setting = serializedObject.FindProperty("setting");

		m_BaseSet = setting.FindPropertyRelative("baseSet");
		m_ActionParam = setting.FindPropertyRelative("param");
		m_CallBackFunc = setting.FindPropertyRelative("callbackFunc");
	}

	public override void OnInspectorGUI()
	{
		// serializedObject Update Start
		serializedObject.Update();
		
		// Draw Test Execute Button
		EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);
		EditorGUILayout.BeginHorizontal();
		isBackward = EditorGUILayout.Toggle("Is Test Backward", isBackward);
		if (GUILayout.Button(EditorGUIUtility.Load("icons/d_PlayButton Anim.png") as Texture, GUILayout.Height(20), GUILayout.MaxWidth(150)))
		{
			((AttachedTween)target).Execute(isBackward);
		}
		EditorGUILayout.EndHorizontal();
		EditorGUI.EndDisabledGroup();

		// Draw Inspector Properties
		EditorGUILayout.PropertyField(m_BaseSet.FindPropertyRelative("onStart"));
		DrawTweenSetting();
		GUILayout.Space(4);
		DrawActionParam();
		DrawCallbackFunc();

		// serializedObject Update Dirty
		serializedObject.ApplyModifiedProperties();
	}

	void DrawTweenSetting()
	{
		EditorGUILayout.PropertyField(m_BaseSet.FindPropertyRelative("time"), new GUIContent("Time"));
		EditorGUILayout.PropertyField(m_BaseSet.FindPropertyRelative("delay"), new GUIContent("Delay"));
		EditorGUILayout.PropertyField(m_BaseSet.FindPropertyRelative("easeType"), new GUIContent("Ease Type"));
		EditorGUILayout.PropertyField(m_BaseSet.FindPropertyRelative("loopType"), new GUIContent("Loop Type"));
	}

	void DrawActionParam()
	{
		TweenTarget targetType;
		
		EditorGUILayout.PropertyField(m_ActionParam.FindPropertyRelative("target"), new GUIContent("iTween Target"));
		
		// Get Tween Target
		targetType = (TweenTarget)m_ActionParam.FindPropertyRelative("target").enumValueIndex;
		
		// "Color" is Invisible Mode Selector
		if (targetType != TweenTarget.Color)
		{
			EditorGUILayout.PropertyField(m_ActionParam.FindPropertyRelative("mode"), new GUIContent("iTween Mode"));
		}
		else
		{
			EditorGUILayout.Popup("iTween Mode", 0, new string[] {"To"});
		}

		// "Color" or "Vector3" Paramator
		if (TweenTarget.Color == (TweenTarget)m_ActionParam.FindPropertyRelative("target").enumValueIndex)
		{
			SerializedProperty paramOption = m_ActionParam.FindPropertyRelative("option");
			EditorGUILayout.PropertyField(paramOption.FindPropertyRelative("color"));
			EditorGUILayout.PropertyField(paramOption.FindPropertyRelative("colorType"), new GUIContent("Target Color"));
		}
		else
		{
			SerializedProperty paramOption = m_ActionParam.FindPropertyRelative("option");
			EditorGUILayout.PropertyField(paramOption.FindPropertyRelative("vec"), new GUIContent(targetType.ToString()));
		}
	}

	void DrawCallbackFunc()
	{
		showCallbackEvent = EditorGUILayout.Foldout(showCallbackEvent, "Callback Events");
		if (showCallbackEvent)
		{
			SerializedProperty unityEvent;

			// Started Event
			unityEvent = m_CallBackFunc.FindPropertyRelative("onStartedEvent");
			EditorGUILayout.PropertyField(unityEvent, new GUIContent("Started Event"));

			// Ended Event
			unityEvent = m_CallBackFunc.FindPropertyRelative("onEndedEvent");
			EditorGUILayout.PropertyField(unityEvent, new GUIContent("Ended Event"));
		}
	}
}