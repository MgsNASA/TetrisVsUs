﻿using UnityEngine;

public class GridPositionValidator : IPositionValidator
{
    private TetrisGridManager _gridManager;

    public GridPositionValidator( TetrisGridManager gridManager )
    {
        _gridManager = gridManager;
    }

    public bool ValidMove( Transform tetrominoTransform,Vector3 direction )
    {
        return _gridManager.ValidMove ( tetrominoTransform, direction );
    }
}