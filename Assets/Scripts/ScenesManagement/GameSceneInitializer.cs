using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] GameObject Humanoid;
    [SerializeField] ActionManager playerActionManager;
    [SerializeField] ControlManager playerControlManager;
    public UnityAction sceneInitialized;
    public void Start()
    {
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
        GameObject playerCharacter = Instantiate(Humanoid, Links.instance.locationSettings.GetRandomSpawnPoint().position, Quaternion.identity);
        Links.instance.mainCamera.GetComponent<CameraFollowing>().SetObjectToFollow(playerCharacter);
        playerCharacter.GetComponent<CharacterContoller>().SetControlManager(playerControlManager);
        playerCharacter.GetComponent<HumanoidBattleSystem>().SetActionManager(playerActionManager);
        Links.instance.playerCharacter = playerCharacter;
    }
}
