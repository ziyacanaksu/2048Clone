
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;
    public float minSwipeDistance = 50f; // Minimum distance for a valid swipe
    [SerializeField]  private Gridİnitialize Grid;

    [SerializeField] private TileMaterialManager MaterialManager;
    [SerializeField] private Tile tilePrefab; // Reference to your Tile prefab
    Tile[,] movementGrid ;
    



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse button pressed, store starting position
            startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Mouse button released, store end position and detect swipe
            endMousePosition = Input.mousePosition;
            DetectSwipe();
        }
    }
    void Start(){
       movementGrid =  Grid.GetComponent<Gridİnitialize>().grid;


    }
       

    private void DetectSwipe()
    {
        Vector2 swipeDirection = endMousePosition - startMousePosition;
        float distance = swipeDirection.magnitude;

        // Check if the swipe is long enough to be considered a swipe
        if (distance > minSwipeDistance)
        {
            swipeDirection.Normalize();

            // Check horizontal and vertical direction
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                // Horizontal Swipe
                if (swipeDirection.x > 0)
                {
                    Debug.Log("Swipe Right");
                    MoveRight(movementGrid);
                    Grid.SpawnRandomTile();
                    CheckWin();

                   
                }
                else
                {
                    Debug.Log("Swipe Left");
                    MoveLeft(movementGrid);
                    Grid.SpawnRandomTile();
                    CheckWin();
                }
            }
            else
            {
                // Vertical Swipe
                if (swipeDirection.y > 0)
                {
                    Debug.Log("Swipe Up");
                    MoveUp(movementGrid);
                    Grid.SpawnRandomTile();
                    CheckWin();
                }
                else
                {
                    Debug.Log("Swipe Down");
                    MoveDown(movementGrid);
                    Grid.SpawnRandomTile();
                    CheckWin();
                }
            }
        }
    }


void MoveRight(Tile[,]grid  )
{
    

    for (int y = 0; y < 4; y++)
    {
        for (int x = 2; x >= 0; x--) // Start from the second to last column and move left
        {
            if (grid[x, y] != null)
            {
                Tile currentTile = grid[x, y];
                int nextX = x;

                while (nextX < 3 && grid[nextX + 1, y] == null) // Find the next non-empty spot or edge
                {
                    nextX++;
                }
                
                

                if (nextX != x) // If the tile can move
                {
                    Vector2Int oldPos = new Vector2Int(x,y);
                    Vector2Int newPos = new Vector2Int(nextX,y);
                    Grid.emptyPositions.Add(oldPos);
                    Grid.emptyPositions.Remove(newPos);
                    grid[x,y].transform.Translate( new UnityEngine.Vector3(nextX-x,0,0));
                    grid[x, y] = null;
                    grid[nextX, y] = currentTile;
                    currentTile.SetPosition(nextX, y); // Move the tile in the grid and update its position
                    

                    
                }
                if(nextX <3){
                        
                        CheckMerge(grid[nextX, y],grid[nextX +1, y]);}
                    

                // Merge tiles with the same number

            }
        }
    }


}
void MoveUp(Tile[,] grid)
{
    

    for (int x = 0; x < 4; x++)
    {
        for (int y = 2; y >= 0; y--) // Start from the second to last row and move down
        {
            if (grid[x, y] != null)
            {
                Tile currentTile = grid[x, y];
                int nextY = y;

                while (nextY < 3 && grid[x, nextY + 1] == null) // Find the next non-empty spot or edge
                {
                    nextY++;
                }

                if (nextY != y) // If the tile can move
                {
                    Vector2Int oldPos = new Vector2Int(x,y);
                    Vector2Int newPos = new Vector2Int(x,nextY);
                    Grid.emptyPositions.Add(oldPos);
                    Grid.emptyPositions.Remove(newPos);
                    grid[x, y].transform.Translate(new UnityEngine.Vector3(0, 0, nextY - y));
                    grid[x, y] = null;
                    grid[x, nextY] = currentTile;
                    currentTile.SetPosition(x, nextY); // Move the tile in the grid and update its position
                    
                }

                if (nextY < 3)
                {
                    CheckMerge(grid[x, nextY], grid[x, nextY + 1]);
                }
            }
        }
    }
}

void MoveDown(Tile[,] grid)
{
    

    for (int x = 0; x < 4; x++)
    {
        for (int y = 1; y < 4; y++) // Start from the second row and move up
        {
            if (grid[x, y] != null)
            {
                Tile currentTile = grid[x, y];
                int nextY = y;

                while (nextY > 0 && grid[x, nextY - 1] == null) // Find the next non-empty spot or edge
                {
                    nextY--;
                }

                if (nextY != y) // If the tile can move
                {
                    Vector2Int oldPos = new Vector2Int(x,y);
                    Vector2Int newPos = new Vector2Int(x,nextY);
                    Grid.emptyPositions.Add(oldPos);
                    Grid.emptyPositions.Remove(newPos);
                    grid[x, y].transform.Translate(new UnityEngine.Vector3(0, 0, nextY - y));
                    grid[x, y] = null;
                    grid[x, nextY] = currentTile;
                    currentTile.SetPosition(x, nextY); // Move the tile in the grid and update its position
                    
                }

                if (nextY > 0)
                {
                    CheckMerge(grid[x, nextY], grid[x, nextY - 1]);
                }
            }
        }
    }
}

void MoveLeft(Tile[,] grid)
{
    

    for (int y = 0; y < 4; y++)
    {
        for (int x = 1; x < 4; x++) // Start from the second column and move left
        {
            if (grid[x, y] != null)
            {
                Tile currentTile = grid[x, y];
                int nextX = x;

                while (nextX > 0 && grid[nextX - 1, y] == null) // Find the next non-empty spot or edge
                {
                    nextX--;
                }

                if (nextX != x) // If the tile can move
                {
                    Vector2Int oldPos = new Vector2Int(x,y);
                    Vector2Int newPos = new Vector2Int(nextX,y);
                    Grid.emptyPositions.Add(oldPos);
                    Grid.emptyPositions.Remove(newPos);
                    grid[x, y].transform.Translate(new UnityEngine.Vector3(nextX - x, 0, 0));
                    grid[x, y] = null;
                    grid[nextX, y] = currentTile;
                    currentTile.SetPosition(nextX, y); // Move the tile in the grid and update its position
                    
                }

                if (nextX > 0)
                {
                    CheckMerge(grid[nextX, y], grid[nextX - 1, y]);
                }
            }
        }
    }
}
private void CheckMerge(Tile self ,Tile adjacent){
    if(self.GetNumber()==adjacent.GetNumber()){
        
        

        Grid.SpawnTile((int)adjacent.position.x,(int)adjacent.position.z,adjacent.GetNumber()*2);
        Vector2Int old = new Vector2Int((int)self.position.x,(int)self.position.z);
        self.DestroyTile();
        movementGrid[(int)self.position.x,(int)self.position.z] = null;
        adjacent.DestroyTile();
        Grid.emptyPositions.Add(old);
        if(adjacent.GetNumber()*2== 2048){
            Debug.Log("You Win!");
        }
       

    }

}

private void CheckWin(){
    if(Grid.emptyPositions.Count == 0){
        Debug.Log("You Lost");
    }
}
}
