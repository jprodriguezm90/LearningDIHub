using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDIHub.Domain.Circular
{
    public sealed class FirstClassFunc
    {
        private readonly ThirdClassFunc thirdClass;

        public FirstClassFunc(ThirdClassFunc thirdClass)
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

    public sealed class SecondClassFunc(Func<FirstClassFunc> firstClass)
    {
        public void DoIt()
        {
            firstClass().DoSomethingElse();
        }
    }

    public sealed class ThirdClassFunc(SecondClassFunc secondClass, Func<FirstClassFunc> firstClass)
    {
        public void DoIt()
        {
            firstClass().DoSomethingElse();
            secondClass.DoIt();
        }
    }
}
