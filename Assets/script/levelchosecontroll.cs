using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class levelchosecontroll : MonoBehaviour {

    // Use this for initialization
    public GameObject levelcube1, levelcube2, levelcube3;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray Cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log("执行到这了");
        RaycastHit getGround;
        if(Physics.Raycast(Cameraray, out getGround))
        {
            GameObject detectgameobject = getGround.transform.gameObject;
            //Debug.Log(detectgameobject.name);
            if(detectgameobject.name.Equals(levelcube1.name))
            {
                detectgameobject.transform.DOScale(8f, 3);
                levelcube2.transform.DOScale(5f, 3);
                levelcube3.transform.DOScale(5f, 3);
            }else if(detectgameobject.name.Equals(levelcube2.name))
            {
                detectgameobject.transform.DOScale(8f, 3);
                levelcube1.transform.DOScale(5f, 3);
                levelcube3.transform.DOScale(5f, 3);
            }else if(detectgameobject.name.Equals(levelcube3.name))
            {
                detectgameobject.transform.DOScale(8f, 3);
                levelcube1.transform.DOScale(5f, 3);
                levelcube2.transform.DOScale(5f, 3);
            }

        }
    }
}
