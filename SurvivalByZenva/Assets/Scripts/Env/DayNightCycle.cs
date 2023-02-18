using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time; // liczba o 0 do 1. 0 = 12 rano, 1 = 12 w nocy, 0,5 midday
    public float fullDayLength; 
    public float startTime = 0.4f; //kiedy zaczynamy grê to gra zaczyna sie o ktorej porze dnia
    private float timeRate;
    public Vector3 noon; //rotacja s³oñca w po³udnie

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Sun")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionsIntensityMultipler;

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    private void Update()
    {

        Debug.Log(sun.intensity);
        //plyniecie czasu
        time += timeRate * Time.deltaTime;

        if (time >= 1.0f)
            time = 0.0f;

        //rotacja siwatla
        sun.transform.eulerAngles = (time - 0.26f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        //intensywnosc swiatla
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        //zmiana koloru
        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);

        //wlacza / wylacza slonce
        if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(false);
        else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(true);


        //wlacza / wylacza ksiezyc
        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(false);
        else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(true);

        // lighting and reflections intensity
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionsIntensityMultipler.Evaluate(time);

    }
}
