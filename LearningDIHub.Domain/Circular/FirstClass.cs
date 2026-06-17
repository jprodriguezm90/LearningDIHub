using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace LearningDIHub.Domain.Circular
{

    public sealed class FirstClass
    {
        private readonly ThirdClass thirdClass;

        public FirstClass(ThirdClass thirdClass)
        {
            this.thirdClass = thirdClass;
        }

        public void DoIt()
        {
            thirdClass.DoIt();
        }

        public void DoSomethingElse()
        {

        }
    }

    public sealed class SecondClass(Lazy<FirstClass> firstClass)
    {
        public void DoIt()
        {
            firstClass.Value.DoSomethingElse();
        }
    }

    public sealed class ThirdClass(SecondClass secondClass, Lazy<FirstClass> firstClass)
    {
        public void DoIt()
        {
            firstClass.Value.DoSomethingElse();
            secondClass.DoIt();
        }
    }
}
