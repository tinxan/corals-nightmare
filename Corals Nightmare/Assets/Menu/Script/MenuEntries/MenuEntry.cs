using UnityEngine;
using System.Collections;

public abstract class MenuEntry : MonoBehaviour
{

		private Vector3 initialScale;
		private bool _selected = false;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MenuEntry"/> is selected.
		/// </summary>
		/// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
		public bool Selected {				
				get {
						return _selected;
				}				
				set {
						bool valueChanged = _selected != value;

						if (valueChanged) {
								_selected = value;
								//changes the size of the selected item
								if (_selected) {
										transform.localScale = initialScale * 1.1f; 
								} else {
										transform.localScale = initialScale;
								}
						}
				}	
		}

		public abstract void OnActivate ();

		public void init ()
		{
				initialScale = transform.localScale;
		}

}
