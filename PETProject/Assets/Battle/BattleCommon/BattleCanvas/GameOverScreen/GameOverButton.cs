using UnityEngine;
using UnityEngine.UI;
using AppUtils;


public class GameOverButton : MonoBehaviour
{
	/// <summary>
	/// ボタンイメージ
	/// </summary>
	[SerializeField]
	Image image;

	/// <summary>
	/// ボタンテキスト
	/// </summary>
	[SerializeField]
	Text text;

	/// <summary>
	/// ボタン
	/// </summary>
	[SerializeField]
	Button button;

	/// <summary>
	/// ボタンの音
	/// </summary>
	[SerializeField]
	AudioClip buttonSound;


	/// <summary>
	/// 押せるかどうか
	/// </summary>
	public void SetButtonActive(bool active)
	{
		if (active)
		{
			ChangeCanPress();
		}
		else
		{
			ChangeNonPress();
		}
	}
	
	/// <summary>
	/// ボタンが押されたとき
	/// </summary>
	public void OnPress()
	{
		PlaySound();
		SetIntaractive(false);
	}

	void ChangeCanPress()
	{
		image.color = ChangeAlpha(image.color, 1f);
		text.color = ChangeAlpha(text.color, 1f);
		SetIntaractive(true);
	}

	void ChangeNonPress()
	{
		image.color = ChangeAlpha(image.color, 0.25f);
		text.color = ChangeAlpha(text.color, 0.25f);
		SetIntaractive(false);
	}

	public void SetIntaractive(bool active)
	{
		button.interactable = active;
	}

	void PlaySound()
	{
		if (buttonSound != null)
			Sound.Instance.PlaySE(buttonSound, Sound.Instance.transform);
	}

	Color ChangeAlpha(Color color, float alpha)
	{
		color.a = alpha;
		return color;
	}
}
