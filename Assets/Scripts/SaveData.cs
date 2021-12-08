using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    public static SaveData Current;

    private void Awake()
    {
        Current = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {

    }

    /// <summary>
    /// Set Hight Score
    /// </summary>
    /// <returns></returns>
    public void SetHightScore(int hightScore)
    {
        PlayerPrefs.SetInt("Score", hightScore);

        /*if (m_Player.GetScore() > PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", m_Player.GetScore());
        }
        Debug.Log($"Hight Score : {PlayerPrefs.GetInt("Score")}");

        return PlayerPrefs.GetInt("Score");*/
    }

    // Update is called once per frame
    /// <summary>
    /// Get Hight Score
    /// </summary>
    public int GetHightScore()
    {
        return PlayerPrefs.GetInt("Score");
    }
}
