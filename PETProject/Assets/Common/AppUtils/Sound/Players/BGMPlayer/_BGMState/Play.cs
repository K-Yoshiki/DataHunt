using UnityEngine;


namespace AppUtils.SoundPlayer
{
	class Play : IState
	{
		AudioData audio;
		
		public Play(AudioData audio)
		{
			this.audio = audio;
		}
		
		public override void Initialize()
		{
			audio.volume = SoundVolume.PlayBGMVolume;
			if (audio.isPlaying == false)
			{
				audio.source.Play();
			}
		}
		
		public override void Update()
		{
			audio.volume = SoundVolume.PlayBGMVolume;
		}
	}
}