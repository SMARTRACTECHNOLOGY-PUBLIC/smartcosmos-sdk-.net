using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.SmartCosmos.TestSuite
{
    public enum TestCaseType
    {
        Functional = 1,
        Performance = 2
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TestSuiteAttribute : System.Attribute
    {
        public readonly TestCaseType testCaseType;

        public TestSuiteAttribute(TestCaseType aTestCaseType = TestCaseType.Functional)  
        {
            this.testCaseType = aTestCaseType;
        }
    }
}
