using UnityEngine;
using System.Collections;

public class MovingPillow : Pillow
{
		/// <summary>
		/// The x delta.
		/// </summary>
		public float xDelta = -0.2f;
		/// <summary>
		/// The y delta.
		/// </summary>
		public float yDelta = -0.2f;
		/// <summary>
		/// The animation time.
		/// </summary>
		public float time = 3.0f;
		// Use this for initialization
		void Start ()
		{
				//starts the animation in x or y direction
				if (xDelta != 0.0f) {
						iTween.MoveBy (gameObject, iTween.Hash ("x", xDelta, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", 0.1, "time", time));
				} else if (yDelta != 0.0f) {
						iTween.MoveBy (gameObject, iTween.Hash ("y", yDelta, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", 0.1, "time", time));
				}
		}
		/// <summary>
		/// Sets the pillow as parent for the character so it is moving while standing on the pillow
		/// </summary>
		/// <param name="girl">Girl.</param>
		public override void OnGirlPillowHit (Girl girl)
		{
				if (girl.transform.parent != transform) {
						girl.transform.parent = transform;
				}
		}
}
