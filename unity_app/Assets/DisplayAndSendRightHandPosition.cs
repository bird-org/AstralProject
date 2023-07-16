using UnityEngine;
using OculusSampleFramework;
using TMPro;

public class DisplayAndSendRightHandPosition : MonoBehaviour
{
    public OVRHand hand;
    public OVRSkeleton skeleton;
    public TextMeshPro handPositionText;
    public OVRCameraRig cameraRig;

    private Vector3 pinchStartPos;
    private bool pinchStarted = false;

    private UDPSender udpSender;
    public string handType = "right";

    public Vector3 relativePos = new Vector3(0, 0, 0);

    void Start()
    {
        udpSender = new UDPSender("192.168.247.210", 12345);  // Use the IP and port of the machine you want to send data to
    }

    void Update()
    {
        if (hand.IsTracked)
        {
            handPositionText.text = "Tracking Confidence: " + hand.HandConfidence.ToString();

            // Check if thumb is pinching
            if (hand.GetFingerIsPinching(OVRHand.HandFinger.Thumb))
            {
                // If pinch just started, store the start position in the head's local coordinate space
                if (!pinchStarted)
                {
                    pinchStartPos = cameraRig.centerEyeAnchor.InverseTransformPoint(
                        skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position);
                    pinchStarted = true;
                }

                // Calculate the relative position and display it
                Vector3 currentPos = cameraRig.centerEyeAnchor.InverseTransformPoint(
                    skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position);
                relativePos = currentPos - pinchStartPos;
                handPositionText.text += "\nThumb Pinch Relative Position: \n" + relativePos.ToString("F3");  // F3 to limit decimal places
                udpSender.Send(handType + ": " + relativePos.ToString("F3"));
            }
            else
            {
                pinchStarted = false;
                relativePos = new Vector3(0, 0, 0);
                handPositionText.text += "\nThumb Pinch: Not Pinching";
                udpSender.Send(handType + ": not pinching");
            }
        }
        else
        {
            relativePos = new Vector3(0, 0, 0);  // testing
            handPositionText.text = "Hand not tracked.";
        }
    }

    void OnDestroy()
    {
        udpSender.Close();
    }
}
