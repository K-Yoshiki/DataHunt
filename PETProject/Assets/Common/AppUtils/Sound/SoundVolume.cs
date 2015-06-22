using UnityEngine;
using System.Collections;


namespace AppUtils
{
	public static class SoundVolume
	{
		static float master = 1.0f;
		static float bgm = 1.0f;
		static float se = 1.0f;
		static float mute = 1.0f;

		/// <summary>
		/// Gets or sets the master volume.
		/// </summary>
		/// <value>Master volume.</value>
		public static float Master
		{
			get { return master; }
			set { master = Mathf.Clamp(value, 0f, 1f); }
		}

		/// <summary>
		/// Gets or sets the BGM volume.
		/// </summary>
		/// <value>BGM volume.</value>
		public static float BGM
		{
			get { return bgm; }
			set { bgm = Mathf.Clamp(value, 0f, 1f); }
		}

		/// <summary>
		/// Gets or sets the SE volume.
		/// </summary>
		/// <value>SE volume.</value>
		public static float SE
		{
			get { return se; }
			set { se = Mathf.Clamp(value, 0f, 1f); }
		}

		/// <summary>
		/// Gets or sets the Mute Flag.
		/// </summary>
		public static bool IsMute
		{
			get { return mute <= 0f; }
			set { mute = value ? 0f : 1f; } 
		}

		/// <summary>
		/// Gets the PlayBGM volume.
		/// </summary>
		/// <value>PlayBGM volume.</value>
		public static float PlayBGMVolume
		{
			get { return master * bgm * mute; }
		}

		/// <summary>
		/// Gets the PlaySE volume.
		/// </summary>
		/// <value>PlaySE volume.</value>
		public static float PlaySEVolume
		{
			get { return master * se * mute; }
		}
	}
}