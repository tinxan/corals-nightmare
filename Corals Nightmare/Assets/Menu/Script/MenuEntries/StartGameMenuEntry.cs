using UnityEngine;
using System.Collections;

public class StartGameMenuEntry : MenuEntry
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
				// starts game and closes menu, then sets the current menu to the Resume Game menu
				GameScript.Instance.StartGame ();
				MenuManager.Instance.DisableMenu ();
				MenuManager.Instance.CurrentMenu = MenuManager.Instance.ResumeGameMenu;
		}



}
