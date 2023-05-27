using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickup : MonoBehaviour {
    [Header( "Rifle's" )] 
    [SerializeField] private GameObject playerRifle;
    [SerializeField] private GameObject pickupRifle;
    [SerializeField]
    private PlayerPunch playerPunch;

    [Header( "Rifle Assign Things" )] 
    [SerializeField] private Player player;
    private float radius = 2.5f;
    private float nextTimeToPunch = 0f;
    private float punchCharge = 15f;
    
    void Awake() {
        playerRifle.SetActive( false );
    }

    void Update() {
        if ( Input.GetButtonDown( "Fire1" ) && Time.time >= nextTimeToPunch ) {
            nextTimeToPunch = Time.time + punchCharge;
            playerPunch.Punch();
        }
        
        if ( Vector3.Distance( transform.position, player.transform.position ) < radius ) {
            if ( Input.GetKeyDown( "F" ) ) {
                playerRifle.SetActive( true );
                pickupRifle.SetActive( false );
                // sound
                
                // objective completed
            }
        }
    }
}
