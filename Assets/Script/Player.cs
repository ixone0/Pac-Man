using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int Life = 3;
    //score
    int score = 0;

    private Rigidbody rb;
    //eaidible
    public static bool eaidale = false;
    int temp = 1;
    public Material newMaterial;
    public Material defualtMaterial;
    public Material enemydiedmaterial;
    public int i;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        UpdateLive(0);
        UpdateScore(0);
        GameObject.Find("Life").GetComponent<TextMeshProUGUI>().text = "Lives : " + Life;
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score : " + score;
    }

    private void OnTriggerEnter(Collider other)
    {
        Eat(other);
    }

    void Move()
    {
            // Get the movement input values from the keyboard
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            Vector3 movementInput = new Vector3(-moveVertical, 0f, moveHorizontal).normalized;

            // Set the velocity based on input values
            rb.velocity = movementInput * 7f;
    }

    void Eat(Collider o)
    {
        if (o.gameObject.tag == "Dot")
        {
            score += 100;
            Destroy(o.gameObject);
        }
        if (o.gameObject.tag == "S.Dot")
        {
            Destroy(o.gameObject);
            Debug.Log("CanEatEnemy");
            GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject t in allEnemy)
            {
                t.GetComponent<Renderer>().material = newMaterial;
                t.tag = "EnemyEatable";
                eaidale = true;
                temp = 0;
            }
            StartCoroutine(PowerUpTimer());
        }
        if (o.gameObject.tag == "EnemyEatable")
        {
            o.GetComponent<Enemy>().UpdateSpeed(5f);
            o.GetComponent<Renderer>().material = enemydiedmaterial;
            o.tag = "EnemyDied";
            UpdateScore(200 * temp);
            temp++;
        }
        if (o.gameObject.tag == "Enemy")
        {
            gameObject.transform.localPosition = new Vector3(0f, 1.74f, 0f); //respawn
            Life -= 1;
        }
    }

    IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Can Not Eat");
        eaidale = false;
        GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("EnemyEatable");
        foreach (GameObject t in allEnemy)
        {
            t.GetComponent<Renderer>().material = defualtMaterial;
            t.tag = "Enemy";
        }
    }

    public void UpdateScore(int l)
    {
        score += l;
        if(score % 5000 == 1)
        {
            UpdateLive(1);
        }
    }

    public void UpdateLive(int s)
    {
        Life += s;
        //if GameClear
        GameObject[] alldot = GameObject.FindGameObjectsWithTag("Dot");
        if (alldot.Length == 0)
        {
            Debug.Log("GameClear");
            i++;
            Debug.Log(i);
            //ResetScene
        }
        if(Life <= 0)
        {
            Debug.Log("Game Over");
            Life = 0;
            GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] allEatableEnemy = GameObject.FindGameObjectsWithTag("EnemyEaiable");
            foreach (GameObject t in allEnemy)
            {
                t.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0f;
                t.GetComponent<Enemy>().enabled = false;
            }
            foreach (GameObject t in allEatableEnemy)
            {
                t.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0f;
                t.GetComponent<Enemy>().enabled = false;
            }
            rb.velocity = Vector3.zero;
            gameObject.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.identity;
            //use Unity program freeze
        }
    } 
}
