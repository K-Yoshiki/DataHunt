using UnityEngine;
using UnityEngine.Events;

public class AwakeEventer : MonoBehaviour
{
	public UnityEvent awakeEvents;

	void Awake()
	{
		awakeEvents.Invoke();
	}
}
