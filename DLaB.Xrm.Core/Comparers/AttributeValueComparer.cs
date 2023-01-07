using System.Collections;
using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;
#if DLAB_UNROOT_COMMON_NAMESPACE
using DLaB.Common;
#else
using Source.DLaB.Common;
#endif

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm.Comparers
#else
namespace Source.DLaB.Xrm.Comparers
#endif
{
    /// <summary>
    /// Comparer for Attributes
    /// </summary>
    public class AttributeComparer
    {
        /// <summary>
        /// Returns true if the given objects are (value) equal.  Assumed to work for any Dataverse value types ie EntityReference, OptionSetValue, etc
        /// </summary>
        /// <param name="service">Service for looking up attribute keys</param>
        /// <param name="value">The value</param>
        /// <param name="preValue">The value to Compare</param>
        /// <returns></returns>
        public static bool ValuesAreEqual(IOrganizationService service, object value, object preValue)
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
                        var entity = service.GetEntityOrDefault(entityRef.LogicalName, entityRef.KeyAttributes, new ColumnSet(false));
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

                    var preItems = new Dictionary<Type, List<object>>();
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

                        var match = typedValues.FirstOrDefault(v => ValuesAreEqual(service, item, v));
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
    }
}
