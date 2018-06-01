using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float sunPower;
    public float moonPower;
    public float O2;
    public float CO2;

    private float O2CO2Rate;

    private GameObject[] plantList;

    public static int Round; //回合

    public static int Move; //行动

    // Use this for initialization
    void Start()
    {
        Move = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RoundConsume();
        }
    }

    public void RoundConsume()
    {
        float totalO2Consume = 0;
        float totalCO2Consume = 0;
        float totalO2Produce = 0;
        float totalCO2Produce = 0;
        float totalSunProduce = 0;
        float totalMoonProduce = 0;
        float O2CO2TotalRate = 0;
        O2CO2Rate = O2 / CO2;
        plantList = GetAllPlant();
        foreach (GameObject i in plantList)
        {
            Plant p = i.GetComponent<Plant>();
            if (Plant.live)
            {
                totalO2Consume = totalO2Consume + p.O2Cost;
                totalCO2Consume = totalCO2Consume + p.CO2Cost;
                totalO2Produce = totalO2Produce + p.GetO2Produce(O2CO2Rate);
                totalCO2Produce = totalCO2Produce + p.GetCO2Produce(O2CO2Rate);
                totalSunProduce = totalSunProduce + p.GetSunProduce(O2CO2Rate);
                totalMoonProduce = totalMoonProduce + p.GetMoonProduce(O2CO2Rate);
            }
        }
        O2 = O2 - totalO2Consume + totalO2Produce;
        CO2 = CO2 - totalCO2Consume + totalCO2Produce;

        sunPower = sunPower + totalSunProduce;
        moonPower = moonPower + totalMoonProduce;

        O2CO2TotalRate = O2 / (O2 + CO2);

        GameObject.Find("UIController").GetComponent<UIControl>().ChangeSunMoon(sunPower, moonPower);
        GameObject.Find("UIController").GetComponent<UIControl>().ChangeAir(O2CO2TotalRate);

        Debug.Log("O2:" + O2.ToString());
        Debug.Log("CO2:" + CO2.ToString());
        Debug.Log("SunPower:" + sunPower.ToString());
        Debug.Log("MoonPower:" + moonPower.ToString());

        Round += 1;
        Move = 3;
        UIControl.In_Round = true;
    }

    //建造植物时消耗太阳能量
    void GrowConsumeSun(int delta)
    {
        sunPower = sunPower - delta;
    }

    //建造植物时消耗月亮能量
    void GrowConsumeMoon(int delta)
    {
        moonPower = moonPower - delta;
    }

    GameObject[] GetAllPlant()
    {
        return GameObject.FindGameObjectsWithTag("Plant");
    }
    


}
