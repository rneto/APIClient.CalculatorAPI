using System;
using System.Collections.Generic;
using System.Text;

namespace APIClient.CalculatorAPI.v2.Models
{
    public class Add : OperationRequest
    {
        public Add(int a, int b) : base(a, b)
        {
        }

        public override string Parse()
        {
            return $"{base.A}+{base.B}";
        }
    }
}
