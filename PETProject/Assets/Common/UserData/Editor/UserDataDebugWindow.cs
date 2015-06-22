using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using AppUtilsEditor;


public class UserDataDebugWindow : EditorWindow
{
	class EditMenuItem
	{
		public string menuName;
		public IEditWindowDrawer drawer;
		public EditMenuItem(string menuName, IEditWindowDrawer drawer)
		{
			this.menuName = menuName;
			this.drawer = drawer;
		}
	}

	UserData _userData;
	List<EditMenuItem> menuItems;
	int selectedNum;

	[MenuItem("Debug/User Data Edit")]
	public static void Open()
	{
		EditorWindowUtility.ShowWindow<UserDataDebugWindow>(640, 320, true).LockResize();
	}

	void OnEnable()
	{
		_userData = UserDataControl.Data;
		selectedNum = 0;
		menuItems = new List<EditMenuItem>();
		menuItems.Add(new EditMenuItem("Option", new UserDataOptionDrawer(_userData.option)));
		menuItems.Add(new EditMenuItem("PET Data", new UserDataPETDataDrawer(_userData.petData)));
		menuItems.Add(new EditMenuItem("Personal Parts", new UserDataPersonalPartsDrawer(_userData.personalParts)));
	}

	void OnGUI()
	{
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		LeftMenu(position.width * 0.25f);
		RightMenu(position.width * 0.70f);
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		BottomMenu();
		GUILayout.Space(2f);
		EditorGUILayout.EndVertical();
	}

	void LeftMenu(float width)
	{
		GUILayoutOption height = GUILayout.Height(30);
		EditorGUILayout.BeginVertical(GUILayout.Width(width));
		for (int i = 0; i < menuItems.Count; ++i)
		{
			ItemDraw(i, menuItems[i], height);
		}
		EditorGUILayout.EndVertical();
	}

	void ItemDraw(int index, EditMenuItem item, params GUILayoutOption[] options)
	{
		Color defColor = GUI.backgroundColor;
		if (index == selectedNum)
		{
			GUI.backgroundColor = new Color(0.75f, 0.75f, 1.0f);
		}
		if (GUILayout.Button(item.menuName, options))
		{
			selectedNum = index;
		}
		GUI.backgroundColor = defColor;
	}

	void RightMenu(float width)
	{
		EditorGUILayout.BeginVertical(GUILayout.Width(width));
		GUILayout.Space(2f);
		menuItems[selectedNum].drawer.OnGUI();
		EditorGUILayout.EndVertical();
	}

	void BottomMenu()
	{
		GUILayoutOption width = GUILayout.Width(100f);
		GUILayoutOption height = GUILayout.Height(20f);
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Save", width, height))
		{
			UserDataControl.Save();
		}
		if (GUILayout.Button("Reset", width, height))
		{
			if (EditorUtility.DisplayDialog("Reset?", "", "OK", "Cancel"))
			{
				UserDataControl.Reset();
				this.Close();
			}
		}
		EditorGUILayout.EndHorizontal();
	}
}

public interface IEditWindowDrawer
{
	void OnGUI();
}


/// <summary>
/// ユーザーデータ：オプション項目
/// </summary>
public class UserDataOptionDrawer : IEditWindowDrawer
{
	OptionData _optionData;

	public UserDataOptionDrawer(OptionData optionData)
	{
		this._optionData = optionData;
	}

	public void OnGUI()
	{
		EditorGUILayout.LabelField("Option Data", EditorStyles.largeLabel, GUILayout.Height(20f));
		EditorGUI.indentLevel++;
		EditorGUILayout.LabelField("Volumes");
		EditorGUI.indentLevel++;
		_optionData.volumes.master = EditorGUILayout.Slider("Master", _optionData.volumes.master, 0f, 1f);
		_optionData.volumes.bgm = EditorGUILayout.Slider("BGM", _optionData.volumes.bgm, 0f, 1f);
		_optionData.volumes.se = EditorGUILayout.Slider("SE", _optionData.volumes.se, 0f, 1f);
		_optionData.volumes.isMute = EditorGUILayout.Toggle("Mute", _optionData.volumes.isMute);
		EditorGUI.indentLevel--;
		EditorGUI.indentLevel--;
	}
}


/// <summary>
/// ユーザーデータ：PETデータ項目
/// </summary>
public class UserDataPETDataDrawer : IEditWindowDrawer
{
	PETData _petData;

	public UserDataPETDataDrawer(PETData petData)
	{
		this._petData = petData;
	}

	public void OnGUI()
	{
		EditorGUILayout.LabelField("PET Data", EditorStyles.largeLabel, GUILayout.Height(20f));
		EditorGUI.indentLevel++;
		EditorGUILayout.LabelField("PET Equip Personal Parts ID");
		EditorGUI.indentLevel++;
		_petData.petEquip.leftPID = EditorGUILayout.IntField("Left", _petData.petEquip.leftPID);
		_petData.petEquip.rightPID = EditorGUILayout.IntField("Right", _petData.petEquip.rightPID);
		_petData.petEquip.topPID = EditorGUILayout.IntField("Top", _petData.petEquip.topPID);
		_petData.petEquip.behindPID = EditorGUILayout.IntField("Behind", _petData.petEquip.behindPID);
		EditorGUI.indentLevel--;
		EditorGUI.indentLevel--;
	}
}


/// <summary>
/// ユーザーデータ：所持物パーツデータ項目
/// </summary>
public class UserDataPersonalPartsDrawer : IEditWindowDrawer
{
	Vector2 scroll;
	PersonalParts _perts;

	public UserDataPersonalPartsDrawer(PersonalParts perts)
	{
		this._perts = perts;
		this.scroll = Vector2.zero;
	}

	public void OnGUI()
	{
		EditorGUILayout.LabelField("Personal Parts Data", EditorStyles.largeLabel, GUILayout.Height(20f));
		EditorGUI.indentLevel++;
		PartsDataList(_perts.partsList);
		EditorGUI.indentLevel--;
	}

	void PartsDataList(List<PartsData> dataList)
	{
		bool result = false;
		scroll = EditorGUILayout.BeginScrollView(scroll);
		for (int i = 0; i < dataList.Count; ++i)
		{
			EditorGUILayout.BeginVertical(EditorStyles.textArea);
			EditorGUILayout.LabelField(string.Format("Parts[{0}]", i));
			EditorGUI.indentLevel++;
			result = DrawPartsData(dataList[i]);
			EditorGUI.indentLevel--;
			EditorGUILayout.EndVertical();
			if (result) break;
		}
		EditorGUILayout.EndScrollView();
	}

	bool DrawPartsData(PartsData data)
	{
		bool result = false;
		data.partsID = EditorGUILayout.IntField("Parts ID", data.partsID);
		return result;
	}
}