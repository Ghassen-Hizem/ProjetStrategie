using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] private SpriteRenderer SelectionSprite;

    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;


    [HideInInspector] public float attackElapsedtime = 0;
    [HideInInspector] public float capacityElapsedtime = 0;

    //jai defini des lifepoints dans ce script car la valeur des lifepoints depend de chaque instance d'unit�
    //jai utilis� plusieurs variables de lifepoints pour tout les types d'unit� car je dois les initialiser dans le start (donc il faut qu'elles soient differentes)
    [HideInInspector] public int MagicienlifePoints;
    [HideInInspector] public int CavalierlifePoints;

    public bool KingModeActive = false;

    public GameObject Flag;
    public GameObject KingParticles;

    public Vector3 unitPosition;
    //public Vector3 enemyPosition;
    public Vector3 enemyPosition = new Vector3(0,0,0);
    public int unitDistance;
    public bool attack;
    private int attackPossibleRadius = 15;

    //au debut du jeu, le chargement d'attaque et de capacit� sont vides
    //si on veut qu'ils soient "recharg�s" d�s le debut, il faut initialiser le attackElaspsedTime avec la attackPeriod, mais chaque unit� a une valeur differente, il faut donc 6 variables

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
        MagicienlifePoints = controlledMagicien.lifePoints;
        CavalierlifePoints = controlledCavalier.lifePoints;

        //attackElapsedtime = controlledMagicien.attackPeriod;
    }

    private void Update()
    {
        attackElapsedtime += Time.deltaTime;
        capacityElapsedtime += Time.deltaTime;
        unitPosition = Agent.transform.position;
        if (enemyPosition != new Vector3(0,0,0))
        {
            unitDistance = (int)Vector3.Distance(enemyPosition, unitPosition);
            attack = unitDistance <= attackPossibleRadius;
            print(attack);
        }
        
    }
    

    public void MoveTo(Vector3 Position)
    {
        

        //on peut pas utiliser le nom car chaque instance a un nom different donc j'utilise le tag
        if (Agent.CompareTag("Magicien"))
        {
            controlledMagicien.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.MoveTo(this, Position);
        }
        
    }

    public void MoveToAttack(Vector3 Position)
    {
        //il faut faire ca que si on attaque et pas un simple moving
        //on peut faire une moveToAttack avec plus grande stopping distance
        enemyPosition = Position;
        //unitDistance = (int)Vector3.Distance(Position, unitPosition);

        //on peut pas utiliser le nom car chaque instance a un nom different donc j'utilise le tag
        if (Agent.CompareTag("Magicien"))
        {
            controlledMagicien.MoveToAttack(this, Position);
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.MoveTo(this, Position);
        }

    }



    public void Attack(scriptTestEnemy enemyUnit)
    {

        
        if (KingModeActive == false)
        {
            if (Agent.CompareTag("Magicien"))
            {
                if (attackElapsedtime >= controlledMagicien.attackPeriod)
                {
                    controlledMagicien.Attack(this, enemyUnit);

                }

            }
            else if (Agent.CompareTag("Cavalier"))
            {
                if (attackElapsedtime >= controlledCavalier.attackPeriod)
                {
                    controlledCavalier.Attack(this, enemyUnit);
                }
            }
        }
    }

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        SelectionSprite.gameObject.SetActive(false);
    }   

    public void UseCapacity()
    {

        if (KingModeActive == false)
        {
            if (Agent.CompareTag("Magicien"))
            {
                if (capacityElapsedtime >= controlledMagicien.capacityPeriod)
                {
                    controlledMagicien.UseCapacity(this);
                }

            }
            else if (Agent.CompareTag("Cavalier"))
            {
                if (capacityElapsedtime >= controlledCavalier.capacityPeriod)
                {
                    controlledCavalier.UseCapacity(this);
                }
            }
        }
        
    }
    public void TakeDamage(int degats)
    {
        if (Agent.CompareTag("Magicien"))
        {           
            controlledMagicien.TakeDamage(this, degats);
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.TakeDamage(this, degats) ;
        }
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
