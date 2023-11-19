using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridİnitialize : MonoBehaviour
{
    
    public Tile[,] grid = new Tile[4,4] ;
    [SerializeField] private TileMaterialManager MaterialManager;
    [SerializeField] private Tile tilePrefab; // Reference to your Tile prefab
    public  List<Vector2Int> emptyPositions = new List<Vector2Int>();

    private int[] StartValues;
    void Start()
    {
        initializeGrid();
        
    }
    private void Awake(){
        StartValues = new int[] {2,2,4};
    }

  
    void initializeGrid(){
        int Randx = Random.Range(0,4);
        int Randy = Random.Range(0,4);
        int Randx1 = Random.Range(0,4);
        int Randy1 = Random.Range(0,4);
        int Number1 = StartValues[Random.Range(0,3)];
        int Number2 = StartValues[Random.Range(0,3)];


        for( int i = 0 ; i< 4; i++){
            for (int y = 0 ; y < 4 ;y++){

                grid[i,y] = null;
                emptyPositions.Add(new Vector2Int(i, y));

            }
        }
        SpawnRandomTile();
        SpawnRandomTile();

      






    }
    public void SpawnTile(int Positionx,int Positiony,int TileNumber){
                Tile RandTile1 = Instantiate(tilePrefab , new Vector3(Positionx,0.3f,Positiony),Quaternion.identity);
                RandTile1.SetPosition(Positionx,Positiony);
                RandTile1.SetNumber(TileNumber);
                var childComponent = RandTile1.GetComponentInChildren<Renderer>();
                Vector2Int TilePos = new Vector2Int(Positionx,Positiony);
                emptyPositions.Remove(TilePos);
                childComponent.material = MaterialManager.GetMaterialForValue(TileNumber);
                grid[Positionx,Positiony] = RandTile1;

    }

    public void SpawnRandomTile(){
        int firstTileIndex = Random.Range(0, emptyPositions.Count);
        Vector2Int firstTilePos = emptyPositions[firstTileIndex];
        emptyPositions.RemoveAt(firstTileIndex); // Remove the position from the list
        int Number1 = StartValues[Random.Range(0, StartValues.Length)];
        SpawnTile(firstTilePos.x, firstTilePos.y, Number1);

    }
}
