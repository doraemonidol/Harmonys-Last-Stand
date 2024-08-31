using UnityEngine;

public class RotateToTargetScript : MonoBehaviour, RotateToDirection
{
    [SerializeField] private GameObject _target;
    
    public Vector3 GetDirection()
    {
        Vector3 dir = _target.transform.position - transform.position;
        dir.y = 0;
        return dir;
    }

    public Quaternion GetRotation()
    {
        return Quaternion.LookRotation(GetDirection());
    }

    public void RotateTo(GameObject obj, Vector3 destination)
    {
        obj.transform.forward = GetDirection();
    }
}