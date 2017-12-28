using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverMotion : MonoBehaviour {
	public float amplitude = 0.1f;
	float speed;
	float delay;
	//float timePassed = 0.0f;
	float tempY;
	Vector3 tempPos;

	// Use this for initialization
	void Start () {
		tempPos = transform.position;
		tempY = transform.position.y;
		amplitude = 0.06f;
		speed = Random.Range (1.0f, 1.5f);
	}

	// Update is called once per frame
	void Update () {
		tempPos.y = tempY + amplitude * Mathf.Sin (speed * Time.time);
		transform.position = tempPos;
	}
}
