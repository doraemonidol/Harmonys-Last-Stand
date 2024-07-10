using UnityEngine;

public class ClownGameController : MonoBehaviour
{
    public GameObject clownHead;
    public float rotationAmount = 40f;

    [Range(0, 5)] public float speed = 0.5f;

    private float timeCounter;

    private void Update()
    {
        var rotationSpeed = speed / rotationAmount;
        timeCounter += rotationAmount * Time.deltaTime * rotationSpeed;
        var rotationOffset = Mathf.Sin(timeCounter) * rotationAmount;
        clownHead.transform.localRotation = Quaternion.Euler(0, rotationOffset, 0);
    }
}