using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLight : MonoBehaviour
{
    [SerializeField]
    private GameObject m_LightG1 = null;
    [SerializeField]
    private GameObject m_LightG2 = null;
    [SerializeField]
    private GameObject m_LightG3 = null;

    [SerializeField]
    private GameObject m_LightR1 = null;
    [SerializeField]
    private GameObject m_LightR2 = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightBlink());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator LightBlink()
    {
        bool m_LightON = true;

        while (true)
        {
            m_LightG1.gameObject.SetActive(m_LightON);
            m_LightG2.gameObject.SetActive(m_LightON);
            m_LightG3.gameObject.SetActive(m_LightON);
            m_LightR1.gameObject.SetActive(!m_LightON);
            m_LightR2.gameObject.SetActive(!m_LightON);
            m_LightON = !m_LightON;
            yield return new WaitForSeconds(1f);
        }
    }
}
