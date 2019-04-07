using UnityEngine;
using System.Collections;

public class Girl : MonoBehaviour
{
		/// <summary>
		/// Gets the third person controller.
		/// </summary>
		/// <value>The third person controller.</value>
		public ThirdPersonController ThirdPersonController { get; private set; }
		/// <summary>
		/// Gets the initial parent transform.
		/// </summary>
		/// <value>The initial parent transform.</value>
		public Transform InitialParentTransform { get; private set; }

		void Start ()
		{
				ThirdPersonController = GetComponent<ThirdPersonController> ();
				InitialParentTransform = transform.parent;
		}
		/// <summary>
		/// Raises the controller collider hit event.
		/// </summary>
		/// <param name="hit">Hit.</param>
		void OnControllerColliderHit (ControllerColliderHit hit)
		{		
				//if the colliding object is a pillow, call OnPillowHit
				Pillow pillow = hit.collider.gameObject.GetComponent<Pillow> ();
				if (pillow != null) {
						OnPillowHit (hit, pillow);
				}
		}
		/// <summary>
		/// Raises the pillow hit event.
		/// </summary>
		/// <param name="hit">Hit.</param>
		/// <param name="pillow">Pillow.</param>
		void OnPillowHit (ControllerColliderHit hit, Pillow pillow)
		{
				//call the OnPillowHit of the specific pillow
				pillow.OnGirlPillowHit (this);
		}
		/// <summary>
		/// value indicating wether the character lives or is dead.
		/// </summary>
		private bool _died;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Girl"/> is died.
		/// </summary>
		/// <value><c>true</c> if died; otherwise, <c>false</c>.</value>
		public bool Died {
				get{ return _died;}
				set{ _died = value;
						print ("died: " + _died);}
		}
		/// <summary>
		/// stops the character from moving and resets it to the last chackpoint
		/// </summary>
		public void Die ()
		{
				if (!Died) {
						Died = true;
						GameScript.Instance.MoveGirlToCurrentCheckpoint ();
				}
		
		}



}
