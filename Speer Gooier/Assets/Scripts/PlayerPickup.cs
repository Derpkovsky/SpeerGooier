using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    //TWEAKABLES
    public float throwSpeed = 5f;
    public float horizontalOffset = 0.15f;
    public float verticalOffset = 0.17f;

    //TRACKERS
    public List<GameObject> Stones;
    public List<GameObject> stonesClose;
    private GameObject closestStone;
    private GameObject loadedStone;
    private bool stoneClose = false;
    private int stonesHolding;

    //CACHING
    private GameObject player;
    public GameObject leftHand;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //leftHand = GameObject.Find("lefthand");
    }




    private void Update()
    {
        if (stoneClose && Input.GetKeyDown(KeyCode.E))
        {
            closestStone = stonesClose[0];
            SetStone();
        }

        if (Stones != null && Input.GetMouseButtonDown(1))
        {
            if (Stones != null)
            {
                loadedStone = Stones[0];
            }
            else
            {
                loadedStone = new GameObject("Empty");
            }

            ThrowStone();
        }


    }

    private void SetStone()
    {
        Debug.Log("pickup");
        closestStone.transform.SetParent(leftHand.transform);
        closestStone.transform.position = leftHand.transform.position + new Vector3(Random.Range(-horizontalOffset, horizontalOffset), Random.Range(-verticalOffset, verticalOffset), 0);
        closestStone.transform.rotation = Random.rotation;
        closestStone.GetComponent<Rigidbody>().isKinematic = true;
        closestStone.transform.GetChild(0).gameObject.SetActive(false);
        Stones.Add(closestStone);
        stonesClose.Remove(closestStone);
    }

    private void ThrowStone()
    {
        loadedStone.transform.SetParent(null);
        closestStone.transform.GetChild(0).gameObject.SetActive(true);
        loadedStone.GetComponent<Rigidbody>().isKinematic = false;
        loadedStone.GetComponent<Rigidbody>().velocity = leftHand.transform.forward * throwSpeed;
        Stones.Remove(loadedStone);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "stone")
        {
            stoneClose = true;
            stonesClose.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "stone")
        {
            stoneClose = false;
            stonesClose.Remove(other.gameObject);
        }
    }
}