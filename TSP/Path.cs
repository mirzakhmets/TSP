/*
 * Created by SharpDevelop.
 * User: mspma
 * Date: 6/9/2024
 * Time: 3:42 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace TSP
{
	/// <summary>
	/// Path candidate.
	/// </summary>
	public class Path
	{
		public static int N;
		
		public static int [,] W;
		
		public static int[] Optimal, Current;
		
		public static Random random = new Random(System.DateTime.Now.Millisecond);
		
		public static int PathLength (int[] path) {
			int result = 0;
			
			for (int i = 1; i < path.Length; ++i) {
				result += W[path[i - 1], path[i]];
			}
			
			return result;
		}
		
		public static void Initialize (int N, int MaxLength) {
			Path.N = N;
			
			W = new int[N, N];
			
			for (int i = 0; i < N; ++i) {
				for (int j = 0; j < N; ++j) {
					W[i, j] = random.Next(MaxLength);
				}
			}
			
			Optimal = new int[N];
			
			Current = new int[N];
			
			for (int i = 0; i < N; ++i) {
				Optimal[i] = i;
				
				Current[i] = i;
			}
			
			Permute(0, N - 1);
		}
		
		public static void Permute(int l, int r) {
			if (l == r) {
				if (PathLength(Current) < PathLength(Optimal)) {
					for (int i = 0; i < N; ++i) {
						Optimal[i] = Current[i];
					}
				}
			} else {
				for (int i = l; i <= r; ++i) {
					int k = Current[i];
					Current[i] = Current[l];
					Current[l] = k;
					
					Permute(l + 1, r);
					
					k = Current[i];
					Current[i] = Current[l];
					Current[l] = k;
				}
			}
		}
		
		public ArrayList items = new ArrayList();
		
		public Path()
		{
		}
		
		public bool Contains(int v) {
			for (int i = 0; i < items.Count; ++i) {
				if ((int) items[i] == v) {
					return true;
				}
			}
			
			return false;
		}
		
		public int Length() {
			int result = 0;
			
			for (int i = 1; i < items.Count; ++i) {
				result += W[(int) items[i - 1], (int) items[i]];
			}
			
			return result;
		}
		
		public void Reverse() {
			this.items.Reverse();
		}
		
		public void Merge(Path path) {
			for (int i = 0; i < path.items.Count; ++i) {
				this.items.Add(path.items[i]);
			}
		}
	}
}
