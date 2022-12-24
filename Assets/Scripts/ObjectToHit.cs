using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour {
    [SerializeField] private float health = 30f;
    [SerializeField] private GameObject hitEffect;

    public void Damage( float amount, Vector3 hitPoint, Quaternion hitRotation ) {
        health -= amount;
        var effect = Instantiate( hitEffect, hitPoint, hitRotation );
        Destroy( effect, 1f );
        if ( health <= 0 ) {
            Destroy( gameObject );
        }
    }
}
