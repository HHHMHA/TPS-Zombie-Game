using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header( "Player Movement" )] [SerializeField] private float playerSpeed = 1.9f;

    [Header( "Player Script Cameras" )] [SerializeField]
    private Transform playerCamera;
    
    [Header( "Player Animator and Gravity" )] [SerializeField]
    private CharacterController controller;

    [Header( "Player Jumping and velocity" )] [SerializeField]
    private float turnCalmTime = 0.1f;

    private float turnCalmVelocity;

    private void Update() {
        PlayerMove();
    }

    private void PlayerMove() {
        float horizontalAxis = Input.GetAxisRaw( "Horizontal" );
        float verticalAxis = Input.GetAxisRaw( "Vertical" );

        Vector3 direction = new Vector3( horizontalAxis, 0f, verticalAxis ).normalized;

        if ( direction.magnitude >= 0.1f ) {
            float targetAngle = Mathf.Atan2( direction.x, direction.z ) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle( transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime );
            transform.rotation = Quaternion.Euler( 0f, angle, 0f );

            Vector3 moveDirection = Quaternion.Euler( 0f, targetAngle, 0f ) * Vector3.forward;
            controller.Move( moveDirection.normalized * (playerSpeed * Time.deltaTime) );
        }
    }
}
