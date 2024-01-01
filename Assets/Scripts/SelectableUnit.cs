using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    public NavMeshAgent Agent;
    [SerializeField] private SpriteRenderer SelectionSprite;

    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();

    }

    public void MoveTo(Vector3 Position)
    {
        if (Agent.CompareTag("Magicien"))
        {
            
            Agent.speed = controlledMagicien.speed;
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            Agent.speed = controlledCavalier.speed;
        }
        //print("Agent speed = " + Agent.speed);
        Agent.SetDestination(Position);
    }

    public void Attack()
    {
        if (Agent.CompareTag("Magicien"))
        {
            controlledMagicien.Attack(this);
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.Attack(this);
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

    public void TakeDamage(int degats)
    {
        if (Agent.CompareTag("Magicien"))
        {
            //est ce que les pdv s'enlevent chez tous les magiciens ? car j'ai modifié le scriptableObject (je crois qu'il faut definir les pdv dans ce script. mais alors quelle est l'utilité des scriptable objects, ou alors il faut instancier un scriptable object avec chaque objet)
            controlledMagicien.TakeDamage(degats);
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.TakeDamage(degats) ;
        }
    }
}
