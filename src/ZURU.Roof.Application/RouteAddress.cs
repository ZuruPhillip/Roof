using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURU.Roof
{
    public class RouteAddress
    {
        public const string AddRoofRecordAsync = RoofServiceConsts.RoutePrefix + "add-roof-record-async";

        public const string GetRoofRecordAndSendAsync = RoofServiceConsts.RoutePrefix + "get-roof-record-and-send-async";
    }
}
