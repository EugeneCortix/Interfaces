using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceRealisation
{
    interface IOptimizator

    {
            IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters,
                             IVector minimumParameters = default, IVector maximumParameters = default);
            
    }
    interface IParametricFunction
    {
        IFunction Bind(IVector parameters);
    }
    interface IDifferentiableFunction : IFunction
    {
        // По параметрам исходной IParametricFunction
        IVector Gradient(IVector point);
    }
    interface IFunctional
    {
        double Value(IFunction function);
    }
    interface IDifferentiableFunctional : IFunctional
    {
        IVector Gradient(IFunction function);
    }
    interface IMatrix : IList<IList<double>>
    {

    }
    interface ILeastSquaresFunctional : IFunctional
    {
        IVector Residual(IFunction function);
        IMatrix Jacobian(IFunction function);
    }
    interface IFunction
    {
        double Value(IVector point);
    }
    interface IVector : IList<double> { }
}
