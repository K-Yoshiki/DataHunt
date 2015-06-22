using UnityEngine;


/// <summary>
/// パーツのプレハブリソースロード管理
/// </summary>
public static class PartsLoader
{
	/// <summary>
	/// プレイヤーパーツのロード
	/// </summary>
	public static PlayerParts Load(int partsID)
	{
		string path = "PlayerParts/Parts_" + partsID.ToString("0000");
		PlayerParts parts = Resources.Load<PlayerParts>(path);
		if (parts == null)
		{
			Debug.LogError(string.Format("[{0}] is not found!", path));
		}
		return parts;
	}

	/// <summary>
	/// プレイヤーパーツのロード
	/// </summary>
	public static PlayerParts LoadByPersonalID(int personalID)
	{
		PartsData data = GetPartsID(personalID);
		if (data != null)
			return Load(data.partsID);
		return null;
	}

	static PartsData GetPartsID(int personalID)
	{
		return UserDataControl.Data.personalParts.GetParts(personalID);
	}
}
