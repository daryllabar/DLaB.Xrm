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
	/// Configuration settings for identification of topics using text analytics.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("topicmodelconfiguration")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.0.1.7297")]
	public partial class TopicModelConfiguration : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public struct Fields
		{
			public const string ComponentState = "componentstate";
			public const string DataFilter = "datafilter";
			public const string Description = "description";
			public const string FetchXmlList = "fetchxmllist";
			public const string IsManaged = "ismanaged";
			public const string MinRelevanceScore = "minrelevancescore";
			public const string Name = "name";
			public const string NgramSize = "ngramsize";
			public const string OrganizationId = "organizationid";
			public const string OverwriteTime = "overwritetime";
			public const string SolutionId = "solutionid";
			public const string SourceEntity = "sourceentity";
			public const string StopWords = "stopwords";
			public const string TimeFilter = "timefilter";
			public const string TimeFilterDuration = "timefilterduration";
			public const string TopicModelConfigurationId = "topicmodelconfigurationid";
			public const string Id = "topicmodelconfigurationid";
			public const string TopicModelConfigurationIdUnique = "topicmodelconfigurationidunique";
			public const string TopicModelId = "topicmodelid";
			public const string organization_topicmodelconfiguration = "organization_topicmodelconfiguration";
			public const string topicmodel_topicmodelconfiguration = "topicmodel_topicmodelconfiguration";
		}

		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public TopicModelConfiguration() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "topicmodelconfiguration";
		
		public const int EntityTypeCode = 9942;
		
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
		/// Specify the data filter configured to filter records.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("datafilter")]
		public string DataFilter
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("datafilter");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("DataFilter");
				this.SetAttributeValue("datafilter", value);
				this.OnPropertyChanged("DataFilter");
			}
		}
		
		/// <summary>
		/// Enter a description for the model
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
		/// Fetch Xml
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("fetchxmllist")]
		public string FetchXmlList
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("fetchxmllist");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("FetchXmlList");
				this.SetAttributeValue("fetchxmllist", value);
				this.OnPropertyChanged("FetchXmlList");
			}
		}
		
		/// <summary>
		/// Is Manageed
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
		/// Enter the minimum relevance score of a topic.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("minrelevancescore")]
		public System.Nullable<decimal> MinRelevanceScore
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<decimal>>("minrelevancescore");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("MinRelevanceScore");
				this.SetAttributeValue("minrelevancescore", value);
				this.OnPropertyChanged("MinRelevanceScore");
			}
		}
		
		/// <summary>
		/// Type a logical name for the model.
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
		/// Enter the maximum number of key phrase words to use in a topic.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("ngramsize")]
		public System.Nullable<int> NgramSize
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("ngramsize");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("NgramSize");
				this.SetAttributeValue("ngramsize", value);
				this.OnPropertyChanged("NgramSize");
			}
		}
		
		/// <summary>
		/// Unique identifier of the organization associated with the topic model configuration.
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
		/// Date and time when the record was created.
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
		/// Type of entity with which the topic model configuration is associated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sourceentity")]
		public string SourceEntity
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("sourceentity");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("SourceEntity");
				this.SetAttributeValue("sourceentity", value);
				this.OnPropertyChanged("SourceEntity");
			}
		}
		
		/// <summary>
		/// Stop words.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("stopwords")]
		public string StopWords
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("stopwords");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("StopWords");
				this.SetAttributeValue("stopwords", value);
				this.OnPropertyChanged("StopWords");
			}
		}
		
		/// <summary>
		/// Select the time window to filter on for the last number of days or weeks.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timefilter")]
		public Microsoft.Xrm.Sdk.OptionSetValue TimeFilter
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("timefilter");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TimeFilter");
				this.SetAttributeValue("timefilter", value);
				this.OnPropertyChanged("TimeFilter");
			}
		}
		
		/// <summary>
		/// Time Filter Duration
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timefilterduration")]
		public System.Nullable<int> TimeFilterDuration
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("timefilterduration");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TimeFilterDuration");
				this.SetAttributeValue("timefilterduration", value);
				this.OnPropertyChanged("TimeFilterDuration");
			}
		}
		
		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("topicmodelconfigurationid")]
		public System.Nullable<System.Guid> TopicModelConfigurationId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("topicmodelconfigurationid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TopicModelConfigurationId");
				this.SetAttributeValue("topicmodelconfigurationid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("TopicModelConfigurationId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("topicmodelconfigurationid")]
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
				this.TopicModelConfigurationId = value;
			}
		}
		
		/// <summary>
		/// Unique identifier of the Topic Model Configuration used when synchronizing customizations for the Microsoft Dynamics CRM client for Outlook
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("topicmodelconfigurationidunique")]
		public System.Nullable<System.Guid> TopicModelConfigurationIdUnique
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("topicmodelconfigurationidunique");
			}
		}
		
		/// <summary>
		/// Unique identifier for Model associated with Topic Model Configuration.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("topicmodelid")]
		public Microsoft.Xrm.Sdk.EntityReference TopicModelId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("topicmodelid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TopicModelId");
				this.SetAttributeValue("topicmodelid", value);
				this.OnPropertyChanged("TopicModelId");
			}
		}
		
		/// <summary>
		/// 1:N topicmodelconfiguration_textanalyticsentitymapping
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("topicmodelconfiguration_textanalyticsentitymapping")]
		public System.Collections.Generic.IEnumerable<Xyz.Xrm.Entities.TextAnalyticsEntityMapping> topicmodelconfiguration_textanalyticsentitymapping
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Xyz.Xrm.Entities.TextAnalyticsEntityMapping>("topicmodelconfiguration_textanalyticsentitymapping", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("topicmodelconfiguration_textanalyticsentitymapping");
				this.SetRelatedEntities<Xyz.Xrm.Entities.TextAnalyticsEntityMapping>("topicmodelconfiguration_textanalyticsentitymapping", null, value);
				this.OnPropertyChanged("topicmodelconfiguration_textanalyticsentitymapping");
			}
		}
		
		/// <summary>
		/// 1:N topicmodelconfiguration_topicmodel
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("topicmodelconfiguration_topicmodel")]
		public System.Collections.Generic.IEnumerable<Xyz.Xrm.Entities.TopicModel> topicmodelconfiguration_topicmodel
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Xyz.Xrm.Entities.TopicModel>("topicmodelconfiguration_topicmodel", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("topicmodelconfiguration_topicmodel");
				this.SetRelatedEntities<Xyz.Xrm.Entities.TopicModel>("topicmodelconfiguration_topicmodel", null, value);
				this.OnPropertyChanged("topicmodelconfiguration_topicmodel");
			}
		}
		
		/// <summary>
		/// 1:N topicmodelconfiguration_topicmodelexecutionhistory
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("topicmodelconfiguration_topicmodelexecutionhistory")]
		public System.Collections.Generic.IEnumerable<Xyz.Xrm.Entities.TopicModelExecutionHistory> topicmodelconfiguration_topicmodelexecutionhistory
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Xyz.Xrm.Entities.TopicModelExecutionHistory>("topicmodelconfiguration_topicmodelexecutionhistory", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("topicmodelconfiguration_topicmodelexecutionhistory");
				this.SetRelatedEntities<Xyz.Xrm.Entities.TopicModelExecutionHistory>("topicmodelconfiguration_topicmodelexecutionhistory", null, value);
				this.OnPropertyChanged("topicmodelconfiguration_topicmodelexecutionhistory");
			}
		}
		
		/// <summary>
		/// N:1 organization_topicmodelconfiguration
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_topicmodelconfiguration")]
		public Xyz.Xrm.Entities.Organization organization_topicmodelconfiguration
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.Organization>("organization_topicmodelconfiguration", null);
			}
		}
		
		/// <summary>
		/// N:1 topicmodel_topicmodelconfiguration
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("topicmodelid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("topicmodel_topicmodelconfiguration")]
		public Xyz.Xrm.Entities.TopicModel topicmodel_topicmodelconfiguration
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.TopicModel>("topicmodel_topicmodelconfiguration", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("topicmodel_topicmodelconfiguration");
				this.SetRelatedEntity<Xyz.Xrm.Entities.TopicModel>("topicmodel_topicmodelconfiguration", null, value);
				this.OnPropertyChanged("topicmodel_topicmodelconfiguration");
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public TopicModelConfiguration(object anonymousType) : 
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
                        Attributes["topicmodelconfigurationid"] = base.Id;
                        break;
                    case "topicmodelconfigurationid":
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
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timefilter")]
		public virtual TopicModelConfiguration_TimeFilter? TimeFilterEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((TopicModelConfiguration_TimeFilter?)(EntityOptionSetEnum.GetEnum(this, "timefilter")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				TimeFilter = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
	}
}