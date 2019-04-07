using UnityEngine;
using System.Collections;

public class RestartGameMenuEntry : MenuEntry
{

		// Use this for initialization
		void Awake ()
		{
				init ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public override void OnActivate ()
		{
				// restarts the game and closes the menu then sets current menu to be the Resume Game menu.
				GameScript.Instance.RestartGame ();
				MenuManager.Instance.DisableMenu ();
				MenuManager.Instance.CurrentMenu = MenuManager.Instance.ResumeGameMenu;
		}

}
