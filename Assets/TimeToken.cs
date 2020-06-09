using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TIME_TOKEN_TYPE
{
    USABLE,
    STOCK,
    BOX
}

public class TimeToken : MonoBehaviour
{
    public TIME_TOKEN_TYPE type;

}
