using UnityEngine;
using System.Collections;

public class FollowGirlCamera : MonoBehaviour
{
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static FollowGirlCamera Instance { get; private set; }
	
		void Start ()
		{
				if (FollowGirlCamera.Instance == null) {
						FollowGirlCamera.Instance = this;
				} else {						
						throw new UnityException ("FollowGirlCamera already set, there should be just one!");			 
				}

				if (girlTarget == null) {
						throw new UnityException ("Target of FollowGirlCamera is not set!");
				} else {
						targetIsSet = true;
						initialXPos = transform.position.x;
				}

		}
		/// <summary>
		/// Resets the camera over the girl.
		/// </summary>
		public void ResetCameraOverGirl ()
		{
				transform.position += new Vector3 (0, girlTarget.position.y + 100f, 0);
		}
		/// <summary>
		/// the girl target transform
		/// </summary>
		public Transform girlTarget;
		/// <summary>
		/// The initial X position.
		/// </summary>
		private float initialXPos;
		/// <summary>
		/// indicates if the target is set
		/// </summary>
		private bool targetIsSet = false;
		/// <summary>
		/// The distance on the horizontal plane.
		/// </summary>
		public float distance = 10.0f;
		/// <summary>
		/// The vertical distance between the girl and the camera.
		/// </summary>
		public float height = 5.0f;
		/// <summary>
		/// The height damping.
		/// </summary>
		public float heightDamping = 2.0f;
		/// <summary>
		/// The rotation damping.
		/// </summary>
		public float rotationDamping = 3.0f;
		
		void LateUpdate ()
		{
				if (targetIsSet) {
		
						// calculate the current rotation angles
						float wantedRotationAngle = girlTarget.eulerAngles.y;
						float wantedHeight = girlTarget.position.y + height;
		
						float currentRotationAngle = transform.eulerAngles.y;
						float currentHeight = transform.position.y;
		
						// damp the rotation around the y-axis
						currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
						// damp the height
						currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
						// convert the angle into rotation
						Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
						// set the position of the camera on the horisontal plane to:
						// distance behind the target
						Vector3 nextPos = girlTarget.position;
						nextPos -= Vector3.forward * distance;

						// set the height of the camera
						nextPos = new Vector3 (initialXPos, currentHeight, nextPos.z);

						transform.position = nextPos;
						// targeting the girl
						transform.LookAt (girlTarget);
				}
		}
}
