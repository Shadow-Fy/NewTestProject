using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class YasuoSword2 : MonoBehaviour
{
    [Header("基础数据")]
    private CapsuleCollider2D coll;
    private BoxCollider2D _boxcoll;
    private Transform _playertr;
    private int speed = 60;
    public Vector3 _target;
    public Vector3 _originPosition;
    public GameObject tips;
    public GameObject _shadowprefab;
    public GameObject swordFire;
    private Transform _fatherTransform;
    public Transform midPoint;
    public Transform rightPoint;

    [Space]
    private Vector3 _currentposition;
    private Vector3 单位向量;
    private int lineAttackCount = 1;
    public float rowAttackCD;
    private float lineAttacktime;
    private bool lineAttackBool = false;
    public GameObject[] swordnum;
    private Vector3[] originPos = new Vector3[26];
    private int _count;

    public GameObject[] RotateSwordnum1;
    public GameObject[] RotateSwordnum2;
    public GameObject[] RotateSwordnum3;
    public GameObject[] RotateSwordnum4;
    private int rotateAttackCount = 1;
    private bool rotateAttackBool;
    private float rotateSpeed = 10;
    private Vector2[] targetpos1 = new Vector2[9];
    private Vector2[] targetpos2 = new Vector2[9];
    private Vector2[] targetpos3 = new Vector2[4];
    private Vector2[] targetpos4 = new Vector2[4];

    public GameObject redLine1;
    public GameObject redLine2;
    public GameObject redLine3;
    public GameObject redLine4;
    private float lineTime;
    public float lineCD;




    private void Start()
    {
        lineTime = lineCD;
        coll = GetComponent<CapsuleCollider2D>();
        _boxcoll = GetComponent<BoxCollider2D>();
        _playertr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _fatherTransform = transform.parent.GetChild(0).GetComponent<Transform>();
        lineAttacktime = rowAttackCD;
        coll.enabled = false;
        _boxcoll.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        单位向量 = (transform.position - _playertr.position).normalized;

        ObjectPool.Instance.GetObject(_shadowprefab);

        if (Input.GetKeyDown(KeyCode.M))
        {
            rotateAttackBool = true;
        }

        if (lineAttackBool)
        {
            lineAttack();
        }
        if (rotateAttackBool)
        {
            RotateAttack();
        }
    }

    private void lineAttack()
    {
        switch (lineAttackCount)
        {
            case 1: //主剑移动到目标位置
                transform.position = Vector2.MoveTowards(transform.position, _target, 20 * Time.deltaTime);
                _target = midPoint.position;
                if (Vector2.Distance(transform.position, _target) < 0.1f)
                {
                    for (int i = 0; i < swordnum.Length; i++)
                    {
                        originPos[i] = swordnum[i].transform.position;
                    }
                    lineAttackCount = 2;
                }
                break;
            case 2: //开启分身
                for (int i = 0; i < swordnum.Length; i++)
                {
                    swordnum[i].transform.position = transform.position;
                    swordnum[i].SetActive(true);
                    lineAttackCount = 3;
                }
                break;
            case 3: //分身移动到目标位置
                for (int i = 0; i < swordnum.Length; i++)
                {
                    swordnum[i].transform.position = Vector2.MoveTowards(swordnum[i].transform.position, originPos[i], 0.8f);
                }
                if (Vector2.Distance(swordnum[swordnum.Length - 1].transform.position, originPos[swordnum.Length - 1]) < 0.1f)
                {
                    _count = 0;
                    lineAttackCount = 4;
                }
                break;
            case 4: //开始攻击
                lineAttacktime -= Time.deltaTime;

                if (lineAttacktime <= 0 && _count == -1)
                {
                    GameObject tip = ObjectPool.Instance.GetObject(tips);
                    tip.transform.position = transform.position + new Vector3(0, 3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = transform.position;
                    lineAttacktime = rowAttackCD;
                    _count = 13;
                }

                if (lineAttacktime <= 0)
                {
                    GameObject tip = ObjectPool.Instance.GetObject(tips);
                    tip.transform.position = swordnum[_count].transform.position + new Vector3(0, 3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = swordnum[_count].transform.position;
                    lineAttacktime = rowAttackCD;
                    if (_count == 12)
                        _count = -1;
                    else
                        _count++;

                    if (_count == 25)
                    {
                        lineAttackCount = 5;
                        lineAttacktime = 0.5f;
                    }
                }
                break;
            case 5:
                lineAttacktime -= Time.deltaTime;

                if (lineAttacktime <= 0 && _count == -1)
                {
                    GameObject tip = ObjectPool.Instance.GetObject(tips);
                    tip.transform.position = transform.position + new Vector3(0, 3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = transform.position;
                    lineAttacktime = rowAttackCD;
                    _count = 12;
                }

                if (lineAttacktime <= 0)
                {
                    GameObject tip = ObjectPool.Instance.GetObject(tips);
                    tip.transform.position = swordnum[_count].transform.position + new Vector3(0, 3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = swordnum[_count].transform.position;
                    lineAttacktime = rowAttackCD;
                    if (_count == 13)
                        _count = -1;
                    else
                        _count--;

                    if (_count == 0)
                    {
                        lineAttackCount = 6;
                        lineAttacktime = 0.6f;
                    }
                }
                break;
            case 6: //收剑
                lineAttacktime -= Time.deltaTime;
                if (lineAttacktime <= 0)
                {
                    for (int i = 0; i < swordnum.Length; i++)
                    {
                        swordnum[i].transform.position = Vector2.MoveTowards(swordnum[i].transform.position, transform.position, 0.8f);
                    }
                    if (Vector2.Distance(swordnum[swordnum.Length - 1].transform.position, transform.position) < 0.1f)
                    {
                        for (int i = 0; i < swordnum.Length; i++)
                        {
                            swordnum[i].SetActive(false);
                        }
                        lineAttackCount = 1;
                        lineAttacktime = rowAttackCD;
                        lineAttackBool = false;
                    }
                }
                break;
        }
    }


    private void RotateAttack()
    {
        Debug.Log(rotateAttackCount);
        switch (rotateAttackCount)
        {
            case 1: //飞剑3旋转
                transform.Rotate(Vector3.forward * rotateSpeed);
                if (rotateSpeed < 30)
                    rotateSpeed += Time.deltaTime * 6;
                else
                {
                    _target = midPoint.position + new Vector3(0, 10, 0);
                    rotateAttackCount = 2;
                }
                break;
            case 2: //飞剑3上升
                transform.Rotate(Vector3.forward * speed);
                transform.position = Vector2.MoveTowards(transform.position, _target, 30 * Time.deltaTime);
                if (Vector2.Distance(transform.position, _target) < 0.1f)
                {
                    rotateAttackCount = 3;
                }
                break;
            case 3: //启动旋转剑
                for (int i = 0; i < RotateSwordnum1.Length; i++)
                {
                    RotateSwordnum1[i].transform.position = new Vector3(89.7f - i * 8, 12.5f);
                    RotateSwordnum1[i].SetActive(true);
                    targetpos1[i] = new Vector2(RotateSwordnum1[i].transform.position.x - 8, -25);
                    if (i == RotateSwordnum1.Length - 1)
                    {
                        rotateAttackCount = 4;
                    }
                }
                break;
            case 4: //线闪烁
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine1.SetActive(true);

                }
                if (lineTime <= lineCD * -2)
                {
                    redLine1.SetActive(false);
                    lineTime = lineCD;
                    rotateAttackCount = 5;
                }
                break;
            case 5:
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine1.SetActive(true);
                }
                if (lineTime <= lineCD * -2)
                {
                    redLine1.SetActive(false);
                    lineTime = lineCD;
                    rotateAttackCount = 6;
                }
                break;
            case 6: //旋转剑移动
                for (int i = 0; i < RotateSwordnum1.Length; i++)
                {
                    RotateSwordnum1[i].transform.position = Vector2.MoveTowards(RotateSwordnum1[i].transform.position, targetpos1[i], 70 * Time.deltaTime);
                }
                if (Vector2.Distance(RotateSwordnum1[0].transform.position, targetpos1[0]) < 0.1f)
                {
                    for (int i = 0; i < RotateSwordnum1.Length; i++)
                    {
                        RotateSwordnum1[i].SetActive(false);
                    }
                    rotateAttackCount = 7;
                }
                break;
            case 7: //线闪烁
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine2.SetActive(true);

                }
                if (lineTime <= lineCD * -2)
                {
                    redLine2.SetActive(false);
                    lineTime = lineCD;
                    rotateAttackCount = 8;
                }
                break;
            case 8:
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine2.SetActive(true);
                }
                if (lineTime <= lineCD * -2)
                {
                    redLine2.SetActive(false);
                    lineTime = lineCD;
                    for (int i = 0; i < RotateSwordnum2.Length; i++)
                    {
                        RotateSwordnum2[i].transform.position = new Vector3(83.7f - i * 8, 12.5f);
                        RotateSwordnum2[i].SetActive(true);
                        targetpos2[i] = new Vector2(RotateSwordnum2[i].transform.position.x + 8, -26.5f);
                        if (i == RotateSwordnum2.Length - 1)
                        {
                            rotateAttackCount = 9;
                        }
                    }
                }
                break;
            case 9:
                for (int i = 0; i < RotateSwordnum2.Length; i++)
                {
                    RotateSwordnum2[i].transform.position = Vector2.MoveTowards(RotateSwordnum2[i].transform.position, targetpos2[i], 70 * Time.deltaTime);
                }
                if (Vector2.Distance(RotateSwordnum2[0].transform.position, targetpos2[0]) < 0.1f)
                {
                    for (int i = 0; i < RotateSwordnum2.Length; i++)
                    {
                        RotateSwordnum2[i].SetActive(false);
                    }
                    rotateAttackCount = 10;
                }
                break;
            case 10:
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine3.SetActive(true);

                }
                if (lineTime <= lineCD * -2)
                {
                    redLine3.SetActive(false);
                    lineTime = lineCD;
                    rotateAttackCount = 11;
                }
                break;
            case 11:
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine3.SetActive(true);
                }
                if (lineTime <= lineCD * -2)
                {
                    redLine3.SetActive(false);
                    lineTime = lineCD;
                    for (int i = 0; i < RotateSwordnum3.Length; i++)
                    {
                        RotateSwordnum3[i].transform.position = new Vector3(25, 6 - i * 8);
                        RotateSwordnum3[i].SetActive(true);
                        targetpos3[i] = new Vector2(94, RotateSwordnum3[i].transform.position.y);
                        if (i == RotateSwordnum3.Length - 1)
                        {
                            rotateAttackCount = 12;
                        }
                    }
                }
                break;
            case 12:
                for (int i = 0; i < RotateSwordnum3.Length; i++)
                {
                    RotateSwordnum3[i].transform.position = Vector2.MoveTowards(RotateSwordnum3[i].transform.position, targetpos3[i], 70 * Time.deltaTime);
                }
                if (Vector2.Distance(RotateSwordnum3[0].transform.position, targetpos3[0]) < 0.1f)
                {
                    for (int i = 0; i < RotateSwordnum3.Length; i++)
                    {
                        RotateSwordnum3[i].SetActive(false);
                    }
                    rotateAttackCount = 13;
                }
                break;
            case 13:
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine4.SetActive(true);

                }
                if (lineTime <= lineCD * -2)
                {
                    redLine4.SetActive(false);
                    lineTime = lineCD;
                    rotateAttackCount = 14;
                }
                break;
            case 14:
                lineTime -= Time.deltaTime;
                if (lineTime <= 0)
                {
                    redLine4.SetActive(true);
                }
                if (lineTime <= lineCD * -2)
                {
                    redLine4.SetActive(false);
                    lineTime = lineCD;
                    for (int i = 0; i < RotateSwordnum4.Length; i++)
                    {
                        RotateSwordnum4[i].transform.position = new Vector3(94, 2 - i * 8);
                        RotateSwordnum4[i].SetActive(true);
                        targetpos4[i] = new Vector2(25, RotateSwordnum4[i].transform.position.y);
                        if (i == RotateSwordnum4.Length - 1)
                        {
                            rotateAttackCount = 15;
                        }
                    }
                }
                break;
            case 15:
                for (int i = 0; i < RotateSwordnum4.Length; i++)
                {
                    RotateSwordnum4[i].transform.position = Vector2.MoveTowards(RotateSwordnum4[i].transform.position, targetpos4[i], 70 * Time.deltaTime);
                }
                if (Vector2.Distance(RotateSwordnum4[0].transform.position, targetpos4[0]) < 0.1f)
                {
                    for (int i = 0; i < RotateSwordnum4.Length; i++)
                    {
                        RotateSwordnum4[i].SetActive(false);
                    }
                    rotateAttackCount = 1;
                    rotateAttackBool = false;
                }
                break;
        }
    }
    public Vector3 GetInspectorRotationValueMethod(Transform transform)
    {
        // 获取原生值
        System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        string temp = value.ToString();
        //将字符串第一个和最后一个去掉
        temp = temp.Remove(0, 1);
        temp = temp.Remove(temp.Length - 1, 1);
        //用‘，’号分割
        string[] tempVector3;
        tempVector3 = temp.Split(',');
        //将分割好的数据传给Vector3
        Vector3 vector3 = new Vector3(float.Parse(tempVector3[0]), float.Parse(tempVector3[1]), float.Parse(tempVector3[2]));
        return vector3;
    }
}
