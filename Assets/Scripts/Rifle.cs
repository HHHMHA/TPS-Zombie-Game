using System;
using UnityEngine;

public class Rifle : MonoBehaviour {
    [Header( "Rifle Things" )] [SerializeField]
    private Camera camera;
    [SerializeField] private float giveDamageOf = 10f;
    [SerializeField] private float shootingRange = 100f;
    [SerializeField] private float fireCharge = 15;
    private float nextTimeToShoot = 0f;

    [Header( "Rifle Effects" )] [SerializeField]
    private ParticleSystem muzzleSpark;

    private void Update() {
        if ( !Input.GetButton( "Fire1" ) || !( Time.time >= nextTimeToShoot ) ) return;
        nextTimeToShoot = Time.time + 1f / fireCharge;
        Shoot();
    }

    private void Shoot() {
        muzzleSpark.Play();
        RaycastHit hit;

        var transform1 = camera.transform;
        if ( Physics.Raycast( transform1.position, transform1.forward, out hit, shootingRange ) ) {
            ObjectToHit objectToHit = hit.transform.GetComponent<ObjectToHit>();
            if (!objectToHit) return;
            objectToHit.Damage( giveDamageOf, hit.point, Quaternion.LookRotation( hit.normal ) );
        }
    }
}
