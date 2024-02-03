using FluentValidation;
using TerraMours.Domains.LoginDomain.Contracts.Req;

namespace TerraMours.Domains.LoginDomain.Contracts.ReqValidators
{
    public class SysUserReqValidator : AbstractValidator<SysUserReq>
    {
        public SysUserReqValidator()
        {
            //todo Validator 还没起作用

            //邮箱的校验 现在还不需要

            RuleFor(x => x.UserAccount).NotNull().EmailAddress()
            .Must(v => v.EndsWith("@qq.com") || v.EndsWith("@163.com"))
            .WithMessage("只支持QQ和163邮箱");

            /* RuleFor(x => x.UserPassword)
            .NotEmpty()
            .WithMessage("密码不能为空");*/

            //对密码的校验
            RuleFor(x => x.UserPassword).NotNull().Length(6, 18)
               .WithMessage("密码长度必须介于6到18之间")
                .Equal(x => x.RepeatPassword).WithMessage("两次密码必须一致");

            //当是注册时候 Repeatpassword 不为空 判断确认的密码与第一次一样，也可以前端判断
            /* RuleFor(x => x.RepeatPassword)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.RepeatPassword))
            .Equal(x => x.UserPassword)
            .WithMessage("两次密码必须一致");*/





        }
    }
}
