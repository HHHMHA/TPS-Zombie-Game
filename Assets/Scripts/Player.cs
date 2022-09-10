using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header( "Player Movement" )] [SerializeField]
    private float playerSpeed = 1.9f;

    [Header( "Player Script Cameras" )] [SerializeField]
    private Transform playerCamera;

    [Header( "Player Animator and Gravity" )] [SerializeField]
    private CharacterController controller;

    [SerializeField] private float gravity = -9.81f;

    [Header( "Player Jumping and velocity" )] [SerializeField]
    private float turnCalmTime = 0.1f;

    [SerializeField] private float jumpRange = 1f;
    [SerializeField] private Transform surfaceCheck;
    [SerializeField] private float surfaceDistance = 0.4f;
    [SerializeField] private LayerMask surfaceMask;

    private float turnCalmVelocity;
    private Vector3 velocity;

    private void Update() {
        ApplyGravity();
        PlayerMove();
    }
    
    private void ApplyGravity() {
        if ( onSurface && onGround ) {
            velocity.y = -2f;
        }

        velocity.y += gravity;
        controller.Move( velocity * Time.deltaTime );
    }

    private bool onSurface => Physics.CheckSphere( surfaceCheck.position, surfaceDistance, surfaceMask );
    private bool onGround => velocity.y < 0;

    private void PlayerMove() {
        float horizontalAxis = Input.GetAxisRaw( "Horizontal" );
        float verticalAxis = Input.GetAxisRaw( "Vertical" );

        Vector3 direction = new Vector3( horizontalAxis, 0f, verticalAxis ).normalized;

        if ( direction.magnitude >= 0.1f ) {
            float targetAngle = Mathf.Atan2( direction.x, direction.z ) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle( transform.eulerAngles.y, targetAngle, ref turnCalmVelocity,
                turnCalmTime );
            transform.rotation = Quaternion.Euler( 0f, angle, 0f );

            Vector3 moveDirection = Quaternion.Euler( 0f, targetAngle, 0f ) * Vector3.forward;
            controller.Move( moveDirection.normalized * ( playerSpeed * Time.deltaTime ) );
        }
    }
}
