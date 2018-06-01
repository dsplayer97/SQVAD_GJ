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

    private String[] prefabName = { "", "", "", "", "" };//预设路径

    public GameObject SelectArea;//选择面板

    void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(1))
        {
            rotatebasicpanel();
        }
        else
        {
            raydetect();
        }

    }


    //旋转平面函数
    private void rotatebasicpanel()
    {
        spotlight.SetActive(false);
        float nor = Input.GetAxis("Mouse X");//获取鼠标的偏移量
        //float nory = Input.GetAxis("Mouse Y");
        PlayerTrans.transform.RotateAround(PlayerTrans.transform.position, -Vector3.up, Time.deltaTime * rotateSpeed * nor);//每帧旋转空物体，相机也跟随旋转
        //PlayerTrans.transform.RotateAround(PlayerTrans.transform.position, -Vector3.left, Time.deltaTime * rotateSpeed * nory);//每帧旋转空物体，相机也跟随旋转
    }

    private void raydetect()
    {
        Ray Cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log("执行到这了");
        RaycastHit getGround;
        if (Physics.Raycast(Cameraray, out getGround))
        {
            GameObject gameObject = getGround.transform.gameObject;
            if (spotcanmove)//点击出现界面后光点不可移动
            {
                spotlight.transform.position = new Vector3(gameObject.transform.position.x, spotlight.transform.position.y, gameObject.transform.position.z);
                spotlight.SetActive(true);
            }
            int[] info = gameObject.GetComponent<touchtest>().testintarray;
            
            if (Input.GetMouseButtonDown(0))
            {
                //spotcanmove = false;
                Debug.Log("触发了");
                aimGardenfield = gameObject;
                int[,] aimSkinmap = GardenMap.skinMap;
                int[,] aimMapstate = GardenMap.mapstate;
                if(aimSkinmap[info[0],info[1]] == 0)
                {
                    //执行土地蒙层撤销动画

                    //将土地状态变为1（已探索）
                    GardenMap.skinMap[info[0],info[1]] = 1;
                    Debug.Log("探索中");
                }else if(aimSkinmap[info[0], info[1]] == 1)
                {
                    //界面参数
                    spotcanmove = false;//设置光点不移动
                    SelectArea_Show();
                    SelectArea_Pos();
                    Debug.Log("以探索");
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
            //spotlight.SetActive(false);
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
        

        //SelectAreaIsShowed = true;
    }
    public void SelectArea_Hide() //选择面板隐藏
    {
        spotcanmove = true;
        SelectArea.SetActive(false);
        //SelectAreaIsShowed = false;
    }
    public void PlantClicked() //种植物操作
    {
        GameController.Move -= 1;
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


    //加载预载到场景
    public void loadprefab(int prefabType)
    {
        UnityEngine.Object prefabobject = Resources.Load(prefabName[prefabType], typeof(GameObject));
        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        GameObject prefabGameobject = Instantiate(prefabobject) as GameObject;
        GameObject parentObject = aimGardenfield;
        prefabGameobject.transform.parent = parentObject.transform;
        prefabGameobject.transform.localScale = Vector2.one;
        prefabGameobject.transform.localPosition = Vector3.zero;

    }
}
