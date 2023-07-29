using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("×´Ì¬»ú")]
    public EnemyBaseState currentstate;
    public enum StateEnum
    {
        idle, attack, patrol
    }
    public Dictionary<StateEnum, EnemyBaseState> states = new Dictionary<StateEnum, EnemyBaseState>();

    public float maxhealth;
    public float currenthealth;
    public float speed;
    public Transform pointA, pointB;
    public Vector2 targetpoint;
    public Animator anim;
    public bool isdead;

    public float idletime;
    public float idlecd;

    [Header("¹¥»÷Ïà¹Ø")]
    public float attackrate;
    public float attackcd;
    public float nextattacktime = -10;

    private void Awake()
    {
        currenthealth = maxhealth;
        anim = GetComponent<Animator>();
        states.Add(StateEnum.idle, new IdleState(this));
        states.Add(StateEnum.attack, new AttackState(this));
        states.Add(StateEnum.patrol, new PatrolState(this));
        TransitionToState(StateEnum.idle);
    }

    // Update is called once per frame
    void Update()
    {
        currentstate.OnUpdate();
    }

    /// <summary>
    /// ÇÐ»»×´Ì¬
    /// </summary>
    /// <param name="type">×´Ì¬Ã¶¾ÙÀàÐÍ</param>
    public void TransitionToState(StateEnum type)
    {
        currentstate = states[type];
        currentstate.EnterState();
    }

    public void MoveAction()
    {
        anim.SetBool("run", true);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetpoint.x, transform.position.y), speed * Time.deltaTime);
        FlipDirection();
    }

    public void IdleAction()
    {
        idletime -= Time.deltaTime;
        if (idletime <= 0)
        {
            TransitionToState(StateEnum.patrol);
        }

    }


    public void AttackAction()
    {
        if (Time.time > nextattacktime)
        {
            FlipDirection();
            anim.SetBool("run", false);
            anim.SetTrigger("attack");
            nextattacktime = Time.time + attackcd;
        }
        else
        {
            TransitionToState(StateEnum.idle);
        }

    }


    public void FlipDirection() //Ðý×ª×óÓÒ·½Ïò
    {
        if (transform.position.x > targetpoint.x)/* ³¯×ó */
        {
            // direction = 1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else                                     /* ³¯ÓÒ */
        {
            // direction = -1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void ChangePatrolTarget()
    {
        if (Mathf.Abs(pointA.transform.position.x - transform.position.x) > Mathf.Abs(pointB.transform.position.x - transform.position.x))
        {
            targetpoint = pointA.position;
        }
        else
        {
            targetpoint = pointB.position;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetpoint = other.transform.position;
            TransitionToState(StateEnum.attack);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionToState(StateEnum.idle);
        }
    }
}
