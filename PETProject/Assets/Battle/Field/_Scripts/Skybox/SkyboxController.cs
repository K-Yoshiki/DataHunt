using UnityEngine;
using System.Collections;


/// <summary>
/// Skybox の操作管理クラス
/// </summary>
public class SkyboxController : MonoBehaviour
{
	[SerializeField]
	SkyboxAnimation skyboxAnim;

	/// <summary>
	/// 落下アニメーションの開始
	/// </summary>
	public void StartFallAnimation()
	{
		skyboxAnim.AnimationStart();
	}

	/// <summary>
	/// 落下アニメーションの終了
	/// </summary>
	public void EndFallAnimation()
	{
		skyboxAnim.AnimationStop();
	}

	void Start()
	{
		if (skyboxAnim == null)
			skyboxAnim = this.GetComponent<SkyboxAnimation>();
		if (skyboxAnim == null)
			skyboxAnim = this.GetComponentInChildren<SkyboxAnimation>();
	}
}
