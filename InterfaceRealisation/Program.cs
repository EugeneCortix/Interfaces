using InterfaceRealisation;
using System.ComponentModel.Design;

namespace interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            var optimizer = new GradientDescent();
            var optimizer2 = new MinimizerMonteCarlo();
            var optimizer3 = new GaussNewton();
            var initial = new Vector();
            int n = int.Parse(Console.ReadLine());
            List<Vector> points = new();
            for (int i = 0; i < n; i++)
            {
                var str = Console.ReadLine().Split();
                var v = new Vector();
                for(int j = 0; j < str.Length; j++)
                {
                    v.Add(Convert.ToDouble(str[j]));
                }
              points.Add(v);
            }
            for (int i = 0;i < points[0].Count; i++) { initial.Add(1);}
            var functinal = new L1() { points = points };
            var fun = new LineFunction();
            optimizer.points = points;
            optimizer3.points = points;
            var res = optimizer.Minimize(functinal, fun, initial);
            Console.WriteLine(typeres(res));
            res = optimizer2.Minimize(functinal, fun, initial);
            Console.WriteLine(typeres(res));
            res = optimizer3.Minimize(functinal, fun, initial);
            Console.WriteLine(typeres(res));

        }
        static public string typeres(IVector v)
        {
            string res = "";
            char paramname = 'a';
            for (int i = 0; i < v.Count; i++)
            {
                res += paramname + " = " + Convert.ToString(v[i]) + ' ';
                paramname++;
            }
            return res;
        }
    }
}