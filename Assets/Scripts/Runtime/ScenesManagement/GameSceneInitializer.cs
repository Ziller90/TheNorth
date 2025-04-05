using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using SiegeUp.Core;
using System.Collections;

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
        Game.SavingService.RestoreLocation(Game.LocationLoader.LoadedLocationModel);
        InitializePlayer();
    }

    public void InitializePlayer()
    {
        var spawnPoint = Game.LocationLoader.GetSpawnPoint();

        if (Game.SavingService.SavedPlayer != null)
        {
            player = Game.SavingService.RestorePlayer();
            player.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }
        else
            player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        SetMainCharacter(player);
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
        StartCoroutine(LeaveLocationCoroutine());
    }

    public IEnumerator LeaveLocationCoroutine()
    {
        ClearDeadBodies();

        Game.SavingService.SavePlayer();
        Game.ActorsAccessModel.DestroyObject(Player);

        yield return null;

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
