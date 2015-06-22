using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace AppUtils.SoundPlayer
{
	public sealed class SEPlayer
	{
		Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();
		GameObject planeSounds = new GameObject("Sound2D");

		public SEPlayer(Transform parent)
		{
			planeSounds.transform.SetParent(parent);
		}

		/// <summary>
		/// Play 2D SoundEffect.
		/// </summary>
		/// <param name="clip">Clip.</param>
		public void PlaySE(AudioClip clip)
		{
			if (!sources.ContainsKey(clip.name))
			{
				AudioSource aSource = planeSounds.AddComponent<AudioSource>();
				aSource.clip = clip;
				sources.Add(clip.name, aSource);
			}
			AudioSource audio = sources[clip.name];
			audio.volume = SoundVolume.PlaySEVolume;
			audio.Play();
		}

		/// <summary>
		/// Play 3D SoundEffect to assigned position.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="pos">Position.</param>
		public void PlaySE(AudioClip clip, Vector3 pos)
		{
			AudioSource.PlayClipAtPoint(clip, pos, SoundVolume.PlaySEVolume);
		}

		/// <summary>
		/// Play 3D SoundEffect by following the parent object.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="parent">Parent.</param>
		public void PlaySE(AudioClip clip, Transform parent)
		{
			AudioSource audio = parent.gameObject.AddComponent<AudioSource>();
			audio.clip = clip;
			audio.volume = SoundVolume.PlaySEVolume;
			audio.Play();
			GameObject.Destroy(audio, clip.length);
		}
	}
}