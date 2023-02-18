using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine fadeAway;

    public void Flash()
    {
        if (fadeAway != null) //je�li FadeAway ju� istnieje
        {
            StopCoroutine(fadeAway);
        }

        image.enabled = true;
        image.color = Color.white;
        fadeAway = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float alfa = 1.0f;

        while (alfa > 0.0f)
        {
            alfa -= (1.0f / flashSpeed) * Time.deltaTime;
            image.color = new Color(1.0f, 1.0f, 1.0f, alfa);
            yield return null;
        }

        image.enabled = false;
    }
}
