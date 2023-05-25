using UnityEngine;
using TMPro;

public class DisplayHeadData : MonoBehaviour
{
    public TextMeshPro headDataText;
    public Camera mainCamera;
    private UDPSender udpSender;

    void Start()
    {
        udpSender = new UDPSender("192.168.247.210", 12347);  // Use the IP and port of the machine you want to send data to
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = mainCamera.transform.position;
        Quaternion rotation = mainCamera.transform.rotation;

        // Display position and rotation data
        headDataText.text = $"Head Position: {position.ToString("F3")} \nHead Rotation: {rotation.eulerAngles.ToString("F3")}";
        udpSender.Send("head: " + rotation.eulerAngles.ToString("F3"));
    }

    void OnDestroy()
    {
        udpSender.Close();
    }
}
