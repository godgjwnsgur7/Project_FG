using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWindow : UIElement
{
    [SerializeField] CustomPanel customPanel;
    [SerializeField] CreateRoomPanel createRoomPanel;

    private void OnEnable() // CustomWindow 활성화 시에 CustomPanel 활성화
    {
        if (createRoomPanel.gameObject.activeSelf)
            CloseCreateRoomPanel();
        else 
            customPanel.Open();
    }

    public override void Open(UIParam param = null)
    {
        base.Open(param);
    }

    public override void Close()
    {
        base.Close();
    }

    public void OpenCreateRoomPanel() // 방 생성 패널 활성화
    {
        customPanel.Close();
        createRoomPanel.Open();
    }

    public void CloseCreateRoomPanel() // 방 목록 패널 활성화
    {
        customPanel.Open();
        createRoomPanel.Close();
    }

    public void EnterCustomScene() 
    {
        Managers.Scene.FadeLoadScene(ENUM_SCENE_TYPE.CustomRoom);
    }

    public void CreateCustomScene()
    {
        Managers.Scene.FadeLoadScene(ENUM_SCENE_TYPE.CustomRoom);
    }
}
