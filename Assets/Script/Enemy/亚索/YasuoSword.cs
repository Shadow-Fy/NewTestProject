using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class YasuoSword : MonoBehaviour
{
    [Header("基础数据")]
    private CapsuleCollider2D coll;
    private BoxCollider2D _boxcoll;
    private Transform _playertr;
    private int speed = 60;
    public Vector3 _target;
    public Vector3 _originPosition;
    public GameObject _shadowprefab;
    private Transform _fatherTransform;

    [Space]
    private Vector3 _currentposition;
    private Vector3 单位向量;

    [Space]
    [Header("Attack1")]
    private bool _isattack_1;
    private float _attack1time = 1.2f;
    private int _attack1choose;
    private Vector3 _playercurrentposition;
    private Vector3 当前单位向量;

    [Space]
    [Header("Attack2")]
    private bool _isattack_2;
    private int _attack2choose;
    public GameObject _yasuoSwordFire;
    private float _fire2time = 0.13f;
    private bool _canfire2;
    public ParticleSystem 剑雨;

    [Space]
    [Header("Attack3")]
    private int _attack3choose;
    [HideInInspector] public bool _isattack_3;

    private void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        _boxcoll = GetComponent<BoxCollider2D>();
        _playertr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _fatherTransform = transform.parent.GetChild(0).GetComponent<Transform>();

        coll.enabled = false;
        _boxcoll.enabled = false;
    }

    private void Update()
    {
        单位向量 = (transform.position - _playertr.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);


        ObjectPool.Instance.GetObject(_shadowprefab);


        if (!_isattack_1 && !_isattack_2 && !_isattack_3)
        {
            FollowYasuo();

            setRotation();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            _attack1choose = 1;
            _isattack_1 = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            _attack2choose = 1;
            _isattack_2 = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _attack3choose = 1;
            _isattack_3 = true;
        }

        if (_isattack_1)
        {
            Attack1();
        }

        if (_isattack_2)
        {
            Attack2();
        }

        if (_isattack_3)
        {
            Attack3();
        }
    }

    public void StartAttack1()/* 外部Boss调用 */
    {
        _attack1choose = 1;
        _isattack_1 = true;
    }

    public void StartAttack2()/* 外部Boss调用 */
    {
        _attack2choose = 1;
        _isattack_2 = true;
    }

    public void StartAttack3()/* 外部Boss调用 */
    {
        _attack3choose = 1;
        _isattack_3 = true;
    }

    void setRotation()//武器移动时有个角度的偏移
    {
        if (_target.x < transform.position.x)
        {
            // transform.rotation = Quaternion.Euler(0, 0, 80);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 105), Time.deltaTime * 10);
        }
        else if (_target.x > transform.position.x)
        {
            // transform.rotation = Quaternion.Euler(0, 0, 100);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 75), Time.deltaTime * 10);
        }
        else
        {
            // transform.rotation = Vector3.MoveTowards(transform.rotation, Quaternion.Euler(0, 0, 90), 10 * Time.deltaTime)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 10);
            // transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }


    void FollowYasuo()//回到亚索旁边
    {
        if (Vector3.Distance(_fatherTransform.position, transform.position) > 6)
        {
            speed = 100;
        }
        else speed = 60;

        if (_fatherTransform.transform.rotation.y == 0)//朝右边
        {
            _target = _originPosition + _fatherTransform.position;
        }
        else
        {
            _target = new Vector3(-_originPosition.x, _originPosition.y) + _fatherTransform.position;
        }
    }

    public void Attack1()
    {
        _attack1time -= Time.deltaTime;
        switch (_attack1choose)
        {
            case 1: //确定目标
                if (transform.position.y <= 0)
                {
                    _currentposition = transform.position;
                    _target = new Vector2(transform.position.x, -2);
                }
                if (Vector3.Distance(transform.position, _target) < 0.1f)
                {
                    //改变圣剑的方向指向玩家
                    float angle1 = Mathf.Atan2(单位向量.x, 单位向量.y) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90 - angle1), Time.deltaTime * 20);
                    _attack1choose = 2;
                }
                break;
            case 2: //后退
                float angle = Mathf.Atan2(单位向量.x, 单位向量.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90 - angle), Time.deltaTime * 20);
                _target = _currentposition + 3.5f * 单位向量;
                speed = 30;
                coll.enabled = true;
                if (Vector3.Distance(transform.position, _target) < 0.1f && _attack1time < 0)
                {
                    当前单位向量 = 单位向量;
                    _playercurrentposition = _playertr.position;
                    _attack1choose = 3;
                }
                break;
            case 3: //前冲
                speed = 90;
                _target = _playercurrentposition - 10 * 当前单位向量;
                if (Vector3.Distance(transform.position, _target) < 0.1f)
                {
                    _attack1choose = 0;
                    _isattack_1 = false;
                    coll.enabled = false;
                    _attack1time = 1.2f;
                }
                break;
        }
    }
    public void Attack2()
    {
        _fire2time -= Time.deltaTime;
        if (_canfire2 && _fire2time <= 0)
        {
            GameObject swordfire = ObjectPool.Instance.GetObject(_yasuoSwordFire);
            swordfire.transform.position = transform.position;
            swordfire.transform.rotation = transform.rotation;
            _fire2time = 0.13f;
        }
        switch (_attack2choose)
        {
            case 1:
                _target = new Vector3(58.9f, -1, transform.position.z) + new Vector3(_originPosition.x * 3, 0, 0);
                if (Vector2.Distance(transform.position, _target) < 0.1f)
                    _attack2choose = 2;
                break;
            case 2:
                
                break;

        }
        //switch (_attack2choose)
        //{
        //    case 1:
        //        _target = new Vector3(58.9f, 0, transform.position.z) + _originPosition;
        //        if (Vector2.Distance(transform.position, _target) < 0.1f)
        //            _attack2choose = 2;
        //        break;
        //    case 2:
        //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90), 2);
        //        _canfire2 = true;//开始发射弹幕
        //        if (GetInspectorRotationValueMethod(transform).z == -90)
        //            _attack2choose = 3;
        //        break;
        //    case 3:
        //        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -50), 2);
        //        if (GetInspectorRotationValueMethod(transform).z == -50)
        //            _attack2choose = 4;
        //        break;
        //    case 4:
        //        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 230), 2);
        //        if (GetInspectorRotationValueMethod(transform).z == 230)
        //            _attack2choose = 5;
        //        break;
        //    case 5:
        //        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -50), 2);
        //        if (GetInspectorRotationValueMethod(transform).z == -50)
        //        {
        //            _attack2choose = 0;
        //            剑雨.Play();
        //            _canfire2 = false;
        //            _isattack_2 = false;
        //        }
        //        break;
        //}

    }
    public void Attack3()
    {
        switch (_attack3choose)
        {
            case 1:
                _target = new Vector3(58.91055f + _originPosition.x * 6, 20, 0);
                if (Vector3.Distance(transform.position, _target) < 0.1f)
                {
                    transform.localScale = new Vector3(-7, 7);
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    speed = 30;
                    _attack3choose = 2;
                    coll.enabled = true;
                    _boxcoll.enabled = true;
                }
                break;
            case 2:
                _target = new Vector3(_target.x, -50, 0);
                if (Vector3.Distance(transform.position, _target) < 0.1f)
                {
                    transform.localScale = new Vector3(-1, 1);
                    _attack3choose = 0;
                    _isattack_3 = false;
                    coll.enabled = false;
                    _boxcoll.enabled = false;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(25);
        }
    }



    //四元数转换为原生数值
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
