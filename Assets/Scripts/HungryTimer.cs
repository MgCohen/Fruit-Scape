using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungryTimer : MonoBehaviour
{
    public float MaxTimer = 10f;
    public float MinTimer = 1f;
    [SerializeField] private float currentTimer;

    public float TickCost = 1;

    public int HungryLevel = 0;

    public int hungryIncrease;

    public Slider slide;

    private void Start()
    {
        currentTimer = MaxTimer;
        slide.maxValue = currentTimer;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0 && Manager.instance.player.EnableInput)
        {
            Manager.instance.PassTurn();
            HungryLevel -= 10;
            HungryLevel = Mathf.Clamp(HungryLevel, 0, 100);
            currentTimer = MaxTimer - ((HungryLevel) / 100) * (MinTimer - MaxTimer);
            slide.maxValue = currentTimer;
        }
        slide.value = currentTimer;
    }

    public void Tick()
    {
        currentTimer -= TickCost;
    }

    public void Eat()
    {
        currentTimer = MaxTimer - (((HungryLevel) / 100) * (MinTimer - MaxTimer));
        HungryLevel += hungryIncrease;
        HungryLevel = Mathf.Clamp(HungryLevel, 0, 100);
    }

    public void Stop()
    {
        enabled = false;
    }
}
