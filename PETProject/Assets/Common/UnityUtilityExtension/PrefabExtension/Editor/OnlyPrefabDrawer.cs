using System;
using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer(typeof(OnlyPrefab))]
public class OnlyPrefabDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
    	OnlyPrefab att = attribute as OnlyPrefab;
    	
        // Type Checking
        if(SerializedPropertyType.ObjectReference != property.propertyType)
        {
            EditorGUI.PropertyField(position, property, label);
            return;
        }
        
        UnityEngine.Object prefab = property.objectReferenceValue;
        label.text += " Prefab";
        
        EditorGUI.BeginChangeCheck();
        prefab = EditorGUI.ObjectField(position, label, prefab, att.attachType, false);
        property.objectReferenceValue = prefab;
        
        if(EditorGUI.EndChangeCheck() == false) return;
        
        if(prefab == null) return;
        
        if(PrefabUtility.GetPrefabType(prefab) == PrefabType.Prefab)
        {
            return;
        }
        else
        {
            property.objectReferenceValue = null;
        }
    }
}