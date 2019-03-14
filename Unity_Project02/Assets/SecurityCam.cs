using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour {

	public GameObject player, neck, head, eye, influence;
	public GameObject[] sightTargets;
	public AudioSource alert, alarm;
	public float rotationSpeed, maxRotation;

	private bool alerted = false;
	private Quaternion headStartRotation;

	// Use this for initialization
	void Start () {
		headStartRotation = head.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// When player exits a trigger, if trigger object is camera, set alerted to false for that camera

	void FixedUpdate() {
		RaycastHit hit;
		if (!alerted) {
			alarm.Stop();
			neck.transform.rotation = Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * rotationSpeed), 0f);
			for (int i=0; i<4; i++) {
				// Unity Documentation
				if (Physics.Raycast(eye.transform.position, sightTargets[i].transform.TransformDirection(Vector3.forward), out hit, 5) && hit.transform.tag == "Player")
				{
					AlertCamera(true);
					Debug.Log("Did Hit");
					alert.Play();
					alarm.Play();
				}
				else
				{
					// Debug.Log("Did not Hit");
				}
			}
		} else {
			head.transform.LookAt(player.transform.position);
		}
	}

	public void AlertCamera(bool isAlerted) {
		alerted = isAlerted;
	}
}
