using UnityEngine;
using UnityEngine.UI;

public class StartPanel : UIPanel
{
   
    public override void Show( )
    {
        gameObject.SetActive ( true );
        button.onClick.AddListener ( OnStartButtonClicked );
    }

    public override void Hide( )
    {
        gameObject.SetActive ( false );
      //  button.onClick.RemoveListener ( OnStartButtonClicked );
    }

    private void OnStartButtonClicked( )
    {
        gameProcessController.StartClass ();
    }
}
