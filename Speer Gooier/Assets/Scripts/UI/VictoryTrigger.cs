using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public bool Victorybool;
    private float VictoryTimer;

    void OnTriggerEnter(Collider other)
        
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Victory!");
            Victorybool = true;
        }
        if (Victorybool == true)
        {
            VictoryTimer -= Time.deltaTime;
            if (VictoryTimer <= 0)
            {
                Victorybool = false;
            }
        }
    }
}
