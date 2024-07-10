using UnityEngine;
using UnityEngine.UI;

public class ChangeSizeColor : MonoBehaviour
{
    public Gradient color;

    public Color m_changeColor;

    //[HideInInspector]
    public GameObject m_obj;
    public Image m_ColorHandler;
    public Text m_intensityfactor;
    private float color_Value;
    private float intensity = 2.0f;
    private bool isChangeColor;
    private Renderer[] m_rnds;

    private void Update()
    {
        m_changeColor = color.Evaluate(color_Value);
        m_ColorHandler.color = m_changeColor;

        if (isChangeColor && m_obj != null)
        {
            m_rnds = m_obj.GetComponentsInChildren<Renderer>(true);

            foreach (var rend in m_rnds)
                for (var i = 0; i < rend.materials.Length; i++)
                {
                    rend.materials[i].SetColor("_TintColor", m_changeColor * intensity);
                    rend.materials[i].SetColor("_Color", m_changeColor * intensity);
                    rend.materials[i].SetColor("_RimColor", m_changeColor * intensity);
                }
        }
    }

    public void ChangeEffectColor(float value)
    {
        color_Value = value;
    }

    public void CheckIsColorChange(bool value)
    {
        isChangeColor = value;
    }

    public void CheckColorState()
    {
        if (isChangeColor)
            isChangeColor = false;
        else
            isChangeColor = true;
    }

    public void GetIntensityFactor()
    {
        var m_intensity = float.Parse(m_intensityfactor.text);
        if (m_intensity > 0)
            intensity = m_intensity;
        else
            intensity = 0;
    }
}