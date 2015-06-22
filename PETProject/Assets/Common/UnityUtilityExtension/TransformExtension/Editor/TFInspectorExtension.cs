//using UnityEngine;
//using UnityEditor;
//
//#if UNITY_EDITOR_OSX || UNITY_EDITOR
//[CustomEditor(typeof(Transform))]
//public class TFInspectorExtension : Editor
//{
//	const float RESET_BUTTON_WIDTH = 20f;
//
//	SerializedObject mTransform;
//
//	void OnEnable()
//	{
//		mTransform = serializedObject;
//	}
//
//	public override void OnInspectorGUI()
//	{
//		Undo.RecordObject(mTransform.targetObject, "target");
//		serializedObject.Update();
//		Transform t = mTransform.targetObject as Transform;
//        DrawPos(t);
//		DrawRot(t);
//		DrawScl(t);
//		serializedObject.ApplyModifiedProperties();
//	}
//
//	void DrawPos(Transform t)
//	{
//		EditorGUILayout.BeginHorizontal();
//		if(GUILayout.Button("R", EditorStyles.miniButton, GUILayout.Width(RESET_BUTTON_WIDTH)))
//		{
//			t.localPosition = Vector3.zero;
//		}
//		t.localPosition = EditorGUILayout.Vector3Field("Position", t.localPosition);
//		EditorGUILayout.EndHorizontal();
//    }
//
//	void DrawRot(Transform t)
//	{
//		EditorGUILayout.BeginHorizontal();
//		if(GUILayout.Button("R", EditorStyles.miniButton, GUILayout.Width(RESET_BUTTON_WIDTH)))
//		{
//			t.localRotation = Quaternion.identity;
//        }
//		t.localEulerAngles = EditorGUILayout.Vector3Field("Rotation", t.localEulerAngles);
//
//		EditorGUILayout.EndHorizontal();
//	}
//
//	void DrawScl(Transform t)
//	{
//		EditorGUILayout.BeginHorizontal();
//		if(GUILayout.Button("R", EditorStyles.miniButton, GUILayout.Width(RESET_BUTTON_WIDTH)))
//		{
//			t.localScale = Vector3.one;
//        }
//		t.localScale = EditorGUILayout.Vector3Field("Scale", t.localScale);
//		EditorGUILayout.EndHorizontal();
//    }
//}
//#endif