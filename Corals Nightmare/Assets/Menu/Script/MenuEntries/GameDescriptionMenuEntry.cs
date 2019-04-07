using UnityEngine;
using System.Collections;

public class GameDescriptionMenuEntry : MenuEntry
{

		// Use this for initialization
		void Awake ()
		{
				init ();
		}

		public override void OnActivate ()
		{
				// disbles the menu, sets the Description menu to be the current menu and then opens it.
				MenuManager.Instance.DisableMenu ();
				MenuManager.Instance.CurrentMenu = MenuManager.Instance.DescriptionGameMenu;
				MenuManager.Instance.EnableMenu ();
		}

}
