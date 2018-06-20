using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoverAudrey : MonoBehaviour {
    RaycastHit hitInfo = new RaycastHit();
    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);      //tag is "Floor"
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo) && hitInfo.transform.tag == "Floor") ;
                agent.destination = hitInfo.point;
        }
    }
}
