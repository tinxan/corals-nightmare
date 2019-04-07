using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{

		/// <summary>
		/// Gets the Singleton instance.
		/// </summary>
		/// <value>The instance.</value>
		public static MenuManager Instance { get; private set; }
	
		void Start ()
		{
				if (MenuManager.Instance == null) {
						MenuManager.Instance = this;
				} else {						
						throw new UnityException ("MenuManager already set, there should be just one!");			 
				}

				if (StartGameMenu == null || EndGameMenu == null || ResumeGameMenu == null) {
						throw new UnityException ("Not all menues are set!");		
				}

				

				StartGameMenu.gameObject.SetActive (false);
				EndGameMenu.gameObject.SetActive (false);
				ResumeGameMenu.gameObject.SetActive (false);
				DescriptionGameMenu.gameObject.SetActive (false);
				CreditsGameMenu.gameObject.SetActive (false);

				CurrentMenu = StartGameMenu;
				EnableMenu ();
		}

		void Update ()
		{
				// if the user hits ESC or the Controllers Start Button the menu will be opened
				if (Input.GetButtonDown ("ToggleMenu")) {
						//if the menu is active, disables it
						if (CurrentMenu.gameObject.activeSelf) {
								DisableMenu ();
								//if the current menu is the description menu, then go to the previous
								if (CurrentMenu == DescriptionGameMenu) {
										CurrentMenu = PreviousMenu;
										EnableMenu ();
								} 
						} else {
								EnableMenu ();
						}
				}
		}
		/// <summary>
		/// The current menu.
		/// </summary>
		private MenuSelection currentMenu;
		/// <summary>
		/// Gets or sets the current menu.
		/// </summary>
		/// <value>The current menu.</value>
		public MenuSelection CurrentMenu { 
				get { 
						return currentMenu;
				}
			// when the current menu is set, the previous menu will be stored to go back to
				set {
						PreviousMenu = currentMenu;
						currentMenu = value;
				}
		}
		/// <summary>
		/// Gets the previous menu.
		/// </summary>
		/// <value>The previous menu.</value>
		public MenuSelection PreviousMenu { get; private set; }
		/// <summary>
		/// The start game menu.
		/// </summary>
		public MenuSelection StartGameMenu;
		/// <summary>
		/// The description game menu.
		/// </summary>
		public MenuSelection DescriptionGameMenu;
		// <summary>
		/// The end game menu.
		/// </summary>/
		public MenuSelection EndGameMenu;
		/// <summary>
		/// The resume game menu.
		/// </summary>
		public MenuSelection ResumeGameMenu;
		/// <summary>
		/// The credits game menu.
		/// </summary>
		public MenuSelection CreditsGameMenu;
		
		/// <summary>
		/// Closes the menu and enables the player to controll the character.
		/// </summary>
		public void DisableMenu ()
		{
				CurrentMenu.gameObject.SetActive (false);
				GameScript.Instance.CanControllCharacter = true;
		}
		/// <summary>
		/// Enables the menu and disables the player to controll the character.
		/// </summary>
		public void EnableMenu ()
		{
				CurrentMenu.gameObject.SetActive (true);
				GameScript.Instance.CanControllCharacter = false;
		}
}
