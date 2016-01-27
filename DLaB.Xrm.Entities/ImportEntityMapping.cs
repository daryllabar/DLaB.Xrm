//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLaB.Xrm.Entities
{
	
	[System.Runtime.Serialization.DataContractAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "7.1.0001.3108")]
	public enum ImportEntityMappingState
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		Active = 0,
	}
	
	/// <summary>
	/// Mapping for entities in a data map.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("importentitymapping")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "7.1.0001.3108")]
	public partial class ImportEntityMapping : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public struct Fields
		{
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string DeDupe = "dedupe";
			public const string ImportEntityMappingId = "importentitymappingid";
			public const string Id = "importentitymappingid";
			public const string ImportMapId = "importmapid";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ProcessCode = "processcode";
			public const string SourceEntityName = "sourceentityname";
			public const string StateCode = "statecode";
			public const string StatusCode = "statuscode";
			public const string TargetEntityName = "targetentityname";
			public const string ImportEntityMapping_ImportMap = "importmapid";
			public const string lk_importentitymapping_createdby = "createdby";
			public const string lk_importentitymapping_createdonbehalfby = "createdonbehalfby";
			public const string lk_importentitymapping_modifiedby = "modifiedby";
			public const string lk_importentitymapping_modifiedonbehalfby = "modifiedonbehalfby";
		}

		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public ImportEntityMapping() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "importentitymapping";
		
		public const int EntityTypeCode = 4428;
		
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
		/// Unique identifier of the user who created the import entity mapping.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public Microsoft.Xrm.Sdk.EntityReference CreatedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("createdby");
			}
		}
		
		/// <summary>
		/// Date and time when the import entity mapping was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdon")]
		public System.Nullable<System.DateTime> CreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who created the importentitymapping.
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
		/// Information about whether the entity needs to be processed to find and delete duplicate records.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dedupe")]
		public Microsoft.Xrm.Sdk.OptionSetValue DeDupe
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("dedupe");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("DeDupe");
				this.SetAttributeValue("dedupe", value);
				this.OnPropertyChanged("DeDupe");
			}
		}
		
		/// <summary>
		/// Unique identifier of the import entity mapping.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importentitymappingid")]
		public System.Nullable<System.Guid> ImportEntityMappingId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("importentitymappingid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ImportEntityMappingId");
				this.SetAttributeValue("importentitymappingid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("ImportEntityMappingId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importentitymappingid")]
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
				this.ImportEntityMappingId = value;
			}
		}
		
		/// <summary>
		/// Unique identifier of the associated data map.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importmapid")]
		public Microsoft.Xrm.Sdk.EntityReference ImportMapId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("importmapid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ImportMapId");
				this.SetAttributeValue("importmapid", value);
				this.OnPropertyChanged("ImportMapId");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who last modified the import entity mapping.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public Microsoft.Xrm.Sdk.EntityReference ModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("modifiedby");
			}
		}
		
		/// <summary>
		/// Date and time when the import entity mapping was last modified.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedon")]
		public System.Nullable<System.DateTime> ModifiedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedon");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who last modified the importentitymapping.
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
		/// Information about whether the import entity mapping needs to be processed.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("processcode")]
		public Microsoft.Xrm.Sdk.OptionSetValue ProcessCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("processcode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ProcessCode");
				this.SetAttributeValue("processcode", value);
				this.OnPropertyChanged("ProcessCode");
			}
		}
		
		/// <summary>
		/// Name of the source entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sourceentityname")]
		public string SourceEntityName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("sourceentityname");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("SourceEntityName");
				this.SetAttributeValue("sourceentityname", value);
				this.OnPropertyChanged("SourceEntityName");
			}
		}
		
		/// <summary>
		/// Status of the import entity mapping.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statecode")]
		public System.Nullable<DLaB.Xrm.Entities.ImportEntityMappingState> StateCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				Microsoft.Xrm.Sdk.OptionSetValue optionSet = this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statecode");
				if ((optionSet != null))
				{
					return ((DLaB.Xrm.Entities.ImportEntityMappingState)(System.Enum.ToObject(typeof(DLaB.Xrm.Entities.ImportEntityMappingState), optionSet.Value)));
				}
				else
				{
					return null;
				}
			}
		}
		
		/// <summary>
		/// Reason for the status of the import entity mapping.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public Microsoft.Xrm.Sdk.OptionSetValue StatusCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("statuscode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("StatusCode");
				this.SetAttributeValue("statuscode", value);
				this.OnPropertyChanged("StatusCode");
			}
		}
		
		/// <summary>
		/// Name of the Microsoft Dynamics CRM entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("targetentityname")]
		public string TargetEntityName
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("targetentityname");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TargetEntityName");
				this.SetAttributeValue("targetentityname", value);
				this.OnPropertyChanged("TargetEntityName");
			}
		}
		
		/// <summary>
		/// 1:N userentityinstancedata_importentitymapping
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("userentityinstancedata_importentitymapping")]
		public System.Collections.Generic.IEnumerable<DLaB.Xrm.Entities.UserEntityInstanceData> userentityinstancedata_importentitymapping
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<DLaB.Xrm.Entities.UserEntityInstanceData>("userentityinstancedata_importentitymapping", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("userentityinstancedata_importentitymapping");
				this.SetRelatedEntities<DLaB.Xrm.Entities.UserEntityInstanceData>("userentityinstancedata_importentitymapping", null, value);
				this.OnPropertyChanged("userentityinstancedata_importentitymapping");
			}
		}
		
		/// <summary>
		/// N:1 ImportEntityMapping_ImportMap
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importmapid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("ImportEntityMapping_ImportMap")]
		public DLaB.Xrm.Entities.ImportMap ImportEntityMapping_ImportMap
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.ImportMap>("ImportEntityMapping_ImportMap", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ImportEntityMapping_ImportMap");
				this.SetRelatedEntity<DLaB.Xrm.Entities.ImportMap>("ImportEntityMapping_ImportMap", null, value);
				this.OnPropertyChanged("ImportEntityMapping_ImportMap");
			}
		}
		
		/// <summary>
		/// N:1 lk_importentitymapping_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importentitymapping_createdby")]
		public DLaB.Xrm.Entities.SystemUser lk_importentitymapping_createdby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_importentitymapping_createdby", null);
			}
		}
		
		/// <summary>
		/// N:1 lk_importentitymapping_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importentitymapping_createdonbehalfby")]
		public DLaB.Xrm.Entities.SystemUser lk_importentitymapping_createdonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_importentitymapping_createdonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_importentitymapping_createdonbehalfby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_importentitymapping_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_importentitymapping_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 lk_importentitymapping_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importentitymapping_modifiedby")]
		public DLaB.Xrm.Entities.SystemUser lk_importentitymapping_modifiedby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_importentitymapping_modifiedby", null);
			}
		}
		
		/// <summary>
		/// N:1 lk_importentitymapping_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importentitymapping_modifiedonbehalfby")]
		public DLaB.Xrm.Entities.SystemUser lk_importentitymapping_modifiedonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_importentitymapping_modifiedonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_importentitymapping_modifiedonbehalfby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_importentitymapping_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_importentitymapping_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public ImportEntityMapping(object anonymousType) : 
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
                        Attributes["importentitymappingid"] = base.Id;
                        break;
                    case "importentitymappingid":
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
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dedupe")]
		public virtual importentitymapping_dedupe? DeDupeEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((importentitymapping_dedupe?)(EntityOptionSetEnum.GetEnum(this, "dedupe")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				DeDupe = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("processcode")]
		public virtual importentitymapping_processcode? ProcessCodeEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((importentitymapping_processcode?)(EntityOptionSetEnum.GetEnum(this, "processcode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				ProcessCode = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public virtual importentitymapping_statuscode? StatusCodeEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((importentitymapping_statuscode?)(EntityOptionSetEnum.GetEnum(this, "statuscode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				StatusCode = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
	}
}