//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xyz.Xrm.Entities
{
	
	/// <summary>
	/// System chart attached to an entity.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("savedqueryvisualization")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.0.1.7297")]
	public partial class SavedQueryVisualization : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public struct Fields
		{
			public const string CanBeDeleted = "canbedeleted";
			public const string ComponentState = "componentstate";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string DataDescription = "datadescription";
			public const string Description = "description";
			public const string IntroducedVersion = "introducedversion";
			public const string IsCustomizable = "iscustomizable";
			public const string IsDefault = "isdefault";
			public const string IsManaged = "ismanaged";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string Name = "name";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string PresentationDescription = "presentationdescription";
			public const string PrimaryEntityTypeCode = "primaryentitytypecode";
			public const string SavedQueryVisualizationId = "savedqueryvisualizationid";
			public const string Id = "savedqueryvisualizationid";
			public const string SavedQueryVisualizationIdUnique = "savedqueryvisualizationidunique";
			public const string SolutionId = "solutionid";
			public const string Type = "type";
			public const string VersionNumber = "versionnumber";
			public const string WebResourceId = "webresourceid";
			public const string lk_savedqueryvisualizationbase_createdby = "lk_savedqueryvisualizationbase_createdby";
			public const string lk_savedqueryvisualizationbase_createdonbehalfby = "lk_savedqueryvisualizationbase_createdonbehalfby";
			public const string lk_savedqueryvisualizationbase_modifiedby = "lk_savedqueryvisualizationbase_modifiedby";
			public const string lk_savedqueryvisualizationbase_modifiedonbehalfby = "lk_savedqueryvisualizationbase_modifiedonbehalfby";
			public const string organization_saved_query_visualizations = "organization_saved_query_visualizations";
			public const string webresource_savedqueryvisualizations = "webresource_savedqueryvisualizations";
		}

		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public SavedQueryVisualization() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "savedqueryvisualization";
		
		public const int EntityTypeCode = 1111;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		[System.Diagnostics.DebuggerNonUserCode()]
		private void OnPropertyChanged(string propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		[System.Diagnostics.DebuggerNonUserCode()]
		private void OnPropertyChanging(string propertyName)
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
			}
		}
		
		/// <summary>
		/// Tells whether the saved query visualization can be deleted.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("canbedeleted")]
		public Microsoft.Xrm.Sdk.BooleanManagedProperty CanBeDeleted
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>("canbedeleted");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("CanBeDeleted");
				this.SetAttributeValue("canbedeleted", value);
				this.OnPropertyChanged("CanBeDeleted");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("componentstate")]
		public Microsoft.Xrm.Sdk.OptionSetValue ComponentState
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("componentstate");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who created the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("CreatedBy");
				this.SetAttributeValue("createdby", value);
				this.OnPropertyChanged("CreatedBy");
			}
		}
		
		/// <summary>
		/// Date and time when the system chart was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
		public System.Nullable<System.DateTime> CreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("CreatedOn");
				this.SetAttributeValue("createdon", value);
				this.OnPropertyChanged("CreatedOn");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who created the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("CreatedOnBehalfBy");
				this.SetAttributeValue("createdonbehalfby", value);
				this.OnPropertyChanged("CreatedOnBehalfBy");
			}
		}
		
		/// <summary>
		/// XML string used to define the underlying data for the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("datadescription")]
		public string DataDescription
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("datadescription");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("DataDescription");
				this.SetAttributeValue("datadescription", value);
				this.OnPropertyChanged("DataDescription");
			}
		}
		
		/// <summary>
		/// Description of the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("description")]
		public string Description
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("description");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("Description");
				this.SetAttributeValue("description", value);
				this.OnPropertyChanged("Description");
			}
		}
		
		/// <summary>
		/// Version in which the form is introduced.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("introducedversion")]
		public string IntroducedVersion
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("introducedversion");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("IntroducedVersion");
				this.SetAttributeValue("introducedversion", value);
				this.OnPropertyChanged("IntroducedVersion");
			}
		}
		
		/// <summary>
		/// Information that specifies whether this component can be customized.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("iscustomizable")]
		public Microsoft.Xrm.Sdk.BooleanManagedProperty IsCustomizable
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.BooleanManagedProperty>("iscustomizable");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("IsCustomizable");
				this.SetAttributeValue("iscustomizable", value);
				this.OnPropertyChanged("IsCustomizable");
			}
		}
		
		/// <summary>
		/// Indicates whether the system chart is the default chart for the entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("isdefault")]
		public System.Nullable<bool> IsDefault
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("isdefault");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("IsDefault");
				this.SetAttributeValue("isdefault", value);
				this.OnPropertyChanged("IsDefault");
			}
		}
		
		/// <summary>
		/// Indicates whether the solution component is part of a managed solution.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ismanaged")]
		public System.Nullable<bool> IsManaged
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("ismanaged");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who last modified the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ModifiedBy");
				this.SetAttributeValue("modifiedby", value);
				this.OnPropertyChanged("ModifiedBy");
			}
		}
		
		/// <summary>
		/// Date and time when the system chart was last modified.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
		public System.Nullable<System.DateTime> ModifiedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ModifiedOn");
				this.SetAttributeValue("modifiedon", value);
				this.OnPropertyChanged("ModifiedOn");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who last modified the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedOnBehalfBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedonbehalfby");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ModifiedOnBehalfBy");
				this.SetAttributeValue("modifiedonbehalfby", value);
				this.OnPropertyChanged("ModifiedOnBehalfBy");
			}
		}
		
		/// <summary>
		/// Name of the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("name")]
		public string Name
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("name");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("Name");
				this.SetAttributeValue("name", value);
				this.OnPropertyChanged("Name");
			}
		}
		
		/// <summary>
		/// Unique identifier of the organization associated with the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		public Microsoft.Xrm.Sdk.EntityReference OrganizationId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("organizationid");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overwritetime")]
		public System.Nullable<System.DateTime> OverwriteTime
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("overwritetime");
			}
		}
		
		/// <summary>
		/// XML string used to define the presentation properties of the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("presentationdescription")]
		public string PresentationDescription
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("presentationdescription");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("PresentationDescription");
				this.SetAttributeValue("presentationdescription", value);
				this.OnPropertyChanged("PresentationDescription");
			}
		}
		
		/// <summary>
		/// Type of entity with which the system chart is attached.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("primaryentitytypecode")]
		public string PrimaryEntityTypeCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("primaryentitytypecode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("PrimaryEntityTypeCode");
				this.SetAttributeValue("primaryentitytypecode", value);
				this.OnPropertyChanged("PrimaryEntityTypeCode");
			}
		}
		
		/// <summary>
		/// Unique identifier of the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("savedqueryvisualizationid")]
		public System.Nullable<System.Guid> SavedQueryVisualizationId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("savedqueryvisualizationid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("SavedQueryVisualizationId");
				this.SetAttributeValue("savedqueryvisualizationid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("SavedQueryVisualizationId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("savedqueryvisualizationid")]
		public override System.Guid Id
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return base.Id;
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.SavedQueryVisualizationId = value;
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("savedqueryvisualizationidunique")]
		public System.Nullable<System.Guid> SavedQueryVisualizationIdUnique
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("savedqueryvisualizationidunique");
			}
		}
		
		/// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("solutionid")]
		public System.Nullable<System.Guid> SolutionId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("solutionid");
			}
		}
		
		/// <summary>
		/// Specifies where the chart will be used, 0 for data centric as well as interaction centric and 1 for just interaction centric
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("type")]
		public Microsoft.Xrm.Sdk.OptionSetValue Type
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("type");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("Type");
				this.SetAttributeValue("type", value);
				this.OnPropertyChanged("Type");
			}
		}
		
		/// <summary>
		/// Version number of the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
		public System.Nullable<long> VersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<long>>("versionnumber");
			}
		}
		
		/// <summary>
		/// Unique identifier of the Web resource that will be displayed in the system chart.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("webresourceid")]
		public Microsoft.Xrm.Sdk.EntityReference WebResourceId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("webresourceid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("WebResourceId");
				this.SetAttributeValue("webresourceid", value);
				this.OnPropertyChanged("WebResourceId");
			}
		}
		
		/// <summary>
		/// 1:N userentityinstancedata_savedqueryvisualization
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("userentityinstancedata_savedqueryvisualization")]
		public System.Collections.Generic.IEnumerable<Xyz.Xrm.Entities.UserEntityInstanceData> userentityinstancedata_savedqueryvisualization
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Xyz.Xrm.Entities.UserEntityInstanceData>("userentityinstancedata_savedqueryvisualization", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("userentityinstancedata_savedqueryvisualization");
				this.SetRelatedEntities<Xyz.Xrm.Entities.UserEntityInstanceData>("userentityinstancedata_savedqueryvisualization", null, value);
				this.OnPropertyChanged("userentityinstancedata_savedqueryvisualization");
			}
		}
		
		/// <summary>
		/// N:1 lk_savedqueryvisualizationbase_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedqueryvisualizationbase_createdby")]
		public Xyz.Xrm.Entities.SystemUser lk_savedqueryvisualizationbase_createdby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_createdby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_savedqueryvisualizationbase_createdby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_createdby", null, value);
				this.OnPropertyChanged("lk_savedqueryvisualizationbase_createdby");
			}
		}
		
		/// <summary>
		/// N:1 lk_savedqueryvisualizationbase_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedqueryvisualizationbase_createdonbehalfby")]
		public Xyz.Xrm.Entities.SystemUser lk_savedqueryvisualizationbase_createdonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_createdonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_savedqueryvisualizationbase_createdonbehalfby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_savedqueryvisualizationbase_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 lk_savedqueryvisualizationbase_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedqueryvisualizationbase_modifiedby")]
		public Xyz.Xrm.Entities.SystemUser lk_savedqueryvisualizationbase_modifiedby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_modifiedby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_savedqueryvisualizationbase_modifiedby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_modifiedby", null, value);
				this.OnPropertyChanged("lk_savedqueryvisualizationbase_modifiedby");
			}
		}
		
		/// <summary>
		/// N:1 lk_savedqueryvisualizationbase_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedqueryvisualizationbase_modifiedonbehalfby")]
		public Xyz.Xrm.Entities.SystemUser lk_savedqueryvisualizationbase_modifiedonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_modifiedonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_savedqueryvisualizationbase_modifiedonbehalfby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_savedqueryvisualizationbase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_savedqueryvisualizationbase_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 organization_saved_query_visualizations
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_saved_query_visualizations")]
		public Xyz.Xrm.Entities.Organization organization_saved_query_visualizations
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.Organization>("organization_saved_query_visualizations", null);
			}
		}
		
		/// <summary>
		/// N:1 webresource_savedqueryvisualizations
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("webresourceid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("webresource_savedqueryvisualizations")]
		public Xyz.Xrm.Entities.WebResource webresource_savedqueryvisualizations
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.WebResource>("webresource_savedqueryvisualizations", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("webresource_savedqueryvisualizations");
				this.SetRelatedEntity<Xyz.Xrm.Entities.WebResource>("webresource_savedqueryvisualizations", null, value);
				this.OnPropertyChanged("webresource_savedqueryvisualizations");
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public SavedQueryVisualization(object anonymousType) : 
				this()
		{
            foreach (var p in anonymousType.GetType().GetProperties())
            {
                var value = p.GetValue(anonymousType, null);
                var name = p.Name.ToLower();
            
                if (name.EndsWith("enum") && value.GetType().BaseType == typeof(System.Enum))
                {
                    value = new Microsoft.Xrm.Sdk.OptionSetValue((int) value);
                    name = name.Remove(name.Length - "enum".Length);
                }
            
                switch (name)
                {
                    case "id":
                        base.Id = (System.Guid)value;
                        Attributes["savedqueryvisualizationid"] = base.Id;
                        break;
                    case "savedqueryvisualizationid":
                        var id = (System.Nullable<System.Guid>) value;
                        if(id == null){ continue; }
                        base.Id = id.Value;
                        Attributes[name] = base.Id;
                        break;
                    case "formattedvalues":
                        // Add Support for FormattedValues
                        FormattedValues.AddRange((Microsoft.Xrm.Sdk.FormattedValueCollection)value);
                        break;
                    default:
                        Attributes[name] = value;
                        break;
                }
            }
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("componentstate")]
		public virtual ComponentState? ComponentStateEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((ComponentState?)(EntityOptionSetEnum.GetEnum(this, "componentstate")));
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("type")]
		public virtual SavedQueryVisualization_Type? TypeEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((SavedQueryVisualization_Type?)(EntityOptionSetEnum.GetEnum(this, "type")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				Type = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
	}
}