﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GardenMap : MonoBehaviour {


    private int[] mapAttribute = new int[49]; public int[] GetMapAttribute() { return mapAttribute; }
    private int[,] mapstate = new int[7, 7]; public int[,] GetMapState() { return mapstate; }
    private static int[,] skinMap = new int[7, 7]; public int[,] GetSkinMap() { return skinMap; }

    private String[] prefabName = { "", "", "","","" };

    private  static GardenMap gardenMap;

    public static GardenMap GetGardenMap()
    {
        return gardenMap;
    }

	// Use this for initialization
	void Start () {
        //预设，记得该为调用关卡读取参数
        int[] attribute = new int[6];
        //初始化关卡内容参数
        initmapAttribute(attribute[0], attribute[1], attribute[2], attribute[3], attribute[4], attribute[5]);
        //初始化地图
        initmapstate();
        initskinMap();

		
	}
	
	// Update is called once per frame
	void Update () {

	}
    //初始化随机序列，f1-f5表示五种花的数量
    private void initmapAttribute(int f1,int f2, int f3, int f4,int f5,int emptyfield)
    {
        //按数量存入序列
        /* 1 - 5表示种子
         * 7-11表示种好的对应1-5
         */
        for(int i = 0; i < f1; i++)
        {
            mapAttribute[i] = 1;
        }
        for(int i = f1; i < f1 + f2; i++)
        {
            mapAttribute[i] = 2;
        }
        for (int i = f1+f2; i < f1+ f2+f3; i++)
        {
            mapAttribute[i] = 3;
        }
        for (int i = f1+ f2+f3; i < f1 + f2 + f3 + f4; i++)
        {
            mapAttribute[i] = 4;
        }
        for (int i = f1 + f2 + f3 + f4; i < f1 + f2 + f3 + f4 + f5; i++)
        {
            mapAttribute[i] = 5;
        }
        //存入空地数量
        for (int i = f1 + f2 + f3 + f4 + f5; i < f1 + f2 + f3 + f4 + f5 + emptyfield; i++)
        {
            mapAttribute[i] = 6;
        }
        //剩余处放入空
        for (int i = f1 + f2 + f3 + f4 + f5; i < 49; i++)
        {
            mapAttribute[i] = 0;
        }

    }
    //初始化地图
    private void initmapstate()
    {
        int[] randomarray = GetRandomArray(49, 0, 49);
        
        for(int i = 0; i < 7; i++)
        {
            for(int j = 0;j < 7; j++)
            {
                mapstate[i, j] = mapAttribute[randomarray[i*7+j]];
            }
        }

    }
    //初始化表面地图
    private void initskinMap()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                skinMap[i, j] = 0;//未点开的为0，点开的为1
            }
        }
    }

    //生成不重复随机数
    public int[] GetRandomArray(int Number, int minNum, int maxNum)
    {
        int j;
        int[] b = new int[Number];
        System.Random r = new System.Random();
        for (j = 0; j < Number; j++)
        {
            int i = r.Next(minNum, maxNum + 1);
            int num = 0;
            for (int k = 0; k < j; k++)
            {
                if (b[k] == i)
                {
                    num = num + 1;
                }
            }
            if (num == 0)
            {
                b[j] = i;
            }
            else
            {
                j = j - 1;
            }
        }
        return b;
    }
    //加载预载到场景
    public void loadprefab(int prefabType,Vector3 position) 
    {
        UnityEngine.Object prefabobject = Resources.Load(prefabName[prefabType], typeof(GameObject));
        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        GameObject prefabGameobject = Instantiate(prefabobject) as GameObject;
        GameObject parentObject = CameraController.aimGardenfield;
        prefabGameobject.transform.parent = parentObject.transform;
        prefabGameobject.transform.localScale = Vector2.one;
        prefabGameobject.transform.localPosition = Vector3.zero;

    }



}