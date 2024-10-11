using UnityEngine;
using UnityEngine.UI;

public class PausePanel : UIPanel
{
    public Button resumeButton;
    public Button restartButton;
    public override void Show( )
    {
        gameObject.SetActive ( true );
        resumeButton.onClick.AddListener ( OnResumeButtonClicked );
    }

    public override void Hide( )
    {
        gameObject.SetActive ( false );
        resumeButton.onClick.RemoveListener ( OnResumeButtonClicked );
    }

    private void OnResumeButtonClicked( )
    {
        // Возобновляем игру через GameProcessController
        FindObjectOfType<GameProcessController> ().Resume ();
    }
    private void OnRestartButtonClicked( )
    {
        // Возобновляем игру через GameProcessController
        FindObjectOfType<GameProcessController> ().Resume ();
    }
}
