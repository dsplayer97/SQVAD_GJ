using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public float rotateSpeed = 100;       //设置旋转的速度
    public GameObject PlayerTrans;       //设置空物体的位置
    public float maxh = 10;               //设置提升的最高高度
    public GameObject spotlight;
    public static GameObject aimGardenfield;
    private bool spotcanmove = true;
    static bool SelectAreaIsShowed = false;  //选择面板是否出现 
    static bool Info_RemoveIsShowed = false;  //信息删除面板是否出现
    public GameObject SelectArea;   //选择面板
    public GameObject Info_RemovePanel;  //信息和删除按钮
    public GameObject Info_Text;  //信息文字
    public GameObject PlantInfo_Text;  //种花消费信息文字
    public GameObject plantbutton;  //种植按钮
                                    // public GameObject Noplantbutton;  //不种按钮
    public GameObject Repairbutton;  //修复按钮
    public GameObject UImesh;
    public Text plantName;
    private String[] prefabName = { "Prefabs/sunflower", "Prefabs/flower2", "Prefabs/flower3", "Prefabs/flower4", "Prefabs/flower5" };//预设路径
    private String[] Plantnamelist = { "sunflower", "flower2", "flower3", "flower4", "flower5" };

    void Start()
    {

        GameObject.Find("Main Camera").GetComponent<GameController>().RoundConsume();


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            rotatebasicpanel();
            SelectArea_Hide();
        }

        else if (SelectAreaIsShowed == false && Info_RemoveIsShowed == false) //选择面板弹出时取消射线
        {
            raydetect();
        }

    }


    //旋转平面函数
    private void rotatebasicpanel()
    {
        spotlight.SetActive(false);
        float nor = Input.GetAxis("Mouse X");//获取鼠标的偏移量
        PlayerTrans.transform.RotateAround(PlayerTrans.transform.position, -Vector3.up, Time.deltaTime * rotateSpeed * nor);//每帧旋转空物体，相机也跟随旋转
    }

    private void raydetect()
    {
        Ray Cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log("执行到这了");
        RaycastHit getGround;
        if (Physics.Raycast(Cameraray, out getGround))
        {

            GameObject gameObject = getGround.transform.gameObject;
            if (spotcanmove)
            {
                spotlight.transform.position = new Vector3(gameObject.transform.position.x, spotlight.transform.position.y, gameObject.transform.position.z);
                spotlight.SetActive(true);
            }
            int[] info = gameObject.GetComponent<touchtest>().testintarray;

            if (Input.GetMouseButtonDown(0))
            {
                aimGardenfield = gameObject;
                int[,] aimSkinmap = GardenMap.skinMap;
                int[,] aimMapstate = GardenMap.mapstate;
                Debug.Log(aimMapstate[info[0], info[1]]);
                if (aimMapstate[info[0], info[1]] >= 7 && aimMapstate[info[0], info[1]] <= 11)
                {
                    //已有花操作
                    spotcanmove = false;
                    Info_RemovePanel_Show(info);
                    Info_RemovePanel_Pos();
                    //Debug.Log(aimGardenfield.name);


                }
                else
                {
                    if (aimSkinmap[info[0], info[1]] == 0 && GameController.Move > 0) //点击未探索土地
                    {
                        GameController.Move -= 1; //本回合行动点减1
                                                  //执行土地蒙层撤销动画

                        //将土地状态变为1（已探索）
                        GardenMap.skinMap[info[0], info[1]] = 1;
                        Debug.Log("探索中");
                        //spotcanmove = false;
                        Debug.Log("info:" + info[0] + " " + info[1]);
                        //SelectArea_Show(info);
                    }
                    else if (aimSkinmap[info[0], info[1]] == 1 && aimMapstate[info[0], info[1]] != 0)
                    {
                        spotcanmove = false;
                        Debug.Log("info:" + info[0] + " " + info[1]);
                        SelectArea_Show(info);
                    }

                    /*SelectArea.SetActive(true);
                    Debug.Log(GardenMap.mapstate[info[0], info[1]]);
                    if (GardenMap.mapstate[info[0], info[1]] >= 1 && GardenMap.mapstate[info[0], info[1]] <= 5)
                    {
                        Debug.Log("到这了");
                        Repairbutton.SetActive(false);
                        plantName.text = Plantnamelist[GardenMap.mapstate[info[0], info[1]] - 1];
                        plantbutton.SetActive(true);
                        Noplantbutton.SetActive(true);
                    }
                    else if (GardenMap.mapstate[info[0], info[1]] == 6)
                    {
                        Repairbutton.SetActive(true);
                        plantbutton.SetActive(false);
                        Noplantbutton.SetActive(false);
                    }


                    SelectAreaIsShowed = true;*/
                    SelectArea_Pos();
                    Debug.Log("已探索");
                }
            }
            //Debug.Log(info[0] + "  " + info[1]);


        }
        else
        {
            if (spotcanmove)
            {
                spotlight.SetActive(false);
            }
            aimGardenfield = null;
        }
    }

    public void SelectArea_Pos() //设置选择面板为鼠标位置
    {
        SelectArea.transform.position = Input.mousePosition;
    }
    public void SelectArea_Show(int[] position) //选择面板显示
    {
        SelectArea.SetActive(true);
        Debug.Log(GardenMap.mapstate[position[0], position[1]]);
        if (GardenMap.mapstate[position[0], position[1]] >= 1 && GardenMap.mapstate[position[0], position[1]] <= 5)
        {
            Debug.Log("到这了");
            Repairbutton.SetActive(false);
            plantName.text = Plantnamelist[GardenMap.mapstate[position[0], position[1]] - 1];
            plantbutton.SetActive(true);
            PlantInfo_Text.SetActive(true);
            //植物消耗量显示
            //PlantInfo_Text.GetComponent<Text>().text = "太阳消耗：" + aimGardenfield.transform.GetChild(0).gameObject.GetComponent<Plant>().sunCost
            //                                          + "月亮消耗：" + aimGardenfield.transform.GetChild(0).gameObject.GetComponent<Plant>().moonCost
            //                                          + "结算回合：" + aimGardenfield.transform.GetChild(0).gameObject.GetComponent<Plant>().produceCD;

        }
        else if (GardenMap.mapstate[position[0], position[1]] == 6)
        {
            Repairbutton.SetActive(true);
            plantbutton.SetActive(false);
            //Noplantbutton.SetActive(false);
        }


        UImesh.SetActive(true);
        SelectAreaIsShowed = true;
    }
    public void SelectArea_Hide() //选择面板隐藏
    {
        spotcanmove = true;

        SelectArea.SetActive(false);
        SelectAreaIsShowed = false;
        UImesh.SetActive(false);

        Debug.Log("hide 执行");
    }

    public void Info_RemovePanel_Pos()  //信息和删除界面位置设置
    {
        Info_RemovePanel.transform.position = Input.mousePosition;
    }

    public void Info_RemovePanel_Show(int[] info)  //信息和删除界面显示
    {
        Info_RemovePanel.SetActive(true);


        Debug.Log("sunProduce" + aimGardenfield.transform.GetChild(0).gameObject.GetComponent<Plant>().sunProduce);
        GameObject sun = GameObject.Find("Sun"); GameObject moon = GameObject.Find("Moon");
        GameObject plant = aimGardenfield.transform.GetChild(0).gameObject;
        float O2CO2Rate = GameObject.Find("Main Camera").GetComponent<GameController>().O2 / (GameObject.Find("Main Camera").GetComponent<GameController>().CO2 + GameObject.Find("Main Camera").GetComponent<GameController>().O2);
        float[] sunMoonBuff = GameObject.Find("Main Camera").GetComponent<GameController>().SunMoonEffect(sun, moon, plant);
        //显示植物信息，待补充
        Info_Text.GetComponent<Text>().text = "太阳产出： " + plant.GetComponent<Plant>().GetSunProduce(O2CO2Rate) * sunMoonBuff[0]
                                            + "\n月亮产出： " + plant.GetComponent<Plant>().GetMoonProduce(O2CO2Rate) * sunMoonBuff[1]
                                            + "\n清算回合：" + plant.GetComponent<Plant>().produceCD
                                            + "\n二氧化碳产出：" + plant.GetComponent<Plant>().GetCO2Produce(O2CO2Rate) * sunMoonBuff[3]
                                            + "\n二氧化碳消耗：" + plant.GetComponent<Plant>().CO2Cost
                                            + "\n氧气产出：" + plant.GetComponent<Plant>().GetO2Produce(O2CO2Rate) * sunMoonBuff[2]
                                            + "\n氧气消耗：" + plant.GetComponent<Plant>().O2Cost;


        UImesh.SetActive(true);
        Info_RemoveIsShowed = true;



    }

    public void Info_RemovePanel_Hide()  //信息和删除界面隐藏
    {
        spotcanmove = true;
        Info_RemovePanel.SetActive(false);
        Info_RemoveIsShowed = false;
        UImesh.SetActive(false);
    }


    public void PlantClicked() //种植物操作
    {
        if (GameController.Move >= 1)
        {
            GameController.Move -= 1;
            loadprefab();
        }
        Debug.Log("种");
        SelectArea_Hide();

    }

    public void RemoveClicked()  //删除植物
    {
        if (GameController.Move >= 1)
        {
            GameController.Move -= 1;

            int[] info = aimGardenfield.GetComponent<touchtest>().testintarray;
            int[,] Remove_aimMapstate = GardenMap.mapstate;
            Remove_aimMapstate[info[0], info[1]] = 6;

            Destroy(aimGardenfield.transform.GetChild(0).gameObject);

        }
        Debug.Log("删");
        Info_RemovePanel_Hide();
    }

    public void NoPlantClicked() //不种
    {

        Debug.Log("不种");
        SelectArea_Hide();
    }

    public void RepairClicked() //耕地
    {
        if (GameController.Move >= 1)
        {
            GameController.Move -= 1;

            int[] info = aimGardenfield.GetComponent<touchtest>().testintarray;
            int[,] Remove_aimMapstate = GardenMap.mapstate;
            Remove_aimMapstate[info[0], info[1]] = 0;
            Debug.Log("耕地");

            SelectArea_Hide();
        }

    }

    public void loadprefab()
    {
        //Debug.Log("执行了");
        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        int[] position = aimGardenfield.GetComponent<touchtest>().testintarray;
        if (GardenMap.mapstate[position[0], position[1]] != 0)
        {
            GameObject prefabGameobject = Instantiate(Resources.Load(prefabName[GardenMap.mapstate[position[0], position[1]] - 1])) as GameObject;

            //Instantiate(prefabGameobject);
            //Debug.Log(prefabGameobject.gameObject.name);

            prefabGameobject.GetComponent<Plant>().live = true;
            prefabGameobject.GetComponent<Plant>().CostSun();
            prefabGameobject.GetComponent<Plant>().CostMoon();
            prefabGameobject.GetComponent<Plant>().Grow(position[0], position[1]);

            //判断种子类型，更改地图状态
            switch (GardenMap.mapstate[position[0], position[1]])
            {
                case 1:
                    GardenMap.mapstate[position[0], position[1]] = 7;
                    break;
                case 2:
                    GardenMap.mapstate[position[0], position[1]] = 8;
                    break;
                case 3:
                    GardenMap.mapstate[position[0], position[1]] = 9;
                    break;
                case 4:
                    GardenMap.mapstate[position[0], position[1]] = 10;
                    break;
                case 5:
                    GardenMap.mapstate[position[0], position[1]] = 11;
                    break;
                default:
                    break;
            }

            GameObject parentObject = aimGardenfield;

            prefabGameobject.transform.parent = parentObject.transform;
            //prefabGameobject.transform.localScale = Vector2.one;
            //prefabGameobject.transform.localScale = new Vector3(80, 80, 80);
            prefabGameobject.transform.localPosition = Vector3.zero;
        }

    }

}