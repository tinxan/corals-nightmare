using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour
{

		public Girl Girl { get; private set; }
	
		/// <summary>
		/// The moving speed while grounded.
		/// </summary>
		public float groundedSpeed = 2.0f;
		/// <summary>
		/// The in air acceleration.
		/// </summary>
		public float inAirAcceleration = 3.0f;
		/// <summary>
		/// The height of the jump.
		/// </summary>
		public float jumpHeight = 5f;
		/// <summary>
		/// The character gravity.
		/// </summary>
		public float gravity = 20.0f;
		/// <summary>
		/// The speed smoothing.
		/// </summary>
		public float speedSmoothing = 10.0f;
		/// <summary>
		/// The rotate speed.
		/// </summary>
		public float rotateSpeed = 500.0f;
		/// <summary>
		/// The horizontal move direction.
		/// </summary>
		private Vector3 horizontalDirection = Vector3.zero;
		// The current vertical speed
		/// <summary>
		/// The vertical speed.
		/// </summary>
		private float verticalSpeed = 0.0f;
		// The current horizontal move speed
		/// <summary>
		/// The move speed.
		/// </summary>
		private float moveSpeed = 0.0f;
		/// <summary>
		/// The in air velocity.
		/// </summary>
		private Vector3 inAirVelocity = Vector3.zero;
		
		void Awake ()
		{
				horizontalDirection = transform.TransformDirection (Vector3.forward);
		}
		/// <summary>
		/// Gets the forward direction considering the point of view.
		/// </summary>
		/// <value>The forward.</value>
		public Vector3 Forward {
				get {
						Vector3 forward = Camera.main.transform.TransformDirection (Vector3.forward);
						forward.y = 0;
						return forward.normalized;
				}
		}
		/// <summary>
		/// Gets the right direction considering the point of view.
		/// </summary>
		/// <value>The right.</value>
		public Vector3 Right { get { return new Vector3 (Forward.z, 0, -Forward.x); } }
		/// <summary>
		/// Gets the vertical input.
		/// </summary>
		/// <value>The vertical input.</value>
		public float VerticalInput { get { return Input.GetAxisRaw ("Vertical"); } }
		/// <summary>
		/// Gets the horizontal input.
		/// </summary>
		/// <value>The horizontal input.</value>
		public float HorizontalInput { get { return Input.GetAxisRaw ("Horizontal"); } }
		/// <summary>
		/// Gets a value indicating whether the character is moving.
		/// </summary>
		/// <value><c>true</c> if this instance is moving; otherwise, <c>false</c>.</value>
		public bool IsMoving { get { return Mathf.Abs (HorizontalInput) > 0.1 || Mathf.Abs (VerticalInput) > 0.1; } }
		/// <summary>
		/// Gets the target direction.
		/// </summary>
		/// <value>The target direction.</value>
		public Vector3 TargetDirection { get { return HorizontalInput * Right + VerticalInput * Forward; } }
		/// <summary>
		/// Applies smooth movement.
		/// </summary>
		void ApplySmoothMovement ()
		{
		
				Vector3 targetDirection = TargetDirection;
		
				
				if (targetDirection != Vector3.zero) {
			
						horizontalDirection = Vector3.RotateTowards (horizontalDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
			
						horizontalDirection = horizontalDirection.normalized;
			
				}

				var curSmooth = speedSmoothing * Time.deltaTime;
				var targetSpeed = Mathf.Min (targetDirection.magnitude, 1.0f);
				targetSpeed *= groundedSpeed;
				moveSpeed = Mathf.Lerp (moveSpeed, targetSpeed, curSmooth);
				if (!Grounded)
				if (IsMoving)
						inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirAcceleration;		
		}
		/// <summary>
		/// Applies the gravity.
		/// </summary>
		void ApplyGravity ()
		{
				//reset verticle speed if grounded
				if (Grounded) {
						verticalSpeed = 0.0f;
				} else {
						verticalSpeed -= gravity * Time.deltaTime;
				}
		}
		/// <summary>
		/// Gets the jump speed.
		/// </summary>
		/// <returns>The jump speed.</returns>
		/// <param name="targetJumpHeight">Target jump height.</param>
		float GetJumpSpeed (float targetJumpHeight)
		{
				return Mathf.Sqrt (2 * targetJumpHeight * gravity);
		}
		/// <summary>
		/// indicating if the character is jumping.
		/// </summary>
		private bool _jumping = false;
		/// <summary>
		/// indicating if the character is falling.
		/// </summary>
		private bool _falling = false;
		/// <summary>
		/// indicating if the character is grounded.
		/// </summary>
		private bool _grounded = false;
		/// <summary>
		/// the characters speed.
		/// </summary>
		private float _speed = 0.0f;
		/// <summary>
		/// The minimum jump time.
		/// </summary>
		public float minJumpTime = 1000;
		/// <summary>
		/// Gets the animator.
		/// </summary>
		/// <value>The animator.</value>
		public Animator Animator { get; private	set; }
		/// <summary>
		/// Gets the character controller.
		/// </summary>
		/// <value>The character controller.</value>
		public CharacterController CharacterController { get; private set; }
	
		void Start ()
		{
				Animator = GetComponent<Animator> ();
				CharacterController = GetComponent<CharacterController> ();
				Girl = GetComponent<Girl> ();
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ThirdPersonController"/> is jumping.
		/// Also sets the current animation to jumping.
		/// </summary>
		/// <value><c>true</c> if jumping; otherwise, <c>false</c>.</value>
		public bool Jumping {
				get{ return _jumping;}
				set {
			
						bool valueChanged = _jumping != value;
			
						if (valueChanged) {
				
								_jumping = value;
								if (_jumping) {
										if (transform.parent != null) {
												transform.parent = null;
										}
										
										Falling = false;
										Grounded = false;
								}
								Animator.SetBool ("jumping", _jumping);
						}
				}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ThirdPersonController"/> is falling.
		/// Also sets the current animation to falling.
		/// </summary>
		/// <value><c>true</c> if falling; otherwise, <c>false</c>.</value>
		public bool Falling {
				get{ return _falling;}
				set {						
						bool valueChanged = _falling != value;
			
						if (valueChanged) {
								_falling = value;
								if (_falling) {
										if (transform.parent != null) {
												transform.parent = null;
										}
										Jumping = false;
										Grounded = false;
								}
								Animator.SetBool ("falling", _falling);
						}
				}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ThirdPersonController"/> is grounded.
		/// Also sets the current animation to walking.
		/// </summary>
		/// <value><c>true</c> if grounded; otherwise, <c>false</c>.</value>
		public bool Grounded {
				get{ return _grounded;}
				set {
						bool valueChanged = _grounded != value;
			
						if (valueChanged) {
								_grounded = value;
				
								if (_grounded) {
										Falling = false;
										Jumping = false;
										inAirVelocity = Vector3.zero;
								}
								Animator.SetBool ("grounded", _grounded);
						}
				}
		}
		/// <summary>
		/// Gets or sets the speed.
		/// Also sets the speed parameter of the animation.
		/// </summary>
		/// <value>The speed.</value>
		public float Speed {
				get{ return _speed;}
				set {
						_speed = value;
						Animator.SetFloat ("speed", _speed);
				}
		}
		/// <summary>
		/// Sets the movement.
		/// </summary>
		/// <value>The movement.</value>
		public Vector3 Movement {
				set {
						bool characterMoved = value != Vector3.zero;
						if (characterMoved) {
								CollisionFlags collisionFlags = CharacterController.Move (value);
								bool isGrounded = (collisionFlags & CollisionFlags.CollidedBelow) != 0 && !Jumping;

								Grounded = isGrounded;	

								bool hitSomething = collisionFlags != CollisionFlags.None;
								if (hitSomething) {
										inAirVelocity = Vector3.zero;
								}
						}
				}
		}
		/// <summary>
		/// Gets a value indicating whether the jump button is pressed.
		/// </summary>
		/// <value><c>true</c> if jump button pressed; otherwise, <c>false</c>.</value>
		public bool JumpButtonPressed { get { return Input.GetButton ("Jump"); } }
		/// <summary>
		/// Gets a value indicating whether the character can jump.
		/// </summary>
		/// <value><c>true</c> if this instance can jump; otherwise, <c>false</c>.</value>
		public bool CanJump { get { return Grounded; } }
		/// <summary>
		/// Jump with the specified jumpMultiplier.
		/// </summary>
		/// <param name="jumpMultiplier">Jump multiplier.</param>
		public void Jump (float jumpMultiplier)
		{
				Jumping = true;
				verticalSpeed = GetJumpSpeed (jumpMultiplier * jumpHeight);
		}
		/// <summary>
		/// The collision flags.
		/// </summary>
		private CollisionFlags collisionFlags;
	
		void Update ()
		{
				if (GameScript.Instance.CanControllCharacter) {
						ApplySmoothMovement ();
		
						ApplyGravity ();
		
						if (JumpButtonPressed && CanJump) {
								Jump (1f);
						}
		
						if (!Falling && verticalSpeed < -0.1f) {
								Falling = true;
						}
		
						// Calculate the motion
						var movement = horizontalDirection * moveSpeed + new Vector3 (0, verticalSpeed, 0) + inAirVelocity;
						movement *= Time.deltaTime;
		
						// Move the controller
						Movement = movement;
						//sets the speed parameter of the animation
						Speed = new Vector3 (CharacterController.velocity.x, 0, CharacterController.velocity.z).magnitude; 
						//sets the rotation of the character to the horizontal direction
						transform.rotation = Quaternion.LookRotation (horizontalDirection);
		
				}
		}
}
