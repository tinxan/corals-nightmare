using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour
{
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static GameScript Instance { get; private set; }
		/// <summary>
		/// The girl.
		/// </summary>
		public Girl girl;
		
		private delegate void ActionWhileScreenIsBlackDelegate ();
		
		private delegate void ActionWhileScreenIsNotBlackDelegate ();
		/// <summary>
		/// The action while screen is black.
		/// </summary>
		private ActionWhileScreenIsBlackDelegate ActionWhileScreenIsBlack;
		/// <summary>
		/// The action while screen is not black.
		/// </summary>
		private ActionWhileScreenIsNotBlackDelegate ActionWhileScreenIsNotBlack;

		void Start ()
		{
				if (GameScript.Instance == null) {
						GameScript.Instance = this;
				} else {						
						throw new UnityException ("GameScript already set, there should be just one!");			 
				}

				BlackFader.OnFadedComplete += HandleOnFadedComplete;

				// First Checkpoint is Startcheckpoint
				CurrentCheckPoint = StartCheckPoint;
				CanControllCharacter = false;
		}
		/// <summary>
		/// Handles once the fade is complete.
		/// </summary>
		/// <param name="currentAlpha">Current alpha.</param>
		void HandleOnFadedComplete (float currentAlpha)
		{
				if (currentAlpha == BlackFader.BLACK) {	
						// if the screen is black, the actions in ActionWhileScreenIsBlack shall be executed
						if (ActionWhileScreenIsBlack != null) {
								ActionWhileScreenIsBlack ();
						}
						//after Execution the Fade Out begins
						BlackFader.Instance.FadeAway ();

				} else if (currentAlpha == BlackFader.TRANSPARENT) {
						// if the screen is visible, the actions in ActionWhileScreenIsNotBlack shall be executed
						if (ActionWhileScreenIsNotBlack != null) {
								ActionWhileScreenIsNotBlack ();
						}

				}

		}
		/// <summary>
		/// Moves the girl to current checkpoint while screen is black.
		/// </summary>
		void MoveGirlToCurrentCheckpointWhileScreenIsBlack ()
		{
				girl.transform.position = GameScript.Instance.CurrentCheckPoint.transform.position;
				girl.ThirdPersonController.Grounded = false;
				FollowGirlCamera.Instance.ResetCameraOverGirl ();
		}
		/// <summary>
		/// Revives the girl.
		/// </summary>
		void ReviveGirl ()
		{				
				girl.Died = false;
		}
		/// <summary>
		/// The start check point.
		/// </summary>
		public CheckPoint StartCheckPoint;
		/// <summary>
		/// Gets or sets the current check point.
		/// </summary>
		/// <value>The current check point.</value>
		public CheckPoint CurrentCheckPoint { get; set; }
		/// <summary>
		/// Moves the girl to current checkpoint.
		/// </summary>
		public void MoveGirlToCurrentCheckpoint ()
		{
				//sets the actions in case of a black screen
				ActionWhileScreenIsBlack = MoveGirlToCurrentCheckpointWhileScreenIsBlack;
				//sets the actions in case of a visible screen
				ActionWhileScreenIsNotBlack = ReviveGirl;

				BlackFader.Instance.FadeBlack ();
		}
		/// <summary>
		/// Starts the game.
		/// </summary>
		public void StartGame ()
		{
				ActionWhileScreenIsBlack = null;
				ActionWhileScreenIsNotBlack = null;

				BlackFader.Instance.FadeAway ();
				
		}
		/// <summary>
		/// The can controll player.
		/// </summary>
		private bool canControllPlayer;
		/// <summary>
		/// Gets or sets a value indicating whether this user can controll the character.
		/// </summary>
		/// <value><c>true</c> if this instance can controll character; otherwise, <c>false</c>.</value>
		public bool CanControllCharacter {
				get{ return canControllPlayer;}
				set {
						canControllPlayer = value;
						//if the user is disabled to controll the character, the animation stops as well
						if (canControllPlayer) {
								girl.GetComponent<Animator> ().speed = 1f;
						} else {
								girl.GetComponent<Animator> ().speed = 0f;
						}
				}
		}
		/// <summary>
		/// Restarts the game.
		/// </summary>
		public void RestartGame ()
		{
				CurrentCheckPoint = StartCheckPoint;
				CheckPointManager.Instance.ResetAllCheckPoints ();
				MoveGirlToCurrentCheckpoint ();
		}
		/// <summary>
		/// Ends the game.
		/// </summary>
		public void EndGame ()
		{
				MenuManager.Instance.CurrentMenu = MenuManager.Instance.EndGameMenu;
				MenuManager.Instance.EnableMenu ();
		}

}
