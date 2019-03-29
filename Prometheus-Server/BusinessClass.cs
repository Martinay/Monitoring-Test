using System;
using Microsoft.AspNetCore.Connections;

namespace Prometheus_Server
{
    public class BusinessClass
    {

        public void BusinessMethod()
        {
            Console.WriteLine("businessMethod called");
            throw new InvalidCastException("This is the message");
        }
        public void BusinessMethod2()
        {
            Console.WriteLine("businessMethod called");
            throw new AddressInUseException("This is the message");
        }
    }
}
