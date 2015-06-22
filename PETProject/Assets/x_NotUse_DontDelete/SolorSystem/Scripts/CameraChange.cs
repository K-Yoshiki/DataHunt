using UnityEngine;
using System.Collections;

public enum Planet
{
	Mercury,
	Venus,
	Earth,
	Mars,
	Jupiter,
	Saturn,
	Uranus,
	Neptune,
	Sun,
};

public class CameraChange : MonoBehaviour {

	public Camera[] PlanetCameras;
	Planet planet = Planet.Sun;

	public float left = 120;
	public float top = 50;
	public float width = 120;
	public float height = 50;

	// Use this for initialization
	void Start () {
		PlanetCameras[0].gameObject.SetActive(true);
		PlanetCameras[1].gameObject.SetActive(false);
		PlanetCameras[2].gameObject.SetActive(false);
		PlanetCameras[3].gameObject.SetActive(false);
		PlanetCameras[4].gameObject.SetActive(false);
		PlanetCameras[5].gameObject.SetActive(false);
		PlanetCameras[6].gameObject.SetActive(false);
		PlanetCameras[7].gameObject.SetActive(false);
		PlanetCameras[8].gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		Change();

	}

	void Change()
	{
		switch(planet)
		{
		case Planet.Mercury:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(true);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		case Planet.Venus:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(true);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		case Planet.Earth:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(true);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		case Planet.Mars:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(true);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		case Planet.Jupiter:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(true);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		case Planet.Saturn:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(true);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		case Planet.Uranus:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(true);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		case Planet.Neptune:
			PlanetCameras[0].gameObject.SetActive(false);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(true);
			break;
		case Planet.Sun:
			PlanetCameras[0].gameObject.SetActive(true);
			PlanetCameras[1].gameObject.SetActive(false);
			PlanetCameras[2].gameObject.SetActive(false);
			PlanetCameras[3].gameObject.SetActive(false);
			PlanetCameras[4].gameObject.SetActive(false);
			PlanetCameras[5].gameObject.SetActive(false);
			PlanetCameras[6].gameObject.SetActive(false);
			PlanetCameras[7].gameObject.SetActive(false);
			PlanetCameras[8].gameObject.SetActive(false);
			break;
		}
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(0,0,80,20),"Mercury"))
		{
			planet = Planet.Mercury;
		}
		if(GUI.Button(new Rect(left,0,width,height),"Venus"))
		{
			planet = Planet.Venus;
		}
		if(GUI.Button(new Rect(left * 2,0,width,height),"Earth"))
		{
			planet = Planet.Earth;
		}
		if(GUI.Button(new Rect(left * 3,0,width,height),"Mars"))
		{
			planet = Planet.Mars;
		}
		if(GUI.Button(new Rect(left * 4,0,width,height),"Jupiter"))
		{
			planet = Planet.Jupiter;
		}
		if(GUI.Button(new Rect(left * 5,0,width,height),"Saturn"))
		{
			planet = Planet.Saturn;
		}
		if(GUI.Button(new Rect(0,20,width,height),"Uranus"))
		{
			planet = Planet.Uranus;
		}
		if(GUI.Button(new Rect(left,20,width,height),"Neptune"))
		{
			planet = Planet.Neptune;
		}
		if(GUI.Button(new Rect(left * 2,20,width,height),"Sun"))
		{
			planet = Planet.Sun;
		}
	}
}
