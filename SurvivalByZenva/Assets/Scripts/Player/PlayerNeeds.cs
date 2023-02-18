using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour, IDamagable
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float noHungerHealthDecay;
    public float noThirsthealthDecay;

    public UnityEvent onTakeDamage;

    private void Start()
    {
        //wartoœci startowe 
        health.curValue = health.startVlue;
        hunger.curValue = hunger.startVlue;
        thirst.curValue = thirst.startVlue;
        sleep.curValue = sleep.startVlue;
    }

    private void Update()
    {
        //zmniejszanie wartoœci wraz z czasem
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);

        //zabieranie hp jeœli jedno z nich jest równe zeru
        if (hunger.curValue == 0.0f)
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        if (thirst.curValue == 0.0f)
            health.Subtract(noThirsthealthDecay * Time.deltaTime);

        //sprawdzenie, czy gracz zyje
        if (health.curValue == 0.0f)
            Die();

        //aktualizowanie UI wskaznikow
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        thirst.uiBar.fillAmount = thirst.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();
    }


    public void Heal (float amount)
    {
        health.Add(amount);
    }

    public void Eat (float amount)
    {
        hunger.Add(amount);
    }

    public void Drinkg (float amount)
    {
        thirst.Add(amount);
    }

    public void Sleep (float amount)
    {
        sleep.Add(amount);
    }

    public void TakePhysicaldamage (int amount)
    {
        health.Subtract(amount);
        onTakeDamage?.Invoke(); // "?" oznacza, ¿e jesli ine mamy nic przypisanego do onTakeDamage to daje nam error (jesli jest null)
    }

    public void Die()
    {
        Debug.Log("Died");
    }

}

[System.Serializable]
public class Need
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float startVlue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    // add to the need
    public void Add (float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue); //leczac sie, hp nie bedzie wieksze niz maxValue
    }

    // subtract from the need
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f); //tracac hp nigdy nie bedzie mniejsze niz 0.0f
    }

    // return the percentage value (0.0 - 1.0)
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}

public interface IDamagable
{
    void TakePhysicaldamage(int damageAmount);
}
