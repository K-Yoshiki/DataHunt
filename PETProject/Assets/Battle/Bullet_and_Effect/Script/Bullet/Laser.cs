using UnityEngine;
using System.Collections;


public class Laser : BulletBase
{
	[SerializeField]
	LineRenderer lineRenderer;
	float rayLength;

	protected override void Initialize()
	{
		Destroy(this.gameObject, parameters.lifeTime);
		rayLength = 0;
	}

	protected override void OnUpdate()
	{
		// レーザーを延ばす処理
		rayLength += parameters.speed * Time.deltaTime;
		lineRenderer.SetPosition(1, new Vector3(0, 0, rayLength));

		// レーザーの当たり判定
		RayCollision();
	}

	/// <summary>
	/// レーザー衝突判定
	/// </summary>
	void RayCollision()
	{
		RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, rayLength);
		foreach(var hit in hits)
		{
			Hit(hit.collider.gameObject, hit.point, false);
		}
	}
}
