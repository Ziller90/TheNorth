using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenPresentation : MonoBehaviour
{
    [SerializeField] WindowOpener deathScreenOpener;

    void Start()
    {
        if (Game.GameSceneInitializer.Player != null)
        {
            Game.GameSceneInitializer.Player.GetComponent<Health>().dieEvent -= deathScreenOpener.ShowWindow;
            Game.GameSceneInitializer.Player.GetComponent<Health>().dieEvent += deathScreenOpener.ShowWindow;
        }
    }

    private void OnEnable()
    {
        if (Game.GameSceneInitializer.Player != null)
        {
            Game.GameSceneInitializer.Player.GetComponent<Health>().dieEvent -= deathScreenOpener.ShowWindow;
            Game.GameSceneInitializer.Player.GetComponent<Health>().dieEvent += deathScreenOpener.ShowWindow;
        }
    }

    private void OnDisable()
    {
        if (Game.GameSceneInitializer.Player != null)
        {
            Game.GameSceneInitializer.Player.GetComponent<Health>().dieEvent -= deathScreenOpener.ShowWindow;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
