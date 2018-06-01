using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float rotateSpeed = 100;       //设置旋转的速度
    public GameObject PlayerTrans;       //设置空物体的位置
    public float maxh = 10;               //设置提升的最高高度
    public GameObject spotlight;   
    public static GameObject aimGardenfield;
    private bool aimtofield;

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
            spotlight.transform.position = new Vector3(gameObject.transform.position.x, spotlight.transform.position.y, gameObject.transform.position.z);
            spotlight.SetActive(true);
            int[] info = gameObject.GetComponent<touchtest>().testintarray;
            aimGardenfield = gameObject;
            if (Input.GetMouseButtonDown(0))
            {
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
                    Debug.Log("以探索");
                }
            }
            //Debug.Log(info[0] + "  " + info[1]);
            

        }
        else
        {
            spotlight.SetActive(false);
            aimGardenfield = null;
        }
    }

   
    
    

}
