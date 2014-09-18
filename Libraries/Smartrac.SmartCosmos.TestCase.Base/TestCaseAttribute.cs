using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.TestCase.Base
{
    public enum TestCaseType
    {
        Functional = 1,
        Performance = 2
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TestCaseAttribute : System.Attribute
    {
        public readonly TestCaseType testCaseType;

        public TestCaseAttribute(TestCaseType aTestCaseType = TestCaseType.Functional)  
        {
            this.testCaseType = aTestCaseType;
        }
    }
}
