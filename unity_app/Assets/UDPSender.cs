using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSender
{
    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    public UDPSender(string remoteIP, int remotePort)
    {
        udpClient = new UdpClient();
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
    }

    public void Send(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        udpClient.Send(data, data.Length, remoteEndPoint);
    }

    public void Close()
    {
        udpClient.Close();
    }
}
