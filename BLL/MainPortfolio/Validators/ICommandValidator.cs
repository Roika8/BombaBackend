using System.Collections.Generic;

namespace BLL.MainPortfolio.Validators
{
    public interface ICommandValidator<T>
    {
        string ValidateCommand(T model);

    }
}
