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
        public List<(double x, double y)> points;
        public IVector Gradient(IFunction function)
        {
            Vector res = new Vector();
            foreach(var p in points) 
            {
                Vector pv = new Vector();
                pv.Add(p.x);
                double gr = Math.Abs(p.y - function.Value(pv));
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
    class L2 : IDifferentiableFunctional
    {
        public List<(double x, double y)> points;
        public IVector Gradient(IFunction function)
        {
            var l1 = new L1() { points = points };
            var res = l1.Gradient(function);
            return res;
        }

        public double Value(IFunction function)
        {
            var gr = Gradient(function);
            double sum = 0;
            foreach (var g in gr) sum += g*g;
            return Math.Sqrt(sum);
        }
    }
    class Linf : ILeastSquaresFunctional
    {
        public IMatrix Jacobian(IFunction function)
        {
            throw new NotImplementedException();
        }

        public IVector Residual(IFunction function)
        {
            throw new NotImplementedException();
        }

        public double Value(IFunction function)
        {
            throw new NotImplementedException();
        }
    }
}
