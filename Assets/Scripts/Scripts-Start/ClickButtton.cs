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
    Music,
    Resume,
    Restart,
    ExitGame,
};

public class ClickButtton : MonoBehaviour
{
    [SerializeField]
    private ClickAction m_Action = ClickAction.Play;


    private Animator m_AnimationFox = null;

    private AudioSource m_Piste = null;

    public static bool m_Musicint = true;

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

            case (ClickAction.Music):
                m_Piste = GetComponent<AudioSource>();

                if (m_Piste.isPlaying == true)
                {
                    m_Musicint = false;
                    m_Piste.Pause();
                }
                else
                {
                    m_Musicint = true;
                    m_Piste.Play();
                }

                break;

            case (ClickAction.Resume):
                GameManagerr.Current?.TogglePlayPause();
                break;

            case (ClickAction.Restart):
                GameManagerr.Current?.LoadOtherScene("Game"); // TODO: Changer de scene
                break;

            case (ClickAction.ExitGame):
                GameManagerr.Current?.FondueFin();
                GameManagerr.Current?.LoadOtherScene("MenuPrincipal"); // TODO: Changer de scene
                break;
        }
    }

}
