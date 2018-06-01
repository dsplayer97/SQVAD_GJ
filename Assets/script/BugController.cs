using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour {

    public GameObject targetMap;
    public GameObject gameController;

    private GardenMap gardenMap;
    private List<Bug> bugList;
    

    private int round;



	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController");
        bugList = new List<Bug>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    //获取当前地图上哪个坐标长着植物
    List<MyPoint> FindPlant() {
        return gameController.GetComponent<GameController>().FindPlant();
    }

    //获取当前回合数
    int GetRound() {
        return gameController.GetComponent<GameController>().round;
    }

    //感染植物
    void Infect() {
        round = GetRound();
        if (round >= 10) {
            List<MyPoint> points = FindPlant();
            int randomNum = Random.Range(0, points.Count);
            if (points.Count!=0) {
                Bug bug = new Bug();
                if (bug.Infect(points[randomNum])) {
                    bugList.Add(bug);
                }
            }
        }
    }

    //扩散感染
    void SpreadInfect() {

        //GameObject[] bugs = GameObject.FindGameObjectsWithTag("Bug");

        foreach (Bug bug in bugList) {
            if (GetRound() % bug.spreadCD == 0) {
                bug.SpreadInfect();
            }
        }
    }

    void AddBug(Bug bug) {
        bugList.Add(bug);
    }
}
