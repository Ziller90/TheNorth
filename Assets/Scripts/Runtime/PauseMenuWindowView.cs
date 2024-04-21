using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuWindowView : WindowView
{
    PauseMenuPresentation presentation;
    public override void SetPresentation(MonoBehaviour presentation)
    {
        this.presentation = presentation as PauseMenuPresentation;  
    }

    public override void HideWindow()
    {
        presentation.ClosePauseMenuButton();
        base.HideWindow();
    }

    public void MainMenuButton() => presentation.GoToMainMenu();
    public void SaveGameButton() => presentation.OpenSaveGameMenu();
    public void LoadGameButton() => presentation.OpenLoadGameMenu();
}
