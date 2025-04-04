using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using SiegeUp.Core;

public class GameSceneInitializer : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<GameObject> multiplayerPlayerPrefabs;
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
        player = Instantiate(playerPrefab, Game.LocationLoader.GetSpawnPoint(), Quaternion.identity);
        SetMainCharacter(player);
        Game.SavingService.RestoreLocation(Game.LocationLoader.LoadedLocationModel);
    }

    public void SetMainCharacter(GameObject playerCharacter)
    {
        Game.CameraControlService.SetObjectToFollow(playerCharacter);

        ActionManager playerActionManager = playerCharacter.GetComponent<ActionManager>();
        ControlManager playerControlManager = playerCharacter.GetComponent<ControlManager>();

        Game.MobileControlService.SetControl(playerControlManager, playerActionManager);
        Game.DesktopControlService.SetControl(playerControlManager, playerActionManager);
        Game.CameraControlService.SetObjectToFollow(playerCharacter);
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
