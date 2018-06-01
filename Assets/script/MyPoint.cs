using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPoint : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
