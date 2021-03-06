﻿using Api.Example;
using System;
using System.Threading;
using Zbus.Mq;
using Zbus.Rpc;

namespace Zbus.Examples
{
    class RpcProcessorExample
    { 
        static void Main(string[] args)
        { 
            RpcProcessor p = new RpcProcessor();
            p.AddModule<MyService>(); //Simple?

            Broker broker = new Broker("localhost:15555");
            //Broker broker = new Broker("localhost:15555;localhost15556"); //Capable of HA failover, test it! 

            Consumer c = new Consumer(broker, "MyRpc");
            c.TopicMask = Protocol.MASK_MEMORY | Protocol.MASK_RPC;
            c.ConnectionCount = 4; 
            c.MessageHandler = p.MessageHandler; //Set processor as message handler
            c.Start();
            Console.WriteLine("MyRpc Service Ready");
            //Console.ReadKey();
        }
    }
}
