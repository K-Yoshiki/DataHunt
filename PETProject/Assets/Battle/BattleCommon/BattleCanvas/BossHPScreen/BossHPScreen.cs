using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ボスHP表示用の画面管理
/// </summary>
public class BossHPScreen : MonoBehaviour
{
	[SerializeField]
	Slider hpBar;
	Boss boss;

	public void ShowBossHP(Boss boss)
	{
		hpBar.maxValue = boss.DefHP;
		hpBar.value = boss.HP;
		this.boss = boss;
		this.gameObject.SetActive(true);
	}

	public void CloseBossHP()
	{
		this.gameObject.SetActive(false);
	}

	void Update()
	{
		hpBar.value = boss.HP;
	}
}
