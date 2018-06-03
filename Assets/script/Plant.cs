using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public GameObject gameController;
    private Animator animator;
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

    public int hp;//植物的生命值, 感染状态下每回合减1, 减到0植物死亡
    public bool live;//表示植物是否处于激活状态
    public bool infected;//表示植物是否处于被感染状态, true表示感染, false表示健康

    private MyPoint point; public MyPoint GetPoint() { return point; }
    public void SetPoint(MyPoint _point) { point = _point; } //表示植物的坐标 


    void Awake() {


    }

    // Use this for initialization
    void Start()
    {
        //gameController = GameObject.Find("GameController");
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(new Vector3(Camera.main.transform.position.x,0, Camera.main.transform.position.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(Camera.main.transform.position - this.transform.position), 0);

        if (infected)
        {
            animator.SetBool("ill", true);

        }
        else
        {
            animator.SetBool("ill", false);
        }
        
    }

    
    public void ChangeLive() {
        live = !live;
    }



    //植物开始生长, 初始化
    public void Grow(int x, int y)
    {
        point = new MyPoint(x, y);
        live = true;
        infected = false;
        hp = 5;
        bugDebuffPersent = 1;
    }

    //被害虫感染
    public void Infect()
    {
        infected = true;
        bugDebuffPersent = 0.2f;
        Debug.Log("我被感染拉!" + this.point.ToString());
    }

    //植物恢复健康

    public void Cure() {

        hp = 5;
        infected = false;
        bugDebuffPersent = 1;
        Debug.Log("我被治好了" + point.ToString());
    }

    //hp减1, 减到0就死亡
    public bool HpDown() {
        hp = hp - 1;
        if (hp == 0)
        {
            this.live = false;
            animator.SetBool("die", true);
            return true;
        }
        else {
            return false;
        }
    }


    //建造植物时消耗阳光
    public void CostSun()
    {
        //GameObject.Find("Main Camera").SendMessageUpwards("GrowConsumeSun", sunCost);
        GameObject.Find("Main Camera").GetComponent<GameController>().GrowConsumeSun(sunCost);
    }

    //建造植物时消耗月亮
    public void CostMoon()
    {
        //GameObject.Find("Main Camera").SendMessageUpwards("GrowConsumeMoon", moonCost);
        GameObject.Find("Main Camera").GetComponent<GameController>().GrowConsumeMoon(moonCost);
    }

    //根据氧气二氧化碳比例和是否被害虫感染返回氧气产出



    public float GetO2Produce(float O2CO2Rate) {
        if (O2CO2Rate > 0.7)
        {
            return O2Produce * bugDebuffPersent * 0.5f;
        }
        else if (O2CO2Rate < 0.3)
        {
            return O2Produce * bugDebuffPersent * 1.5f;
        }
        else
        {
            return O2Produce * bugDebuffPersent;
        }
    }

    public float GetCO2Produce(float O2CO2Rate) {
        if (O2CO2Rate > 0.7)
        {
            return CO2Produce * bugDebuffPersent * 1.5f;
        }
        else if (O2CO2Rate < 0.3)
        {
            return CO2Produce * bugDebuffPersent * 0.5f;
        }
        else
        {
            return CO2Produce * bugDebuffPersent;
        }
    }

    public float GetSunProduce(float O2CO2Rate) {
        animator.SetBool("produce", true);
        animator.SetBool("produce", false);
        if (O2CO2Rate < 0.1)
        {
            return sunProduce * bugDebuffPersent * 0.2f;
        }

        if (O2CO2Rate < 0.3) {

            return sunProduce * bugDebuffPersent * 0.5f;
        }


        return sunProduce * bugDebuffPersent;

    }



    public float GetMoonProduce(float O2CO2Rate) {
        if (O2CO2Rate > 0.9)
        {
            return moonProduce * bugDebuffPersent * 0.2f;
        }

        if (O2CO2Rate > 0.7)
        {
            return moonProduce * bugDebuffPersent * 0.5f;
        }

        return moonProduce * bugDebuffPersent;
    }

}
