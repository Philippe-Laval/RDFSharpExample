using RDFSharp.Model;
using RDFSharp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFSharpExample
{
    public static class StoreBuilder
    {
        //Creating store based on: http://dadev.cloudapp.net/Datos%20Abiertos/PDF/ReferenceGuide.pdf
        public static RDFMemoryStore CreateStore()
        {
            RDFMemoryStore rdfstore = new RDFMemoryStore();
            // "from wikipedia.com: mickey mouse is 85 years old" 
            rdfstore.AddQuadruple(new RDFQuadruple(
                new RDFContext("http://www.wikipedia.com/"),
                new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                new RDFResource("http://xmlns.com/foaf/0.1/age"),
                new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INT))
            );

            // "from waltdisney.com: mickey mouse is 85 years old" 
            rdfstore.AddQuadruple(new RDFQuadruple(
                new RDFContext("http://www.waltdisney.com/"),
                new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                new RDFResource("http://xmlns.com/foaf/0.1/age"),
                new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INT))
            );

            // "from wikipedia.com: donald duck has english-us name "donald duck"" 
            rdfstore.AddQuadruple(new RDFQuadruple(
                new RDFContext("http://www.wikipedia.com/"),
                new RDFResource("http://www.waltdisney.com/donald_duck"),
                new RDFResource("http://xmlns.com/foaf/0.1/name"),
                new RDFTypedLiteral("donald duck", RDFModelEnums.RDFDatatypes.XSD_STRING))
            );
            return rdfstore;
        }

        public static RDFFederation CreateFederation()
        {
            RDFFederation federation = new RDFFederation();
            federation.AddStore(CreateStore());
            return federation;
        }
    }
}
