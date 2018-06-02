using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public float rotateSpeed = 100;       //设置旋转的速度
    public GameObject PlayerTrans;       //设置空物体的位置
    public float maxh = 10;               //设置提升的最高高度
    public GameObject spotlight;   
    public static GameObject aimGardenfield;
    private bool spotcanmove = true;
    static bool SelectAreaIsShowed = false;  //选择面板是否出现    
    public GameObject SelectArea;   //选择面板
    public GameObject plantbutton;  //种植按钮
   // public GameObject Noplantbutton;  //不种按钮
    public GameObject Repairbutton;  //修复按钮
    public GameObject UImesh;
    public Text plantName;
	private String[] prefabName = { "Prefabs/sunflower", "", "", "", "" };//预设路径
    private String[] Plantnamelist = { "sunflower", "", "", "", "" };

    void Start () {

        GameObject.Find("Main Camera").GetComponent<GameController>().RoundConsume();


    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(1))
        {
            rotatebasicpanel();
            SelectArea_Hide();
        }
        
        else if(SelectAreaIsShowed == false) //选择面板弹出时取消射线
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
			if(spotcanmove){
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
                    //已有花则不产生操作
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
                    else if(aimSkinmap[info[0], info[1]] == 1)
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
		   if(spotcanmove){
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
        if (GardenMap.mapstate[position[0], position[1]]>=1&& GardenMap.mapstate[position[0], position[1]] <= 5)
        {
            Debug.Log("到这了");
            Repairbutton.SetActive(false);
            plantName.text = Plantnamelist[GardenMap.mapstate[position[0], position[1]]-1];
            plantbutton.SetActive(true);
            //Noplantbutton.SetActive(true);
        }
        else if(GardenMap.mapstate[position[0], position[1]] == 6)
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

    public void NoPlantClicked() //不种
    {

        Debug.Log("不种"); 
        SelectArea_Hide();
    }

    public void RepairClicked() //耕地
    {
        GameController.Move -= 1;
        Debug.Log("耕地");
        SelectArea_Hide();
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
            prefabGameobject.transform.localScale = new Vector3(80, 80, 80);
            prefabGameobject.transform.localPosition = Vector3.zero;
        }

    }

}
