using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZURU.Roof.Utility.Maths;
using ZURU.Roof.Utility.Transforms;

namespace ZURU.Roof.Robots
{
    /// <summary>
    /// Manage the robot Coordinate System and generate E6POS strut to send to PLC
    /// </summary>
    public class RobotController
    {
        public Transform RobotCS { get; set; }
        public Transform WorkpieceCS { get; set; }
        private static readonly float PI = MathF.PI;
        private static readonly float EPISION = 1E-6f;

        public RobotController()
        {
            RobotCS = new Transform();
        }

        public RobotController(Transform _workpieceCS) : this()
        {
            WorkpieceCS = _workpieceCS;
        }

        public RobotController(Transform _workpieceCS, Transform _robotCS) : this(_workpieceCS)
        {
            RobotCS = _robotCS;
        }

        /// <summary>
        /// Convert transform to E6POS in Robot Coordinate System
        /// </summary>
        /// <param name="transform">Transform to convert</param>
        /// <returns>E6POS in Robot Coordinate System</returns>
        public E6POS ToRobotCS(Transform transform, int s = 0, int t = 0)
        {
            return ToSomeCS(transform, RobotCS, s, t);
        }

        /// <summary>
        /// Convert transform to E6POS in Workpiece Coordinate System
        /// </summary>
        /// <param name="transform">Transform to convert</param>
        /// <returns>E6POS in Workpiece Coordinate System</returns>
        public E6POS ToWorkpieceCS(Transform transform, int s = 0, int t = 0)
        {
            return ToSomeCS(transform, WorkpieceCS, s, t);
        }

        /// <summary>
        /// Convert transform to E6POS in a given Coordinate System
        /// </summary>
        /// <param name="transform">Transform to convert</param>
        /// <param name="coordinateSystem"></param>
        /// <returns>E6POS in the given Coordinate System</returns>
        public E6POS ToSomeCS(Transform transform, Transform coordinateSystem, int s = 0, int t = 0)
        {
            // TODO: CHECK THE INVERSTRANSFORM
            //Transform tInCS = coordinateSystem.InverseTransformPoint(transform);
            //tInCS.LocalPosition = tInCS.LocalPosition.Round();
            Vector3 aBC = QuatToABC(transform.GlobalRotation).Round();
            E6POS resE6POS = new E6POS()
            {
                X = transform.GlobalPosition.X,
                Y = transform.GlobalPosition.Y,
                Z = transform.GlobalPosition.Z,
                A = aBC.X,
                B = aBC.Y,
                C = aBC.Z,
                S = s,
                T = t
            };
            return resE6POS;
        }

        /// <summary>
        /// Convert quaternion to KUKA ABC
        /// </summary>
        /// <param name="q">quaternion to convert</param>
        /// <returns>KUKA ABC</returns>
        public static Vector3 QuatToABC(Quaternion q)
        {
            float valve = 0.5f;
            float yaw;
            float pitch;
            float roll;

            // north pole
            if (MathF.Abs(q.W * q.Y - q.Z * q.X - valve) < EPISION)
            {
                yaw = -2 * MathF.Atan2(q.X, q.W);
                pitch = PI / 2;
                roll = 0;
                return new Vector3(yaw * 180f / PI, pitch * 180f / PI, roll * 180f / PI);
            }

            // south pole
            if (MathF.Abs(q.W * q.Y - q.Z * q.X + valve) < EPISION)
            {
                yaw = 2 * MathF.Atan2(q.X, q.W);
                pitch = -PI / 2;
                roll = 0;
                return new Vector3(yaw * 180f / PI, pitch * 180f / PI, roll * 180f / PI);
            }

            // roll (x-axis rotation)
            float sinr_cosp = 2.0f * (q.W * q.X + q.Y * q.Z);
            float cosr_cosp = 1.0f - 2.0f * (q.X * q.X + q.Y * q.Y);
            roll = MathF.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            float sinp = +2.0f * (q.W * q.Y - q.Z * q.X);
            if (MathF.Abs(sinp) >= 1)
            {
                // use 90 degrees if out of range
                if (sinp > EPISION)
                {
                    pitch = PI / 2;
                }
                else
                {
                    pitch = -PI / 2;
                }
            }
            else
            {
                pitch = MathF.Asin(sinp);

            }

            // yaw (z-axis rotation)
            float siny_cosp = 2.0f * (q.W * q.Z + q.X * q.Y);
            float cosy_cosp = 1.0f - 2.0f * (q.Y * q.Y + q.Z * q.Z);
            yaw = MathF.Atan2(siny_cosp, cosy_cosp);

            return new Vector3(yaw * 180f / PI, pitch * 180f / PI, roll * 180f / PI);
        }

        public static Quaternion ABCToQuat(float A, float B, float C)
        {
            return ABCToQuat(new Vector3(A, B, C));
        }

