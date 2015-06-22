using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditController : MonoBehaviour 
{

	[Multiline()]
	public string[] scenarios;
	[SerializeField] Text uiText;
	[SerializeField][Range(0.001f, 0.3f)]
	float intervalForCharacterDisplay = 0.05f;	
	
	private int currentLine = 0;
	private string currentText = string.Empty;	
	private float timeUntilDisplay = 0;		
	private float timeElapsed = 1;			
	private int lastUpdateCharacter = -1;		
	//private int counter;
	
	void Start()
	{
		SetNextLine();
	}
	
	void Update () 
	{
		if(currentLine < scenarios.Length && Input.GetMouseButtonDown(0))
		{
			SetNextLine();
		}
		else if(currentLine >= scenarios.Length && Input.GetMouseButtonDown(0))
		{

		}

		
		int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
		
		if( displayCharacterCount != lastUpdateCharacter ){
			uiText.text = currentText.Substring(0, displayCharacterCount);
			lastUpdateCharacter = displayCharacterCount;
		}
	}	
	
	void SetNextLine()
	{
		currentText = scenarios[currentLine];
		currentLine ++;
		
		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
		timeElapsed = Time.time;
		
		lastUpdateCharacter = -1;
	}

	public void NextScene()
	{
		SceneManager.Instance.SetState(SceneState.Home);
	}
}
