using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header( "Player Movement" )] [SerializeField]
    private float playerSpeed = 1.9f;

    [SerializeField] private float playerSprintBoost = 3f;

    [Header( "Player Script Cameras" )] [SerializeField]
    private Transform playerCamera;

    [Header( "Player Animator and Gravity" )] [SerializeField]
    private CharacterController controller;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Animator animator;

    [Header( "Player Jumping and velocity" )] [SerializeField]
    private float turnCalmTime = 0.1f;

    [SerializeField] private float jumpRange = 1f;
    [SerializeField] private Transform surfaceCheck;
    [SerializeField] private float surfaceDistance = 0.4f;
    [SerializeField] private LayerMask surfaceMask;
    [SerializeField] private Vector3 velocity;


    private float turnCalmVelocity;
    private static readonly int Idle = Animator.StringToHash( "Idle" );
    private static readonly int Walk = Animator.StringToHash( "Walk" );
    private static readonly int Running = Animator.StringToHash( "Running" );
    private static readonly int RifleWalk = Animator.StringToHash( "RifleWalk" );
    private static readonly int IdleAim = Animator.StringToHash( "IdleAim" );
    private static readonly int Jump = Animator.StringToHash( "Jump" );

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        TryJump();
        ApplyGravity();
        PlayerMove();
    }
    
    private void ApplyGravity() {
        if ( onSurface && onGround ) {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move( velocity * Time.deltaTime );
    }

    private bool onSurface => Physics.CheckSphere( surfaceCheck.position, surfaceDistance, surfaceMask );
    private bool onGround => velocity.y < 0;
    
    private float currentSpeed => isSprinting ? playerSpeed + playerSprintBoost : playerSpeed;

    private void PlayerMove() {
        float horizontalAxis = Input.GetAxisRaw( "Horizontal" );
        float verticalAxis = Input.GetAxisRaw( "Vertical" );

        Vector3 direction = new Vector3( horizontalAxis, 0f, verticalAxis ).normalized;

        if ( direction.magnitude >= 0.1f ) {
            if ( onSurface ) {
                animator.SetBool( Idle, false );
                animator.SetBool( Walk, true );
                animator.SetBool( Running, isSprinting );
                animator.SetBool( RifleWalk, false );
                animator.SetBool( IdleAim, false );
            }
            
            float targetAngle = Mathf.Atan2( direction.x, direction.z ) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle( transform.eulerAngles.y, targetAngle, ref turnCalmVelocity,
                turnCalmTime );
            transform.rotation = Quaternion.Euler( 0f, angle, 0f );

            Vector3 moveDirection = Quaternion.Euler( 0f, targetAngle, 0f ) * Vector3.forward;
            float speed = currentSpeed;
            controller.Move( moveDirection.normalized * ( currentSpeed * Time.deltaTime ) );
            return;
        }
        
        animator.SetBool( Idle, true );
        animator.SetBool( Walk, false );
        animator.SetBool( Running, false );
    }

    private void TryJump() {
        if ( Input.GetButtonDown( "Jump" ) && onSurface ) {
            animator.SetBool( Idle, false );
            animator.SetBool( Walk, false );
            animator.SetBool( Running, false );
            animator.SetTrigger( Jump );
            velocity.y = Mathf.Sqrt( jumpRange * -2 * gravity );
            return;
        }
        // animator.SetBool( Idle, true );
    }

    private bool isSprinting => Input.GetButton( "Sprint" ) && onSurface;
}
