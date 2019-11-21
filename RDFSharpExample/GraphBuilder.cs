using RDFSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFSharpExample
{
    public static class GraphBuilder
    {
        public static RDFGraph WaltDisneyGraphBuild()
        {
            RDFResource donaldduck = new RDFResource("http://www.waltdisney.com/donald_duck");

            RDFPlainLiteral donaldduck_name = new RDFPlainLiteral("Donald Duck");
            //create typed literal
            // "85"^^xsd:integer
            RDFTypedLiteral mickeymouse_age = new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER);
            RDFTypedLiteral donaldduck_age = new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER);


            //create triples
            // "Mickey Mouse is 85 years old"
            RDFTriple mickeymouse_is85yr = new RDFTriple(new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                                                         RDFVocabulary.FOAF.AGE,
                                                         mickeymouse_age);

            // "Donald Duck has English-US name "Donald Duck""
            RDFTriple donaldduck_name_enus_triple = new RDFTriple(donaldduck,
                                                                  RDFVocabulary.FOAF.NAME,
                                                                  donaldduck_name);

            // "Donald Duck is 85 years old"
            RDFTriple donaldduck_is85yr = new RDFTriple(donaldduck,
                                                         RDFVocabulary.FOAF.AGE,
                                                         donaldduck_age);



            List<RDFTriple> triples = new List<RDFTriple> { mickeymouse_is85yr, donaldduck_name_enus_triple, donaldduck_is85yr };

            RDFGraph waltdisney = new RDFGraph(triples);
            waltdisney.SetContext(new Uri("http://waltdisney.com/"));

            return waltdisney;
        }

    }
}
