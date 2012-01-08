using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Plugins;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Orders;

namespace Nop.Plugin.DiscountRules.PurchasedAllProducts
{
    public partial class PurchasedAllProductsDiscountRequirementRule : BasePlugin, IDiscountRequirementRule
    {
        private readonly IOrderService _orderService;

        public PurchasedAllProductsDiscountRequirementRule(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        /// <summary>
        /// Check discount requirement
        /// </summary>
        /// <param name="request">Object that contains all information required to check the requirement (Current customer, discount, etc)</param>
        /// <returns>true - requirement is met; otherwise, false</returns>
        public bool CheckRequirement(CheckDiscountRequirementRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.DiscountRequirement == null)
                throw new NopException("Discount requirement is not set");

            if (String.IsNullOrWhiteSpace(request.DiscountRequirement.RestrictedProductVariantIds))
                return true;

            if (request.Customer == null)
                return false;

            var restrictedProductVariantIds = new List<int>();
            try
            {
                restrictedProductVariantIds = request.DiscountRequirement.RestrictedProductVariantIds
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToList();
            }
            catch
            {
                //error parsing
                return false;
            }
            if (restrictedProductVariantIds.Count == 0)
                return false;

            //purchased products
            var purchasedProductVariants = _orderService.GetAllOrderProductVariants(0,
                request.Customer.Id, null, null, OrderStatus.Complete, null, null);
            
            bool allFound = true;
            foreach (var restrictedPvId in restrictedProductVariantIds)
            {
                bool found1 = false;
                foreach (var purchasedPv in purchasedProductVariants)
                {
                    if (restrictedPvId == purchasedPv.ProductVariantId)
                    {
                        found1 = true;
                        break;
                    }
                }

                if (!found1)
                {
                    allFound = false;
                    break;
                }
            }

            if (allFound)
                return true;

            return false;
        }

        /// <summary>
        /// Get URL for rule configuration
        /// </summary>
        /// <param name="discountId">Discount identifier</param>
        /// <param name="discountRequirementId">Discount requirement identifier (if editing)</param>
        /// <returns>URL</returns>
        public string GetConfigurationUrl(int discountId, int? discountRequirementId)
        {
            //configured in RouteProvider.cs
            string result = "Plugins/DiscountRulesPurchasedAllProducts/Configure/?discountId=" + discountId;
            if (discountRequirementId.HasValue)
                result += string.Format("&discountRequirementId={0}", discountRequirementId.Value);
            return result;
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Plugins.DiscountRules.PurchasedAllProducts.Fields.ProductVariants", "Restricted product variants");
            this.AddOrUpdatePluginLocaleResource("Plugins.DiscountRules.PurchasedAllProducts.Fields.ProductVariants.Hint", "The comma-separated list of product variant identifiers (e.g. 77, 123, 156). You can find a product variant ID on its details page.");
            base.Install();
        }
    }
}