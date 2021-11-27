using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Util
{
	public struct IntCoord
	{
		public int x;
		public int y;
		public int z;

		public IntCoord(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public IntCoord Zero()
		{
			return new IntCoord(0, 0, 0);
		}
		
		
		
		
		
		
		
		
        /// <summary>
        /// Finds all the surrounding coordinates around a given coordinate
        /// (only lists coordinates. Independent of use of coordinates. 
        /// </summary>
        /// <param name="></param>
        /// <param name="includeY"></param>
        /// <returns></returns>
        public List<IntCoord> Surrounding(bool includeCorners, bool includeY = false)
        {
            //
            List<IntCoord> surrounding = new List<IntCoord>()
            {
                //left right
                new IntCoord(x+1, y, z),
                new IntCoord(x-1, y, z),
                //forward backward
                new IntCoord(x, y, z+1),
                new IntCoord(x, y, z-1)
            };
            
            if (includeCorners)
            {
	            //corners
	            surrounding.Add(new IntCoord(x + 1, y, z + 1));
	            surrounding.Add(new IntCoord(x + 1, y, z - 1));
	            surrounding.Add(new IntCoord(x - 1, y, z + 1));
	            surrounding.Add(new IntCoord(x - 1, y, z - 1));

            }
            

            if (includeY)
            {
                //gayly up down
                List<IntCoord> ySurrounding = new List<IntCoord>()
                    {
                        new IntCoord(x, y + 1, z),
                        new IntCoord(x, y - 1, z)
                    };
                
                //2d surround up
                foreach (var s in surrounding)
                    ySurrounding.Add(new IntCoord(s.x, s.y+1, s.z));
                
                //2d srround down
                foreach (var s in surrounding)
                    ySurrounding.Add(new IntCoord(s.x, s.y-1, s.z));
                
                surrounding.AddRange(ySurrounding);
            }

            return surrounding;
        }

		/// <summary>
		/// The absolute value of the integer furthest from 0 in any direction;
		/// </summary>
		/// <returns></returns>
        public int Furthest()
        {
	        int furthest = 0;

	        furthest = Math.Max(Math.Abs(x), Math.Abs(y));
	        furthest = Math.Max(furthest, Math.Abs(z));
	        return furthest;
        }


		public int ToSeed()
		{
			return x * 100 + y * 10 + z;
		}

		public override string ToString()
		{
			return $"({x}, {y}, {z})";
		}

		public bool Equals(IntCoord other)
		{
			return (x == other.x && y == other.y && z == other.z);
		}
	}
}