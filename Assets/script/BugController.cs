using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour {

    //public GameObject targetMap;
    public GameObject gameController;

    private GardenMap gardenMap;
    private List<Bug> bugList;
    

    private int round;

    void Awake() {
        gameController = GameObject.Find("Main Camera");
        bugList = new List<Bug>();
    }


	// Use this for initialization
	void Start () {
        
        
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
    public void Infect() {
        round = GetRound();
        if (round >= 5) {
            List<MyPoint> points = FindPlant();
            int randomNum = Random.Range(0, points.Count);
            //Debug.Log(points.Count);
            if (points.Count != 0) {
                Bug bug = new Bug();
                //Debug.Log(points[randomNum].ToString());
                if (bug.Infect(points[randomNum])) {
                    bugList.Add(bug);
                }
            }
        }
    }

    //扩散感染
    public void SpreadInfect() {
        foreach (Bug bug in bugList) {
            if (GetRound() % bug.spreadCD == 0) {
                bug.SpreadInfect();
            }
        }
    }

    void AddBug(Bug bug) {
        bugList.Add(bug);
    }

    //杀死指定坐标的害虫


    public void KillBug(MyPoint _point) {
        foreach (GameObject i in gameController.GetComponent<GameController>().plantList) {
            Plant p = i.GetComponent<Plant>();
            if (p.GetPoint().equal(_point)) {
                p.Cure();
            }
        }
        foreach (Bug b in bugList) {
            if (b.GetPoint().equal(_point)) {
                bugList.Remove(b);
            }
        }
    }

    public void removeBug(MyPoint _point) {
        foreach (Bug b in bugList)
        {
            if (b.GetPoint().equal(_point))
            {
                bugList.Remove(b);
            }
        }
    }
}
