using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_3 : YaSuoState
{
    private YaSuo Boss;
    private bool rlwind;
    private float cdfly = 2f;
    private float cdfire_2 = 2f;
    private float windcd = 0f;
    private Vector3 movePos = new Vector3(Random.Range(-37, 37), Random.Range(0, 17), 0);
    private float dist_3;
    private bool start = false;
    public float attackcount = 0;

    public override void EnterState(YaSuo yasuo)
    {
        Boss = yasuo;
        Boss.anim.SetBool("run", false);
        Boss.anim.SetBool("idle", true);
        if (!start)
        {
            Boss.blue = ObjectPool.Instance.GetObject(Boss.blueprefeb);
            Boss.purple = ObjectPool.Instance.GetObject(Boss.purpleprefeb);

            Boss.circle.SetActive(true);
            Boss.line1.SetActive(true);
            Boss.line2.SetActive(true);

            Boss.line1.GetComponent<LineRenderer>().startWidth = 0.25f;
            Boss.line1.GetComponent<LineRenderer>().endWidth = 0.25f;
            Boss.line2.GetComponent<LineRenderer>().startWidth = 0.25f;
            Boss.line2.GetComponent<LineRenderer>().endWidth = 0.25f;

            Boss.transform.position = new Vector3(0, 0, 0);
            Boss.anim.Play("state3");
            Boss.levelfire.SetActive(true);
            start = true;
            Boss.Invoke("State3CanMove", 2f);
        }
        Boss.rb.gravityScale = 0;
    }

    public override void OnUpdateState(YaSuo yasuo)
    {
        if (Boss.blue != null)
            Boss.line1.GetComponent<LineRenderer>().SetPosition(0, Boss.blue.transform.GetChild(0).transform.position);
        Boss.line1.GetComponent<LineRenderer>().SetPosition(1, Boss.circle.transform.position);
        if (Boss.purple != null)
            Boss.line2.GetComponent<LineRenderer>().SetPosition(0, Boss.purple.transform.GetChild(0).transform.position);
        Boss.line2.GetComponent<LineRenderer>().SetPosition(1, Boss.circle.transform.position);

        if (Boss.blue == null && Boss.purple == null)
        {
            Boss.circle.SetActive(false);

        }

        if (Boss.blue == null)
            Boss.line1.SetActive(false);

        if (Boss.purple == null)
            Boss.line2.SetActive(false);

        dist_3 = Vector3.Distance(Boss.transform.position, movePos);
        if (Boss.canmove)
            FlyMove();

        StartWind();
    }

    void StartWind()/* 开启风场 */
    {
        windcd -= Time.deltaTime;
        if (windcd <= 0)
        {
            if (rlwind)
            {
                rlwind = false;
            }
            else if (!rlwind)
            {
                rlwind = true;
            }
            windcd = 10f;
            if (rlwind)
            {
                Boss.windspace.SetActive(true);
                Boss.windspaceright.SetActive(false);
            }

            if (!rlwind)
            {
                Boss.windspace.SetActive(false);
                Boss.windspaceright.SetActive(true);
            }
        }
    }

    void TransfromationWind()/* 切换左右风场 */
    {

    }


    void FlyMove()
    {
        Boss.transform.position = Vector2.MoveTowards(Boss.transform.position, movePos, 90 * Time.deltaTime);
        cdfire_2 -= Time.deltaTime;
        if (dist_3 < 0.01f)
        {
            if (cdfly <= 0)
            {
                movePos = GetRandomPos();
                cdfly = 4;
                if (cdfire_2 <= 0 && attackcount == 0)
                {
                    Boss.Invoke("GroundAttack", 0.36f);
                    cdfire_2 = 4f;
                    Boss.groundattackcount = 0;
                    attackcount = 1;
                }
                else if (cdfire_2 <= 0 && attackcount == 1)
                {
                    Boss.Invoke("FireAttack", 0.36f);
                    cdfire_2 = 2.8f;
                    attackcount = 2;
                }
                else if (cdfire_2 <= 0 && attackcount == 2)
                {
                    Boss.Invoke("SwordAttack", 0.36f);
                    cdfire_2 = 3f;
                    attackcount = 3;
                }
                else if (cdfire_2 <= 0 && attackcount == 3)
                {
                    Boss.Invoke("FireAttack2", 0.36f);
                    cdfire_2 = 2.8f;
                    attackcount = 0;
                }

            }
            else
            {

                cdfly -= Time.deltaTime;
            }

        }
    }

    public Vector2 GetRandomPos()/* 获取一个随机坐标让亚索移动 */
    {
        Vector2 randPos = new Vector3(Random.Range(-37, 37), Random.Range(0, 17), 0);
        return randPos;
    }

}
