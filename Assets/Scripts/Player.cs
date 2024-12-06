using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_Rigidbody;

    [SerializeField] private Camera m_Camera;

    [SerializeField] private Vector3 startPosition;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float bounchSpeed;
    [SerializeField] private float spinningSpeed;

    [SerializeField] private int health = 3;
    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite fullHearth;
    [SerializeField] private Sprite emptyhHearth;

    [SerializeField] private bool isInFinish = false;
    [SerializeField] private Vector3 finishPosition;
    [SerializeField] private float speedToFinish;
    [SerializeField] private float rotationSpeedFinish;
    private void Start()
    {
        startPosition = transform.position;

        m_Camera.transform.position = new Vector3(transform.position.x, transform.position.y, m_Camera.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            m_Rigidbody.AddForce(new Vector2(moveSpeed, 0));
        }

        if(Input.GetAxis("Horizontal") < 0)
        {
            m_Rigidbody.AddForce(new Vector2(-moveSpeed, 0));
        }

        if(Input.GetAxis("Vertical") > 0)
        {
            m_Rigidbody.AddForce(new Vector2(0, moveSpeed));
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            m_Rigidbody.AddForce(new Vector2(0, -moveSpeed));
        }

        if(isInFinish)
        {
            m_Rigidbody.linearVelocity = Vector3.zero;
            transform.position = Vector2.Lerp(transform.position, finishPosition, Time.deltaTime * speedToFinish);
            m_Rigidbody.rotation += rotationSpeedFinish;
        }
        else
        {
            m_Rigidbody.rotation += spinningSpeed;
        }

        m_Camera.transform.position = new Vector3(transform.position.x, transform.position.y, m_Camera.transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            Vector2 bounceDirection = collision.contacts[0].normal;
            m_Rigidbody.AddForce(bounceDirection * bounchSpeed, ForceMode2D.Impulse);

            if (health == 1)
            {
                health = 3;
                transform.position = new Vector2(startPosition.x, startPosition.y + 0.2f);
                m_Rigidbody.linearVelocity = Vector3.zero;
            }
            else
            {
                health -= 1;
            }

            updateHealth();
        }else if(collision.gameObject.tag == "Stop")
        {
            Vector2 bounceDirection = collision.contacts[0].normal;
            m_Rigidbody.linearVelocity = Vector3.zero;
            m_Rigidbody.AddForce(bounceDirection * 0.5f, ForceMode2D.Impulse);
        }
        else if(collision.gameObject.tag == "CheckPoint")
        {
            Debug.Log("Checkpoint");
            startPosition = collision.transform.position;
        }
        else if(collision.gameObject.tag == "Finish")
        {
            finishPosition = collision.transform.position;
            isInFinish = true;
        }
    }
        
    private void updateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = emptyhHearth;
        }

        for (int i = 0;i < health; i++)
        {
            hearts[i].sprite = fullHearth;
        }
    }
}
