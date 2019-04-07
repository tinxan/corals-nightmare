using UnityEngine;
using System.Collections;

public class BackMenuEntry : MenuEntry
{

		void Awake ()
		{
				init ();
		}

		public override void OnActivate ()
		{
				// disbles the menu, sets the previous menu to be the current menu and then opens it.
				MenuManager.Instance.DisableMenu ();
				MenuManager.Instance.CurrentMenu = MenuManager.Instance.PreviousMenu;
				MenuManager.Instance.EnableMenu ();


		}

}
