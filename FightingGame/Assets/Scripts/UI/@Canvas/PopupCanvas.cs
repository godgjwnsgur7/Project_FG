using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCanvas : MonoBehaviour
{


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        DontDestroyOnLoad(this);
    }

    public void Open<T>()
    {
    }

    public void Close<T>()
    {

    }
}