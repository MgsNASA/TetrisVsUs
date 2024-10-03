
using UnityEngine;

public class GameHudPanel : UIPanel
{
    public override void Show( )
    {
        gameObject.SetActive( true );
    }
    public override void Hide( )
    {
        gameObject.SetActive ( false );
    }
}
