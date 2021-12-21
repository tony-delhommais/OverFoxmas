using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image m_BarImage = null;

    private float m_Max = 1f;

    private float m_Value = 1f;

    private void Awake()
    {
        m_BarImage = transform.Find("bar")?.GetComponent<Image>();

        UpdateBar();
    }

    public void SetMaxValue(float p_Max)
    {
        if (p_Max < 1)
            m_Max = 1;
        else
            m_Max = p_Max;

        if (m_Value > m_Max)
            m_Max = m_Value;

        UpdateBar();
    }

    public void SetValue(float p_Value)
    {
        if(p_Value < 0)
            m_Value = 0;
        else if (p_Value > m_Max)
            m_Value = m_Max;
        else
            m_Value = p_Value;

        UpdateBar();
    }

    private void UpdateBar()
    {
        if (m_BarImage) m_BarImage.fillAmount = m_Value / m_Max;
    }
}
