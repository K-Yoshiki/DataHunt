using UnityEngine;


namespace AppUtils.SoundPlayer
{
	/// <summary>
	/// Wrapping class of AudioSource
	/// </summary>
	public class AudioData
	{
		public AudioSource source;
	
		public AudioClip clip
		{
			get { return source.clip; }
			set
			{
				if (source.clip != value)
				{
					source.clip = value;
				}
			}
		}
	
		public float volume
		{
			get { return source.volume; }
			set { source.volume = value; }
		}
	
		public bool isPlaying
		{
			get { return source.isPlaying; }
		}
	
		public bool loop
		{
			get { return source.loop; }
			set { source.loop = value; }
		}
	
		public bool playOnAwake
		{
			get { return source.playOnAwake; }
			set { source.playOnAwake = value; }
		}
	
		public AudioData(AudioSource source)
		{
			this.source = source;
		}
	}
}