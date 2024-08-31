using UnityEngine;

public class MultiCircleObjectMake : _ObjectMakeBase
{
    public float m_interval;
    public int m_makeCount;
    public float m_makeDelay;
    public float m_startDelay;
    private float m_count;
    private float m_Time;
    private float m_Time2;

    private void Start()
    {
        m_Time2 = Time.time;
    }

    private void Update()
    {
        m_Time += Time.deltaTime;
        if (Time.time < m_Time2 + m_startDelay)
            return;

        if (m_Time > m_makeDelay && m_count < m_makeCount)
        {
            var Angle = 2.0f * Mathf.PI / m_makeCount * m_count;
            var pos_X = Mathf.Cos(Angle) * m_interval;
            var pos_Z = Mathf.Sin(Angle) * m_interval;

            m_Time = 0.0f;
            for (var i = 0; i < m_makeObjs.Length; i++)
            {
                var m_obj = Instantiate(m_makeObjs[i], transform.position + new Vector3(pos_X, 0, pos_Z),
                    Quaternion.LookRotation(new Vector3(pos_X, 0, pos_Z)) * m_makeObjs[i].transform.rotation);
                m_obj.transform.parent = transform;

                if (m_movePos)
                    if (m_obj.GetComponent<MoveToObject>())
                    {
                        var m_script = m_obj.GetComponent<MoveToObject>();
                        m_script.m_movePos = m_movePos;
                    }
            }

            m_count++;
        }
    }
}