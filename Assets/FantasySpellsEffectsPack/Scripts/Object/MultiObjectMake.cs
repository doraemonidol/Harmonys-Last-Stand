using UnityEngine;

public class MultiObjectMake : _ObjectMakeBase
{
    public float m_startDelay;
    public int m_makeCount;
    public float m_makeDelay;
    public Vector3 m_randomPos;
    public Vector3 m_randomRot;
    private float m_count;
    private float m_delayTime;
    private float m_Time;
    private float m_Time2;

    private void Start()
    {
        m_Time = m_Time2 = Time.time;
    }

    private void Update()
    {
        if (Time.time > m_Time + m_startDelay)
            if (Time.time > m_Time2 + m_makeDelay && m_count < m_makeCount)
            {
                var m_pos = transform.position + GetRandomVector(m_randomPos);
                var m_rot = transform.rotation * Quaternion.Euler(GetRandomVector(m_randomRot));

                for (var i = 0; i < m_makeObjs.Length; i++)
                {
                    var m_obj = Instantiate(m_makeObjs[i], m_pos, m_rot);
                    m_obj.transform.parent = transform;

                    if (m_movePos)
                        if (m_obj.GetComponent<MoveToObject>())
                        {
                            var m_script = m_obj.GetComponent<MoveToObject>();
                            m_script.m_movePos = m_movePos;
                        }
                }

                m_Time2 = Time.time;
                m_count++;
            }
    }
}