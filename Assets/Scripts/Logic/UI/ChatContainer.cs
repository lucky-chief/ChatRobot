using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatContainer : MonoBehaviour {
    public float itemHeight = 200;
    public int leftItemCoordX;
    public int rightItemCoordX;
    public int firstAddCoordY;
    public float contentHeiht = 400;
    public Scrollbar scrollBar;

    private float AddCoordY;
    private float nowY;
	// Use this for initialization
	void Start () {
        AddCoordY = firstAddCoordY;
        scrollBar.onValueChanged.AddListener(OnScroll);
        
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void AddItem(Transform item,float height, bool isLeft)
    {
        item.SetParent(transform);
        item.localScale = Vector3.one;
        item.localPosition = new Vector3(isLeft ? leftItemCoordX : rightItemCoordX, AddCoordY, 0);
        AddCoordY -= height > itemHeight ? height : itemHeight;
        if(-AddCoordY > contentHeiht * 0.5f)
        {
            transform.localPosition +=  new Vector3(0, height > itemHeight ? height : itemHeight, 0);
            scrollBar.gameObject.SetActive(true);
        }
        nowY = transform.localPosition.y;
    }

    public void Repos()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(child.localPosition.x < 0 ? leftItemCoordX : rightItemCoordX, firstAddCoordY - itemHeight * i, 0);
        }
    }

    public float GetTotalHeight()
    {
        return AddCoordY + 20;
    }

    void OnValidate()
    {
        Repos();
    }

    void OnScroll(float value)
    {
        transform.localPosition = new Vector3(0, nowY * (1 - value), 0);
    }
}
