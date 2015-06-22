using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HomeUIManager : MonoBehaviour
{
	GameObject titleUI;
	GameObject titleMask;
	GameObject statusUI;
	GameObject mainUI;
	GameObject petEditUI;
	GameObject optionUI;
	GameObject missionUI;
	GameObject menuUiRing; 
	GameObject burstPrticle;

	AttachedTween titleUiTween;
	AttachedTween petUiTween;
	AttachedTween optionUiTween;
	AttachedTween missionUITween;

	Text titleText;
	Color startButtonColor;

	float alphaSpeed;
	float speed;



	void OnEnable()
	{
		//初期化処理
		titleUI = gameObject.transform.FindChild("Title_UI").gameObject;
		titleMask = titleUI.transform.FindChild("Mask").gameObject;
		statusUI = gameObject.transform.FindChild("CenterPosition/StatusPanel/StatusUI").gameObject;
		mainUI = gameObject.transform.FindChild("CenterPosition/MainUI").gameObject;
		petEditUI = mainUI.transform.FindChild("PeTEditUI").gameObject;
		optionUI = mainUI.transform.FindChild("CenterPosition/OptionUI").gameObject;
		missionUI = mainUI.transform.FindChild("MissionUI").gameObject;
		menuUiRing = gameObject.transform.Find("MainUI/MenuCircleCamera").gameObject;
		burstPrticle = titleUI.transform.FindChild("Burst").gameObject;

		titleText = titleUI.transform.FindChild("Button/Text").GetComponent<Text>();

		titleUiTween = titleMask.GetComponent<AttachedTween>();
		petUiTween = petEditUI.GetComponent<AttachedTween>();
		optionUiTween = optionUI.GetComponent<AttachedTween>();
		missionUITween = missionUI.GetComponent<AttachedTween>();

		petUiTween.enabled = true;
		optionUiTween.enabled = true;
		missionUITween.enabled = true;

		startButtonColor = titleText.color;
		speed = 1;
	}

	void Update()
	{
		alphaSpeed = Mathf.Abs(Mathf.Sin(Time.time * speed));
		titleText.color = startButtonColor * alphaSpeed;
	}

	public void OnClick()
	{
		Debug.Log("OnClick");

		StartCoroutine("Flash");
	}
	
	public void OnClickPlay()
	{
		Debug.Log("Play");

		SceneManager.Instance.SetState(SceneState.Battle);
	}

	public void OnClickPetEdit()
	{
		petEditUI.SetActive(true);
		petUiTween.enabled = false;
		petUiTween.enabled = true;
	}

	public void OnClickOption()
	{
		optionUI.SetActive(true);
		optionUiTween.enabled = false;
		optionUiTween.enabled = true;
	}
	
	public void NowPetOnClickBack()
	{
		petUiTween.setting.param.option.vec = new Vector3(260,-463,0);
		petUiTween.enabled = false;
		petUiTween.Execute();
		petUiTween.setting.param.option.vec = new Vector3(-260,-463,0);
	}

	public void NowOptionOnClickBack()
	{
		optionUiTween.setting.param.option.vec = new Vector3(260,-463,0);
		optionUiTween.enabled = false;
		optionUiTween.Execute();
		optionUiTween.setting.param.option.vec = new Vector3(-260,-463,0);
	}

	private IEnumerator Flash()
	{
		speed = 30;
		alphaSpeed = Mathf.Abs(Mathf.Sin(Time.time * speed));
		titleText.color = startButtonColor * alphaSpeed;
		titleMask.SetActive(true);
		burstPrticle.SetActive(false);

		yield return new WaitForSeconds(0.2f);

		titleUiTween.Execute();

		yield return new WaitForSeconds(1.0f);

		menuUiRing.SetActive(true);
		mainUI.SetActive(true);
		statusUI.SetActive(true);
		titleUI.SetActive(false);
		titleMask.SetActive(false);
	}
}