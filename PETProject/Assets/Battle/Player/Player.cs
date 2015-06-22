using UnityEngine;
using System.Collections;


/// <summary>
/// Player Character Params Manage
/// </summary>
public class Player : CharaBase
{
	[SerializeField]
	RailChanger railChanger;

	[SerializeField]
	PartsConnector connector;

 	void Start()
	{
		PETEquip equip = UserDataControl.Data.petData.petEquip;
		RegistShot(connector.SetLeft(PartsLoader.LoadByPersonalID(equip.leftPID)));
		RegistShot(connector.SetRight(PartsLoader.LoadByPersonalID(equip.rightPID)));
		RegistShot(connector.SetTop(PartsLoader.LoadByPersonalID(equip.topPID)));
		RegistShot(connector.SetBehind(PartsLoader.LoadByPersonalID(equip.behindPID)));
	}

	void RegistShot(PlayerParts partsInstance)
	{
		PlayerController controller = PlayerController.Instance;
		if (partsInstance != null)
		{
			foreach(var param in partsInstance.parameters)
			{
				controller.RegistShooter(param.shotPoint);
			}
		}
	}
	
	public override void Damage(int damage)
	{
		railChanger.DamageOutRail();
		BattleUI.Instance.PlayerDamage();
	}

	public override bool IsDead
	{
		get{ return PlayerController.Instance.NowRail > FieldManager.Instance.RailCount() - 1; }
	}
}
