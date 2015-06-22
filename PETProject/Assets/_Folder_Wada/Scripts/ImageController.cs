using UnityEngine;
using System.Collections;

public class ImageController : MonoBehaviour {

	public GameObject[] tutorialImages = new GameObject[6] ;
	private int counter;

	// Use this for initialization
	void Start () 
	{
		tutorialImages[0].SetActive(true);
		tutorialImages[1].SetActive(false);
		tutorialImages[2].SetActive(false);
		tutorialImages[3].SetActive(false);
		tutorialImages[4].SetActive(false);
		tutorialImages[5].SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			OnClick();
			SetImage();
		}
	}

	void OnClick()
	{
		if(Input.GetMouseButtonDown(0) && counter < tutorialImages.Length)
		{
			counter = counter >= tutorialImages.Length ? 0 : counter + 1;
		}
	}
	
	void SetImage()
	{
		if(counter == 0)
		{
			tutorialImages[0].SetActive(true);
			tutorialImages[1].SetActive(false);
			tutorialImages[2].SetActive(false);
			tutorialImages[3].SetActive(false);
			tutorialImages[4].SetActive(false);
			tutorialImages[5].SetActive(false);
		}
		else if(counter == 1)
		{
			tutorialImages[0].SetActive(true);
			tutorialImages[1].SetActive(false);
			tutorialImages[2].SetActive(false);
			tutorialImages[3].SetActive(false);
			tutorialImages[4].SetActive(false);
			tutorialImages[5].SetActive(false);
		}
		else if(counter == 2)
		{
			tutorialImages[0].SetActive(false);
			tutorialImages[1].SetActive(false);
			tutorialImages[2].SetActive(false);
			tutorialImages[3].SetActive(true);
			tutorialImages[4].SetActive(false);
			tutorialImages[5].SetActive(false);

		}
		else if(counter == 3)
		{
			tutorialImages[0].SetActive(false);
			tutorialImages[1].SetActive(false);
			tutorialImages[2].SetActive(false);
			tutorialImages[3].SetActive(false);
			tutorialImages[4].SetActive(true);
			tutorialImages[5].SetActive(false);
		}
		else if(counter == 4)
		{
			tutorialImages[0].SetActive(false);
			tutorialImages[1].SetActive(false);
			tutorialImages[2].SetActive(false);
			tutorialImages[3].SetActive(false);
			tutorialImages[4].SetActive(false);
			tutorialImages[5].SetActive(true);
		}
		else if(counter == 5)
		{
			tutorialImages[0].SetActive(true);
			tutorialImages[1].SetActive(false);
			tutorialImages[2].SetActive(false);
			tutorialImages[3].SetActive(false);
			tutorialImages[4].SetActive(false);
			tutorialImages[5].SetActive(false);
		}
	}
}
