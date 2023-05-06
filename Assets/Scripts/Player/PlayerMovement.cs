using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float baseMoveSpeed { get; set; } = 3f ;
    private float moveSpeed;
    [SerializeField] float sprintDuration = 1;
    [SerializeField] float reloadDuration = 3;
    public bool sprintCharged { get; set; } = true;


    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed = false;

	void Awake()
    {
        input = new PlayerInput();
        moveSpeed = baseMoveSpeed;

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

        input.CharacterControls.Sprint.performed += ctx =>
        {
            if (sprintCharged)
            {
                float boost = baseMoveSpeed * 1.2f;
                sprintCharged = false;
                StartCoroutine(Sprint(boost, sprintDuration, reloadDuration));
            }
        };    
    }

    IEnumerator Sprint(float speed, float sprintDuration, float reloadDuration)
    {
        moveSpeed += speed;
        yield return new WaitForSeconds(sprintDuration);
        moveSpeed -= speed;
        yield return new WaitForSeconds(reloadDuration);
        sprintCharged = true;
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