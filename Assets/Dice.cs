using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    [SerializeField] SO_Dice data;
    Material mat;

    void Awake()
    {
        mat = GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void Update()
    {


        switch (data.value)
        {
            case RESOURCE.DRUG:
                mat.SetColor("_baseColor", Color.red);
                break;
            case RESOURCE.VACCINE:
                mat.SetColor("_baseColor", Color.white);
                break;
            case RESOURCE.WATER:
                mat.SetColor("_baseColor", Color.blue);
                break;
            case RESOURCE.FOOD:
                mat.SetColor("_baseColor", Color.green);
                break;
            case RESOURCE.PLANE:
                mat.SetColor("_baseColor", Color.black);
                break;
            case RESOURCE.ENERGY:
                mat.SetColor("_baseColor", Color.yellow);
                break;
        }
    }
}
