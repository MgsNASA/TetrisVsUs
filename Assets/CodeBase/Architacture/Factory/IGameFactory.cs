using UnityEngine;

public interface IGameFactory: IService
{
    GameObject CreateHero( GameObject at);
    GameObject CreateObject( string asset );
    void CreateHud( );
  //  void CreateCube( Vector3 at , Quaternion rotation );
    GameObject CreateRandomCube( Vector3 at , Quaternion rotation );
}