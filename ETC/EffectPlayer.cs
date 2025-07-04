using System.Collections;
using ObjectPooling;
using UnityEngine;

public class EffectPlayer : PoolableMono
{
    private ParticleSystem _particle;
    private float _duration;
    private WaitForSeconds _paricleDuration;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
        _duration = _particle.main.duration;
        _paricleDuration = new WaitForSeconds(_duration);
    }

    public void SetPositionAndPlay(Vector3 position)
    {
        transform.position = position;
        _particle.Play();
        StartCoroutine(DelayAndGotoPoolCoroutine());
    }

    private IEnumerator DelayAndGotoPoolCoroutine()
    {
        yield return _paricleDuration;
        _particle.Stop();
        _particle.Simulate(0);
        PoolManager.Instance.Push(this);
    }

    public override void ResetItem() { }
}