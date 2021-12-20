using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClickAction
{
    Play,
    Options,
    Credits,
    Exit,
    BackMenu,
    Dance,
};

public class ClickButtton : MonoBehaviour
{
    [SerializeField]
    private ClickAction m_Action = ClickAction.Play;


    private Animator m_AnimationFox = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        switch(m_Action)
        {
            case (ClickAction.Play):
                GameManagerStart.Current.GoToSpotPlay();
                GameManagerStart.Current.LoadGameScene("Game");
                break;

            case (ClickAction.Options):
                GameManagerStart.Current.GoToSpotOptions();
                break;

            case (ClickAction.Credits):
                GameManagerStart.Current.GoToSpotCredits();
                break;

            case (ClickAction.Exit):
                GameManagerStart.Current.ExitGame();
                break;

            case (ClickAction.BackMenu):
                GameManagerStart.Current.GoToSpotMenu();
                break;

            case (ClickAction.Dance):
                m_AnimationFox = GetComponent<Animator>();
                
                int choice = m_AnimationFox.GetInteger("dance") + 1; 
                
                if (choice > 6)
                {
                    choice = 1;
                }
                m_AnimationFox.SetInteger("dance", choice);

                break;
        }
    }

}
