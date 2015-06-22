using UnityEngine;
using System.Collections;


/// <summary>
/// Character Interface.
/// (ex: Player, Enemy, Boss, object have HP)
/// </summary>
public abstract class CharaBase : MonoBehaviour
{
	public abstract void Damage(int damage);
	public abstract bool IsDead { get; }

	/// <summary>
	/// レール半径から速度を調節する
	/// </summary>
	/// <returns>The speed.</returns>
	/// <param name="baseSpeed">Base speed.</param>
	/// <param name="radius">radius.</param>
	public static float CalcSpeed(float baseSpeed, float radius)
	{
		float baseRadius = FieldManager.Instance.GetRadius(0);
		float baseLength = baseRadius * 2.0f * Mathf.PI;
		float length = radius * 2.0f * Mathf.PI;

		if (length == 0f)
		{
			return 0f;
		}
		return baseSpeed * (baseLength / length);
	}
}