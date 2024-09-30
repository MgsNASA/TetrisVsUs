using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 RotationPoint
    {
        get; private set;
    }
    private float previousTime;
    private float fallTime = 0.8f;

    public event System.Action OnBlockStopped;
    public event System.Action OnNewTetrominoRequested;

    private IBlockMover blockMover;
    private IBlockRotator blockRotator;

    public TetrisBlock( IBlockMover blockMover , IBlockRotator blockRotator )
    {
        this.blockMover = blockMover;
        this.blockRotator = blockRotator;
    }

    void Awake( )
    {
        // Создаем экземпляры зависимостей напрямую
        ITetrisGridManager gridManager = FindObjectOfType<TetrisGridManager> ();

        blockMover = new BlockMover ( gridManager );
        blockRotator = new BlockRotator ( gridManager );
    }

    void Start( )
    {
        CalculateRotationPoint ();
    }

    void Update( )
    {
        HandleRotation ();
        HandleFalling ();
    }

    private void HandleRotation( )
    {
        blockRotator.Rotate ( transform , RotationPoint );
    }

    private void HandleFalling( )
    {
        if ( Time.time - previousTime > fallTime )
        {
            blockMover.Move ( Vector3.down , transform );
            previousTime = Time.time;

            if ( !blockMover.ValidMove ( transform ) )
            {
                BlockStopped ();
            }
        }
    }

    private void BlockStopped( )
    {
        OnBlockStopped?.Invoke ();
        OnNewTetrominoRequested?.Invoke ();
        this.enabled = false;
    }

    private void CalculateRotationPoint( )
    {
        Vector3 totalPosition = Vector3.zero;
        int childCount = 0;

        foreach ( Transform child in transform )
        {
            totalPosition += child.localPosition;
            childCount++;
        }

        if ( childCount > 0 )
        {
            RotationPoint = totalPosition / childCount;
        }
    }
}
