using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public  float sunPower;
    public  float moonPower;
    public float O2;
    public float CO2;
    private float alpha = 0;

    public int round;

    private float O2CO2Rate;

    public GameObject[] plantList;

    public GameObject targetMap;
    public GameObject bugController;
    private GardenMap gardenMap; public GardenMap GetGardenMap() { return gardenMap; }



    public static int Move; //行动

    void Awake() {
        bugController = GameObject.Find("Main Camera");
        round = 0;
    }

    // Use this for initialization
    void Start()
    {
        Move = 3;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            RoundConsume();
        }*/
    }

    //回合结束时进行资源结算
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
        GameObject sun = GameObject.Find("sun");
        GameObject moon = GameObject.Find("moon");
        foreach (GameObject i in plantList)
        {
            Plant p = i.GetComponent<Plant>();

            float[] sunMoonBuff = SunMoonEffect(sun, moon, i);
            
            //被感染的植物要减血, 减到0销毁植物
            if (p.infected)
            {
                if (p.HpDown())
                {   
                    switch (GardenMap.mapstate[p.GetPoint().GetX(), p.GetPoint().GetY()])
                    {
                        case 7:
                            GardenMap.mapstate[p.GetPoint().GetX(), p.GetPoint().GetY()] = 1;
                            break;
                        case 8:
                            GardenMap.mapstate[p.GetPoint().GetX(), p.GetPoint().GetY()] = 2;
                            break;
                        case 9:
                            GardenMap.mapstate[p.GetPoint().GetX(), p.GetPoint().GetY()] = 3;
                            break;
                        default:
                            GardenMap.mapstate[p.GetPoint().GetX(), p.GetPoint().GetY()] = 6;
                            break;
                    }
                    Destroy(i);
                    bugController.GetComponent<BugController>().removeBug(p.GetPoint());
                }
            }
            if (p.live)
            {
                totalO2Consume = totalO2Consume + p.O2Cost;
                totalCO2Consume = totalCO2Consume + p.CO2Cost;
                totalO2Produce = totalO2Produce + p.GetO2Produce(O2CO2Rate) * sunMoonBuff[2];
                totalCO2Produce = totalCO2Produce + p.GetCO2Produce(O2CO2Rate) * sunMoonBuff[3];
                if (round % p.produceCD == 0)
                {
                    totalSunProduce = totalSunProduce + p.GetSunProduce(O2CO2Rate) * sunMoonBuff[0];
                    totalMoonProduce = totalMoonProduce + p.GetMoonProduce(O2CO2Rate) * sunMoonBuff[1];
                }
            }
            

        }
        O2 = O2 - totalO2Consume + totalO2Produce;
        CO2 = CO2 - totalCO2Consume + totalCO2Produce;

        sunPower = sunPower + totalSunProduce;
        moonPower = moonPower + totalMoonProduce;

       

        O2CO2TotalRate = O2 / (O2 + CO2);

        GameObject.Find("Main Camera").GetComponent<UIControl>().ChangeSunMoon(sunPower, moonPower);
        GameObject.Find("Main Camera").GetComponent<UIControl>().ChangeAir(O2CO2TotalRate);

        //Debug.Log("O2:" + O2.ToString());
        //Debug.Log("CO2:" + CO2.ToString());
        //Debug.Log("SunPower:" + sunPower.ToString());
        //Debug.Log("MoonPower:" + moonPower.ToString());

        round += 1;
        Move = 3;
        UIControl.In_Round = true;
    }

    //根据太阳月亮与植物的距离计算植物的产物的增益
    //数组角标0,1,2,3分别影响太阳月亮氧气二氧化碳产量
    public float[] SunMoonEffect(GameObject sun, GameObject moon, GameObject plant) {
        float[] buff = { 1, 1, 1, 1 };
        float sunDistance = Mathf.Sqrt(Mathf.Pow((sun.transform.position.x - plant.transform.position.x), 2) +
            Mathf.Pow((sun.transform.position.y - plant.transform.position.y), 2) +
            Mathf.Pow((sun.transform.position.z - plant.transform.position.z), 2));
        float moonDistance = Mathf.Sqrt(Mathf.Pow((moon.transform.position.x - plant.transform.position.x), 2) +
            Mathf.Pow((moon.transform.position.y - plant.transform.position.y), 2) +
            Mathf.Pow((moon.transform.position.z - plant.transform.position.z), 2));
        if (sunDistance < moonDistance){
            //Debug.Log("sun:" + sunDistance.ToString());
            buff[0] = Mathf.Sqrt((100 - sunDistance) / 100 + 1);
            buff[1] = Mathf.Sqrt(sunDistance / 100);
            buff[2] = Mathf.Sqrt((100 - sunDistance) / 100 + 1);
            buff[3] = Mathf.Sqrt(sunDistance / 100);
        }
        else if (moonDistance < sunDistance) {
            //Debug.Log("moon:" + moonDistance.ToString());
            buff[0] = Mathf.Sqrt(moonDistance / 100);
            buff[1] = Mathf.Sqrt((100 - moonDistance) / 100 + 1);
            buff[2] = Mathf.Sqrt(moonDistance / 100);
            buff[3] = Mathf.Sqrt((100 - moonDistance) / 100 + 1);            
        }
        return buff;
    }

    //建造植物时消耗太阳能量
    public void GrowConsumeSun(int delta){
        Debug.Log("sun:"+delta);
        sunPower = sunPower - delta;
    }

    //建造植物时消耗月亮能量
    public void GrowConsumeMoon(int delta){
        moonPower = moonPower - delta;
    }



    public GameObject[] GetAllPlant() {
        return GameObject.FindGameObjectsWithTag("Plant");
    }



    //获取当前地图上哪个坐标长着植物
    public List<MyPoint> FindPlant()
    {
        int[,] skinMap = GardenMap.skinMap;
        int[,] mapState = GardenMap.mapstate;
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

    public void StartInfect() {
        if (round % 5 == 0)
        {
            for (int i = 0; i < round / 5; i++)
            {
                bugController.GetComponent<BugController>().Infect();
            }
        }
        bugController.GetComponent<BugController>().SpreadInfect();
    }


    public void SunBurst() {
        sunPower = sunPower - 300;
        O2 = 500;
        CO2 = 500;
        float O2CO2TotalRate = O2 / (O2 + CO2);
        GameObject.Find("Main Camera").GetComponent<UIControl>().ChangeSunMoon(sunPower, moonPower);
        GameObject.Find("Main Camera").GetComponent<UIControl>().ChangeAir(O2CO2TotalRate);
    }

    public void MoonSword(MyPoint point) {
        moonPower = moonPower - 300;
        bugController.GetComponent<BugController>().KillBug(point);
        Debug.Log("MoonSwordActive");
    }

}
