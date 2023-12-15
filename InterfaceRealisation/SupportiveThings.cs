using System;
using System.Collections;
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
    public class Matrix : IMatrix
    {
        public IList<double> this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(IList<double> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IList<double> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IList<double>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IList<double>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(IList<double> item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IList<double> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IList<double> item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class Derivative : IDifferentiableFunction
    {
        public List<(double x, double y)> points;
        public IParametricFunction functiontype;
        public IVector Gradient(IVector point) // point[0] = a, point[1] = b;
        {
            var grad = new Vector();
            double diffa = 0, diffb = 0;
            int type = Type(functiontype);
            switch (type)
            {
                case 0:
                    foreach (var p in points)
                    {
                        diffa += -2 * (p.y - point[0] * p.x - point[1]) * p.x;
                        diffb += -2 * (p.y - point[0] * p.x - point[1]);
                    }
                    break;
               case 1:
                    foreach (var p in points)
                    {
                        diffa += 0;
                        diffb += 0;
                    }
                    break;
            }
            grad.Add(diffa);
            grad.Add(diffb);
            return grad;
        }
        public double Value(IVector point)
        {
            throw new NotImplementedException();
        }
        int Type(IParametricFunction function)
        {
            int res = -3;
            switch(function.GetType().ToString())
            {
                case "InterfaceRealisation.LineFunction":
                    res = 0; 
                    break;
                case "InterfaceRealisation.Polynomial":
                    res = 1; 
                    break;
                    
            }
            return res;
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
