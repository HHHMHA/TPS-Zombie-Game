using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {
    [Header( "Camera to Assign" )] 
    [SerializeField] private GameObject aimCam;
    [SerializeField] private GameObject aimCanvas;
    [SerializeField] private GameObject thirdPersonCamera;
    [SerializeField] private GameObject thirdPersonCanvas;

    private void Update() {
        if ( Input.GetButton( "Fire2" ) ) {
            thirdPersonCamera.SetActive( false );
            thirdPersonCanvas.SetActive( false );
            aimCam.SetActive( true );
            aimCanvas.SetActive( true );
            return;
        }
        thirdPersonCamera.SetActive( true );
        thirdPersonCanvas.SetActive( true );
        aimCam.SetActive( false );
        aimCanvas.SetActive( false );
    }
}
