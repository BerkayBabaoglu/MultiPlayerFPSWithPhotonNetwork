using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text targetText;
    public AudioSource audio;
    private bool hasPlayedSound = false;
    private Color originalColor;

    private void Start()
    {
        if (targetText != null)
        {
            originalColor = targetText.color; //baslangictaki rengi kaydet
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetText != null)
        {
            Color color = targetText.color;
            color.a = 1f; // 1 = 255 (float formatinda)
            targetText.color = color;
        }

        if (!hasPlayedSound && audio != null)
        {
            audio.Play();
            hasPlayedSound = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetText != null)
        {
            targetText.color = originalColor;
        }

        if (audio != null)
        {
            audio.Stop();
            hasPlayedSound = false;
        }

    }

}