using UnityEngine;
using System.Collections;


namespace AppUtils
{
	/// <summary>
	/// Singleton for Unity MonoBehaviour
	/// </summary>
	public abstract class UnitySingleton<T> : MonoBehaviour where T : UnitySingleton<T>
	{
		static T instance = null;

		public static T Instance
		{
			get
			{
				if (instance != null)
				{
					return instance;
				}

				T[] instances = GameObject.FindObjectsOfType<T>();
				if (instances.Length >= 1)
				{
					for (int i = 1; i < instances.Length; ++i)
					{
						GameObject.DestroyImmediate(instances[i].gameObject);
					}
					instance = instances[0];
				}

				if (instance == null)
				{
					System.Type type = typeof(T);
					GameObject obj = new GameObject(type.Name, type);
					instance = obj.GetComponent<T>();
				}

				return instance;
			}
		}

		/// <summary>
		/// Initialize calling after "MonoBehaviour Awake()"
		/// </summary>
		protected abstract void Initialize();

		/// <summary>
		/// Calling after "MonoBehaviour OnDestroy()"
		/// </summary>
		protected virtual void Destroyed(){}

		/// <summary>
		/// Calling after "MonoBehaviour OnApplicationQuit()"
		/// </summary>
		protected virtual void AppQuit(){}

		#region UnityEngine.MonoBehaviour.Messages
		void Awake()
		{
			if (instance == null)
			{
				instance = this as T;
				Initialize();
			}
			else
			{
				DestroyImmediate(this.gameObject);
			}
		}

		void OnDestroy()
		{
			if (instance == (this as T))
			{
				instance.Destroyed();
				instance = null;
			}
		}

		void OnApplicationQuit()
		{
			if (instance == (this as T))
			{
				instance.AppQuit();
				instance = null;
			}
		}
		#endregion
	}
}