using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour {

    private MyPoint point; public void SetPoint(MyPoint _p) { point = _p; } public MyPoint GetPoint() { return point; }
    private GameObject gameController;
    private GameObject targetMap;
    private int[,] skinMap;
    private int[,] mapState;
    public int spreadCD;//扩散感染的cd时间

    // Use this for initialization
    void Start() {
        gameController = GameObject.Find("GameController");
        targetMap = GameObject.Find("GardenMap");
    }

    // Update is called once per frame
    void Update() {

    }

    //感染给定坐标的植物, 返回成功与否
    public bool Infect(MyPoint _point) {
        foreach (GameObject i in gameController.GetComponent<GameController>().GetAllPlant())
        {
            Plant p = i.GetComponent<Plant>();
            if (p.GetPoint() == _point && p.live && !p.infected)
            {
                //
                p.Infect();
                this.point = _point;
                return true;
            }
        }
        return false;
    }

    //扩散感染自己临近的植物
    public void SpreadInfect() {
        List<MyPoint> points = FindNearPlant();
        foreach (GameObject i in gameController.GetComponent<GameController>().GetAllPlant())
        {
            Plant p = i.GetComponent<Plant>();
            foreach(MyPoint point in points)
            if (p.GetPoint() == point)
            {
                Bug newBug = new Bug();
                if (newBug.Infect(point)) {
                    GameObject.Find("BugController").SendMessageUpwards("AddBug", newBug);
                }                
            }
        }
    }

    //返回临近的活着的植物坐标
    List<MyPoint> FindNearPlant() {

        int[,] skinMap = GardenMap.skinMap;
        int[,] mapState = GardenMap.mapstate;

        List<MyPoint> points = new List<MyPoint>();
        MyPoint nextPoint = new MyPoint(-1,-1);

        nextPoint.SetX(point.GetX() + 1); nextPoint.SetY(point.GetY() + 1);
        if (plantAlive(nextPoint.GetX(), nextPoint.GetY())) {
            points.Add(nextPoint);
        }

        nextPoint.SetX(point.GetX() - 1); nextPoint.SetY(point.GetY() + 1);
        if (plantAlive(nextPoint.GetX(), nextPoint.GetY()))
        {
            points.Add(nextPoint);
        }

        nextPoint.SetX(point.GetX() + 1); nextPoint.SetY(point.GetY() - 1);
        if (plantAlive(nextPoint.GetX(), nextPoint.GetY()))
        {
            points.Add(nextPoint);
        }

        nextPoint.SetX(point.GetX() - 1); nextPoint.SetY(point.GetY() - 1);
        if (plantAlive(nextPoint.GetX(), nextPoint.GetY()))
        {
            points.Add(nextPoint);
        }

        return points;
    }

    //判断给定棋盘坐标是否有植物活着
    bool plantAlive(int x, int y) {
        if (skinMap[x, y] == 1 && mapState[x, y] >= 7 && mapState[x, y] <= 11)
        {
            return true;
        }
        return false;
    }
}
