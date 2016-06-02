using System;
using System.Collections.Generic;
using RedisCacheWebPoc.Controllers;

namespace RedisCacheWebPoc.Model
{
    public class TransactionModel
    {
        public long GetTime { get; set; }
        public string AcquirerTransactionId { get; set; }
        public long Amount { get; set; }
        public string AuthorizationCode { get; set; }
        public Guid? BraspagOrderId { get; set; }
        public long? CapturedAmount { get; set; }
        public DateTime ConfirmationPostNextTryDate { get; set; }
        public short ConfirmationPostRemainingTries { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? DeliveryAddressId { get; set; }
        public string Eci { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> ExtraData { get; set; }
        public DateTime? FinalizedDate { get; set; }
        public int Installments { get; set; }
        public bool IsSplitted { get; set; }
        public Guid MerchantId { get; set; }
        public string MerchantOrderId { get; set; }
        public bool MustBeProbed { get; set; }
        public DateTime? PaidDate { get; set; }
        public Guid? ParentTransactionId { get; set; }
        public short PaymentMethodCode { get; set; }
        public Guid PaymentMethodId { get; set; }
        public string PlanCode { get; set; }
        public string ProofOfSale { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string RequestIp { get; set; }
        public string SentOrderId { get; set; }
        public long ServiceTaxAmount { get; set; }
        public DateTime? StartedDate { get; set; }
        public Guid TransactionId { get; set; }
        public long? VoidedAmount { get; set; }
        public DateTime? VoidedDate { get; set; }
        public AffiliationModel Affiliation { get; set; }
        public CustomerModel Customer { get; set; }
        public PaymentPlanEnum PaymentPlan { get; set; }
        public TransactionStatusEnum Status { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public CardModel Card { get; set; }
    }
}
