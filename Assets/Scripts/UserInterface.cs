using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface Current;

    [SerializeField]
    private GameObject m_fox = null;

    private Player m_Player = null;

    [Space]
    [SerializeField]
    private GameObject m_TextScore = null;

    [Space]
    [SerializeField]
    private Transform m_Slider_PV = null;

    private HealthBar m_HealthBar = null;

    [Space]
    [SerializeField]
    private GameObject m_GBBonus1 = null;
    private Image m_ImageBonus1 = null;
    [SerializeField]
    private GameObject m_TextBonus1 = null;
    private TextMeshProUGUI m_TextProBonus1 = null;

    [Space]
    [SerializeField]
    private GameObject m_GBBonus2 = null;
    private Image m_ImageBonus2 = null;
    [SerializeField]
    private GameObject m_TextBonus2 = null;
    private TextMeshProUGUI m_TextProBonus2 = null;

    [Space]
    [SerializeField]
    private GameObject m_GBBonus3 = null;
    private Image m_ImageBonus3 = null;
    [SerializeField]
    private GameObject m_TextBonus3 = null;
    private TextMeshProUGUI m_TextProBonus3 = null;

    private bool[] m_ImageBonusDispo = { true, true, true };

    [Space]
    [SerializeField]
    private Sprite m_Default = null;

    [Space]

    [SerializeField]
    private Sprite m_BonusMultiShot = null;
    [SerializeField]
    private Sprite m_BonusShotSpeed = null;
    [SerializeField]
    private Sprite m_BonusShield = null;
    [SerializeField]
    private Sprite m_BonusHeal = null;
    [SerializeField]
    private Sprite m_BonusScore = null;



    private void Awake()
    {
        Current = this;

        m_Player = m_fox?.GetComponent<Player>();

        m_HealthBar = m_Slider_PV?.GetComponent<HealthBar>();

        m_ImageBonus1 = m_GBBonus1?.GetComponent<Image>();
        m_ImageBonus2 = m_GBBonus2?.GetComponent<Image>();
        m_ImageBonus3 = m_GBBonus3?.GetComponent<Image>();

        m_TextProBonus1 = m_TextBonus1?.GetComponent<TextMeshProUGUI>();
        m_TextProBonus2 = m_TextBonus2?.GetComponent<TextMeshProUGUI>();
        m_TextProBonus3 = m_TextBonus3?.GetComponent<TextMeshProUGUI>();

        //abonnement Netflix
        m_Player.OnHPChange += OnHPChange;
        m_Player.OnScoreChange += OnScoreChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_HealthBar?.SetMaxValue(m_Player.GetMaxPV());
        m_HealthBar?.SetValue(m_Player.GetCurrentPV());

        m_TextScore?.GetComponent<TextMeshProUGUI>().SetText("Score : " + m_Player.GetScore().ToString());
    }

    private void OnHPChange()
    {
        m_HealthBar?.SetValue(m_Player.GetCurrentPV());
    }

    private void OnScoreChange()
    {
        m_TextScore?.GetComponent<TextMeshProUGUI>().SetText("Score : " + m_Player.GetScore().ToString());
    }

    public void AddBonus(BonusType p_BonusType, int p_value)
    {
        Image BonusSpot = null;
        TextMeshProUGUI TextSpot = null;
        int spot = -1;

        for(int i = 0; i < 3; i++)
        {
            if(m_ImageBonusDispo[i])
            {
                if (i == 0) {
                    BonusSpot = m_ImageBonus1;
                    TextSpot = m_TextProBonus1;
                }
                if (i == 1) { 
                    BonusSpot = m_ImageBonus2;
                    TextSpot = m_TextProBonus2;
                }
                if (i == 2) { 
                    BonusSpot = m_ImageBonus3;
                    TextSpot = m_TextProBonus3;
                }

                m_ImageBonusDispo[i] = false;
                spot = i;
                break;
            }
        }

        if (!BonusSpot)
        {
            BonusSpot = m_ImageBonus1;
            TextSpot = m_TextProBonus1;
            spot = 1;
        }

        switch (p_BonusType)
        {
            case BonusType.Heal:
                StartCoroutine(DisplayBonusCoroutine(BonusSpot, m_BonusHeal, TextSpot, spot, false, p_value));
                break;
            case BonusType.MultiShoot:
                StartCoroutine(DisplayBonusCoroutine(BonusSpot, m_BonusMultiShot, TextSpot, spot, false, p_value));
                break;
            case BonusType.Shield:
                StartCoroutine(DisplayBonusCoroutine(BonusSpot, m_BonusShield, TextSpot, spot, true, p_value));
                break;
            case BonusType.ShootSpeed:
                StartCoroutine(DisplayBonusCoroutine(BonusSpot, m_BonusShotSpeed, TextSpot, spot, false, p_value));
                break;
            case BonusType.Points:
                StartCoroutine(DisplayBonusCoroutine(BonusSpot, m_BonusScore, TextSpot, spot, false, p_value));
                break;
        }
    }

    IEnumerator DisplayBonusCoroutine(Image p_BonusSpot, Sprite p_Sprite, TextMeshProUGUI p_TextBonus, int p_BonusSpotPos, bool p_IsBonusDuration, int p_BonusValue)
    {
        if(!p_BonusSpot || !p_Sprite)
            yield return new WaitForSeconds(0);

        p_BonusSpot.sprite = p_Sprite;

        if (p_IsBonusDuration)
        {
            int timeWait = p_BonusValue;
            while(timeWait > 0)
            {
                p_TextBonus?.SetText(timeWait.ToString());
                yield return new WaitForSeconds(1);
                timeWait--;
            }
        }
        else
        {
            p_TextBonus?.SetText(p_BonusValue.ToString());
            yield return new WaitForSeconds(3);
        }

        p_TextBonus?.SetText("");
        p_BonusSpot.sprite = m_Default;
        m_ImageBonusDispo[p_BonusSpotPos] = true;
    }
}
