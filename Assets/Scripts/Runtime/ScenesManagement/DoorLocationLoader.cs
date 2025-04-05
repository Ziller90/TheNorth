using UnityEngine;

public class DoorLocationLoader : MonoBehaviour
{
    [SerializeField] LocationModel targetLocation;
    [SerializeField] int targetSpawnPoint;

    public void OpenDoor(GameObject opener)
    {
        Game.SceneLauncherService.LoadGameSceneWithLocation(targetLocation.gameObject, targetSpawnPoint);
    }
}
