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
        public double a, b;
        public IVector Gradient(IVector point)
        {
            //Попробуем по a & b
            var grad = new Vector();
            //double val = Value(point);
            grad.Add(point[0]);  // a derivative
            grad.Add(1); // b
            return grad;
        }

        public double Value(IVector point)
        {
            double delta = 0.001;
            double left = (point[0]-delta)*a + b;
            double right = (point[0]+delta)*a + b;
            return (right - left)/(2*delta);           
        }
        public IDifferentiableFunction Bind(IVector parameters) => new LineDerivative() { a = parameters[0], b = parameters[1] };
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
