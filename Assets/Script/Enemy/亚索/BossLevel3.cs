using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Clock;

public class BossLevel3 : MonoBehaviour
{
    public TimeUI timeUI;
    public GameObject bar;
    public GameObject vcam3;
    public GameObject vcam4;
    public PlayableDirector director;
    public GameObject boundaryLeft2;
    public GameObject boundaryRight2;
    public GameObject boundaryLeft3;
    public GameObject boundaryRight3;

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
        timeUI.StopTiming();
        director.Play();
        vcam3.SetActive(false);
        vcam4.SetActive(true);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void StartBoss(PlayableDirector obj)
    {
        //bar.SetActive(true);
        timeUI.StartTiming();
        yasuoControl.enabled = true;
        yasuoControl.Sword3.GetComponent<YasuoSword2>().lineAttackBool = true;
        yasuoControl.canhurt = true;
        boundaryLeft2.SetActive(false);
        boundaryRight2.SetActive(false);
        boundaryLeft3.SetActive(true);
        boundaryRight3.SetActive(true);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
