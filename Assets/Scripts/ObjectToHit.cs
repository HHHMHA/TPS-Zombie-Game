using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour {
    [SerializeField] private float health = 30f;

    public void Damage( float amount ) {
        health -= amount;
        if ( health <= 0 ) {
            Destroy( gameObject );
        }
    }
}
