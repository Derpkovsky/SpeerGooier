using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    private Text VictoryText;
    private bool victory;

    void Start()
    {
        victory = GameObject.FindGameObjectWithTag("Finish").GetComponent<VictoryTrigger>().Victorybool;
        VictoryText = GameObject.Find("VictoryText").GetComponent<Text>();
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
    }













}
