using UnityEngine;
using System.Collections;


/// <summary>
/// テスト用のビルドセットアップ
/// </summary>
public class PlayerPartsTest : MonoBehaviour
{
	public bool isTest;
	public PlayerParts leftParts;
	public PlayerParts rightParts;
	public PlayerParts topParts;
	public PlayerParts behindParts;
	PartsConnector connecter;

	void Awake()
	{
		if (isTest)
		{
			connecter = GetComponent<PartsConnector>();
			if (connecter != null)
				PartsEntry();
		}
	}

	// パーツ設置
	void PartsEntry()
	{
		PlayerController controller =PlayerController.Instance;

		if (leftParts != null)
			SetRegist(controller, connecter.SetLeft(leftParts));
		if (rightParts != null)
			SetRegist(controller, connecter.SetRight(rightParts));
		if (topParts != null)
			SetRegist(controller, connecter.SetTop(topParts));
		if (behindParts != null)
			SetRegist(controller, connecter.SetBehind(behindParts));
	}

	void SetRegist(PlayerController controller, PlayerParts parts)
	{
		foreach(var param in parts.parameters)
		{
			controller.RegistShooter(param.shotPoint);
		}
	}
}
