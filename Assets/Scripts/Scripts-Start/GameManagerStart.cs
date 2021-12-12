using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManagerStart : MonoBehaviour
{
    public static GameManagerStart Current;

    [SerializeField]
    private CinemachineVirtualCamera m_CineCam = null;

    [SerializeField]
    private Transform m_SpotMenu = null;

    [SerializeField]
    private Transform m_SpotOptions = null;

    [SerializeField]
    private Transform m_SpotCredits = null;

    [SerializeField]
    private Transform m_LookAtSpotMenu = null;

    [SerializeField]
    private Transform m_LookAtSpotOptions = null;

    [SerializeField]
    private Transform m_LookAtSpotCredits = null;



    //PANELS
    /*[SerializeField]
    private GameObject m_Menu = null;
    [SerializeField]
    private GameObject m_Settings = null;
    [SerializeField]
    private GameObject m_Credit = null;*/

    private void Awake()
    {
        Current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*m_Menu.SetActive(true);
        m_Settings.SetActive(false);
        m_Credit.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void LoadGameScene(string p_SceneName)
    {
        if(p_SceneName.Length != 0) SceneManager.LoadScene(p_SceneName);
    }

    /*public void LoadPanelMenu()
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
    }*/

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToSpotMenu()
    {
        if (m_CineCam != null && m_SpotMenu != null)
        {
            m_CineCam.Follow = m_SpotMenu;

            if (m_LookAtSpotMenu != null)
            {
                m_CineCam.LookAt = m_LookAtSpotMenu;
            }
        }
    }

    public void GoToSpotOptions()
    {
        if (m_CineCam != null && m_SpotOptions != null)
        {
            m_CineCam.Follow = m_SpotOptions;

            if (m_LookAtSpotOptions != null)
            {
                m_CineCam.LookAt = m_LookAtSpotOptions;
            }
        }
    }

    public void GoToSpotCredits()
    {
        if (m_CineCam != null && m_SpotCredits != null)
        {
            m_CineCam.Follow = m_SpotCredits;

            if (m_LookAtSpotCredits != null)
            {
                m_CineCam.LookAt = m_LookAtSpotCredits;
            }
        }
    }
}
