using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DMXLight : MonoBehaviour
{
  // Light Channel class. Used to be able to give each channel a name and value.
	[System.Serializable]
	private class LightChannel
	{
		public string name = "Light Element";

		[Range(0, 255)]
		public int value = 0;
	}

  // First DMX channel of the DMXlight
	[SerializeField]
  private int firstChannelNumber = 1;

  // Number of channels on DMXlight
  [SerializeField]
  private LightChannel[] numberOfChannels = new LightChannel[5];
	private int channelNumber;  // Efficiency variable for FixedUpdate method

	private LightGrid lightGrid;  /* LightGrid instance. Set in start function to parent 
	                               object lightgrid component. */

  void Start()
  {
    // Set the lightGrid object and initialize the channel number
    lightGrid = gameObject.transform.parent.gameObject.GetComponent<LightGrid>();
    channelNumber = 0;
	}

  void FixedUpdate()
  {
    // Each frame, send the current channel values to the lightgrid.
    for (int i = 0; i < numberOfChannels.Length; i++)
    {
      channelNumber = i + firstChannelNumber;
      Debug.Log("Setting channel " + channelNumber);
      if (!lightGrid.setChannel(channelNumber, numberOfChannels[i].value))
        Debug.Log("CHANNEL SETTING FAILED ON CHANNEL NUMBER " + channelNumber);
    }
  }

  // Not to be confused with 'bool LightGrid.setChannel(int, int)'
  /* Commented out for now. Uncomment when we start controlling lights from somewhere 
   Other than the Unity inspector (eg when we control lights with raycasting or something. */
  // void setChannel(int channel, int value) => numberOfChannels[channel].value = value;
}
