using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] private SpriteRenderer SelectionSprite;

    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;
    public ControlledUnit controlledSoldat;
    public ControlledUnit controlledBouclier;
    public ControlledUnit controlledTirailleur;
    public ControlledUnit controlledSoignant;

    [HideInInspector] public float attackElapsedtime = 0;
    [HideInInspector] public float capacityElapsedtime = 0;

    //jai defini des lifepoints dans ce script car la valeur des lifepoints depend de chaque instance d'unité
    [HideInInspector] public int MagicienlifePoints;
    [HideInInspector] public int CavalierlifePoints;
    [HideInInspector] public int SoldatlifePoints;
    [HideInInspector] public int TirailleurlifePoints;
    [HideInInspector] public int BouclierlifePoints;
    [HideInInspector] public int SoignantlifePoints;

    [HideInInspector] public bool KingModeActive = false;

    public GameObject Flag;
    public GameObject KingParticles;

    [HideInInspector] public Vector3 unitPosition;
    [HideInInspector] public Vector3 enemyPosition = new Vector3(0,0,0);
    [HideInInspector] public int unitDistance;
    [HideInInspector] public bool attack;
    private int attackPossibleRadius = 10;

    //au debut du jeu, le chargement d'attaque et de capacité sont vides
    //si on veut qu'ils soient "rechargés" dès le debut, il faut initialiser le attackElaspsedTime avec la attackPeriod, mais chaque unité a une valeur differente, il faut donc 6 variables
    //attackElapsedtime = controlledMagicien.attackPeriod;

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
        MagicienlifePoints = controlledMagicien.lifePoints;
        CavalierlifePoints = controlledCavalier.lifePoints;
        SoldatlifePoints = controlledSoldat.lifePoints;
        BouclierlifePoints = controlledBouclier.lifePoints;
        TirailleurlifePoints = controlledTirailleur.lifePoints;
        SoignantlifePoints = controlledSoignant.lifePoints;
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
        else if (Agent.CompareTag("Soldat"))
        {
            controlledSoldat.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Tirailleur"))
        {
            controlledTirailleur.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Bouclier"))
        {
            controlledBouclier.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Soignant"))
        {
            controlledSoignant.MoveTo(this, Position);
        }
    }


    public IEnumerator HandleAttack(scriptTestEnemy enemyUnit)
    {
        unitDistance = (int)Vector3.Distance(enemyUnit.transform.position, unitPosition);
        attack = unitDistance <= attackPossibleRadius;
        if (!attack)
        {
            MoveToAttack(enemyUnit.transform.position);
            
            yield return new WaitUntil(() => attack == true);

            while (enemyUnit != null)
            {
                Attack(enemyUnit);
                yield return null;

                List<Collider> colliders = new List<Collider>( Physics.OverlapSphere(transform.position, attackPossibleRadius));
                List<scriptTestEnemy> enemies = new List<scriptTestEnemy>();
                foreach (Collider collider in colliders)
                {
                    if(collider.TryGetComponent<scriptTestEnemy>(out scriptTestEnemy otherEnemy))
                    {
                        enemies.Add(otherEnemy);
                    }
                }
                var sortedEnemies = enemies.OrderBy(otherEnemy => Vector3.Distance(otherEnemy.transform.position, unitPosition));
                if (sortedEnemies.Count() != 0)
                {
                    enemyUnit = sortedEnemies.FirstOrDefault();
                }
            }
        }
        else
        {
            while (enemyUnit != null)
            {
                Attack(enemyUnit);
                yield return null;

                List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(transform.position, attackPossibleRadius));
                List<scriptTestEnemy> enemies = new List<scriptTestEnemy>();
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent<scriptTestEnemy>(out scriptTestEnemy otherEnemy))
                    {
                        enemies.Add(otherEnemy);
                    }
                }
                var sortedEnemies = enemies.OrderBy(otherEnemy => Vector3.Distance(otherEnemy.transform.position, unitPosition));
                if (sortedEnemies.Count() != 0)
                {
                    enemyUnit = sortedEnemies.FirstOrDefault();
                }
            }
        }
    }


    public void MoveToAttack(Vector3 Position)
    {
        enemyPosition = Position;
        
        if (Agent.CompareTag("Magicien"))
        {
            controlledMagicien.MoveToAttack(this, Position);
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Soldat"))
        {
            controlledSoldat.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Tirailleur"))
        {
            controlledTirailleur.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Bouclier"))
        {
            controlledBouclier.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Soignant"))
        {
            controlledSoignant.MoveTo(this, Position);
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
            else if (Agent.CompareTag("Soldat"))
            {
                if (attackElapsedtime >= controlledSoldat.attackPeriod)
                {
                    controlledSoldat.Attack(this, enemyUnit);
                }
            }
            else if (Agent.CompareTag("Tirailleur"))
            {
                if (attackElapsedtime >= controlledTirailleur.attackPeriod)
                {
                    controlledTirailleur.Attack(this, enemyUnit);
                }
            }
            else if (Agent.CompareTag("Bouclier"))
            {
                if (attackElapsedtime >= controlledBouclier.attackPeriod)
                {
                    controlledBouclier.Attack(this, enemyUnit);
                }
            }
            else if (Agent.CompareTag("Soignant"))
            {
                if (attackElapsedtime >= controlledSoignant.attackPeriod)
                {
                    controlledSoignant.Attack(this, enemyUnit);
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
            else if (Agent.CompareTag("Soldat"))
            {
                if (capacityElapsedtime >= controlledSoldat.capacityPeriod)
                {
                    controlledSoldat.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Tirailleur"))
            {
                if (capacityElapsedtime >= controlledTirailleur.capacityPeriod)
                {
                    controlledTirailleur.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Bouclier"))
            {
                if (capacityElapsedtime >= controlledBouclier.capacityPeriod)
                {
                    controlledBouclier.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Soignant"))
            {
                if (capacityElapsedtime >= controlledSoignant.capacityPeriod)
                {
                    controlledSoignant.UseCapacity(this);
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
        else if (Agent.CompareTag("Soldat"))
        {
            controlledSoldat.TakeDamage(this, degats);
        }
        else if (Agent.CompareTag("Tirailleur"))
        {
            controlledTirailleur.TakeDamage(this, degats);
        }
        else if (Agent.CompareTag("Bouclier"))
        {
            controlledBouclier.TakeDamage(this, degats);
        }
        else if (Agent.CompareTag("Soignant"))
        {
            controlledSoignant.TakeDamage(this, degats);
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
