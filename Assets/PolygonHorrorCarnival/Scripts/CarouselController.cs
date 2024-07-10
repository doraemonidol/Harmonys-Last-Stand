using UnityEngine;

public class CarouselController : MonoBehaviour
{
    [Header("Ride Speed")] [Range(-30, 30)]
    public float rideSpeed = -12.0f;

    [Header("Base Platform")] public GameObject Platform;

    [Header("Cranks")] public Transform[] Cranks;


    private void Update()
    {
        //rotate main platform ride
        Platform.transform.Rotate(Vector3.up * rideSpeed * Time.deltaTime);

        //rotate cranks based on ride speed
        foreach (var crank in Cranks) crank.Rotate(Vector3.forward * (rideSpeed * 1.25f) * Time.deltaTime * 10);
    }
}