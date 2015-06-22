using UnityEngine;
using System.Collections;

public class Laser_Scale_Cont : MonoBehaviour {

	void Awake()
	{
		var particles = this.gameObject.GetComponentsInChildren<ParticleSystem>();
		for(int i=0; i < particles.Length; i++)
		{
			particles[i].startLifetime *= s_lifeTime;
		}
	}

	public float s_lifeTime = 1.0f;
}
