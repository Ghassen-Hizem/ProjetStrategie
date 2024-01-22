using System.Collections;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class scriptEnemy : MonoBehaviour
{

    public EnemyUnit enemyMagicien;
    public EnemyUnit enemyCavalier;
    public EnemyUnit enemyBouclier;

    public Image healthBar;
    public float healthAmountSoldat = 50;
    
    public float healthAmount;
    public float maxHealth;

    private Camera mainCam;
    [SerializeField] public Canvas canvaHealthBar;

    private SelectableUnit Unit;
    private Transform UnitPosition;
    private float distance;
    public NavMeshAgent Agent;
    public Animator anim;
    private string Attack_Animation_Soldat = "IsAttacking";

    private bool enemyReached = false;

    public A1EnemyRadius EnemyRadius;

    private bool IsFighting = false;

   
    private int attackPeriodSoldat = 2;
    private int attackPeriod;
    public float attackElapsedtime = 0;

    private int nbArmors;
    private int nbArmorsSoldat = 1;
    private int speedSoldat = 3;
    private int degatAttack;
    private int degatAttackSoldat = 3;

    public pushRadiusCavalierEnemy pushRadiusCavalier;
    public pushRadiusBouclierEnemy pushRadiusBouclier;

    void Start()
    {
        mainCam = Camera.main;

        //Agent.speed = speed;

        if (gameObject.CompareTag("Magicien"))
        {
            healthAmount = enemyMagicien.lifePoints;
            nbArmors = enemyMagicien.nbArmors;
            maxHealth = healthAmount;
            degatAttack = enemyMagicien.degatAttack;
            attackPeriod = enemyMagicien.attackPeriod;
        }
        if (gameObject.CompareTag("Soldat"))
        {
            healthAmount = healthAmountSoldat;
            nbArmors = nbArmorsSoldat;
            maxHealth = healthAmount;
            degatAttack = degatAttackSoldat;
            attackPeriod = attackPeriodSoldat;
        }
        if (gameObject.CompareTag("Bouclier"))
        {
            healthAmount = enemyBouclier.lifePoints;
            nbArmors = enemyBouclier.nbArmors;
            maxHealth = healthAmount;
            degatAttack = enemyBouclier.degatAttack;
            degatAttack = degatAttackSoldat;
            attackPeriod = enemyBouclier.attackPeriod;
        }
        if (gameObject.CompareTag("Cavalier"))
        {
            healthAmount = enemyCavalier.lifePoints;
            nbArmors = enemyCavalier.nbArmors;
            maxHealth = healthAmount;
            degatAttack = enemyCavalier.degatAttack;
            attackPeriod = enemyCavalier.attackPeriod;
        }
    }

    void Update()
    {

        canvaHealthBar.transform.rotation = Quaternion.LookRotation(healthBar.transform.position - mainCam.transform.position);
        attackElapsedtime += Time.deltaTime;

        IsFighting = EnemyRadius.haveTarget;
        if (IsFighting)
        {
            if (EnemyRadius.target)
            {
                Unit = EnemyRadius.target;
                UnitPosition = Unit.transform;
                distance = Vector3.Distance(gameObject.transform.position, UnitPosition.position);
                
                if (attackElapsedtime >= attackPeriod)
                {
                    Attack();
                    
                    
                }
            }
            else
            {
                EnemyRadius.haveTarget = false;
            }

        }
        if (EnemyRadius.haveTarget == false)
        {
            Agent.ResetPath();
            if (gameObject.CompareTag("Soldat"))
            {
                anim.SetBool(Attack_Animation_Soldat, false);
            }
            
        }


    }

    void Attack()
    {
        if (Unit != null)
        {
            if (enemyReached == false)
            {
                if (gameObject.CompareTag("Magicien"))
                {
                    Agent.speed = enemyMagicien.speed;
                    Agent.SetDestination(UnitPosition.position);
                }
                else if (gameObject.CompareTag("Cavalier"))
                {
                    Agent.speed = enemyCavalier.speed;
                    Agent.SetDestination(UnitPosition.position);
                    pushRadiusCavalier.gameObject.SetActive(false);
                }
                else if (gameObject.CompareTag("Soldat"))
                {
                    Agent.speed = speedSoldat;
                    Agent.SetDestination(UnitPosition.position);
                }
                else if (gameObject.CompareTag("Bouclier"))
                {
                    Agent.speed = enemyBouclier.speed;
                    Agent.SetDestination(UnitPosition.position);
                    pushRadiusBouclier.gameObject.SetActive(false);
                }

            }

            if (gameObject.CompareTag("Soldat"))
            {
                if (distance < 1)
                {
                    Agent.ResetPath();
                    enemyReached = true;
                    Agent.speed = 0f;
                    transform.LookAt(Unit.transform);
                    //Debug.Log("enemy attacking");

                    anim.SetBool(Attack_Animation_Soldat, true);
                    
                    attackElapsedtime = 0;
                    StartCoroutine("giveDamage");

                }
                else if (Unit == null)
                {
                    StopCoroutine("giveDamage");
                }
                else
                {
                    enemyReached = false;
                    //Agent.speed = speed;
                   
                    anim.SetBool(Attack_Animation_Soldat, false);
                    
                    StopCoroutine("giveDamage");
                }

            }
            else
            {
                if (distance < 10)
                {
                    Agent.ResetPath();
                    enemyReached = true;
                    Agent.speed = 0f;
                    //Debug.Log("enemy attacking");

                    if (gameObject.CompareTag("Magicien"))
                    {
                        transform.LookAt(Unit.transform);
                        //var lookRotation = Unit.transform.position - transform.position;
                        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime);
                        enemyMagicien.Attack(this, Unit);
                        StartCoroutine("giveDamage");
                    }
                    else if (gameObject.CompareTag("Cavalier"))
                    {
                        //transform.LookAt(Unit.transform);
                        enemyCavalier.Attack(this, Unit);
                    }
                    else if (gameObject.CompareTag("Bouclier"))
                    {
                        //transform.LookAt(Unit.transform);
                        enemyBouclier.Attack(this, Unit);
                    }



                }
                else if (Unit == null)
                {
                    StopCoroutine("giveDamage");
                }
                else
                {
                    enemyReached = false;
                    //Agent.speed = speed;
                    StopCoroutine("giveDamage");
                }
            }
            
        }


    }

    IEnumerator giveDamage()
    {
        if (Unit != null)
        {
            if (Unit)
            {
                Unit.TakeDamage(degatAttack);
            }
            yield return new WaitForSecondsRealtime(attackPeriod);
            //yield return new WaitForSeconds(2f);
        }

    }

    public void TakeDamage(int damage)
    {

        
        healthAmount = healthAmount - damage + nbArmors;
        healthBar.fillAmount = healthAmount / maxHealth;
       
        if (healthAmount <= 0)
        {
            Agent.speed = 0;
            //stop the moveToAttack
            if (gameObject.CompareTag("Soldat"))
            {
                anim.SetBool("IsDead", true);
            }
            
            Destroy(gameObject, 5);

        }
    }


}
