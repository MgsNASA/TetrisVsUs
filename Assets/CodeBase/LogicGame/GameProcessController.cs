using System.Collections.Generic;
using UnityEngine;

public class GameProcessController : MonoBehaviour, IStateClass
{
    public GameObject uiManagerPrefab;
    public GameObject spawnerPrefab;
    public GameObject tetrisGridManagerPrefab;
    public GameObject cameraControllerPrefab;

    private UiManager _uiManager;
    private Spawner _spawner;
    private TetrisGridManager _tetrisGridManager;
    private CameraController _cameraController;

    // Список классов, реализующих интерфейс IStateClass
    private List<IStateClass> stateClasses = new List<IStateClass> ();

    public void StartGame( )
    {
        // Создаем экземпляры из префабов
        _uiManager = Instantiate ( uiManagerPrefab , transform ).GetComponent<UiManager> ();
        _tetrisGridManager = Instantiate ( tetrisGridManagerPrefab ).GetComponent<TetrisGridManager> ();
        _spawner = Instantiate ( spawnerPrefab ).GetComponent<Spawner> ();
        _cameraController = Instantiate ( cameraControllerPrefab ).GetComponent<CameraController> ();

        // Инициализация спаунера через сервисы
        var tetrominoFactory = AllServices.Container.Single<ITetrominoFactory> ();
        var positionValidator = AllServices.Container.Single<IPositionValidator> ();
        var rotationManager = AllServices.Container.Single<IRotationManager> ();

        // Проверка зависимостей
        if ( tetrominoFactory == null || positionValidator == null || rotationManager == null )
        {
            Debug.LogError ( "Одна или несколько зависимостей не инициализированы!" );
            return;
        }

        _spawner.Initialize ( tetrominoFactory , positionValidator , rotationManager );

        // Добавляем классы в список
   
        stateClasses.Add ( _uiManager ); // Добавляем UI Manager (если он реализует IStateClass)
        stateClasses.Add ( _tetrisGridManager ); // Добавляем Tetris Grid Manager (если он реализует IStateClass)
        stateClasses.Add ( _spawner ); // Добавляем Spawner (если он реализует IStateClass)
        stateClasses.Add ( _cameraController ); // Добавляем Camera Controller (если он реализует IStateClass)
        _uiManager.ShowPanel ( GamePanel.StartPanel );
        // StartClass ();
    }

    public void GameOver( )
    {
        Debug.Log ( "GameOver" );
        Time.timeScale = 0f; // Останавливаем все процессы
        _uiManager.ShowPanel ( GamePanel.EndPanel ); // Показываем панель завершения игры
    }

    public void StartClass( )
    {
        Debug.Log ( "StartGame" );
        Time.timeScale = 1f; // Возвращаем нормальную скорость времени при старте игры

        foreach ( var stateClass in stateClasses )
        {
            stateClass.StartClass (); // Вызов StartClass для всех классов
        }

        _uiManager.HideAllAndReset ();
        _uiManager.ShowPanel ( GamePanel.GameHudPanel );
        _spawner.NewTetromino ();
    }

    public void Pause( )
    {
        Debug.Log ( "Pause" );
        foreach ( var stateClass in stateClasses )
        {
            stateClass.Pause (); // Вызов Pause для всех классов
        }

        _uiManager.ShowPanel ( GamePanel.PausePanel );
        Time.timeScale = 0f; // Ставим игру на паузу
    }

    public void Resume( )
    {
        Debug.Log ( "Resume" );
        foreach ( var stateClass in stateClasses )
        {
            stateClass.Resume (); // Вызов Resume для всех классов
        }

        _uiManager.HideAllAndReset ();
        _uiManager.ShowPanel ( GamePanel.GameHudPanel );
        Time.timeScale = 1f; // Возвращаем время в нормальный ход для снятия паузы
    }

    public void Restart( )
    {
        foreach ( var stateClass in stateClasses )
        {
            stateClass.Restart (); // Вызов Restart для всех классов
        }

        // Вызов перезапуска игры
        Debug.Log ( "Restarting Game" );
        Time.timeScale = 0f;

        // Сбрасываем состояние сетки
        if ( _tetrisGridManager != null )
        {
            _tetrisGridManager.Restart (); // Сбрасываем сетку
        }

        // Перезапускаем UI
        if ( _uiManager != null )
        {
            _uiManager.HideAllAndReset ();
            _uiManager.ShowPanel ( GamePanel.GameHudPanel );
        }

        // Удаляем старые объекты и перезапускаем игру
        Destroy ( _spawner.gameObject );
        Destroy ( _tetrisGridManager.gameObject );
        Destroy ( _cameraController.gameObject );
        Destroy ( _uiManager.gameObject );

        StartGame (); // Перезапускаем игру
    }
}
