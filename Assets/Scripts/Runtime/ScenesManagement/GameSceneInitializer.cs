using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] GameObject player;

    public GameObject Player => player;

    public void Awake()
    {
        InitializeScene();
    }

    public void InitializeScene()
    {
        Game.LocationLoader.LoadLocation();
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        GameObject player;
        player = Instantiate(this.player, Game.LocationLoader.LoadedLocationModel.GetRandomSpawnPoint().position, Quaternion.identity);
        SetPlayer(player);
    }

    public void SetPlayer(GameObject player)
    {
        Game.MainCameraService.SetObjectToFollow(player);

        ActionManager characterActionManager = player.GetComponent<ActionManager>();
        ControlManager characterControlManager = player.GetComponent<ControlManager>();

        Links.instance.fixedJoystick.SetControlManager(characterControlManager);
        Links.instance.keyboard.SetControlManager(characterControlManager);
        Links.instance.keyboard.SetActionManager(characterActionManager);
        Links.instance.mobileButtonsManager.SetActionManager(characterActionManager);
        player.GetComponent<Health>().dieEvent += Links.instance.deathScreen.ActivateDeathScreen;
        this.player = player;
    }

    public void LeaveLocation()
    {
        Game.SavingService.SaveLocation();
        SceneManager.LoadScene("GlobalMapScene");
    }
}
