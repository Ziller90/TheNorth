using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] GameObject Humanoid;

    ControlManager playerControlManager;
    ActionManager playerActionManager;
    public UnityAction sceneInitialized;

    public void Start()
    {
        playerActionManager = Links.instance.playerActionManager;
        playerControlManager = Links.instance.playerControlManager;
        InitializeScene();
    }
    public void InitializeScene()
    {
        Links.instance.locationLoader.LoadLocation();
        CreatePlayerCharacter();
        sceneInitialized?.Invoke();
    }
    public void CreatePlayerCharacter()
    {
        Debug.Log(Links.instance.locationSettings);
        GameObject playerCharacter = Instantiate(Humanoid, Links.instance.locationSettings.GetRandomSpawnPoint().position, Quaternion.identity);
        Links.instance.mainCamera.GetComponent<CameraFollowing>().SetObjectToFollow(playerCharacter);
        playerCharacter.GetComponent<CharacterContoller>().SetControlManager(playerControlManager);
        playerCharacter.GetComponent<HumanoidBattleSystem>().SetActionManager(playerActionManager);
        playerCharacter.GetComponentInChildren<ItemsCollector>().SetActionManager(playerActionManager);
        Links.instance.playerCharacter = playerCharacter;
    }
}
