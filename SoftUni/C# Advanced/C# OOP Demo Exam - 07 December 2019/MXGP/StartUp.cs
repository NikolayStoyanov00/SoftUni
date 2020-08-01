using System;

namespace MXGP
{
    using Models.Motorcycles;
    using MXGP.Core.Contracts;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            //TODO Add IEngine
            IEngine engine = new Engine();
            
        }
    }
}
