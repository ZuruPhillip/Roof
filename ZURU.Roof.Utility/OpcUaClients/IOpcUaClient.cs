using System.Collections.Generic;
using System.Threading.Tasks;
using Opc.Ua.Client;

namespace ZURU.Roof.Utility.OpcUaClients
{
    public interface IOpcUaClient
    {
        MonitoredItem AddMonitoredItem(Subscription subscription, string nodeIdString, string itemName, int samplingInterval, bool defaultNotificationFunction = false);
        bool ReadBoolFromPlc(string plcAddress);
        int ReadIntFromPlc(string plcAddress);
        string ReadStringFromPlc(string plcAddress);
        List<string> ReadStringListFromPlc(List<string> plcAddresses);
        MonitoredItem RemoveMonitoredItem(Subscription subscription, MonitoredItem monitoredItem);
        void RemoveSubscription(Subscription subscription);
        Subscription Subscribe(int publishingInterval = 100);
        Task WaitNodeValueToBeFalseAsync(string plcAddress);
        Task WaitNodeValueToBeTrueAsync(string plcAddress);
        Task WaitTaskIndexIncreaseToEnd(int actionSum, string actionIndexNode);
        void WriteBoolToPlc(string plcAddress, bool value);
        void WriteIntToPlc(string plcAddress, int value);
        void WriteKeyValuePairsToPlc(List<KeyValuePair<string, object>> keyValuePairs, string dataWriteCompletedNode);
        void WriteStringListToPlc(List<string> keys, List<string> values);
    }
}