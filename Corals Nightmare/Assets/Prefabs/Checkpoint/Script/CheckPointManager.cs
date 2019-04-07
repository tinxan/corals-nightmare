using UnityEngine;
using System.Collections;

public class CheckPointManager : MonoBehaviour
{
		/// <summary>
		/// Gets the Singleton instance.
		/// </summary>
		/// <value>The instance.</value>
		public static CheckPointManager Instance { get; private set; }
		/// <summary>
		/// the checkpoints
		/// </summary>
		public CheckPoint[] CheckPoints;
		
		void Start ()
		{
				if (CheckPointManager.Instance == null) {
						CheckPointManager.Instance = this;
				} else {						
						throw new UnityException ("CheckPointManager already set, there should be just one!");			 
				}
		}
		/// <summary>
		/// Resets all checkpoints.
		/// </summary>
		public void ResetAllCheckPoints ()
		{
				foreach (CheckPoint checkPoint in CheckPoints) {
						checkPoint.Reset ();
				}
		}
}
