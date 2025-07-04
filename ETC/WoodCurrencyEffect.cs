using System.Collections;
using ObjectPooling;
using UnityEngine;

public class WoodCurrencyEffect : PoolableMono
{
    public void StartEffect(float lifeDuration)
    {
        transform.localScale = Vector3.one;
        StartCoroutine(LifeCoroutine(lifeDuration));
    }

    private IEnumerator LifeCoroutine(float lifeDuration)
    {
        yield return new WaitForSeconds(lifeDuration);
        
        GameManager.Instance.AddWoodCurrency(1);
        PoolManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        transform.SetParent(null);
    }
}
