// namespace organizes resources and variables. import here rather than specify everywhere!
using UnityEngine; 
using System.Collections;
using System;

public class clockAnimator : MonoBehaviour {
	// clockAnimator inherits monobehaviour from unity engine (UnityEngine.MonoBehaviorur)
	// Use this for initialization. consider class a blueprint

	//set constants
	private const float
		hoursToDegrees = 360f / 12f,
		minutesToDegrees = 360f / 60f,
		secondsToDegrees = 360f / 60f;

	//add a transform variable for each arm -- then assign it in unity
	public Transform hours, minutes, seconds;
	public bool analog;

	// Update is called once per frame
	private void Update () {
		if (analog) {
			TimeSpan timespan = DateTime.Now.TimeOfDay;
			hours.localRotation = 
				Quaternion.Euler (0f, 0f, (float)timespan.TotalHours * -hoursToDegrees);
			minutes.localRotation =
				Quaternion.Euler (0f, 0f, (float)timespan.TotalMinutes * -minutesToDegrees);
			seconds.localRotation = 
				Quaternion.Euler (0f, 0f, (float)timespan.TotalSeconds * -secondsToDegrees);

		} else {
			//access system time library
			DateTime time = DateTime.Now;
			//set local rotation of the arms
			hours.localRotation = 
			Quaternion.Euler (0f, 0f, time.Hour * -hoursToDegrees);
			minutes.localRotation =
			Quaternion.Euler (0f, 0f, time.Minute * -minutesToDegrees);
			seconds.localRotation = 
			Quaternion.Euler (0f, 0f, time.Second * -secondsToDegrees);
		}
	}
}
