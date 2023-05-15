using UnityEngine;
using OculusSampleFramework;
using TMPro;

public class DisplayHandData : MonoBehaviour
{
    public OVRHand hand;
    public TextMeshPro handDataText;

    void Update()
    {
        if (hand.IsTracked)
        {
            handDataText.text = "Tracking Confidence: " + hand.HandConfidence.ToString();
            handDataText.text += "\nThumb Pinch: " + hand.GetFingerIsPinching(OVRHand.HandFinger.Thumb).ToString();
            handDataText.text += "\nIndex Pinch: " + hand.GetFingerIsPinching(OVRHand.HandFinger.Index).ToString();
        }
        else
        {
            handDataText.text = "Hand not tracked.";
        }
    }
}
