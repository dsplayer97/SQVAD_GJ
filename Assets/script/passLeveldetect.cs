using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passLeveldetect : MonoBehaviour {

    private float sunsum;
    private float moonsum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        sunsum = GameObject.Find("Main Camera").GetComponent<GameController>().sunPower;
        moonsum = GameObject.Find("Main Camera").GetComponent<GameController>().moonPower;

        if(sunsum == 100 && moonsum == 100)
        {
            //通关
        }

    }

    //特殊条件达成检测
    public int Trophydetect(int levelnumber)
    {
        int star = 1;
        switch (levelnumber)
        {
            //对应三关的奖杯条件
            case 1:

                break;
            case 2:
                break;
            case 3:
                break;
        }
        return star;
    }
}
