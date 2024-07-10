using System.Collections.Generic;
using BlazeAIDemo;
using UnityEngine;

public class DeathDemoScript : MonoBehaviour
{
    public BlazeAI[] blazeAI;
    public List<Vector3> startPositions = new();

    private void Start()
    {
        foreach (var item in blazeAI) startPositions.Add(item.transform.position);
    }

    private void Update()
    {
        // hit the AI
        if (Input.GetKeyDown(KeyCode.E))
            for (var i = 0; i < blazeAI.Length; i++)
            {
                blazeAI[i].Hit();
                var blazeHealth = blazeAI[i].GetComponent<Health>();

                if (blazeHealth.currentHealth > 0) blazeHealth.currentHealth -= 10;

                if (blazeHealth.currentHealth <= 0)
                {
                    if (i < 2)
                        // plays either death animation or ragdolls instantly depending on inspector
                        blazeAI[i].Death();
                    else
                        // plays death animation and then ragdolls midway
                        blazeAI[i].DeathDoll(0.5f);
                }
            }


        // return alive
        if (Input.GetKeyDown(KeyCode.R))
            for (var i = 0; i < blazeAI.Length; i++)
            {
                var blazeHealth = blazeAI[i].GetComponent<Health>();
                blazeHealth.currentHealth = blazeHealth.maxHealth;
                blazeAI[i].ChangeState("normal");
                blazeAI[i].transform.position = startPositions[i];
            }
    }
}