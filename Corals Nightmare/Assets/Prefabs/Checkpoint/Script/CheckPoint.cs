using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{
		/// <summary>
		/// The checkpoint audio source.
		/// </summary>
		private AudioSource CheckpointAudioSource;
		/// <summary>
		/// The particle emitter1.
		/// </summary>
		public ParticleEmitter particleEmitter1;
		/// <summary>
		/// The particle emitter2.
		/// </summary>
		public ParticleEmitter particleEmitter2;
		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="other">Other.</param>
		public virtual void OnTriggerEnter (Collider other)
		{		
				//when the player passes the checkpoint it is activated, 
				//the particle emitter stops and the audio fades out
				GameScript.Instance.CurrentCheckPoint = this;
				Debug.Log ("Checkpoint activated: " + GameScript.Instance.CurrentCheckPoint.name);

				particleEmitter1.emit = false;
				particleEmitter2.emit = false;
				iTween.AudioTo (this.gameObject, 0f, 1f, 2f);
		}

		void Start ()
		{
				CheckpointAudioSource = GetComponent<AudioSource> ();	
				CheckpointAudioSource.loop = true;
		}
		/// <summary>
		/// restarts the particle emitters and sound
		/// </summary>
		public void Reset ()
		{
				iTween.AudioTo (this.gameObject, 0.025f, 1f, 2f);
				particleEmitter1.emit = true;
				particleEmitter2.emit = true;
		}
}
