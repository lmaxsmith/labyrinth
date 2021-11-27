using System.Collections.Generic;
using Util;

namespace Maze
{
	public class MazeCell
	{
		public IntCoord Coord;
		public MazeCell Parent;
		public HashSet<MazeCell> Cildren = new HashSet<MazeCell>();

		public MazeCell(IntCoord coord)
		{
			Coord = coord;
		}


		public override string ToString()
		{
			return $"MazeCell({Coord.x},{Coord.y},{Coord.z})";
		}
	}
}