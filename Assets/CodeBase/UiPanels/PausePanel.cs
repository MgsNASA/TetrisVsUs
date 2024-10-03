using UnityEngine;

public class PausePanel : UIPanel
{
    public override void Show( )
    {
        gameObject.SetActive ( true );
    }
    public override void Hide( )
    {
        gameObject.SetActive ( false );
    }
}

