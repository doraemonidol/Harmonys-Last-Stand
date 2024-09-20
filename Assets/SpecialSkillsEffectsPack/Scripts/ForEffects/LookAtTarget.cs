using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform Target;

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(Target);
    }
}