using UnityEngine;
using System.Collections;

public class LesserEnemy : EnemyBase
{
	EnemyBase leader;
	bool setupEnd;

	/// <summary>
	/// このエネミーのリーダーを登録
	/// </summary>
	/// <param name="leader">Leader.</param>
	public void RegistLeader(EnemyBase leader)
	{
		this.leader = leader;
	}

	void Start ()
	{
		setupEnd = false;
		transform.eulerAngles = Vector3.up * enemyParams.angle;
		Vector3 targetPos = Vector3.back * FieldManager.Instance.GetRadius(enemyParams.rail);
		targetPos.y = core.transform.localPosition.y;

		core.transform.localPosition = new Vector3(Random.Range(0f, 1f), -80f, Random.Range(-1f, 1f));
		core.transform.localEulerAngles = new Vector3(0f, 180f, 0f);

		shooter.transform.SetParent(core.transform);
		shooter.transform.localPosition = Vector3.zero;
		shooter.transform.localRotation = Quaternion.identity;

		StartCoroutine(StartMove(targetPos));
	}

	// Start Move
	IEnumerator StartMove(Vector3 pos)
	{
		bool isLoop = true;
		float ySpeed = Mathf.Abs(core.transform.localPosition.y) / 1f;
		float slideSpeed = core.transform.localPosition.x / 1f;

		// Move up
		while(isLoop)
		{
			isLoop = false;
			if (core.transform.localPosition.y < pos.y)
			{
				core.transform.localPosition += Vector3.up * ySpeed * Time.deltaTime;
				isLoop = true;
			}
			if (core.transform.localPosition.x > pos.x)
			{
				core.transform.localPosition += Vector3.left * slideSpeed * Time.deltaTime;
				isLoop = true;
			}
			yield return null;
		}

		// Move to def rail 
		Vector3 p0 = core.transform.localPosition;
		Vector3 p1 = new Vector3(0f, -pos.z * 0.5f, pos.z * 0.1f);
		Vector3 p2 = new Vector3(0f, -pos.z * 0.5f, pos.z * 0.1f);
		Bezier bezier = new Bezier(p0, p1, p2, pos);
		float time = Time.deltaTime;
		float duration = 1f;
		while(time <= duration)
		{
			core.transform.localPosition = bezier.GetPointAtTime(time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		// calibrate local position
		core.transform.localPosition = pos;

		// Setup end
		setupEnd = true;

		enemyParams.speed = CalcSpeed(enemyParams.speed, FieldManager.Instance.GetRadius(enemyParams.rail));
	}

	void Update()
	{
		// Setupが終わっていないならUpdate実行しない
		if (setupEnd == false) return;

		transform.Rotate(Vector3.up * enemyParams.speed * MoveAmount() * Time.deltaTime);
		shooter.OnShot();
	}

	float MoveAmount()
	{
		if (leader == null) return 1f;

		Vector3 forwardPos = leader.core.transform.position;
		EnemyBase forward = leader.Get(leader.GetSubIndex(this) - 1);

		if (forward != null)
		{
			forwardPos = forward.core.transform.position;
		}

		return Vector3.Distance(forwardPos, this.core.transform.position);
	}
}
