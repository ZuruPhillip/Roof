using Microsoft.Extensions.Logging;
using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ZURU.Roof.OpcUaClients
{

    public class OpcUaClient : IOpcUaClient
    {
        private readonly string _endpointUrl;
        private readonly ILogger<OpcUaClient> _logger;

        OpcUaConnection _connection;

        public OpcUaClient(string endpointUrl, ILogger<OpcUaClient> logger)
        {
            _endpointUrl = endpointUrl;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            TryConnect();
        }

        public void WriteBoolToPlc(string plcAddress, bool value)
        {
            Write(new List<string> { plcAddress }, new List<string> { value.ToString() });

            //CheckPlcDataSentCorrectly(new List<string> { plcAddress }, new List<string> { value.ToString() });

        }

        public void WriteIntToPlc(string plcAddress, int value)
        {
            Write(new List<string> { plcAddress }, new List<string> { value.ToString() });

            //CheckPlcDataSentCorrectly(new List<string> { plcAddress }, new List<string> { value.ToString() });
        }

        public void WriteStringListToPlc(List<string> keys, List<string> values)
        {
            Write(keys, values);

            //CheckPlcDataSentCorrectly(keys, values);
        }

        public void WriteKeyValuePairsToPlc(List<KeyValuePair<string, object>> keyValuePairs, string dataWriteCompletedNode)
        {
            List<string> keys = keyValuePairs.Select(kv => kv.Key).ToList();
            List<string> values = keyValuePairs.Select(kv => kv.Value.ToString()).ToList();

            Write(keys, values);

            //CheckPlcDataSentCorrectly(keys, values);

            //WriteBoolToPlc(dataWriteCompletedNode, true);

            //CheckPlcDataSentCorrectly(new List<string> { dataWriteCompletedNode }, new List<string> { true.ToString() });
        }

        public bool ReadBoolFromPlc(string plcAddress)
        {
            List<string> results = Read(new List<string> { plcAddress });

            bool result = false;

            if (results.Any())
            {
                result = bool.Parse(results.First());
            }
            else
            {
                _logger.LogWarning("Read bool value failed! check if {plcAddress} is valid", plcAddress);
            }
            return result;
        }

        public int ReadIntFromPlc(string plcAddress)
        {
            List<string> results = Read(new List<string> { plcAddress });
            int result = -1;
            if (results.Any())
            {
                result = int.Parse(results.First());
                //_logger.LogInformation("The value of address {plcAddress} is {result}", plcAddress, result);
            }
            else
            {
                _logger.LogWarning("Read bool value failed! check if address {plcAddress} is valid", plcAddress);
            }
            return result;
        }

        public string ReadStringFromPlc(string plcAddress)
        {
            List<string> results = Read(new List<string> { plcAddress });

            string result = "";

            if (results.Any())
            {
                result = results[0];
                //_logger.LogInformation("The value of address {plcAddress} is {result}", plcAddress, result);
            }
            else
            {
                _logger.LogWarning("Read string value failed! check if {plcAddress} is valid", plcAddress);
            }
            return result;
        }

        public List<string> ReadStringListFromPlc(List<string> plcAddresses)
        {
            List<string> results = Read(plcAddresses);
            return results;
        }

        public async Task WaitNodeValueToBeTrueAsync(string plcAddress)
        {
            _logger.LogInformation("Waiting the value of address {plcAddress} to be true", plcAddress);

            while (true)
            {
                try
                {
                    var result = ReadBoolFromPlc(plcAddress);
                    if (result)
                    {
                        _logger.LogInformation("The value of address {plcAddress} has been true", plcAddress);
                        break;
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromSeconds(10));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task WaitNodeValueToBeFalseAsync(string plcAddress)
        {
            _logger.LogInformation("Waiting the value of address {plcAddress} to be false", plcAddress);
            while (true)
            {
                try
                {
                    var result = ReadBoolFromPlc(plcAddress);
                    if (!result)
                    {
                        _logger.LogInformation("The value of address {plcAddress} has been false", plcAddress);
                        break;
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromSeconds(10));
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task WaitTaskIndexIncreaseToEnd(int actionSum, string actionIndexNode)
        {
            int count = ReadIntFromPlc(actionIndexNode);

            while (count != actionSum - 1)
            {
                count = ReadIntFromPlc(actionIndexNode);
                await Task.Delay(TimeSpan.FromMilliseconds(100));
            }
            _logger.LogInformation("Task index has reached to the sum of actions {actionSum}", actionSum);
        }

        #region Subscription
        public Subscription Subscribe(int publishingInterval = 100)
        {
            _logger.LogInformation("Now the subscribe interval is {publishingInterval} milliseconds", publishingInterval);
            return _connection.Subscribe(publishingInterval);
        }

        public MonitoredItem AddMonitoredItem(Subscription subscription, string nodeIdString, string itemName, int samplingInterval, bool defaultNotificationFunction = false)
        {
            _logger.LogInformation("Add monitored item for {Itemname}", itemName);
            return _connection.AddMonitoredItem(subscription, nodeIdString, itemName, samplingInterval, defaultNotificationFunction);
        }

        public MonitoredItem RemoveMonitoredItem(Subscription subscription, MonitoredItem monitoredItem)
        {
            _logger.LogInformation("Remove monitored item for {Itemname}", monitoredItem.DisplayName);
            return _connection.RemoveMonitoredItem(subscription, monitoredItem);
        }

        public void RemoveSubscription(Subscription subscription)
        {
            _logger.LogInformation("Now the subscribe item: {subsctiprionName} is removed", subscription.DisplayName);
            _connection.RemoveSubscription(subscription);
        }

        #endregion

        private void CheckPlcDataSentCorrectly(List<string> keys, List<string> values)
        {
            List<string> results = Read(keys);
            if (results.SequenceEqual(values))
                _logger.LogDebug("Plc data sent correctly");
            else
                _logger.LogError("Plc Data Not Sent Correctly!");
        }

        private void TryConnect()
        {
            _connection = new OpcUaConnection(_endpointUrl, true);
            _connection.TryConnect().GetAwaiter().GetResult();
        }

        private List<string> Read(List<string> keys)
        {
            return _connection.TryReadValues(keys);
        }

        private void Write(List<string> keys, List<string> values)
        {
            _connection.TryWriteValues(values, keys);
        }


    }
}
