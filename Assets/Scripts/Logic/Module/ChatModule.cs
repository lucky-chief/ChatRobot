using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json;
using System.Text;

public class MsgData
{
    public string key = "ddbe2636974b4285a0c165561f8413c2";
    public string info = "";
}

public class RevcMsgData
{
    public int code;
    public string text;
}

[Module("ChatModule", true)]
public class ChatModule : ModuleBase
{
    public const string MSG_RESPOND = "MSG_RESPOND";
    public override void OnLoad()
    {
    }

    public override void OnRelease()
    {
    }

    private MsgData msg;

    public void SendMsg(string msg)
    {
        MsgData data = new MsgData();
        data.info = msg;
        this.msg = data;
        Game.Instance().StartCoroutine(Send());
    }

    IEnumerator Send()
    {
        WWWForm form = new WWWForm();
        form.AddField("key", msg.key);
        form.AddField("info", msg.info);
        ///WWW www = new WWW("http://www.tuling123.com/openapi/api?key=77689f5beb91461a90a4e3f005e12c68&info=%22你好%22");
        WWW www = new WWW("http://www.tuling123.com/openapi/api",form);
        yield return www;
        RevcMsgData revcMsg = JsonConvert.DeserializeObject<RevcMsgData>(www.text);
        Debug.Log( "==========:  "+www.text);
        string text = "我好想知道，但我不告诉你！";
        if(revcMsg.code == 100000)
        {
            text = revcMsg.text;
        }
        Notification notify = new Notification(MSG_RESPOND);
        notify["text"] = text;
        notify.Send();
    }
}
