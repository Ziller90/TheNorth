using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] GameObject handle;
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    [SerializeField] ControlManager controlManager;
    [SerializeField] CameraFollowing camera;
    [SerializeField] float maxMagnitude;
    [SerializeField] float modificatorToWalk; // Minimal speed modificator value when character starts walking
    [SerializeField] float modificatorToRun; // Minimal speed modificator value when character starts runing

    GameObject handleBackground;
    Vector2 localPoint;
    Vector3 direction;
    float joystickMagnitude;
    float cameraAngleCorrector;
    void Start()
    {
        handle = gameObject;
        handleBackground = handle.transform.parent.gameObject;
    }
    public void SetControlManager(ControlManager controlManager)
    {
        this.controlManager = controlManager;
    }

    void Update()
    {
        cameraAngleCorrector = camera.CameraYRotation;

        vertical = handle.transform.localPosition.y / 100;
        horizontal = (handle.transform.localPosition.x / 100);
        direction = Utils.GetDirection(horizontal, vertical, cameraAngleCorrector);
        joystickMagnitude = direction.magnitude;

        if (joystickMagnitude > modificatorToWalk && joystickMagnitude < modificatorToRun)
        {
            controlManager.SetControl(direction, MovingMode.Walk);
        }
        else if (joystickMagnitude > modificatorToRun)
        {
            controlManager.SetControl(direction, MovingMode.Run);
        }
        else
        {
            controlManager.SetControl(direction, MovingMode.Stand);
        }
    } 
    public void OnPointerUp(PointerEventData eventData)
    {
        handle.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void OnPointerDown(PointerEventData eventData) { }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(handleBackground.GetComponent<Image>().rectTransform,
            eventData.position, eventData.pressEventCamera, out localPoint);
        handle.transform.localPosition = Vector2.ClampMagnitude(localPoint, maxMagnitude);
    }
}
