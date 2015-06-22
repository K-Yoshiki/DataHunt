using UnityEngine;
using System.Collections;

/// <summary>
/// MonoBehaviourを継承したシングルトンの基底クラス
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	/// <summary>
	/// SingletonMonoBehaviourインスタンスの取得
	/// </summary>
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = (T)FindObjectOfType(typeof(T));
			}
			return instance;
		}
	}

	protected virtual void Awake()
	{
		CheckInstance();
	}

	/// <summary>
	/// インスタンスの確認
	/// </summary>
	/// <returns></returns>
	protected bool CheckInstance()
	{
		if (this == Instance)
			return true;
		Destroy(this.gameObject);
		return false;
	}
}
