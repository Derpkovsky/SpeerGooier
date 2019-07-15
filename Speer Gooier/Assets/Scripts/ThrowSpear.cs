using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ThrowSpear : MonoBehaviour
{
    //PUBLIC TWEAKABLES
    public float throwSpeed;
    public float recallSpeed;
    public float jumpThreshold;
    public float boostJump;

    //INTERNAL STATE TRACKERS
    private int spearAmount;
    private bool leftDown;
    private bool Rdown;
    private float jumpTimer;
    private float oldJump;
    [HideInInspector]
    public Quaternion spearRotation;

    //CACHING
    private GameObject player;
    private GameObject playerCamera;
    public GameObject spearInstantiator;
    private GameObject spear;
    private Transform pivot;
    private FirstPersonController controller;


    void Start()
    {
        GameObject spearObject = (GameObject)Instantiate(spearInstantiator, transform.position, transform.rotation);
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        spear = GameObject.FindGameObjectWithTag("Spear");
        controller = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        pivot = GameObject.FindGameObjectWithTag("Spear").transform.GetChild(0).transform;
        oldJump = controller.m_JumpSpeed;
        SpearHold();
    }






    void Update()
    {
        // ACTION TRIGGERS
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {leftDown = true;}
        else
        {leftDown = false;}
        if (leftDown == true && spearAmount > 0)
        {
            SpearThrow();
        }
        if (Input.GetKey("r"))  
        {
            RecallSpear();
        }
        else
        {
            spear.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        if (Input.GetKeyUp("q") && spear.GetComponent<SpearCollision>().playerCloseEnough == true)
        {
            SpearHold();
        }


        //BOOSTJUMP
        if (
                Input.GetKey(KeyCode.M) &&
                spear.GetComponent<SpearCollision>().playerCloseEnough &&
                spear.transform.parent == null &&
                spear.GetComponent<Rigidbody>().isKinematic
            )
        {
            jumpTimer += 1 * Time.deltaTime;
            spear.transform.RotateAround(pivot.position, -pivot.right, jumpTimer /5);
            if ( jumpTimer >= jumpThreshold)
            {
                jumpTimer = 0;
                spear.transform.rotation = spearRotation;
                controller.m_OldJumpSpeed = boostJump;
                controller.m_Jump = true;
            }
        }
        if (controller.m_Jumping)
        {
            controller.m_OldJumpSpeed = oldJump;
        }
    }




    // ROEPT SPEER TERUG
    //(unparent, position lerp, rotate lerp, freeposition + rotation, bij player: hold)
    void RecallSpear()
    {
        float recallSpeedCounter = 1 / recallSpeed;
        while (recallSpeedCounter < 1)
        {
            recallSpeedCounter += Time.deltaTime * recallSpeedCounter;
            spear.transform.parent = null;
            spear.transform.position = Vector3.Lerp(spear.transform.position, transform.position, recallSpeed * Time.deltaTime);
            spear.GetComponent<Rigidbody>().isKinematic = false;
            spear.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            spear.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            spear.transform.rotation = Quaternion.Lerp(spear.transform.rotation, transform.rotation, recallSpeed * Time.deltaTime);
            if (Vector3.Distance(spear.transform.position, transform.position) < 0.7)
            {
                SpearHold();
            }
        }
    }

    // GOOIT DE SPEER
    // (kinematic uit, unparent, geef velocity, zet collider aan, -1 speer, jumptimer=0)
    void SpearThrow()
    {
        spear.GetComponent<Rigidbody>().isKinematic = false;
        spear.transform.parent = null;
        spear.GetComponent<Rigidbody>().velocity = transform.forward * throwSpeed;
        spear.GetComponent<Collider>().enabled = true;
        spearAmount -= 1;
        spearRotation = transform.rotation;
    }

    // HOUDT DE SPEER VAST
    // (setParent, zet transform en rotatie gelijk, kinematic aan, collider uit, +1 speer, freezeposition uit)
    public void SpearHold()
    {
        jumpTimer = 0;
        spear.GetComponent<Transform>().SetParent(transform);
        spear.transform.position = transform.position;
        spear.transform.rotation = transform.rotation;
        spear.GetComponent<Rigidbody>().isKinematic = true;
        spear.GetComponent<Collider>().enabled = false;
        spearAmount += 1;
        if (spear.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezePosition  || spear.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeRotation)
        {
            spear.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}