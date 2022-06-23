using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.Parsers
{
    public static class DiscountAndPolicyParser
    {
        public static ServiceExpression ConvertDiscountFromJson(string json)
        {
            var sexp = JsonConvert.DeserializeObject<ServiceExpression>(json);
            if (sexp.tag.Contains("Discount"))
            {
                return JsonToServiceDiscount(sexp, json);
            }
            else // IServiceConditional
            {
                JObject rss = JObject.Parse(json);
                var test = JsonConvert.DeserializeObject<ServicePredicate>(rss["test"].ToString());
                var discount1 = JsonConvert.DeserializeObject<ServiceExpression>(rss["thenDis"].ToString());
                var discount2 = JsonConvert.DeserializeObject<ServiceExpression>(rss["elseDis"].ToString());
                if (discount2 != null)
                    return new ServiceIf(JsonToServicePredicate(test, rss["test"].ToString()), JsonToServiceDiscount(discount1, rss["thenDis"].ToString()), JsonToServiceDiscount(discount2, rss["elseDis"].ToString()));
                return new ServiceConditionDiscount(JsonToServicePredicate(test, rss["test"].ToString()), JsonToServiceDiscount(discount1, rss["thenDis"].ToString()));

            }
        }

        private static ServicePredicate JsonToServicePredicate(dynamic spred, string json)
        {

            if (spred.tag == "AndPredicate")
            {

                JObject rss = JObject.Parse(json);
                var pred1 = JsonConvert.DeserializeObject<ServicePredicate>(rss["firstExpression"].ToString());
                var pred2 = JsonConvert.DeserializeObject<ServicePredicate>(rss["secondExpression"].ToString());
                return new ServiceAnd(JsonToServicePredicate(pred1, rss["firstExpression"].ToString()), JsonToServicePredicate(pred2, rss["secondExpression"].ToString()));

            }
            else if (spred.tag == "OrPredicate")
            {
                JObject rss = JObject.Parse(json);
                var pred1 = JsonConvert.DeserializeObject<ServicePredicate>(rss["firstExpression"].ToString());
                var pred2 = JsonConvert.DeserializeObject<ServicePredicate>(rss["secondExpression"].ToString());
                return new ServiceOr(JsonToServicePredicate(pred1, rss["firstExpression"].ToString()), JsonToServicePredicate(pred2, rss["secondExpression"].ToString()));
            }
            else if (spred.tag == "XorPredicate")// Xor
            {
                JObject rss = JObject.Parse(json);
                var pred1 = JsonConvert.DeserializeObject<ServicePredicate>(rss["firstExpression"].ToString());
                var pred2 = JsonConvert.DeserializeObject<ServicePredicate>(rss["secondExpression"].ToString());
                return new ServiceXor(JsonToServicePredicate(pred1, rss["firstExpression"].ToString()), JsonToServicePredicate(pred2, rss["secondExpression"].ToString()));
            }


            else if (spred.tag == "BagValuePredicate")
            {

                var result = JsonConvert.DeserializeObject<ServiceBagValue>(json);
                return new ServiceBagValue(result.worth);

            }
            else // ServiceProductAmount
            {
                var result = JsonConvert.DeserializeObject<ServiceProductAmount>(json);
                return new ServiceProductAmount(result.pid, result.quantity);

            }
        }

        private static ServiceRestriction convertRestrictionFromJson(dynamic expression, string json)
        {
            if (expression.tag == "AfterHourAmountRestriction")
            {
                var result = JsonConvert.DeserializeObject<ServiceAfterHourProduct>(json);
                return new ServiceAfterHourProduct(result.hour, result.productId, result.amount);
            }
            else if (expression.tag == "BeforeHourProductRestriction")
            {
                var result = JsonConvert.DeserializeObject<ServiceBeforeHourProduct>(json);
                return new ServiceBeforeHourProduct(result.hour, result.productId, result.amount);
            }
            else if (expression.tag == "BeforeHourRestriction")
            {
                var result = JsonConvert.DeserializeObject<ServiceBeforeHour>(json);
                return new ServiceBeforeHour(result.hour);
            }
            else if (expression.tag == "AfterHourRestriction")
            {
                var result = JsonConvert.DeserializeObject<ServiceAfterHour>(json);
                return new ServiceAfterHour(result.hour);
            }
            else if (expression.tag == "AtMostAmountRestriction")
            {
                var result = JsonConvert.DeserializeObject<ServiceAtMostAmount>(json);
                return new ServiceAtMostAmount(result.productId, result.amount);
            }
            else if (expression.tag == "AtLeastAmountRestriction")
            {
                var result = JsonConvert.DeserializeObject<ServiceAtlestAmount>(json);
                return new ServiceAtlestAmount(result.productId, result.amount);
            }
            else // date restriction
            {
                var result = JsonConvert.DeserializeObject<ServiceDateRestriction>(json);
                return new ServiceDateRestriction(result.year, result.month, result.day);
            }


        }

        private static ServiceDiscount JsonToServiceDiscount(dynamic discount, string json)
        {
            if (discount.tag == "DateDiscount")
            {
                var result = JsonConvert.DeserializeObject<ServiceDateDiscount>(json);
                return new ServiceDateDiscount(result.discount, result.year, result.month, result.day);
            }
            else if (discount.tag == "productDiscount")
            {
                var result = JsonConvert.DeserializeObject<ServiceProductDiscount>(json);
                return new ServiceProductDiscount(result.productId, result.discount);
            }
            else if (discount.tag == "XorDiscount")
            {
                JObject rss = JObject.Parse(json);
                var discount1 = JsonConvert.DeserializeObject<ServiceExpression>(rss["firstExpression"].ToString());
                var discount2 = JsonConvert.DeserializeObject<ServiceExpression>(rss["secondExpression"].ToString());
                return new ServiceXorDiscount(JsonToServiceDiscount(discount1, rss["firstExpression"].ToString()), JsonToServiceDiscount(discount2, rss["secondExpression"].ToString()));

            }
            else if (discount.tag == "OrDiscount")
            {
                JObject rss = JObject.Parse(json);
                var discount1 = JsonConvert.DeserializeObject<ServiceExpression>(rss["firstExpression"].ToString());
                var discount2 = JsonConvert.DeserializeObject<ServiceExpression>(rss["secondExpression"].ToString());
                return new ServiceOrDiscount(JsonToServiceDiscount(discount1, rss["firstExpression"].ToString()), JsonToServiceDiscount(discount2, rss["secondExpression"].ToString()));

            }
            else if (discount.tag == "AndDiscount")
            {
                JObject rss = JObject.Parse(json);
                var discount1 = JsonConvert.DeserializeObject<ServiceExpression>(rss["firstExpression"].ToString());
                var discount2 = JsonConvert.DeserializeObject<ServiceExpression>(rss["secondExpression"].ToString());
                return new ServiceAndDiscount(JsonToServiceDiscount(discount1, rss["firstExpression"].ToString()), JsonToServiceDiscount(discount2, rss["secondExpression"].ToString()));

            }
            else if (discount.tag == "AddativeDiscount")
            {
                IList<ServiceDiscount> disList = new List<ServiceDiscount>();
                ServiceAddative sa = new ServiceAddative();
                JObject rss = JObject.Parse(json);
                int length = rss["discounts"].Count();
                for (int i = 0; i < length; i++)
                {
                    var currDiscount = JsonConvert.DeserializeObject<ServiceExpression>(rss["discounts"][i].ToString());
                    sa.AddDiscount(JsonToServiceDiscount(currDiscount, rss["discounts"][i].ToString()));
                }
                return sa;
            }
            else if (discount.tag == "maxDiscount")
            {
                IList<ServiceDiscount> disList = new List<ServiceDiscount>();
                ServiceMax sm = new ServiceMax();
                JObject rss = JObject.Parse(json);
                int length = rss["discounts"].Count();
                for (int i = 0; i < length; i++)
                {
                    var currDiscount = JsonConvert.DeserializeObject<ServiceExpression>(rss["discounts"][i].ToString());
                    sm.AddDiscount(JsonToServiceDiscount(currDiscount, rss["discounts"][i].ToString()));
                }
                return sm;
            }
            else
            {
                var result = JsonConvert.DeserializeObject<ServiceStoreDiscount>(json);
                return new ServiceStoreDiscount(result.discount);
            }

        }



        private static ServicePurchasePredicate convertPredicateFromJson(dynamic pred, string json)
        {
            if (pred.tag == "CheckProductMorePredicate")
            {
                var result = JsonConvert.DeserializeObject<ServiceCheckProductMore>(json);
                return new ServiceCheckProductMore(result.productId, result.amount);
            }
            else // product less
            {
                var result = JsonConvert.DeserializeObject<ServiceCheckProductLess>(json);
                return new ServiceCheckProductMore(result.productId, result.amount);
            }
        }

        private static ServicePurchasePolicy convertPurchaseFromJson(dynamic expression, string json)
        {

            if (expression.tag == "AndPurchase")
            {
                JObject rss = JObject.Parse(json);
                var firstPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["firstPred"].ToString());
                var secondPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["secondPred"].ToString());
                return new ServicePurchaseAnd(convertRestrictionFromJson(firstPred, rss["firstPred"].ToString()), convertRestrictionFromJson(secondPred, rss["secondPred"].ToString()));
            }
            else if (expression.tag == "OrPurchase")
            {

                JObject rss = JObject.Parse(json);
                var firstPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["firstPred"].ToString());
                var secondPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["secondPred"].ToString());
                return new ServicePurchaseOr(convertRestrictionFromJson(firstPred, rss["firstPred"].ToString()), convertRestrictionFromJson(secondPred, rss["secondPred"].ToString()));
            }
            else // implies
            {
                JObject rss = JObject.Parse(json);
                var firstPred = JsonConvert.DeserializeObject<ServicePredicate>(rss["condition"].ToString());
                var secondPred = JsonConvert.DeserializeObject<ServicePredicate>(rss["allowing"].ToString());
                return new ServiceImplies(convertPredicateFromJson(firstPred, rss["condition"].ToString()), convertPredicateFromJson(secondPred, rss["allowing"].ToString()));
            }


        }
        public static ServicePurchasePolicy ConvertPolicyFromJson(string json)
        {
            var sexp = JsonConvert.DeserializeObject<ServicePurchasePolicy>(json);
            if (sexp.tag.Contains("Restriction"))
                return convertRestrictionFromJson(sexp, json);
            else
                return convertPurchaseFromJson(sexp, json);
        }



    }
}
