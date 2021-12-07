using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EasyButtons;
using UnityEngine;
using Util;

namespace Maze
{
    public class Chunk : MonoBehaviour
    {
        public IntCoord Coordinate;

        private ChunkManager _manager;
        private System.Random _random;
        

        private ChunkManager Manager
        {
            get
            {
                if (!_manager)
                    _manager = FindObjectOfType<ChunkManager>();

                return _manager;
            }
        }

        private HashSet<IntCoord> _wallCoordinates = new HashSet<IntCoord>();
        
        private int GridsExtent => Manager.gridsExtent;
        public float CellSize => Manager.chunkSize / (GridsExtent * 2 + 1);


        private SssnakeMaze _sssnakeMaze;
        
        // Start is called before the first frame update
        public void Setup(IntCoord coord)
        {
            Coordinate = coord;
            
            _random = new System.Random(Coordinate.ToSeed());
            
            _sssnakeMaze = new SssnakeMaze(Coordinate.ToSeed(), GridsExtent);
            _sssnakeMaze.BuildMaze(new IntCoord(0,0,0));
            Debug.Log("snakemaze done");
            
            VisualizePath();
            BuildWalls();
        }

        [Button]
        public void VisualizePath()
        {
            foreach (var cell in _sssnakeMaze.Cells)
            {
                if (cell.Value.Parent != null)
                {
                    Debug.DrawLine(
                        transform.TransformPoint(cell.Key.ToPosition(CellSize)),
                        transform.TransformPoint(cell.Value.Parent.Coord.ToPosition(CellSize)), 
                        Color.green, 200);
                }
            }
        }

        private bool pauseWallBuild = true;
        
        [Button]
        private async void BuildWalls()
        {
            //north walls
            int northSkip = _random.Next(-GridsExtent, GridsExtent);
            for (int i = -GridsExtent; i <= GridsExtent; i++)
                if(i != northSkip)
                    BuildWall(new IntCoord(i * 2, 0, GridsExtent * 2 + 1));
            BuildWall(new IntCoord(northSkip * 2 + 1, 0, GridsExtent * 2));

            
            //west walls
            int westSkip = _random.Next(-GridsExtent, GridsExtent);
            for (int i = -GridsExtent; i <= GridsExtent; i++)
                if(i != northSkip)
                    BuildWall(new IntCoord(GridsExtent * 2 + 1, 0, i * 2));
            BuildWall(new IntCoord(GridsExtent * 2 , 0, westSkip * 2 + 1));

            // while (pauseWallBuild)
            // {
            //     await UniTask.NextFrame();
            // }
            // pauseWallBuild = true;
            

            
            foreach (var cell in _sssnakeMaze.Cells.Values)
            {
                List<IntCoord> neighbors = cell.Coord.Surrounding(false);

                foreach (var neighbor in neighbors)
                {
                    if(neighbor.Furthest() > GridsExtent)
                        continue;
                    if(neighbor.Equals(cell.Parent?.Coord))
                        continue;

                    bool isChild = false;
                    foreach (var child in cell.Cildren)
                        if (neighbor.Equals(child.Coord))
                            isChild = true;

                    if(isChild)
                        continue;
                    
                    
                    _wallCoordinates.Add(Between(cell.Coord, neighbor) );
                }


            }

            
            //do the build
            foreach (var wallCoordinate in _wallCoordinates)
            {
                BuildWall(wallCoordinate);
                
                
            }            
            

            IntCoord Between(IntCoord first, IntCoord second)
            {
                return new IntCoord(
                    (first.x * 2 + second.x * 2) / 2,
                    (first.y * 2 + second.y * 2) / 2,
                    (first.z * 2 + second.z * 2) / 2);
            }

            
            // void WallNorth(IntCoord coord)
            // {
            //     Vector3 cellPosition = coord.ToPosition(CellSize);
            //     Transform wallTform = Instantiate(Manager.WallPrefab, transform).transform;
            //     wallTform.localPosition = new Vector3(
            //         cellPosition.x, 0, cellPosition.z + (CellSize / 2));
            //     wallTform.localScale = new Vector3(CellSize, 2, .1f);
            // }
            //
            // void WallWest(IntCoord coord)
            // {
            //     Vector3 cellPosition = coord.ToPosition(CellSize);
            //     Transform wallTform = Instantiate(Manager.WallPrefab, transform).transform;
            //     wallTform.localPosition = new Vector3(
            //         cellPosition.x + (CellSize / 2) , 0, cellPosition.z );
            //     wallTform.localScale = new Vector3(.1f, 2, CellSize);
            // }


            void BuildWall(IntCoord coord)
            {
                Vector3 wallPosition = coord.ToPosition(CellSize / 2);
                Transform wallTform = Instantiate(Manager.WallPrefab, transform).transform;
                wallTform.localPosition = new Vector3(
                    wallPosition.x, 0, wallPosition.z);
                
                if(coord.x % 2 == 0)
                    wallTform.localScale = new Vector3(CellSize, 2, .1f);
                else
                    wallTform.localScale = new Vector3(.1f, 2, CellSize);
            }
            
        }


        [Button]
        public void NextStep()
        {
            pauseWallBuild = false;
        }
        
        public void PopulateSurrounding()
        {
            Manager.PopulateByPosition(Coordinate);
        }
    }
}
