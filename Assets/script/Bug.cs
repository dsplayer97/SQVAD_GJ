using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bug {


    private MyPoint point; public void SetPoint(MyPoint _p) { point = _p; }
    public MyPoint GetPoint() { return point; }
    private GameObject gameController;
    private GameObject targetMap;
    private int[,] skinMap;
    private int[,] mapState;
    public int spreadCD;//扩散感染的cd时间
    public int spreadPercent;


    public Bug() {

        gameController = GameObject.Find("Main Camera");
        //Debug.Log("昆虫出生!");
        spreadCD = 1;
        spreadPercent = 3;
        //targetMap = GameObject.Find("GardenMap");

        GameObject[] i = gameController.GetComponent<GameController>().plantList;
    }



    //感染给定坐标的植物, 返回成功与否
    public bool Infect(MyPoint _point) {

        foreach (GameObject i in gameController.GetComponent<GameController>().plantList)
        {

            Plant p = i.GetComponent<Plant>();
            if (p.GetPoint().equal(_point) && p.live && !p.infected)
            {
                p.Infect();
                this.point = _point;
                //Debug.Log(this.point.ToString()+"被感染了");
                return true;
            }
        }
        return false;
    }

    //扩散感染自己临近的植物
    public void SpreadInfect()
    {
        List<MyPoint> points = FindNearPlant();
        //Debug.Log(points.Count);
        foreach (GameObject i in gameController.GetComponent<GameController>().plantList)
        {
            Plant p = i.GetComponent<Plant>();
            foreach (MyPoint point in points)
            {
                if (p.GetPoint().equal(point))
                {
                    int r = Random.Range(1, 10);
                    //有一定概率感染周围植物
                    if (r > spreadPercent)
                    {
                        Bug newBug = new Bug();
                        if (newBug.Infect(point))
                        {

                            GameObject.Find("Main Camera").GetComponent<BugController>().SendMessageUpwards("AddBug", newBug);

                        }
                    }
                }
            }
        }
    }

    //返回临近的活着的植物坐标
    List<MyPoint> FindNearPlant()
    {

        skinMap = GardenMap.skinMap;
        mapState = GardenMap.mapstate;

        List<MyPoint> points = new List<MyPoint>();
        MyPoint nextPoint = new MyPoint(-1, -1);

        nextPoint.SetX(this.point.GetX() + 1);
        nextPoint.SetY(this.point.GetY());

        if (plantAlive(nextPoint.GetX(), nextPoint.GetY())) {

            //points.Add(nextPoint);
            points.Insert(0, nextPoint);
            //Debug.Log("加入:" + nextPoint.ToString());
        }
        
        nextPoint = new MyPoint(-1, -1);
        nextPoint.SetX(point.GetX() - 1);
        nextPoint.SetY(point.GetY());
        if (plantAlive(nextPoint.GetX(), nextPoint.GetY()))
        {
            //points.Add(nextPoint);
            points.Insert(0, nextPoint);
            //Debug.Log("加入:" + nextPoint.ToString());
        }

        nextPoint = new MyPoint(-1, -1);
        nextPoint.SetX(point.GetX());
        nextPoint.SetY(point.GetY() - 1);
        if (plantAlive(nextPoint.GetX(), nextPoint.GetY()))
        {
            //points.Add(nextPoint);
            points.Insert(0, nextPoint);
            //Debug.Log("加入:" + nextPoint.ToString());
        }

        nextPoint = new MyPoint(-1, -1);
        nextPoint.SetX(point.GetX());
        nextPoint.SetY(point.GetY() + 1);
        if (plantAlive(nextPoint.GetX(), nextPoint.GetY()))
        {
            //points.Add(nextPoint);
            points.Insert(0, nextPoint);
            //Debug.Log("加入:" + nextPoint.ToString());
        }

        return points;
    }

    //判断给定棋盘坐标是否有植物活着

    bool plantAlive(int x, int y) {      
        if (x >= skinMap.GetLength(0) || y >= skinMap.GetLength(1) || x < 0 || y < 0) {

            return false;
        }
        if (skinMap[x, y] == 1 && mapState[x, y] >= 7 && mapState[x, y] <= 11)
        {
            return true;
        }
        return false;
    }
}
