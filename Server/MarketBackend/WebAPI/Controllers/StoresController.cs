using MarketBackend.BusinessLayer.Market.StoreManagment;
using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI.Requests;
using ServicePurchase = MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServicePurchasePolicy;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreManagementFacade storeManagementFacade;

        public StoresController(IStoreManagementFacade storeManagementFacade) => 
            this.storeManagementFacade = storeManagementFacade;

        [HttpPost("AddNewProduct")]
        public ActionResult<Response<int>> AddNewProduct([FromBody] AddNewProductToStoreRequest request)
        {
            Response<int> response = storeManagementFacade.AddNewProduct(
                request.UserId, request.StoreId, request.ProductName, request.Price, request.Category);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("IncreaseProductAmount")]
        public ActionResult<Response<bool>> IncreaseProductAmountInInventory([FromBody] ChangeProductAmountInStoreRequest request)
        {
            Response<bool> response = storeManagementFacade.AddProductToInventory(
                request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("DecreaseProductAmount")]
        public ActionResult<Response<bool>> DecreaseProductInInventory([FromBody] ChangeProductAmountInStoreRequest request)
        {
            Response<bool> response = storeManagementFacade.DecreaseProduct(
                request.UserId, request.StoreId, request.ProductId, request.Amount);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("MakeCoOwner")]
        public ActionResult<Response<bool>> MakeCoOwner([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.MakeCoOwner(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("RemoveCoOwner")]
        public ActionResult<Response<bool>> RemoveCoOwner([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.RemoveCoOwner(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("MakeCoManager")]
        public ActionResult<Response<bool>> MakeCoManager([FromBody] RolesManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.MakeCoManager(
                request.UserId, request.TargetUserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("MembersInRole")]
        public ActionResult<Response<IList<int>>> GetMembersInRole([FromBody] GetMembersInRoleRequest request)
        {
            Response<IList<int>> response = storeManagementFacade.GetMembersInRole(
                request.StoreId, request.UserId, request.Role);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Founder")]
        public ActionResult<Response<ServiceMember>> GetFounder([FromBody] StoreManagementRequest request)
        {
            Response<ServiceMember> response = storeManagementFacade.GetFounder(request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ManagerPermissions")]
        public ActionResult<Response<IList<Permission>>> GetManagerPermissions([FromBody] RolesManagementRequest request)
        {
            Response<IList<Permission>> response = storeManagementFacade.GetManagerPermissions(
                request.StoreId, request.UserId, request.TargetUserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ChangeManagerPermission")]
        public ActionResult<Response<bool>> ChangeManagerPermission([FromBody] ChangePermissionsRequest request)
        {
            Response<bool> response = storeManagementFacade.ChangeManagerPermission(
                request.UserId, request.TargetUserId, request.StoreId, request.Permissions);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("PurchaseHistory")]
        public ActionResult<Response<IList<Purchase>>> GetPurchaseHistory([FromBody] StoreManagementRequest request)
        {
            Response<IList<Purchase>> response = storeManagementFacade.GetPurchaseHistory(
                request.UserId ,request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("OpenNewStore")]
        public ActionResult<Response<int>> OpenNewStore([FromBody] OpenStoreRequest request)
        {
            Response<int> response = storeManagementFacade.OpenStore(
                request.UserId, request.StoreName);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("CloseStore")]
        public ActionResult<Response<bool>> CloseStore([FromBody] StoreManagementRequest request)
        {
            Response<bool> response = storeManagementFacade.CloseStore(
                request.UserId, request.StoreId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddDiscountPolicy")]
        public ActionResult<Response<int>> AddDiscountPolicy([FromBody] AddDiscountPolicyRequest request)
        {
            ServiceExpression exp = ConvertDiscountFromJson(request.Expression);
            Response<int> response = storeManagementFacade.AddDiscountPolicy(
                exp, request.Description, request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemoveDiscountPolicy")]
        public ActionResult<Response<bool>> RemoveDiscountPolicy([FromBody] RemovePolicyRequest request)
        {
            Response<bool> response = storeManagementFacade.RemoveDiscountPolicy(
                request.PolicyId, request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddPurchasePolicy")]
        public ActionResult<Response<int>> AddPurchasePolicy([FromBody] AddPurchasePolicyRequest request)
        {
            MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServicePurchasePolicy policy = ConvertPolicyFromJson(request.Expression);
            Response<int> response = storeManagementFacade.AddPurchasePolicy(
                policy, request.Description, request.StoreId, request.UserId);
            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemovePurchasePolicy")]
        public ActionResult<Response<bool>> RemovePurchasePolicy([FromBody] RemovePolicyRequest request)
        {
            Response<bool> response = storeManagementFacade.RemovePurchasePolicy(
                request.PolicyId, request.StoreId, request.UserId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("AddBid")]
        public ActionResult<Response<int>> AddBid([FromBody] AddBidRequest request)
        {
            Response<int> response = storeManagementFacade.AddBid(
                request.StoreId, request.ProductId, request.MemberId, request.BidPrice);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("ApproveBid")]
        public ActionResult<Response<bool>> ApproveBid([FromBody] ApproveBidRequest request)
        {
            Response<bool> response = storeManagementFacade.ApproveBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("DenyBid")]
        public ActionResult<Response<bool>> DenyBid([FromBody] DenyBidRequest request)
        {
            Response<bool> response = storeManagementFacade.DenyBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("MakeCounterOffer")]
        public ActionResult<Response<bool>> MakeCounterOffer([FromBody] MakeCounterOfferRequest request)
        {
            Response<bool> response = storeManagementFacade.MakeCounterOffer(
                request.StoreId, request.MemberId, request.BidId, request.Offer);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("GetApproveForBid")]
        public ActionResult<Response<IList<int>>> GetApproveForBid([FromBody] GetApproveForBidRequest request)
        {
            Response< IList<int>> response = storeManagementFacade.GetApproveForBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("RemoveBid")]
        public ActionResult<Response<bool>> RemoveBid([FromBody] RemoveBidRequest request)
        {
            Response<bool> response = storeManagementFacade.RemoveBid(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("ApproveCounterOffer")]
        public ActionResult<Response<bool>> ApproveCounterOffer([FromBody] ApproveCounterOfferRequest request)
        {
            Response<bool> response = storeManagementFacade.ApproveCounterOffer(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPut("DenyCounterOffer")]
        public ActionResult<Response<bool>> DenyCounterOffer([FromBody] DenyCounterOfferRequest request)
        {
            Response<bool> response = storeManagementFacade.DenyCounterOffer(
                request.StoreId, request.MemberId, request.BidId);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("GetProductReviews")]
        public ActionResult<Response<IDictionary<string, IList<string>>>> GetProductReviews([FromBody] GetProductReviewsRequest request)
        {
            Response<IDictionary<string, IList<string>>> response = storeManagementFacade.GetProductReviews(
                request.StoreId, request.ProductId);
             
            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }
        [HttpPost("AddProductReview")]
        public ActionResult<Response<IDictionary<ServiceMember, IList<string>>>> AddProductReview([FromBody] AddProductReviewRequest request)
        {
            Response<bool> response = storeManagementFacade.AddProductReview(
                request.StoreId, request.MemberId, request.ProductId, request.Review);

            if (response.IsErrorOccured())
                return BadRequest(response);

            return Ok(response);
        }










        // aid function for parsing
        private ServicePredicate JsonToServicePredicate(dynamic spred, string json)
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
        private ServiceDiscount JsonToServiceDiscount(dynamic discount, string json)
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
        private ServiceExpression ConvertDiscountFromJson(string json)
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
        private ServiceRestriction convertRestrictionFromJaon(dynamic expression, string json)
        {
            if (expression.tag== "AfterHourAmountRestriction")
            {
                var result = JsonConvert.DeserializeObject<ServiceAfterHourProduct>(json);
                return new ServiceAfterHourProduct(result.hour, result.productId, result.amount);
            }
            else if (expression.tag== "BeforeHourProductRestriction")
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
        private ServicePurchasePredicate convertPredicateFromJaon(dynamic pred, string json)
        {
            if (pred.tag== "CheckProductMorePredicate")
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

        private MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServicePurchasePolicy convertPurchaseFromJaon(dynamic expression, string json)
        {
       
            if (expression.tag== "AndPurchase")
            {
                JObject rss = JObject.Parse(json);
                var firstPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["firstPred"].ToString());
                var secondPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["secondPred"].ToString());
                return new ServicePurchaseAnd(convertRestrictionFromJaon(firstPred, rss["firstPred"].ToString()), convertRestrictionFromJaon(secondPred, rss["secondPred"].ToString()));
            }
            else if (expression.tag == "OrPurchase")
            {

                JObject rss = JObject.Parse(json);
                var firstPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["firstPred"].ToString());
                var secondPred = JsonConvert.DeserializeObject<ServiceRestriction>(rss["secondPred"].ToString());
                return new ServicePurchaseOr(convertRestrictionFromJaon(firstPred, rss["firstPred"].ToString()), convertRestrictionFromJaon(secondPred, rss["secondPred"].ToString()));
            }
            else // implies
            {
                JObject rss = JObject.Parse(json);
                var firstPred = JsonConvert.DeserializeObject<ServicePredicate>(rss["condition"].ToString());
                var secondPred = JsonConvert.DeserializeObject<ServicePredicate>(rss["allowing"].ToString());
                return new ServiceImplies(convertPredicateFromJaon(firstPred, rss["condition"].ToString()), convertPredicateFromJaon(secondPred, rss["allowing"].ToString()));
            }


        }
        private MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServicePurchasePolicy ConvertPolicyFromJson(string json)
        {
            var sexp = JsonConvert.DeserializeObject<MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs.ServicePurchasePolicy>(json);
            if (sexp.tag.Contains("Restriction"))
                return convertRestrictionFromJaon(sexp, json);
            else
                return convertPurchaseFromJaon(sexp, json);
        }

    }
}
