using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;


/// <summary>
/// ユーザーに関する様々なデータ
/// </summary>
[Serializable]
public class UserData
{
	/// <summary>
	/// 各種設定データ
	/// </summary>
	public OptionData option;

	/// <summary>
	/// プレイヤーの保存データ
	/// </summary>
	public PlayerData playerData;

	/// <summary>
	/// スコアの情報
	/// </summary>
	public ScoreData scoreData;

	/// <summary>
	/// PETのデータ
	/// </summary>
	public PETData petData;

	/// <summary>
	/// 所持パーツ情報
	/// </summary>
	public PersonalParts personalParts;

	public UserData()
	{
		option = new OptionData();
		playerData = new PlayerData();
		scoreData = new ScoreData();
		petData = new PETData();
		personalParts = new PersonalParts();
	}
}

/// <summary>
/// 所持しているパーツ情報
/// </summary>
[Serializable]
public class PersonalParts
{
	/// <summary>
	/// 所持しているパーツリスト(非整形)
	/// </summary>
	public List<PartsData> partsList;

	/// <summary>
	/// 所持しているパーツリスト(整形済み)
	/// </summary>
	[XmlIgnore]
	public Dictionary<int, PartsData> partsDict;

	public PersonalParts()
	{
		partsList = new List<PartsData>() { new PartsData() { partsID = 0, personalID = 0 } };
		partsDict = new Dictionary<int, PartsData>();
	}

	/// <summary>
	/// リストを整形する
	/// </summary>
	public void ListToDict()
	{
		partsDict.Clear();
		partsList.ForEach(delegate(PartsData data) {
			partsDict[data.personalID] = data;
		});
	}
	
	/// <summary>
	/// 整形データをリストに全て入れる
	/// </summary>
	public void DictToList()
	{
		partsList.Clear();
		foreach (var data in partsDict.Values)
		{
			partsList.Add(data);
		}
	}
	
	/// <summary>
	/// 所持パーツデータをセット
	/// </summary>
	void SetParts(int partsID, int personalID)
	{
		if (partsDict.ContainsKey(personalID) == false)
		{
			partsDict[personalID] = new PartsData();
		}

		PartsData data = partsDict[personalID];
		data.partsID = partsID;
		data.personalID = personalID;
	}

	/// <summary>
	/// 所持パーツを追加
	/// </summary>
	public void AddParts(int partsID)
	{
		if (CheckSameParts(partsID))
			return;

		int personalID = 0;
		while (partsDict.ContainsKey(personalID))
		{
			++personalID;
		}
		SetParts(partsID, personalID);
	}

	/// <summary>
	/// 同じパーツを所持していないかを確認
	/// </summary>
	bool CheckSameParts(int partsID)
	{
		foreach (var item in partsDict.Values)
		{
			if(item.partsID == partsID)
				return true;
		}
		return false;
	}
	
	/// <summary>
	/// 所持パーツデータを取得
	/// </summary>
	public PartsData GetParts(int personalID)
	{
		if (partsDict.ContainsKey(personalID))
		{
			return partsDict[personalID];
		}
		return null;
	}

	public List<PartsData> GetParts()
	{
		List<PartsData> list = new List<PartsData>();
		foreach(var parts in partsDict.Values)
		{
			list.Add(parts);
		}
		return list;
	}
}

/// <summary>
/// パーツ情報
/// </summary>
[Serializable]
public class PartsData
{
	/// <summary>
	/// 所持物としての固有ID
	/// </summary>
	public int personalID;

	/// <summary>
	/// 基本パーツプレハブのID
	/// </summary>
	public int partsID;
	
	public PartsData()
	{
		partsID = 0;
		personalID = 0;
	}
	
	public PartsData(int partsID, int personalID)
	{
		this.partsID = partsID;
		this.personalID = personalID;
	}
}

/// <summary>
/// PETに関するデータ
/// </summary>
[Serializable]
public class PETData
{
	/// <summary>
	/// PETに装着しているパーツID
	/// </summary>
	public PETEquip petEquip;

	public PETData()
	{
		petEquip = new PETEquip();
	}
}

/// <summary>
/// PETに装着している所持パーツIDのデータ
/// </summary>
[Serializable]
public class PETEquip
{
	/// <summary>
	/// (Petからみて)左側面
	/// </summary>
	public int leftPID;
	
	/// <summary>
	/// (Petからみて)右側面
	/// </summary>
	public int rightPID;
	
	/// <summary>
	/// 上面
	/// </summary>
	public int topPID;
	
	/// <summary>
	/// 背面
	/// </summary>
	public int behindPID;
	
