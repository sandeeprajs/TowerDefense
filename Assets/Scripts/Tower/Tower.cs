using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius;
    private float attackCounter;

    private Enemy targetEnemy = null;

    [SerializeField]
    private Projectiles projectile;

    private bool isAttacking = false;
    #endregion

    #region Main Methods
    private void Update()
    {
        attackCounter -= Time.deltaTime;

        if(targetEnemy == null || targetEnemy.IsDead)
        {
            Enemy nearestEnemy = GetNearestEnemyInRange();
            if(nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.transform.position) <= attackRadius)
            {
                targetEnemy = nearestEnemy;
            }
        }
        else
        {
            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
            {
                targetEnemy = null;
            }

            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            }
            else
            {
                isAttacking = false;
            }
        }
        
    }

    private void FixedUpdate()
    {
        if(isAttacking)
        {
            Attack();
        }
    }
    #endregion

    #region Unity Utilites
    private void Attack ()
    {
        isAttacking = false;

        Projectiles newProjectile = Instantiate(projectile) as Projectiles;
        newProjectile.transform.localPosition = transform.localPosition;

        if(targetEnemy == null)
        {
            Destroy(newProjectile);
        }
        else
        {
            StartCoroutine(MoveProjectile(newProjectile));
        }
    }

    IEnumerator MoveProjectile (Projectiles projectile)
    {
        while (GetEnemyDistance(targetEnemy) > 0.2f && targetEnemy != null && projectile != null)
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.position = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);

            yield return null;
        }
        if(projectile == null || targetEnemy == null)
        {
            Destroy(projectile);
        }
    }

    private float GetEnemyDistance (Enemy enemy)
    {
        if(enemy == null)
        {
            enemy = GetNearestEnemyInRange();
            if(enemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, enemy.transform.localPosition));
    }

    private List<Enemy> GetEnemyInRange()
    {
        List<Enemy> enemeisInRange = new List<Enemy>();

        foreach (Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius)
            {
                enemeisInRange.Add(enemy);
            }
        }

        return enemeisInRange;
    }

    private Enemy GetNearestEnemyInRange ()
    {
        Enemy nearestEnemey = null;
        float smallestDistnace = float.PositiveInfinity;

        foreach(Enemy enemy in GetEnemyInRange())
        {
            if(Vector2.Distance(transform.position, enemy.transform.position) < smallestDistnace)
            {
                smallestDistnace = Vector2.Distance(transform.position, enemy.transform.position);
                nearestEnemey = enemy;
            }
        }
        return nearestEnemey;
    }
    #endregion
}
