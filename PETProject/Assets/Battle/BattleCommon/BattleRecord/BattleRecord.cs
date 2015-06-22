using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// バトル中の情報保持, 保存動作クラス
/// </summary>
public class BattleRecord : MonoSingleton<BattleRecord>
{
	/// <summary>
	/// アイテム取得時のプレハブ
	/// </summary>
	public ItemParticle itemPerticle;

	/// <summary>
	/// バトルのスコア情報
	/// </summary>
	BattleScore scores;

	/// <summary>
	/// 取得したアイテムリスト
	/// </summary>
	List<int> getItemIDList;


	/// <summary>
	/// バトルのタイム
	/// </summary>
	/// <value>The battle time.</value>
	public float BattleTime { get { return scores.BattleTime; } }

	/// <summary>
	/// 現在のスコア情報
	/// </summary>
	/// <value>The score.</value>
	public uint Score { get { return scores.Score; } }

	/// <summary>
	/// 取得したパーツのID
	/// </summary>
	/// <value>The geted item identifier list.</value>
	public List<int> GetedItemIDList { get { return getItemIDList; } }


	// Initialize
	void Start()
	{
		scores = new BattleScore();
		getItemIDList = new List<int>();
	}

	/// <summary>
	/// バトルのスコア, 取得アイテムなどをユーザーデータに保存します
	/// </summary>
	public void SaveRecord()
	{
		UserData userData = UserDataControl.Data;
		//SaveScores(stageID, levelID, userData);
		SaveItem(userData);
		UserDataControl.Save();
	}

	/// <summary>
	/// タイマーのアップデートを行います
	/// </summary>
	public void TimeUpdate()
	{
		scores.TimeUpdate();
	}

	/// <summary>
	/// スコアに加算します
	/// </summary>
	/// <param name="score">Score.</param>
	public void AddScore(uint score)
	{
		scores.AddScore(score);
	}

	/// <summary>
	/// アイテムの取得
	/// </summary>
	/// <param name="item">Item ID.</param>
	public void AddItem(int itemID, Vector3 pos)
	{
		AddItemIDList(itemID);
		Instantiate(itemPerticle, pos, Quaternion.identity);
	}

	void AddItemIDList(int itemID)
	{
		foreach (var id in getItemIDList)
		{
			if (id == itemID)
				return;
		}
		getItemIDList.Add(itemID);
	}

	/// <summary>
	/// スコア関連情報をユーザーデータに入れ込む
	/// </summary>
	void SaveScores(uint stageID, uint levelID, UserData userData)
	{
		ScoreItem scoreItem = userData.scoreData.GetScore(stageID, levelID);
		if (scoreItem == null)
		{
			userData.scoreData.SetScore(stageID, levelID, Score, BattleTime);
			return;
		}
		uint registScore = System.Math.Max(this.Score, scoreItem.score);
		float registTime = System.Math.Min(this.BattleTime, scoreItem.clearTime);
		userData.scoreData.SetScore(stageID, levelID, registScore, registTime);
	}

	/// <summary>
	/// アイテム情報をユーザーデータに入れ込む
	/// </summary>
	void SaveItem(UserData userData)
	{
		getItemIDList.ForEach(delegate(int id)
		{
			ItemData data = ItemDataLoader.LoadItem(id);
			if (data == null) return;
			data.OnSaveInUser(userData);
		});
	}
}
