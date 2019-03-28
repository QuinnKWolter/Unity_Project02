using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour {

	public GameObject neck, head, eye, influence, gameManager;
	public GameObject[] sightTargets;
	public AudioSource alert, alarm;
	public float rotationSpeed, maxRotation;

	private GameObject player;
	private bool alerted = false;
	private Quaternion neckStartRotation, headStartRotation;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
        gameManager = GameObject.Find("GameManager");
        neckStartRotation = neck.transform.rotation;
		headStartRotation = head.transform.rotation;
	}

	void FixedUpdate() {
		if (!alerted) {
            RaycastHit hit;
			neck.transform.rotation = neckStartRotation * Quaternion.Euler(0f, maxRotation * Mathf.Sin(Time.time * rotationSpeed) + 45f, 0f);
			for (int i=0; i<11; i++) {
				// Unity Documentation
				if (Physics.Raycast(eye.transform.position, (transform.position - sightTargets[i].transform.position).normalized*-10, out hit, 50) && hit.collider.tag == "Player")
				{
					AlertCamera(true);
					// Debug.Log("Did Hit");
					alert.Play();
					alarm.Play();
				}
				else
				{
					Debug.DrawRay(eye.transform.position, (transform.position - sightTargets[i].transform.position).normalized*-10, Color.yellow, 20);
					// Debug.Log("Did not Hit");
				}
			}
		} else {
			head.transform.LookAt(player.transform.position);
		}
	}

	public void AlertCamera(bool isAlerted) {
		alerted = isAlerted;
		if (isAlerted == false) {
            gameManager.GetComponent<GameManager>().setDetectionSpeed(1f);
            neck.transform.rotation = neckStartRotation;
			head.transform.rotation = headStartRotation;
			alarm.Stop();
		}
	}
}
