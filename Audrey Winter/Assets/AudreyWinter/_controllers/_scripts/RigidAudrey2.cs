using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]


public class RigidAudrey2 : MonoBehaviour {

    public float Speed = 1.0f;   
    private Rigidbody _body;          
    private Animator _anim;
    private Vector3 destination = Vector3.zero;

    void Start() {
        _body = GetComponent<Rigidbody>();       
        _anim = GetComponent<Animator>();
    }

    void Update() {


        RaycastHit hitInfo = new RaycastHit();

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                destination = hitInfo.point;
        }

    }


    void FixedUpdate() {

        Quaternion deltaRotation = Quaternion.Euler(destination * Time.deltaTime);
        _body.MoveRotation(_body.rotation * deltaRotation);
        _body.MovePosition(destination * Speed * Time.fixedDeltaTime);
    }

}
