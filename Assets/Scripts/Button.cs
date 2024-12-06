using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private bool isPressed = false;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite buttonUp;
    [SerializeField] private Sprite buttonDown;

    [SerializeField] private float timer;
    [SerializeField] private float timerTarget;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPressed == false && collision.gameObject.tag == "Player")
        {
            door.GetComponent<Door>().OpenOrClose();
            isPressed = true;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        if(isPressed)
        {
            spriteRenderer.sprite = buttonDown;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            spriteRenderer.sprite = buttonUp;
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void Update()
    {
        if (isPressed)
        {
            timer += Time.deltaTime;
            if (timer >= timerTarget)
            {
                isPressed = false;
                UpdateSprite();
                door.GetComponent<Door>().OpenOrClose();
                timer = 0;  
            }
        }
    }
}
