using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 3f ;
    private float moveSpeed;
    [SerializeField] float sprintDuration = 1;
    [SerializeField] float reloadDuration = 3;
    public bool sprintCharged = true;

    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed = false;
    bool runPressed = false;

	void Awake()
    {
        input = new PlayerInput();
        moveSpeed = baseMoveSpeed;
        animator = GameObject.Find("madame").GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

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
                runPressed = true;
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
        runPressed = false;
        moveSpeed -= speed;
        yield return new WaitForSeconds(reloadDuration);
        sprintCharged = true;
    }

    void handleMovement()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (movementPressed && !isWalking)
            animator.SetBool(isWalkingHash, true);

        if (!movementPressed && isWalking)
            animator.SetBool(isWalkingHash, false);

        if ((movementPressed && runPressed) && !isRunning)
            animator.SetBool(isRunningHash, true);

        if ((!movementPressed || !runPressed ) && isRunning)
            animator.SetBool(isRunningHash, false);
    }


    void handleRotation()
    {
        Vector3 currentPosition = GameObject.Find("madame").transform.position; 
        Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);
        Vector3 positionToLookAt = currentPosition + newPosition;
        GameObject.Find("madame").transform.LookAt(positionToLookAt);
    }
    
    private void Update()
    {
        handleMovement();
        handleRotation();
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

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}