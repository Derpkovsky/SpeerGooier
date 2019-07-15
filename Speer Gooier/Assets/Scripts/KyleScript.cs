using UnityEngine;
using System.Collections;
using UnityEngine.Accessibility;

public class KyleScript : MonoBehaviour
{
    //TWEAKABLES
    public float moveSpeed;
    public float rotateSpeed;
    public float stunTime = 2.0f;


    //INTERNAL STATE TRACKERS
    private float oldMoveSpeed;
    private float oldStunTime;
    private float distanceSpearTarget;
    private bool spearClose;
    private bool spearHit;
    private bool dead = false;
    private bool vision = false;
    private bool stunTimer;

    //CACHING
    private GameObject player;
    private GameObject spear;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spear = GameObject.FindGameObjectWithTag("Spear");
    }




    void Update()
    {
        float step = rotateSpeed * Time.deltaTime;
        Vector3 targetDir = player.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        if (spear == null)
        {
            spear = GameObject.FindGameObjectWithTag("Spear");
        }
        distanceSpearTarget = Vector3.Distance(spear.transform.position + new Vector3(0, 0, 1f), transform.position);


        if (distanceSpearTarget <= 2)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }


        if (!dead && vision)
        {
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        }



        if (stunTimer == true)
        {
            oldStunTime = stunTime;
            stunTime -= Time.deltaTime;
            if (stunTime <= 0)
            {
                moveSpeed = oldMoveSpeed;
                stunTime = oldStunTime;
            }
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        // geeft aan of de target geraakt is
        if (other.gameObject.tag == "Spear")
        {
            dead = true;
        }

        if (other.gameObject.tag == "stone")
        {
            oldMoveSpeed = moveSpeed;
            moveSpeed = 0;
            stunTimer = true;
        }
    }
 

    // vision cone triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            vision = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            vision = false;
        }
    }
}
