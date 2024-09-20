using UnityEngine;

public class FowardObjectMake : _ObjectMakeBase
{
    public float m_objectSize;
    public float m_subtractYValue;
    public float m_makeCount;
    public float m_makeDelay;
    public bool m_isCrossMake;
    private float m_count;
    private float m_Time;

    private void Update()
    {
        m_Time += Time.deltaTime;
        var addedPos = new Vector3(0, 0, 0);
        var crossMake = 0;

        if (m_Time > m_makeDelay && m_count < m_makeCount)
        {
            if (m_isCrossMake)
            {
                if (m_count % 2 == 0)
                    crossMake = 1;
                else
                    crossMake = -1;
            }

            addedPos = transform.forward * m_objectSize * m_count;
            var pos = transform.position - new Vector3(0, m_subtractYValue, 0) + addedPos + transform.right * crossMake;
            var rot = transform.rotation * Quaternion.Euler(-90, 0, 0);

            for (var i = 0; i < m_makeObjs.Length; i++)
            {
                var m_obj = Instantiate(m_makeObjs[i], pos, rot);
                m_obj.transform.parent = transform;

                if (m_movePos)
                    if (m_obj.GetComponent<MoveToObject>())
                    {
                        var m_script = m_obj.GetComponent<MoveToObject>();
                        m_script.m_movePos = m_movePos;
                    }
            }

            m_Time = 0;
            m_count++;
        }
    }
}