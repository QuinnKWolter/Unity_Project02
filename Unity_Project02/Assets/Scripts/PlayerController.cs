using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	CharacterController player;

    public GameManager gameManager;
	public float moveSpeed;
	public float jumpSpeed;

	// Simple Mouse Look Script
	// https://answers.unity.com/questions/29741/mouse-look-script.html
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 5F;
	public float sensitivityY = 5F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		player = GetComponent<CharacterController>();
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		//if (player.isGrounded && Input.GetButton("Jump")){
			//player.Move(new Vector3(0,jumpSpeed,0) * Time.deltaTime);
		//}
		Vector3 frontBack = Input.GetAxis("Vertical")*transform.TransformDirection(Vector3.forward)*moveSpeed;
		Vector3 leftRight = Input.GetAxis("Horizontal")*transform.TransformDirection(Vector3.right)*moveSpeed;
		player.Move((frontBack + leftRight) * Time.deltaTime);
		player.SimpleMove(Physics.gravity);

		if (axes == RotationAxes.MouseXAndY)
		{
			// Read the mouse input axis
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			rotationX = ClampAngle (rotationX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = ClampAngle (rotationY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}
		return Mathf.Clamp (angle, min, max);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "WinZone")
        {
            gameManager.winGame();
        }
    }

    void OnTriggerExit(Collider col) {
		if (col.tag == "CameraInfluence") {
            gameManager.setDetectionSpeed(0.25f);
			col.gameObject.transform.parent.GetComponent<SecurityCam>().AlertCamera(false);
		}
	}

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "InfoInfluence")
        {
            col.gameObject.transform.parent.GetComponent<InfoNode>().Convert();
        }
    }

    public void setLookSpeed(float x, float y)
    {
        sensitivityX = x;
        sensitivityY = y;
    }
}
