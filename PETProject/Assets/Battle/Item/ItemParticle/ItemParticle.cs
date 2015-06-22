using UnityEngine;
using System.Collections;


/// <summary>
/// アイテムの取得演出用パーティクル制御クラス
/// </summary>
public class ItemParticle : MonoBehaviour
{
	public float startStopTime = 1f;
	public float baseSpeed = 10f;
	public EffectBase moveEffect;
	public EffectBase getEffect;
	public AudioClip getSound;

	IEnumerator Start()
	{
		// Start Valiable Set
		Transform target = PlayerController.Instance.Core;
		EffectBase effect = (EffectBase)Instantiate(moveEffect, this.transform.position, Quaternion.identity);
		effect.transform.SetParent(this.transform);

		// Wait Time
		yield return new WaitForSeconds(startStopTime);

		// Move
		float dist = 100f;
		Vector3 vec = Vector3.zero;
		while (Mathf.Abs(dist) >= 0.5f)
		{
			dist = Vector3.Distance(effect.transform.position, target.position);
			vec = Vector3.Normalize(target.position - effect.transform.position);
			effect.transform.position += vec * baseSpeed * Time.deltaTime;
			yield return null;
		}
		Destroy(effect.gameObject);
		
		// Show Get Effect
		EffectBase lastEffect = (EffectBase)Instantiate(getEffect, target.position, Quaternion.identity);
		lastEffect.transform.SetParent(target);
		AppUtils.Sound.Instance.PlaySE(getSound);
		Destroy(this.gameObject);
	}
}
