using FluentValidation;
using TerraMours.Domains.LoginDomain.Contracts.Req;

namespace TerraMours.Domains.LoginDomain.Contracts.ReqValidators
{
    public class SysLoginUserReqValidator : AbstractValidator<SysLoginUserReq>
    {
        public SysLoginUserReqValidator()
        {
            //邮箱的校验 现在还不需要

            RuleFor(x => x.UserAccount).NotNull().EmailAddress()
            .Must(v => v.EndsWith("@qq.com") || v.EndsWith("@163.com"))
            .WithMessage("只支持QQ和163邮箱");

        }
    }
}
