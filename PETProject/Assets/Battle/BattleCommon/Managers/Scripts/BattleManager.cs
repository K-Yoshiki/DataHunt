using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Battle manager.
/// </summary>
public class BattleManager : MonoSingleton<BattleManager>
{
	/// <summary>
	/// プレイヤーのプレハブ
	/// </summary>
	[SerializeField]
	Player playerPrefab;

	/// <summary>
	/// テスト用のサンプルバトルデータ
	/// </summary>
	[SerializeField]
	BattleData testBattleData;

	/// <summary>
	/// バトルのフェーズサイクル管理クラス
	/// </summary>
	BattlePhaseCycle phaseCycle;

	/// <summary>
	/// エネミーの管理クラス
	/// </summary>
	EnemiesManager enemiesManager;


	/// <summary>
	/// 次のフェーズへいけるかどうか
	/// </summary>
	/// <returns><c>true</c> if this instance is can next go; otherwise, <c>false</c>.</returns>
	public bool CanNextPhase()
	{
		return enemiesManager.IsAllHuntEnemy();
	}

	public void StartBattle()
	{
		BattleData data = SceneManager.Instance.GetSceneData<BattleData>() ?? testBattleData;
		enemiesManager = new EnemiesManager(data.FieldDropTable, data.BossDropTable);
		List<IState> phases = new List<IState>();
		// Cycle Phases Add
		for (int i = 0; i < data.PhaseList.Count - 1; ++i)
		{
			phases.Add(new ChangePhase(data.PhaseList[i], phaseCycle, PhaseType.Normal, (byte)(i + 1)));
			phases.Add(new BattlePhase(data.PhaseList[i], phaseCycle, enemiesManager, PhaseType.Normal));
		}
		
		// Boss Phase Add
		int lastIndex = data.PhaseList.Count - 1;
		phases.Add(new ChangePhase(data.PhaseList[lastIndex], phaseCycle, PhaseType.Boss, (byte)(lastIndex + 1)));
		phases.Add(new BattlePhase(data.PhaseList[lastIndex], phaseCycle, enemiesManager, PhaseType.Boss));
		
		// Result Phase Add
		phases.Add(new ResultPhase(data));

		// BGMデータをセット
		BattleSound.Instance.SetData(data.StageBGM, data.BossBGM);
		
		// フェーズサイクルを初期化
		phaseCycle.Initialize(phases.ToArray(), 3);
	}

	protected override void Awake()
	{
		base.Awake();
		phaseCycle = new BattlePhaseCycle();
		
		// フィールドを初期化する
		FieldManager.Instance.Initialize();
		FieldManager.Instance.GameOverAnim();
		// プレイヤーのインスタンスを生成する
		Instantiate(playerPrefab);
	}

	void Update()
	{
		phaseCycle.Update();
	}	
}