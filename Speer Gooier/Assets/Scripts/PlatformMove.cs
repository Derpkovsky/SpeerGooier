using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Transform platform;
    public float maxRight;
    public float maxLeft;
    public float moveSpeed;
    private float oldPlatformPos;
    private float oldMoveSpeed;

    void Start()
    {
        oldPlatformPos = platform.position.x;
        oldMoveSpeed = moveSpeed;
    }

    void Update()
    {
        platform.position += transform.right * moveSpeed * Time.deltaTime;
        if (platform.position.x - oldPlatformPos > maxRight)
        {
            moveSpeed = moveSpeed * -1;
        }
        if (platform.position.x - oldPlatformPos < -maxLeft)
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
