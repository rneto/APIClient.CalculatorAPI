using System;
using System.Collections.Generic;
using System.Text;

namespace APIClient.CalculatorAPI.v2.Models
{
    public abstract class OperationRequest
    {
        public int A { get; private set; }
        public int B { get; private set; }

        public OperationRequest(int a, int b)
        {
            A = a;
            B = b;
        }

        public abstract string Parse();
    }
}
