using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float attackRange = 3f;
    [SerializeField]
    private float attackSpeed = 1f;

    private NavMeshAgent navMeshAgent = null;
    private Damager damager = null;

    private Nexus nexus = null;
    private Health nexusHealth = null;

    private float timeRemainingForAttack = 0f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        damager = GetComponent<Damager>();

        nexus = FindObjectOfType<Nexus>();
        if(nexus)
        {
            nexusHealth = nexus.GetComponent<Health>();
        }
    }

    private void Start()
    {
        SetDestinationToNexus();
    }

    private void SetDestinationToNexus()
    {
        navMeshAgent.SetDestination(nexus.transform.position);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, nexus.transform.position) <= attackRange)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.SetDestination(transform.position);

            AttackNexus();
        }
        else
        {
            timeRemainingForAttack = attackSpeed;
        }
    }

    private void AttackNexus()
    {
        if(nexusHealth.IsAlive)
        {
            timeRemainingForAttack -= Time.deltaTime;

            if (timeRemainingForAttack <= 0f)
            {
                timeRemainingForAttack = attackSpeed;
                damager.Attack(nexusHealth);
            }
        }
    }
}
