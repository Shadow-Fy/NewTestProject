using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{
    public class TimerInformationStu
    {
        public Vector3 pos;
        public Quaternion rot;


        public TimerInformationStu(Vector3 pos, Quaternion rot)
        {
            this.pos = pos;
            this.rot = rot;
        }
    }

    private List<TimerInformationStu> timerlist = new List<TimerInformationStu>();
    public float recordtime = 5f;
    private bool isrewarding = false;
    private Rigidbody2D rb;
    private float gravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartReward();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            StopReward();
        }
    }

    private void FixedUpdate()
    {
        if (isrewarding)
        {
            Reward();
        }
        else
        {
            Record();
        }
    }

    private void Record()
    {
        if (timerlist.Count > Mathf.Round(recordtime / Time.deltaTime))
        {
            timerlist.RemoveAt(timerlist.Count - 1);
        }

        timerlist.Insert(0, new TimerInformationStu(transform.position, transform.rotation));

    }

    private void StartReward()
    {
        isrewarding = true;
        rb.gravityScale = 0;
        rb.isKinematic = true;
    }

    private void StopReward()
    {
        isrewarding = false;
        rb.isKinematic = false;
        rb.gravityScale = gravity;
    }

    private void Reward()
    {
        if (timerlist.Count > 0)
        {
            TimerInformationStu information = timerlist[0];
            transform.position = information.pos;
            transform.rotation = information.rot;
            timerlist.RemoveAt(0);
        }
    }
}
