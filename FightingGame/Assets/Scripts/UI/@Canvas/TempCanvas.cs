using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FGDefine;

public class TempCanvas : BaseCanvas
{
    [SerializeField] SelectionUI selectionUI;

    public override void Open<T>(UIParam param = null)
    {
        if (typeof(T) == typeof(SelectionUI)) selectionUI.Open();
        else Debug.Log("범위 벗어남");
    }

    public override void Close<T>()
    {
        if (typeof(T) == typeof(SelectionUI)) selectionUI.Close();
        else Debug.Log("범위 벗어남");
    }

    public void OnClickSeleteChar(ENUM_CHARACTER_TYPE type)
    {

    }

    public void OnClickStart()
    {
        Managers.Scene.LoadScene(ENUM_SCENE_TYPE.Battle);
    }
}
