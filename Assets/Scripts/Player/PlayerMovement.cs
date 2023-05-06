using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed { get; set; } = 3f ;
    [SerializeField] float sprintDuration = 1;
    [SerializeField] float reloadDuration = 3;
    bool sprintCharged = true;


    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed = false;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GameObject.FindGameObjectWithTag("Player").transform.position, 10);
    }
#endif


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

        input.CharacterControls.Sprint.performed += ctx =>
        {
            if (sprintCharged)
            {
                float boost = moveSpeed * 1.5f;
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