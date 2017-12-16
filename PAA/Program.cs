using System;
using System.Diagnostics;

namespace PAA
{
    class Program
    {
        private static int MinKey(int[] key, bool[] mstSet)
        {
            // Initialize min value
            var min = int.MaxValue;
            var minIndex=0;

            for (var v = 0; v < key.Length; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }

            return minIndex;
        }

        private static void PrimMst(int[,] graph)
        {
            var parent = new int[graph.GetLength(0)]; // Array to store constructed MST
            var key = new int[graph.GetLength(0)];   // Key values used to pick minimum weight edge in cut
            var mstSet= new bool[graph.GetLength(0)];  // To represent set of vertices not yet included in MST

            var comparations = 0;
            // Initialize all keys as INFINITE
            for (var i = 0; i < graph.GetLength(0); i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }
            
            key[0] = 0;     
            parent[0] = -1; 

            // O(V^2)
            Stopwatch st = new Stopwatch();
            st.Start();
            for (int count = 0; count < graph.GetLength(0) - 1; count++)
            {

                int u = MinKey(key, mstSet);

                mstSet[u] = true;
                for (int v = 0; v < graph.GetLength(0); v++)
                {
                    if (graph[u, v] <= 0) continue;
                    comparations++;
                    if (mstSet[v]) continue;
                    comparations++;
                    if (graph[u, v] >= key[v]) continue;
                    comparations++;
                    parent[v] = u;
                    key[v] = graph[u, v];
                }
            }
            st.Stop();
            Console.WriteLine(st.ElapsedTicks);
            TimeSpan ts = st.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            Console.WriteLine("Tempo: {0}",elapsedTime);
            Console.WriteLine("Comparacoes: {0}", comparations);
            //printMST(parent, graph);
        }
        static void printMST(int[] parent, int[,] graph)
        {
            Console.WriteLine("Edge   Weight");
            for (int i = 1; i < graph.GetLength(0); i++)
                Console.WriteLine("{0} - {1}    {2}", parent[i], i, graph[i,parent[i]]);
        }
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());
            var graph = new int[size, size];
            for (var i = 0; i < size; i++)
            {
                var line = Console.ReadLine().Split(' ');
                for (var j = 0; j < size; j++)
                {
                    graph[i, j] = int.Parse(line[j]);
                }
            }
            PrimMst(graph);
            Console.ReadLine();
        }
    }
}
