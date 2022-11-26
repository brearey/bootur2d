using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    private BoxCollider weapon;

    public float gravity = 9.8f * 2f;
    private float jumpForce = 8f;

    private bool isLeftHalf;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        weapon = gameObject.GetComponentInChildren<BoxCollider>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            // https://docs.unity3d.com/ru/530/Manual/MobileInput.html
            if (Input.touchCount > 0)
            {
                if (Input.touchCount == 1)
                {
                    isLeftHalf = Input.GetTouch(0).position.x < Screen.width / 2;
                    if (Input.GetTouch(0).phase == TouchPhase.Ended && isLeftHalf)
                    {
                        direction = Vector3.up * jumpForce;
                    }
                }
            }
            if (Input.GetButton("Jump"))
            {
               direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Obstacle")) {
            Destroy(other.gameObject);
            GameManager.Instance.GameOver();
        } else if (other.CompareTag("question"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.GamePause();
            GameManager.Instance.ShowQuestion();
        } else if (other.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            // TODO open next level
            //SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
        }

    }
}
