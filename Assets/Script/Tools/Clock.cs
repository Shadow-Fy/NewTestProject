using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Clock
{
    public class Clock : MonoBehaviour
    {
        float time = 0.0f;
        public float ClearTime{
            get{return time;}
        }
        // Update is called once per frame
        UnityAction action = null;

        private void Start() {
            action += Timing;    
        }

        void FixedUpdate(){
            action?.Invoke();
        }

        public void StartTiming(){
            time = 0.0f;
            action += Timing;
        }
        
        public void StopTiming(){
            action -= Timing;
        }

        void Timing(){
            time += Time.deltaTime;
            Debug.Log(time);
        }
    }
}
