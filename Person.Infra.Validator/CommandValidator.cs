using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.Infra.Validator
{
    public abstract class CommandValidator<T> : AbstractValidator<T> where T : Command
    {
        public CommandValidator()
        {
            CreateRules();
        }

        protected abstract void CreateRules();

    }
}
