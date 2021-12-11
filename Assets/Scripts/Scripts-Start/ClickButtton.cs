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
};
public class ClickButtton : MonoBehaviour
{
    [SerializeField]
    private ClickAction m_Action = ClickAction.Play;

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
        }
    }
}
