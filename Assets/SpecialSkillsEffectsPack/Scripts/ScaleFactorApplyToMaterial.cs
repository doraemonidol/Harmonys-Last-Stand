using UnityEngine;

public class ScaleFactorApplyToMaterial : MonoBehaviour
{
    private float m_changedFactor;
    private float m_scaleFactor;
    private ParticleSystemRenderer ps;
    private float value;

    private void Awake()
    {
        ps = GetComponent<ParticleSystemRenderer>();
        value = ps.material.GetFloat("_NoiseScale");
        m_scaleFactor = 1;
    }

    private void Update()
    {
        m_changedFactor = VariousEffectsScene.m_gaph_scenesizefactor; //Please change this in your actual project

        if (m_scaleFactor != m_changedFactor && m_changedFactor <= 1)
        {
            m_scaleFactor = m_changedFactor;
            if (m_scaleFactor <= 0.5f)
                ps.material.SetFloat("_NoiseScale", value * 0.25f);
            else
                ps.material.SetFloat("_NoiseScale", value * m_scaleFactor);
        }
    }
}