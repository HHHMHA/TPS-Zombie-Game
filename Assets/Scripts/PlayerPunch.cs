using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour {
    [Header( "Player Punch Var" )] [SerializeField]
    private Camera cam;

    [SerializeField] private float giveDamageOf = 10f;
    [SerializeField] private float punchRange = 5f;

    void Start() {
    }

    void Update() {
    }
    
    public void Punch() {
        RaycastHit hit;

        var transform1 = cam.transform;
        if ( !Physics.Raycast( transform1.position, transform1.forward, out hit, punchRange ) ) return;
        
        ObjectToHit objectToHit = hit.transform.GetComponent<ObjectToHit>();
        if (!objectToHit) return;
        objectToHit.Damage( giveDamageOf, hit.point, Quaternion.LookRotation( hit.normal ) );
    }
}
