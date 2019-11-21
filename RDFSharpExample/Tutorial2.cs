using RDFSharp.Model;
using RDFSharp.Query;
using RDFSharp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFSharpExample
{
    public class Tutorial2
    {
        public void Run()
        {
            // WORKING WITH RDF MODELS
            WorkingWithRdfModels();
        }

        private void WorkingWithRdfModels()
        {
            // CREATE RESOURCE
            var donaldduck = new RDFResource("http://www.waltdisney.com/donald_duck");
            
            // CREATE BLANK RESOURCE
            var disney_group = new RDFResource();

            // CREATE PLAIN LITERAL
            // "Donald Duck"
            var donaldduck_name = new RDFPlainLiteral("Donald Duck");
            // CREATE PLAIN LITERAL WITH LANGUAGE TAG
            // "Donald Duck"@en-US
            var donaldduck_name_enusLiteral = new RDFPlainLiteral("Donald Duck", "en-US");
            // CREATE TYPED LITERAL
            // "85"^^xsd:integer
            var mickeymouse_age = new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER);


            // CREATE TRIPLES
            // "Mickey Mouse is 85 years old"
            RDFTriple mickeymouse_is85yr
            = new RDFTriple(
            new RDFResource("http://www.waltdisney.com/mickey_mouse"),
            new RDFResource("http://xmlns.com/foaf/0.1/age"),
            new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));
            
            // "Donald Duck has english (US) name "Donald Duck""
            RDFTriple donaldduck_name_enus = new RDFTriple(
                new RDFResource("http://www.waltdisney.com/donald_duck"),
                new RDFResource("http://xmlns.com/foaf/0.1/name"),
                new RDFPlainLiteral("Donald Duck", "en-US"));


            // CREATE EMPTY GRAPH
            var another_graph = new RDFGraph();
            var waltdisney_filled = new RDFGraph();

            // CREATE GRAPH FROM A LIST OF TRIPLES
            var triples = new List<RDFTriple>{ mickeymouse_is85yr, donaldduck_name_enus };
            var waltdisney = new RDFGraph(triples);

            // SET CONTEXT OF A GRAPH
            waltdisney.SetContext(new Uri("http://waltdisney.com/"));

            // GET A DATATABLE FROM A GRAPH
            var waltdisney_table = waltdisney.ToDataTable();
            // GET A GRAPH FROM A DATATABLE
            var waltdisney_newgraph = RDFGraph.FromDataTable(waltdisney_table);

            // ITERATE TRIPLES OF A GRAPH WITH FOREACH
            foreach (var t in waltdisney)
            {
                Console.WriteLine("Triple: " + t);
                Console.WriteLine(" Subject: " + t.Subject);
                Console.WriteLine(" Predicate: " + t.Predicate);
                Console.WriteLine(" Object: " + t.Object);
            }

            // ITERATE TRIPLES OF A GRAPH WITH ENUMERATOR
            var triplesEnum = waltdisney.TriplesEnumerator;
            while (triplesEnum.MoveNext())
            {
                Console.WriteLine("Triple: " + triplesEnum.Current);
                Console.WriteLine(" Subject: " + triplesEnum.Current.Subject);
                Console.WriteLine(" Predicate: " + triplesEnum.Current.Predicate);
                Console.WriteLine(" Object: " + triplesEnum.Current.Object);
            }

            // GET COUNT OF TRIPLES CONTAINED IN A GRAPH
            var triplesCount = waltdisney.TriplesCount;

            // MULTIPLE SELECTIONS
            var multiple_selections_graph =
            waltdisney.SelectTriplesBySubject(new RDFResource("http://www.waltdisney.com/donald_duck"))
            .SelectTriplesByPredicate(new RDFResource("http://xmlns.com/foaf/0.1/name"));
            
            // SET OPERATIONS
            var set_operations_graph = waltdisney.IntersectWith(waltdisney_filled).UnionWith(another_graph);



            /*
            var ntriplesFormat = RDFModelEnums.RDFFormats.NTriples;
            // READ N-TRIPLES FILE
            var graph = RDFGraph.FromFile(ntriplesFormat, "C:\\file.nt");
            // READ N-TRIPLES STREAM
            var graph = RDFGraph.FromStream(ntriplesFormat, inStream);
            // WRITE N-TRIPLES FILE
            graph.ToFile(ntriplesFormat, "C:\\newfile.nt");
            // WRITE N-TRIPLES STREAM
            graph.ToStream(ntriplesFormat, outStream);
            */

            /*
             var turtleFormat = RDFModelEnums.RDFFormats.Turtle;
// READ TURTLE FILE
var graph = RDFGraph.FromFile(turtleFormat, "C:\\file.ttl");
// READ TURTLE STREAM
var graph = RDFGraph.FromStream(turtleFormat, inStream);
// WRITE TURTLE FILE
graph.ToFile(turtleFormat, "C:\\newfile.ttl");
// WRITE TURTLE STREAM
graph.ToStream(turtleFormat, outStream);
            */

            /*
var xmlFormat = RDFModelEnums.RDFFormats.RdfXml;
// READ RDF/XML FILE
var graph = RDFGraph.FromFile(xmlFormat, "C:\\file.rdf");
// READ RDF/XML STREAM
var graph = RDFGraph.FromStream(xmlFormat, inStream);
// WRITE RDF/XML FILE
graph.ToFile(xmlFormat, "C:\\newfile.rdf");
// WRITE RDF/XML STREAM
graph.ToStream(xmlFormat, outStream);
             */

            // CREATE NAMESPACE
            var waltdisney_ns = new RDFNamespace("wd", "http://www.waltdisney.com/");
            
            // USE NAMESPACE IN RESOURCE CREATION
            var duckburg = new RDFResource(waltdisney_ns + "duckburg");
            var mouseton = new RDFResource(waltdisney_ns + "mouseton");


            RDFNamespaceRegister.AddNamespace(waltdisney_ns);

            // Retrieves a namespace by seeking presence of its prefix (null if not found). Supports prefix.cc
            var ns1 = RDFNamespaceRegister.GetByPrefix("dbpedia", false); //local search
            var ns2 = RDFNamespaceRegister.GetByPrefix("dbpedia", true); //search prefix.cc service if no result

            // GET DEFAULT NAMESPACE
            var nSpace = RDFNamespaceRegister.DefaultNamespace;
            
            // SET DEFAULT NAMESPACE
            RDFNamespaceRegister.SetDefaultNamespace(waltdisney_ns); //new graphs will default to this context

            // ITERATE NAMESPACES OF REGISTER WITH FOREACH
            foreach (var ns in RDFNamespaceRegister.Instance)
            {
                Console.WriteLine("Prefix: " + ns.NamespacePrefix);
                Console.WriteLine("Namespace: " + ns.NamespaceUri);
            }

            // ITERATE NAMESPACES OF REGISTER WITH ENUMERATOR
            var nspacesEnum = RDFNamespaceRegister.NamespacesEnumerator;
            while (nspacesEnum.MoveNext())
            {
                Console.WriteLine("Prefix: " + nspacesEnum.Current.NamespacePrefix);
                Console.WriteLine("Namespace: " + nspacesEnum.Current.NamespaceUri);
            }


            // CREATE TRIPLES WITH VOCABULARY FACILITIES
            // "Goofy Goof is 82 years old"
            RDFTriple goofygoof_is82yr = new RDFTriple(
                new RDFResource(new Uri("http://www.waltdisney.com/goofy_goof").ToString()),
                RDFVocabulary.FOAF.AGE,
                new RDFPlainLiteral("82")
            );
            
            // "Donald Duck knows Goofy Goof"
            RDFTriple donaldduck_knows_goofygoof = new RDFTriple(
                new RDFResource(new Uri("http://www.waltdisney.com/donald_duck").ToString()),
                RDFVocabulary.FOAF.KNOWS,
                new RDFResource(new Uri("http://www.waltdisney.com/goofy_goof").ToString())
            );

            // CREATE TYPED LITERALS
            var myAge = new RDFTypedLiteral("34", RDFModelEnums.RDFDatatypes.XSD_INT);
            var myDate = new RDFTypedLiteral("2017-01-07", RDFModelEnums.RDFDatatypes.XSD_DATE);
            var myDateTime = new RDFTypedLiteral("2017-01-07T23:11:05", RDFModelEnums.RDFDatatypes.XSD_DATETIME);
            var myXml = new RDFTypedLiteral("<book>title</book>", RDFModelEnums.RDFDatatypes.RDF_XMLLITERAL);
            var myLiteral = new RDFTypedLiteral("generic literal", RDFModelEnums.RDFDatatypes.RDFS_LITERAL);

            /*
             * The given list of items may be incomplete.
             * A container is semantically opened to the possibility of having further elements
             * 
             * Alt: unordered semantic, duplicates not allowed;
             * Bag: unordered semantic, duplicates allowed;
             * Seq: ordered semantic, duplicates allowed;
             */

            // CREATE CONTAINER AND ADD ITEMS
            RDFContainer beatles_cont = new RDFContainer(RDFModelEnums.RDFContainerTypes.Bag, RDFModelEnums.RDFItemTypes.Resource);
            beatles_cont.AddItem(new RDFResource("http://beatles.com/ringo_starr"));
            beatles_cont.AddItem(new RDFResource("http://beatles.com/john_lennon"));
            beatles_cont.AddItem(new RDFResource("http://beatles.com/paul_mc_cartney"));
            beatles_cont.AddItem(new RDFResource("http://beatles.com/george_harrison"));

            /*
             * The given list of items may not be incomplete.
             * A collection is semantically closed to the possibility of having further elements
             */

            // CREATE COLLECTION AND ADD ITEMS
            RDFCollection beatles_coll = new RDFCollection(RDFModelEnums.RDFItemTypes.Resource);
            beatles_coll.AddItem(new RDFResource("http://beatles.com/ringo_starr"));
            beatles_coll.AddItem(new RDFResource("http://beatles.com/john_lennon"));
            beatles_coll.AddItem(new RDFResource("http://beatles.com/paul_mc_cartney"));
            beatles_coll.AddItem(new RDFResource("http://beatles.com/george_harrison"));


            // ADD CONTAINER/COLLECTION TO GRAPH
            waltdisney.AddContainer(beatles_cont);
            waltdisney.AddCollection(beatles_coll);



            // REIFY TRIPLE AND MERGE IT INTO A GRAPH
            RDFGraph reifGraph = goofygoof_is82yr.ReifyTriple();
            waltdisney = waltdisney.UnionWith(reifGraph);
            
            // ASSERT SOMETHING ABOUT REIFIED TRIPLE
            waltdisney.AddTriple(new RDFTriple(
                new RDFResource("http://www.wikipedia.com/"),
                new RDFResource("http://example.org/verb_state"),
                goofygoof_is82yr.ReificationSubject
            ));


            var existingGraph = new RDFGraph();


            // REIFY CONTAINER
            existingGraph.AddContainer(beatles_cont);

            existingGraph.AddTriple(new RDFTriple(
                new RDFResource("http://www.thebeatles.com/"),
                RDFVocabulary.FOAF.GROUP,
                beatles_cont.ReificationSubject
            ));

            // REIFY COLLECTION
            existingGraph.AddCollection(beatles_coll);

            existingGraph.AddTriple(new RDFTriple(
                new RDFResource("http://www.thebeatles.com/"),
                RDFVocabulary.FOAF.GROUP,
                beatles_coll.ReificationSubject
            ));

            // WORKING WITH RDF STORES

            // CREATE CONTEXT FROM STRING
            var wdisney_ctx = new RDFContext("http://www.waltdisney.com/");
            // CREATE CONTEXT FROM URI
            var wdisney_ctx_uri = new RDFContext(new Uri("http://www.waltdisney.com/"));
            // CREATE DEFAULT CONTEXT (DEFAULT NAMESPACE)
            var wdisney_ctx_default = new RDFContext();

            // CREATE QUADRUPLES
            // "From Wikipedia.com: Mickey Mouse is 85 years old"
            RDFQuadruple wk_mickeymouse_is85yr = new RDFQuadruple(
                new RDFContext("http://www.wikipedia.com/"),
                new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                RDFVocabulary.FOAF.AGE,
                new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER)
            );
            
            // "From WaltDisney.com: Mickey Mouse is 85 years old"
            RDFQuadruple wd_mickeymouse_is85yr = new RDFQuadruple(
                new RDFContext("http://www.waltdisney.com/"),
                new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                RDFVocabulary.FOAF.AGE,
                new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER)
            );

            // "From Wikipedia.com: Donald Duck has english name "Donald Duck""
            RDFQuadruple wk_donald_duck_name_enus = new RDFQuadruple(
                new RDFContext("http://www.wikipedia.com/"),
                new RDFResource("http://www.waltdisney.com/donald_duck"),
                RDFVocabulary.FOAF.NAME,
                new RDFPlainLiteral("Donald Duck", "en")
            );

            // CREATE EMPTY MEMORY STORE
            var wdStore = new RDFMemoryStore();

            // CREATE MEMORY STORE FROM A LIST OF QUADRUPLES
            var quadruples = new List<RDFQuadruple> { wk_mickeymouse_is85yr, wk_mickeymouse_is85yr };
            var wdStoreFilled = new RDFMemoryStore();
            foreach(var q in quadruples)
            {
                wdStoreFilled.AddQuadruple(q);
            }

            // GET A DATATABLE FROM A MEMORY STORE (any kind of store can be exported to datatable)
            var wdStore_table = wdStoreFilled.ToDataTable();
            // GET A MEMORY STORE FROM A DATATABLE
            var wdStore_new = RDFMemoryStore.FromDataTable(wdStore_table);


            // ITERATE QUADRUPLES OF A MEMORY STORE WITH FOREACH
            foreach (var q in wdStore)
            {
                Console.WriteLine("Quadruple: " + q);
                Console.WriteLine(" Context: " + q.Context);
                Console.WriteLine(" Subject: " + q.Subject);
                Console.WriteLine(" Predicate: " + q.Predicate);
                Console.WriteLine(" Object: " + q.Object);
            }

            // ITERATE QUADRUPLES OF A MEMORY STORE WITH ENUMERATOR
            var quadruplesEnum = wdStore.QuadruplesEnumerator;
            while (quadruplesEnum.MoveNext())
            {
                Console.WriteLine("Quadruple: " + quadruplesEnum.Current);
                Console.WriteLine(" Context: " + quadruplesEnum.Current.Context);
                Console.WriteLine(" Subject: " + quadruplesEnum.Current.Subject);
                Console.WriteLine(" Predicate: " + quadruplesEnum.Current.Predicate);
                Console.WriteLine(" Object: " + quadruplesEnum.Current.Object);
            }

            var nquadsFormat = RDFStoreEnums.RDFFormats.NQuads;
            // READ N-QUADS FILE
            //var myStore = RDFMemoryStore.FromFile(nquadsFormat, "C:\\file.nq");
            // READ N-QUADS STREAM
            //var myStore = RDFMemoryStore.FromStream(nquadsFormat, inStream);
            // WRITE N-QUADS FILE
            wdStoreFilled.ToFile(nquadsFormat, @"C:\TEMP\newfile.nq");
            // WRITE N-QUADS STREAM
            //myStore.ToStream(nquadsFormat, outStream);


            var trixFormat = RDFStoreEnums.RDFFormats.TriX;
            // READ TRIX FILE
            //var memStore = RDFMemoryStore.FromFile(trixFormat, "C:\\file.trix");
            // READ TRIX STREAM
            //var memStore = RDFMemoryStore.FromStream(trixFormat, inStream);
            // WRITE TRIX FILE
            wdStoreFilled.ToFile(trixFormat, @"C:\TEMP\newfile.trix");
            // WRITE TRIX STREAM
            //myStore.ToStream(trixFormat, outStream);


            // CONNECT TO SQLSERVER STORE WITH CONNECTION STRING
            //var sqlServer = new RDFSQLServerStore(sqlServerConnectionString);


            // CREATE EMPTY FEDERATION
            var fed = new RDFFederation();

            /*
            // CREATE FEDERATION FROM A LIST OF STORES
            var stores = new List<RDFStore>{ waltDisneyStore, waltDisneyStoreFilled };
            var fedFilled = new RDFFederation();
            foreach(var store in stores)
            {
                fedFilled.AddStore(store);
            }
            */

            // ITERATE STORES OF A FEDERATION
            foreach (var s in fed)
            {
                Console.WriteLine("Store: " + s);
                Console.WriteLine(" Type: " + s.StoreType);
            }


            


        }


        /// <summary>
        /// WORKING WITH SPARQL QUERIES
        /// </summary>
        private void WorkingWithQueries(RDFGraph graph, RDFResource donaldduck)
        {
            RDFSelectQuery selectQuery = new RDFSelectQuery();

            // CREATE VARIABLE
            var x = new RDFVariable("x"); // ?X
            var y = new RDFVariable("y"); // ?Y
            var n = new RDFVariable("n"); // ?N
            var c = new RDFVariable("c"); // ?C

            // CREATE PATTERNS
            var dogOf = new RDFResource(RDFVocabulary.DC.BASE_URI + "dogOf");
            var y_dogOf_x = new RDFPattern(y, dogOf, x); // TRIPLE PATTERN
            var c_y_dogOf_x = new RDFPattern(c, y, dogOf, x); // QUADRUPLE PATTERN


            // CREATE EMPTY PATTERN GROUP
            var pg1 = new RDFPatternGroup("PG1");

            // CREATE PATTERN GROUP FROM A LIST OF PATTERNS
            var patterns = new List<RDFPattern>() { y_dogOf_x };
            var pg2 = new RDFPatternGroup("PG2", patterns);

            // ADD PATTERNS TO PATTERN GROUP
            pg1.AddPattern(y_dogOf_x);
            pg1.AddPattern(c_y_dogOf_x);


            // ADD PATTERN GROUPS TO QUERY
            selectQuery.AddPatternGroup(pg1);
            selectQuery.AddPatternGroup(pg2);

            // ADD FILTERS TO PATTERN GROUP
            pg1.AddFilter(new RDFSameTermFilter(new RDFVariable("character"), donaldduck));
            pg1.AddFilter(new RDFLangMatchesFilter(n, "it-IT"));


            // ADD MODIFIERS TO QUERY
            selectQuery.AddModifier(new RDFOrderByModifier(n, RDFQueryEnums.RDFOrderByFlavors.ASC));
            selectQuery.AddModifier(new RDFDistinctModifier());
            selectQuery.AddModifier(new RDFGroupByModifier(new List<RDFVariable>{ x }));
            selectQuery.AddModifier(new RDFLimitModifier(100));
            selectQuery.AddModifier(new RDFOffsetModifier(25));


            // INITIALIZE PROPERTY PATH (VARIABLE TERMS)
            var variablePropPath = new RDFPropertyPath(new RDFVariable("START"), new RDFVariable("END"));
            // INITIALIZE PROPERTY PATH (MIXED TERMS)
            var mixedPropPath = new RDFPropertyPath(new RDFResource("http://res.org/"), new RDFVariable("END"));


            //ADD SEQUENCE STEPS TO PROPERTY PATH
            variablePropPath.AddSequenceStep(new
            RDFPropertyPathStep(new RDFResource("rdf:P1")));
            variablePropPath.AddSequenceStep(new
            RDFPropertyPathStep(new RDFResource("rdf:P2")));

            //ADD ALTERNATIVE STEPS TO PROPERTY PATH
            var altSteps = new List<RDFPropertyPathStep>();
            altSteps.Add(new RDFPropertyPathStep(new
            RDFResource("rdf:P3")));
            altSteps.Add(new RDFPropertyPathStep(new
            RDFResource("rdf:P7")));
            variablePropPath.AddAlternativeSteps(altSteps);


            // ADD INVERSE SEQUENCE STEP TO PROPERTY PATH: ?START ^rdf:INVP ?END
            variablePropPath.AddSequenceStep(new RDFPropertyPathStep(new RDFResource("rdf:INVP")).Inverse());

            //ADD ALTERNATIVE STEPS (ONE INVERSE) TO PROPERTY PATH: ?START (rdf:P3|^rdf:INVP3) ?END
            var altSteps2 = new List<RDFPropertyPathStep>();
            altSteps2.Add(new RDFPropertyPathStep(new RDFResource("rdf:P3")));
            altSteps2.Add(new RDFPropertyPathStep(new RDFResource("rdf:INVP3")).Inverse());
            variablePropPath.AddAlternativeSteps(altSteps2);

            // ADD SUBQUERY TO QUERY
            RDFSelectQuery mySubQuery = new RDFSelectQuery();
            selectQuery.AddSubQuery(mySubQuery);


            // ADD AGGREGATORS TO GROUPBY MODIFIER
            RDFGroupByModifier gm = new RDFGroupByModifier(new List<RDFVariable> { x });

            gm.AddAggregator(new RDFAvgAggregator(new RDFVariable("age"), new RDFVariable("avg_age")));
            gm.AddAggregator(new RDFCountAggregator(new RDFVariable("dept"), new RDFVariable("count_dept")));
            gm.AddAggregator(new RDFGroupConcatAggregator(new RDFVariable("name"), new RDFVariable("gc_name"), "-"));
            gm.AddAggregator(new RDFSampleAggregator(new RDFVariable("name"), new RDFVariable("sample_name")));
            gm.AddAggregator(new RDFSumAggregator(new RDFVariable("salary"), new RDFVariable("sum_salary")));
            gm.AddAggregator(new RDFMinAggregator(new RDFVariable("age"), new RDFVariable("min_age"),
            RDFQueryEnums.RDFMinMaxAggregatorFlavors.Numeric)); //?age is expected to have numeric typedliterals
            gm.AddAggregator(new RDFMinAggregator(new RDFVariable("city"), new RDFVariable("min_city"),
            RDFQueryEnums.RDFMinMaxAggregatorFlavors.String));
            gm.AddAggregator(new RDFMaxAggregator(new RDFVariable("salary"), new RDFVariable("max_salary"),
            RDFQueryEnums.RDFMinMaxAggregatorFlavors.Numeric)); //?salary is expected to have numeric typedliterals
            gm.AddAggregator(new RDFMaxAggregator(new RDFVariable("city"), new RDFVariable("min_city"),
            RDFQueryEnums.RDFMinMaxAggregatorFlavors.String));


            // It is possible to filter a group - by partitioned set of SPARQL results by applying the SetHavingClause operator on desired aggregators:

            // ADD AGGREGATORS TO GROUPBY MODIFIER
            RDFModelEnums.RDFDatatypes xsdDbl = RDFModelEnums.RDFDatatypes.XSD_DOUBLE;
            RDFModelEnums.RDFDatatypes xsdInt = RDFModelEnums.RDFDatatypes.XSD_INT;
            gm.AddAggregator(new RDFAvgAggregator(new RDFVariable("age"), new RDFVariable("avg_age"))
                .SetHavingClause(RDFQueryEnums.RDFComparisonFlavors.GreaterThan, new RDFTypedLiteral("25.5", xsdDbl))
            );
            gm.AddAggregator(new RDFCountAggregator(new RDFVariable("dept"), new RDFVariable("count_dept"))
                .SetHavingClause(RDFQueryEnums.RDFComparisonFlavors.EqualTo, new RDFTypedLiteral("4", xsdInt))
            );


            //Declare the following SPARQL values:
            /*
            VALUES (?a ?b ?c) {
            ("1" "2" "3")
            ("2" "4" "6")
            ("3" "6" UNDEF)
            }
            */
            RDFValues myValues = new RDFValues()
                .AddColumn(new RDFVariable("a"),
                new List<RDFPatternMember>()
                {
                    new RDFPlainLiteral("1"),
                    new RDFPlainLiteral("2"),
                    new RDFPlainLiteral("3")
                })
                .AddColumn(new RDFVariable("b"),
                new List<RDFPatternMember>()
                {
                    new RDFPlainLiteral("2"),
                    new RDFPlainLiteral("4"),
                    new RDFPlainLiteral("6")
                })
                .AddColumn(new RDFVariable("c"),
                new List<RDFPatternMember>()
                {
                    new RDFPlainLiteral("3"),
                    new RDFPlainLiteral("6"),
                    null //UNDEF
                });

            // ADD PROPERTY PATH TO PATTERN GROUP
            pg1.AddValues(myValues);

            // CREATING AND EXECUTING SELECT QUERIES


            


            // APPLY SELECT QUERY TO GRAPH
            RDFSelectQueryResult selectQueryResult = selectQuery.ApplyToGraph(graph);
            // APPLY SELECT QUERY TO STORE
            //RDFSelectQueryResult selectQueryResult = selectQuery.ApplyToStore(store);
            // APPLY SELECT QUERY TO FEDERATION
            //RDFSelectQueryResult selectQueryResult = selectQuery.ApplyToFederation(federation);

            // EXPORT SELECT QUERY RESULTS TO SPARQL XML FORMAT (FILE)
            selectQueryResult.ToSparqlXmlResult(@"C:\TMP\select_results.srq");
            // EXPORT SELECT QUERY RESULTS TO SPARQL XML FORMAT (STREAM)
            //selectQueryResult.ToSparqlXmlResult(myStream);
            // IMPORT SELECT QUERY RESULTS FROM SPARQL XML FORMAT (FILE)
            selectQueryResult = RDFSelectQueryResult.FromSparqlXmlResult(@"C:\TMP\select_results.srq");
            // IMPORT SELECT QUERY RESULTS FROM SPARQL XML FORMAT (STREAM)
            //selectQueryResult = RDFSelectQueryResult.FromSparqlXmlResult(myStream);

        }


    }
}
