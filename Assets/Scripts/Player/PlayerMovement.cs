using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 3f ;
    public float moveSpeed;

    public bool sprintCharged = true;
    public Adrenaline adrenalineScript;
    
    [SerializeField] float sprintDuration = 1;
    [SerializeField] float reloadDuration = 3;
    
    [SerializeField] private Animator animator;
    [SerializeField] private PhoneLogics phoneLogics;
    [SerializeField] private PlayerScript playerScript;

    private readonly int _isWalkingHash = Animator.StringToHash("isWalking");
    private readonly int _isRunningHash = Animator.StringToHash("isRunning");
    private PlayerInput _input;

    private Vector2 _currentMovement;
    private bool _movementPressed;
    private bool _runPressed;

    private PlayerMovement _movement;
    private AudioController _audioController;

    private void Awake()
    {
        _input = new PlayerInput();
        moveSpeed = baseMoveSpeed;
        _audioController = GameObject.Find("AudioManager").GetComponent<AudioController>();

        _input.CharacterControls.Movement.performed += ctx =>
        {
            _currentMovement = ctx.ReadValue<Vector2>();
            _movementPressed = _currentMovement.x != 0 || _currentMovement.y != 0;
        };
        
        _input.CharacterControls.Movement.canceled += ctx =>
        {
            _currentMovement = Vector2.zero;
            _movementPressed = false;
        };

        _input.CharacterControls.Sprint.performed += ctx =>
        {
            if (sprintCharged)
            {
                _runPressed = true;
                float boost = baseMoveSpeed * 1.2f;
                sprintCharged = false;
                StartCoroutine(Sprint(boost));
            }
        };

        _input.CharacterControls.Select.performed += ctx =>
        {
            if (!phoneLogics.gameStarted)
            {
                phoneLogics.gameStarted = true;
                _audioController.PlayMusic(_audioController.Musics[2]);
                StartCoroutine(HidePhone());
            }
            else
            {
                if (adrenalineScript.adrenaline >= adrenalineScript.maxAdrenaline)
                    StartCoroutine(ActivateTakedown(baseMoveSpeed*1.2f));
            }
        };

        _input.CharacterControls.hidePhone.performed += ctx =>
        {
            GameObject.Find("Phone").GetComponent<PhoneLogics>().phoneActive = false;
        };

        _input.CharacterControls.showPhone.performed += ctx =>
        {
            GameObject.Find("Phone").GetComponent<PhoneLogics>().phoneActive = true;
        };
    }

    IEnumerator Sprint(float speed)
    {
        moveSpeed += speed;
        yield return new WaitForSeconds(sprintDuration);
        _runPressed = false;
        moveSpeed -= speed;
        yield return new WaitForSeconds(reloadDuration);
        sprintCharged = true;
    }

    void HandleMovement()
    {
        bool isWalking = animator.GetBool(_isWalkingHash);
        bool isRunning = animator.GetBool(_isRunningHash);

        if (_movementPressed && !isWalking)
            animator.SetBool(_isWalkingHash, true);

        if (!_movementPressed && isWalking)
            animator.SetBool(_isWalkingHash, false);

        if ((_movementPressed && _runPressed) && !isRunning)
            animator.SetBool(_isRunningHash, true);

        if ((!_movementPressed || !_runPressed ) && isRunning)
            animator.SetBool(_isRunningHash, false);

        if (_movementPressed && phoneLogics.gameStarted)
        {
            Vector3 moveDir = new Vector3(_currentMovement.x, 0, _currentMovement.y);
            transform.position += Time.deltaTime * moveSpeed * moveDir;
        }
    }


    private void HandleRotation()
    {
        Vector3 currentPosition = transform.position; 
        Vector3 newPosition = new Vector3(_currentMovement.x, 0, _currentMovement.y);
        Vector3 positionToLookAt = currentPosition + newPosition;
        transform.LookAt(positionToLookAt);
    }
    
    private void Update()
    {
        HandleMovement();
        HandleRotation();

    }

    void OnEnable()
    {
        _input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _input.CharacterControls.Disable();
    }

    IEnumerator HidePhone()
    {
        yield return new WaitForSeconds(2);
        phoneLogics.phoneActive = false;
    }

    IEnumerator ActivateTakedown(float speed)
    {
        _audioController.m_Effect3.loop = false;
        _audioController.PlayEffect3(_audioController.takedown);
        Debug.Log("TAKEDOWN !!!!");
        adrenalineScript.adrenaline = 0;
        playerScript.isTakedown = true;
        moveSpeed += speed;
        yield return new WaitForSeconds(5);
        playerScript.isTakedown = false;
        moveSpeed -= speed;
    }
}