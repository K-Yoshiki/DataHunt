using UnityEngine;
using System.Collections;


public class HomingBullet : BulletBase
{
	public float maxRotate;
	Transform target;
	
	protected override void Initialize()
	{
		Destroy(this.gameObject, parameters.lifeTime);
		target = SearchTarget();
	}

	Transform SearchTarget()
	{
		Collider[] searchObjects = Physics.OverlapSphere(transform.position, parameters.range);
		foreach(var obj in searchObjects)
		{
			if (obj.tag == parameters.targetTag)
			{
				return obj.transform;
			}
		}
		return null;
	}
	
	protected override void OnUpdate()
	{
		if (target != null)
		{
			Horming();
		}
		else
		{
			// Nothing Target
			transform.position += transform.TransformDirection(Vector3.forward.normalized) * parameters.speed * Time.deltaTime;
		}					
	}
	
	void Horming()
	{
		//ターゲットまでの角度を取得
		Vector3 vecTarget = target.transform.position - transform.position;     //ターゲットへのベクトル
		Vector3 vecForward = transform.TransformDirection(Vector3.forward.normalized);          //弾の正面ベクトル
		float angleDiff = Vector3.Angle(vecForward, vecTarget);                 //ターゲットまでの角度
		float angleAdd = (maxRotate * Time.deltaTime);                           //回転角
		Quaternion rotTarget = Quaternion.LookRotation(vecTarget);
		
		if (angleDiff <= angleAdd)
		{
			//ターゲットが回転角いないなら完全にターゲットの方向に向く
			transform.rotation = rotTarget;
		}
		else
		{
			//ターゲットが回転角外なら、指定角度だけターゲットに向ける
			float t = (angleAdd / angleDiff);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, t);
		}
		//前進
		transform.position += transform.TransformDirection(Vector3.forward.normalized) * parameters.speed * Time.deltaTime;
	}
	
}