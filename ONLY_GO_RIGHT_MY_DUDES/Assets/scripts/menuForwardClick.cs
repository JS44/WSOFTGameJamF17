using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuForwardClick : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip audioClip;

	public void playClip(){
		audioSource.clip = audioClip;
		audioSource.Play();
	}
}
