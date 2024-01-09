using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class selectUnit : MonoBehaviour
{
    private NavMeshAgent Agent;
    [SerializeField]
    private SpriteRenderer SelectionSprite;
     

public bool KingModeActive = false;

    public GameObject Flag;
    public GameObject KingParticles;


            private Animator anim;

    private void Awake()
    {
        Selection.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {

    //Destination atteinte algorithme
   // if (!Agent.pathPending)
//{
   // if (Agent.remainingDistance <= Agent.stoppingDistance)
   // {
    //    if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
     //   {
           //anim = Agent.gameObject.GetComponent<Animator>();
            //anim.SetBool(WALK_ANIMATION,false);
     //   }
   // }
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

     public void KingMode()
    {
        

        KingModeActive = true;
        print("KingMode");

        Vector3 positionFlag = transform.position;
        positionFlag.y = 4;
        var flag = Instantiate(Flag, positionFlag, Flag.transform.rotation, gameObject.transform);
        flag.SetActive(true);

        var kingParticles = Instantiate(KingParticles, transform.position, KingParticles.transform.rotation, gameObject.transform);
        kingParticles.SetActive(true);

    }

}
