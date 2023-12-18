using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 7f;
    private float currentChaseDistance;
    public float attackDistance = 1.8f;
    public float chaseAfterAttackDistance = 2f;
    public float patrolRadiusMin = 20f, paltrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    public float patrolTimer;
    public float waitBeforeAttack = 2f;
    private float attackTimer;
    private Transform target;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolForThisTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyState.PATROL)
        {
            this.Patrol();
        }
        if(enemyState == EnemyState.CHASE)
        {
            this.Chase();
        }
        if(enemyState == EnemyState.ATTACK)
        {
            this.Attack();
        }
    }

    protected void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;
        patrolTimer += Time.deltaTime;
        if(patrolTimer > patrolForThisTime)
        {
            SetNewRanDomDestination();
            patrolTimer = 0f;
        }
        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Walk(true);
        }
        else
        {
            enemyAnimator.Walk(false);
;       }
        if(Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnimator.Walk(false);
            enemyState = EnemyState.CHASE;
        }
    }

    protected void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        navAgent.SetDestination(target.position);
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimator.Run(true);
        }
        else
        {
            enemyAnimator.Run(false);
        }

        if(Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            enemyState = EnemyState.ATTACK;
            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            enemyAnimator.Run(false);
            enemyState = EnemyState.PATROL;
            patrolTimer = patrolForThisTime;
            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }

    protected void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        attackTimer += Time.deltaTime;
        if(attackTimer > waitBeforeAttack)
        {
            enemyAnimator.Attack();
            attackTimer = 0f;

        }
        if (Vector3.Distance(transform.position, target.position)
            > attackDistance + chaseAfterAttackDistance)
        {
                enemyState = EnemyState.CHASE;
        }
    }

    protected void SetNewRanDomDestination()
    {
        float randRadius = Random.Range(patrolRadiusMin, paltrolRadiusMax);
        Vector3 ranDir = Random.insideUnitSphere * randRadius;
        ranDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(ranDir, out navHit, randRadius, -1);
        navAgent.SetDestination(navHit.position);
    }

}
