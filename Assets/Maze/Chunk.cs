using System;
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
        
        [Button]
        private void BuildWalls()
        {
            //north walls
            int topSkip = _random.Next(-GridsExtent, GridsExtent);
            for (int i = -GridsExtent; i <= GridsExtent; i++)
            {
                if(i == topSkip)
                    continue;

                IntCoord coord = new IntCoord(i, 0, GridsExtent);
                WallNorth(coord);
            }
            //west walls
            int westSkip = _random.Next(-GridsExtent, GridsExtent);
            for (int i = -GridsExtent; i <= GridsExtent; i++)
            {
                if(i == westSkip)
                    continue;

                IntCoord coord = new IntCoord(-GridsExtent, 0, i);
                WallWest(coord);
            }












            void WallNorth(IntCoord coord)
            {
                Vector3 cellPosition = coord.ToPosition(CellSize);
                Transform wallTform = Instantiate(Manager.WallPrefab, transform).transform;
                wallTform.localPosition = new Vector3(
                    cellPosition.x, 0, cellPosition.z + (CellSize / 2));
                wallTform.localScale = new Vector3(CellSize, 2, .1f);
            }

            void WallWest(IntCoord coord)
            {
                Vector3 cellPosition = coord.ToPosition(CellSize);
                Transform wallTform = Instantiate(Manager.WallPrefab, transform).transform;
                wallTform.localPosition = new Vector3(
                    cellPosition.x + (CellSize / 2) , 0, cellPosition.z );
                wallTform.localScale = new Vector3(.1f, 2, CellSize);

            }
        }

        public void PopulateSurrounding()
        {
            Manager.PopulateByPosition(Coordinate);
        }
    }
}
