using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CenterHoleColorChanger), typeof(CenterHoleAnimation), typeof(AttachedTween))]
public class CenterHole : MonoBehaviour
{
	[SerializeField]
	Color closeColor;

	CenterHoleAnimation holeAnim;
	CenterHoleColorChanger holeColor;
	AttachedTween tween;

	void Awake()
	{
		holeAnim = GetComponent<CenterHoleAnimation>();
		holeColor = GetComponent<CenterHoleColorChanger>();
		tween = GetComponent<AttachedTween>();
	}

	public void StartAnim()
	{
		holeAnim.StartAnim();
	}

	public void StopAnim()
	{
		holeAnim.StopAnim();
	}

	public void HoleLost()
	{
		tween.Execute();
	}

	public void ChangeColor(float colorMoveTime)
	{
		holeColor.ChangeColor(closeColor, colorMoveTime);
	}

	public void ResetColor(float colorMoveTime)
	{
		holeColor.ResetColor(colorMoveTime);
	}
}
