using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    GameObject player;

    public GameObject Player => player;

    public void Awake()
    {
        InitializeScene();
    }

    public void InitializeScene()
    {
        Game.LocationLoader.LoadLocation();
        InitializePlayer();
    }

    public void InitializePlayer()
    {
        player = Instantiate(playerPrefab, Game.LocationLoader.LoadedLocationModel.GetRandomSpawnPoint().position, Quaternion.identity);
        Game.CameraControlService.SetObjectToFollow(player);

        ActionManager playerActionManager = player.GetComponent<ActionManager>();
        ControlManager playerControlManager = player.GetComponent<ControlManager>();

        Game.MobileControlService.SetControl(playerControlManager, playerActionManager);
        Game.DesktopControlService.SetControl(playerControlManager, playerActionManager);
        Game.CameraControlService.SetObjectToFollow(player);
        Game.SavingService.RestoreLocation(Game.LocationLoader.LoadedLocationModel);
    }

    public void LeaveLocation()
    {
        Game.SavingService.SaveLocation();
        SceneManager.LoadScene("GlobalMapScene");
    }
}
