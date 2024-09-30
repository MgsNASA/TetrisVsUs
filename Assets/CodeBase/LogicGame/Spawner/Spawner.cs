using UnityEngine;

public class Spawner : MonoBehaviour
{
    private ITetrominoFactory _tetrominoFactory;
    private IPositionValidator _positionValidator;
    private IRotationManager _rotationManager;
    private Vector3 lastSpawnPosition;
    public int maxSpawnShift = 4;

    // ћетод дл€ инициализации через зависимости
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
                Debug.LogError ( "Ќе удалось создать новый тетромино!" );
                return;
            }

            if ( _positionValidator.ValidMove ( newTetromino.transform ) )
            {
                lastSpawnPosition = spawnPosition;
                return;
            }
            else
            {
                Destroy ( newTetromino );
            }
        }

        Debug.LogWarning ( "Ќе удалось найти допустимую позицию дл€ спавна тетромино!" );
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
}