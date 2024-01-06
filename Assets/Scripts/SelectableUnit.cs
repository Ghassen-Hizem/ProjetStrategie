using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] private SpriteRenderer SelectionSprite;

    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;
    [HideInInspector] public bool attack = false;
    [HideInInspector] public bool capacity = false;

    private float attackElapsedtime = 0;
    private float capacityElapsedtime = 0;

    //jai defini des lifepoints dans ce script car la valeur des lifepoints depend de chaque instance d'unité
    //jai utilisé plusieurs variables de lifepoints pour tout les types d'unité car je dois les initialiser dans le start (donc il faut qu'elles soient differentes)
    [HideInInspector] public int MagicienlifePoints;
    [HideInInspector] public int CavalierlifePoints;


    //au debut du jeu, le chargement d'attaque et de capacité sont vides
    //si on veut qu'ils soient "rechargés" dès le debut, il faut initialiser le attackElaspsedTime avec la attackPeriod, mais chaque unité a une valeur differente, il faut donc 6 variables

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
        if (attack)
        {
            attackElapsedtime = 0;
        }
        else
        {
            attackElapsedtime += Time.deltaTime;
        }

        if (capacity)
        {
            capacityElapsedtime = 0;
        }
        else
        {
            capacityElapsedtime += Time.deltaTime;
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

    public void Attack()
    {
        
        if (Agent.CompareTag("Magicien"))
        {           
            if (attackElapsedtime >= controlledMagicien.attackPeriod)
            {
                controlledMagicien.Attack(this);
                attackElapsedtime = 0;
                attack = false;
            }
            
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.Attack(this);
            attack = false;
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

        if (Agent.CompareTag("Magicien"))
        {
            if (capacityElapsedtime >= controlledMagicien.capacityPeriod)
            {
                controlledMagicien.UseCapacity(this);
                capacityElapsedtime = 0;
                capacity = false;
            }

        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.UseCapacity(this);
            capacity = false;
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
}
