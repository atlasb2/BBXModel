using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;


public class LightGrid : MonoBehaviour
{
	// IP Address of the ArtNet system
  [SerializeField]
  private string destinationIP = "127.0.0.1";

	// Port
	[SerializeField]
	private int destinationPort = 6454;

	// Universe
  [SerializeField]
  private byte universe = 0X00;

	// Output frequency
	[SerializeField]
	private float outputHz = 44f;

	// Socket variables
	private UdpClient socket;
	private IPEndPoint target;

	// Allocate memory
	private byte[] data = new byte[512];
	private byte[] previousData = new byte[512];
	private byte[] artNetPacket = new byte[530];

	// Time since last transmission to artnet
	private float intervalTime;

	// Other grids
	public GameObject soundGrid;
	public GameObject projectorGrid;
	public GameObject motionCaptureGrid;

	public void Start()
	{
		// Set up the socket
		target = new IPEndPoint(IPAddress.Parse(destinationIP), destinationPort);
		socket = new UdpClient(); // From System.Net.Sockets
		socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
		socket.Connect(target);

		/* Build the Artnet packet */
		// Field 1
		string str = "Art-Net";
		System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
		encoding.GetBytes(str, 0, str.Length, artNetPacket, 0);
		artNetPacket[7] = 0x0;

		// Field 2: (int16) opcode low byte first
		artNetPacket[8] = 0x00;
		artNetPacket[9] = 0x50;

		// Field 3: (int8) high byte of Art-Net protocol revision number
		artNetPacket[10] = 0x0;

		// Field 4: (int8) low byte of Art-Net protocol revision number
		artNetPacket[11] = 0x14;

		//TODO: Full Addressing

		// Field 5: (int8) sequence 
		artNetPacket[12] = 0x0;

		// Field 6: (int8) physical port
		artNetPacket[13] = 0x0;

		// Field 7 - 8: (int8, int8): universe low byte first
		artNetPacket[14] = universe;
		artNetPacket[15] = 0x0;

		// Field 9 - 10: (int8, int8): length of dmx array high byte first
		artNetPacket[16] = ((512 >> 8) & 0xFF);
		artNetPacket[17] = (512 & 0xFF);
		/* End artnet packet building */

		// Initialize
		intervalTime = 0f;
	}

	private void transmit()
	{
		// Only transmit a packet if something has changed
		if (previousData.SequenceEqual(data))
			return;

		// Copy channel data to the ArtNetPacket. Set record historical data.
		Buffer.BlockCopy(data, 0, artNetPacket, 18, 512);
		Buffer.BlockCopy(data, 0, previousData, 0, 512);
		
		// Send data across the socket. Report any errors
		try
		{
			socket.Send(artNetPacket, artNetPacket.Length);
		}
		catch (Exception e)
		{
			Debug.Log(this + " " + e);
		}
	}

	public void FixedUpdate()
	{
		// Record time elapsed
		intervalTime = intervalTime + Time.fixedDeltaTime;

		// Transmit and reset intervalTime 
		if (intervalTime >= 1f / outputHz)
		{
			intervalTime = 0f;
			transmit();
		}
	}

	// Not to be confused with 'void Light.setChannel(int, int)'
	public bool setChannel(int channel, int value)
	{
		// Lighting channels go from 1 to 512
		if ((channel < 1) || (channel > 512)) return false;

		// Lighting values go from 0 to 255
		if ((value < 0) || value > 255) return false;

		// Set the data
		data[channel - 1] = Convert.ToByte(value);
		return true;
	}
}
