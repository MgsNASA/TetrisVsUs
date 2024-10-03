    using UnityEngine;

    public class GameProcessController : MonoBehaviour
    {
        public GameObject uiManagerPrefab;
        public GameObject spawnerPrefab;
        public GameObject tetrisGridManagerPrefab;

        private UiManager uiManager;
        private Spawner spawner;
        private TetrisGridManager tetrisGridManager;

    private void Awake( )
    {
        // Создаем экземпляры из префабов
        uiManager = Instantiate ( uiManagerPrefab , transform ).GetComponent<UiManager> ();
        tetrisGridManager = Instantiate ( tetrisGridManagerPrefab ).GetComponent<TetrisGridManager> ();
        spawner = Instantiate ( spawnerPrefab ).GetComponent<Spawner> ();

        

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

        spawner.Initialize ( tetrominoFactory , positionValidator , rotationManager );
        GameStart ();
    }




    public void GameOver( )
        {
            Debug.Log ( "GameOver" );
            Time.timeScale = 0f; // Останавливаем все процессы
        }

        public void GameStart( )
        {
            Debug.Log ( "StartGame" );
            Time.timeScale = 1f; // Возвращаем нормальную скорость времени при старте игры
            spawner.NewTetromino ();
        }

        public void GamePause( )
        {
            Debug.Log ( "Pause" );
            Time.timeScale = 0f; // Ставим игру на паузу
        }

        public void GameResume( )
        {
            Debug.Log ( "Resume" );
            Time.timeScale = 1f; // Возвращаем время в нормальный ход для снятия паузы
        }
    }
