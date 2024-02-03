using FluentValidation;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;

namespace TerraMours_Gpt.Domains.PayDomain.Contracts.ReqValidators
{
    public class CategoryReqValidator : AbstractValidator<CategoryReq>
    {
        public CategoryReqValidator()
        {
            //todo Validator 还没起作用

            //邮箱的校验 现在还不需要

            /* RuleFor(x => x.user).NotNull().EmailAddress()
             .Must(v => v.EndsWith("@qq.com") || v.EndsWith("@163.com"))
             .WithMessage("只支持QQ和163邮箱");*/

        }
    }
}
