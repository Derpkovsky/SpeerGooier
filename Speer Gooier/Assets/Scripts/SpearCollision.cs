using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{
    //STATE TRACKING
    public bool playerCloseEnough = false;

    //CACHING
    private GameObject player;
    private ThrowSpear throwSpearScript;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        throwSpearScript = GameObject.Find("speerspawn").GetComponent<ThrowSpear>();
    }




    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 4)
        {
            playerCloseEnough = true;
        }
        else
        {
            playerCloseEnough = false;
        }
    }
    

    //Zorgt dat de speer stilstaat als hij een StickWall raakt
    private void OnCollisionEnter(Collision other)
    {
        if ( other.gameObject.tag == "target")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(other.gameObject.transform);
            //transform.rotation = player.GetComponent<ThrowSpear>().spearRotation;
        }

        if (other.gameObject.tag == "stickWall")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.rotation = throwSpearScript.spearRotation;
        }

        if (other.gameObject.tag == "Terrain")
        {

        }
    }
}

