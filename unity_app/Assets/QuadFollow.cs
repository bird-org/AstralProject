using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadFollow : MonoBehaviour
{
    public Transform Quad; // Quad that displays the drone's camera image
	public OVRCameraRig CameraRig; // Reference to the OVRCameraRig
    public RenderTexture droneCameraTexture; // assign in inspector

	// The distance from the face at which the drone should be positioned
	public float QuadDistanceFromFace = 1.0f;

    // Start is called before the first frame update
	void Awake() {
        Quad.GetComponent<Renderer>().material.mainTexture = droneCameraTexture;

	}

    // Update is called once per frame
	void FixedUpdate () {

		// Position the Quad in front of the face based on the head tracking
		Vector3 headPosition = CameraRig.centerEyeAnchor.position;
		Vector3 headForward = CameraRig.centerEyeAnchor.rotation * Vector3.forward;
		Quad.transform.position = headPosition + headForward * QuadDistanceFromFace;
		// Make sure the Quad is facing the user
		Quad.transform.rotation = Quaternion.LookRotation(headForward);

	}
}