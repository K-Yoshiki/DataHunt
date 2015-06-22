using UnityEngine;
using System.Collections;
using AppUtils;

public class UserDataControl
{
	/// <summary>
	/// ユーザーの保存情報
	/// </summary>
	static UserData _userData;
	public static UserData Data
	{
		get {
			Load();
			return _userData;
		}
	}

	// クラス生成防止
	private UserDataControl(){}

	/// <summary>
	/// ユーザーデータの書き込み
	/// </summary>
	public static void Save()
	{
		if (_userData != null)
		{
			_userData.scoreData.DictToList();
			_userData.personalParts.DictToList();
			SaveDataFiler<UserData>.Save(_userData, 0);
		}
	}
	
	/// <summary>
	/// ユーザーデータの読込
	/// </summary>
	public static void Load()
	{
		if (_userData == null)
		{
			_userData = SaveDataFiler<UserData>.Load(0) ?? new UserData();
			_userData.scoreData.ListToDict();
			_userData.personalParts.ListToDict();
		}
	}

	public static void Reset()
	{
		if (_userData != null)
		{
			SaveDataFiler<UserData>.Remove(0);
			_userData = null;
		}
	}
}
