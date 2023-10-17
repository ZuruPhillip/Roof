namespace ZURU.Roof.Plcs
{
    public class PlcClient : IPlcClient
    {
        /*
        private readonly IOpcUaClient _opcUaClient;
        private readonly ILogger<PlcClient> _logger;
        private readonly IAddress _address;

        public PlcClient(IOpcUaClient opcUaClient, IAddress address, ILogger<PlcClient> logger)
        {
            _opcUaClient = opcUaClient ?? throw new ArgumentNullException(nameof(opcUaClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public async Task SendPlcTasksToPlc(List<PlcRobotAction> plcTasks)
        {
            int count = plcTasks.Count;
            int numEachBulk = 100;
            if (count >= 150)
            {
                int numBulks = count / numEachBulk + 1;
                for (int m = 0; m < numBulks; m++)
                {
                    List<PlcRobotAction> temp = new List<PlcRobotAction>();
                    if (m == numBulks - 1)
                    {
                        temp = plcTasks.Skip(numEachBulk * m).Take(count - numEachBulk * m).ToList();
                    }
                    else
                    {
                        temp = plcTasks.Skip(numEachBulk * m).Take(numEachBulk).ToList();
                    }
                    var plcKeyValuePairs = PlcKeyValuePairGenerator.GetPlcKeyValuePairs(temp, _address.PreStr, _address.TaskSum);
                    _opcUaClient.WriteKeyValuePairsToPlc(plcKeyValuePairs, _address.TaskSent);
                    await _opcUaClient.WaitTaskIndexIncreaseToEnd(temp.Count, _address.TaskIndex);
                    await _opcUaClient.WaitNodeValueToBeTrueAsync(_address.TaskDone);
                }

            }
            else
            {
                var plcKeyValuePairs = PlcKeyValuePairGenerator.GetPlcKeyValuePairs(plcTasks, _address.PreStr, _address.TaskSum);
                _opcUaClient.WriteKeyValuePairsToPlc(plcKeyValuePairs, _address.TaskSent);
                await _opcUaClient.WaitTaskIndexIncreaseToEnd(plcTasks.Count, _address.TaskIndex);
                await _opcUaClient.WaitNodeValueToBeTrueAsync(_address.TaskDone);
            }
        }

        public async Task SendPlcTaskToPlc(PlcRobotAction plcTask)
        {
            var plcKeyValuePairs = PlcKeyValuePairGenerator.GetPlcKeyValuePairs(new List<PlcRobotAction> { plcTask }, _address.PreStr, _address.TaskSum);
            _opcUaClient.WriteKeyValuePairsToPlc(plcKeyValuePairs, _address.TaskSent);
            await _opcUaClient.WaitNodeValueToBeTrueAsync(_address.TaskDone);
        }

        public bool HasToolInSpindle()
        {
            return _opcUaClient.ReadBoolFromPlc(_address.ToolInSpindle);
        }

        public void SendAirGunStartTaskToPlc()
        {
            _opcUaClient.WriteBoolToPlc(_address.DustClean, true);
        }

        public void SendAirGunStopTaskToPlc()
        {
            _opcUaClient.WriteBoolToPlc(_address.DustClean, false);
        }

        public void ClearLaserProfilerData()
        {
            _opcUaClient.WriteBoolToPlc(_address.LaserProfilerDataDelete, true);
        }
        public async Task CheckAgvPermission()
        {
            _logger.LogInformation("Checking station {StationId} agv permission...", _settings.StationId);
            while (true)
            {
                try
                {
                    var agvEnable = _opcUaClient.ReadBoolFromPlc(_address.AgvEnable);
                    if (agvEnable) break;
                    else await Task.Delay(TimeSpan.FromSeconds(10));
                }
                catch
                {
                    throw;
                }
            }
            _logger.LogInformation("Station {StationId} agv is permitted", _settings.StationId);
        }

        public async Task CheckIfTableIn()
        {
            _logger.LogInformation("Checking station {StationId} if table in the cnc room ...", _settings.StationId);
            while (true)
            {
                try
                {
                    var tableIn = _opcUaClient.ReadBoolFromPlc(_address.TableIn);
                    if (tableIn) break;
                    else await Task.Delay(TimeSpan.FromSeconds(10));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            _logger.LogInformation("Station {StationId} table is in the cnc room", _settings.StationId);
        }

        public async Task CheckIfTableOut()
        {
            _logger.LogInformation("Checking station {StationId} if table out of cnc room ...", _settings.StationId);
            while (true)
            {
                try
                {
                    var tableIn = _opcUaClient.ReadBoolFromPlc(_address.TableOut);
                    if (tableIn) break;
                    else await Task.Delay(TimeSpan.FromSeconds(10));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            _logger.LogInformation("Station {StationId} table is out of cnc room", _settings.StationId);
        }

        public void SendOneSideCompletedToPlc()
        {
            _opcUaClient.WriteBoolToPlc(_address.ProcessDone, true);
        }

        //there is a sensor to detect if table arrived, we dont need to send this to plc
        public void SendTableArrivedToPlc()
        {
            _opcUaClient.WriteBoolToPlc(_address.AgvIn, true);
        }

        public async Task<Queue<List<string>>> Send2OPCWaitReadLaserData(List<PlcRobotAction> plcTasks)
        {
            var laserScanDataStorage = new Queue<List<string>>();
            var LaserScanDataAddresses = new List<string> { _address.LaserRead, _address.RobotPosX, _address.RobotPosY };
            var plcKeyValuePairs = PlcKeyValuePairGenerator.GetPlcKeyValuePairs(plcTasks, _address.PreStr, _address.TaskSum);

            _opcUaClient.WriteKeyValuePairsToPlc(plcKeyValuePairs, _address.TaskSent);

            var taskDone = _opcUaClient.ReadBoolFromPlc(_address.TaskDone);

            while (!taskDone)
            {
                if (laserScanDataStorage.Count >= 200)
                {
                    laserScanDataStorage.Dequeue();
                }
                laserScanDataStorage.Enqueue(_opcUaClient.ReadStringListFromPlc(LaserScanDataAddresses));
                taskDone = _opcUaClient.ReadBoolFromPlc(_address.TaskDone);
                await Task.Delay(TimeSpan.FromMilliseconds(20)); //sampling rate
            }
            _logger.LogInformation("Laser scan completed");
            return laserScanDataStorage;
        }



        public async Task WaitTableToArrive()
        {
            _logger.LogInformation("Wait table to arrive at station {StationId}...", _settings.StationId);
            while (true)
            {
                try
                {
                    var trayArrived = _opcUaClient.ReadBoolFromPlc(_address.TableCheck);
                    if (trayArrived) break;
                    else await Task.Delay(TimeSpan.FromSeconds(20));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            _logger.LogInformation("Tray has arrived at Station {StationId}", _settings.StationId);
        }
        public async Task WaitTableToLeave()
        {
            _logger.LogInformation("Wait table to leave from station {StationId}...", _settings.StationId);
            while (true)
            {
                try
                {
                    var trayArrived = _opcUaClient.ReadBoolFromPlc(_address.TableCheck);
                    if (!trayArrived) break;
                    else await Task.Delay(TimeSpan.FromSeconds(20));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            _logger.LogInformation("Tray has left from Station {StationId}", _settings.StationId);
        }
         */
    }
}
