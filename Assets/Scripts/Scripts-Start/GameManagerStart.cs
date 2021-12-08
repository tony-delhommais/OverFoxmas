using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerStart : MonoBehaviour
{
    //PANELS
    [SerializeField]
    private GameObject m_Menu = null;
    [SerializeField]
    private GameObject m_Settings = null;
    [SerializeField]
    private GameObject m_Credit = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Menu.SetActive(true);
        m_Settings.SetActive(false);
        m_Credit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("dev-celine");
    }

    public void LoadPanelMenu()
    {
        if (m_Settings.activeSelf == true)
        {
            m_Settings.SetActive(false);
        }
        else
        {
            m_Credit.SetActive(false);
        }
        m_Menu.SetActive(true);
    }
    public void LoadPanelSettings()
    {
        m_Menu.SetActive(false);
        m_Settings.SetActive(true);
    }

    public void LoadPanelCredit()
    {
        m_Menu.SetActive(false);
        m_Credit.SetActive(true);
    }
}
