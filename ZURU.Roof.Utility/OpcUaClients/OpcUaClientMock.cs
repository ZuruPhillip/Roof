using Microsoft.Extensions.Logging;
using Opc.Ua.Client;

namespace ZURU.Roof.Utility.OpcUaClients
{
    public sealed class OpcUaClientMock : IOpcUaClient
    {
        private ILogger<OpcUaClientMock> _logger;

        public OpcUaClientMock(ILogger<OpcUaClientMock> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool ReadBoolFromPlc(string plcAddress)
        {
            var value = false;
            _logger.LogInformation("MOCK - {plcAddress} is: {Value}", plcAddress, value);
            return value;
        }

        public int ReadIntFromPlc(string plcAddress)
        {
            _logger.LogInformation("MOCK - {plcAddress} has value: 0", plcAddress);
            return 0;
        }

        public string ReadStringFromPlc(string plcAddress)
        {
            var value = "stringValue";
            _logger.LogInformation("MOCK - {plcAddress} has value: {value}", plcAddress, value);
            return value;
        }

        public List<string> ReadStringListFromPlc(List<string> plcAddresses)
        {
            var values = new List<string>();
            for (int i = 0; i < plcAddresses.Count; i++)
            {
                var value = "stringValue_" + i;
                values.Add(value);
                _logger.LogInformation("MOCK - {plcAddress} has value: {value}", plcAddresses[i], value);
            }
            return values;
        }

        public async Task WaitNodeValueToBeFalseAsync(string plcAddress)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            _logger.LogInformation("MOCK - {plcAddress} is: false", plcAddress);
        }

        public async Task WaitNodeValueToBeTrueAsync(string plcAddress)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            _logger.LogInformation("MOCK - {plcAddress} is: true", plcAddress);
        }

        public async Task WaitTaskIndexIncreaseToEnd(int actionSum, string actionIndexNode)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            _logger.LogInformation("MOCK - Index Increase to the end");
        }

        public void WriteBoolToPlc(string plcAddress, bool value)
        {
            _logger.LogInformation("MOCK - {plcAddress} write to: {value}", plcAddress, value);
        }

        public void WriteIntToPlc(string plcAddress, int value)
        {
            _logger.LogInformation("MOCK - {plcAddress} write to: {value}", plcAddress, value);
        }

        public void WriteKeyValuePairsToPlc(List<KeyValuePair<string, object>> keyValuePairs, string dataWriteCompleteNode)
        {
            foreach (var item in keyValuePairs)
            {
                _logger.LogInformation("MOCK - {key} write sucess with {value}", item.Key, item.Value);
            }
        }

        public void WriteStringListToPlc(List<string> keys, List<string> values)
        {
            if (keys.Count != values.Count) throw new ArgumentException("keys-values length not mathch");
            for (int i = 0; i < keys.Count; i++)
            {
                _logger.LogInformation("MOCK - {key} write sucess with {value}", keys[i], values[i]);
            }
        }

        public Subscription Subscribe(int publishingInterval = 100)
        {
            return new Subscription();
        }

        public void RemoveSubscription(Subscription subscription)
        {
            _logger.LogInformation("Remove subscription");
        }

        public MonitoredItem RemoveMonitoredItem(Subscription subscription, MonitoredItem monitoredItem)
        {
            _logger.LogInformation("Remove monitoredItem{itemName}", monitoredItem.DisplayName);
            return monitoredItem;
        }

        public MonitoredItem AddMonitoredItem(Subscription subscription, string nodeIdString, string itemName, int samplingInterval, bool defaultNotificationFunction = false)
        {
            _logger.LogInformation("Add monitoredItem{itemName}", itemName);
            return new MonitoredItem();
        }
    }
}
