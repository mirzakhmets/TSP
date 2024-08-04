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
using System.Collections.Generic;

namespace TSP
{
	class Program
	{
		public static void Main(string[] args)
		{
			Path.Initialize(10, 2000);
			
			Console.WriteLine(Path.PathLength(Path.Optimal));
			
			for (int i = 0; i < Path.N; ++i) {
				Console.Write(Path.Optimal[i] + " ");
			}
			
			Console.WriteLine();
			
			List<Path> current = new List<Path>();
			
			for (int i = 0; i < Path.N; ++i) {
				Path p = new Path();
				
				p.items.Add(i);
				
				current.Add(p);
			}
			
			while (current.Count > 1) {
				Path nPath = null;
				
				Path pa = null, pb = null;
				
				for (int i = 0; i < current.Count; ++i) {
					for (int j = 0; j < current.Count; ++j) {
						if (i != j) {
							for (int k = 0; k < 2; ++k) {
								Path a = (Path) current[i];
								
								for (int l = 0; l < 2; ++l) {
									Path b = (Path) current[j];
									
									Path z = new Path();
									
									z.Merge(a);
									
									z.Merge(b);
									
									bool cont = false;
									
									do {
										cont = false;
										
										for (int pi = 0; pi < z.items.Count; ++pi) {
											for (int pj = pi + 1; pj < z.items.Count; ++pj) {
												int lk = z.Length();
												
												int e = (int) z.items[pi];
													
												z.items[pi] = z.items[pj];
													
												z.items[pj] = e;
												
												if (lk > z.Length()) {													
													cont = true;
													
													goto aab;
												}
												
												e = (int) z.items[pi];
													
												z.items[pi] = z.items[pj];
													
												z.items[pj] = e;																								
											}
										}
										
									aab:
										;
									} while (cont);
									
									if (nPath == null || nPath.Length() > z.Length()) {
										nPath = z;
										
										pa = current[i];
										
										pb = current[j];
									}
									
									b.Reverse();
								}
								
								a.Reverse();
							}
						}
					}
				}
				
				foreach (Path p in current) {
					Console.WriteLine(p.Length());
					
					for (int i = 0; i < p.items.Count; ++i) {
						Console.Write(p.items[i] + " ");
					}
					
					Console.WriteLine();
					
					Console.WriteLine("--");
				}
				
				Console.WriteLine();
				
				if (nPath != null) {
					current.Add(nPath);
					
					if (pa != null) {
						current.Remove(pa);
					}
					
					if (pb != null) {
						current.Remove(pb);
					}
				}
			}
			
			Console.WriteLine(current[0].Length());
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}