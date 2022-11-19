using System;
using UnityEngine;

public class Rifle : MonoBehaviour {
    [Header( "Rifle Things" )] [SerializeField]
    private Camera camera;
    [SerializeField] private float giveDamageOf = 10f;
    [SerializeField] private float shootingRange = 100f;

    private void Update() {
        if ( Input.GetButtonDown( "Fire1" ) ) {
            Shoot();
        }
    }

    private void Shoot() {
        RaycastHit hit;

        var transform1 = camera.transform;
        if ( Physics.Raycast( transform1.position, transform1.forward, out hit, shootingRange ) ) {
            
        }
    }
}
