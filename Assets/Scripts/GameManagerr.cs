using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{
    public static GameManagerr Current;

    [SerializeField]
    private GameObject m_fox = null;

    private Player m_Player = null;

    [SerializeField]
    private GameObject m_PausePanel = null;

    private Animator m_PausePanelAnimator = null;

    [SerializeField]
    private GameObject m_EndPanel = null;

    private Animator m_EndPanelAnimator = null;

    private bool m_pause = false;

    [SerializeField]
    private GameObject m_MusicGO = null;

    private AudioSource m_Audio = null;


    private void Awake()
    {
        Current = this;

        m_Player = m_fox?.GetComponent<Player>();

        m_PausePanelAnimator = m_PausePanel?.GetComponent<Animator>();
        m_EndPanelAnimator = m_EndPanel?.GetComponent<Animator>();

        if (m_MusicGO)
        {
            m_Audio = m_MusicGO.GetComponent<AudioSource>();
        }
    }
    void Start()
    {
        if (ClickButtton.m_Musicint == true)
        {
            m_Audio.Play();
        }
    }
    
    static public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public bool IsPaused()
    {
        return m_pause;
    }
    public void TogglePlayPause()
    {
        m_pause = !m_pause;

        if (m_pause)
        {
            m_PausePanelAnimator?.SetBool("pause", true);
            Time.timeScale = 0f;
        }
        else
        {
            m_PausePanelAnimator?.SetBool("pause", false);
            Time.timeScale = 1f;
        }
    }

    public void LoadOtherScene(string p_SceneName)
    {
        if (p_SceneName.Length != 0)
        {
            SceneManager.LoadScene(p_SceneName);
            Time.timeScale = 1f;
        }
    }

    /// <summary>
    /// Display at the end of the game
    /// </summary>
    /// <param name="win">Define if the game is won or lost</param>
    public void EndGame(bool win)
    {
        if (win) m_Player?.GameFinished();

        //Set the hight score
        if (SaveData.Current && m_Player?.GetScore() > SaveData.Current?.GetHightScore())
        {
            SaveData.Current?.SetHightScore(m_Player.GetScore());
        }

        //UserInterface.Current?.OnGameEnd(win);
        m_EndPanel.GetComponent<EndPanel>()?.OnGameEnd(win);
        m_EndPanelAnimator?.SetBool("finish", true);
    }

}
