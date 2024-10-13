using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : UIPanel
{
    public Button restartButton; // Дополнительная кнопка рестарта

    public override void Show( )
    {
        gameObject.SetActive ( true );
        restartButton.onClick.AddListener ( OnRestartButtonClicked );
    }

    public override void Hide( )
    {
        gameObject.SetActive ( false );
        restartButton.onClick.RemoveListener ( OnRestartButtonClicked );
    }

    private void OnRestartButtonClicked( )
    {
        // Рестарт игры через GameProcessController
        FindObjectOfType<GameProcessController> ().Restart ();
    }
}
