using UnityEngine;
using System.Linq;
using System.Collections;

public class MenuSelection : MonoBehaviour
{
		/// <summary>
		/// The menu entries.
		/// </summary>
		public MenuEntry[] menuEntries;
		// <summary>
		/// The menu camera.
		/// </summary>/
		public Camera menuCamera;
		/// <summary>
		/// The current selected menu entry.
		/// </summary>
		private MenuEntry currentSelectedMenuEntry;
		/// <summary>
		/// The previous mouse position.
		/// </summary>
		private Vector3 prevMousePosition;
		
		/// <summary>
		/// Gets or sets the current selected menu entry.
		/// </summary>
		/// <value>The current selected menu entry.</value>
		public MenuEntry CurrentSelectedMenuEntry {
				get{ return currentSelectedMenuEntry;}
				set {
						deselectAllMenuEntries ();
						currentSelectedMenuEntry = value;
						currentSelectedMenuEntry.Selected = true;
				}
		}
		/// <summary>
		/// Gets the index of the selected menu entry.
		/// </summary>
		/// <value>The index of the selected menu entry.</value>
		public int SelectedMenuEntryIndex {
				get {
						for (int i = 0; i < menuEntries.Length; i++) {
								if (menuEntries [i] == CurrentSelectedMenuEntry) {
										return i;
								}
						}
						return -1;
				}
		}
		/// <summary>
		/// Deselects all menu entries.
		/// </summary>
		void deselectAllMenuEntries ()
		{
				foreach (MenuEntry entry in menuEntries) {
						entry.Selected = false;
				}
		}

		void Start ()
		{
				CurrentSelectedMenuEntry = menuEntries [0];
				prevMousePosition = Input.mousePosition;
		}
		
		/// <summary>
		/// Reacts the on mouse hover.
		/// </summary>
		void ReactOnMouseHover ()
		{

				bool mouseMoved = Input.mousePosition != prevMousePosition;
				if (mouseMoved) {
						//sends out a ray from the menu camera to the menu entries
						RaycastHit hit;
						Ray ray = menuCamera.ScreenPointToRay (Input.mousePosition);
						if (Physics.Raycast (ray, out hit, 100.0f)) {
								bool hitMenuEntry = hit.collider.GetComponent<MenuEntry> () != null;
								//if the ray hits one of the menu entries MouseOver is called
								if (hitMenuEntry) {
										MenuEntry menuEntry = hit.collider.GetComponent<MenuEntry> ();
										MouseOverMenuEntry (menuEntry);
								}
								
						}
				}
				prevMousePosition = Input.mousePosition;
		}
		/// <summary>
		/// Mouses the over menu entry.
		/// </summary>
		/// <param name="menuEntry">Menu entry.</param>
		void MouseOverMenuEntry (MenuEntry menuEntry)
		{
				CurrentSelectedMenuEntry = menuEntry;
		}

		public float VerticalInput { get { return Input.GetAxisRaw ("Vertical"); } }

		public float HorizontalInput { get { return Input.GetAxisRaw ("Horizontal"); } }

		private bool inputForCurrentFrame = false;
		private bool noInputForPrevFrame = false;

		/// <summary>
		/// Reacts on joystick and keyboard input.
		/// </summary>
		void ReactOnJoystickAndKeyboardInput ()
		{
				//if the user hits up or left over keyboard or joystick input
				if (VerticalInput > 0.5f || HorizontalInput < -0.5f) {				
						inputForCurrentFrame = true;
						//if there is no input in the previous Frame, the previous Menu Entry is selected
						if (noInputForPrevFrame) {
								SelectPrevMenuEntry ();
								
						}		
				//if the user hits down or right over keyboard or joystick input
				} else if (VerticalInput < -0.5f || HorizontalInput > 0.5f) {						
						inputForCurrentFrame = true;
						//if there is no input in the previous Frame, the next Menu Entry is selected
						if (noInputForPrevFrame) {
								SelectNextMenuEntry ();
						}						
				} else {
						inputForCurrentFrame = false;
				}

				noInputForPrevFrame = !inputForCurrentFrame;
		}
		/// <summary>
		/// Selects the next menu entry.
		/// </summary>
		void SelectNextMenuEntry ()
		{
				int nextIndex = SelectedMenuEntryIndex + 1;
				if (nextIndex >= menuEntries.Length) {
						nextIndex = 0;
				}
				CurrentSelectedMenuEntry = menuEntries [nextIndex];
		}
		/// <summary>
		/// Selects the previous menu entry.
		/// </summary>
		void SelectPrevMenuEntry ()
		{
				int prevIndex = SelectedMenuEntryIndex - 1;
				if (prevIndex < 0) {
						prevIndex = menuEntries.Length - 1;
				}
				CurrentSelectedMenuEntry = menuEntries [prevIndex];
		}
		/// <summary>
		/// Reacts the on accept.
		/// </summary>
		void ReactOnAccept ()
		{
				if (Input.GetButtonDown ("MenuEnter") && CurrentSelectedMenuEntry != null) {
						CurrentSelectedMenuEntry.OnActivate ();
				}
		}

		// Update is called once per frame
		void Update ()
		{


				ReactOnMouseHover ();
				ReactOnJoystickAndKeyboardInput ();

				ReactOnAccept ();


		}
}
