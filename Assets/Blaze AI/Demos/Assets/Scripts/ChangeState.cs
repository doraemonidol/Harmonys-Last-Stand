using UnityEngine;

public class ChangeState : MonoBehaviour
{
    private BlazeAI blaze;

    // Start is called before the first frame update
    private void Start()
    {
        blaze = GetComponent<BlazeAI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) blaze.ChangeState("normal");

        if (Input.GetKeyDown(KeyCode.R)) blaze.ChangeState("alert");
    }
}