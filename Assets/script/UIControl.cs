using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    public Slider Moon;
    public Slider Sun;
    public Slider Air;
    public GameObject SelectArea;
    public GameObject MoveText;
    public static bool In_Round = true; //玩家是否在回合内,行动点耗尽为不在回合内
    //bool SelectAreaIsShowed = false; //选择区域是否显示
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.Move == 0)
        {
            In_Round = false;
        }
        if (In_Round == true) //玩家在回合内
        {
            //UI_Raydetect();

            //if (SelectAreaIsShowed == false) //面板没有显示
            //{
            //    //if (Input.GetMouseButtonDown(1))
            //    //{
            //    //    SelectArea_Show();
            //    //    SelectArea_Pos();
            //    //}

            //    UI_Raydetect(); //鼠标射线检测

            //}       
        }

        MoveText.GetComponent<Text>().text = GameController.Move.ToString();
        
    }

    public void Sun_Moon() //日月button执行函数
    {
        ChangeSunMoon(10, 20);
    }

    public void ChangeSunMoon(float sun, float moon)  //日月能量槽改变函数
    {
        Moon.value = moon;
        Sun.value = sun;
    }

    public void O2_CO2() //空气按钮执行函数
    {
        ChangeAir(20);
    }

    public void ChangeAir(float O2) //空气值改变执行函数
    {
        Air.value = O2;
    }
    /*
    public void SelectArea_Show() //选择面板显示
    {
        SelectArea.SetActive(true);
        //SelectAreaIsShowed = true;
    }

    public void SelectArea_Hide() //选择面板隐藏
    {
        SelectArea.SetActive(false);
        //SelectAreaIsShowed = false;
    }

    public void SelectArea_Pos() //设置选择面板为鼠标位置
    {
        SelectArea.transform.position = Input.mousePosition;
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
    
    private void UI_Raydetect() //鼠标射线、右键指定tag的物体
    {

        Ray UI_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit myUI_Ray;
        if(Physics.Raycast(UI_Ray,out myUI_Ray))
        {
            if (Input.GetMouseButtonDown(1) && myUI_Ray.collider.gameObject.tag=="Plant")
            {
                SelectArea_Show();
                SelectArea_Pos();
            }
        }
    }*/
}
