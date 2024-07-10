using UnityEngine;

public class MultipleObjectsMake : _ObjectsMakeBase
{
    public float m_startDelay;
    public int m_makeCount;
    public float m_makeDelay;
    public Vector3 m_randomPos;
    public Vector3 m_randomRot;
    public Vector3 m_randomScale;
    public bool isObjectAttachToParent = true;
    private float m_count;
    private float m_delayTime;
    private float m_scalefactor;

    private float m_Time;
    private float m_Time2;


    private void Start()
    {
        m_Time = m_Time2 = Time.time;
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor; //transform.parent.localScale.x; 
    }


    private void Update()
    {
        if (Time.time > m_Time + m_startDelay)
            if (Time.time > m_Time2 + m_makeDelay && m_count < m_makeCount)
            {
                var m_pos = transform.position + GetRandomVector(m_randomPos) * m_scalefactor;
                var m_rot = transform.rotation * Quaternion.Euler(GetRandomVector(m_randomRot));


                for (var i = 0; i < m_makeObjs.Length; i++)
                {
                    var m_obj = Instantiate(m_makeObjs[i], m_pos, m_rot);
                    var m_scale = m_makeObjs[i].transform.localScale + GetRandomVector2(m_randomScale);
                    if (isObjectAttachToParent)
                        m_obj.transform.parent = transform;
                    m_obj.transform.localScale = m_scale;
                }

                m_Time2 = Time.time;
                m_count++;
            }
    }
}