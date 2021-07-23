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
	/// Announcement associated with an organization.
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("businessunitnewsarticle")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "9.0.0.9369")]
	public partial class BusinessUnitNewsArticle : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public static class Fields
		{
			public const string ActiveOn = "activeon";
			public const string ActiveUntil = "activeuntil";
			public const string ArticleTitle = "articletitle";
			public const string ArticleTypeCode = "articletypecode";
			public const string ArticleUrl = "articleurl";
			public const string BusinessUnitNewsArticleId = "businessunitnewsarticleid";
			public const string Id = "businessunitnewsarticleid";
			public const string CreatedBy = "createdby";
			public const string CreatedOn = "createdon";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string ImportSequenceNumber = "importsequencenumber";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOn = "modifiedon";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string NewsArticle = "newsarticle";
			public const string OrganizationId = "organizationid";
			public const string OverriddenCreatedOn = "overriddencreatedon";
			public const string ShowOnHomepage = "showonhomepage";
			public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
			public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
			public const string VersionNumber = "versionnumber";
			public const string lk_businessunitnewsarticle_createdonbehalfby = "lk_businessunitnewsarticle_createdonbehalfby";
			public const string lk_businessunitnewsarticle_modifiedonbehalfby = "lk_businessunitnewsarticle_modifiedonbehalfby";
			public const string lk_businessunitnewsarticlebase_createdby = "lk_businessunitnewsarticlebase_createdby";
			public const string lk_businessunitnewsarticlebase_modifiedby = "lk_businessunitnewsarticlebase_modifiedby";
			public const string organization_business_unit_news_articles = "organization_business_unit_news_articles";
		}
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public BusinessUnitNewsArticle() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "businessunitnewsarticle";
		
		public const string PrimaryIdAttribute = "businessunitnewsarticleid";
		
		public const string PrimaryNameAttribute = "articletitle";
		
		public const int EntityTypeCode = 132;
		
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
		/// Date and time for the announcement to become active.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("activeon")]
		public System.Nullable<System.DateTime> ActiveOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("activeon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ActiveOn");
				this.SetAttributeValue("activeon", value);
				this.OnPropertyChanged("ActiveOn");
			}
		}
		
		/// <summary>
		/// Date and time of the last day the announcement is active.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("activeuntil")]
		public System.Nullable<System.DateTime> ActiveUntil
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("activeuntil");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ActiveUntil");
				this.SetAttributeValue("activeuntil", value);
				this.OnPropertyChanged("ActiveUntil");
			}
		}
		
		/// <summary>
		/// Title of the announcement.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("articletitle")]
		public string ArticleTitle
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("articletitle");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ArticleTitle");
				this.SetAttributeValue("articletitle", value);
				this.OnPropertyChanged("ArticleTitle");
			}
		}
		
		/// <summary>
		/// Type of announcement.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("articletypecode")]
		public Microsoft.Xrm.Sdk.OptionSetValue ArticleTypeCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("articletypecode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ArticleTypeCode");
				this.SetAttributeValue("articletypecode", value);
				this.OnPropertyChanged("ArticleTypeCode");
			}
		}
		
		/// <summary>
		/// URL for the Website on which the announcement is located.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("articleurl")]
		public string ArticleUrl
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("articleurl");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ArticleUrl");
				this.SetAttributeValue("articleurl", value);
				this.OnPropertyChanged("ArticleUrl");
			}
		}
		
		/// <summary>
		/// Unique identifier of the announcement.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("businessunitnewsarticleid")]
		public System.Nullable<System.Guid> BusinessUnitNewsArticleId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("businessunitnewsarticleid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("BusinessUnitNewsArticleId");
				this.SetAttributeValue("businessunitnewsarticleid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("BusinessUnitNewsArticleId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("businessunitnewsarticleid")]
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
				this.BusinessUnitNewsArticleId = value;
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who created the announcement.
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
		/// Date and time when the announcement was created.
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
		/// Unique identifier of the delegate user who created the businessunitnewsarticle.
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
		/// Unique identifier of the data import or data migration that created this record.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("importsequencenumber")]
		public System.Nullable<int> ImportSequenceNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("importsequencenumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ImportSequenceNumber");
				this.SetAttributeValue("importsequencenumber", value);
				this.OnPropertyChanged("ImportSequenceNumber");
			}
		}
		
		/// <summary>
		/// Unique identifier of the user who last modified the announcement.
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
		/// Date and time when the announcement was last modified.
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
		/// Unique identifier of the delegate user who last modified the businessunitnewsarticle.
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
		/// Text for the announcement.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("newsarticle")]
		public string NewsArticle
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("newsarticle");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("NewsArticle");
				this.SetAttributeValue("newsarticle", value);
				this.OnPropertyChanged("NewsArticle");
			}
		}
		
		/// <summary>
		/// Unique identifier of the organization associated with the announcement.
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
		/// Date and time that the record was migrated.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("overriddencreatedon")]
		public System.Nullable<System.DateTime> OverriddenCreatedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("overriddencreatedon");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("OverriddenCreatedOn");
				this.SetAttributeValue("overriddencreatedon", value);
				this.OnPropertyChanged("OverriddenCreatedOn");
			}
		}
		
		/// <summary>
		/// Information about whether to show the announcement on the Website home page.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("showonhomepage")]
		public System.Nullable<bool> ShowOnHomepage
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<bool>>("showonhomepage");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("ShowOnHomepage");
				this.SetAttributeValue("showonhomepage", value);
				this.OnPropertyChanged("ShowOnHomepage");
			}
		}
		
		/// <summary>
		/// For internal use only.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("timezoneruleversionnumber")]
		public System.Nullable<int> TimeZoneRuleVersionNumber
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("timezoneruleversionnumber");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TimeZoneRuleVersionNumber");
				this.SetAttributeValue("timezoneruleversionnumber", value);
				this.OnPropertyChanged("TimeZoneRuleVersionNumber");
			}
		}
		
		/// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("utcconversiontimezonecode")]
		public System.Nullable<int> UTCConversionTimeZoneCode
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("utcconversiontimezonecode");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("UTCConversionTimeZoneCode");
				this.SetAttributeValue("utcconversiontimezonecode", value);
				this.OnPropertyChanged("UTCConversionTimeZoneCode");
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
		/// 1:N BusinessUnitNewsArticle_AsyncOperations
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("BusinessUnitNewsArticle_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<DLaB.Xrm.Entities.AsyncOperation> BusinessUnitNewsArticle_AsyncOperations
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<DLaB.Xrm.Entities.AsyncOperation>("BusinessUnitNewsArticle_AsyncOperations", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("BusinessUnitNewsArticle_AsyncOperations");
				this.SetRelatedEntities<DLaB.Xrm.Entities.AsyncOperation>("BusinessUnitNewsArticle_AsyncOperations", null, value);
				this.OnPropertyChanged("BusinessUnitNewsArticle_AsyncOperations");
			}
		}
		
		/// <summary>
		/// 1:N BusinessUnitNewsArticle_BulkDeleteFailures
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("BusinessUnitNewsArticle_BulkDeleteFailures")]
		public System.Collections.Generic.IEnumerable<DLaB.Xrm.Entities.BulkDeleteFailure> BusinessUnitNewsArticle_BulkDeleteFailures
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<DLaB.Xrm.Entities.BulkDeleteFailure>("BusinessUnitNewsArticle_BulkDeleteFailures", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("BusinessUnitNewsArticle_BulkDeleteFailures");
				this.SetRelatedEntities<DLaB.Xrm.Entities.BulkDeleteFailure>("BusinessUnitNewsArticle_BulkDeleteFailures", null, value);
				this.OnPropertyChanged("BusinessUnitNewsArticle_BulkDeleteFailures");
			}
		}
		
		/// <summary>
		/// 1:N BusinessUnitNewsArticle_ProcessSessions
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("BusinessUnitNewsArticle_ProcessSessions")]
		public System.Collections.Generic.IEnumerable<DLaB.Xrm.Entities.ProcessSession> BusinessUnitNewsArticle_ProcessSessions
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<DLaB.Xrm.Entities.ProcessSession>("BusinessUnitNewsArticle_ProcessSessions", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("BusinessUnitNewsArticle_ProcessSessions");
				this.SetRelatedEntities<DLaB.Xrm.Entities.ProcessSession>("BusinessUnitNewsArticle_ProcessSessions", null, value);
				this.OnPropertyChanged("BusinessUnitNewsArticle_ProcessSessions");
			}
		}
		
		/// <summary>
		/// 1:N userentityinstancedata_businessunitnewsarticle
		/// </summary>
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("userentityinstancedata_businessunitnewsarticle")]
		public System.Collections.Generic.IEnumerable<DLaB.Xrm.Entities.UserEntityInstanceData> userentityinstancedata_businessunitnewsarticle
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntities<DLaB.Xrm.Entities.UserEntityInstanceData>("userentityinstancedata_businessunitnewsarticle", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("userentityinstancedata_businessunitnewsarticle");
				this.SetRelatedEntities<DLaB.Xrm.Entities.UserEntityInstanceData>("userentityinstancedata_businessunitnewsarticle", null, value);
				this.OnPropertyChanged("userentityinstancedata_businessunitnewsarticle");
			}
		}
		
		/// <summary>
		/// N:1 lk_businessunitnewsarticle_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitnewsarticle_createdonbehalfby")]
		public DLaB.Xrm.Entities.SystemUser lk_businessunitnewsarticle_createdonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticle_createdonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_businessunitnewsarticle_createdonbehalfby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticle_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_businessunitnewsarticle_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 lk_businessunitnewsarticle_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitnewsarticle_modifiedonbehalfby")]
		public DLaB.Xrm.Entities.SystemUser lk_businessunitnewsarticle_modifiedonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticle_modifiedonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_businessunitnewsarticle_modifiedonbehalfby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticle_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_businessunitnewsarticle_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 lk_businessunitnewsarticlebase_createdby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitnewsarticlebase_createdby")]
		public DLaB.Xrm.Entities.SystemUser lk_businessunitnewsarticlebase_createdby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticlebase_createdby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_businessunitnewsarticlebase_createdby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticlebase_createdby", null, value);
				this.OnPropertyChanged("lk_businessunitnewsarticlebase_createdby");
			}
		}
		
		/// <summary>
		/// N:1 lk_businessunitnewsarticlebase_modifiedby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitnewsarticlebase_modifiedby")]
		public DLaB.Xrm.Entities.SystemUser lk_businessunitnewsarticlebase_modifiedby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticlebase_modifiedby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_businessunitnewsarticlebase_modifiedby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_businessunitnewsarticlebase_modifiedby", null, value);
				this.OnPropertyChanged("lk_businessunitnewsarticlebase_modifiedby");
			}
		}
		
		/// <summary>
		/// N:1 organization_business_unit_news_articles
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_business_unit_news_articles")]
		public DLaB.Xrm.Entities.Organization organization_business_unit_news_articles
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.Organization>("organization_business_unit_news_articles", null);
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public BusinessUnitNewsArticle(object anonymousType) : 
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
                        Attributes["businessunitnewsarticleid"] = base.Id;
                        break;
                    case "businessunitnewsarticleid":
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
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("articletypecode")]
		public virtual BusinessUnitNewsArticle_ArticleTypeCode? ArticleTypeCodeEnum
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return ((BusinessUnitNewsArticle_ArticleTypeCode?)(EntityOptionSetEnum.GetEnum(this, "articletypecode")));
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				ArticleTypeCode = value.HasValue ? new Microsoft.Xrm.Sdk.OptionSetValue((int)value) : null;
			}
		}
	}
}