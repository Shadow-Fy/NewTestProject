using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossLevel2 : MonoBehaviour
{
    public GameObject bar;
    public GameObject vcam2;
    public GameObject vcam3;
    public PlayableDirector director;
    public GameObject boundaryLeft1;
    public GameObject boundaryRight1;
    public GameObject boundaryLeft2;
    public GameObject boundaryRight2;
    private BoxCollider2D box => GetComponent<BoxCollider2D>();

    public YasuoControl yasuoControl;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        director.stopped += StartBoss;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void TimeLinePlay()
    {
        //bar.SetActive(false);
        yasuoControl.canhurt = false;
        yasuoControl.enabled = false;
        director.Play();
        vcam2.SetActive(false);
        vcam3.SetActive(true);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void StartBoss(PlayableDirector obj)
    {
        //bar.SetActive(true);
        yasuoControl.enabled = true;
        yasuoControl.canhurt = true;
        boundaryLeft1.SetActive(false);
        boundaryRight1.SetActive(false);
        boundaryLeft2.SetActive(true);
        boundaryRight2.SetActive(true);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
