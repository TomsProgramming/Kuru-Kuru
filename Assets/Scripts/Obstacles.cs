using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private Vector2 defaultPosition;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private Vector2 movePosition;

    private string Direction = "default";

    [SerializeField] private float speed;

    [SerializeField] private float timer;
    [SerializeField] private float targetTimer;
    void Awake()
    {
        if(targetPosition.x == 0){ targetPosition.x = transform.position.x; }
        if (targetPosition.y == 0) { targetPosition.y = transform.position.y; }

        defaultPosition = transform.position;
        UpdateDirection();
    }

    void Update()
    {
        if(timer >= targetTimer)
        {
            timer = 0;
            UpdateDirection();
        }
        else
        {
            timer += Time.deltaTime;
        }

        transform.position = Vector2.Lerp(transform.position, movePosition, Time.deltaTime * speed);
    }

    private void UpdateDirection()
    {
        if(Direction == "default")
        {
            Direction = "target";
            movePosition = targetPosition;
        }
        else
        {
            Direction = "default";
            movePosition = defaultPosition;
        }
    }
}
