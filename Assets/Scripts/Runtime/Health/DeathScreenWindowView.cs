using System.Collections;
using UnityEngine;

public class DeathScreenWindowView : WindowView
{
    [SerializeField] float fadeInSpeed;
    [SerializeField] CanvasGroup deathScreenGroup;

    DeathScreenPresentation presentation;

    public override void SetPresentation(MonoBehaviour presentation)
    {
        this.presentation = presentation as DeathScreenPresentation;
        deathScreenGroup.alpha = 0;
        StartCoroutine(nameof(FadeInAnimation));
    }

    public void MainMenuButton()
    {
        presentation.GoToMainMenu();
    }

    public void RespawnButton()
    {
        presentation.Respawn();
    }

    IEnumerator FadeInAnimation()
    {
        while (deathScreenGroup.alpha < 1)
        {
            yield return new WaitForSeconds(0.01f);
            deathScreenGroup.alpha += fadeInSpeed * 0.01f;
        }
    }
}
