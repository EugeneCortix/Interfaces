using InterfaceRealisation;

namespace interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            var optimizer = new GradientDescent();
            var optimizer2 = new MinimizerMonteCarlo();
            var initial = new Vector();
            initial.Add(1);
            initial.Add(1);
            int n = int.Parse(Console.ReadLine());
            List<(double x, double y)> points = new();
            for (int i = 0; i < n; i++)
            {
                var str = Console.ReadLine().Split();
                points.Add((double.Parse(str[0]), double.Parse(str[1])));
            }
            var functinal = new MyFunctional() { points = points };
            var fun = new LineFunction();

            var p = new Vector();
            p.Add(points[0].x);
            var res = optimizer.Minimize(functinal, fun, initial, p);
            Console.WriteLine($"a={res[0]},b={res[1]}");
            res = optimizer2.Minimize(functinal, fun, initial, p);
            Console.WriteLine($"a={res[0]},b={res[1]}");

        }
        
    }
}