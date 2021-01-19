using System;
using System.Collections.Generic;
using System.Text;

namespace APIClient.CalculatorAPI.v2.Models
{
    public class OperationResult
    {
        public int Value { get; private set; }
        public OperationResult(int value)
        {
            Value = value;
        }
    }
}
