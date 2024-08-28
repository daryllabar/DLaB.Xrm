﻿#if NET
using DataverseUnitTest.Assumptions;
using Source.DLaB.Xrm;
#endif
using DLaB.Xrm.Entities;
using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test.Assumptions
{
    // ReSharper disable once InconsistentNaming
    public class Product_Install : EntityDataAssumptionBaseAttribute, IAssumptionEntityType<Product_Install, Product>
    {
        protected override Entity RetrieveEntity(IOrganizationService service)
        {
            return service.GetFirstOrDefault<Product>(p => new
            {
                p.ProductNumber,
                p.ProductId,
                p.Description
            },
            Product.Fields.ProductNumber, "Install");
        }
    }
}