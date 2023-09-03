using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Clock;

public class BossStart : MonoBehaviour
{
    public TimeUI timeUI;
    public GameObject bar;
    public GameObject vcam1;
    public GameObject vcam2;
    public PlayableDirector director;
    public GameObject boundaryLeft;
    public GameObject boundaryRight;
    private BoxCollider2D box => GetComponent<BoxCollider2D>();

    public YasuoControl yasuoControl;
    private GameObject player;

    private void Start()
    {
        director.stopped += StartBoss;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void StartBoss(PlayableDirector obj)
    {
        bar.SetActive(true);
        yasuoControl.enabled = true;
        yasuoControl.canhurt = true;
        boundaryLeft.SetActive(true);
        boundaryRight.SetActive(true);
        timeUI.StartTiming();
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            vcam1.SetActive(false);
            vcam2.SetActive(true);
            director.Play();
            box.enabled = false;
        }
    }
}
