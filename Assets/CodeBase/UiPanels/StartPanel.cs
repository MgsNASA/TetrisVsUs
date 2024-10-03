using UnityEngine;

public class StartPanel : UIPanel
{
    public override void Show( )
    {
        gameObject.SetActive (true);
    }
    public override void Hide( )
    {
       gameObject.SetActive(false);
    }
}