
using UnityEngine;
using System.Collections;

public class MouseEffect : MonoBehaviour 
{
	public Camera mainCamera;
	public GameObject targetPanel;
	public GameObject targetPrefab1;
	public GameObject targetPrefab2;
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0) == false){
			return;
		}

		Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 targetPos     = targetPanel.transform.InverseTransformPoint(mouseWorldPos);
		
		GameObject target1 = (GameObject)Instantiate(targetPrefab1);
		GameObject target2 = (GameObject)Instantiate(targetPrefab2);
		
		target1.transform.parent = targetPanel.transform;
		target2.transform.parent = targetPanel.transform;

		target1.transform.localPosition = targetPos;
		target1.transform.localRotation = Quaternion.identity;
		target2.transform.localPosition = targetPos;
		target2.transform.localRotation = Quaternion.identity;
		
		target1.transform.localScale    = targetPrefab1.transform.localScale;
		target2.transform.localScale    = targetPrefab2.transform.localScale;

		Destroy(target1, 1.0f);
		Destroy(target2, 1.0f);
	}
}
