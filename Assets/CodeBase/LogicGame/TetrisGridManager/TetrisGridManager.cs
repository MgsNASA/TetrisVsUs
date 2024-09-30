using System;
using UnityEngine;

public class TetrisGridManager : MonoBehaviour, ITetrisGridManager
{
    public static int Width = 10;
    public static int Height = 52;

    private IGridManager gridManager;

    private BlockFallAnimator fallAnimator;

        void Awake( )
    {
        gridManager = new GridOperations ( Width , Height );
        fallAnimator = new BlockFallAnimator ( 2.0f );
    }
    
    

    public void AddToGrid( Transform tetrisBlock )
    {
        if ( gridManager == null )
        {
            Debug.LogError ( "gridManager is null when trying to add to grid." );
            return;
        }
        gridManager.AddToGrid ( tetrisBlock );
    }

    public bool ValidMove( Transform tetrisBlock )
    {
        if ( gridManager == null )
        {
            Debug.LogError ( "gridManager is null" );
            return false;
        }
        return gridManager.ValidMove ( tetrisBlock );
    }

    public void CheckForLines( )
    {
        if ( gridManager == null )
        {
            Debug.LogError ( "gridManager is null when checking for lines." );
            return;
        }
        gridManager.CheckForLines ();
    }
}