using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class Pause : IState
	{
		AudioData audio;
		
		public Pause(AudioData audio)
		{
			this.audio = audio;
		}
		
		public override void Initialize()
		{
			if (audio.isPlaying)
			{
				audio.source.Pause();
			}
		}
	}
}