using UnityEngine;

public class MoveToTag : MonoBehaviour
{
    public string m_tag;
    public float m_startDelay;
    public float m_durationTime;
    public float m_lerpValue;
    public float m_lookValue;

    private bool m_isRunning;

    private GameObject m_movePos;
    private float m_Time;

    private void Start()
    {
        m_movePos = GameObject.FindGameObjectWithTag(m_tag);
        m_Time = Time.time;
    }

    private void Update()
    {
        if (Time.time > m_Time + m_startDelay || m_isRunning)
        {
            m_isRunning = true;
            if (Time.time < m_Time + m_durationTime)
            {
                transform.position = Vector3.Lerp(transform.position, m_movePos.transform.position,
                    Time.deltaTime * m_lerpValue);
                if (Vector3.Distance(transform.position, m_movePos.transform.position) > 1)
                {
                    var lookPos = Quaternion.LookRotation(transform.position - m_movePos.transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookPos, Time.deltaTime * m_lookValue);
                }
            }
        }
    }
}