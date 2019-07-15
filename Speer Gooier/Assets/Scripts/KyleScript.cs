using UnityEngine;
using System.Collections;
using UnityEngine.Accessibility;

public class KyleScript : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float stunTime = 2.0f;


    private float oldMoveSpeed;
    private float oldStunTime;
    private float spearSpeed;
    private float distanceSpearTarget;

    public bool spearClose;
    private bool spearHit;
    private bool dead = false;
    private bool vision = false;
    private bool stunTimer;



    void Update()
    {
        Vector3 targetDir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        distanceSpearTarget =
            Vector3.Distance(GameObject.FindGameObjectWithTag("Spear").transform.position + new Vector3(0, 0, 1f),
                transform.position);
            

        if (distanceSpearTarget <= 2)
        {
            spearClose = true;
        }
        else
        {
            spearClose = false;
        }


        if (spearHit == true)
        {
            dead = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            if (spearClose == true)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
            
            if (vision == true && dead != true)
            {
                transform.rotation = Quaternion.LookRotation(newDir);
                transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position, Time.deltaTime * moveSpeed);
            }
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
            spearHit = true;
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
