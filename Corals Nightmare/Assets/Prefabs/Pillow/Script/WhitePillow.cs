using UnityEngine;
using System.Collections;

public class WhitePillow : Pillow
{	
		/// <summary>
		/// Resets the parent of the character		
		/// </summary>
		/// <param name="girl">Girl.</param>
		public override void OnGirlPillowHit (Girl girl)
		{
				if (girl.transform.parent != null) {
						girl.transform.parent = null;
				}
		}

}
