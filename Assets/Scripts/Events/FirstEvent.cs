using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FirstEvent : MonoBehaviour
{	
    public void ShowText(string Message)
    {
        GameObject.Find("UICanvas").transform.GetChild(1).GetComponent<Text>().text = Message;
    }
}
