using UnityEngine;

public class GameStoreScene : GeneralSceneHandler
{
    [SerializeField] BottomMenuPanel _bottomMenuPanel;
    [SerializeField] TopMenuPanel _topMenuPanel;



    public override void Start()
    {
        base.Start();
        _bottomMenuPanel.Initialize();
        _topMenuPanel.Initialize();
    }
}