using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public GameObject playerCap;
    public float mSpeed = 2.75f;
    public float timeBetweenAttacks = 2.5f;
    public int damage = 20;
    public float attackRadius = 4f;
    public float maxHealth;
    public CharacterHandler pHealth;
    public CharacterController controller;
    public float distToTarget = 5f;
    private float timer;

    //public RigidBody eRigidBody;
    void Awake()
    {
        playerCap = GameObject.FindGameObjectWithTag("Player");
        player = playerCap.GetComponent<Transform>();
        pHealth = playerCap.GetComponent<CharacterHandler>();
        controller = playerCap.GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        maxHealth = 100f;

        //eRigidBody = GetComponent<RigidBody>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        distToTarget = Vector3.Distance(transform.position, player.position);

        if (pHealth.curHealth > 0)
        {
            agent.SetDestination(player.position);
            if (distToTarget < attackRadius && timer >= timeBetweenAttacks && pHealth.curHealth > 0)
            {
                Attack();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack();
        }
    }
    void Attack()
    {
        timer = 0f;
        Debug.Log("Attacking!");
        pHealth.TakeDamage(damage);
    }
    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
