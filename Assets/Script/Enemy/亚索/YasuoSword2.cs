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
    public int lineAttackCount;
    public float lineAttackCD;
    private float lineAttacktime;
    private bool lineAttackBool = false;
    public GameObject[] swordnum;
    public Vector3[] originPos;
    private int _count;



    private void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        _boxcoll = GetComponent<BoxCollider2D>();
        _playertr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _fatherTransform = transform.parent.GetChild(0).GetComponent<Transform>();
        lineAttacktime = lineAttackCD;
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
            lineAttackBool = true;
        }

        if (lineAttackBool)
        {
            lineAttack();
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
                    tip.transform.position = transform.position + new Vector3(0,3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = transform.position;
                    lineAttacktime = lineAttackCD;
                    _count = 13;
                }

                if (lineAttacktime <= 0)
                {
                    GameObject tip = ObjectPool.Instance.GetObject(tips);
                    tip.transform.position = swordnum[_count].transform.position + new Vector3(0,3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = swordnum[_count].transform.position;
                    lineAttacktime = lineAttackCD;
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
                    tip.transform.position = transform.position + new Vector3(0,3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = transform.position;
                    lineAttacktime = lineAttackCD;
                    _count = 12;
                }

                if (lineAttacktime <= 0)
                {
                    GameObject tip = ObjectPool.Instance.GetObject(tips);
                    tip.transform.position = swordnum[_count].transform.position + new Vector3(0,3);
                    GameObject sword = ObjectPool.Instance.GetObject(swordFire);
                    sword.transform.position = swordnum[_count].transform.position;
                    lineAttacktime = lineAttackCD;
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
                        lineAttacktime = lineAttackCD;
                        lineAttackBool = false;
                    }
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
