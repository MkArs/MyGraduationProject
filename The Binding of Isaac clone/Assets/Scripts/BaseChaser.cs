using System.Collections;
using UnityEngine;

/// <summary>
/// Класс врага ближнего боя
/// </summary>
public class BaseChaser : BaseEnemy
{
    [SerializeField]
    private float _speed;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        Chase();
    }

    /// <summary>
    /// Преследовать
    /// </summary>
    public void Chase()
    {
        gameObject.transform.position = (Vector3)Vector2.MoveTowards(gameObject.transform.position, Player.transform.position, _speed * Time.deltaTime);

        if (Vector2.Distance(gameObject.transform.position, Player.transform.position) < AttackRange && IsAttackStarted == false)
        {
            StartCoroutine(Attack());
        }
    }

    /// <summary>
    /// Аттаковать
    /// </summary>
    /// <returns></returns>
    public IEnumerator Attack()
    {
        IsAttackStarted = true;

        yield return new WaitForSeconds(AttackDelay);

        if (Vector2.Distance(gameObject.transform.position, Player.transform.position) <= AttackRange)
        {
            Player.gameObject.GetComponent<PlayerController>().Health -= 0.5f;
        }

        IsAttackStarted = false;
    }

}