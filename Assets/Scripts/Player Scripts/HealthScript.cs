using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;

    public float health = 100f;
    public bool isPlayer, isBoar, isCannibal;
    public bool isDead;
    private EnemyAudio enemyAudio;
    private PlayerStats playerStats;

    private void Awake()
    {
        if(isBoar || isCannibal)
        {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent <NavMeshAgent> ();
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if (isPlayer)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }
  
    public void ApplyDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;
        if (isPlayer)
        {
            playerStats.DisplayHealthStats(health);
            if (health <= 0)
            {
                this.PlayerDied();
                isDead = true;
            }
        }
        if(isBoar || isCannibal)
        {
            if(enemyController.EnemyState == EnemyState.PATROL)
            {
                enemyController.chaseDistance = 50f;
                Debug.Log("hit" + enemyController.gameObject.tag);
            }

            if (health <= 0)
            {
                Debug.Log("code run here");
                this.PlayerDied();
                isDead = true;
            }
        }
    }

    private void PlayerDied()
    {
        if (isCannibal)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 50f);
            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnimator.enabled = false;
            StartCoroutine(DeadSound());
            Destroy(gameObject);
            EnemyManager.instance.EnemyDied(true);
        }

        if (isBoar)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemyController.enabled = false;
            enemyAnimator.Dead();
            Debug.Log("this enemiy is dead");
            StartCoroutine(DeadSound());
            EnemyManager.instance.EnemyDied(false);
        }

        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
                EnemyManager.instance.StopSpawning();
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerAttack>().enabled = false;
                GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            }

            if(tag == Tags.PLAYER_TAG)
            {
                Invoke("RestartGame", 3f);
            }
            else
            {
                Invoke("TurnOffGameObject", 3f);
            }
        }
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    private void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeadSound();
    }
}
