using UnityEngine;
using System.Collections;


/// <summary>
/// 仮Home画面のUIManager
/// </summary>
public class TmpHome_UIManager : MonoBehaviour
{
	public void BattleMove()
	{
		SceneManager.Instance.SetState(SceneState.Battle);
	}

	public void GoToCredit()
	{
		SceneManager.Instance.SetState(SceneState.Credit);
	}
}