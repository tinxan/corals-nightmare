using UnityEngine;
using System.Collections;

public abstract class Pillow : MonoBehaviour
{
		/// <summary>
		/// The OnGirlPillowHit interface which is implemented by all concrete subclass pillows
		/// </summary>
		/// <param name="girl">Girl.</param>
		public abstract void OnGirlPillowHit (Girl girl);
}
