using UnityEngine;

public class CarouselHorseController : MonoBehaviour
{
    public float maxDistance = 0.4f;
    public float speed = 0.3f;
    public bool reverseMotion; // Reverse the motion
    private bool movingToEnd = true;
    private Vector3 originalLocalPosition; // Original local position

    private float startTime;

    private void Start()
    {
        originalLocalPosition = transform.localPosition;
        startTime = Time.time;
    }

    private void Update()
    {
        var distanceCovered = (Time.time - startTime) * speed;
        var fractionOfJourney = distanceCovered / maxDistance;
        fractionOfJourney = Mathf.Clamp01(fractionOfJourney);

        if (reverseMotion)
        {
            movingToEnd = !movingToEnd;
            reverseMotion = false;
        }

        //get Original position to know where to return
        var startLocalPosition = originalLocalPosition;

        //get Target position to move to. Change Vector3.up to change direction of where Target is. 
        var endLocalPosition = originalLocalPosition + Vector3.up * maxDistance;

        // Used to track local position of objects if parent shapes are moving in scene
        if (transform.parent != null)
        {
            var parentMatrix = transform.parent.localToWorldMatrix;

            startLocalPosition = parentMatrix.MultiplyPoint3x4(startLocalPosition);
            endLocalPosition = parentMatrix.MultiplyPoint3x4(endLocalPosition);
        }

        // Move objects to target destination
        if (movingToEnd)
        {
            var easedFraction = SmoothStep(fractionOfJourney);
            transform.position = Vector3.Lerp(startLocalPosition, endLocalPosition, easedFraction);
        }
        else
        {
            var easedFraction = SmoothStep(fractionOfJourney);
            transform.position = Vector3.Lerp(endLocalPosition, startLocalPosition, easedFraction);
        }

        if (fractionOfJourney >= 1.0f)
        {
            movingToEnd = !movingToEnd;
            startTime = Time.time;
        }
    }


    // Used to smooth out the motion as it reaches its destinations
    private float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);
    }
}