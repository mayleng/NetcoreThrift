using Common;
using System;
using Thrift.Server;
using Thrift.Transport;

namespace ThriftServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TServerSocket serverTransport = new TServerSocket(7911, 0, false);
            ThriftCase.Processor processor = new ThriftCase.Processor(new BusinessImpl());
            TServer server = new TSimpleServer(processor, serverTransport);
            Console.WriteLine("Starting server on port 7911 ...");
            server.Serve();
        }
    }
}
