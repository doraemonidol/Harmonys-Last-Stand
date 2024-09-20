using UnityEngine;

public class VariousRotateObject : MonoBehaviour
{
    public Vector3 RotateOffset;
    public float m_delay;
    private float m_Time;
    private Vector3 RotateMulti;

    private void Awake()
    {
        m_Time = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time < m_Time + m_delay)
            return;
        RotateMulti = Vector3.Lerp(RotateMulti, RotateOffset, Time.deltaTime);

        transform.rotation *= Quaternion.Euler(RotateMulti);
    }
}