using BlazeAIDemo;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Health blazeHealth;


    // Update is called once per frame
    private void Update()
    {
        healthText.text = "Health: " + blazeHealth.currentHealth;
    }
}