	public PETEquip()
	{
		leftPID = 0;
		rightPID = topPID = behindPID = -1;
	}
}

/// <summary>
/// オプションの設定データ(音量など)
/// </summary>
[Serializable]
public class OptionData
{
	/// <summary>
	/// 音量
	/// </summary>
	public VolumeData volumes;

	public OptionData()
	{
		volumes = new VolumeData();
	}
}

/// <summary>
/// プレイヤーの保存データ
/// </summary>
[Serializable]
public class PlayerData
{
	/// <summary>
	/// 金銭用の変数
	/// </summary>
	public uint gold;

	public PlayerData()
	{
		this.gold = 100;
	}
}

/// <summary>
/// 音量データ
/// </summary>
[Serializable]
public class VolumeData
{
	/// <summary>
	/// 全体音量
	/// </summary>
	public float master;

	/// <summary>
	/// 音楽の音量
	/// </summary>
	public float bgm;

	/// <summary>
	/// 効果音の音量
	/// </summary>
	public float se;

	/// <summary>
	/// 消音フラグ
	/// </summary>
	public bool isMute;

	public VolumeData()
	{
		master = 1.0f;
		bgm = se = 1.0f;
		isMute = false;
	}
}

/// <summary>
/// スコアの情報
/// </summary>
[Serializable]
public class ScoreData
{
	/// <summary>
	/// スコアの情報(未整形)
	/// </summary>
	public List<ScoreItem> scoreList;

	/// <summary>
	/// スコアの情報(整形済み)
	/// </summary>
	[NonSerialized]
	Dictionary<uint, Dictionary<uint, ScoreItem>> scoreDict;

	public ScoreData()
	{
		scoreList = new List<ScoreItem>();
		scoreDict = new Dictionary<uint, Dictionary<uint, ScoreItem>>();
	}

	/// <summary>
	/// スコアリストを整形する
	/// </summary>
	public void ListToDict()
	{
		ClearScoreDict();
		scoreList.ForEach(delegate(ScoreItem item)
		{
			SetScore(item.stageID, item.levelID, item.score, item.clearTime);
		});
	}

	/// <summary>
	/// 整形データをリストに全て入れる
	/// </summary>
	public void DictToList()
	{
		scoreList.Clear();
		foreach (var levels in scoreDict.Values)
		{
			foreach (var item in levels.Values)
			{
				scoreList.Add(item);
			}
		}
	}

	/// <summary>
	/// スコア情報のセット
	/// </summary>
	/// <param name="stageID">ステージID</param>
	/// <param name="levelID">レベルID</param>
	/// <param name="score">スコア</param>
	/// <param name="clearTime">クリアタイム</param>
	public void SetScore(uint stageID, uint levelID, uint score, float clearTime)
	{
		if (scoreDict[stageID] == null)
		{
			scoreDict[stageID] = new Dictionary<uint, ScoreItem>();
		}

		ScoreItem item = scoreDict[stageID][levelID];
		item.stageID = stageID;
		item.levelID = levelID;
		item.score = score;
		item.clearTime = clearTime;
	}

	/// <summary>
	/// スコアの取得
	/// </summary>
	/// <returns>スコア情報(スコア情報が無い場合はnullを返します)</returns>
	/// <param name="stageID">ステージID</param>
	/// <param name="levelID">レベルID</param>
	public ScoreItem GetScore(uint stageID, uint levelID)
	{
		ScoreItem item = null;
		if (scoreDict.ContainsKey(stageID))
		{
			if (scoreDict[stageID].ContainsKey(levelID))
			{
				item = scoreDict[stageID][levelID];
			}
		}
		return item;
	}
	
	void ClearScoreDict()
	{
		foreach (var levels in scoreDict.Values)
		{
			levels.Clear();
		}
		scoreDict.Clear();
	}
}

/// <summary>
/// スコアの小情報
/// </summary>
[Serializable]
public class ScoreItem
{
	/// <summary>
	/// ステージID
	/// </summary>
	public uint stageID;

	/// <summary>
	/// レベルID
	/// </summary>
	public uint levelID;

	/// <summary>
	/// スコア情報
	/// </summary>
	public uint score;

	/// <summary>
	/// クリアタイム情報
	/// </summary>
	public float clearTime;

	public ScoreItem()
	{
		stageID = levelID = 0;
		score = 0;
		clearTime = float.MaxValue;
	}

	public ScoreItem(uint stageID, uint levelID, uint score)
	{
		this.stageID = stageID;
		this.levelID = levelID;
		this.score = score;
	}
}