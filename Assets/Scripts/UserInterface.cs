using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GameOver = null;

    [SerializeField]
    private Button m_PlayAgain = null ;

    [SerializeField]
    private Text m_Score = null;

    [SerializeField]
    private Slider m_Slider_PV = null;

    [SerializeField]
    private GameObject m_fox = null;

    private Player m_Player = null;


    private void Awake()
    {
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
        m_Score.text = "Score :" + m_Player.GetScore();

        //Ne pas afficher lors du jeu
        m_GameOver.gameObject.SetActive(false);
        m_PlayAgain.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnHPChange()
    {
        m_Slider_PV.value = m_Player.GetCurrentPV();

        if (m_Player.GetCurrentPV() <= 0)
        {
            m_GameOver.gameObject.SetActive(true);
            m_PlayAgain.gameObject.SetActive(true);
        }
        
    }

    private void OnScoreChange()
    {
        m_Score.text = "Score :" + m_Player.GetScore();
    }
}
