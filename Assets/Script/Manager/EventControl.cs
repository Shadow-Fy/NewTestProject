using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventControl : Singleton<EventControl>
{
    public Action<float, float> PlayerHurtEvent;
    public Action<float, float> PLayerInitEvent;
    public Action<int, int> PlayerGunEvent;


    public void InitEvent(float currenthealth, float maxhealth)
    {
        PLayerInitEvent?.Invoke(currenthealth, maxhealth);
    }

    public void GetHitEvent(float currenthealth, float maxhealth)
    {
        PlayerHurtEvent?.Invoke(currenthealth, maxhealth);
    }

    public void GunEvent(int lastcount, int count)
    {
        PlayerGunEvent?.Invoke(lastcount, count);
    }
}
