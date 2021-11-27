using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Maze
{
    public class ChunkManager : MonoBehaviour
    {
        public float chunkSize = 4;
        public Dictionary<IntCoord, Chunk> Chunks = new Dictionary<IntCoord, Chunk>();
        public Transform playerTform;
        public GameObject _chunkPrefab;
    
        // Start is called before the first frame update
        void Start()
        {
            PopulateByPosition(Vector3.zero);
            StartCoroutine(CheckPositionCo());
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        #region === Translation ===---------

        private IntCoord PositionToIntCoord(Vector3 position)
        {
            return new IntCoord(
                Mathf.RoundToInt(position.x / chunkSize),
                Mathf.RoundToInt(position.y / chunkSize),
                Mathf.RoundToInt(position.z / chunkSize));
        }

        private Vector3 IntCoordToPosition(IntCoord coord) => 
            new Vector3(coord.x * chunkSize, coord.y * chunkSize, coord.z * chunkSize);
        

        #endregion /Translation ===---------



        
        
        #region === Populate ===------------------

        IEnumerator CheckPositionCo()
        {
            while (true)
            {
                PopulateByPosition(playerTform.position);
                yield return new WaitForSeconds(1);
            }
        }

        
        

        public void PopulateByPosition(Vector3 position) =>
            PopulateByPosition(PositionToIntCoord(position));
        

        /// <summary>
        /// Populates all surrounding chunks based on a coordinate input.
        /// </summary>
        /// <param name="coord"></param>
        public void PopulateByPosition(IntCoord coord)
        {
            var surrounding = coord.Surrounding(true);
            
            if(!Chunks.ContainsKey(coord))
                InstantiateChunk(coord);
            
            foreach (var s in surrounding)
                if(!Chunks.ContainsKey(s))
                    InstantiateChunk(s);
        }

        private void InstantiateChunk(IntCoord coord)
        {
            var chunk = Instantiate(
                _chunkPrefab, IntCoordToPosition(coord), Quaternion.identity, transform).GetComponent<Chunk>();
            chunk.Coordinate = coord;
            Chunks.Add(coord, chunk);
        }

        #endregion /Populate ===------------------
    }
}
