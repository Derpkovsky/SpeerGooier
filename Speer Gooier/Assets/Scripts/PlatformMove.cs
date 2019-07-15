using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    //TWEAKABLES
    public float maxRight;
    public float maxLeft;
    public float moveSpeed;

    //STATE TRACKERS
    private float oldPlatformPos;
    private float oldMoveSpeed;





    void Start()
    {
        oldPlatformPos = transform.position.x;
        oldMoveSpeed = moveSpeed;
    }

    void Update()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
        if (transform.position.x - oldPlatformPos > maxRight)
        {
            moveSpeed = moveSpeed * -1;
        }
        if (transform.position.x - oldPlatformPos < -maxLeft)
        {
            moveSpeed = moveSpeed * -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spear")
        {
            moveSpeed = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Spear")
        {
            moveSpeed = oldMoveSpeed;
        }
    }
}
