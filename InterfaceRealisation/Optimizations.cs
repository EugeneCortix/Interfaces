using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Monte-Carlo, Gradient, ..
namespace InterfaceRealisation
{
    class GradientDescent : IOptimizator
    {
        public List<Vector> points;
        double precision = 0.001;
        double rate = 0.0001;
        double difference = 1;
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters,
            IVector minimumParameters = null, IVector maximumParameters = null)
        {
            var prevparam = new Vector();
            var param = new Vector();
            foreach (var p in initialParameters) param.Add(p);
            foreach (var p in initialParameters) prevparam.Add(p);

            var obj = CreateFunctional(points, param.Count, objective);

            while (difference > precision)
            {
                var fun = function.Bind(param);
                var diff = obj.Gradient(fun);
                for (int i = 0; i < param.Count; i++)
                {
                    param[i] -= rate*diff[i];
                }
                difference = 0;
                for(int i = 0; i < param.Count;i++) { difference += Math.Abs(Math.Abs(prevparam[i]) - Math.Abs(param[i])); }

                prevparam = param;

            }
            return param;
        }

        IDifferentiableFunctional CreateFunctional(List<Vector> points, int n, IFunctional objective)
        {
            string type = define(objective.GetType().ToString());
            switch (type)
            {
                case "L1":
                    return new L1 { n = n, points = points };
                    break;
                case "L2":
                    return new L2 { n = n, points = points };
                    break;
            }
            return null;
        }
        string define(string s)
        {
            if (s.IndexOf("L1") > -1)
                return "L1";
            if (s.IndexOf("L2") > -1)
                return "L2";
            if (s.IndexOf("Linf") > -1)
                return "Linf";
            if (s.IndexOf("Integal") > -1)
                return "Inegral";
            return s;
        }
    }

    class GaussNewton : IOptimizator
    {
        // Do with respect to a & b, like I did for Gradient method
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            throw new NotImplementedException();
        }
    }

    // Пример
    class MinimizerMonteCarlo : IOptimizator
    {
        public int MaxIter = 100000;
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            var param = new Vector();
            var minparam = new Vector();
            foreach (var p in initialParameters) param.Add(p);
            foreach (var p in initialParameters) minparam.Add(p);
            var fun = function.Bind(param);
            var currentmin = objective.Value(fun);
            var rand = new Random(0);
            for (int i = 0; i < MaxIter; i++)
            {
                for (int j = 0; j < param.Count; j++) param[j] = rand.NextDouble();
                var f = objective.Value(function.Bind(param));
                if (f < currentmin)
                {
                    currentmin = f;
                    for (int j = 0; j < param.Count; j++) minparam[j] = param[j];
                }
            }
            return minparam;
        }
    }
}


 