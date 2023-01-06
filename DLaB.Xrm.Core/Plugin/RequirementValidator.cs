using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DLaB.Xrm.Plugin;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
using DLaB.Common;

namespace DLaB.Xrm.Plugin
#else
using Source.DLaB.Common;

namespace Source.DLaB.Xrm.Plugin
#endif
{
    /// <summary>
    /// Used to validate requirements
    /// </summary>
    public class RequirementValidator: IRequirementValidator
    {
        private Dictionary<ContextEntity, Requirement> Requirements { get; } = new Dictionary<ContextEntity,Requirement>();

        /// <summary>
        /// Returns true if the context does not meet the requirements for execution
        /// </summary>
        /// <param name="context">The Context</param>
        /// <returns></returns>
        public bool SkipExecution(IExtendedPluginContext context)
        {
            foreach (var requirement in Requirements.Values)
            {
                if (requirement.SkipExecution(context))
                {
                    return true;
                }
            }
            return false;
        }


        #region Contains

        #region Non-null

        /// <summary>
        /// Requires all columns are in the attributes collection with a non-null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator Contains(ContextEntity entityType, params string[] columnNames)
        {
            Get(entityType).RequiredColumns.AddMissing(columnNames);
            return this;
        }

        /// <summary>
        /// Requires all columns are in the attributes collection with a non-null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator Contains<T>(ContextEntity entityType, Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return Contains(entityType, anonymousTypeInitializer.GetAttributeNamesArray());
        }

        /// <summary>
        /// Requires at least one column is in the attributes collection with a non-null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator ContainsAny(ContextEntity entityType, params string[] columnNames)
        {
            Get(entityType).RequiredOrColumns.Add(new List<string>(columnNames));
            return this;
        }

        /// <summary>
        /// Requires at least one column is in the attributes collection with a non-null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator ContainsAny<T>(ContextEntity entityType, Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return ContainsAny(entityType, anonymousTypeInitializer.GetAttributeNamesArray());
        }

        #endregion Non-null

        #region Nullable

        /// <summary>
        /// Requires all columns are in the attributes collection (Allows nulls)
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator ContainsNullable(ContextEntity entityType, params string[] columnNames)
        {
            Get(entityType).RequiredColumnsAllowNulls.AddMissing(columnNames);
            return this;
        }

        /// <summary>
        /// Requires all columns are in the attributes collection (Allows nulls)
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator ContainsNullable<T>(ContextEntity entityType, Expression<Func<T, object>> anonymousTypeInitializer) where T: Entity
        {
            return ContainsNullable(entityType, anonymousTypeInitializer.GetAttributeNamesArray());
        }

        /// <summary>
        /// Requires at least one column is in the attributes collection (Allows nulls)
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator ContainsAnyNullable(ContextEntity entityType, params string[] columnNames)
        {
            Get(entityType).RequiredOrColumnsAllowNulls.Add(new List<string>(columnNames));
            return this;
        }

        /// <summary>
        /// Requires at least one column is in the attributes collection (Allows nulls)
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator ContainsAnyNullable<T>(ContextEntity entityType, Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return ContainsAnyNullable(entityType, anonymousTypeInitializer.GetAttributeNamesArray());
        }

        #endregion Nullable

        #region Null

        /// <summary>
        /// Requires all columns are in the attributes collection with a null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator ContainsNull(ContextEntity entityType, params string[] columnNames)
        {
            Get(entityType).RequiredNullColumns.AddMissing(columnNames);
            return this;
        }

        /// <summary>
        /// Requires all columns are in the attributes collection with a null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator ContainsNull<T>(ContextEntity entityType, Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return ContainsNull(entityType, anonymousTypeInitializer.GetAttributeNamesArray());
        }

        /// <summary>
        /// Requires at least one column is in the attributes collection with a null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator ContainsAnyNull(ContextEntity entityType, params string[] columnNames)
        {
            Get(entityType).RequiredNullOrColumns.Add(new List<string>(columnNames));
            return this;
        }

        /// <summary>
        /// Requires at least one column is in the attributes collection with a null value
        /// </summary>
        /// <param name="entityType">The entity type</param>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator ContainsAnyNull<T>(ContextEntity entityType, Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return ContainsAnyNull(entityType, anonymousTypeInitializer.GetAttributeNamesArray());
        }

        #endregion Null

        #endregion Contains

        #region Updated (Non-Null) / Changed (Nullable) / Cleared (Null)

        #region Updated (Non-Null)

        /// <summary>
        /// Requires all columns have been updated to a non-null value that is different than the pre-image value
        /// </summary>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator Updated(params string[] columnNames)
        {
            Get(ContextEntity.Target).UpdatedColumns.AddMissing(columnNames);
            return this;
        }

        /// <summary>
        /// Requires all columns to have been updated to a non-null value that is different than the pre-image value
        /// </summary>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator Updated<T>(Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return Updated(anonymousTypeInitializer.GetAttributeNamesArray());
        }

        /// <summary>
        /// Requires at least one column has been updated to a non-null value that is different than the pre-image value
        /// </summary>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator UpdatedAny(params string[] columnNames)
        {
            Get(ContextEntity.Target).UpdatedOrColumns.Add(new List<string>(columnNames));
            return this;
        }

        /// <summary>
        /// Requires at least one column has been updated to a non-null value that is different than the pre-image value
        /// </summary>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator UpdatedAny<T>(Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return UpdatedAny(anonymousTypeInitializer.GetAttributeNamesArray());
        }

        #endregion Updated (Non-Null)

        #region Changed (Nullable)

        /// <summary>
        /// Requires all columns to have been updated to a value that is different than the pre-image (allows updating to null)
        /// </summary>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator Changed(params string[] columnNames)
        {
            Get(ContextEntity.Target).UpdatedColumnsAllowNulls.AddMissing(columnNames);
            return this;
        }

        /// <summary>
        /// Requires all columns to have been updated to a value that is different than the pre-image (allows updating to null)
        /// </summary>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator Changed<T>(Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return Changed(anonymousTypeInitializer.GetAttributeNamesArray());
        }

        /// <summary>
        /// Requires at least one column has been updated to a value that is different than the pre-image value (allows updating to null)
        /// </summary>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator ChangedAny(params string[] columnNames)
        {
            Get(ContextEntity.Target).UpdatedOrColumnsAllowNulls.Add(new List<string>(columnNames));
            return this;
        }

        /// <summary>
        /// Requires at least one column has been updated to a value that is different than the pre-image value (allows updating to null)
        /// </summary>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator ChangedAny<T>(Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return ChangedAny(anonymousTypeInitializer.GetAttributeNamesArray());
        }

        #endregion Changed (Nullable)

        #region Cleared (Null)

        /// <summary>
        /// Requires all columns to have been updated from a non-null value to a null value
        /// </summary>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator Cleared(params string[] columnNames)
        {
            Get(ContextEntity.Target).UpdatedNullColumns.AddMissing(columnNames);
            return this;
        }

        /// <summary>
        /// Requires all columns to have been updated from a non-null value to a null value
        /// </summary>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator Cleared<T>(Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return Cleared(anonymousTypeInitializer.GetAttributeNamesArray());
        }

        /// <summary>
        /// Requires at least one column has been updated from a non-null value to a null value)
        /// </summary>
        /// <param name="columnNames">The column names</param>
        /// <returns></returns>
        public RequirementValidator ClearedAny(params string[] columnNames)
        {
            Get(ContextEntity.Target).UpdatedNullOrColumns.Add(new List<string>(columnNames));
            return this;
        }

        /// <summary>
        /// Requires at least one column has been updated from a non-null value to a null value)
        /// </summary>
        /// <param name="anonymousTypeInitializer">The type initializer</param>
        /// <returns></returns>
        public RequirementValidator ClearedAny<T>(Expression<Func<T, object>> anonymousTypeInitializer) where T : Entity
        {
            return ClearedAny(anonymousTypeInitializer.GetAttributeNamesArray());
        }

        #endregion Cleared (Null)

        #endregion Updated (Non-Null) / Changed (Nullable) / Cleared (Null)

        private Requirement Get(ContextEntity entityType)
        {
            if (Requirements.TryGetValue(entityType, out var requirement))
            {
                return requirement;
            }

            requirement = new Requirement(entityType);
            Requirements[entityType] = requirement;
            return requirement;
        }

        private class Requirement
        {
            private ContextEntity EntityType { get; }

            public HashSet<string> RequiredColumns { get; } = new HashSet<string>();
            public List<List<string>> RequiredOrColumns { get; } = new List<List<string>>();

            public HashSet<string> RequiredColumnsAllowNulls { get; } = new HashSet<string>();
            public List<List<string>> RequiredOrColumnsAllowNulls { get; } = new List<List<string>>();

            public HashSet<string> RequiredNullColumns { get; } = new HashSet<string>();
            public List<List<string>> RequiredNullOrColumns { get; } = new List<List<string>>();

            public HashSet<string> UpdatedColumns { get; } = new HashSet<string>();
            public List<List<string>> UpdatedOrColumns { get; } = new List<List<string>>();

            public HashSet<string> UpdatedColumnsAllowNulls { get; } = new HashSet<string>();
            public List<List<string>> UpdatedOrColumnsAllowNulls { get; } = new List<List<string>>();

            public HashSet<string> UpdatedNullColumns { get; } = new HashSet<string>();
            public List<List<string>> UpdatedNullOrColumns { get; } = new List<List<string>>();

            private bool IsPreImageRequired { get; set; } = true;

            public Requirement(ContextEntity entityType)
            {
                EntityType = entityType;
            }

            public bool SkipExecution(IExtendedPluginContext context)
            {
                var entity = GetEntity(context);

                var preImage = context.GetMessageType() == MessageType.Update
                    ? context.GetPreEntity<Entity>()
                    : new Entity();
                AssertHasPreImageIfRequired(preImage);

                return SkipExecution(context, entity, RequiredColumns, RequiredOrColumns, checkNotNull: true)
                       || SkipExecution(context, entity, RequiredColumnsAllowNulls, RequiredOrColumnsAllowNulls, checkNotNull: false)
                       || SkipNonNullExecution(context, entity, RequiredNullColumns, RequiredNullOrColumns)
                       || SkipExecutionForUpdate(context, entity, preImage, UpdatedColumns, UpdatedOrColumns, checkNotNull: true)
                       || SkipExecutionForUpdate(context, entity, preImage, UpdatedColumnsAllowNulls, UpdatedOrColumnsAllowNulls, checkNotNull: false)
                       || SkipNonNullExecutionForUpdate(context, entity, preImage, UpdatedNullColumns, UpdatedNullOrColumns);
            }

            private bool SkipExecution(IExtendedPluginContext context, Entity entity, HashSet<string> allColumns, List<List<string>> atLeastOneColumns, bool checkNotNull)
            {
                foreach (var column in allColumns)
                {
                    if (!entity.Contains(column))
                    {
                        context.Trace("The {0} entity type did not contain the required column {1}!", EntityType, column);
                        return true;
                    }

                    if (checkNotNull && entity[column] == null)
                    {
                        context.Trace("The {0} entity type contained a null value for the required column {1}!", EntityType, column);
                        return true;
                    }
                }

                foreach (var set in atLeastOneColumns)
                {
                    var requirementMet = false;
                    foreach (var column in set)
                    {
                        if (entity.Contains(column) && (!checkNotNull || entity[column] != null))
                        {
                            requirementMet = true;
                            break;
                        }
                    }

                    if (!requirementMet)
                    {
                        if (checkNotNull)
                        {
                            context.Trace("The {0} entity type did not contain a non-null value for at least one of the following columns: {1}!", EntityType, string.Join(", ", set));
                        }
                        else
                        {
                            context.Trace("The {0} entity type did not contain at least one of the following columns: {1}!", EntityType, string.Join(", ", set));
                        }
                        return true;
                    }
                }

                return false;
            }

            private bool SkipNonNullExecution(IExtendedPluginContext context, Entity entity, HashSet<string> requiredNulls, List<List<string>> atLeastOneNulls)
            {
                foreach (var column in requiredNulls)
                {
                    if (!entity.Contains(column))
                    {
                        context.Trace("The {0} entity type did not contain the required to be null column {1}!", EntityType, column);
                        return true;
                    }

                    if (entity[column] != null)
                    {
                        context.Trace("The {0} entity type contained a non-null value for the required to be null column {1}!", EntityType, column);
                        return true;
                    }
                }

                foreach (var set in atLeastOneNulls)
                {
                    var requirementMet = false;
                    foreach (var column in set)
                    {
                        if (entity.Contains(column) && entity[column] == null)
                        {
                            requirementMet = true;
                            break;
                        }
                    }

                    if (!requirementMet)
                    {
                        context.Trace("The {0} entity type did not contain a null value for at least one of the following columns: {1}!", EntityType, string.Join(", ", set));
                        return true;
                    }
                }

                return false;
            }

            private void AssertHasPreImageIfRequired(Entity preImage)
            {
                if (preImage != null
                    || !IsPreImageRequired)
                {
                    return;
                }

                var requiredPreImageColumns = new HashSet<string>();
                requiredPreImageColumns.AddMissing(UpdatedColumns);
                requiredPreImageColumns.AddMissing(UpdatedColumnsAllowNulls);
                requiredPreImageColumns.AddMissing(UpdatedOrColumns.SelectMany(c => c));
                requiredPreImageColumns.AddMissing(UpdatedOrColumnsAllowNulls.SelectMany(c => c));
                if (EntityType == ContextEntity.PreImage || EntityType == ContextEntity.CoalesceTargetPreImage)
                {
                    requiredPreImageColumns.AddMissing(RequiredColumns);
                    requiredPreImageColumns.AddMissing(RequiredColumnsAllowNulls);
                    requiredPreImageColumns.AddMissing(RequiredNullColumns);
                    requiredPreImageColumns.AddMissing(RequiredOrColumns.SelectMany(c => c));
                    requiredPreImageColumns.AddMissing(RequiredOrColumnsAllowNulls.SelectMany(c => c));
                }

                if (requiredPreImageColumns.Any())
                {
                    throw new InvalidPluginExecutionException(
                        $"A pre-image was required but not found!  Expected a pre-image to be registered for this step with the following columns: {string.Join(", ", requiredPreImageColumns)}");
                }

                IsPreImageRequired = false;
            }

            private bool SkipExecutionForUpdate(IExtendedPluginContext context, Entity target, Entity preImage, HashSet<string> allColumns, List<List<string>> atLeastOneColumns, bool checkNotNull)
            {
                foreach (var column in allColumns)
                {
                    if (!target.Contains(column))
                    {
                        context.Trace("The target did not contain a required update of column {1}!", EntityType, column);
                        return true;
                    }

                    if (checkNotNull && target[column] == null)
                    {
                        context.Trace("The target did not contain a required update of column {1} to a non-null value!", EntityType, column);
                        return true;
                    }

                    if (!ColumnValueHasChanged(context, target, preImage, column))
                    {
                        context.Trace("The target did not update the column {1} to a non-null value!", EntityType, column);
                        return true;
                    }
                }

                foreach (var set in atLeastOneColumns)
                {
                    var requirementMet = false;
                    foreach (var column in set)
                    {
                        if (target.Contains(column) && (!checkNotNull || target[column] != null) && ColumnValueHasChanged(context, target, preImage, column))
                        {
                            requirementMet = true;
                            break;
                        }
                    }

                    if (!requirementMet)
                    {
                        if (checkNotNull)
                        {
                            context.Trace("The target did not update to a non-null value for at least one of the following columns: {0}!", string.Join(", ", set));
                        }
                        else
                        {
                            context.Trace("The target did not update at least one of the following columns: {0}!", string.Join(", ", set));
                        }
                        return true;
                    }
                }

                return false;
            }

            private bool SkipNonNullExecutionForUpdate(IExtendedPluginContext context, Entity target, Entity preImage, HashSet<string> allNullColumns, List<List<string>> atLeastOneNullColumns)
            {
                foreach (var column in allNullColumns)
                {
                    if (!target.Contains(column))
                    {
                        context.Trace("The target did not contain a required update of column {0} to null!", column);
                        return true;
                    }

                    if (target[column] != null)
                    {
                        context.Trace("The target contained a non-null value for column {0} that was required to be updated to null!", column);
                        return true;
                    }

                    if (!ColumnValueHasChanged(context, target, preImage, column))
                    {
                        context.Trace("The target contained a null value for column {0} that was required to be updated to null, but it was already null!", column);
                        return true;
                    }
                }

                foreach (var set in atLeastOneNullColumns)
                {
                    var requirementMet = false;
                    foreach (var column in set)
                    {
                        if (target.Contains(column) && target[column] == null && ColumnValueHasChanged(context, target, preImage, column))
                        {
                            requirementMet = true;
                            break;
                        }
                    }
                
                    if (!requirementMet)
                    {
                        context.Trace("The target did not update at least one of the following columns to null: {0}!", string.Join(", ", set));
                        return true;
                    }
                }

                return false;
            }

            private bool ColumnValueHasChanged(IExtendedPluginContext context, Entity target, Entity preImage, string column)
            {
                if (!target.Contains(column))
                {
                    return false;
                }

                return !ColumnValuesAreEqual(context, target[column], preImage.Contains(column) ? preImage[column] : null);
            }

            private static bool ColumnValuesAreEqual(IExtendedPluginContext context, object value, object preValue)
            {
                if (preValue == null)
                {
                    return value == null;
                }

                switch (value)
                {
                    case null:
                        return false;

                    //case ColumnSet cs:
                    //    value = cs.AllColumns
                    //        ? "\"ColumnSet(allColumns:true)\""
                    //        : $"\"{string.Join(",", cs.Columns.OrderBy(c => c))}\"";
                    //    break;

                    //case Entity entity:
                    //    value = entity.ToStringAttributes(info);
                    //    break;

                    case EntityReference entityRef:
                        var preEntityRef = (EntityReference)preValue;
                        if (entityRef.Id == Guid.Empty
                            && entityRef.KeyAttributes?.Any() == true)
                        {
                            var entity = context.SystemOrganizationService.GetEntityOrDefault(entityRef.LogicalName, entityRef.KeyAttributes, new ColumnSet(false));
                            entityRef = entity.ToEntityReference();
                        }

                        return entityRef.Id != preEntityRef.Id
                               || entityRef.LogicalName != preEntityRef.LogicalName;

                    //case EntityCollection entities:
                    //    value = entities.ToStringDebug(info);
                    //    break;
                    //
                    //case EntityReferenceCollection entityRefCollection:
                    //    value = entityRefCollection.ToStringDebug(info);
                    //    break;

                    case Dictionary<string, string> dict:
                        var preDict = (Dictionary<string, string>)preValue;
                        return dict.Keys.OrderBy(k => k).SequenceEqual(preDict.Keys.OrderBy(k => k))
                               && dict.OrderBy(k => k).Select(kvp => kvp.Value).SequenceEqual(preDict.OrderBy(k => k).Select(kvp => kvp.Value));

                    //case FetchExpression fetch:
                    //    value = $"\"{fetch.Query.Trim()}\"";
                    //    break;

                    case byte[] imageArray:
                        return imageArray.SequenceEqual((byte[])preValue);

                    case IEnumerable enumerable when !(enumerable is string):
                    
                        var preItems = new Dictionary<Type,List<object>>();
                        var preCount = 0;
                        foreach (var item in ((IEnumerable)preValue))
                        {
                            preItems.AddOrAppend(item.GetType(), item);
                            preCount++;
                        }
                    
                        var items = new List<object>();
                        foreach (var item in enumerable)
                        {
                            if (!preItems.TryGetValue(item.GetType(), out var typedValues))
                            {
                                return false;
                            }

                            var match = typedValues.FirstOrDefault(v => ColumnValuesAreEqual(context, item, v));
                            if (match == null)
                            {
                                return false;
                            }

                            typedValues.Remove(match);
                            items.Add(item);
                        }

                        return items.Count == preCount;


                    case OptionSetValue optionSet:
                        return optionSet.Value == ((OptionSetValue)preValue).Value;

                    case Money money:
                        return money.Value == ((Money)preValue).Value;

                    //case QueryExpression qe:
                    //    value = $"\"{qe.GetSqlStatement().Trim()}\"";
                    //    break;

                    default:
                        return value.Equals(preValue);
                }
            }

            private Entity GetEntity(IExtendedPluginContext context)
            {
                switch (EntityType)
                {
                    case ContextEntity.CoalesceTargetPostImage:
                        return context.CoalesceTargetWithPostEntity<Entity>();
                    case ContextEntity.CoalesceTargetPreImage:
                        return context.CoalesceTargetWithPreEntity<Entity>();
                    case ContextEntity.PostImage:
                        return context.GetPostEntity<Entity>();
                    case ContextEntity.PreImage:
                        return context.GetPreEntity<Entity>();
                    case ContextEntity.Target:
                        return context.GetTarget<Entity>();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}