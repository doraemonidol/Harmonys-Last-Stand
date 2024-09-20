using UnityEngine;

public class DelayObjectMake : _ObjectMakeBase
{
    public float m_startDelay;
    private bool isMade;
    private float m_Time;

    private void Start()
    {
        m_Time = Time.time;
    }

    private void Update()
    {
        if (Time.time > m_Time + m_startDelay && !isMade)
        {
            isMade = true;
            for (var i = 0; i < m_makeObjs.Length; i++)
            {
                var m_obj = Instantiate(m_makeObjs[i], transform.position, transform.rotation);
                m_obj.transform.parent = transform;

                if (m_movePos)
                    if (m_obj.GetComponent<MoveToObject>())
                    {
                        var m_script = m_obj.GetComponent<MoveToObject>();
                        m_script.m_movePos = m_movePos;
                    }
            }
        }
    }
}