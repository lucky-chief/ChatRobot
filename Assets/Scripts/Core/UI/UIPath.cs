public sealed class UIPath
{
    public readonly static UIPath Instance = new UIPath();
    private UIPath() { }

    public string GetUIPath(UINames UIName)
    {
        string path = "";
        switch(UIName)
        {
            case UINames.ChatUI:
                path = "Prefabs/UI/ChatUI";
                break;
        }
        return path;
    }
}
