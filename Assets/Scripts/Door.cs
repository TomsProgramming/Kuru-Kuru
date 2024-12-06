using UnityEngine;

public class Door : MonoBehaviour
{
    private enum DoorStatus
    {
        close,
        open
    }

    [SerializeField] private DoorStatus status;

    [SerializeField] private Vector2 defaultPosition;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private Vector2 movePosition;


    [SerializeField] private float speed;

    void Awake()
    {
        status = DoorStatus.close;
        defaultPosition = transform.position;
        movePosition = transform.position;
    }

    void Update()
    {
        if (status == DoorStatus.open || status == DoorStatus.close)
        {
            transform.position = Vector2.Lerp(transform.position, movePosition, Time.deltaTime * speed);
        }
    }

    public void OpenOrClose()
    {
        if (status == DoorStatus.close)
        {
            movePosition = targetPosition;
            status = DoorStatus.open;
        }
        else if (status == DoorStatus.open)
        {
            movePosition = defaultPosition;
            status = DoorStatus.close;
        }

    }
}
