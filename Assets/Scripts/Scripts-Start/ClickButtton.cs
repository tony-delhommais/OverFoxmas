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
    Resume,
    Restart,
    ExitGame,
};

public class ClickButtton : MonoBehaviour
{
    [SerializeField]
    private ClickAction m_Action = ClickAction.Play;


    private Animator m_AnimationFox = null;

    public void OnClick()
    {
        switch(m_Action)
        {
            case (ClickAction.Play):
                GameManagerStart.Current?.GoToSpotPlay();
                GameManagerStart.Current?.LoadGameScene("Game");
                break;

            case (ClickAction.Options):
                GameManagerStart.Current?.GoToSpotOptions();
                break;

            case (ClickAction.Credits):
                GameManagerStart.Current?.GoToSpotCredits();
                break;

            case (ClickAction.Exit):
                GameManagerStart.Current?.ExitGame();
                break;

            case (ClickAction.BackMenu):
                GameManagerStart.Current?.GoToSpotMenu();
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

            case (ClickAction.Resume):
                GameManagerr.Current?.TogglePlayPause();
                break;

            case (ClickAction.Restart):
                GameManagerr.Current?.LoadOtherScene(""); // TODO: Changer de scene
                break;

            case (ClickAction.ExitGame):
                GameManagerr.Current?.LoadOtherScene(""); // TODO: Changer de scene
                break;
        }
    }

}
