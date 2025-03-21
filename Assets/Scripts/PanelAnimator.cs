using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PanelAnimator : MonoBehaviour
{
    public RectTransform panelRectTransform;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float scaleDownDuration = 1f;
    public Vector2 targetScale = new Vector2(1.2f, 1.2f); // Panelin büyüyeceði boyut

    private Vector2 originalScale;

    void Start()
    {
        originalScale = panelRectTransform.localScale;
    }

    public void AnimatePanel()
    {
        if(!this.gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        Debug.Log("geldi");
        // Panelin baþlangýçta küçük olmasýný saðla
        panelRectTransform.localScale = Vector2.zero;

        // Panelin yavaþça ekrana gelmesi 
        panelRectTransform.DOScale(targetScale, fadeInDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Panelin küçülerek kaybolmasý 
                panelRectTransform.DOScale(Vector2.zero, scaleDownDuration)
                    .SetEase(Ease.InSine)
                    .SetDelay(1f); 
            });
    }
    

    
}