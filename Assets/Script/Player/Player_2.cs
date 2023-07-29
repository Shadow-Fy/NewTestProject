using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : PlayerController
{
    private AnimatorStateInfo animStateInfo;
    public override void OnEnable()
    {
        base.OnEnable();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Attack();

    }

    protected override void FixedUpdate()
    {
        Dash();
        if (isDash)
            return;
        Movement();
        Jump();
    }

    public override void Movement()
    {
        rb.velocity = new Vector2(horizontalmove_float * speed, rb.velocity.y);
        if (horizontalmove_int != 0)
            transform.localScale = new Vector3(horizontalmove_int, 1, 1);
        if (horizontalmove_float != 0)
        {
            if (isRun)
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", false);
                anim.SetBool("walk", true);
            }
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
        }
    }

    protected override void Dash()
    {
        if (isDash)
        {
            anim.SetBool("dash", true);
            anim.SetBool("walk", false);
            anim.SetBool("fall", false);
            anim.SetBool("jump", false);
            if (dashTimeleft > 0)
            {
                shadowcd += Time.deltaTime;
                rb.velocity = new Vector2(dashspeed * horizontalmove_int, (dashspeed - 20) * verticalmove_int);
                dashTimeleft -= Time.deltaTime;
                if (shadowcd >= 0.01f)
                {
                    ObjectPool.Instance.GetObject(shadowPrefab);
                    shadowcd = 0;
                }
            }
            if (dashTimeleft <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                isDash = false;
                anim.SetBool("dash", false);
            }
        }
    }

    public override void Attack()
    {
        animStateInfo = anim.GetCurrentAnimatorStateInfo(1);
        if (animStateInfo.IsName("idleState"))
        {
            anim.SetInteger("attack", 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (animStateInfo.IsName("idleState"))
            {
                anim.SetInteger("attack", 1);
            }

            if (animStateInfo.IsName("attack1") && animStateInfo.normalizedTime > 0.4f)
            {
                anim.SetInteger("attack", 2);
            }

            if (animStateInfo.IsName("attack2") && animStateInfo.normalizedTime > 0.4f)
            {
                anim.SetInteger("attack", 3);
            }
        }
    }
}
