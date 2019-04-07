using UnityEngine;
using System.Collections;

public class BlackFader : MonoBehaviour
{
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static BlackFader Instance { get; private set; }
		/// <summary>
		/// The action which is executed when the fade is complete.
		/// </summary>
		public delegate void FadeCompleteAction (float currentAlpha);
		/// <summary>
		/// Occurs when the fade is complete.
		/// </summary>
		public static event FadeCompleteAction OnFadedComplete;

		/// <summary>
		/// The constant for the black alpha value.
		/// </summary>
		public const float BLACK = 0.5f;
		/// <summary>
		/// The constant for the transpartent alpha value.
		/// </summary>
		public const float TRANSPARENT = 0.0f;

		void Start ()
		{
				if (BlackFader.Instance == null) {
						BlackFader.Instance = this;
				} else {						
						throw new UnityException ("BlackFader already set, there should be just one!");			 
				}
		}

		void Update ()
		{
				if (FadeInProgress) {
						//the colour is lineary interpolated from the start to the end alpha value
						GetComponent<GUITexture>().color = Color.Lerp (new Color (0.0f, 0.0f, 0.0f, fadeStartAlpha), new Color (0.0f, 0.0f, 0.0f, DesiredAlpha), FadeProgess);
				}
		}
		/// <summary>
		/// Gets the passed time since fade start.
		/// </summary>
		/// <value>The passed time since fade start.</value>
		public float PassedTimeSinceFadeStart { get { return Time.time - fadeStart; } }
		/// <summary>
		/// Gets the fade progess. A value between 1 and 0 indicating the position between the start and end time stamp.
		/// </summary>
		/// <value>The fade progess.</value>
		public float FadeProgess {
				get {
						float fadeProgess = PassedTimeSinceFadeStart / fadeDuration;
						return Mathf.Clamp (fadeProgess, 0.0f, 1.0f);
				}
		}
		/// <summary>
		/// The fade in progress.
		/// </summary>
		private bool fadeInProgress = false;
		/// <summary>
		/// Gets or sets a value indicating whether this fade in progress.
		/// </summary>
		/// <value><c>true</c> if fade in progress; otherwise, <c>false</c>.</value>
		public bool FadeInProgress {
				get{ return fadeInProgress;}
				set {
						fadeInProgress = value;
						//if the fade progress was just completed, the OnFadedComplete Event is called
						if (!fadeInProgress) {
								OnFadedComplete (desiredAlpha);
						}

				}
		}
		/// <summary>
		/// The fade start alpha.
		/// </summary>
		private float fadeStartAlpha;
		/// <summary>
		/// The desired alpha.
		/// </summary>
		private float desiredAlpha;
		/// <summary>
		/// The fade start.
		/// </summary>
		private float fadeStart;
		/// <summary>
		/// The duration of the fade.
		/// </summary>
		public float fadeDuration = 3f;
		/// <summary>
		/// Gets or sets the desired alpha.
		/// </summary>
		/// <value>The desired alpha.</value>
		public float DesiredAlpha {
				get { 
						FadeInProgress = PassedTimeSinceFadeStart < fadeDuration;
						return desiredAlpha;
				}
				//when the desired alpha is set, the animation starts and fadeStart is set to "now"
				set {
						fadeStart = Time.time;
						fadeStartAlpha = GetComponent<GUITexture>().color.a;
						desiredAlpha = value;
						FadeInProgress = true;
				}
		}
		/// <summary>
		/// fades to black.
		/// </summary>
		public void FadeBlack ()
		{
				DesiredAlpha = BLACK;
		}
		/// <summary>
		/// fades to transparent
		/// </summary>
		public void FadeAway ()
		{		
				DesiredAlpha = TRANSPARENT;
		}
}
