using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public enum RotationAxis
    {
        X,
        Y,
        Z
    }

    public RotationAxis rotationAxis = RotationAxis.Y;
    public float rotationSpeed = 50.0f;

    private void Update()
    {
        var rotationValue = rotationSpeed * Time.deltaTime;

        // Rotation Axis
        var axis = Vector3.zero;
        switch (rotationAxis)
        {
            case RotationAxis.X:
                axis = Vector3.right;
                break;
            case RotationAxis.Y:
                axis = Vector3.up;
                break;
            case RotationAxis.Z:
                axis = Vector3.forward;
                break;
        }

        // Rotate object
        transform.Rotate(axis, rotationValue);
    }
}