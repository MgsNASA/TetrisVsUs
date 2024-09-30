
    // Интерфейс для управления вводом
    public interface IInputManager :IService
    {
        bool IsMoveLeftPressed( );
        bool IsMoveRightPressed( );
        bool IsMoveDownPressed( );
        bool IsRotatePressed( );
    }
