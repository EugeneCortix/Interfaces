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
        public List<(double x, double y)> points;
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
            
            var der = new Derivative() { points = points, functiontype = function };
            
            while (difference > precision)
            {
                var diff = der.Gradient(param);
                for (int i = 0; i < param.Count; i++)
                {
                    param[i] -= rate*diff[i];
                }
                /*  var fun = function.Bind(prevparam);
                  difference = Math.Abs(objective.Value(fun));
                  fun = function.Bind(param);
                  difference -= Math.Abs(objective.Value(fun));
                  difference = Math.Abs(difference);*/
                double adiff = Math.Abs(Math.Abs(prevparam[0]) - Math.Abs(param[0]));
                double bdiff = Math.Abs(Math.Abs(prevparam[1]) - Math.Abs(param[1]));
              if (adiff > bdiff)
                    difference = adiff;
              else difference = bdiff;
               // for(int i = 0; i < param.Count;i++) { difference += Math.Abs(Math.Abs(prevparam[i]) - Math.Abs(param[i])); }
                prevparam = param;

            }
            return param;
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


 