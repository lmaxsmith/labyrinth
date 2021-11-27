using EasyButtons;
using UnityEngine;

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
	}
}