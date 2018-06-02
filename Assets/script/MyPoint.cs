using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPoint{

    public int x;
    public int y;

    public MyPoint(int _x, int _y) {
        this.x = _x;
        this.y = _y;
    }

    public int GetX() {
        return x;
    }

    public int GetY() {
        return y;
    }

    public void SetX(int _x) {
        this.x = _x;
    }

    public void SetY(int _y) {
        this.y = _y;
    }

    public string ToString() {
        return "(" + x.ToString() + "," + y.ToString() + ")";
    }

    public bool equal(MyPoint _point) {
        if (this.x == _point.GetX() && this.y == _point.GetY()){
            return true;
        }
        else {
            return false;
        }
    }
    
}
