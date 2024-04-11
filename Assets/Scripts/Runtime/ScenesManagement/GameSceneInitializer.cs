using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void Awake()
    {
        InitializeScene();
    }

    public void InitializeScene()
    {
        Links.instance.locationLoader.LoadLocation();
        Links.instance.savingService = FindObjectOfType<SavingService>();
        CreatePlayerCharacter();
    }

    public void CreatePlayerCharacter()
    {
        GameObject playerCharacter;
        playerCharacter = Instantiate(player, Links.instance.locationModel.GetRandomSpawnPoint().position, Quaternion.identity);
        SetMainCharacter(playerCharacter);
    }

    public void SetMainCharacter(GameObject character)
    {
        Links.instance.mainCamera.GetComponent<CameraFollowing>().SetObjectToFollow(character);

        ActionManager characterActionManager = character.GetComponent<ActionManager>();
        ControlManager characterControlManager = character.GetComponent<ControlManager>();

        Links.instance.fixedJoystick.SetControlManager(characterControlManager);
        Links.instance.keyboard.SetControlManager(characterControlManager);
        Links.instance.keyboard.SetActionManager(characterActionManager);
        Links.instance.mobileButtonsManager.SetActionManager(characterActionManager);
        Links.instance.playerCharacter = character;
        character.GetComponent<Health>().dieEvent += Links.instance.deathScreen.ActivateDeathScreen;
    }

    public void LeaveLocation()
    {
        Links.instance.savingService.SaveLocation();
        SceneManager.LoadScene("GlobalMapScene");
    }
}
