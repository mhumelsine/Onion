using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TransactionRequiredAttribute : Attribute
    {
    }
}
