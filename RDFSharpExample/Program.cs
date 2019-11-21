using RDFSharp.Model;
using RDFSharp.Query;
using RDFSharp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// unit test for dotNetRDF 
// https://github.com/BME-MIT-IET/Intel-iet-2019
// https://github.com/BME-MIT-IET/chillisbab-iet-2019
// https://github.com/BME-MIT-IET/csak3anvagyunk-iet-2019/tree/master/dotnetrdf


// unit test  for  rdfSharp
// https://github.com/BME-MIT-IET/Integracijos-iet-2019
// https://github.com/BME-MIT-IET/IETeam-iet-2019
// https://github.com/BME-MIT-IET/The-Rabbit-Slayers-iet-2019
// https://github.com/BME-MIT-IET/Integracijos-iet-2019

namespace RDFSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {

            Tutorial2 tutorial2 = new Tutorial2();
            tutorial2.Run();

            Tutorial1 tutorial1 = new Tutorial1();
            tutorial1.Run();

            Unesco unesco = new Unesco();
            unesco.Load();
        }

        public static void Test()
        { 
            RDFFederation federation = new RDFFederation();

            StoreManager manager = new StoreManager();

            federation.AddStore(manager.Store);

            int storeCount = federation.StoresCount;

            // ITERATE STORES OF A FEDERATION
            foreach (RDFStore store in federation)
            {
                Console.WriteLine("Store: " + store);
                Console.WriteLine(" Type: " + store.StoreType);
            }

            /*
             * Create this graph named "http://www.example.org/graph1"
             * 
<http://www.example.org/index.html> <http://purl.org/dc/elements/1.1/creator> <http://www.example.org/staffid/85740> .
<http://www.example.org/index.html> <http://www.example.org/terms/creation-date> "August 16, 1999" .
<http://www.example.org/index.html> <http://purl.org/dc/elements/1.1/language> "en" .             
             */

            string contextUri = "http://www.example.org/graph1";
            string subjectUri = "http://www.example.org/index.html";

            RDFContext context = new RDFContext(contextUri);
            RDFResource subj = new RDFResource(subjectUri);

            RDFResource pred1 = new RDFResource("http://purl.org/dc/elements/1.1/creator");
            RDFResource obj = new RDFResource("http://www.example.org/staffid/85740");

            RDFQuadruple quadruple1 = new RDFQuadruple(context, subj, pred1, obj);
            manager.Store.AddQuadruple(quadruple1);


            RDFResource pred2 = new RDFResource("http://www.example.org/terms/creation-date");
            RDFLiteral lit = new RDFPlainLiteral("August 16, 1999");

            RDFQuadruple quadruple2 = new RDFQuadruple(context, subj, pred2, lit);
            manager.Store.AddQuadruple(quadruple2);

            manager.AddQuadrupleLiteral(contextUri, subjectUri, "http://purl.org/dc/elements/1.1/language", "en");

            // It is recommended that N-Quads files have the extension ".nq" (all lowercase) on all platforms.
            manager.Store.ToFile(RDFStoreEnums.RDFFormats.NQuads, @"C:\TEMP\index.nq");

            manager.Store.ToFile(RDFStoreEnums.RDFFormats.TriX, @"C:\TEMP\index.trix");

            List<RDFGraph> graphs = manager.Store.ExtractGraphs();
            foreach(RDFGraph graph in graphs)
            {
                graph.ToFile(RDFModelEnums.RDFFormats.NTriples, @"C:\TEMP\graph1.nt");
                graph.ToFile(RDFModelEnums.RDFFormats.RdfXml, @"C:\TEMP\graph1.rdf");
                graph.ToFile(RDFModelEnums.RDFFormats.Turtle, @"C:\TEMP\graph1.ttl");
                graph.ToFile(RDFModelEnums.RDFFormats.TriX, @"C:\TEMP\graph1.trix");
            }

            /*

            // CREATE VARIABLE
            RDFVariable x = new RDFVariable("x"); // ?X
            RDFVariable y = new RDFVariable("y"); // ?Y
            RDFVariable n = new RDFVariable("n"); // ?N (don’t show in SELECT results)

            // CREATE PATTERNS
            RDFResource dogOf = new RDFResource(RDFVocabulary.DC.BASE_URI + "dogOf");
            RDFPattern y_dogOf_x = new RDFPattern(y, dogOf, x); // TRIPLE PATTERN
            RDFPattern n_y_dogOf_x = new RDFPattern(n, y, dogOf, x); // QUADRUPLE PATTERN

            // CREATE EMPTY PATTERN GROUP
            RDFPatternGroup pg1 = new RDFPatternGroup("PG1");

            // ADD PATTERNS TO PATTERN GROUP
            pg1.AddPattern(y_dogOf_x);

            // CREATE PATTERN GROUP FROM A LIST OF PATTERNS
            List<RDFPattern> patterns = new List<RDFPattern>() { y_dogOf_x };
            RDFPatternGroup pg2 = new RDFPatternGroup("PG2", patterns);

            // ADD FILTERS TO PATTERN GROUP
            pg1.AddFilter(new RDFSameTermFilter(x, donaldduck));
            pg1.AddFilter(new RDFLangMatchesFilter(n, "it-IT"));
            pg1.AddFilter(new RDFComparisonFilter(
            RDFQueryEnums.RDFComparisonFlavors.LessThan, y, new RDFPlainLiteral("25"))); // ?Y < "25"

            // CREATE SELECT QUERY
            RDFSelectQuery selectQuery = new RDFSelectQuery();


            // ADD PATTERN GROUPS TO QUERY
            selectQuery.AddPatternGroup(pg1);

            //y_dogOf_x.Optional();
            //y_dogOf_x.UnionWithNext();

            // ADD MODIFIERS TO QUERY
            selectQuery.AddModifier(new RDFDistinctModifier());
            selectQuery.AddModifier(new RDFOrderByModifier(x, RDFQueryEnums.RDFOrderByFlavors.DESC));
            selectQuery.AddModifier(new RDFOrderByModifier(n, RDFQueryEnums.RDFOrderByFlavors.ASC));
            selectQuery.AddModifier(new RDFLimitModifier(100));
            selectQuery.AddModifier(new RDFOffsetModifier(25));
            */

            /*
            // APPLY SELECT QUERY TO GRAPH
            RDFSelectQueryResult selectQueryResult1 = selectQuery.ApplyToGraph(graph);
            // APPLY SELECT QUERY TO STORE
            RDFSelectQueryResult selectQueryResult2 = selectQuery.ApplyToStore(store);
            // APPLY SELECT QUERY TO FEDERATION
            RDFSelectQueryResult selectQueryResult3 = selectQuery.ApplyToFederation(federation);


            // CREATE ASK QUERY
            RDFAskQuery askQuery = new RDFAskQuery();

            // APPLY ASK QUERY TO GRAPH
            RDFAskQueryResult askQueryResult1 = askQuery.ApplyToGraph(graph);
            // APPLY ASK QUERY TO STORE
            RDFAskQueryResult askQueryResult2 = askQuery.ApplyToStore(store);
            // APPLY ASK QUERY TO FEDERATION
            RDFAskQueryResult askQueryResult3 = askQuery.ApplyToFederation(federation);


            // CREATE CONSTRUCT QUERY
            RDFConstructQuery constructQuery = new RDFConstructQuery();

            // APPLY CONSTRUCT QUERY TO GRAPH
            RDFConstructQueryResult constructResult1 = constructQuery.ApplyToGraph(graph);
            // APPLY CONSTRUCT QUERY TO STORE
            RDFConstructQueryResult constructResult2 = constructQuery.ApplyToStore(store);
            // APPLY CONSTRUCT QUERY TO FEDERATION
            RDFConstructQueryResult constructResult3 = constructQuery.ApplyToFederation(federation);

            // CREATE DESCRIBE QUERY
            RDFDescribeQuery describeQuery = new RDFDescribeQuery();

            // APPLY DESCRIBE QUERY TO GRAPH
            RDFDescribeQueryResult describeQueryResult1 = describeQuery.ApplyToGraph(graph);
            // APPLY DESCRIBE QUERY TO STORE
            RDFDescribeQueryResult describeQueryResult2 = describeQuery.ApplyToStore(store);
            // APPLY DESCRIBE QUERY TO FEDERATION
            RDFDescribeQueryResult describeQueryResult3 = describeQuery.ApplyToFederation(federation);
            */
        }
    }
}
