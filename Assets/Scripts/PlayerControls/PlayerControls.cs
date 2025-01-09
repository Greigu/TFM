using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool isActiveMove = true;

    public CharacterController controller;

    public float speed = 8f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private bool isPaused;
    private GameController gameController;

    private AudioSource audio;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        isPaused = gameController.GetIsPaused();
        if (!isPaused )
        {
            Controls();
        }
    }

    public void SetIsActiveMove(bool a)
    {
        isActiveMove = a;
    }

    private void Controls()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement
        if (isActiveMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if((x != 0 || z!= 0) && !audio.isPlaying)
            {
                audio.Play();
            }
            else{
                audio.Pause();
            }
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            
        }


        // Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
