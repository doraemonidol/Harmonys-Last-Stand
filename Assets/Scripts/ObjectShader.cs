using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShader : MonoBehaviour
{
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject powerObject;
    // [SerializeField]
    // private GameObject image;

    private Quaternion initialRotation;

    void Start()
    {
        // Set initial active states
        title.SetActive(true);
        powerObject.SetActive(true);
        // image.SetActive(false);

        // Store the initial rotation of the image
        initialRotation = title.transform.rotation;
    }

    void Update()
    {
        // Check if the image has been rotated
        if (title.transform.rotation != initialRotation)
        {
            title.SetActive(false);
            powerObject.SetActive(false);
            // image.SetActive(true);
        }
        else
        {
            title.SetActive(true);
            powerObject.SetActive(true);
            // image.SetActive(false);
        }
    }
}
