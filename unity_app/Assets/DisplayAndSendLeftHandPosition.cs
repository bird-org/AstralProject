using UnityEngine;
using OculusSampleFramework;
using TMPro;

public class DisplayAndSendLeftHandPosition : MonoBehaviour
{
    public OVRHand hand;
    public OVRSkeleton skeleton;
    public TextMeshPro handPositionText;

    private Vector3 pinchStartPos;
    private bool pinchStarted = false;

    private UDPSender udpSender;
    public string handType = "left";

    void Start()
    {
        udpSender = new UDPSender("192.168.247.210", 12346);  // Use the IP and port of the machine you want to send data to
    }

    void Update()
    {
        if (hand.IsTracked)
        {
            handPositionText.text = "Tracking Confidence: " + hand.HandConfidence.ToString();

            // Check if thumb is pinching
            if (hand.GetFingerIsPinching(OVRHand.HandFinger.Thumb))
            {
                // If pinch just started, store the start position
                if (!pinchStarted)
                {
                    pinchStartPos = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
                    pinchStarted = true;
                }

                // Calculate the relative position and display it
                Vector3 currentPos = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position;
                Vector3 relativePos = currentPos - pinchStartPos;
                handPositionText.text += "\nThumb Pinch Relative Position: \n" + relativePos.ToString("F3");  // F3 to limit decimal places
                udpSender.Send(handType + ": " + relativePos.ToString("F3"));
            }
            else
            {
                pinchStarted = false;
                handPositionText.text += "\nThumb Pinch: Not Pinching";
                udpSender.Send(handType + ": not pinching");
            }
        }
        else
        {
            handPositionText.text = "Hand not tracked.";
        }
    }

    void OnDestroy()
    {
        udpSender.Close();
    }
}
