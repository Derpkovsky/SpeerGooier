using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    public Text VictoryText;
    private bool victory;

    void Start()
    {
        victory = GameObject.Find("Exit").GetComponent<VictoryTrigger>().Victorybool;
    }

    void Update()
    {
        if (victory)
        {
            VictoryText.text = "Thank you for delivering the package!" +
                               "You've helped us so much";
            
        }
        else
        {
            VictoryText.text = "";
        }

        if (GameObject.FindGameObjectWithTag("Spear").GetComponent<SpearCollision>().vincentHit == true)
        {
            VictoryText.text = "auw!";
        }
        else
        {
            VictoryText.text = "";
        }
    }
}
