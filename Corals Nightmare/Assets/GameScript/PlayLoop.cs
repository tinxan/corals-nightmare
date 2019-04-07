using UnityEngine;
using System.Collections;

public class PlayLoop : MonoBehaviour
{
		/// <summary>
		/// The start theme clip.
		/// </summary>
		public AudioClip StartThemeClip;
		/// <summary>
		/// The loop theme clip.
		/// </summary>
		public AudioClip LoopThemeClip;
		/// <summary>
		/// The start theme audio source.
		/// </summary>
		private AudioSource startThemeAudioSource;
		/// <summary>
		/// The loop theme audio source.
		/// </summary>
		private AudioSource loopThemeAudioSource;

		void Awake ()
		{
				// create the player objects
				GameObject startThemePlayer = new GameObject ("startThemeAudioPlayer");
				GameObject loopThemePlayer = new GameObject ("loopThemePlayer");

				// the sound should follow the character controller
				startThemePlayer.transform.parent = gameObject.transform;
				loopThemePlayer.transform.parent = gameObject.transform;

				// add the AudioSource for both clips to the player objects
				startThemeAudioSource = startThemePlayer.AddComponent <AudioSource>() as AudioSource;
				loopThemeAudioSource = loopThemePlayer.AddComponent <AudioSource>() as AudioSource;

				// the clips start manually
				startThemeAudioSource.playOnAwake = false;
				loopThemeAudioSource.playOnAwake = false;

				// assign the clips
				startThemeAudioSource.clip = StartThemeClip;
				loopThemeAudioSource.clip = LoopThemeClip;

				// moving should not cause the doppler effect for the theme clip
				startThemeAudioSource.dopplerLevel = 0;
				loopThemeAudioSource.dopplerLevel = 0;

				// loop the loopclip
				loopThemeAudioSource.loop = true;

				// start both clips, the second starts delayed after the length of the first

				startThemeAudioSource.PlayDelayed (1.0f);
				loopThemeAudioSource.PlayDelayed (1.0f + StartThemeClip.length);
		}



}
