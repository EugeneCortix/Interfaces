using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// 2. IFunctional 
namespace InterfaceRealisation
{
    class MyFunctional : IFunctional
    {
        public List<(double x, double y)> points;
        public double Value(IFunction function)
        {
            double sum = 0;
            foreach (var point in points)
            {
                var param = new Vector();
                param.Add(point.x);
                var s = function.Value(param) - point.y;
                sum += s * s;
            }
            return sum;
        }
    }
    class L1 : IDifferentiableFunctional
    {
        public List<Vector> points;
        public IVector Gradient(IFunction function)
        {
            int n = points[0].Count - 1;
            Vector res = new Vector();
            foreach(var p in points) 
            {
                double gr = Math.Abs(p[n] - function.Value(p));
                res.Add(gr);
            }
            return res;
        }

        public double Value(IFunction function)
        {
            var gr = Gradient(function);
            double sum = 0;
            foreach(var g in gr) sum += g;
            return sum;
        }

    }
    class L2 : IDifferentiableFunctional, ILeastSquaresFunctional
    {
        public List<Vector> points;
        public IVector Gradient(IFunction function)
        {
            var l1 = new L1() { points = points };
            var res = l1.Gradient(function);
            return res;
        }

        public IMatrix Jacobian(IFunction function)
        {
            var J = new Matrix();
            var fun1 = new List<double>();
            var ders = Gradient(function);
            for(int i = 0; i < ders.Count; i++) { fun1.Add(ders[i]); }
            J.Add(fun1);
            return J;
        }

        public IVector Residual(IFunction function)
        {
            throw new NotImplementedException();
        }

        public double Value(IFunction function)
        {
            var gr = Gradient(function);
            double sum = 0;
            foreach (var g in gr) sum += g*g;
            return Math.Sqrt(sum);
        }
    }
    class Linf : IFunctional
    {
        public List<Vector> points;
        public double Value(IFunction function)
        {
            double val = function.Value(points[0]);
            double currval;
            foreach(var p in points)
            {
                currval = Math.Abs(function.Value(p));
                if (currval > val) val = currval;
            }
            return val;
        }
    }
    class Integral : IFunctional
    {
        public List<Vector> points;  // x =0, y = 1
        public double Value(IFunction function)
        {
            if(points.Count < 2) return 0;
            double sum = 0;
            for(int i = 1 ; i < points.Count; i++)
            {
                var p1 = new Vector() { points[i - 1][0], points[i - 1][1] };
                var p2 = new Vector() { points[i][0], points[i][1] };
                double x1 = points[i - 1][0];
                double x2 = points[i][0];
                double y1 = function.Value(p1);
                double y2 = function.Value(p2);
                // halfsumm
                double h = halfsum(y1, y2);
                sum += (x2-x1)*h;
            }
            return sum;
        }
        double halfsum(double l, double r) 
        { 
        if(l < 0 && r < 0)
            {
                l *= -1;
                r *= -1;
            }
        if(l > 0 && r < 0)
            {
                r *= -1;
                l += r;
            }
        if(l < 0 && r > 0)
            {
                l *= -1;
                r += l;
            }
            return (l + r) / 2;
        }
    }
}
