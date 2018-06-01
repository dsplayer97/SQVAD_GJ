﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float sunPower;
    public float moonPower;
    public float O2;
    public float CO2;

    public int round;

    private float O2CO2Rate;

    private GameObject[] plantList;

    public GameObject targetMap;
    private GardenMap gardenMap; public GardenMap GetGardenMap() { return gardenMap; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RoundConsume();
        }
    }

    //回合结束时进行资源结算
    void RoundConsume() {
        float totalO2Consume = 0;
        float totalCO2Consume = 0;
        float totalO2Produce = 0;
        float totalCO2Produce = 0;
        float totalSunProduce = 0;
        float totalMoonProduce = 0;
        O2CO2Rate = O2 / CO2;
        plantList = GetAllPlant();
        foreach (GameObject i in plantList) {
            Plant p = i.GetComponent<Plant>();
            if (p.live){
                totalO2Consume = totalO2Consume + p.O2Cost;
                totalCO2Consume = totalCO2Consume + p.CO2Cost;
                totalO2Produce = totalO2Produce + p.GetO2Produce(O2CO2Rate);
                totalCO2Produce = totalCO2Produce + p.GetCO2Produce(O2CO2Rate);
                if (round % p.produceCD == 0)
                {
                    totalSunProduce = totalSunProduce + p.GetSunProduce(O2CO2Rate);
                    totalMoonProduce = totalMoonProduce + p.GetMoonProduce(O2CO2Rate);
                }
            }
        }
        O2 = O2 - totalO2Consume + totalO2Produce;
        CO2 = CO2 - totalCO2Consume + totalCO2Produce;
        sunPower = sunPower + totalSunProduce;
        moonPower = moonPower + totalMoonProduce;

        gardenMap = targetMap.GetComponent<GardenMap>();

        Debug.Log("O2:" + O2.ToString());
        Debug.Log("CO2:" + CO2.ToString());
        Debug.Log("SunPower:" + sunPower.ToString());
        Debug.Log("MoonPower:" + moonPower.ToString());
    }

    //建造植物时消耗太阳能量
    void GrowConsumeSun(int delta)
    {
        sunPower = sunPower - delta;
    }

    //建造植物时消耗月亮能量
    void GrowConsumeMoon(int delta) {
        moonPower = moonPower - delta;
    }

    public GameObject[] GetAllPlant() {
        return GameObject.FindGameObjectsWithTag("Plant");
    }


    //获取当前地图上哪个坐标长着植物
    public List<MyPoint> FindPlant()
    {
        gardenMap = targetMap.GetComponent<GardenMap>();
        int[,] skinMap = gardenMap.GetSkinMap();
        int[,] mapState = gardenMap.GetMapState();
        List<MyPoint> points = new List<MyPoint>();
        for (int i = 0; i < skinMap.GetLength(0); i++)
        {
            for (int j = 0; j < skinMap.GetLength(1); j++)
            {
                if (skinMap[i, j] == 1 && mapState[i, j] >= 7 && mapState[i, j] <= 11)
                {
                    MyPoint point = new MyPoint(i, j);
                    points.Add(point);
                }
            }
        }
        return points;
    }
}