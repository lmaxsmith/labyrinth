using UnityEngine;
using Util;

namespace Maze
{
    public class Chunk : MonoBehaviour
    {
        public IntCoord Coordinate;

        private ChunkManager _manager;

        private ChunkManager Manager
        {
            get
            {
                if (!_manager)
                    _manager = FindObjectOfType<ChunkManager>();

                return _manager;
            }
        }


        private SssnakeMaze _sssnakeMaze;
        
        // Start is called before the first frame update
        void Start()
        {
            _sssnakeMaze = new SssnakeMaze(Coordinate.ToSeed(), 10);
            _sssnakeMaze.BuildMaze(new IntCoord(0,0,0));
            Debug.Log("snakemaze done");
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        public void PopulateSurrounding()
        {
            Manager.PopulateByPosition(Coordinate);
        }
    }
}
