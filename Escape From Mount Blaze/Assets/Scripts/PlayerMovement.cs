using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; 
    public float runSpeed = 10f; 
    public float rotateSpeed = 100f; 
    public float jumpForce = 8f; 
    public Transform cameraTransform; 
    public KeyCode switchViewKey = KeyCode.V; 
    public KeyCode runKey = KeyCode.LeftShift; 

    private CharacterController controller;
    private bool isRunning = false;
    private bool isFirstPerson = false;
    private float verticalSpeed = 0f;
    private float gravity = 20f;
    private Animation animationComponent;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animationComponent = GetComponent<Animation>(); 
    }

    void Update()
    {
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * vertical;
        moveDirection *= moveSpeed;

        transform.Rotate(Vector3.up * horizontal * rotateSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            verticalSpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpForce;
                PlayAnimation("Jump"); 
            }
            else if (moveDirection.magnitude > 0)
            {
                PlayAnimation(isRunning ? "Run" : "Walk");
            }
            else
            {
                PlayAnimation("idle"); 
            }
        }

        verticalSpeed -= gravity * Time.deltaTime;
        moveDirection.y = verticalSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(switchViewKey))
        {
            isFirstPerson = !isFirstPerson;
            cameraTransform.gameObject.SetActive(!isFirstPerson);
            Camera.main.gameObject.SetActive(isFirstPerson);
        }
        isRunning = Input.GetKey(runKey);
    }

    void PlayAnimation(string animationName)
    {
        if (!animationComponent.IsPlaying(animationName))
        {
            animationComponent.Play(animationName);
        }
    }

    public bool Once = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava") && !Once)
        {
            
            Debug.Log(other.gameObject.name);

            Once = true;
            other.gameObject.tag = "Untagged";
            Time.timeScale = 0;
            FindObjectOfType<UIManagerGamePlay>().LevelFailed.SetActive(true);
            Debug.Log("Game Failed");
        }
    }
}
