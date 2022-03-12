using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class FixedJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    public GameObject handle;
    public float horizontal;
    public float vertical;
    public bool isDraged;
    public float joystickMagnitude;
    public Vector3 direction;
    public float maxMagnitude;
    public float cameraAngleCorrector;

    public ControlManager controlManager;
    public CameraFollowing camera;

    GameObject handleBackground;
    Vector2 touchPosition;
    Vector2 localPoint;

    void Start()
    {
        handle = gameObject;
        handleBackground = handle.transform.parent.gameObject;
    }

    void Update()
    {
        cameraAngleCorrector = camera.cameraYRotation;

        vertical = handle.transform.localPosition.y / 100;
        horizontal = (handle.transform.localPosition.x / 100);
        direction = Utils.GetDirection(horizontal, vertical, cameraAngleCorrector);
        joystickMagnitude = direction.magnitude;
        controlManager.SetControl(direction, joystickMagnitude);
    } 
    public void OnPointerUp(PointerEventData eventData)
    {
        handle.transform.localPosition = new Vector3(0, 0, 0);
        isDraged = false;
    }
    public void OnPointerDown(PointerEventData eventData) { }
    public void OnDrag(PointerEventData eventData)
    {
        isDraged = true;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(handleBackground.GetComponent<Image>().rectTransform,
            eventData.position, eventData.pressEventCamera, out localPoint);
        handle.transform.localPosition = Vector2.ClampMagnitude(localPoint, maxMagnitude);
    }
}
