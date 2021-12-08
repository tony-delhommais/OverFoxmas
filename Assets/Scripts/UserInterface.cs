using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface Current;

    [SerializeField]
    private Text m_Score = null;

    [SerializeField]
    private Slider m_Slider_PV = null;

    [SerializeField]
    private GameObject m_fox = null;

    private Player m_Player = null;

    [SerializeField]
    private GameObject m_Pause = null;

    [SerializeField]
    private GameObject m_End = null;

    [SerializeField]
    private Text m_HightScore = null;

    [SerializeField]
    private GameObject m_PanelEndWin = null;

    [SerializeField]
    private GameObject m_PanelEndLose = null;



    private void Awake()
    {
        Current = this;

        m_Player = m_fox?.GetComponent<Player>();

        //abonnement Netflix
        m_Player.OnHPChange += OnHPChange;
        m_Player.OnScoreChange += OnScoreChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Slider_PV.maxValue = m_Player.GetMaxPV();
        m_Slider_PV.value = m_Player.GetCurrentPV();
        m_Score.text = "Score : " + m_Player.GetScore();

        //Ne pas afficher lors du jeu
        m_End.SetActive(false);
        m_Pause.SetActive(false);
        m_HightScore.gameObject.SetActive(false);
        m_PanelEndWin.SetActive(false);
        m_PanelEndLose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnHPChange()
    {
        m_Slider_PV.value = m_Player.GetCurrentPV();
    }

    private void OnScoreChange()
    {
        m_Score.text = "Score : " + m_Player.GetScore();
    }

    public void SetPausePanel(bool pause)
    {
        m_Pause.SetActive(pause);
    }

    public void OnGameEnd(bool win)
    {
        if (win == true)
        {
            m_PanelEndWin.SetActive(true);
        }
        else
        {
            m_PanelEndLose.SetActive(true);
        }

        m_End.SetActive(true);
        m_HightScore.gameObject.SetActive(true);
        m_HightScore.text = "Hight score : " + SaveData.Current.GetHightScore();
    }
}
