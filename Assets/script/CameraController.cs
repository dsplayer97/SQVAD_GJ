using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float rotateSpeed = 100;       //设置旋转的速度
    public GameObject PlayerTrans;       //设置空物体的位置
    public float maxh = 10;               //设置提升的最高高度
    public GameObject spotlight;   
    public static GameObject aimGardenfield;
    private bool spotcanmove = true;
    static bool SelectAreaIsShowed = false;  //选择面板是否出现    
    public GameObject SelectArea;   //选择面板
	 private String[] prefabName = { "Prefabs/sunflower", "", "", "", "" };//预设路径

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
                   
            if (Input.GetMouseButtonDown(0) && GameController.Move > 0)
            {
                aimGardenfield = gameObject;    
                int[,] aimSkinmap = GardenMap.skinMap;
                int[,] aimMapstate = GardenMap.mapstate;
                if(aimSkinmap[info[0],info[1]] == 0) //点击未探索土地
                {
                    GameController.Move -= 1; //本回合行动点减1
                    //执行土地蒙层撤销动画

                    //将土地状态变为1（已探索）
                    GardenMap.skinMap[info[0],info[1]] = 1;
                    Debug.Log("探索中");
                }else if(aimSkinmap[info[0], info[1]] == 1)
                {
                    //界面参数
                    //通过状态判断显示不同面板



                    spotcanmove = false;
                    SelectArea_Show();
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
    public void SelectArea_Show() //选择面板显示
    {
        SelectArea.SetActive(true);
        SelectAreaIsShowed = true;
    }
    public void SelectArea_Hide() //选择面板隐藏
    {
	    spotcanmove = true;
        SelectArea.SetActive(false);
        SelectAreaIsShowed = false;
    }
    public void PlantClicked() //种植物操作
    {
        GameController.Move -= 1;
        loadprefab(0);
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

public void loadprefab(int prefabtype)
    {
        Debug.Log("执行了");
        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        GameObject prefabGameobject = Instantiate(Resources.Load(prefabName[prefabtype])) as GameObject;
        //Instantiate(prefabGameobject);
        //Debug.Log(prefabGameobject.gameObject.name);
        prefabGameobject.GetComponent<Plant>().live = true;
        GameObject parentObject = aimGardenfield;
        prefabGameobject.transform.parent = parentObject.transform;
        //prefabGameobject.transform.localScale = Vector2.one;
        prefabGameobject.transform.localScale = new Vector3(80,80,80);
        prefabGameobject.transform.localPosition = Vector3.zero;

    }

}
