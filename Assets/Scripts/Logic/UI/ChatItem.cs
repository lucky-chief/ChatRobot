using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour {
    public Text IconName;
    public Text msgText;
    public RectTransform msgBg;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetMsg(string msg)
    {
        msgText.text = msg;
        msgBg.sizeDelta = new Vector2(msgText.preferredWidth + 30,msgText.preferredHeight + 20);
    }
}
