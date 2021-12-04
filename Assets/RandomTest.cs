using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using Util;

namespace DefaultNamespace
{
	public class RandomTest : MonoBehaviour
	{
		public int seed;
		
		
		[Button]
		public void TestRandom()
		{
			System.Random r = new System.Random(seed);
			
			Random.InitState(seed);
			
			Debug.Log($"random: {r.Next()}");
			
			Debug.Log($"Unity Random: {Random.value}");
		}


		[Button]
		public void TestFrequency()
		{
			TestFrequency(1);
			TestFrequency(2);
			TestFrequency(3);
		}


		public void TestFrequency(int options)
		{
			var r = new System.Random(new IntCoord(0, 0, 0).ToSeed());
			int cycles = 10000;
			List<int> gen = new List<int>();

			for (int i = 0; i < cycles; i++)
			{
				gen.Add(r.Next(0, options ));
			}

			for (int i = 0; i < options; i++)
			{
				int qty = 0;
				foreach (var g in gen)
					if (g == i)
						qty++;
				float percent = qty / (cycles * 1f) * 100;
				Debug.Log($"For {options}, {percent}% {i}.");
			}
			
		}
		
	}
}