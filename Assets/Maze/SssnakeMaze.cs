using System.Collections.Generic;
using UnityEngine;
using Util;
using Random = System.Random;

namespace Maze
{
	public class SssnakeMaze
	{
		private Random _random;

		private int _extent;
		public Dictionary<IntCoord, MazeCell> Cells { get; } = new Dictionary<IntCoord, MazeCell>();

		private List<IntCoord> path = new List<IntCoord>();
		
		
		

		public SssnakeMaze(int seed, int extents)
		{
			_extent = extents;
			_random = new Random(seed);
			//Cells.Add(startPoint, new MazeCell(startPoint));
		}


		public void BuildMaze(IntCoord startPoint)
		{
			int i = 0;

			TryBuildCell(startPoint);


			
			
			MazeCell TryBuildCell(IntCoord coord, MazeCell parent = null)
			{
				if (i++ > 1000)
				{
					Debug.LogWarning("Too much recursion!");
					return null;
				}
				
				//get cell reference
				MazeCell cell;
				if (Cells.ContainsKey(coord))
					cell = Cells[coord];
				else
				{
					cell = new MazeCell(coord);
					Cells.Add(coord, cell);
				}
				if(parent != null)
					cell.Parent = parent;
				
				
				var openNeighbors = OpenNeighbors(coord);
				//if open space available
				if (openNeighbors.Count > 0)
				{
					IntCoord next = ChooseBetween(openNeighbors);
					var nextCell = TryBuildCell(next, cell);
					cell.Cildren.Add(nextCell);
					Cells[coord] = cell; //TODO: this should be unnecessary?
				}
				//if dead end, back up until available
				else
				{
					if(!cell.Coord.Equals(startPoint))
						TryBuildCell(cell.Parent.Coord);
					else
						Debug.Log($"Done building maze section");
				}

				return cell;
			}

			
			
			IntCoord ChooseBetween(List<IntCoord> options)
			{
				return options[ _random.Next(0, options.Count)];
			}
			
			
			List<IntCoord> OpenNeighbors(IntCoord coord)
			{
				List<IntCoord> neighbors = coord.Surrounding(false);
				List<IntCoord> includedNeighbors = new List<IntCoord>();

				foreach (var neighbor in neighbors)
				{
					if(neighbor.Furthest() <= _extent && !Cells.ContainsKey(neighbor))
						includedNeighbors.Add(neighbor);
				}

				return includedNeighbors;
			}
		}
	}
}