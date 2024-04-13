using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] GameObject handle;
    [SerializeField] float maxMagnitude;

    GameObject handleBackground;
    Vector2 localPoint;
    Vector3 direction;
    float cameraAngle;
    float horizontal;
    float vertical;

    public Vector3 Direction => direction;

    void Start()
    {
        handle = gameObject;
        handleBackground = handle.transform.parent.gameObject;
    }

    void Update()
    {
        cameraAngle = Game.CameraControlService.CameraYRotation;

        vertical = handle.transform.localPosition.y / 100;
        horizontal = (handle.transform.localPosition.x / 100);
        direction = ModelUtils.GetDirection(horizontal, vertical, cameraAngle);
    } 

    public void OnPointerUp(PointerEventData eventData)
    {
        handle.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(handleBackground.GetComponent<Image>().rectTransform,
            eventData.position, eventData.pressEventCamera, out localPoint);
        handle.transform.localPosition = Vector2.ClampMagnitude(localPoint, maxMagnitude);
    }

    public void OnPointerDown(PointerEventData eventData) { }
}
