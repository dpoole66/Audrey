using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class RigidAudrey : MonoBehaviour {

    Animator m_Anim;
    Rigidbody m_Rigid;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    Vector3 destination, destinationAdj;
    Vector3 worldDeltaPosition;
    float distance;

    void Awake() {

        m_Anim = GetComponent<Animator>();
        m_Rigid = GetComponent<Rigidbody>();
        m_Rigid.freezeRotation = true;
        m_Rigid.useGravity = false;

    }

    void Update() {

        RaycastHit hitInfo = new RaycastHit();

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                destination = hitInfo.point;
        }
        worldDeltaPosition = destination - this.transform.position;
        distance = Vector3.Distance(worldDeltaPosition, destination);

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            m_Rigid.velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = m_Rigid.velocity.magnitude > 0.5f && distance > 0.5f;

        // Update m_Animation parameters
        m_Anim.SetBool("move", shouldMove);
        m_Anim.SetFloat("velx", velocity.x);
        m_Anim.SetFloat("vely", velocity.y);

        //Look At script component
        GetComponent<LookAtAudrey>().lookAtTargetPosition = destination + transform.forward;
        LookAtAudrey lookAt = GetComponent<LookAtAudrey>();
        if (lookAt)
            lookAt.lookAtTargetPosition = destination + transform.forward;

        //////Character movement tuning
        ////// Pull character towards agent
        ////if (worldDeltaPosition.magnitude > agent.radius)
        ////    transform.position = agent.nextPosition - 0.9f * worldDeltaPosition;

        //// Pull agent towards character
        //if (worldDeltaPosition.magnitude > 0.05f)
        //    destination = transform.position + 0.9f * worldDeltaPosition;
        //    destinationAdj = Vector3.Lerp(transform.position, destination, distance);
        //    m_Rigid.MovePosition(destinationAdj + transform.forward * Time.deltaTime);
    }

    private void FixedUpdate() {

        if (worldDeltaPosition.magnitude > 0.05f)
            destination = transform.position + 0.9f * worldDeltaPosition;
            destinationAdj = Vector3.Lerp(transform.position, destination, distance);
            m_Rigid.MovePosition(destinationAdj * Time.deltaTime);

    }

    void OnAnimatorMove() {
        // Update position based on m_Animation movement using navigation surface height
        Vector3 position = m_Anim.rootPosition;
        position.y = destination.y;
        transform.position = position;
    }
}