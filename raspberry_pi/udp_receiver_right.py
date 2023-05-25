import socket

def start_udp_receiver(ip, port):
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # AF_INET means IPv4, SOCK_DGRAM means UDP
    sock.bind((ip, port))

    while True:
        data, addr = sock.recvfrom(1024) # Buffer size is 1024 bytes
        print(data.decode('utf-8'))

start_udp_receiver('192.168.247.210', 12345) # Replace with your IP and port
