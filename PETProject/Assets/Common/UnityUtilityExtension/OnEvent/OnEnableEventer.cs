using UnityEngine;
using UnityEngine.Events;


public class OnEnableEventer : MonoBehaviour
{
	public UnityEvent onEnableEvents;

	void OnEnable()
	{
		onEnableEvents.Invoke();
	}
}