        /// <summary>
        /// Convert KUKA ABC to quaternion
        /// </summary>
        /// <param name="aBC">KUKA ABC to convert</param>
        /// <returns>Result quaternion</returns>
        public static Quaternion ABCToQuat(Vector3 aBC)
        {
            float yaw = aBC.X * PI / 180;
            float pitch = aBC.Y * PI / 180;
            float roll = aBC.Z * PI / 180;
            float cy = MathF.Cos(yaw * 0.5f);
            float sy = MathF.Sin(yaw * 0.5f);
            float cp = MathF.Cos(pitch * 0.5f);
            float sp = MathF.Sin(pitch * 0.5f);
            float cr = MathF.Cos(roll * 0.5f);
            float sr = MathF.Sin(roll * 0.5f);

            Quaternion q;
            q.W = cy * cp * cr + sy * sp * sr;
            q.X = cy * cp * sr - sy * sp * cr;
            q.Y = sy * cp * sr + cy * sp * cr;
            q.Z = sy * cp * cr - cy * sp * sr;
            return q;
        }

        /// <summary>
        /// Convert quaternion to Euler in XYZ sequence
        /// </summary>
        /// <param name="q">quaternion to convert</param>
        /// <returns>Euler angle in XYZ sequence</returns>
        public static Vector3 QuatToEulerXYZ(Quaternion q)
        {
            // quaternion to rotation matrix
            Matrix4x4 rotationMatrix = Matrix4x4.CreateFromQuaternion(q);

            // rotation matrix to ABC
            float sb = rotationMatrix.M31;
            float cb;

            if (1f - sb * sb < EPISION)
            {
                cb = 0;
            }
            else
            {
                cb = MathF.Sqrt(1f - sb * sb);
            }
            float ca = rotationMatrix.M11;
            float sa = -rotationMatrix.M21;
            float cc = rotationMatrix.M33;
            float sc = -rotationMatrix.M32;
            if (MathF.Abs(rotationMatrix.M11) < 1E-7 && MathF.Abs(rotationMatrix.M21) < 1E-7)
            {
                cc = rotationMatrix.M22;
                sc = rotationMatrix.M23;
            }
            float a = MathF.Atan2(sa, ca) * 180f / PI;
            float b = MathF.Atan2(sb, cb) * 180f / PI;
            float c = MathF.Atan2(sc, cc) * 180f / PI;

            return new Vector3(a, b, c);
        }

        /// <summary>
        /// Convert E6POS to transform
        /// </summary>
        /// <param name="e6POS">E6POS to convert</param>
        /// <returns>Result transform</returns>
        public static Transform E6POSToTransform(E6POS e6POS)
        {
            return new Transform()
            {
                GlobalPosition = new Vector3(e6POS.X, e6POS.Y, e6POS.Z),
                GlobalRotation = ABCToQuat(new Vector3(e6POS.A, e6POS.B, e6POS.C)),
                SValue = e6POS.S,
                TValue = e6POS.T
            };
        }


        /// <summary>
        /// Convert transform to E6POS with default S and T
        /// </summary>
        /// <param name="transform">Transform to convet</param>
        /// <param name="s">Status, deault 0</param>
        /// <param name="t">Turn, deault 0</param>
        /// <returns>Result E6POS</returns>
        public static E6POS TransformToE6POS(Transform transform)
        {
            Vector3 aBC = QuatToABC(transform.GlobalRotation);
            return new E6POS()
            {
                X = transform.GlobalPosition.X,
                Y = transform.GlobalPosition.Y,
                Z = transform.GlobalPosition.Z,
                A = aBC.X,
                B = aBC.Y,
                C = aBC.Z,
                S = transform.SValue,
                T = transform.TValue
            };
        }

        /// <summary>
        /// Add tooltip to the input transform.
        /// </summary>
        /// <param name="t">Point the flange is to reach</param>
        /// <param name="flangecenter">Flangecenter transform</param>
        /// <param name="tooltip">Tooltip transform, must be measured in the same coordinate system with flangecenter</param>
        /// <returns>Point the flange is to reach when consider the tooltip</returns>
        public static Transform SetTooltip(Transform t, Transform flangecenter, Transform tooltip)
        {
            Transform flangecenterInTooltipCS = tooltip.InverseTransformPoint(flangecenter);
            Transform res = new Transform()
            {
                Parent = t,
                LocalPosition = flangecenterInTooltipCS.LocalPosition,
                LocalRotation = flangecenterInTooltipCS.LocalRotation
            };
            return res;
        }

        /// <summary>
        /// Unset tooltp to the input transform
        /// </summary>
        /// <param name="t">Point the flange is to reach when consider the tooltip</param>
        /// <param name="flangecenter">Flangecenter transform</param>
        /// <param name="tooltip">Tooltip transform, must be measured in the same coordinate system with flangecenter</param>
        /// <returns>Point the flange is to reach when get rid of the tooltip</returns>
        public static Transform UnsetTooltip(Transform t, Transform flangecenter, Transform tooltip)
        {
            Transform TooltipInFlangecenterCS = flangecenter.InverseTransformPoint(tooltip);
            Transform res = new Transform()
            {
                Parent = t,
                LocalPosition = TooltipInFlangecenterCS.LocalPosition,
                LocalRotation = TooltipInFlangecenterCS.LocalRotation
            };
            return res;
        }
    }
}
