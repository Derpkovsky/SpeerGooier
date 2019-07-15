using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartText : MonoBehaviour
{
    public float time = 5f; //Seconds to read the text

    void Start()
    {

        Destroy(gameObject, time);
    }
}
