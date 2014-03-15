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


namespace BrightstarDB.OData 
{
    public partial class MyEntityContext : BrightstarEntityContext {
    	private static readonly EntityMappingStore TypeMappings;
    	
    	static MyEntityContext() 
    	{
    		TypeMappings = new EntityMappingStore();
    		var provider = new ReflectionMappingProvider();
    		provider.AddMappingsForType(TypeMappings, typeof(BrightstarDB.OData.IDepartment));
    		TypeMappings.SetImplMapping<BrightstarDB.OData.IDepartment, BrightstarDB.OData.Department>();
    		provider.AddMappingsForType(TypeMappings, typeof(BrightstarDB.OData.IPerson));
    		TypeMappings.SetImplMapping<BrightstarDB.OData.IPerson, BrightstarDB.OData.Person>();
    		provider.AddMappingsForType(TypeMappings, typeof(BrightstarDB.OData.ISkill));
    		TypeMappings.SetImplMapping<BrightstarDB.OData.ISkill, BrightstarDB.OData.Skill>();
    	}
    	
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// Data Object Store connection
    	/// </summary>
    	/// <param name="dataObjectStore">The connection to the Brightstar Data Object Store that will provide the entity objects</param>
    	public MyEntityContext(IDataObjectStore dataObjectStore) : base(TypeMappings, dataObjectStore)
    	{
    		InitializeContext();
    	}
    
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// connection string
    	/// </summary>
    	/// <param name="connectionString">The connection to be used to connect to an existing BrightstarDB store</param>
    	/// <param name="enableOptimisticLocking">Optional boolean flag to override the optimistic locking setting specified in the connection string.</param>
    	public MyEntityContext(string connectionString, bool? enableOptimisticLocking = null) : base(TypeMappings, connectionString, enableOptimisticLocking)
    	{
    		InitializeContext();
    	}
    
    	/// <summary>
    	/// Initialize a new entity context using the specified Brightstar
    	/// connection string retrieved from the configuration.
    	/// </summary>
    	public MyEntityContext() : base(TypeMappings)
    	{
    		InitializeContext();
    	}
    	
    	private void InitializeContext() 
    	{
    		Departments = 	new BrightstarEntitySet<BrightstarDB.OData.IDepartment>(this);
    		Persons = 	new BrightstarEntitySet<BrightstarDB.OData.IPerson>(this);
    		Skills = 	new BrightstarEntitySet<BrightstarDB.OData.ISkill>(this);
    	}
    	
    	public IEntitySet<BrightstarDB.OData.IDepartment> Departments 
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<BrightstarDB.OData.IPerson> Persons 
    	{
    		get; private set;
    	}
    	
    	public IEntitySet<BrightstarDB.OData.ISkill> Skills 
    	{
    		get; private set;
    	}
    	
    }
}
namespace BrightstarDB.OData 
{
    public partial class Department : BrightstarEntityObject, IDepartment 
    {
    	public Department(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Department() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of BrightstarDB.OData.IDepartment
    
    	public System.String Name
    	{
            		get { return GetRelatedProperty<System.String>("Name"); }
            		set { SetRelatedProperty("Name", value); }
    	}
    	public System.Collections.Generic.ICollection<BrightstarDB.OData.IPerson> Members
    	{
    		get { return GetRelatedObjects<BrightstarDB.OData.IPerson>("Members"); }
    		set { SetRelatedObjects("Members", value); }
    								}
    	#endregion
    }
}
namespace BrightstarDB.OData 
{
    public partial class Person : BrightstarEntityObject, IPerson 
    {
    	public Person(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Person() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of BrightstarDB.OData.IPerson
    
    	public System.String Name
    	{
            		get { return GetRelatedProperty<System.String>("Name"); }
            		set { SetRelatedProperty("Name", value); }
    	}
    
    	public System.String Email
    	{
            		get { return GetRelatedProperty<System.String>("Email"); }
            		set { SetRelatedProperty("Email", value); }
    	}
    
    	public System.Int32 EmployeeNumber
    	{
            		get { return GetRelatedProperty<System.Int32>("EmployeeNumber"); }
            		set { SetRelatedProperty("EmployeeNumber", value); }
    	}
    	public System.Collections.Generic.ICollection<BrightstarDB.OData.ISkill> Skills
    	{
    		get { return GetRelatedObjects<BrightstarDB.OData.ISkill>("Skills"); }
    		set { SetRelatedObjects("Skills", value); }
    								}
    
    	public BrightstarDB.OData.ISkill MainSkill
    	{
            get { return GetRelatedObject<BrightstarDB.OData.ISkill>("MainSkill"); }
            set { SetRelatedObject<BrightstarDB.OData.ISkill>("MainSkill", value); }
    	}
    	public System.Collections.Generic.ICollection<BrightstarDB.OData.ISkill> BackupSkills
    	{
    		get { return GetRelatedObjects<BrightstarDB.OData.ISkill>("BackupSkills"); }
    		set { SetRelatedObjects("BackupSkills", value); }
    								}
    
    	public BrightstarDB.OData.IDepartment Department
    	{
            get { return GetRelatedObject<BrightstarDB.OData.IDepartment>("Department"); }
            set { SetRelatedObject<BrightstarDB.OData.IDepartment>("Department", value); }
    	}
    	#endregion
    }
}
namespace BrightstarDB.OData 
{
    public partial class Skill : BrightstarEntityObject, ISkill 
    {
    	public Skill(BrightstarEntityContext context, IDataObject dataObject) : base(context, dataObject) { }
    	public Skill() : base() { }
    	public System.String Id { get {return GetIdentity(); } set { SetIdentity(value); } }
    	#region Implementation of BrightstarDB.OData.ISkill
    
    	public System.String Name
    	{
            		get { return GetRelatedProperty<System.String>("Name"); }
            		set { SetRelatedProperty("Name", value); }
    	}
    
    	public BrightstarDB.OData.IPerson MainExpert
    	{
            get { return GetRelatedObject<BrightstarDB.OData.IPerson>("MainExpert"); }
            set { SetRelatedObject<BrightstarDB.OData.IPerson>("MainExpert", value); }
    	}
    	public System.Collections.Generic.ICollection<BrightstarDB.OData.IPerson> BackupPeople
    	{
    		get { return GetRelatedObjects<BrightstarDB.OData.IPerson>("BackupPeople"); }
    		set { SetRelatedObjects("BackupPeople", value); }
    								}
    	#endregion
    }
}
