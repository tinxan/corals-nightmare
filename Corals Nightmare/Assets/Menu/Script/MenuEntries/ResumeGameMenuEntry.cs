using UnityEngine;
using System.Collections;

public class ResumeGameMenuEntry : MenuEntry
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
				//closes the menu
				MenuManager.Instance.DisableMenu ();
		}

}
