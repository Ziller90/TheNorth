using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] float fadeInSpeed;
    [SerializeField] CanvasGroup deathScreenGroup;

    void Start()
    {
        Links.instance.sceneInitializer.sceneInitialized += () => 
        Links.instance.playerCharacter.GetComponentInChildren<Health>().dieEvent += ActivateDeathScreen;
    }
    public void ActivateDeathScreen()
    {
        deathScreen.SetActive(true);
        StartCoroutine("FadeIn");
    }
    public void DeactivateDeathScreen()
    {
        deathScreen.SetActive(false);
        deathScreenGroup.alpha = 0;
    }
    public void RestartGame()
    {
        if (GameSceneLauncher.LocationToLoadGameType == GameType.Singleplayer)
        {
            SceneManager.LoadScene(0);
        }
        else if (GameSceneLauncher.LocationToLoadGameType == GameType.DeathMatch)
        {
            DeactivateDeathScreen();
            Links.instance.sceneInitializer.CreatePlayerCharacter();
        }
    }
    IEnumerator FadeIn()
    {
        while (deathScreenGroup.alpha < 1)
        {
            yield return new WaitForSeconds(0.01f);
            deathScreenGroup.alpha += fadeInSpeed * 0.01f;
        }
    }
}
