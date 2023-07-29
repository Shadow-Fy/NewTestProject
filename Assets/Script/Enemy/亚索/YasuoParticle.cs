using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YasuoParticle : MonoBehaviour
{
    public ParticleSystem particle;
    private bool _reduce;
    private float alphaMultiplier = 0.93f;
    private float alpha;
    public float alphaSet = 1;
    private void OnEnable()
    {
        alpha = alphaSet;
        var main = particle.main;
        main.startColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 1);
        Invoke("SetBool", 1.3f);
    }

    public void Start()
    {
    }

    public void Update()
    {
        DestroyThis();
    }

    public void SetBool()
    {
        _reduce = true;
    }

    public void DestroyThis()
    {
        if (_reduce)
        {
            var main = particle.main;
            alpha *= alphaMultiplier;
            main.startColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, alpha);
            if (alpha <= 0.0001f)
            {
                gameObject.SetActive(false);
                _reduce = false;
            }
        }
    }
}
