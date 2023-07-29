# Everythings

## 关于实例Instantiate && 预设Prefab（用于射击等）

在Unity3D的工程建设中，Prefabs（预设）是最非常用的一种资源类型，是一种可被重复使用的游戏对象。

特点1：它可以被置入多个场景中，也可以在一个场景中多次置入。

特点2：当你在一个场景中增加一个Prefabs，你就实例化了一个Prefabs。

特点3：所有Prefabs实例都是Prefab的克隆，所以如果实在运行中生成对象会有(Clone)的标记。

特点4：只要Prefabs原型发生改变，所有的Prefabs实例都会产生变化。

```c#
Prefabs的用法：如果需要创建一些想要重复使用的东西，就该用它了（例如子弹等）。
public class shoot : MonoBehaviour {



    public GameObject bullet;//获取一个所创建的预设体
    public float speed = 5;
    void Start () 
    {
       	
    }
    void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject b= GameObject.Instantiate(bullet,transform.position, transform.rotation);
            //Instantiate用法，注意参数
            //bullet用来克隆的Prefabs
            //transform.position 脚本绑定对象的位置 
            //transform.rotation 脚本绑定对象的角度
        }
    }
}
```





## Invoke

**Invoke** (string **methodName**, float **time**);

在 `time` 秒后调用 `methodName` 方法。

Invoke() 方法是 Unity3D 的一种委托机制

如：`` Invoke("SendMsg", 5); `` 它的意思是：**5 秒之后调用 SendMsg() 方法**；

使用 Invoke() 方法需要注意 3点：

1 ：它应该在 脚本的生命周期里的（Start、Update、OnGUI、FixedUpdate、LateUpdate）中被调用；

2：Invoke(); 不能接受含有 参数的方法；

3：在 Time.ScaleTime = 0; 时， Invoke() 无效，因为它不会被调用到

Invoke() 也支持重复调用：`InvokeRepeating("SendMsg", 2 , 3); `

这个方法的意思是指：**2 秒后调用SendMsg()方法**，并且之后**每隔 3 秒**调用一次 SendMsg () 方法



## 求两点之间的距离Distance

```c#
//比较a，b两点之间的距离返还给dist
float dist = Vector3.Distance(a.position, b.position);

```





## 平移动作MoveTowards()

用法：从**当前位置**移动到**指定位置**

```c#
transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
//从tramsform.position移动到targetpsition,移动速度是speed * Time.deltaTime)
```

注意不要写成从Start点到End点，否则会徘徊不前

***\*错误用法\****

transform.position = Vector3.MoveTowards(StartPos, EndPos, speed * Time.deltaTime);

- 即使速度极高，也会准确到达指定位置。无需担心超过。
- 到达后便不再继续移动。
- 匀速移动

**此代码应在Update函数中实现，需要被多次调用才能实现缓慢移动。**

**如果只调用一次，只能实现瞬间移动。**



## **角度旋转**

从一个角度匀速旋转到另一个角度 

```c#
transform.rotation = Quaternion.Lerp(_currentRotation, _targetRotation, speed * Time.deltaTime);
```



## 关于时间Time

**只读:**

**Time.time：**表示从游戏开发到现在的时间，会随着游戏的暂停而停止计算。

**Time.deltaTime：**表示从上一帧到当前帧的时间，以秒为单位。

**Time.unscaledDeltaTime：**不考虑timescale时候与deltaTime相同，若timescale被设置，则无效。

**Time.timeSinceLevelLoad：**表示从当前Scene开始到目前为止的时间，也会随着暂停操作而停止。

**Time.unscaledTime：**不考虑timescale时候与time相同，若timescale被设置，则无效。

**Time.fixedDeltaTime：**表示以秒计间隔，在物理和其他固定帧率进行更新，在Edit->ProjectSettings->Time的Fixed Timestep可以自行设置。

**Time.realtimeSinceStartup：** 表示自游戏开始后的总时间，即使暂停也会不断的增加。

**Time.frameCount：**总帧数

**可读可写:**

**Time.fixedTime：**表示以秒计游戏开始的时间，固定时间以定期间隔更新（相当于fixedDeltaTime）直到达到time属性。

**Time.SmoothDeltaTime：**表示一个平稳的deltaTime，根据前 N帧的时间加权平均的值。

**Time.timeScale：时间**缩放，默认值为1，若设置<1，表示时间减慢，若设置>1,表示时间加快，可以用来加速和减速游戏，非常有用。

**Time.captureFramerate：**表示设置每秒的帧率，然后不考虑真实时间。







## Tilemap

**规则瓦片**（快捷绘制地图）

1.安装(新版unity的package中会带有)

windows->package manager 左上角加号添加git：https://github.com/Unity-Technologies/2d-extras.git

完成后 in project会显示2D Tilemap Extras

在project中创建一个 `Rule Tile` 点击使用





## 获取对象组件

用Find查询

`GameObejct go = GameObject.Find("对象名").GetComponent<获取对象上面的组件>();`

用标签 Tag

` GameObejct go = GameObject.FindGameObjectWithTag("对象设置的tag值").GetComponent<获取对象上面的组件或者脚本>();`

用Type

`m_Palyer = GameObject.FindObjectOfType<直接获取类：class>();`





## 鼠标坐标转为世界坐标

```c#
			mouseScreenPos = Input.mousePosition;
       		mouseScreenPos.z = 10f; //z最终会变问0
     		mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        	Debug.Log("输入鼠标" + mouseWorldPos);
```





## 关于碰撞检测的一些bug

在碰撞检测的时候，可以把 Rigidbody2D 中的 CollisionDetection (碰撞检测) 从 Discre(离散的) 调为 Continuous(持续的) ，防止一些穿墙或者检测失误的情况

**<u>关于 OnTirggerStay2D()</u>** 

如果需要使用此函数时建议把需要发生碰撞的物体上的 Rigidbody2D 中的 SleepingMode(休眠模式) 从 StartAwake(开始唤醒) 调整为 NeverSleep(永不休眠) 

用于防止在此函数中检测时出现空挡期（即有几帧可能会无法检测到）



当一个父类中写的脚本中使用了 OnTiggerStay2D() 这个函数，并且子类中也有其他需要检测碰撞的函数会出现检测失灵的问题

例如：敌人的脚本中有 OnTiggerStay2D() 检测玩家是否进入范围，而子类有个敌人攻击脚本并且脚本中有OnTiggerEnter2D() 用于检测玩家造成伤害

可以尝试在 OnTiggerStay2D() 中进行 Debug ，在敌人进行攻击的时候会发现 OnTiggerStay2D() 函数会失灵，原因是因为子类在碰撞检测的时候，如果子类没有添加Rigidbody2D ，子类在触发碰撞时会引起父类触发碰撞，引起 OnTiggerStay2D() 检测变化

如果要防止这个问题，给子类添加 Rigidbody2D 即可

**整理：**

有Rigidbody组件的物体，当自身或子物体的碰撞器发生碰撞，会执行“OnCollisionEnter”；当自身或子物体的触发器发生碰撞，会执行“OnTriggerEnter”。

不过，给父物体和子物体分别添加RigidBody组件，在碰撞器和触发器执行时，会分别检测，调用对应函数，子物体只触发子物体回调，父物体只触发父物体回调。

如果父物体要屏蔽子物体碰撞器/触发器，可以给子物体添加Rigidbody。
