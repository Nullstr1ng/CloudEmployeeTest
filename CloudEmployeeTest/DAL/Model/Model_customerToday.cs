/*
 * author:  Jayson Ragasa
 * Date:    July 22, 2015
 */

namespace CloudEmployee.DAL.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class Model_QueueDataBase
    {
        public Model_queueData queueData { get; set; }
    }

    public class Model_queueData
    {
        [JsonProperty("currentUserRole")]
        public string currentUserRole { get; set; }
        public string customerServed { get; set; }
        public bool isActive { get; set; }
        public bool isMyLastCustomer { get; set; }
        public int minutesToNextFree { get; set; }
        public Model_queue queue { get; set; }
        public List<Model_serversAvailable> serversAvailable { get; set; }
    }

    public class Model_queue
    {
        public int averageServeTimeMinutes { get; set; }
        public List<Model_customerToday> customersToday { get; set; }
        public string finishReminder { get; set; }
        public int id { get; set; }
        public string identifier { get; set; }
        public bool isBookingAllowed { get; set; }
        public bool isTabletCollectionEnabled { get; set; }
        public int? maxQueueLength { get; set; }
        public string name { get; set; }
        public string requiredMpn { get; set; }
        public List<Model_servers> servers { get; set; }
        public List<Model_servingServers> servingServers { get; set; }
        public string showAssignedCustomerPopup { get; set; }
        public string snsTopicArn { get; set; }
        public int unreadMessagesForQueue { get; set; }
        public Model_venu venu { get; set; }
    }

    #region customerToday 
    public class Model_customerToday
    {
        public string bookingStartTime { get; set; }
        public string collectingServer { get; set; }
        public int currentPosition { get; set; }
        public Model_customer customer { get; set; }
        public string expectedTime { get; set; }
        public bool hasBeenSentReturnMessage { get; set; }
        public int id { get; set; }
        public bool inStore { get; set; }
        public bool isArrived { get; set; }
        public bool isFixed { get; set; }
        public string joinedTime {get;set;}
        public string lastSMSStatus { get; set; }
        public string originalExpectedTime { get; set; }
        public Model_product product { get; set; }
        public string productDescription { get; set; }
        public string timeArrivedInStore { get; set; }
        public string timeSentReturnMessage { get; set; }
        public int unreadMessages { get; set; }
        public string waitTime { get; set; }
    }

    public class Model_customer
    {
        public Model_createdBy createdBy { get; set; }
        public string emailAddress { get; set; }
        public int id { get; set; }
        public Model_language language { get; set; }
        public Model_merchantCustomer merchantCustomer { get; set; }
        public string mobileNetwork { get; set; }
        public string mobileNumber { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public string pagerNumber { get; set; }
    }

    public class Model_createdBy
    {
        public string displayName { get; set; }
    }

    public class Model_language
    {
        public string isoCode { get; set; }
        public string name { get; set; }
    }

    public class Model_merchantCustomer
    {
        public int id { get; set; }
    }

    public class Model_product
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    #endregion

    public class Model_servers
    {
        public string displayName { get; set; }
        public int id { get; set; }
    }

    public class Model_servingServers
    {

    }

    public class Model_venu
    {
        public string name { get; set; }
    }

    public class Model_serversAvailable
    {
        public int minutesUntilNextAvailability { get; set; }

        public int nextAvailableMinutes { get; set; }
        public Model_server server { get; set; }
    }

    public class Model_server
    {
        public string currentBreakReason { get; set; }
        public string displayName { get; set; }
        public int id { get; set; }
    }
}
