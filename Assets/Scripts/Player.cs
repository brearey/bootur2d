using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float gravity = 9.8f * 2f;
    private float jumpForce = 8f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
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
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
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
            //GameManager.Instance.GameOver();
            GameManager.Instance.GamePause();
        }
    }
}
