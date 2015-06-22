using UnityEngine;
using System.Collections;


/// <summary>
/// ホーム画面の選択リング管理
/// </summary>
public class SelectableRingController : MonoBehaviour
{
	public Camera ringCamera;
	public float rotateSpeed;
	SelectableRing ring;

	void Start()
	{
		ring = FindObjectOfType<SelectableRing>();
	}

	void OnEnable()
	{
		TouchSensor.EnterEvent += OnEnter;
		TouchSensor.SwipeEvent += OnSwipe;
		TouchSensor.ExitEvent += OnExit;
	}

	void OnDisable()
	{
		TouchSensor.EnterEvent -= OnEnter;
		TouchSensor.SwipeEvent -= OnSwipe;
		TouchSensor.ExitEvent -= OnExit;
	}

	void OnEnter(TouchInfo info)
	{
		RaycastHit hit;
		Ray ray = ringCamera.ScreenPointToRay(new Vector3(info.position.x, info.position.y, 0f));
		if (Physics.Raycast(ray, out hit, 500f))
		{
			if (ring == hit.transform.GetComponentInParent<SelectableRing>())
			{
				ring.Catch();
			}
		}
	}

	void OnSwipe(TouchInfo info)
	{
		ring.Rotate(info.deltaPosition.x * -rotateSpeed);
	}

	void OnExit(TouchInfo info)
	{
		ring.Release();
	}
}
