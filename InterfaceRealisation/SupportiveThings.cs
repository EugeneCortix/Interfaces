using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceRealisation
{
    public class Vector : List<double>, IVector
    {
    }

    class LineDerivative : IDifferentiableFunction
    {
        public List<(double x, double y)> points;
        public IVector Gradient(IVector point) // point[0] = a, point[1] = b;
        {
            var grad = new Vector();
            double diffa = 0, diffb = 0;
            foreach (var p in points)
            {
                diffa += -2 * (p.y - point[0] * p.x - point[1]) * p.x;
                diffb += -2 * (p.y - point[0] * p.x - point[1]);
            }
            grad.Add(diffa);
            grad.Add(diffb);
            return grad;
        }

        public double Value(IVector point)
        {
            throw new NotImplementedException();
        }
    }
}


/*
 class LineFunction : IParametricFunction
    {
        class InternalLineFunction : IFunction
        {
            public double a, b;
            public double Value(IVector point) => a * point[0] + b;  // y = ax + b
        }
        public IFunction Bind(IVector parameters) => new InternalLineFunction() { a = parameters[0], b = parameters[1] };
    }
 */
