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
    public bool attack = false;

    public float elapsedtime = 0;

    private void Update()
    {
        if (attack)
        {
            elapsedtime = 0;
        }
        else
        {
            elapsedtime += Time.deltaTime;
        }
        
    }
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();

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
            /*
            while (elapsedtime < controlledMagicien.attackPeriod)
            {
                elapsedtime += Time.deltaTime;
                print(elapsedtime);
            }*/
            
            if (elapsedtime >= controlledMagicien.attackPeriod)
            {
                controlledMagicien.Attack(this);
                elapsedtime = 0;
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
