using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatUI : UIBase {
    public Button btnSend;
    public ChatContainer container;
    public InputField ifChat;

    private Transform chatRightModel;
    private Transform chatLeftModel;
    private ChatModule module;

    protected override void OnLoad()
    {
        module = ModuleManager.Instance.GetModule<ChatModule>();
        UIManager.SetButtonClick(btnSend.gameObject, OnSend);
        chatLeftModel = Resources.Load<Transform>("Prefabs/UI/RotbotChatItem");
        chatRightModel = Resources.Load<Transform>("Prefabs/UI/selfChatItem");

        Messager.Instance.AddNotification(ChatModule.MSG_RESPOND, OnRespond);
        base.OnLoad();
    }

    protected override void OnUpdate()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            OnSend(null, 0, 0);
            ifChat.text.Replace((char)KeyCode.Return, ' ');
        }
        base.OnUpdate();
    }

    protected override void OnRelease()
    {
        base.OnRelease();
    }

    void OnSend(GameObject obj,int parm1,int parm2)
    {
        if("" != ifChat.text)
        {
            Transform item = Instantiate<Transform>(chatRightModel);
            ChatItem cItem = item.GetComponent<ChatItem>();
            cItem.SetMsg(ifChat.text);

            container.AddItem(item,0, false);
            module.SendMsg(ifChat.text);
            ifChat.text = "";
        }
    }

   void OnRespond(Notification notify)
    {
        Transform item = Instantiate<Transform>(chatLeftModel);
        ChatItem cItem = item.GetComponent<ChatItem>();
        cItem.SetMsg(notify["text"].ToString());
        container.AddItem(item,cItem.msgText.preferredHeight+100, true);
    }
}
