using UnityEngine;

public interface RotateToDirection
{
    public Vector3 GetDirection();

    public Quaternion GetRotation();

    public void RotateTo(GameObject obj, Vector3 destination);
}