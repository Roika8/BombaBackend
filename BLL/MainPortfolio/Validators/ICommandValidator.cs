using DATA.Enums;
using System.Collections.Generic;

namespace BLL.MainPortfolio.Validators
{
    public interface ICommandValidator<T>
    {
        List<ErrorMessage> ValidateCommand(T model);
    }
}
