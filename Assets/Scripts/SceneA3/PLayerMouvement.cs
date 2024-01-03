using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PLayerMouvement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    private string WALK_ANIMATION = "IsRunning";
    private Animator anim;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            print("button is clicked");
            anim.SetBool(WALK_ANIMATION,true);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                // move the Agent
                agent.SetDestination(hit.point);

            }
        }
    }
}
