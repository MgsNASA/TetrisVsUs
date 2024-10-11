using UnityEngine;

public class Spawner : MonoBehaviour, ISpawnerController, IStateClass
{
    private static Spawner _instance;  // Статическая переменная для хранения единственного экземпляра
    private ITetrominoFactory _tetrominoFactory;
    private IPositionValidator _positionValidator;
    private IRotationManager _rotationManager;
    private Vector3 lastSpawnPosition;
    public int maxSpawnShift = 4;

    // Свойство для доступа к экземпляру синглтона
    public static Spawner Instance
    {
        get
        {
            if ( _instance == null )
            {
                Debug.LogError ( "Spawner instance не инициализирован! Убедитесь, что объект Spawner существует в сцене." );
            }
            return _instance;
        }
    }

    private void Awake( )
    {
        // Проверяем, есть ли уже экземпляр
        if ( _instance != null && _instance != this )
        {
            Destroy ( gameObject );  // Удаляем дубликат
            return;
        }

        _instance = this;  // Устанавливаем экземпляр
        DontDestroyOnLoad ( gameObject );  // Сохраняем объект между сценами
    }

    // Метод для инициализации через зависимости
    public void Initialize( ITetrominoFactory tetrominoFactory , IPositionValidator positionValidator , IRotationManager rotationManager )
    {
        _tetrominoFactory = tetrominoFactory;
        _positionValidator = positionValidator;
        _rotationManager = rotationManager;
    }

    public void NewTetromino( )
    {
        Vector3 spawnPosition;
        Quaternion randomRotation;

        for ( int i = 0; i < maxSpawnShift * 2; i++ )
        {
            spawnPosition = GenerateNewPosition ();
            randomRotation = _rotationManager.GenerateRandomRotation ();

            GameObject newTetromino = _tetrominoFactory.CreateRandomTetromino ( spawnPosition , randomRotation );

            if ( newTetromino == null )
            {
                Debug.LogError ( "Не удалось создать новый тетромино!" );
                return;
            }

            // Добавляем направление движения (вниз)
            if ( _positionValidator.ValidMove ( newTetromino.transform , Vector3.down ) )
            {
                lastSpawnPosition = spawnPosition;
                return;
            }
            else
            {
                Destroy ( newTetromino );
            }
        }

        Debug.LogWarning ( "Не удалось найти допустимую позицию для спавна тетромино!" );
    }

    private Vector3 GenerateNewPosition( )
    {
        int randomShift;
        Vector3 spawnPosition;

        do
        {
            randomShift = Random.Range ( -maxSpawnShift , maxSpawnShift + 1 );
            spawnPosition = transform.position;
            spawnPosition.x += randomShift;
        }
        while ( spawnPosition == lastSpawnPosition );

        return spawnPosition;
    }

    public void MoveSpawnerUp( float step )
    {
        Vector3 spawnerPosition = transform.position;
        spawnerPosition.y += step; // Увеличиваем Y координату спаунера
        transform.position = spawnerPosition;
    }

    public void StartClass( )
    {
        lastSpawnPosition = Vector3.zero;  // Сброс последней позиции спауна
        Debug.Log ( "Spawner has started." );
    }

    public void Pause( )
    {
        // Логика для паузы, например, отключение спавна
        Debug.Log ( "Spawner is paused." );
    }

    public void Resume( )
    {
        // Логика для возобновления, например, включение спавна
        Debug.Log ( "Spawner has resumed." );
    }

    public void Restart( )
    {
        lastSpawnPosition = Vector3.zero;  // Сброс последней позиции спауна
        Debug.Log ( "Spawner has restarted." );
    }
}
