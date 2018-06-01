using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Asynchronousload : MonoBehaviour {

    public Slider progressSlider;//进度条  
    public Text ProgressSliderText;//进度条进度显示文字  
    private int nowProcess;//当前加载进度  
    private AsyncOperation async;
   
    

    void Start()
    {
        StartCoroutine(LoadScene());
    }


    void Update()
    {
        if (async == null)
        {
            return;
        }

        int toProcess;
        // async.progress 你正在读取的场景的进度值  0---0.9      
        // 如果当前的进度小于0.9，说明它还没有加载完成，就说明进度条还需要移动      
        // 如果，场景的数据加载完毕，async.progress 的值就会等于0.9    
        if (async.progress < 0.9f)
        {
            toProcess = (int)async.progress * 100;
        }
        else
        {
            toProcess = 100;
        }
        // 如果滑动条的当前进度，小于，当前加载场景的方法返回的进度     
        if (nowProcess < toProcess)
        {
            nowProcess++;
        }

        progressSlider.value = nowProcess / 100f;
        //设置progressText进度显示  
        ProgressSliderText.text = progressSlider.value * 100 + "%";
        // 设置为true的时候，如果场景数据加载完毕，就可以自动跳转场景     
        if (nowProcess == 100)
        {
            async.allowSceneActivation = true;
        }
    }
    //异步加载scene  
    IEnumerator LoadScene()
    {
        //int ID = loadc.getloadID();
        //Debug.Log(loadc.getloadID());
        async = SceneManager.LoadSceneAsync(loadcontrol.loadID);
        //Debug.Log(loadc.getloadID());
        async.allowSceneActivation = false;
        yield return async;
    }
    //外部调用的加载的方法  
    
      
    
}
