using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header( "Player Movement" )] [SerializeField] private float playerSpeed = 1.9f;
    [Header( "Player Animator and Gravity" )] [SerializeField]
    private CharacterController controller;

    private void Update() {
        PlayerMove();
    }

    private void PlayerMove() {
        float horizontalAxis = Input.GetAxisRaw( "Horizontal" );
        float verticalAxis = Input.GetAxisRaw( "Vertical" );

        Vector3 direction = new Vector3( horizontalAxis, 0f, verticalAxis ).normalized;

        if ( direction.magnitude >= 0.1f ) {
            float targetAngle = Mathf.Atan2( direction.x, direction.z ) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler( 0f, targetAngle, 0f );
            controller.Move( direction.normalized * (playerSpeed * Time.deltaTime) );
        }
    }
}
