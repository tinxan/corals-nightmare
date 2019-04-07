using UnityEngine;
using System.Collections;

public class EndGameMenuEntry : MenuEntry
{

		// Use this for initialization
		void Awake ()
		{
				init ();
		}

		public override void OnActivate ()
		{

				Application.Quit ();
		}

}
