using UnityEngine;
using System.Collections.Generic;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    public GameObject playerPrefab;
    public GameObject cubePrefab; 
    public Vector3 CellSize = new Vector3(1, 1, 0);

    public Maze maze;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }

        SpawnPlayer();
        SpawnCubes(10); 
    }

    private void SpawnPlayer()
    {
        List<MazeGeneratorCell> validSpawnCells = new List<MazeGeneratorCell>();

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                if (maze.cells[x, y].WallLeft == false || maze.cells[x, y].WallBottom == false)
                {
                    validSpawnCells.Add(maze.cells[x, y]);
                }
            }
        }

        if (validSpawnCells.Count > 0)
        {
            MazeGeneratorCell spawnCell = validSpawnCells[Random.Range(0, validSpawnCells.Count)];
            Vector3 spawnPosition = new Vector3(spawnCell.X * CellSize.x, 5.0f, spawnCell.Y * CellSize.z);
            Object.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void SpawnCubes(int count)
    {
        List<MazeGeneratorCell> validSpawnCells = new List<MazeGeneratorCell>();


        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                if (maze.cells[x, y].WallLeft == false && maze.cells[x, y].WallBottom == false)
                {
                    validSpawnCells.Add(maze.cells[x, y]);
                }
            }
        }

        // Спавним кубы
        for (int i = 0; i < count; i++)
        {
            if (validSpawnCells.Count > 0)
            {
                MazeGeneratorCell spawnCell = validSpawnCells[Random.Range(0, validSpawnCells.Count)];
                Vector3 spawnPosition = new Vector3(spawnCell.X * CellSize.x, 0.5f, spawnCell.Y * CellSize.z); 

                validSpawnCells.Remove(spawnCell);

                Object.Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}