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
	/// Office Graph Documents Description
	/// </summary>
	[System.Runtime.Serialization.DataContractAttribute()]
	[Microsoft.Xrm.Sdk.Client.EntityLogicalNameAttribute("officegraphdocument")]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("CrmSvcUtil", "7.1.0001.3108")]
	public partial class OfficeGraphDocument : Microsoft.Xrm.Sdk.Entity, System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		public struct Fields
		{
			public const string AuthorNames = "authornames";
			public const string CreatedBy = "createdby";
			public const string CreatedOnBehalfBy = "createdonbehalfby";
			public const string CreatedTime = "createdtime";
			public const string DocumentId = "documentid";
			public const string DocumentLastModifiedBy = "documentlastmodifiedby";
			public const string DocumentLastModifiedOn = "documentlastmodifiedon";
			public const string DocumentPreviewMetadata = "documentpreviewmetadata";
			public const string ExchangeRate = "exchangerate";
			public const string FileExtension = "fileextension";
			public const string FileType = "filetype";
			public const string ModifiedBy = "modifiedby";
			public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
			public const string ModifiedTime = "modifiedtime";
			public const string OfficeGraphDocumentId = "officegraphdocumentid";
			public const string Id = "officegraphdocumentid";
			public const string OrganizationId = "organizationid";
			public const string PreviewImageUrl = "previewimageurl";
			public const string QueryType = "querytype";
			public const string Rank = "rank";
			public const string ReadUrl = "readurl";
			public const string SecondaryFileExtension = "secondaryfileextension";
			public const string SiteTitle = "sitetitle";
			public const string SiteUrl = "siteurl";
			public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
			public const string Title = "title";
			public const string TransactionCurrencyId = "transactioncurrencyid";
			public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
			public const string VersionNumber = "versionnumber";
			public const string ViewCount = "viewcount";
			public const string WebLocationUrl = "weblocationurl";
			public const string lk_officegraphdocument_createdonbehalfby = "createdonbehalfby";
			public const string lk_officegraphdocument_modifiedonbehalfby = "modifiedonbehalfby";
			public const string organization_officegraphdocument = "organizationid";
			public const string TransactionCurrency_officegraphdocument = "transactioncurrencyid";
		}

		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public OfficeGraphDocument() : 
				base(EntityLogicalName)
		{
		}
		
		public const string EntityLogicalName = "officegraphdocument";
		
		public const int EntityTypeCode = 9950;
		
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
		/// Shows Author Names of Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("authornames")]
		public string AuthorNames
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("authornames");
			}
		}
		
		/// <summary>
		/// Shows Created By of Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdby")]
		public string CreatedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("createdby");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who created the record.
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
		/// Date and time when the record was created.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdtime")]
		public System.Nullable<System.DateTime> CreatedTime
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("createdtime");
			}
		}
		
		/// <summary>
		/// Document Id.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("documentid")]
		public string DocumentId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("documentid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("DocumentId");
				this.SetAttributeValue("documentid", value);
				this.OnPropertyChanged("DocumentId");
			}
		}
		
		/// <summary>
		/// Document Last Modified By
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("documentlastmodifiedby")]
		public string DocumentLastModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("documentlastmodifiedby");
			}
		}
		
		/// <summary>
		/// Document Last Modified On
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("documentlastmodifiedon")]
		public System.Nullable<System.DateTime> DocumentLastModifiedOn
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("documentlastmodifiedon");
			}
		}
		
		/// <summary>
		/// document preview metadata
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("documentpreviewmetadata")]
		public string DocumentPreviewMetadata
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("documentpreviewmetadata");
			}
		}
		
		/// <summary>
		/// Exchange rate for the currency associated with the Office Graph Document with respect to the base currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("exchangerate")]
		public System.Nullable<decimal> ExchangeRate
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<decimal>>("exchangerate");
			}
		}
		
		/// <summary>
		/// File Extension of Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("fileextension")]
		public string FileExtension
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("fileextension");
			}
		}
		
		/// <summary>
		/// Shows the File Type of Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("filetype")]
		public string FileType
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("filetype");
			}
		}
		
		/// <summary>
		/// Shows modified by of Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedby")]
		public string ModifiedBy
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("modifiedby");
			}
		}
		
		/// <summary>
		/// Unique identifier of the delegate user who modified the record.
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
		/// Date and time when the record was modified.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedtime")]
		public System.Nullable<System.DateTime> ModifiedTime
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.DateTime>>("modifiedtime");
			}
		}
		
		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("officegraphdocumentid")]
		public System.Nullable<System.Guid> OfficeGraphDocumentId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<System.Guid>>("officegraphdocumentid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("OfficeGraphDocumentId");
				this.SetAttributeValue("officegraphdocumentid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
				this.OnPropertyChanged("OfficeGraphDocumentId");
			}
		}
		
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("officegraphdocumentid")]
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
				this.OfficeGraphDocumentId = value;
			}
		}
		
		/// <summary>
		/// Unique identifier for the organization
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
		/// Shows the Preview Image Url Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("previewimageurl")]
		public string PreviewImageUrl
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("previewimageurl");
			}
		}
		
		/// <summary>
		/// Shows Query Type of child folders
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("querytype")]
		public System.Nullable<int> QueryType
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("querytype");
			}
		}
		
		/// <summary>
		/// The relevancy rank of the document retrieved
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("rank")]
		public System.Nullable<int> Rank
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("rank");
			}
		}
		
		/// <summary>
		/// The online read url
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("readurl")]
		public string ReadUrl
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("readurl");
			}
		}
		
		/// <summary>
		/// Secondary File Extension of Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("secondaryfileextension")]
		public string SecondaryFileExtension
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("secondaryfileextension");
			}
		}
		
		/// <summary>
		/// The title of the parent document site
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("sitetitle")]
		public string SiteTitle
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("sitetitle");
			}
		}
		
		/// <summary>
		/// The site url for the parent document site
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("siteurl")]
		public string SiteUrl
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("siteurl");
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
		/// The title of the entity.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("title")]
		public string Title
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("title");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("Title");
				this.SetAttributeValue("title", value);
				this.OnPropertyChanged("Title");
			}
		}
		
		/// <summary>
		/// Exchange rate for the currency associated with the Office Graph Document with respect to the base currency.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("transactioncurrencyid")]
		public Microsoft.Xrm.Sdk.EntityReference TransactionCurrencyId
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("transactioncurrencyid");
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TransactionCurrencyId");
				this.SetAttributeValue("transactioncurrencyid", value);
				this.OnPropertyChanged("TransactionCurrencyId");
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
		/// Shows View Count of child folders.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("viewcount")]
		public System.Nullable<int> ViewCount
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<System.Nullable<int>>("viewcount");
			}
		}
		
		/// <summary>
		/// Shows the Web Location Url of Office Graph Document.
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("weblocationurl")]
		public string WebLocationUrl
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetAttributeValue<string>("weblocationurl");
			}
		}
		
		/// <summary>
		/// N:1 lk_officegraphdocument_createdonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("createdonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_officegraphdocument_createdonbehalfby")]
		public DLaB.Xrm.Entities.SystemUser lk_officegraphdocument_createdonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_officegraphdocument_createdonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_officegraphdocument_createdonbehalfby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_officegraphdocument_createdonbehalfby", null, value);
				this.OnPropertyChanged("lk_officegraphdocument_createdonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 lk_officegraphdocument_modifiedonbehalfby
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("modifiedonbehalfby")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_officegraphdocument_modifiedonbehalfby")]
		public DLaB.Xrm.Entities.SystemUser lk_officegraphdocument_modifiedonbehalfby
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_officegraphdocument_modifiedonbehalfby", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("lk_officegraphdocument_modifiedonbehalfby");
				this.SetRelatedEntity<DLaB.Xrm.Entities.SystemUser>("lk_officegraphdocument_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("lk_officegraphdocument_modifiedonbehalfby");
			}
		}
		
		/// <summary>
		/// N:1 organization_officegraphdocument
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("organizationid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("organization_officegraphdocument")]
		public DLaB.Xrm.Entities.Organization organization_officegraphdocument
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.Organization>("organization_officegraphdocument", null);
			}
		}
		
		/// <summary>
		/// N:1 TransactionCurrency_officegraphdocument
		/// </summary>
		[Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("transactioncurrencyid")]
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("TransactionCurrency_officegraphdocument")]
		public DLaB.Xrm.Entities.TransactionCurrency TransactionCurrency_officegraphdocument
		{
			[System.Diagnostics.DebuggerNonUserCode()]
			get
			{
				return this.GetRelatedEntity<DLaB.Xrm.Entities.TransactionCurrency>("TransactionCurrency_officegraphdocument", null);
			}
			[System.Diagnostics.DebuggerNonUserCode()]
			set
			{
				this.OnPropertyChanging("TransactionCurrency_officegraphdocument");
				this.SetRelatedEntity<DLaB.Xrm.Entities.TransactionCurrency>("TransactionCurrency_officegraphdocument", null, value);
				this.OnPropertyChanged("TransactionCurrency_officegraphdocument");
			}
		}
		
		/// <summary>
		/// Constructor for populating via LINQ queries given a LINQ anonymous type
		/// <param name="anonymousType">LINQ anonymous type.</param>
		/// </summary>
		[System.Diagnostics.DebuggerNonUserCode()]
		public OfficeGraphDocument(object anonymousType) : 
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
                        Attributes["officegraphdocumentid"] = base.Id;
                        break;
                    case "officegraphdocumentid":
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