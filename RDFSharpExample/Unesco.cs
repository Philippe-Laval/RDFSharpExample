using RDFSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFSharpExample
{
    public class Unesco
    {
        public void Load()
        {
            //RDFGraph graph = RDFGraph.FromFile(RDFModelEnums.RDFFormats.Turtle, @"C:\Users\philippel\Documents\ONU\unescothes.ttl");
            RDFGraph graph = RDFGraph.FromFile(RDFModelEnums.RDFFormats.RdfXml, @"C:\Users\philippel\Documents\ONU\unescothes.rdf");

            long triplesCount = graph.TriplesCount;
        }
    }
}
