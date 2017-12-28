using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnToDeskTop : MonoBehaviour {

	// Use this for initialization
	public void QuitGame() {
		#if UNITY_EDITOR 
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
