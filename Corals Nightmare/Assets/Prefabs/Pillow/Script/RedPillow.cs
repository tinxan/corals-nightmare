using UnityEngine;
using System.Collections;

public class RedPillow : Pillow
{
		/// <summary>
		/// "kills" the girl and resets the parent of the character
		/// </summary>
		/// <param name="girl">Girl.</param>
		public override void OnGirlPillowHit (Girl girl)
		{
				if (girl.transform.parent != null) {
						girl.transform.parent = null;
				}
				girl.Die ();
		}
}
