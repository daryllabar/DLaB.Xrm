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
	
	/// <summary>
	/// 
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("incidentknowledgebaserecord")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "7.1.0001.3108")]
	public partial class IncidentKnowledgeBaseRecord : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public struct Fields
		{
			public const string IncidentId = "incidentid";
			public const string IncidentKnowledgeBaseRecordId = "incidentknowledgebaserecordid";
			public const string Id = "incidentknowledgebaserecordid";
			public const string KnowledgeBaseRecordId = "knowledgebaserecordid";
			public const string VersionNumber = "versionnumber";
		}

		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public IncidentKnowledgeBaseRecord() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "incidentknowledgebaserecord";
		
		public const int EntityTypeCode = 9931;
		
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
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("incidentid")]
		public System.Nullable<System.Guid> IncidentId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("incidentid");
			}
		}
		
		/// <summary>
		/// Unique identifier of the knowledgebase records for the incident.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("incidentknowledgebaserecordid")]
		public System.Nullable<System.Guid> IncidentKnowledgeBaseRecordId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("incidentknowledgebaserecordid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("IncidentKnowledgeBaseRecordId");
				this.SetAttributeValue("incidentknowledgebaserecordid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("IncidentKnowledgeBaseRecordId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("incidentknowledgebaserecordid")]
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
				this.IncidentKnowledgeBaseRecordId = value;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("knowledgebaserecordid")]
		public System.Nullable<System.Guid> KnowledgeBaseRecordId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("knowledgebaserecordid");
			}
		}
		
		/// <summary>
		/// 
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
		/// N:N KnowledgeBaseRecord_Incident
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("KnowledgeBaseRecord_Incident")]
		public System.Collections.Generic.IEnumerable<DLaB.Xrm.Entities.KnowledgeBaseRecord> KnowledgeBaseRecord_Incident
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<DLaB.Xrm.Entities.KnowledgeBaseRecord>("KnowledgeBaseRecord_Incident", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("KnowledgeBaseRecord_Incident");
				this.SetRelatedEntities<DLaB.Xrm.Entities.KnowledgeBaseRecord>("KnowledgeBaseRecord_Incident", null, value);
				this.OnPropertyChanged("KnowledgeBaseRecord_Incident");
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public IncidentKnowledgeBaseRecord(object anonymousType) : 
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
                        Attributes["incidentknowledgebaserecordid"] = base.Id;
                        break;
                    case "incidentknowledgebaserecordid":
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
	}
}