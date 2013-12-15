using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using BrightstarDB.Query;
using BrightstarDB.Storage;
using VDS.RDF;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Storage.Virtualisation;

namespace BrightstarDB.Rdf
{
    internal class BrightstarTripleStoreAdapter : ITripleStore
    {
        private IStore _store;

        public BrightstarTripleStoreAdapter(IStore readStore)
        {
            _store = readStore;
        }

        public void Dispose()
        {
            
        }

        public bool Add(IGraph g)
        {
            throw new NotImplementedException();
        }

        public bool Add(IGraph g, bool mergeIfExists)
        {
            throw new NotImplementedException();
        }

        public bool AddFromUri(Uri graphUri)
        {
            throw new NotImplementedException();
        }

        public bool AddFromUri(Uri graphUri, bool mergeIfExists)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Uri graphUri)
        {
            throw new NotImplementedException();
        }

        public bool HasGraph(Uri graphUri)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty { get { return false; } }

        public BaseGraphCollection Graphs { get { return new StoreGraphCollection(_store); } }
        public IEnumerable<Triple> Triples { get {throw new NotImplementedException();} }

        public IGraph this[Uri graphUri]
        {
            get { return new BrightstarGraphAdapter(_store, graphUri.ToString()); }
        }

#pragma warning disable 67 // Required to implement the dotNetRDF interface
        public event TripleStoreEventHandler GraphAdded;
        public event TripleStoreEventHandler GraphRemoved;
        public event TripleStoreEventHandler GraphChanged;
        public event TripleStoreEventHandler GraphCleared;
        public event TripleStoreEventHandler GraphMerged;
#pragma warning restore 67
    }

    internal class StoreGraphCollection : BaseGraphCollection
    {
        private readonly IStore _store;
        private readonly List<string> _graphUris;
 
        public StoreGraphCollection(IStore store)
        {
            _store = store;
            _graphUris = store.GetGraphUris().ToList();
        }

        public override bool Contains(Uri graphUri)
        {
            return _graphUris.Contains(graphUri.ToString());
        }

        protected override bool Add(IGraph g, bool mergeIfExists)
        {
            throw new NotImplementedException();
        }

        protected override bool Remove(Uri graphUri)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
        }

        public override IEnumerator<IGraph> GetEnumerator()
        {
            return _graphUris.Select(g => new BrightstarGraphAdapter(_store, g)).GetEnumerator();
        }

        public override int Count
        {
            get { throw new NotImplementedException(); }
        }

        public override IEnumerable<Uri> GraphUris
        {
            get { return _graphUris.Select(g=>new Uri(g)); }
        }

        public override IGraph this[Uri graphUri]
        {
            get { return new BrightstarGraphAdapter(_store, graphUri.ToString()); }
        }
    }

    internal class BrightstarGraphAdapter : IGraph
    {
        private readonly IStore _store;
        private readonly string _graphUri;

        public BrightstarGraphAdapter(IStore store, string graphUri)
        {
            _store = store;
            _graphUri = graphUri;
            NamespaceMap = new NamespaceMapper();
        }

        public IBlankNode CreateBlankNode()
        {
            throw new NotImplementedException();
        }

        public IBlankNode CreateBlankNode(string nodeId)
        {
            throw new NotImplementedException();
        }

        public IGraphLiteralNode CreateGraphLiteralNode()
        {
            throw new NotImplementedException();
        }

        public IGraphLiteralNode CreateGraphLiteralNode(IGraph subgraph)
        {
            throw new NotImplementedException();
        }

        public ILiteralNode CreateLiteralNode(string literal, Uri datatype)
        {
            throw new NotImplementedException();
        }

        public ILiteralNode CreateLiteralNode(string literal)
        {
            throw new NotImplementedException();
        }

        public ILiteralNode CreateLiteralNode(string literal, string langspec)
        {
            throw new NotImplementedException();
        }

        public IUriNode CreateUriNode(Uri uri)
        {
            throw new NotImplementedException();
        }

        public IVariableNode CreateVariableNode(string varname)
        {
            throw new NotImplementedException();
        }

        public string GetNextBlankNodeID()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        public bool Assert(Triple t)
        {
            throw new NotImplementedException();
        }

        public bool Assert(IEnumerable<Triple> ts)
        {
            throw new NotImplementedException();
        }

        public bool Retract(Triple t)
        {
            throw new NotImplementedException();
        }

        public bool Retract(IEnumerable<Triple> ts)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IUriNode CreateUriNode()
        {
            throw new NotImplementedException();
        }

        public IUriNode CreateUriNode(string qname)
        {
            throw new NotImplementedException();
        }

        public IBlankNode GetBlankNode(string nodeId)
        {
            throw new NotImplementedException();
        }

        public ILiteralNode GetLiteralNode(string literal, string langspec)
        {
            throw new NotImplementedException();
        }

        public ILiteralNode GetLiteralNode(string literal)
        {
            throw new NotImplementedException();
        }

        public ILiteralNode GetLiteralNode(string literal, Uri datatype)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriples(Uri uri)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriples(INode n)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithObject(Uri u)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithObject(INode n)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithPredicate(INode n)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithPredicate(Uri u)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithSubject(INode n)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithSubject(Uri u)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithSubjectPredicate(INode subj, INode pred)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithSubjectObject(INode subj, INode obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Triple> GetTriplesWithPredicateObject(INode pred, INode obj)
        {
            throw new NotImplementedException();
        }

        public IUriNode GetUriNode(string qname)
        {
            throw new NotImplementedException();
        }

        public IUriNode GetUriNode(Uri uri)
        {
            throw new NotImplementedException();
        }

        public bool ContainsTriple(Triple t)
        {
            throw new NotImplementedException();
        }

        public void Merge(IGraph g)
        {
            throw new NotImplementedException();
        }

        public void Merge(IGraph g, bool keepOriginalGraphUri)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IGraph g, out Dictionary<INode, INode> mapping)
        {
            throw new NotImplementedException();
        }

        public bool IsSubGraphOf(IGraph g)
        {
            throw new NotImplementedException();
        }

        public bool IsSubGraphOf(IGraph g, out Dictionary<INode, INode> mapping)
        {
            throw new NotImplementedException();
        }

        public bool HasSubGraph(IGraph g)
        {
            throw new NotImplementedException();
        }

        public bool HasSubGraph(IGraph g, out Dictionary<INode, INode> mapping)
        {
            throw new NotImplementedException();
        }

        public GraphDiffReport Difference(IGraph g)
        {
            throw new NotImplementedException();
        }

        public DataTable ToDataTable()
        {
            throw new NotImplementedException();
        }

        public Uri ResolveQName(string qname)
        {
            throw new NotImplementedException();
        }

        public Uri BaseUri { get; set; }
        public bool IsEmpty { get; private set; }
        public INamespaceMapper NamespaceMap { get; private set; }
        public IEnumerable<INode> Nodes { get; private set; }
        public BaseTripleCollection Triples { get { return new BrightstarTripleCollection(_store, _graphUri); } }
#pragma warning disable 67 // Required to implement dotNetRDF interface
        public event TripleEventHandler TripleAsserted;
        public event TripleEventHandler TripleRetracted;
        public event GraphEventHandler Changed;
        public event CancellableGraphEventHandler ClearRequested;
        public event GraphEventHandler Cleared;
        public event CancellableGraphEventHandler MergeRequested;
        public event GraphEventHandler Merged;
#pragma warning restore 67
    }

    internal class BrightstarTripleCollection : BaseTripleCollection
    {
        private IStore _store;
        private string _graphUri;

        public BrightstarTripleCollection(IStore store, string graphUri)
        {
            _store = store;
            _graphUri = graphUri;
        }

        protected override bool Add(Triple t)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(Triple t)
        {
            throw new NotImplementedException();
        }

        protected override bool Delete(Triple t)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            
        }

        public override IEnumerator<Triple> GetEnumerator()
        {
            Uri g = new Uri(_graphUri);
            return _store.Match(null, null, null, graphs: new string[] {_graphUri})
                         .Select(t =>
                                 new Triple(
                                     new BrightstarUriNode(new Uri(t.Subject)),
                                     new BrightstarUriNode(new Uri(t.Object)),
                                     t.IsLiteral
                                         ? BrightstarLiteralNode.Create(t.Object, t.DataType, t.LangCode) as INode
                                         : new BrightstarUriNode(new Uri(t.Object)),
                                     g
                                     )).GetEnumerator();
        }

        public override int Count
        {
            get { throw new NotImplementedException(); }
        }

        public override Triple this[Triple t]
        {
            get { throw new NotImplementedException(); }
        }

        public override IEnumerable<INode> ObjectNodes
        {
            get { throw new NotImplementedException(); }
        }

        public override IEnumerable<INode> PredicateNodes
        {
            get { throw new NotImplementedException(); }
        }

        public override IEnumerable<INode> SubjectNodes
        {
            get { throw new NotImplementedException(); }
        }
    }
}
