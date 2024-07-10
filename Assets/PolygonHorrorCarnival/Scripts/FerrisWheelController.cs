using UnityEngine;

public class FerrisWheelController : MonoBehaviour
{
    [Header("Ride Controls")] public float rotationSpeed = 15.0f;

    public float rockingSpeed = .2f;
    public float rockingAmplitude = 18f;

    public Transform[] chairs;
    public Transform[] wheelsForward;
    public Transform[] wheelsReverse;

    private float timeCounter;

    private void Update()
    {
        // Rotate the Ferris wheel
        transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);


        //rotate top wheels forwards with rotation speed multiplier
        foreach (var wheelFwd in wheelsForward) wheelFwd.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * 10);

        ///rotate bottom wheels backwards with rotation speed multiplier
        foreach (var wheelRev in wheelsReverse) wheelRev.Rotate(Vector3.back * rotationSpeed * Time.deltaTime * 10);

        //chair rocking motion
        foreach (var chair in chairs)
        {
            timeCounter += rockingSpeed * Time.deltaTime;
            // rotation speed added to reduce/add more motion depending on ferris wheel speed
            var rockingOffset = Mathf.Sin(timeCounter) * rockingAmplitude * (rotationSpeed / 10);
            chair.localRotation = Quaternion.Euler(0, 0, rockingOffset);
        }
    }
}