using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NormalPhase : PhaseLabel
{
	public Text viewNum;
	
	public void Set(int num)
	{
		viewNum.text = num.ToString();
	}
}
