using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FramePerSecondSetting : MonoBehaviour
{
	public int targetFrameRate = 60;
	public Text fpsText;
	public bool showFps = true;
	private int frame;

	void Awake()
	{
		Application.targetFrameRate = this.targetFrameRate;
	}

	void Start()
	{
		frame = 0;
		if(fpsText != null && showFps)
			StartCoroutine(FPSChecking());
	}

	void Update()
	{
		++frame;
	}

	IEnumerator FPSChecking()
	{
		while(true)
		{
			fpsText.text = "fps:" + frame.ToString();
			frame = 0;
			yield return new WaitForSeconds(1f);
		}
	}
}