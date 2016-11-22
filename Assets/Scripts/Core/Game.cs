﻿using UnityEngine;
#if UNITY_5_3
using UnityEngine.SceneManagement;
#endif
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using FSM;

public class Game : MonoBehaviour {
    private SceneBase curScene ;
    public SceneBase CurScene{get { return curScene; }}
    public Camera camera2D { get; private set; }
    public Action loadComplete;


    private Transform container3D;
    public Transform Container3D
    {
        get { return container3D; }
    }

    void Start () {
        Singleton.GetInstance("TimeUtil");
        ModuleManager.Instance.Init();
        InitUIRoot();
	}

    void OnDestroy()
    {
    }
	
	void Update ()
    {
        if (null != curScene) curScene.OnUpdate(Time.deltaTime);
	}

    public static Game Instance()
    {
        return Singleton.GetInstance("Game") as Game;
    }

    public void BeginCoroutine(Func<IEnumerator> func)
    {
        StartCoroutine(func());
    }

    public void SetContainer3D(Transform container)
    {
        container3D = container;
    }

    private void InitUIRoot()
    {
        GameObject ui_Root = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/Canvas"));
        uiRoot = ui_Root;
        UIManager.Instance.InitUIManager(uiRoot.transform.FindChild("UI"));
        DontDestroyOnLoad(uiRoot);
        UIManager.Instance.OpenUI(UINames.ChatUI);
    }

    public void UnRegisterUpdateObj<T>() where T : IUpdate
    {

    }

    public void CreateScene(string sceneName,Type sceneType,Action loadComplete = null)
    {
        toLoadSceneData = new SceneData(sceneName, sceneType) ;
        this.loadComplete = loadComplete == null ? null : loadComplete ;
        BeginCoroutine(LoadScene);
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation opt = null;
#if UNITY_5_3
        opt = SceneManager.LoadSceneAsync(toLoadSceneData.sceneName);
#else
        opt = Application.LoadLevelAsync(toLoadSceneData.sceneName);
#endif
        yield return opt;
        if(null != curScene) curScene.OnRelease();
        curScene = Activator.CreateInstance(toLoadSceneData.sceneType) as SceneBase;
        curScene.OnLoad();
        if(null != loadComplete)
        {
            loadComplete.Invoke();
        }
    }

    private SceneData toLoadSceneData;
    private GameObject uiRoot;
}
