using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    private List<Image> _transitionImages = new List<Image>();
        
    private void Awake()
    {
        _transitionImages = GetComponentsInChildren<Image>().ToList();
    }
    
    public void TransitionScreen(Action transCallback, Action endCallback)
    {
        StartCoroutine(TransitionCoroutine(transCallback, endCallback));
    }

    private IEnumerator TransitionCoroutine(Action transCallback, Action endCallback)
    {
        foreach (var image in _transitionImages)
        {
            DOTween.To(() => image.rectTransform.anchoredPosition,
                x => image.rectTransform.anchoredPosition = x,
                new Vector2(0, image.rectTransform.anchoredPosition.y), 0.6f)
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.07f);
        }
        
        yield return new WaitForSeconds(0.6f);
        transCallback?.Invoke();
        yield return new WaitForSeconds(0.35f);
        
        foreach (var image in _transitionImages)
        {
            DOTween.To(() => image.rectTransform.anchoredPosition,
                    x => image.rectTransform.anchoredPosition = x,
                    new Vector2(-2060f, image.rectTransform.anchoredPosition.y), 0.6f)
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.07f);
        }

        yield return new WaitForSeconds(0.8f);
        endCallback?.Invoke();
        
        _transitionImages.ForEach(image => image.transform.localPosition = new Vector3(2060f ,image.transform.localPosition.y));
    }
}
