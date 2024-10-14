using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameProcessController : MonoBehaviour, IStateClass
{
    public GameObject uiManagerPrefab;
    public GameObject spawnerPrefab;
    public GameObject tetrisGridManagerPrefab;
    public GameObject cameraControllerPrefab;
    public GameObject player;
    public GameObject musicSerivcePrefab;
    private CharacterController _characterController;
    public UiManager _uiManager;
    public Spawner _spawner;
    public TetrisGridManager _tetrisGridManager;
    public CameraController _cameraController;
    public MusicService _musicService;

    // Список классов, реализующих интерфейс IStateClass
    private List<IStateClass> stateClasses = new List<IStateClass> ();

    public void StartGame( )
    {
        player = AllServices.Container.Single<IGameFactory> ().CreateObject ( "Prefab/Player/Player" );
        _characterController = player.GetComponent<CharacterController> ();
        // Создаем экземпляры из префабов
        _uiManager = Instantiate ( uiManagerPrefab , transform ).GetComponent<UiManager> ();
        _uiManager.Initialize (player,this);
        _tetrisGridManager = Instantiate ( tetrisGridManagerPrefab ).GetComponent<TetrisGridManager> ();
        _spawner = Instantiate ( spawnerPrefab ).GetComponent<Spawner> ();
        _musicService = Instantiate ( musicSerivcePrefab ).GetComponent<MusicService> ();
        _cameraController = Instantiate ( cameraControllerPrefab ).GetComponent<CameraController> ();
        _cameraController.Initialize ( _uiManager.GetComponent<VerticalMovementTracker>());
        // Инициализация спаунера через сервисы
        var tetrominoFactory = AllServices.Container.Single<ITetrominoFactory> ();
        var positionValidator = AllServices.Container.Single<IPositionValidator> ();
        var rotationManager = AllServices.Container.Single<IRotationManager> ();
        _spawner.Initialize ( tetrominoFactory , positionValidator , rotationManager );
        stateClasses.Add ( _uiManager ); // Добавляем UI Manager (если он реализует IStateClass)
        stateClasses.Add ( _tetrisGridManager ); // Добавляем Tetris Grid Manager (если он реализует IStateClass)
        stateClasses.Add ( _spawner ); // Добавляем Spawner (если он реализует IStateClass)
        stateClasses.Add ( _cameraController ); // Добавляем Camera Controller (если он реализует IStateClass)
        _musicService.currentTrackIndex = 0;
        _musicService.PlayMusic ();
        _spawner.Restart ();

   
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

        // Включаем первый трек (индекс 0)
        _musicService.currentTrackIndex = 1;
        _musicService.PlayMusic ();

        foreach ( var stateClass in stateClasses )
        {
            stateClass.StartClass (); // Вызов StartClass для всех классов
        }

        _uiManager.HideAllPanels ();
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

        _uiManager.HideAllPanels ();
        _uiManager.ShowPanel ( GamePanel.GameHudPanel );

        // Включаем второй трек (индекс 1) при возобновлении игры
        _musicService.currentTrackIndex = 1;
        _musicService.PlayMusic ();

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
            _uiManager.HideAllPanels ();
            _uiManager.ShowPanel ( GamePanel.GameHudPanel );
        }
        Time.timeScale = 1f; // Возвращаем время в нормальный ход для снятия паузы
        Destroy (_spawner);
        Destroy ( _tetrisGridManager.gameObject );
        Destroy ( _cameraController.gameObject );
        Destroy (_uiManager.gameObject );
        Destroy ( player );
        Destroy(_musicService.gameObject );
        StartGame ();
    }
}
