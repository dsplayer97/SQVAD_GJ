using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class selfRotate : MonoBehaviour {

    // Use this for initialization
    public static bool doscale = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.gameObject.transform.Rotate(0, 25 * Time.deltaTime, 0, Space.Self);

        
        if (doscale)
        {
            Debug.Log("true了");
            this.gameObject.transform.DOScale(8f, 2);
        }
        else
        {
            this.gameObject.transform.DOScale(5f, 2);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("放大");
        //this.gameObject.transform.DOScale(8f, 2);
        doscale = true;
    }
    public  void OnCollisionExit(Collision collision)
    {
        //Debug.Log("缩小");
        //this.gameObject.transform.DOScale(5f, 2);
        doscale = false;
    }




}
