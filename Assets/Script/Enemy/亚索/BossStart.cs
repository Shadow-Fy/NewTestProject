using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossStart : MonoBehaviour
{
    public GameObject vcam1;
    public GameObject vcam2;
    public GameObject bar;
    public PlayableDirector director;
    private BoxCollider2D box => GetComponent<BoxCollider2D>();

    public YasuoControl yasuoControl;

    private void Awake()
    {
        director.stopped += StartBoss;
    }
    public void StartBoss(PlayableDirector obj)
    {

        bar.SetActive(true);
        yasuoControl.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            vcam1.SetActive(false);
            vcam2.SetActive(true);
            director.Play();
            box.enabled = false;
        }
    }
}
