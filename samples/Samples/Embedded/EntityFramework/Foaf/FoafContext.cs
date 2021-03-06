﻿ 

// -----------------------------------------------------------------------
// <autogenerated>
//    This code was generated from a template.
//
//    Changes to this file may cause incorrect behaviour and will be lost
//    if the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using BrightstarDB.Client;
using BrightstarDB.EntityFramework;


namespace BrightstarDB.Samples.EntityFramework.Foaf 
{
    public partial class FoafContext : BrightstarEntityContext {
    	private static readonly EntityMappingStore TypeMappings;
    	
    	static FoafContext() 
    	{
    		TypeMappings = new EntityMappingStore();
    		var provider = new ReflectionMappingProvider();
    		provider.AddMappingsForType(TypeMappings, typeof(BrightstarDB.Samples.EntityFramework.Foaf.IPerson));
    		TypeMappings.SetImplMapping<BrightstarDB.Samples.EntityFramework.Foaf.IPerson, BrightstarDB.Samples.EntityFramework.Foaf.Person>();
    	}
    	
    	/// <summary>
    	/// Initialize a new entity context using the specified BrightstarDB
    	/// Data Object Store connection
    	/// </summary>
    	/// <param name="dataObjectStore">The connection to the BrightstarDB Data Object Store that will provide the entity objects</param>
        /// <param name="typeMappings">OPTIONAL: A <see cref="EntityMappingStore"/> that overrides the default mappings generated by reflection.</param>
    	public FoafContext(IDataObjectStore dataObjectStore, EntityMappingStore typeMappings = null) : base(typeMappings ?? TypeMappings, dataObjectStore)
    	{
    		InitializeContext();
    	}
    
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar connection string
    	/// </summary>
    	/// <param name="connectionString">The connection to be used to connect to an existing BrightstarDB store</param>
    	/// <param name="enableOptimisticLocking">OPTIONAL: If set to true optmistic locking will be applied to all entity updates</param>
        /// <param name="updateGraphUri">OPTIONAL: The URI identifier of the graph to be updated with any new triples created by operations on the store. If
        /// not defined, the default graph in the store will be updated.</param>
        /// <param name="datasetGraphUris">OPTIONAL: The URI identifiers of the graphs that will be queried to retrieve entities and their properties.
        /// If not defined, all graphs in the store will be queried.</param>
        /// <param name="versionGraphUri">OPTIONAL: The URI identifier of the graph that contains version number statements for entities. 
        /// If not defined, the <paramref name="updateGraphUri"/> will be used.</param>
        /// <param name="typeMappings">OPTIONAL: A <see cref="EntityMappingStore"/> that overrides the default mappings generated by reflection.</param>
    	public FoafContext(
    	    string connectionString, 
    		bool? enableOptimisticLocking=null,
    		string updateGraphUri = null,
    		IEnumerable<string> datasetGraphUris = null,
    		string versionGraphUri = null,
            EntityMappingStore typeMappings = null
        ) : base(typeMappings ?? TypeMappings, connectionString, enableOptimisticLocking, updateGraphUri, datasetGraphUris, versionGraphUri)
    	{
    		InitializeContext();
    	}
    
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// connection string retrieved from the configuration.
    	/// </summary>
        /// <param name="typeMappings">OPTIONAL: A <see cref="EntityMappingStore"/> that overrides the default mappings generated by reflection.</param>
    	public FoafContext(EntityMappingStore typeMappings = null) : base(typeMappings ?? TypeMappings)
    	{
    		InitializeContext();
    	}
    	
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// connection string retrieved from the configuration and the
    	//  specified target graphs
    	/// </summary>
        /// <param name="updateGraphUri">The URI identifier of the graph to be updated with any new triples created by operations on the store. If
        /// set to null, the default graph in the store will be updated.</param>
        /// <param name="datasetGraphUris">The URI identifiers of the graphs that will be queried to retrieve entities and their properties.
        /// If set to null, all graphs in the store will be queried.</param>
        /// <param name="versionGraphUri">The URI identifier of the graph that contains version number statements for entities. 
        /// If set to null, the value of <paramref name="updateGraphUri"/> will be used.</param>
        /// <param name="typeMappings">OPTIONAL: A <see cref="EntityMappingStore"/> that overrides the default mappings generated by reflection.</param>
    	public FoafContext(
    		string updateGraphUri,
    		IEnumerable<string> datasetGraphUris,
    		string versionGraphUri,
            EntityMappingStore typeMappings = null
    	) : base(typeMappings ?? TypeMappings, updateGraphUri:updateGraphUri, datasetGraphUris:datasetGraphUris, versionGraphUri:versionGraphUri)
    	{
    		InitializeContext();
    	}
    	
    	private void InitializeContext() 
    	{
    		Persons = 	new BrightstarEntitySet<BrightstarDB.Samples.EntityFramework.Foaf.IPerson>(this);
    	}
    	
    	public IEntitySet<BrightstarDB.Samples.EntityFramework.Foaf.IPerson> Persons
    	{
    		get; private set;
    	}
    	
    }
}
namespace BrightstarDB.Samples.EntityFramework.Foaf 
{
    
    public partial class Person : BrightstarEntityObject, IPerson 
    {
    	public Person(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Person() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of BrightstarDB.Samples.EntityFramework.Foaf.IPerson
    
    	public System.String Nickname
    	{
            		get { return GetRelatedProperty<System.String>("Nickname"); }
            		set { SetRelatedProperty("Nickname", value); }
    	}
    
    	public System.String Name
    	{
            		get { return GetRelatedProperty<System.String>("Name"); }
            		set { SetRelatedProperty("Name", value); }
    	}
    
    	public System.String Organisation
    	{
            		get { return GetRelatedProperty<System.String>("Organisation"); }
            		set { SetRelatedProperty("Organisation", value); }
    	}
    	public System.Collections.Generic.ICollection<BrightstarDB.Samples.EntityFramework.Foaf.IPerson> Knows
    	{
    		get { return GetRelatedObjects<BrightstarDB.Samples.EntityFramework.Foaf.IPerson>("Knows"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("Knows", value); }
    								}
    	public System.Collections.Generic.ICollection<BrightstarDB.Samples.EntityFramework.Foaf.IPerson> KnownBy
    	{
    		get { return GetRelatedObjects<BrightstarDB.Samples.EntityFramework.Foaf.IPerson>("KnownBy"); }
    		set { if (value == null) throw new ArgumentNullException("value"); SetRelatedObjects("KnownBy", value); }
    								}
    	#endregion
    }
}
