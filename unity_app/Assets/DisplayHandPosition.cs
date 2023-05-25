using UnityEngine;
using OculusSampleFramework;
using TMPro;

public class DisplayHandPosition : MonoBehaviour
{
    public OVRHand hand;
    public OVRSkeleton skeleton;
    public TextMeshPro handPositionText;

    private Vector3 pinchStartPos;
    private bool pinchStarted = false;

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
            }
            else
            {
                pinchStarted = false;
                handPositionText.text += "\nThumb Pinch: Not Pinching";
            }
        }
        else
        {
            handPositionText.text = "Hand not tracked.";
        }
    }
}
