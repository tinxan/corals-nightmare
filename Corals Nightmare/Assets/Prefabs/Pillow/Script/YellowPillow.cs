using UnityEngine;
using System.Collections;

public class YellowPillow : Pillow
{

		public float jumpMultiplier = 2f;
		/// <summary>
		/// Forces the character to jump with the given jump multiplier. 
		/// Also resets the parent of the character		
		/// </summary>
		/// <param name="girl">Girl.</param>
		public override void OnGirlPillowHit (Girl girl)
		{
				if (girl.transform.parent != null) {
						girl.transform.parent = null;
				}
				ThirdPersonController thirdPersonController = girl.gameObject.GetComponent<ThirdPersonController> ();
				thirdPersonController.Jump (jumpMultiplier);
		}
}
