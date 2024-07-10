using System.Collections;
using UnityEngine;

public class clownEntranceController : MonoBehaviour
{
    public Transform[] eyeballs; // List of objects 'eyes' to include
    public float rotationRange = 20.0f; // The range of rotation for eyes 


    public float smoothness = 5.0f;
    public float frequency = 3f;
    public float timeElapsed = 1.0f;

    private Coroutine entranceCoroutine;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        //disable script if no objects are in eyeball list
        if (eyeballs.Length == 0)
        {
            Debug.LogError("No objects Detected to Rotate, Disabling script");
            enabled = false;
            return;
        }

        foreach (var eyeball in eyeballs)
        {
            initialRotation = eyeball.localRotation;
            entranceCoroutine = StartCoroutine(RandomizeEyeRotation());
        }
    }


    //stops all coroutine's if disabled in scene as one is created for each eye
    private void OnDisable()
    {
        if (entranceCoroutine != null) StopAllCoroutines();
    }

    private IEnumerator RandomizeEyeRotation()
    {
        while (true)
        {
            var randomPitch = Random.Range(-rotationRange, rotationRange);
            var randomYaw = Random.Range(-rotationRange, rotationRange);

            targetRotation = initialRotation * Quaternion.Euler(randomPitch, randomYaw, 0);

            float elapsedTime = 0;

            while (elapsedTime < timeElapsed)
            {
                elapsedTime += Time.deltaTime * smoothness;
                //Apply rotation to each eye
                foreach (var eyeball in eyeballs)
                {
                    eyeball.localRotation = Quaternion.Slerp(eyeball.localRotation, targetRotation, elapsedTime);
                    yield return null;
                }
            }

            // Delay before next eye movement
            yield return new WaitForSeconds(Random.Range(frequency * 0.75f, frequency * 1.5f));
        }
    }
}