using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenPresentation : MonoBehaviour
{
    [SerializeField] WindowOpener deathScreenOpener;

    void Start()
    {
        Game.GameSceneInitializer.Player.GetComponent<Health>().dieEvent += deathScreenOpener.ShowWindow;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
