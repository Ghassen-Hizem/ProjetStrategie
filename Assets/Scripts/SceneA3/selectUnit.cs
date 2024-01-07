using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class selectUnit : MonoBehaviour
{
    private NavMeshAgent Agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;

        private string WALK_ANIMATION = "IsRunning";

            private Animator anim;

    private void Awake()
    {
        Selection.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
    if (!Agent.pathPending)
{
    if (Agent.remainingDistance <= Agent.stoppingDistance)
    {
        if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
        {
           anim = Agent.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
            anim.SetBool(WALK_ANIMATION,false);
        }
    }
}
    }

    public void MoveTo(Vector3 Position)
    {
        Agent.SetDestination(Position);
    }

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        SelectionSprite.gameObject.SetActive(false);
    }
}