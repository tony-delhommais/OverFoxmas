using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject m_WinOrLoseText = null;

    [SerializeField]
    private GameObject m_ScoreText = null;

    [SerializeField]
    private GameObject m_HighScoreText = null;

    [Space]

    [SerializeField]
    private GameObject m_fox = null;

    private Player m_Player = null;

    private void Awake()
    {
        m_Player = m_fox?.GetComponent<Player>();
    }

    public void OnGameEnd(bool p_win)
    {
        m_WinOrLoseText?.GetComponent<TextMeshPro>()?.SetText(p_win ? "WIN" : "LOSE");

        m_ScoreText?.GetComponent<TextMeshPro>()?.SetText(m_Player.GetScore().ToString());

        m_HighScoreText?.GetComponent<TextMeshPro>()?.SetText(SaveData.Current?.GetHightScore().ToString());
    }
}
