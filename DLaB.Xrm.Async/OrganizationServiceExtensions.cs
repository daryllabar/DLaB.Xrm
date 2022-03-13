using System.Linq.Expressions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;

#if DLAB_UNROOT_COMMON_NAMESPACE
using DLaB.Common;
#else
using Source.DLaB.Common;
#endif

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm
#else
namespace Source.DLaB.Xrm
#endif
{
    public static partial class Extensions
    {
        #region AssignAsync

        /// <summary>
        /// Assigns the supplied entity to the supplied user
        /// </summary>
        /// <param name="service"></param>
        /// <param name="target"></param>
        /// <param name="newOwner"></param>
        /// <param name="token"></param>
        public static Task AssignAsync(this IOrganizationServiceAsync2 service, EntityReference target, EntityReference newOwner, CancellationToken token = default)
        {
            return service.UpdateAsync(new Entity(target.LogicalName)
            {
                Id = target.Id,
                ["ownerid"] = newOwner
            }, token);
        }

        /// <summary>
        /// Reassigns the owner of the entity to the new owner
        /// </summary>
        /// <param name="service"></param>
        /// <param name="itemToChangeOwnershipOf">Must have Logical Name and Id Populated</param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        public static Task AssignAsync(this IOrganizationServiceAsync2 service, Entity itemToChangeOwnershipOf, Guid userId, CancellationToken token = default)
        {
            return service.AssignAsync(itemToChangeOwnershipOf.ToEntityReference(), new EntityReference("systemuser", userId), token);
        }

        /// <summary>
        /// Reassigns the owner of the entity to the new owner
        /// </summary>
        /// <param name="service"></param>
        /// <param name="itemToChangeOwnershipOf">Must have Logical Name and Id Populated</param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        public static Task AssignAsync(this IOrganizationServiceAsync2 service, EntityReference itemToChangeOwnershipOf, Guid userId, CancellationToken token = default)
        {
            return service.AssignAsync(itemToChangeOwnershipOf, new EntityReference("systemuser", userId), token);
        }


        /// <summary>
        /// Reassigns the owner of the entity to the new owner
        /// </summary>
        /// <param name="service"></param>
        /// <param name="itemToChangeOwnershipOf">Must have Logical Name and Id Populated</param>
        /// <param name="teamId"></param>
        /// <param name="token"></param>
        public static Task AssignTeamAsync(this IOrganizationServiceAsync2 service, Entity itemToChangeOwnershipOf, Guid teamId, CancellationToken token = default)
        {
            return service.AssignAsync(itemToChangeOwnershipOf.ToEntityReference(), new EntityReference("team", teamId), token);
        }

        /// <summary>
        /// Reassigns the owner of the entity to the new owner
        /// </summary>
        /// <param name="service"></param>
        /// <param name="itemToChangeOwnershipOf">Must have Logical Name and Id Populated</param>
        /// <param name="teamId"></param>
        /// <param name="token"></param>
        public static Task AssignTeamAsync(this IOrganizationServiceAsync2 service, EntityReference itemToChangeOwnershipOf, Guid teamId, CancellationToken token = default)
        {
            return service.AssignAsync(itemToChangeOwnershipOf, new EntityReference("team", teamId), token);
        }

        #endregion AssignAsync

        #region AssociateAsync

        /// <summary>
        /// Associates one or more entities to an entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity"></param>
        /// <param name="relationshipLogicalName"></param>
        /// <param name="token"></param>
        /// <param name="entities"></param>
        public static Task AssociateAsync(this IOrganizationServiceAsync2 service, Entity entity, string relationshipLogicalName,
                CancellationToken token = default, params Entity[] entities)
        {
            var relationship = new Relationship(relationshipLogicalName);
            if (entity.LogicalName == entities.First().LogicalName)
            {
                relationship.PrimaryEntityRole = EntityRole.Referenced;
            }

            return service.AssociateAsync(entity.LogicalName, entity.Id,
                relationship,
                new EntityReferenceCollection(entities.Select(e => e.ToEntityReference()).ToList()), token);
        }

        /// <summary>
        /// Associates one or more entities to an entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity"></param>
        /// <param name="relationshipLogicalName"></param>
        /// <param name="entities"></param>
        public static Task AssociateAsync(this IOrganizationServiceAsync2 service, Entity entity, string relationshipLogicalName, params Entity[] entities)
        {
            return service.AssociateAsync(entity, relationshipLogicalName, default, entities);
        }

        /// <summary>
        /// Associates one or more entities to an entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity"></param>
        /// <param name="relationshipLogicalName"></param>
        /// <param name="token"></param>
        /// <param name="entities"></param>
        public static Task AssociateAsync(this IOrganizationServiceAsync2 service, EntityReference entity, string relationshipLogicalName,
                CancellationToken token = default, params EntityReference[] entities)
        {
            var relationship = new Relationship(relationshipLogicalName);
            if (entity.LogicalName == entities.First().LogicalName)
            {
                relationship.PrimaryEntityRole = EntityRole.Referenced;
            }

            return service.AssociateAsync(entity.LogicalName, entity.Id,
                relationship,
                new EntityReferenceCollection(entities.ToList()), token);
        }

        /// <summary>
        /// Associates one or more entities to an entity.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity"></param>
        /// <param name="relationshipLogicalName"></param>
        /// <param name="entities"></param>
        public static Task AssociateAsync(this IOrganizationServiceAsync2 service, EntityReference entity, string relationshipLogicalName, params EntityReference[] entities)
        {
            return service.AssociateAsync(entity, relationshipLogicalName, default, entities);
        }

        #endregion AssociateAsync

        #region CreateOrMinimumUpdateAsync

        /// <summary>
        /// Compares the values in the most recent entities by id dictionary to the given value, only updating the fields that are out of date.
        /// </summary>
        /// <typeparam name="TEntity">The Entity Type</typeparam>
        /// <param name="service">The Service.</param>
        /// <param name="entity">The Entity to Create or Update.</param>
        /// <param name="mostRecentEntitiesById">The most recent entities by id.</param>
        /// <param name="token"></param>
        public static Task CreateOrMinimumUpdateAsync<TEntity>(this IOrganizationServiceAsync2 service, TEntity entity,
                Dictionary<Guid, TEntity> mostRecentEntitiesById, CancellationToken token = default) where TEntity : Entity
        {
            return service.CreateOrMinimumUpdateAsync(entity, new MinimumUpdaterDefault<TEntity>(mostRecentEntitiesById), token);
        }

        /// <summary>
        /// Compares the values in the most recent entities by id dictionary to the given value, only updating the fields that are out of date.
        /// </summary>
        /// <typeparam name="TEntity">The Entity Type</typeparam>
        /// <param name="service">The Service.</param>
        /// <param name="entity">The Entity to Create or Update.</param>
        /// <param name="updater">The IMinimumUpdater to use.</param>
        /// <param name="token"></param>
        public static async Task CreateOrMinimumUpdateAsync<TEntity>(this IOrganizationServiceAsync2 service, TEntity entity,
            IMinimumUpdater<TEntity> updater = null, CancellationToken token = default) where TEntity: Entity
        {
            if (entity.Id == Guid.Empty)
            {
                // No Guid.  Must be for create
                updater?.PreCreate(entity);
                entity.Id = entity.KeyAttributes?.Count > 0
                    ? (await service.UpsertAsync(entity, token)).Target.Id
                    : await service.CreateAsync(entity, token);
                updater?.PostCreate(entity);
                return;
            }

            var image = await GetCurrentValueAsync(service, entity, updater, token);
            if (image == null)
            {
                // Guid exists, but no current version, update everything.
                updater?.PreUpdate(entity);
                await service.UpdateAsync(entity, token);
                return;
            }

            // Perform a minimum update
            var localEntity = entity.Clone();
            var unchangedAttributes = new List<string>();

            foreach (var keyValue in image.Attributes.Where(kvp => localEntity.Contains(kvp.Key)
                                                                   && kvp.Value.NullSafeEquals(localEntity.GetAttributeValue<object>(kvp.Key))
                                                                   && !kvp.Value.NullSafeEquals(localEntity.Id)))
            {
                unchangedAttributes.Add(keyValue.Key);
                localEntity.Attributes.Remove(keyValue.Key);
            }
            if (localEntity.Attributes.Count == 1
                && localEntity.Attributes.First().Value.Equals(localEntity.Id))
            {
                // Only attribute left is the Id Guid.  Skip!
                updater?.NoChangesToSync(entity);
                return;
            }

            updater?.PreMinimalUpdate(entity, localEntity, unchangedAttributes);
            await service.UpdateAsync(localEntity, token);

            if (updater?.ShouldUpdateCurrentVersion(image, localEntity) == true)
            {
                foreach (var keyValue in localEntity.Attributes)
                {
                    image[keyValue.Key] = keyValue.Value;
                }
            }
        }

        private static Task<TEntity> GetCurrentValueAsync<TEntity>(IOrganizationServiceAsync2 service, TEntity entity,
                IMinimumUpdater<TEntity> updater, CancellationToken token) where TEntity : Entity
        {
            if(updater != null)
            {
                return Task.FromResult(updater.GetCurrentValue(entity));
            }

            return typeof(TEntity) == typeof(Entity)
                ? service.GetEntityOrDefaultAsync(entity.LogicalName, entity.Id, token: token).CastType<TEntity, Entity>()
                : service.GetEntityOrDefaultAsync<TEntity>(entity.Id, token: token);
        }

        #endregion CreateOrMinimumUpdateAsync

        #region CreateWithSuppressDuplicateDetectionAsync

        /// <summary>
        /// Creates a record with SuppressDuplicateDetection Enabled to Ignore any potential Duplicates Created
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Guid> CreateWithSuppressDuplicateDetectionAsync(this IOrganizationServiceAsync2 service, Entity entity, CancellationToken token = default)
        {
            var response = await service.ExecuteAsync(new CreateRequest
            {
                Target = entity,
                ["SuppressDuplicateDetection"] = true
            }, token).CastResponse<CreateResponse>();
            return response.id;
        }

        #endregion CreateWithSuppressDuplicateDetectionAsync

        /// <summary>
        /// Executes a batch of requests against the CRM Web Service using the ExecuteMultipleRequest command.
        /// </summary>
        /// <remarks>
        /// ExecuteMultipleRequest allows for a maximum of 1000 messages to be processed in a single batch job.
        /// </remarks>
        /// <param name="service">Organization Service proxy for connecting to the relevant CRM instance.</param>
        /// <param name="requestCollection">Collection of organization requests to execute against the CRM Web Services.</param>
        /// <param name="returnResponses">Indicates if responses should be returned for the action taken on each entity in the bulk operation.</param>
        /// <param name="continueOnError">Indicates if the batch job should continue if an error occurs for any of the entities being processed. Default is true.</param>
        /// <param name="token"></param>
        /// <returns>Returns the <see cref="ExecuteMultipleResponse"/> containing responses and faults from the operation if the returnResponses parameter is set to true; otherwise returns null. Default is true.</returns>
        public static Task<ExecuteMultipleResponse> ExecuteMultipleAsync(this IOrganizationServiceAsync2 service, OrganizationRequestCollection requestCollection, bool returnResponses = true, bool continueOnError = true, CancellationToken token = default)
        {
            // Validate required parameters.
            if (service == null)
                throw new ArgumentNullException(nameof(service), "A valid Organization Service Proxy must be specified.");
            // Validate the request collection.
            if (requestCollection == null)
                throw new ArgumentNullException(nameof(requestCollection), "The collection of requests to batch process cannot be null.");
            // Ensure the user is not attempting to pass in more than 1000 requests for the batch job, as this is the maximum number CRM allows within a single batch.
            if (requestCollection.Count > 1000)
                throw new ArgumentOutOfRangeException(nameof(requestCollection), "The Entity Collection cannot contain more than 1000 items, as that is the maximum number of messages that can be processed by the CRM web services in a single batch.");

            try
            {
                // Instantiate a new ExecuteMultipleRequest.
                var multipleRequest = new ExecuteMultipleRequest
                {
                    Settings = new ExecuteMultipleSettings { ContinueOnError = continueOnError, ReturnResponses = returnResponses },
                    Requests = requestCollection
                };

                return service.ExecuteAsync(multipleRequest, token).CastResponse<ExecuteMultipleResponse>();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while executing an ExecuteMultipleRequest. See inner exception for details.", ex);
            }
        }

        /// <summary>
        /// Returns the WhoAmIResponse to determine the current user's UserId, BusinessUnitId, and OrganizationId
        /// </summary>
        /// <param name="service"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<WhoAmIResponse> GetCurrentlyExecutingUserInfoAsync(this IOrganizationServiceAsync2 service, CancellationToken token = default)
        {
            return service.ExecuteAsync(new WhoAmIRequest(), token).CastResponse<WhoAmIResponse>();
        }

        #region GetEntityAsync

        /// <summary>
        /// Retrieves the Entity of the given type with the given Id, with all columns
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="id">Primary Key of Entity</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<T> GetEntityAsync<T>(this IOrganizationServiceAsync2 service, Guid id, CancellationToken token = default)
            where T : Entity
        {
            return service.GetEntityAsync<T>(id, SolutionCheckerAvoider.CreateColumnSetWithAllColumns(), token);
        }

        /// <summary>
        /// Retrieves the Entity of the given type with the given Id, with the given columns
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="id">Primary Key of Entity</param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<T> GetEntityAsync<T>(this IOrganizationServiceAsync2 service, Guid id, Expression<Func<T, object>> anonymousTypeInitializer, CancellationToken token = default)
            where T : Entity
        {
            return service.GetEntityAsync<T>(id, new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray()), token);
        }

        /// <summary>
        /// Retrieves the Entity of the given type with the given Id, with the given columns
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="id">Primary Key of Entity</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<T> GetEntityAsync<T>(this IOrganizationServiceAsync2 service, Guid id, ColumnSet columnSet, CancellationToken token = default)
            where T : Entity
        {
            return (await service.RetrieveAsync(EntityHelper.GetEntityLogicalName<T>(), id, columnSet, token)).AsEntity<T>();
        }

        #endregion GetEntityAsync

        #region GetEntityLogicalNameAsync

        /// <summary>
        /// Gets the Entity Logical Name for the given object Type Code
        /// </summary>
        /// <param name="service"></param>
        /// <param name="objectTypeCode">The Object Type Code</param>
        /// <param name="useCache">Allows for caching the calls in a thread safe manner</param>
        /// <returns></returns>
        public static async Task<string> GetEntityLogicalNameAsync(this IOrganizationServiceAsync2 service, int objectTypeCode, bool useCache = true, CancellationToken token = default)
        {
            if (useCache)
            {
                if (ObjectTypeToLogicalNameMapping.TryGetValue(objectTypeCode, out var cachedValue))
                {
                    return cachedValue;
                }

                var serverValue = await GetEntityLogicalNameInternalAsync(service, objectTypeCode, token);
                ObjectTypeToLogicalNameMapping[objectTypeCode] = serverValue;
                return serverValue;
            }
            else
            {
                return await GetEntityLogicalNameInternalAsync(service, objectTypeCode, token);
            }
        }

        private static async Task<string> GetEntityLogicalNameInternalAsync(this IOrganizationServiceAsync2 service, int objectTypeCode, CancellationToken token)
        {
            var entityFilter = new MetadataFilterExpression(LogicalOperator.And);
            entityFilter.Conditions.Add(new MetadataConditionExpression("ObjectTypeCode", MetadataConditionOperator.Equals, objectTypeCode));
            var propertyExpression = new MetadataPropertiesExpression { AllProperties = false };
            propertyExpression.PropertyNames.Add("LogicalName");

            var response = await service.ExecuteAsync(new RetrieveMetadataChangesRequest
            {
                Query = new EntityQueryExpression
                {
                    Criteria = entityFilter,
                    Properties = propertyExpression
                }
            }, token).CastResponse<RetrieveMetadataChangesResponse>();

            return response.EntityMetadata.Count >= 1
                ? response.EntityMetadata[0].LogicalName
                : null;
        }

        #endregion GetEntityLogicalNameAsync

        #region GetEntitiesAsync

        /// <summary>
        /// Returns first 5000 entities using the Query Expression
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qb">Query to Execute.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, QueryBase qb, CancellationToken token = default) where T : Entity
        {
            return (await service.RetrieveMultipleAsync(qb, token)).ToEntityList<T>();
        }

        /// <summary>
        /// Returns first 5000 entities using the Query Expression
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qe">Query Expression to Execute.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, TypedQueryExpression<T> qe, CancellationToken token = default) where T : Entity
        {
            return (await service.RetrieveMultipleAsync(qe, token)).ToEntityList<T>();
        }

        #endregion GetEntitiesAsync

        #region GetFirstAsync

        /// <summary>
        /// Gets the first entity that matches the query expression.  An exception is thrown if none are found.
        /// </summary>
        /// <typeparam name="T">The Entity Type.</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qe">The query expression.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service, QueryExpression qe, CancellationToken token = default) where T : Entity
        {
            var entity = await service.GetFirstOrDefaultAsync<T>(qe, token);
            AssertExists(entity, qe);
            return entity;
        }

        /// <summary>
        /// Gets the first entity that matches the query expression.  An exception is thrown if none are found.
        /// </summary>
        /// <typeparam name="T">The Entity Type.</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qe">The query expression.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service, TypedQueryExpression<T> qe, CancellationToken token = default) where T : Entity
        {
            var entity = await service.GetFirstOrDefaultAsync(qe, token);
            AssertExists(entity, qe);
            return entity;
        }

        #endregion GetFirstAsync

        #region GetFirstOrDefaultAsync

        /// <summary>
        /// Gets the first entity that matches the query expression.  Null is returned if none are found.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="query">The query.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Entity> GetFirstOrDefaultAsync(this IOrganizationServiceAsync2 service, QueryBase query, CancellationToken token = default)
        {
            query.First();
            return (await service.RetrieveMultipleAsync(query, token)).Entities.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first entity that matches the query.  Null is returned if none are found.
        /// </summary>
        /// <typeparam name="T">The Entity Type.</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qb">The query.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<T> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service, QueryBase qb, CancellationToken token = default) where T : Entity
        {
            qb.First();
            return (await service.GetEntitiesAsync<T>(qb, token)).FirstOrDefault();
        }

        /// <summary>
        /// Gets the first entity that matches the query expression.  Null is returned if none are found.
        /// </summary>
        /// <typeparam name="T">The Entity Type.</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qe">The query expression.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<T> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service, TypedQueryExpression<T> qe, CancellationToken token = default) where T : Entity
        {
            return service.GetFirstOrDefaultAsync<T>(qe.Query, token);
        }

        #endregion GetFirstOrDefaultAsync

        #region GetUserLocalTimeAsync

        /// <summary>
        /// Gets the local time from the UTC time.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="userId">The id of the user to lookup the timezone code user settings</param>
        /// <param name="utcTime">The given UTC time to find the user's local time for.  Defaults to DateTime.UtcNow</param>
        /// <param name="defaultTimeZoneCode">Default TimeZoneCode if the user has no TimeZoneCode defined.  Defaults to EDT.</param>
        /// <param name="token"></param>
        public static async Task<DateTime> GetUserLocalTimeAsync(this IOrganizationServiceAsync2 service, Guid userId, DateTime? utcTime, int defaultTimeZoneCode = 35, CancellationToken token = default)
        {
            var timeZoneCode = (await RetrieveUserSettingsTimeZoneCodeAsync(service, userId, token)) ?? defaultTimeZoneCode;
            var request = new LocalTimeFromUtcTimeRequest
            {
                TimeZoneCode = timeZoneCode,
                UtcTime = utcTime ?? DateTime.UtcNow
            };

            var response = (LocalTimeFromUtcTimeResponse)service.Execute(request);

            return response.LocalTime;
        }

        /// <summary>
        /// Retrieves the current users TimeZoneCode
        /// </summary>
        private static async Task<int?> RetrieveUserSettingsTimeZoneCodeAsync(IOrganizationServiceAsync2 service, Guid userId, CancellationToken token)
        {
            // ReSharper disable StringLiteralTypo
            var setting = await service.GetFirstOrDefaultAsync("usersettings", new ColumnSet("timezonecode"), "systemuserid", userId, token);
            return setting?.GetAttributeValue<int?>("timezonecode");
            // ReSharper restore StringLiteralTypo
        }

        #endregion GetUserLocalTimeAsync

        #region InitializeFromAsync

        /// <summary>
        /// Utilizes the standard OOB Mappings from CRM to hydrate fields on child record from a parent.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="parentEntity">The Parent Entity.</param>
        /// <param name="childLogicalName">The logical name of the child</param>
        /// <param name="targetFieldType">The Target Field Type</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Entity> InitializeFromAsync(this IOrganizationServiceAsync2 service, EntityReference parentEntity, string childLogicalName, TargetFieldType targetFieldType = TargetFieldType.All, CancellationToken token = default)
        {
            var initialize = new InitializeFromRequest
            {
                TargetEntityName = childLogicalName,
                EntityMoniker = parentEntity,
                TargetFieldType = targetFieldType
            };
            var initialized = (InitializeFromResponse)await service.ExecuteAsync(initialize, token);

            return initialized.Entity;
        }

        /// <summary>
        /// Utilizes the standard OOB Mappings from CRM to hydrate fields on child record from a parent.
        /// </summary>
        /// <typeparam name="T">The Entity Type to Return</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="parentEntity">The Parent Entity.</param>
        /// <param name="targetFieldType">The Target Field Type</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<T> InitializeFromAsync<T>(this IOrganizationServiceAsync2 service, EntityReference parentEntity, TargetFieldType targetFieldType = TargetFieldType.All, CancellationToken token = default) where T : Entity
        {
            var initialize = new InitializeFromRequest
            {
                TargetEntityName = EntityHelper.GetEntityLogicalName<T>(),
                EntityMoniker = parentEntity,
                TargetFieldType = targetFieldType
            };
            var initialized = (InitializeFromResponse) await service.ExecuteAsync(initialize, token);

            return initialized.Entity.AsEntity<T>();
        }

        #endregion InitializeFromAsync

        #region SetStateAsync

        /// <summary>
        /// Sets the state
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="entity">The entity to set the state of.</param>
        /// <param name="state">The state to change the entity to.</param>
        /// <param name="status">The status to change the entity to.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SetStateAsync(this IOrganizationServiceAsync2 service, EntityReference entity, int state, int? status = null, CancellationToken token = default)
        {
            var info = new LateBoundActivePropertyInfo(entity.LogicalName);

            var local = new Entity(entity.LogicalName)
            {
                Id = entity.Id,
                [info.AttributeName] = info.ActiveAttribute == ActiveAttributeType.IsDisabled
                    ? state == 1
                    : new OptionSetValue(state)
            };
            if (status.HasValue)
            {
                local["status"] = new OptionSetValue(status.Value);
            }

            return service.UpdateAsync(local, token);
        }

        /// <summary>
        /// Sets the state
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="entity">The entity to set the state of.</param>
        /// <param name="state">The state to change the entity to.</param>
        /// <param name="status">The status to change the entity to.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SetStateAsync(this IOrganizationServiceAsync2 service, Entity entity, int state, int? status = null, CancellationToken token = default)
        {
            return SetStateAsync(service, entity.ToEntityReference(), state, status, token);
        }

        /// <summary>
        /// Currently only tested against System Users.  Not sure if it will work with other entities
        /// May need to rename this to SetSystemUserState
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">logical name of the entity.</param>
        /// <param name="id">The id of the entity.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SetStateAsync(this IOrganizationServiceAsync2 service, string logicalName, Guid id, bool active, CancellationToken token = default)
        {
            return SetStateAsync(service, new EntityReference(logicalName, id), active, token);
        }

        /// <summary>
        /// Currently only tested against System Users.  Not sure if it will work with other entities
        /// May need to rename this to SetSystemUserState
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task SetStateAsync(this IOrganizationServiceAsync2 service, EntityReference entity, bool active, CancellationToken token = default)
        {
            var info = new LateBoundActivePropertyInfo(entity.LogicalName);
            var state = active ?
                info.ActiveState ?? 0 :
                info.NotActiveState ?? (info.ActiveState == 1 ? 0 : 1);

            return service.SetStateAsync(entity, state, token: token);
        }

        #endregion SetStateAsync

        #region UpdateWithSupressDuplicateDetectionAsync

        /// <summary>
        /// Creates a record with SuppressDuplicateDetection Enabled to Ignore any potential Duplicates Created
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task UpdateWithSuppressDuplicateDetectionAsync(this IOrganizationServiceAsync2 service, Entity entity, CancellationToken token = default) 
        {
            return service.ExecuteAsync(new UpdateRequest
            {
                Target = entity,
                ["SuppressDuplicateDetection"] = true
            }, token);
        }

        #endregion CreateWithSupressDuplicateDetectionAsync

        /// <summary>
        /// Updates or insert a record in CRM.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static Task<UpsertResponse> UpsertAsync(this IOrganizationServiceAsync2 service, Entity entity, CancellationToken token = default)
        {
            return service.ExecuteAsync(new UpsertRequest
            {
                Target = entity
            }, token).CastResponse<UpsertResponse>();
        }
    }
}
