using UnityEngine;

public class GameFactory : IGameFactory, ITetrominoFactory
{
    private readonly IAssetProvider assetProvider;
    private readonly string [ ] cubePaths = {
        AssetPath.CubePath1,
        AssetPath.CubePath2,
        AssetPath.CubePath3,
        AssetPath.CubePath4,
        AssetPath.CubePath5,
        AssetPath.CubePath6
    };

    // Пути к префабам тетромино
    private readonly string [ ] tetrominoPaths = {
         AssetPath.CubePath1,
        AssetPath.CubePath2,
        AssetPath.CubePath3,
        AssetPath.CubePath4,
        AssetPath.CubePath5,
        AssetPath.CubePath6
    };

    public GameFactory( IAssetProvider assetProvider )
    {
        this.assetProvider = assetProvider;
    }

    public GameObject CreateHero( GameObject at ) =>
        assetProvider.Instantiate ( AssetPath.HeroPath , at.transform.position );

    public GameObject CreateObject( string asset ) =>
        assetProvider.Instantiate ( asset );

    public void CreateHud( ) =>
        assetProvider.Instantiate ( AssetPath.HudPath );

    public GameObject CreateRandomCube( Vector3 at , Quaternion rotation )
    {
        string randomCubePath = cubePaths [ Random.Range ( 0 , cubePaths.Length ) ];
        var cubePrefab = assetProvider.Instantiate ( randomCubePath , at );
        if ( cubePrefab != null )
        {
            cubePrefab.transform.rotation = rotation;
        }
        return cubePrefab;
    }

    public GameObject CreateRandomTetromino( Vector3 position , Quaternion rotation )
    {
        // Выбираем случайный префаб тетромино
        string randomTetrominoPath = tetrominoPaths [ Random.Range ( 0 , tetrominoPaths.Length ) ];

        // Создаем тетромино на заданной позиции и с заданным поворотом
        var tetrominoPrefab = assetProvider.Instantiate ( randomTetrominoPath , position );
        if ( tetrominoPrefab != null )
        {
            tetrominoPrefab.transform.rotation = rotation;
        }

        return tetrominoPrefab;
    }
}
