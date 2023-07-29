using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBackMachine : MonoBehaviour
{
    public class TimeBackInfo
    {
        public Vector3 position;

        public TimeBackInfo(Vector3 position)
        {
            this.position = position;
        }
    }

    private float gravity;
    private List<TimeBackInfo> infos = new List<TimeBackInfo>();
    private Rigidbody2D rb;
    public float addtime = 2f;
    private bool back = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartBack();
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            EndBack();
        }
    }

    private void FixedUpdate()
    {
        if (back)
        {
            GetInfo();
        }
        else
        {
            AddInfo();
        }
    }

    public void AddInfo()
    {
        if (infos.Count >= Mathf.Round(addtime / Time.deltaTime))
        {
            infos.RemoveAt(infos.Count - 1);
        }

        infos.Insert(0, new TimeBackInfo(transform.position));
    }

    public void GetInfo()
    {
        if (infos.Count > 0)
        {
            TimeBackInfo info = infos[0];
            transform.position = info.position;
            infos.RemoveAt(0);
        }
    }

    public void StartBack()
    {
        back = true;
        rb.gravityScale = 0;
        rb.isKinematic = true;
    }

    public void EndBack()
    {
        back = false;
        rb.gravityScale = gravity;
        rb.isKinematic = false;
    }

}
