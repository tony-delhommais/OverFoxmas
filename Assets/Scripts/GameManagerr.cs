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

    private bool m_pause = false;


    private void Awake()
    {
        Current = this;
        m_Player = m_fox?.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public bool GetPause()
    {
        return m_pause;
    }
    public void TogglePlayPause()
    {
        m_pause = !m_pause;
        UserInterface.Current.SetPausePanel(m_pause);
        if (m_pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("dev-celine-menu");
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Display at the end of the game
    /// </summary>
    /// <param name="win">Define if the game is won or lost</param>
    public void EndGame(bool win)
    {
        //Set the hight score
        if (m_Player.GetScore() > SaveData.Current.GetHightScore())
        {
            SaveData.Current.SetHightScore(m_Player.GetScore());
        }

        UserInterface.Current.OnGameEnd(win);
    }

}
