using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    #region Variable
    [SerializeField]
    private int target = 0;

    private Transform enemy;
    [SerializeField]
    private Transform exitPoint;
    [SerializeField]
    private Transform[] wayPoints;

    private float navigationUpdate = 0f;
    private float navigationTime;

    [SerializeField]
    private int healthPoints;

    private bool isDead = false;

    private Collider2D enemyCollider;

    private Animator anim;
    #endregion

    #region getters
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }
    #endregion

    #region Main Method
    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        GameManager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Move in Path
        if(wayPoints != null && !isDead)
        {
            navigationTime += Time.deltaTime;

            if(navigationTime > navigationUpdate)
            {
                if(target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }
            }

            navigationTime = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "WayPoint")
        {
            target++;
        }
        else if(collider.tag == "Exit")
        {
            GameManager.Instance.UnRegiesterEnemy(this);
            //GameManager.Instance.RemoveEnemiesOnScreen();
            //Destroy(gameObject);
        }
        else if(collider.tag == "Projectile")
        {
            if(isDead == false)
            {
                Projectiles newPorjectileHit = collider.gameObject.GetComponent<Projectiles>();
                EnemyHit(newPorjectileHit.AttackStrength);
                Destroy(collider.gameObject);
            }
        }
    }

    public void EnemyHit (int hitPoint)
    {
        if(healthPoints - hitPoint >= 0)
        {
            healthPoints -= hitPoint;

            anim.Play("Enemy01_Hurt");
        }
        else
        {
            Die();
        }
    }

    public void Die ()
    {
        isDead = true;
        enemyCollider.enabled = false;

        anim.SetTrigger("IsDeadAnim");
    }
    #endregion

    #region Unity Utility
    #endregion
}
