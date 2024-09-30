using UnityEngine;

public class BlockMover : IBlockMover
{
    private readonly ITetrisGridManager gridManager;

    public BlockMover( ITetrisGridManager gridManager )
    {
        this.gridManager = gridManager;
    }

    public void Move( Vector3 direction , Transform blockTransform )
    {
        blockTransform.position += direction;

        if ( !gridManager.ValidMove ( blockTransform ) )
        {
            blockTransform.position -= direction;
        }
    }

    public bool ValidMove( Transform tetrisBlock ) // Реализация метода
    {
        return gridManager.ValidMove ( tetrisBlock );
    }
}
