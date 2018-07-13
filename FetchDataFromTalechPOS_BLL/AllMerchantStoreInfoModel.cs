using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{


    public class ResponseCode
    {
        public string desc { get; set; }
        public int statusCode { get; set; }
        public int totalCnt { get; set; }
    }

    public class PremiumFeature
    {
        public string activationDate { get; set; }
        public string featureType { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isDefault { get; set; }
        public bool isDeleted { get; set; }
        public int merchantId { get; set; }
        public string metaData { get; set; }
        public string parentFeatureType { get; set; }
        public int parentId { get; set; }
        public int provider { get; set; }
        public int storeId { get; set; }
    }

    public class SocialNetworkURLs
    {
        public string twitURL { get; set; }
        public string website { get; set; }
        public string instURL { get; set; }
        public string businessDescription { get; set; }
        public string fbURL { get; set; }
        public string gURL { get; set; }
        public string ytbeURL { get; set; }
        public string yelpURL { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public object entityType { get; set; }
        public int id { get; set; }
        public int parentId { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }

    public class DeliveryAddress
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public object entityType { get; set; }
        public int id { get; set; }
        public int parentId { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    }

    public class GatewayInfo
    {
        public int id { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public bool isDemo { get; set; }
        public string merchantAccountId { get; set; }
        public int merchantId { get; set; }
        public string paymentChannel { get; set; }
        public int paymentsProcessorid { get; set; }
        public string processorCode { get; set; }
        public int storeId { get; set; }
    }

    public class Store
    {
        public Address address { get; set; }
        public string businessCloseHour { get; set; }
        public string businessEmail { get; set; }
        public string businessName { get; set; }
        public string businessPhone { get; set; }
        public string createdOn { get; set; }
        public string currency { get; set; }
        public List<DeliveryAddress> deliveryAddress { get; set; }
        public List<object> deliveryHours { get; set; }
        public bool equipmentShipped { get; set; }
        public GatewayInfo gatewayInfo { get; set; }
        public bool gatewayProvisioned { get; set; }
        public string goliveDate { get; set; }
        public int id { get; set; }
        public string locale { get; set; }
        public string marketingMessage { get; set; }
        public int merchantId { get; set; }
        public string mid { get; set; }
        public string modifiedOn { get; set; }
        public string note { get; set; }
        public object ooGatewayInfo { get; set; }
        public string prevStatus { get; set; }
        public bool setupDone { get; set; }
        public string statusChangeDate { get; set; }
        public List<object> storeHours { get; set; }
        public int storeId { get; set; }
        public string storeName { get; set; }
        public string vatId { get; set; }
    }

    public class CashDrawerInfo
    {
        public bool isCashDrawerMgmtEnable { get; set; }
    }

    public class MerchantDetail
    {
        public bool accountSetup { get; set; }
        public string activationDate { get; set; }
        public string agentInfo { get; set; }
        public string applicationReceivedDate { get; set; }
        public List<object> billingInfos { get; set; }
        public string businessDescription { get; set; }
        public string category { get; set; }
        public bool contractSigned { get; set; }
        public string contractSignedDate { get; set; }
        public string createdOn { get; set; }
        public string defaultTimeZone { get; set; }
        public int id { get; set; }
        public string industryTypeCode { get; set; }
        public bool isDemo { get; set; }
        public object legalBusinessTO { get; set; }
        public bool merchantBoarded { get; set; }
        public int merchantIdentification { get; set; }
        public string merchantLogo { get; set; }
        public string microSiteStatus { get; set; }
        public string modifiedOn { get; set; }
        public List<object> multiStoreInfo { get; set; }
        public int parentMerchantId { get; set; }
        public List<object> parentMerchantIds { get; set; }
        public List<PremiumFeature> premiumFeatures { get; set; }
        public object prevStatus { get; set; }
        public bool primary { get; set; }
        public int salesAgent { get; set; }
        public string signupIPAddress { get; set; }
        public SocialNetworkURLs socialNetworkURLs { get; set; }
        public string statusChangeDate { get; set; }
        public List<Store> stores { get; set; }
        public string tosAccepted { get; set; }
        public string trialEndDate { get; set; }
        public string trialStartDate { get; set; }
        public string website { get; set; }
        public CashDrawerInfo cashDrawerInfo { get; set; }
    }

    public class DisplayTracking
    {
        public string displayName { get; set; }
        public string lastViewed { get; set; }
        public int totalViewed { get; set; }
    }

    public class CreatedOn
    {
        public int date { get; set; }
        public int day { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int month { get; set; }
        public int nanos { get; set; }
        public int seconds { get; set; }
        public object time { get; set; }
        public int timezoneOffset { get; set; }
        public int year { get; set; }
    }

    public class ModifiedOn
    {
        public int date { get; set; }
        public int day { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int month { get; set; }
        public int nanos { get; set; }
        public int seconds { get; set; }
        public object time { get; set; }
        public int timezoneOffset { get; set; }
        public int year { get; set; }
    }

    public class OverridenPermissionsBitMap
    {
        public List<string> allowedOperations { get; set; }
    }

    public class Wage
    {
        public int doubleTimeRate { get; set; }
        public int id { get; set; }
        public bool isDeleted { get; set; }
        public bool overriden { get; set; }
    }

    public class MerchantStoreAccess
    {
        public string allowedOp { get; set; }
        public CreatedOn createdOn { get; set; }
        public int id { get; set; }
        public int inheritedDoubleTimeRate { get; set; }
        public int inheritedOverTimeRate { get; set; }
        public int inheritedRegularRate { get; set; }
        public bool isClockInReqToAccess { get; set; }
        public bool isClockedIn { get; set; }
        public int isDefault { get; set; }
        public int isDeleted { get; set; }
        public int merchantId { get; set; }
        public ModifiedOn modifiedOn { get; set; }
        public OverridenPermissionsBitMap overridenPermissionsBitMap { get; set; }
        public bool owner { get; set; }
        public int roleId { get; set; }
        public int storeId { get; set; }
        public object user { get; set; }
        public int userId { get; set; }
        public string usrGroupCode { get; set; }
        public Wage wage { get; set; }
    }

    public class CreatedOn2
    {
        public int date { get; set; }
        public int day { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int month { get; set; }
        public int nanos { get; set; }
        public int seconds { get; set; }
        public object time { get; set; }
        public int timezoneOffset { get; set; }
        public int year { get; set; }
    }

    public class ModifiedOn2
    {
        public int date { get; set; }
        public int day { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int month { get; set; }
        public int nanos { get; set; }
        public int seconds { get; set; }
        public object time { get; set; }
        public int timezoneOffset { get; set; }
        public int year { get; set; }
    }

    public class OverridenPermissionsBitMap2
    {
        public List<string> allowedOperations { get; set; }
    }

    public class Wage2
    {
        public int doubleTimeRate { get; set; }
        public int id { get; set; }
        public bool isDeleted { get; set; }
        public bool overriden { get; set; }
    }

    public class MerchantStoreAccessIncludingDeleted
    {
        public string allowedOp { get; set; }
        public CreatedOn2 createdOn { get; set; }
        public int id { get; set; }
        public int inheritedDoubleTimeRate { get; set; }
        public int inheritedOverTimeRate { get; set; }
        public int inheritedRegularRate { get; set; }
        public bool isClockInReqToAccess { get; set; }
        public bool isClockedIn { get; set; }
        public int isDefault { get; set; }
        public int isDeleted { get; set; }
        public int merchantId { get; set; }
        public ModifiedOn2 modifiedOn { get; set; }
        public OverridenPermissionsBitMap2 overridenPermissionsBitMap { get; set; }
        public bool owner { get; set; }
        public int roleId { get; set; }
        public int storeId { get; set; }
        public object user { get; set; }
        public int userId { get; set; }
        public string usrGroupCode { get; set; }
        public Wage2 wage { get; set; }
    }

    public class User
    {
        public string allowedHourWeekly { get; set; }
        public string allowedOp { get; set; }
        public bool changePasswordOnLogin { get; set; }
        public string createdOn { get; set; }
        public int defaultMerchantId { get; set; }
        public List<DisplayTracking> displayTrackings { get; set; }
        public int doubleTimeRate { get; set; }
        public string email { get; set; }
        public int failedLoginAttempts { get; set; }
        public string hPin { get; set; }
        public bool hasMultipleStoreAccess { get; set; }
        public int hasPin { get; set; }
        public int id { get; set; }
        public string isActive { get; set; }
        public bool isClockInReqToAccess { get; set; }
        public bool isClockedIn { get; set; }
        public List<MerchantStoreAccess> merchantStoreAccess { get; set; }
        public List<MerchantStoreAccessIncludingDeleted> merchantStoreAccessIncludingDeleted { get; set; }
        public string modifiedOn { get; set; }
        public string name { get; set; }
        public string oldPassword { get; set; }
        public int ownedBy { get; set; }
        public bool requireAdminPin { get; set; }
        public string resetDate { get; set; }
        public bool sendWelcomeEmail { get; set; }
        public int storeId { get; set; }
        public bool superUser { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userName { get; set; }
        public string usrGroupCode { get; set; }
    }

    public class MerchantStoreDetails
    {
        public List<object> allRoles { get; set; }
        public int clockedInRoleId { get; set; }
        public int expiresIn { get; set; }
        public int isClockedIn { get; set; }
        public object loginSessionInfoTO { get; set; }
        public List<MerchantDetail> merchantDetails { get; set; }
        public string securityToken { get; set; }
        public string sessionJWT { get; set; }
        public List<string> supportedFeatures { get; set; }
        public List<string> supportedPremiumFeatures { get; set; }
        public User user { get; set; }
    }

    public class AllMerchantStoreInfoModel
    {
        public ResponseCode ResponseCode { get; set; }
        public MerchantStoreDetails merchantStoreDetails { get; set; }
    }

    public class MerchantIdentification_StoreName
    {
        public string merchantIdentification { get; set; }
        public string merchantStoreName { get; set; }
        public int storeId { get; set; }
        public int merchantId { get; set; }

        public List<EmployeeResultModel> lstEmployee { get; set; }
    }
}
