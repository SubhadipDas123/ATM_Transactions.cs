using System;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Function
{
    public class Function
    {
        public dynamic FunctionHandler(dynamic eventTrigger)
        {
            Console.WriteLine(eventTrigger);

            return new {};
        }
    }
}
