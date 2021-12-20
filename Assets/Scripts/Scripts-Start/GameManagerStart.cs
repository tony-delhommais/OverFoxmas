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
    private Transform m_SpotPlay = null;

    [SerializeField]
    private Transform m_LookAtSpotMenu = null;

    [SerializeField]
    private Transform m_LookAtSpotOptions = null;

    [SerializeField]
    private Transform m_LookAtSpotCredits = null;

    [SerializeField]
    private Transform m_LookAtSpotPlay = null;

    private Transform m_LookFrom = null;
    private Transform m_LookTo = null;

    [SerializeField]
    private GameObject m_TransitionEnd = null;



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
        m_TransitionEnd.SetActive(false);
    }

    float timeToWait = 7.0f;
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if(m_LookTo && m_CineCam)
        {
            m_CineCam.LookAt.position = Vector3.Lerp(m_LookFrom.position, m_LookTo.position, timer / timeToWait);
            //m_CineCam.LookAt.rotation = Quaternion.Lerp(m_LookFrom.rotation, m_LookTo.rotation, timer / timeToWait);

            if (timer >= timeToWait)
            {
                m_LookTo = null;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void LoadGameScene(string p_SceneName)
    {
        if (p_SceneName.Length != 0)
        {
            StartCoroutine(TransitionToPlay(p_SceneName));  
        }
    }

    private IEnumerator TransitionToPlay(string p_SceneName)
    {
        yield return new WaitForSeconds(3.5f);
        m_TransitionEnd.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(p_SceneName);

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

            m_LookFrom = m_CineCam.LookAt.transform;
            m_LookTo = m_LookAtSpotMenu;
            timer = 0;

            /*if (m_LookAtSpotMenu != null)
            {
                m_CineCam.LookAt = m_LookAtSpotMenu;
            }*/
        }
    }

    public void GoToSpotOptions()
    {
        if (m_CineCam != null && m_SpotOptions != null)
        {
            m_CineCam.Follow = m_SpotOptions;

            m_LookFrom = m_CineCam.LookAt.transform;
            m_LookTo = m_LookAtSpotOptions;
            timer = 0;

            /*if (m_LookAtSpotOptions != null)
            {
                m_CineCam.LookAt = m_LookAtSpotOptions;
            }*/
        }
    }

    public void GoToSpotCredits()
    {
        if (m_CineCam != null && m_SpotCredits != null)
        {
            m_CineCam.Follow = m_SpotCredits;

            m_LookFrom = m_CineCam.LookAt.transform;
            m_LookTo = m_LookAtSpotCredits;
            timer = 0;

            /*if (m_LookAtSpotCredits != null)
            {
                m_CineCam.LookAt = m_LookAtSpotCredits;
            }*/
        }
    }

    public void GoToSpotPlay()
    {
        if (m_CineCam != null && m_SpotPlay != null)
        {
            m_CineCam.Follow = m_SpotPlay;

            m_LookFrom = m_CineCam.LookAt.transform;
            m_LookTo = m_LookAtSpotPlay;
            timer = 0;
        }

    }
}
