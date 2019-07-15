using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class holdLeft : MonoBehaviour
{
    public float throwSpeed;

    private bool playerCloseToStone;
    private bool rightDown;
    private int stoneAmount;
    private int stoneIndex;
    private GameObject closestStone;


    List<GameObject> stonesHolding = new List<GameObject>();
    List<GameObject> stonesOnGround = new List<GameObject>();
    List<float> distances = new List<float>();


    void Start()
    {
        foreach (GameObject Stone in GameObject.FindGameObjectsWithTag("stone"))
        {
            stonesOnGround.Add(Stone);
        }
    }



    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            rightDown = true;
        }
        else
        {
            rightDown = false; 
        }

        if (rightDown == true && stonesHolding != null)
        {
            stoneThrow();
        }

        if (Vector3.Distance(transform.position, GameObject.Find("FPSController").GetComponent<Transform>().position) < 0.5)
        {
            playerCloseToStone = true;
        }
        else
        {
            playerCloseToStone = false;
        }

        if (Input.GetKeyUp("e") && playerCloseToStone == true)
        {
            stoneHold();

        }

    }

    public void stoneHold()
    {
        
        foreach (GameObject stone in stonesOnGround)
        {
            float stoneDistance = Vector3.Distance(transform.position, stone.transform.position);
            distances.Add(stoneDistance);
        }
        stoneIndex = ArrayUtility.IndexOf(distances.ToArray(), Mathf.Min(distances.ToArray()));
        closestStone = stonesOnGround[stoneIndex];

        closestStone.transform.SetParent(transform);
        closestStone.transform.position = transform.position;
        closestStone.transform.rotation = transform.rotation;
        closestStone.GetComponent<Rigidbody>().isKinematic = true;
        closestStone.GetComponent<Collider>().enabled = false;
        stonesHolding.Add(closestStone);
    }

    void stoneThrow()
    {
        GameObject.FindGameObjectWithTag("stone").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.FindGameObjectWithTag("stone").transform.parent = null;
        GameObject.FindGameObjectWithTag("stone").GetComponent<Rigidbody>().velocity = transform.forward * throwSpeed;
        GameObject.FindGameObjectWithTag("stone").GetComponent<Collider>().enabled = true;
        stoneAmount -= 1;
    }
}