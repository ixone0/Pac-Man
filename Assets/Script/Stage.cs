using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int level = 0;
    [SerializeField] private float[] EnemySpeed;
    [SerializeField] private string[] fruit;
    private Player player;

    void Start()
    {
        EnemySpeed = new float[] {5, 6, 7};
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        Nextlevel(level);
    }
    void Nextlevel(int u)
    {
        GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] allEatableEnemy = GameObject.FindGameObjectsWithTag("EnemyEaiable");
        foreach (GameObject t in allEnemy)
        {
            t.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = EnemySpeed[u];
        }
        foreach (GameObject t in allEatableEnemy)
        {
            t.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = EnemySpeed[u];
        }
    }

}
