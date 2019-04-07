using UnityEngine;
using System.Collections;

public class EndCheckPoint : CheckPoint
{
		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="other">Other.</param>
		public override void OnTriggerEnter (Collider other)
		{
				//ends the game when the player passes the last checkpoint
				base.OnTriggerEnter (other);

				GameScript.Instance.EndGame ();
		}
}
