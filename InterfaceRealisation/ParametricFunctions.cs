using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceRealisation
{
    class LineFunction : IParametricFunction
    {
        public List<(double x, double y)> points;
        class InternalLineFunction : IFunction
        {
            public double a, b;
            public double Value(IVector point) => a * point[0] + b;  // y = ax + b
        }
        public IFunction Bind(IVector parameters) => new InternalLineFunction() { a = parameters[0], b = parameters[1] };

    }

    class Polynomial : IParametricFunction
    {
        class PolynomialFunction : IFunction
        {
            public double a, b;
            public double Value(IVector point)
            { 
                return a * point[0]* point[0] + b * point[0];  // y = ax^2 + bx
            }
        }
        public IFunction Bind(IVector parameters)
        {
            return new PolynomialFunction() { a = parameters[0],b = parameters[1] };
        }
    }
    

}
