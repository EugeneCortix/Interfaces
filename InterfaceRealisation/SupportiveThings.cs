using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceRealisation
{
    public class Vector : List<double>, IVector
    {

    }
    public class Matrix : List<List<double>>, IMatrix
    {
        IList<double> IList<IList<double>>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(IList<double> item)
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

        IEnumerator<IList<double>> IEnumerable<IList<double>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class Derivative : IDifferentiableFunctional
    {
        public List<Vector> points;
        public int n;
        public IVector Gradient(IFunction function)
        {
            var grad = new Vector();
            double fsum = 0;
            for (int i = 0; i < points.Count; i++)
                fsum += points[i][n - 1];
            for(int i = 0; i <  n; i++)
            {
                grad.Add(fsum);
                //grad.Add(0);
            }
            string type = define(function.GetType().ToString());
            switch (type)
            {
                case "LineFunction":
                    for (int i = 0; i < n - 1; i++)
                    {
                        /*  grad[i] += function.Value(points[i]);
                          grad[i] *= 2 * points[i][i];*/
                         for(int j = 0; j < points.Count; j++)
                        {
                            grad[i] -= function.Value(points[j]);
                            grad[i] *= -2 * points[j][i];
                        }
                    }
                    for (int j = 0; j < points.Count; j++)
                    {
                        grad[n-1] -= function.Value(points[j]);
                        grad[n-1] *= -2;
                    }
                    break;
                    
                case "Polynomial":
                    foreach (var p in points)
                    {
                        
                    }
                    break;
            }
            return grad;
        }

        public double Value(IFunction function)
        {
            throw new NotImplementedException();
        }
        string define(string s)
        {
            if (s.IndexOf("LineFunction") > -1)
                return "LineFunction";
            if (s.IndexOf("Polynomial") > -1)
                return "Polynomial";
            return s;
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
