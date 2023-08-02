using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Clock
{
    public class Clock : Singleton<Clock>
    {
        float time = 0.0f;
        float clearTime = 0.0f;
        public float ClearTime{
            get{return clearTime;}
        }
        UnityAction action = null;

        protected override void Awake(){
            base.Awake();
            DontDestroyOnLoad(this);
        }

        private void Start() {
            action += Timing;    
        }

        void FixedUpdate(){
            action?.Invoke();
        }

        //计时开始
        public void StartTiming(){
            action += Timing;
        }
        
        //计时结束
        public void EndTiming(){
            action -= Timing;
            clearTime = Instance.time;
            time = 0.0f;
        }

        //计时暂停
        public void StopTiming(){
            action -= Timing;
            clearTime = Instance.time;
        }

        //计时函数
        void Timing(){
            Instance.time += Time.deltaTime;
            clearTime = Instance.time;
            Debug.Log(time);
        }
    }
}
