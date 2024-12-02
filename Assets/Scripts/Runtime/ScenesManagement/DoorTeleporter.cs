using UnityEngine;

public class DoorTeleporter : MonoBehaviour
{
    [SerializeField] Transform teleportPosition;
    [SerializeField] DoorTeleporter linkedDoor;
    [SerializeField] LightingPreset targetLocationLightPreset;
    public Transform TeleportPosition => teleportPosition;

    public void OpenDoor(GameObject opener)
    {
        opener.transform.position = linkedDoor.TeleportPosition.position;
        Game.LightingManager.SetLightingPreset(targetLocationLightPreset);
    }
}
