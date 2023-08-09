using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Clock{
    public class TimeUI : MonoBehaviour
    {
        public TMP_Text text;
        protected void FixedUpdate() {
            text.text = System.TimeSpan.FromSeconds(Clock.Instance.ClearTime).ToString(@"mm\:ss\:ff");
        }

        //Button Event
        public void StartTiming(){
            Clock.Instance.StartTiming();
        }

        //Button Event
        public void StopTiming(){
            Clock.Instance.StopTiming();
        }

        //Button Event
        public void EndTiming(){
            Clock.Instance.EndTiming();
        }
    }
}
