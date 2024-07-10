using UnityEngine;

public class SimpleRotateCarnival : MonoBehaviour
{
    public bool rotX;
    public float rotXSpeed = 50f;
    public bool rotY;
    public float rotYSpeed = 50f;
    public bool rotZ;
    public float rotZSpeed = 50f;

    // Update is called once per frame
    private void Update()
    {
        if (rotX) transform.Rotate(Vector3.left * Time.deltaTime * rotXSpeed);
        if (rotY) transform.Rotate(Vector3.up * Time.deltaTime * rotYSpeed);

        if (rotZ) transform.Rotate(Vector3.back * Time.deltaTime * rotZSpeed);
    }
}