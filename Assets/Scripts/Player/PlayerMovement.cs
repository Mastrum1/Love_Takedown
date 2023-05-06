using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public float moveSpeed { get; set; } = 3f;

    PlayerInput input;

    //Variables to store input from the player
    Vector2 currentMovement; // Movement input from the player
    bool movementPressed = false; // Is the player pressing a movement key


	void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        
        input.CharacterControls.Movement.canceled += ctx =>
        {
            currentMovement = Vector2.zero;
            movementPressed = false;
        };
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }
    private void Update()
    {
        if (movementPressed)
        {
            Vector3 PlayerForward = GameObject.FindGameObjectWithTag("Player").transform.forward;
            Vector3 PlayerRight = GameObject.FindGameObjectWithTag("Player").transform.right;
            Vector3 forwardRelative = PlayerForward * currentMovement.y;
            Vector3 rightRelative = PlayerRight * currentMovement.x;
            Vector3 moveDir = forwardRelative + rightRelative;
            transform.position += Time.deltaTime * moveSpeed * moveDir;
        }
    }
}