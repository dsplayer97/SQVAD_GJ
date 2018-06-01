using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class Plant : MonoBehaviour {

    public GameObject gameController;

=======
public class Plant : MonoBehaviour
{
>>>>>>> e02fd8718296b9d618b3129b3581453b5e23fe97
    public string name;
    public int sunCost;
    public int moonCost;
    public int sunProduce;
    public int moonProduce;
    public float CO2Produce;
    public float O2Produce;
    public float CO2Cost;
    public float O2Cost;
    public int produceCD;//表示几回合产生一次太阳和月亮
    public float buffO2Persent;//用于计算氧气被buff影响的资源产出
    public float buffCO2Persent;//用于计算二氧化碳被buff影响的资源产出
    private float bugDebuffPersent;//表示害虫感染后的产出debuff

<<<<<<< HEAD
    public bool live;//表示植物是否处于激活状态
    public bool infected;//表示植物是否处于被感染状态, true表示感染, false表示健康

    private MyPoint point; public MyPoint GetPoint() { return point; } public void SetPoint(MyPoint _point) { point = _point; } //表示植物的坐标 

    

    // Use this for initialization
    void Start () {
        //gameController = GameObject.Find("GameController");
    }
	
	// Update is called once per frame
	void Update () {
=======
    public static bool live;//表示植物是否处于激活状态

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
>>>>>>> e02fd8718296b9d618b3129b3581453b5e23fe97


<<<<<<< HEAD
    
    public void ChangeLive() {
        live = !live;
    }

    //植物开始生长, 初始化
    public void Grow(int x, int y) {
        point = new MyPoint(x, y);
        live = true;
        infected = false;
        bugDebuffPersent = 1;
    }

    //被害虫感染
    public void Infect() {
        infected = true;
        bugDebuffPersent = 0.5f;
    }

    //植物恢复健康
    public void Cure() {
        infected = false;
        bugDebuffPersent = 1;
    }

    //建造植物时消耗阳光
    void CostSun() {
=======
    public void changeLive()
    {
        live = !live;
    }

    void CostSun()
    {
>>>>>>> e02fd8718296b9d618b3129b3581453b5e23fe97
        GameObject.Find("GameController").SendMessageUpwards("GrowConsumeSun", sunCost);

    }

<<<<<<< HEAD
    //建造植物时消耗月亮
    void CostMoon() {
        GameObject.Find("GameController").SendMessageUpwards("GrowConsumeMoon", moonCost);
    }

    //根据氧气二氧化碳比例和是否被害虫感染返回氧气产出
    public float GetO2Produce(float O2CO2Rate) {
        return O2Produce * bugDebuffPersent;
    }

    public float GetCO2Produce(float O2CO2Rate) {
        return CO2Produce * bugDebuffPersent;
    }

    public float GetSunProduce(float O2CO2Rate) {
        return sunProduce * bugDebuffPersent;
    }

    public float GetMoonProduce(float O2CO2Rate) {
        return moonProduce * bugDebuffPersent;
=======
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
>>>>>>> e02fd8718296b9d618b3129b3581453b5e23fe97
    }

}
