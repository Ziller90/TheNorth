using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] SimpleContainer remainsPrefab;
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
        ClearDeadBodies();
        Game.SavingService.SaveLocation();
        SceneManager.LoadScene("GlobalMapScene");
    }

    public void ClearDeadBodies()
    {
        var deadUnits = FindObjectsOfType<Unit>().Where(i => i.IsDead);
        foreach (var deadBody in deadUnits)
        {
            if (!deadBody.DeadBodyContainer.IsEmpty())
            {
                var remains = Instantiate(remainsPrefab, deadBody.DeadBodyContainer.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                foreach (var slot in deadBody.DeadBodyContainer.SlotGroup.Slots)
                    ModelUtils.TryMoveFromSlotToSlotGroup(deadBody.DeadBodyContainer, slot, remains, remains.SlotGroup);
            }
            Destroy(deadBody);
        }
    }
}
