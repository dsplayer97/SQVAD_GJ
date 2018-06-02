using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CardSystem : MonoBehaviour {

   // public Transform card01;//意图表示第一张牌的位置  

    //public Transform card02;

   // public GameObject cardsprefab;

    private String[] cardprefabroot = { "Prefabs/cards", "", "", "", "" };//预设路径

    private float thedistance = 2f;//两张牌的距离  

    public GameObject cardGroup;

    public GameObject panelParents;



    private List<GameObject> cards = new List<GameObject>();

    void Start()
    {
        //thedistance = card02.position.x - card01.position.x;//两张牌的距离  
        Getcard(0);
        Getcard(0);
        Getcard(0);
        Getcard(0);

    }

    void Update()
    {
       
    }

    //获得卡牌即摸牌  
    public void Getcard(int cardtype)
    {
        // GameObject go = NGUITools.AddChild(this.gameObject, cardsprefab);//this .gameObject指的就是当前要把脚本的绑定到物体，把这个物体赋值给物体go  
        GameObject prefabGameobject = Instantiate(Resources.Load(cardprefabroot[cardtype])) as GameObject;

        prefabGameobject.transform.parent = cardGroup.transform;

        prefabGameobject.transform.localScale = new Vector3(1, 1, 1);

        Vector3 toposition = Vector3.zero + new Vector3(thedistance, 0, 0) * cards.Count;//获得卡牌到达的位置，（现有牌数量的最后面，即与第一张牌的距离位置）  

        iTween.MoveTo(prefabGameobject, toposition, 1f);//移动物体go到指定位置即toposition  

        cards.Add(prefabGameobject);
    }

    //移除卡牌即出牌  
    public void losecard(int[] Discard)
    {
       for(int i = 0; i < Discard.Length; i++)
        {
            Destroy(cards[Discard[i]]);
            cards.RemoveAt(Discard[i]);
        }

        for (int i = 0; i < cards.Count; i++)//移除后刷新手中所有牌的位置  
        {
            Vector3 toposition = Vector3.zero + new Vector3(thedistance, 0, 0) * i;//第i张牌的位置（即与第一张牌的距离）  

            iTween.MoveTo(cards[i], toposition, 0.5f);//刷新与第一张牌的距离（即刷新手中所有牌的位置）  
        }
    }
}
