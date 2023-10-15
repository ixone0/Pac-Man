using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 _destinationPoint;
    public float _destinationRadius;
    public float PlayerPosition;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if(gameObject.tag == "Enemy" || gameObject.tag == "EnemyEaiable")
        {
            if(agent.remainingDistance < 0.1f)
            {
                SearhWaypoint();
            }
        }
        if(gameObject.tag == "EnemyDied")
        {
            _destinationPoint = new Vector3(-7.47f, 0.9f, -0.83f);
            agent.SetDestination(_destinationPoint);
            if (Vector3.Distance(transform.position, _destinationPoint) < 1f)
            {
                gameObject.tag = "Enemy";
                gameObject.GetComponent<Renderer>().material = player.defualtMaterial;
                UpdateSpeed(0f);
            }
        }
    }

    private void SearhWaypoint()
    {
        _destinationPoint  = Random.insideUnitSphere * _destinationRadius;
        _destinationPoint = _destinationPoint + transform.position;
        
        NavMeshHit hit;
        if(NavMesh.SamplePosition(_destinationPoint, out hit, 1f, NavMesh.AllAreas))
                               //(จุดที่ต้องการตรวจสอบ, เก็บค่าจุดที่ได้กลับมา, รัศมีของการตรวจสอบ, พื้นที่ที่ต้องการตรวจสอบ)
        {
            _destinationPoint = hit.position;
            agent.SetDestination(_destinationPoint);
        }
        else SearhWaypoint();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _destinationRadius);
    }

    public void UpdateSpeed(float x)
    {
        agent.speed += x;
    }
}
