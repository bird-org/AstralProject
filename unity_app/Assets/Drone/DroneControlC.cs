using UnityEngine;
using System.Collections;

public class DroneControlC : MonoBehaviour {
	public Rigidbody Drone;
	// private Vector3 DroneRotation;
	private Quaternion LastDroneRotation;

	DisplayAndSendLeftHandPosition DisplayAndSendLeftHandPosition;
	DisplayAndSendRightHandPosition DisplayAndSendRightHandPosition;
	DisplayHeadData DisplayHeadData;
	[SerializeField] GameObject LeftHandData;
	[SerializeField] GameObject RightHandData;
	[SerializeField] GameObject HeadData;

	void Awake() {
		DisplayAndSendLeftHandPosition = LeftHandData.GetComponent<DisplayAndSendLeftHandPosition>();
		DisplayAndSendRightHandPosition = RightHandData.GetComponent<DisplayAndSendRightHandPosition>();
		DisplayHeadData = HeadData.GetComponent<DisplayHeadData>();
	}

	void FixedUpdate () {

		// right hand velocity in drone coords
		Vector3 localVelocity = Drone.transform.TransformDirection(DisplayAndSendRightHandPosition.relativePos);
		Drone.velocity = localVelocity;

		// head rotation
		Drone.rotation = Quaternion.Euler(Drone.rotation.eulerAngles.x, (Drone.rotation.eulerAngles + DisplayHeadData.rotation.eulerAngles - LastDroneRotation.eulerAngles).y, Drone.rotation.eulerAngles.z);
		LastDroneRotation = DisplayHeadData.rotation;

		// left hand angular velocity
		Drone.angularVelocity = new Vector3(0, DisplayAndSendLeftHandPosition.relativePos.y, 0);

		// gravity
		Drone.AddForce(0,9.80665f,0);

		// Debug.Log(DisplayAndSendLeftHandPosition.relativePos[0].ToString("F3"));
		// Debug.Log(DisplayAndSendRightHandPosition.relativePos.ToString("F3"));
		// Debug.Log(DisplayHeadData.rotation.eulerAngles.ToString("F3"));
		// DroneRotation=Drone.transform.localEulerAngles;

		// if(DroneRotation.z>10 && DroneRotation.z<=180){Drone.AddRelativeTorque (0, 0, -1);}//if tilt too big(stabilizes drone on z-axis)
		// if(DroneRotation.z>180 && DroneRotation.z<=350){Drone.AddRelativeTorque (0, 0, 1);}//if tilt too big(stabilizes drone on z-axis)
		// if(DroneRotation.z>1 && DroneRotation.z<=10){Drone.AddRelativeTorque (0, 0, -0.3f);}//if tilt not very big(stabilizes drone on z-axis)
		// if(DroneRotation.z>350 && DroneRotation.z<359){Drone.AddRelativeTorque (0, 0, 0.3f);}//if tilt not very big(stabilizes drone on z-axis)

		// if(Input.GetKey(KeyCode.A)) {Drone.angularVelocity += new Vector3(0,-1,0);}//tilt drone left
		// if(Input.GetKey(KeyCode.D)){Drone.angularVelocity += new Vector3(0,1,0);}//tilt drone right

		// if(DroneRotation.x>10 && DroneRotation.x<=180){Drone.AddRelativeTorque (-1, 0, 0);}//if tilt too big(stabilizes drone on x-axis)
		// if(DroneRotation.x>180 && DroneRotation.x<=350){Drone.AddRelativeTorque (1, 0, 0);}//if tilt too big(stabilizes drone on x-axis)
		// if(DroneRotation.x>1 && DroneRotation.x<=10){Drone.AddRelativeTorque (-0.3f, 0, 0);}//if tilt not very big(stabilizes drone on x-axis)
		// if(DroneRotation.x>350 && DroneRotation.x<359){Drone.AddRelativeTorque (0.3f, 0, 0);}//if tilt not very big(stabilizes drone on x-axis)

		// if(Input.GetKey(KeyCode.W)){Drone.velocity += new Vector3(0, 0, 1);}//drone fly forward

		// if(Input.GetKey(KeyCode.LeftArrow)){Drone.velocity += new Vector3(-1, 0, 0);}//rotate drone left

		// if(Input.GetKey(KeyCode.RightArrow)){Drone.velocity += new Vector3(1, 0, 0);}//rotate drone right

		// if(Input.GetKey(KeyCode.S)){Drone.velocity += new Vector3(0, 0, -1);}// drone fly backward

		// if(Input.GetKey(KeyCode.UpArrow)){Drone.velocity += new Vector3(0, 1, 0);}//drone fly up

		// if(Input.GetKey(KeyCode.DownArrow)){Drone.velocity += new Vector3(0, -1, 0);}//drone fly down

	}

}