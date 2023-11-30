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
        double precision = 0.01;
        double rate = 0.0001;
        double difference = 1;
        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters,
            IVector minimumParameters = null, IVector maximumParameters = null)
        {
            var param = new Vector();
          //  var prevparam = new Vector();
            foreach (var p in initialParameters) param.Add(p);
           // foreach (var p in initialParameters) prevparam.Add(p);
            var fun = function.Bind(param);
            var prev = objective.Value(fun);
            int it = 0;
            while (difference > precision && it < 10000)
            {
                var grad = new LineDerivative().Bind(param);
                var gr = grad.Gradient(minimumParameters);
                for (int  i = 0; i < param.Count; i++)
                {
                    param[i] -= gr[i]*rate;
                }
                var f = objective.Value(function.Bind(param));
                difference = Math.Abs(Math.Abs(prev) - Math.Abs(f));
                prev = f;
                it++;
            }
            return param;
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


 