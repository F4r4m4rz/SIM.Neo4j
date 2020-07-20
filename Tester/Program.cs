using SIM.Neo4j.Connection;
using System;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Neo4jClient client = new Neo4jClient("bolt://localhost:7687", "neo4j", "1234"))
            {
                var command = client.NewCommand();
                command.Match().Pattern().Node("a", typeof(Person)).All()
                                         //.Relation("b").All()
                                         //.Node("c").All()
                                         //.Relation("d").All()
                                         //.Node("e").All()
                                         .Close()
                               //.Pattern().Node("f").All()
                               //          .Relation("g").All()
                               //          .Node("h").All()
                               //          .Close()
                               .Close()
                        .Return("a");

                //var cmd = command.AsPainCypher();
                //Console.WriteLine(cmd);
                var test = client.ExecuteQuery(command);
                Console.Read();
            }
        }
    }
}
