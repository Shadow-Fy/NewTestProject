# BUG



## 关于地图TileMap的问题

### 复合器问题

对于一个需要添加 `TilemapCollider2D`  组件的地图，为了防止穿过地图的缝隙，一般会添加**复合碰撞组件 `CompositeCollider2D`**

添加 `CompositeCollider2D` 组件后会自动添加一个 `Rigidbody2D` 组件，**这个组件需要把 `Body Type` 设置为Static(静态)，否则`Tilemap`会掉落**

并且在 `CompositeCollider2D` 中 需要**把 `Offset Distance` 数值改为0**，否则地面会有倾斜，在player移动的过程中动画可能会出现bug



### 关于 `Tile Palate` 中素材和格子错位的问题

错位问题如下图所示：

<img src="https://shadow-fy.oss-cn-chengdu.aliyuncs.com/img/202206052309006.png" alt="image-20220605230925921" style="zoom:67%;" /> 

素材没有完整布满格子



+ 解决办法

问题出现的原因是因为在切割图片时 `pivot` 没有选择为 `center` ，如图改为 `center` 后正常显示

<img src="https://shadow-fy.oss-cn-chengdu.aliyuncs.com/img/202206052313805.png" alt="image-20220605231315725" style="zoom: 50%;" /> 





## API  lookAt

当实现一个物体朝向一个点时，通常使用transform.LookAt。对于3D物体没有问题，但是对于2D物体，会出现奇怪的效果，这样因为API的原理，它的官方解释是这样的：旋转自身，使得当前对象的正Z轴指向目标对象target所在的位置。但是对于2D物体的朝向，我们的目的就是要改变Z轴，通过transform.LookAt只能改变物体的X，Y轴

