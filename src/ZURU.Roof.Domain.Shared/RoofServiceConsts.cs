using System;
using System.Collections.Generic;
using System.Text;

namespace ZURU.Roof
{
    public static class RoofServiceConsts
    {
        public const string DbTablePrefix = "tb_";

        public const string DbSchema = null;

        public const string AppName = "Roof";
        public const float PathOffset = 100.0f;
        public const int RobotId = 4;
        public const int RobotVelocity = 5;
        public const int RobotOverwrite = 10;

        /// <summary>
        /// 是否启用多租户
        /// </summary>
        public const bool MultiTenancyIsEnabled = true;
        public const string RoutePrefix = "api/roof/";
    }
}
