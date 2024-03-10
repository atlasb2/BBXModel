using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap_Color : MonoBehaviour
{
    public Material mat;
    public Light lt;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        lt = gameObject.transform.parent.gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetColor("_EmissionColor", lt.color);
    }
}
