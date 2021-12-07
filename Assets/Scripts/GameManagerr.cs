using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{
    public static GameManagerr Current;
    private bool m_pause = false;

    private void Awake()
    {
        Current = this;
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

}
