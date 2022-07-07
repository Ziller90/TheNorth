using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    [SerializeField] Health mainCharacterHealth;
    [SerializeField] float fadeInSpeed;
    [SerializeField] CanvasGroup deathScreenGroup;

    void Start()
    {
        mainCharacterHealth.dieEvent += ActivateDeathScreen;
    }
    public void ActivateDeathScreen()
    {
        deathScreen.SetActive(true);
        StartCoroutine("FadeIn");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
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
