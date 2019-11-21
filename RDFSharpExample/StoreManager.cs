using RDFSharp.Model;
using RDFSharp.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFSharpExample
{
    public class StoreManager
    {
        public RDFMemoryStore Store { get; private set; }
        public RDFSQLServerStore SqlStore { get; private set; }

        public StoreManager()
        {
            Store = new RDFMemoryStore();

            String sqlServerConnString = "";
            SqlStore = new RDFSQLServerStore(sqlServerConnString);
        }

        public void AddQuadrupleObject(string contextUri, string subjUri, string predUri, string objectUri)
        {
            RDFContext context = new RDFContext(contextUri);
            RDFResource subj = new RDFResource(subjUri);
            RDFResource pred = new RDFResource(predUri);
            RDFResource obj = new RDFResource(objectUri);

            RDFQuadruple quadruple = new RDFQuadruple(context, subj, pred, obj);
            Store.AddQuadruple(quadruple);
        }

        public void AddQuadrupleLiteral(string contextUri, string subjUri, string predUri, string literalValue)
        {
            RDFContext context = new RDFContext(contextUri);
            RDFResource subj = new RDFResource(subjUri);
            RDFResource pred = new RDFResource(predUri);
            RDFLiteral lit = new RDFPlainLiteral(literalValue);

            RDFQuadruple quadruple = new RDFQuadruple(context, subj, pred, lit);
            Store.AddQuadruple(quadruple);
        }

    }
}
