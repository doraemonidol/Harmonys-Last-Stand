using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float m_DestroytTime;
    private float m_Time;

    // Start is called before the first frame update
    private void Start()
    {
        m_Time = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > m_Time + m_DestroytTime)
            Destroy(gameObject);
    }
}