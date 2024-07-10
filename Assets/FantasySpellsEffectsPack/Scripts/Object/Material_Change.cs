using UnityEngine;

public class Material_Change : MonoBehaviour
{
    public Material m_inputMaterial;
    public float m_timeToReduce;
    public float m_reduceFactor = 1.0f;
    private float m_cutOutFactor;
    private MeshRenderer m_meshRenderer;
    private Material m_objectMaterial;
    private float m_submitReduceFactor;
    private float m_time;

    private void Awake()
    {
        m_meshRenderer = gameObject.GetComponent<MeshRenderer>();
        m_meshRenderer.material = m_inputMaterial;
        m_objectMaterial = m_meshRenderer.material;
        m_submitReduceFactor = 0.0f;
        m_cutOutFactor = 0.0f;
    }

    private void LateUpdate()
    {
        m_time += Time.deltaTime;
        if (m_time > m_timeToReduce)
        {
            m_cutOutFactor += m_submitReduceFactor;
            m_submitReduceFactor = Mathf.Lerp(m_submitReduceFactor, m_reduceFactor, Time.deltaTime / 50);
        }

        m_cutOutFactor = Mathf.Clamp01(m_cutOutFactor);
        if (m_cutOutFactor >= 1 && m_time > m_timeToReduce)
            Destroy(gameObject);
        m_objectMaterial.SetFloat("_CutOut", m_cutOutFactor);
    }
}