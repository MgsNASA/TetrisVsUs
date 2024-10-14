using TMPro;
using UnityEngine;

public class VerticalMovementTracker : MonoBehaviour, IStateClass
{
    public TextMeshProUGUI textMeshProUGUI; // Ссылка на компонент TextMeshPro
    public GameObject player;

    private int previousThreshold = 0; // Переменная для отслеживания предыдущей отметки
    private bool isPaused = false;  // Флаг для отслеживания состояния паузы

    public void Initialize( GameObject player )
    {
        this.player = player;
    }

    public delegate void ThresholdReached( int threshold );
    public static event ThresholdReached OnThresholdReached;



    private void Update( )
    {
        if ( isPaused )
            return;  // Если в состоянии паузы, выходим из метода

        // Отслеживаем текущее положение игрока по оси Y
        float currentYPosition = player.transform.position.y;

        // Округляем положение до ближайшего целого числа
        int roundedYPosition = Mathf.RoundToInt ( currentYPosition );

        // Обновляем текстовое поле с текущей позицией по оси Y
        UpdateText ( roundedYPosition );

        // Проверяем, достигли ли новой отметки (например, каждые 10 единиц)
        if ( roundedYPosition >= previousThreshold + 10 )
        {
            previousThreshold += 10; // Обновляем предыдущую отметку

            // Сообщаем другим скриптам о достижении новой высоты
            OnThresholdReached?.Invoke ( previousThreshold );
        }
    }

    // Метод для обновления текста на экране
    private void UpdateText( int currentYPosition )
    {
        if ( textMeshProUGUI != null )
        {
            textMeshProUGUI.text = $"Current {currentYPosition} height";
        }
    }

    public void StartClass( )
    {
        previousThreshold = 0;  // Сброс предыдущей отметки
        isPaused = false;       // Убедитесь, что менеджер не в состоянии паузы
        Debug.Log ( "VerticalMovementTracker has started." );

    }

    public void Pause( )
    {
        isPaused = true;  // Устанавливаем флаг паузы
        Debug.Log ( "VerticalMovementTracker is paused." );
    }

    public void Resume( )
    {
        isPaused = false;  // Сбрасываем флаг паузы
        Debug.Log ( "VerticalMovementTracker has resumed." );
    }

    public void Restart( )
    {
        previousThreshold = 0;  // Сброс предыдущей отметки
        isPaused = false;       // Убедитесь, что менеджер не в состоянии паузы
        Debug.Log ( "VerticalMovementTracker has restarted." );
    
    }
}
