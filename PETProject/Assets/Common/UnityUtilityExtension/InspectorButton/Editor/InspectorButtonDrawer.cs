using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(InspecterButton))]
public class InspectorButton : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Type Checking
		if(SerializedPropertyType.Boolean != property.propertyType)
		{
			EditorGUI.PropertyField(position, property, label);
			return;
		}

		// Get Attribute
		InspecterButton button = attribute as InspecterButton;
		if(GUI.Button(position, button.label, EditorStyles.miniButton))
		{
			property.boolValue = true;
		}
	}
}