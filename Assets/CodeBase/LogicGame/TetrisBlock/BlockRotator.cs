using UnityEngine;

public class BlockRotator : IBlockRotator
{
    private readonly ITetrisGridManager gridManager;

    public BlockRotator( ITetrisGridManager gridManager )
    {
        this.gridManager = gridManager;
    }

    public void Rotate( Transform blockTransform , Vector3 rotationPoint )
    {
        blockTransform.RotateAround ( rotationPoint , Vector3.forward , 90 );

        if ( !gridManager.ValidMove ( blockTransform, rotationPoint ) )
        {
            blockTransform.RotateAround ( rotationPoint , Vector3.forward , -90 );
        }
    }
}
