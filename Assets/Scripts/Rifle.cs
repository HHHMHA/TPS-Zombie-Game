using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class Rifle : MonoBehaviour {
    [Header( "Rifle Things" )] [SerializeField]
    private Camera camera;
    [SerializeField] private float giveDamageOf = 10f;
    [SerializeField] private float shootingRange = 100f;
    [SerializeField] private float fireCharge = 15;
    private float nextTimeToShoot = 0f;
    public Player player;

    [Header( "Rifle Ammunition and shooting" )]
    [SerializeField] private int maximumAmmunition = 32;
    [SerializeField] private int mag = 10;
    [SerializeField] private int presentAmmunition;
    [SerializeField] private float reloadingTime = 1.3f;
    private bool setReloading = false;
    
    
    [Header( "Rifle Effects" )] [SerializeField]
    private ParticleSystem muzzleSpark;


    private void Awake() {
        presentAmmunition = maximumAmmunition;
        
    }
    private void Update() {
        if (setReloading)
            return;

        if ( presentAmmunition <= 0 ) {
            StartCoroutine( Reload() );
            return;
        }
        
        if ( !Input.GetButton( "Fire1" ) || !( Time.time >= nextTimeToShoot ) ) return;
        nextTimeToShoot = Time.time + 1f / fireCharge;
        Shoot();
    }

    private void Shoot() {
        // check for mag
        if ( mag == 0 ) {
            // show ammo out text
            return;
        }

        presentAmmunition--;

        if ( presentAmmunition == 0 ) {
            mag--;
        }
        // updating the UI
        
        
        muzzleSpark.Play();
        RaycastHit hit;

        var transform1 = camera.transform;
        if ( Physics.Raycast( transform1.position, transform1.forward, out hit, shootingRange ) ) {
            ObjectToHit objectToHit = hit.transform.GetComponent<ObjectToHit>();
            if (!objectToHit) return;
            objectToHit.Damage( giveDamageOf, hit.point, Quaternion.LookRotation( hit.normal ) );
        }
    }

    IEnumerator Reload() {
        setReloading = true;
        Debug.Log( "Reloading ..." );
        // play anim
        // play reload sound
        yield return new WaitForSeconds( reloadingTime );
        // play anim
        presentAmmunition = maximumAmmunition;
        setReloading = false;

    }
    
}
