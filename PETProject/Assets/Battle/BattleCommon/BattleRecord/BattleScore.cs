using UnityEngine;
using System.Collections;

/// <summary>
/// バトルのスコア関連情報
/// </summary>
public class BattleScore
{
	/// <summary>
	/// バトルのタイム
	/// </summary>
	public float BattleTime { get; private set; }

	/// <summary>
	/// バトルのスコア(敵の撃破ポイントや何らかのボーナスポイントなど)
	/// </summary>
	public uint Score { get; private set; }


	/// <summary>
	/// タイムの更新
	/// </summary>
	public void TimeUpdate()
	{
		BattleTime += Time.deltaTime;
	}

	/// <summary>
	/// スコアの加算
	/// </summary>
	/// <param name="score">Score.</param>
	public void AddScore(uint score)
	{
		Score += score;
	}
}
