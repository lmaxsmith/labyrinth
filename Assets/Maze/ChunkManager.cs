using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Maze
{
    public class ChunkManager : MonoBehaviour
    {
        [Tooltip("Size of chunks, in meters")]
        public float chunkSize = 40;
        [Tooltip("In extents format. # of grid sections between center and edge.")]
        public int gridsExtent = 10;

        public GameObject WallPrefab;
        
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
            PopulateByPosition(position.ToIntCoord(chunkSize));
        

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
            var blah = coord.ToPosition(chunkSize);
            var chunk = Instantiate(
                _chunkPrefab, coord.ToPosition(chunkSize), Quaternion.identity, transform).GetComponent<Chunk>();
            chunk.Setup(coord);
            
            Chunks.Add(coord, chunk);
        }

        #endregion /Populate ===------------------
    }
}
