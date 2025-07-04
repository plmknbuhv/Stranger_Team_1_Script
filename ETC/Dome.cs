using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Dome : MonoBehaviour
{
    private readonly int glowValueHesh = Shader.PropertyToID("_GlowValue");
    [SerializeField] private HpBar hpBar;
    public Health healthCompo = new Health();
    
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
        
        healthCompo.SetHealth(1000);
        hpBar.SetHpBar(healthCompo.MaxHealth, healthCompo.MaxHealth);
        
        healthCompo.OnDamageEvent += (next, prev) =>
        {
            hpBar.SetHpBar(healthCompo.MaxHealth, next);
            StartCoroutine(GlowCoroutine());
        };
        
        healthCompo.OnDeadEvent += GameOver.Instance.OveredGame;
    }

    private IEnumerator GlowCoroutine()
    {
        DOTween.To(value => _material.SetFloat(glowValueHesh, value), 0.35f, 2, 0.15f);
        yield return new WaitForSeconds(0.15f);
        DOTween.To(value => _material.SetFloat(glowValueHesh, value), 2f, 0.35f, 0.21f);
    }
}
