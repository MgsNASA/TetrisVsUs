using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, ISpawnerController, IStateClass
{
    private static Spawner _instance;  // Статическая переменная для хранения единственного экземпляра
    private ITetrominoFactory _tetrominoFactory;
    private IPositionValidator _positionValidator;
    private IRotationManager _rotationManager;
    private Vector3 lastSpawnPosition;
    public int maxSpawnShift = 4;

    private List<GameObject> spawnedTetrominos = new List<GameObject> (); // Список для отслеживания созданных объектов

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
                spawnedTetrominos.Add ( newTetromino ); // Добавляем объект в список
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

    // Метод для очистки всех созданных тетромино
    public void ClearAllTetrominos( )
    {
        foreach ( var tetromino in spawnedTetrominos )
        {
            if ( tetromino != null )
            {
                Destroy ( tetromino );
            }
        }
        spawnedTetrominos.Clear (); // Очищаем список после удаления
        Debug.Log ( "Все созданные тетромино были удалены." );
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
        ClearAllTetrominos (); // Удаляем все созданные объекты перед перезапуском
        lastSpawnPosition = Vector3.zero;  // Сброс последней позиции спауна
        Debug.Log ( "Spawner has restarted." );
    }
}
