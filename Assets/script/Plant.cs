using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public string name;
    public int sunCost;
    public int moonCost;
    public int sunProduce;
    public int moonProduce;
    public float CO2Produce;
    public float O2Produce;
    public float CO2Cost;
    public float O2Cost;
    public float buffO2Persent;//用于计算氧气被buff影响的资源产出
    public float buffCO2Persent;//用于计算二氧化碳被buff影响的资源产出

    public bool live = false;//表示植物是否处于激活状态

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void changeLive()
    {
        live = !live;
    }

    void CostSun()
    {
        GameObject.Find("GameController").SendMessageUpwards("GrowConsumeSun", sunCost);

    }

    void CostMoon()
    {
        GameObject.Find("GameController").SendMessageUpwards("GrowConsumeMoon", moonCost);
    }


    public float GetO2Produce(float O2CO2Rate)
    {
        return O2Produce;
    }

    public float GetCO2Produce(float O2CO2Rate)
    {
        return CO2Produce;
    }

    public float GetSunProduce(float O2CO2Rate)
    {
        return sunProduce;
    }

    public float GetMoonProduce(float O2CO2Rate)
    {
        return moonProduce;
    }

}
