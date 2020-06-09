using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{

    [SerializeField] SO_City data;


    // Update is called once per frame
    void Update()
    {
        data.position = transform.position;
    }
}
