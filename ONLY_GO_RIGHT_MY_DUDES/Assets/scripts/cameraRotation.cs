using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour {

	public Transform rotationPoint;
	public float rotationSpeed;

	void Update() {
		transform.LookAt (rotationPoint);
		transform.Translate(Vector3.right * Time.deltaTime * rotationSpeed);
	}

}
