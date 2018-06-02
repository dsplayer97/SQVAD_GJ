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

        
      

    }
    




}
