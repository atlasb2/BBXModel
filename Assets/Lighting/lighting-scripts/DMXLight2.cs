using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMXLight2 : MonoBehaviour
{
  [SerializeField]
  private int firstChannelNumber = 1;

  //[SerializeField]
  //[Range(0, 255)]
  private int[] numberOfChannels = new int[5];

  LightGrid lightGrid;
  public Light lt;
  // Start is called before the first frame update
  void Start()
  {
    lightGrid = gameObject.transform.parent.gameObject.GetComponent<LightGrid>();
    lt = GetComponent<Light>();
  	//lightGrid.setChannel(3, 200);
	}

  // Update is called once per frame
  void FixedUpdate()
  {
    numberOfChannels[0] = (int)lt.intensity;
    numberOfChannels[1] = (int)(lt.color.r*255.0);
    numberOfChannels[2] = (int)(lt.color.g*255.0);
    numberOfChannels[3] = (int)(lt.color.b*255.0);
    numberOfChannels[4] = 0;
    //Debug.Log("colors: " + numberOfChannels[1] + " " + numberOfChannels[2] + " " + numberOfChannels[3]);
    for (int i = 0; i < numberOfChannels.Length; i++)
      lightGrid.setChannel(i + firstChannelNumber, numberOfChannels[i]);
  }
}
