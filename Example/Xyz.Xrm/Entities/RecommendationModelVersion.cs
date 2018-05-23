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
	/// The product recommendation model version that's built using the Azure recommendation service.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("recommendationmodelversion")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "8.0.1.7297")]
	public partial class RecommendationModelVersion : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public struct Fields
		{
			public const string AzureBuildId = "azurebuildid";
			public const string AzureModelBuildStatus = "azuremodelbuildstatus";
			public const string BasketDataSynchronizationStatus = "basketdatasynchronizationstatus";
			public const string BuildEndedOn = "buildendedon";
			public const string BuildStartedOn = "buildstartedon";
			public const string CatalogCoverage = "catalogcoverage";
			public const string CatalogSynchronizationStatus = "catalogsynchronizationstatus";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string Description = "description";
			public const string Duration = "duration";
			public const string LogicAppRunId = "logicapprunid";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string Name = "name";
			public const string OrganizationId = "organizationid";
			public const string PercentileRank = "percentilerank";
			public const string RecommendationModelId = "recommendationmodelid";
			public const string RecommendationModelVersionId = "recommendationmodelversionid";
			public const string Id = "recommendationmodelversionid";
			public const string StatusCode = "statuscode";
			public const string lk_recommendationmodelversion_createdby = "lk_recommendationmodelversion_createdby";
			public const string lk_recommendationmodelversion_createdonbehalfby = "lk_recommendationmodelversion_createdonbehalfby";
			public const string lk_recommendationmodelversion_modifiedby = "lk_recommendationmodelversion_modifiedby";
			public const string lk_recommendationmodelversion_modifiedonbehalfby = "lk_recommendationmodelversion_modifiedonbehalfby";
			public const string organization_recommendationmodelversion = "organization_recommendationmodelversion";
			public const string recommendationmodel_recommendationmodelversion = "recommendationmodel_recommendationmodelversion";
		}

		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public RecommendationModelVersion() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "recommendationmodelversion";
		
		public const int EntityTypeCode = 9935;
		
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
		/// Shows the Azure Bbuild IdID.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("azurebuildid")]
		public string AzureBuildId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("azurebuildid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("AzureBuildId");
				this.SetAttributeValue("azurebuildid", value);
				this.OnPropertyChanged("AzureBuildId");
			}
		}
		
		/// <summary>
		/// Shows the status of the model build in Azure.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("azuremodelbuildstatus")]
		public Microsoft.Xrm.Sdk.OptionSetValue AzureModelBuildStatus
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("azuremodelbuildstatus");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("AzureModelBuildStatus");
				this.SetAttributeValue("azuremodelbuildstatus", value);
				this.OnPropertyChanged("AzureModelBuildStatus");
			}
		}
		
		/// <summary>
		/// Shows the status of the basket data synchronization, for example, if it hasn't started, or it's in progress.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("basketdatasynchronizationstatus")]
		public Microsoft.Xrm.Sdk.OptionSetValue BasketDataSynchronizationStatus
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("basketdatasynchronizationstatus");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("BasketDataSynchronizationStatus");
				this.SetAttributeValue("basketdatasynchronizationstatus", value);
				this.OnPropertyChanged("BasketDataSynchronizationStatus");
			}
		}
		
		/// <summary>
		/// Shows the time when the model build was successfully completed.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("buildendedon")]
		public System.Nullable<System.DateTime> BuildEndedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("buildendedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("BuildEndedOn");
				this.SetAttributeValue("buildendedon", value);
				this.OnPropertyChanged("BuildEndedOn");
			}
		}
		
		/// <summary>
		/// Shows Tthe time when the model build was started.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("buildstartedon")]
		public System.Nullable<System.DateTime> BuildStartedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("buildstartedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("BuildStartedOn");
				this.SetAttributeValue("buildstartedon", value);
				this.OnPropertyChanged("BuildStartedOn");
			}
		}
		
		/// <summary>
		/// Shows an estimate of how good the data used for model building covers relationships between products. A higher number is better.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("catalogcoverage")]
		public System.Nullable<int> CatalogCoverage
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("catalogcoverage");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("CatalogCoverage");
				this.SetAttributeValue("catalogcoverage", value);
				this.OnPropertyChanged("CatalogCoverage");
			}
		}
		
		/// <summary>
		/// Shows the catalog synchronization status.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("catalogsynchronizationstatus")]
		public Microsoft.Xrm.Sdk.OptionSetValue CatalogSynchronizationStatus
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("catalogsynchronizationstatus");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("CatalogSynchronizationStatus");
				this.SetAttributeValue("catalogsynchronizationstatus", value);
				this.OnPropertyChanged("CatalogSynchronizationStatus");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who created the recommendation model version.
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
		/// Date and time when the recommendation model version was created.
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
		/// Unique identifier of the delegate user who created the recommendation model version.
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
		/// Type a meaningful description for the recommendation model version.
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
		/// Shows how long, in minutes, it took to finish the build.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("duration")]
		public System.Nullable<int> Duration
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("duration");
			}
		}
		
		/// <summary>
		/// Shows the name of the Azure Logic App instance triggered by the model version.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("logicapprunid")]
		public string LogicAppRunId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("logicapprunid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("LogicAppRunId");
				this.SetAttributeValue("logicapprunid", value);
				this.OnPropertyChanged("LogicAppRunId");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who modified the recommendation model version.
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
		/// Date and time when the recommendation model version was last modified.
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
		/// Unique identifier of the delegate user who last modified the recommendation model version.
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
		/// Type a meaningful name for the recommendation model version.
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
		/// Unique identifier of the organization associated with the recommendation model version.
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
		/// Shows an estimate of top percentile rank that the tested recommendations fall in. A lower number is better.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("percentilerank")]
		public System.Nullable<int> PercentileRank
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("percentilerank");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("PercentileRank");
				this.SetAttributeValue("percentilerank", value);
				this.OnPropertyChanged("PercentileRank");
			}
		}
		
		/// <summary>
		/// Unique identifier for the model associated with the model version.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("recommendationmodelid")]
		public Microsoft.Xrm.Sdk.EntityReference RecommendationModelId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("recommendationmodelid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("RecommendationModelId");
				this.SetAttributeValue("recommendationmodelid", value);
				this.OnPropertyChanged("RecommendationModelId");
			}
		}
		
		/// <summary>
		/// Unique identifier of the recommendation model version.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("recommendationmodelversionid")]
		public System.Nullable<System.Guid> RecommendationModelVersionId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("recommendationmodelversionid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("RecommendationModelVersionId");
				this.SetAttributeValue("recommendationmodelversionid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("RecommendationModelVersionId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("recommendationmodelversionid")]
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
				this.RecommendationModelVersionId = value;
			}
		}
		
		/// <summary>
		/// Shows the status of the model version.
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
		/// 1:N recommendationmodelversion_recommendationmodel
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("recommendationmodelversion_recommendationmodel")]
		public System.Collections.Generic.IEnumerable<Xyz.Xrm.Entities.RecommendationModel> recommendationmodelversion_recommendationmodel
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Xyz.Xrm.Entities.RecommendationModel>("recommendationmodelversion_recommendationmodel", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("recommendationmodelversion_recommendationmodel");
				this.SetRelatedEntities<Xyz.Xrm.Entities.RecommendationModel>("recommendationmodelversion_recommendationmodel", null, value);
				this.OnPropertyChanged("recommendationmodelversion_recommendationmodel");
			}
		}
		
		/// <summary>
		/// 1:N recommendationmodelversion_recommendationmodelmapping
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("recommendationmodelversion_recommendationmodelmapping")]
		public System.Collections.Generic.IEnumerable<Xyz.Xrm.Entities.RecommendationModelMapping> recommendationmodelversion_recommendationmodelmapping
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Xyz.Xrm.Entities.RecommendationModelMapping>("recommendationmodelversion_recommendationmodelmapping", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("recommendationmodelversion_recommendationmodelmapping");
				this.SetRelatedEntities<Xyz.Xrm.Entities.RecommendationModelMapping>("recommendationmodelversion_recommendationmodelmapping", null, value);
				this.OnPropertyChanged("recommendationmodelversion_recommendationmodelmapping");
			}
		}
		
		/// <summary>
		/// 1:N recommendationmodelversion_recommendationmodelversionhistory
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("recommendationmodelversion_recommendationmodelversionhistory")]
		public System.Collections.Generic.IEnumerable<Xyz.Xrm.Entities.RecommendationModelVersionHistory> recommendationmodelversion_recommendationmodelversionhistory
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<Xyz.Xrm.Entities.RecommendationModelVersionHistory>("recommendationmodelversion_recommendationmodelversionhistory", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("recommendationmodelversion_recommendationmodelversionhistory");
				this.SetRelatedEntities<Xyz.Xrm.Entities.RecommendationModelVersionHistory>("recommendationmodelversion_recommendationmodelversionhistory", null, value);
				this.OnPropertyChanged("recommendationmodelversion_recommendationmodelversionhistory");
			}
		}
		
		/// <summary>
		/// N:1 lk_recommendationmodelversion_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_recommendationmodelversion_createdby")]
		public Xyz.Xrm.Entities.SystemUser lk_recommendationmodelversion_createdby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_createdby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_recommendationmodelversion_createdby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_createdby", null, value);
				this.OnPropertyChanged("lk_recommendationmodelversion_createdby");
			}
		}
		
		/// <summary>
		/// N:1 lk_recommendationmodelversion_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_recommendationmodelversion_createdonbehalfby")]
		public Xyz.Xrm.Entities.SystemUser lk_recommendationmodelversion_createdonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_createdonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_recommendationmodelversion_createdonbehalfby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_recommendationmodelversion_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 lk_recommendationmodelversion_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_recommendationmodelversion_modifiedby")]
		public Xyz.Xrm.Entities.SystemUser lk_recommendationmodelversion_modifiedby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_modifiedby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_recommendationmodelversion_modifiedby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_modifiedby", null, value);
				this.OnPropertyChanged("lk_recommendationmodelversion_modifiedby");
			}
		}
		
		/// <summary>
		/// N:1 lk_recommendationmodelversion_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_recommendationmodelversion_modifiedonbehalfby")]
		public Xyz.Xrm.Entities.SystemUser lk_recommendationmodelversion_modifiedonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_modifiedonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_recommendationmodelversion_modifiedonbehalfby");
				this.SetRelatedEntity<Xyz.Xrm.Entities.SystemUser>("lk_recommendationmodelversion_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_recommendationmodelversion_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 organization_recommendationmodelversion
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_recommendationmodelversion")]
		public Xyz.Xrm.Entities.Organization organization_recommendationmodelversion
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.Organization>("organization_recommendationmodelversion", null);
			}
		}
		
		/// <summary>
		/// N:1 recommendationmodel_recommendationmodelversion
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("recommendationmodelid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("recommendationmodel_recommendationmodelversion")]
		public Xyz.Xrm.Entities.RecommendationModel recommendationmodel_recommendationmodelversion
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<Xyz.Xrm.Entities.RecommendationModel>("recommendationmodel_recommendationmodelversion", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("recommendationmodel_recommendationmodelversion");
				this.SetRelatedEntity<Xyz.Xrm.Entities.RecommendationModel>("recommendationmodel_recommendationmodelversion", null, value);
				this.OnPropertyChanged("recommendationmodel_recommendationmodelversion");
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public RecommendationModelVersion(object anonymousType) : 
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
                        Attributes["recommendationmodelversionid"] = base.Id;
                        break;
                    case "recommendationmodelversionid":
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
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("azuremodelbuildstatus")]
		public virtual RecommendationModelVersion_AzureModelBuildStatus? AzureModelBuildStatusEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((RecommendationModelVersion_AzureModelBuildStatus?)(EntityOptionSetEnum.GetEnum(this, "azuremodelbuildstatus")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				AzureModelBuildStatus = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("basketdatasynchronizationstatus")]
		public virtual recommendationmodelversion_SynchronizationStatus? BasketDataSynchronizationStatusEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((recommendationmodelversion_SynchronizationStatus?)(EntityOptionSetEnum.GetEnum(this, "basketdatasynchronizationstatus")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				BasketDataSynchronizationStatus = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("catalogsynchronizationstatus")]
		public virtual recommendationmodelversion_SynchronizationStatus? CatalogSynchronizationStatusEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((recommendationmodelversion_SynchronizationStatus?)(EntityOptionSetEnum.GetEnum(this, "catalogsynchronizationstatus")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				CatalogSynchronizationStatus = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("statuscode")]
		public virtual RecommendationModelVersion_StatusCode? StatusCodeEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((RecommendationModelVersion_StatusCode?)(EntityOptionSetEnum.GetEnum(this, "statuscode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				StatusCode = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
	}
}