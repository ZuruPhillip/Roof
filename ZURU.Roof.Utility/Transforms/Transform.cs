using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZURU.Roof.Utility.Maths;

namespace ZURU.Roof.Utility.Transforms
{

    /// <summary>
    /// Transform class for managing 3D Geometry of points
    /// Includes implementation for Parental Hierachy of points and both local and global position and rotation setting.
    /// When the position or rotation is updated for this Transform or the Parent transform the values are automatically updated using the functions in this class.
    /// </summary>
    public sealed class Transform
    {

        public string _id;

        public List<Transform> children;

        //TODO look at adding locking for thread safety if required later

        /// <summary>
        /// Parent Variable for storing current parent Transform for this transform
        /// </summary>
        private Transform _parent;
        public Transform Parent
        {
            get { return _parent; }
            set
            {
                if (value != _parent)
                {
                    if (_parent != null)
                    {
                        _parent.children.Remove(this);
                    }

                    _parent = value;
                    if (value != null)
                    {
                        _parent.children.Add(this);
                    }
                    ParentChanged();
                }
            }
        }

        /// <summary>
        /// localPosition of this Transform
        /// </summary>
        private Vector3 _localPosition;
        public Vector3 LocalPosition
        {
            get { return _localPosition; }
            set
            {
                if (value != _localPosition)
                {
                    _localPosition = value;
                    UpdateGlobalPosition();
                }
            }
        }

        /// <summary>
        /// Global Position of this Transform
        /// </summary>
        private Vector3 _globalPosition;
        public Vector3 GlobalPosition
        {
            get { return _globalPosition; }
            set
            {
                if (value != _globalPosition)
                {
                    _globalPosition = value;
                    UpdateLocalPosition();
                }
            }
        }

        /// <summary>
        /// Local Rotation for this transform
        /// </summary>
        private Quaternion _localRotation;
        public Quaternion LocalRotation
        {
            get
            {
                return _localRotation;
            }
            set
            {
                if (value != _localRotation)
                {
                    _localRotation = value;
                    UpdateGlobalRotation();
                }
            }
        }

        /// <summary>
        /// Global Rotation of this Transform
        /// </summary>
        private Quaternion _globalRotation;
        public Quaternion GlobalRotation
        {
            get
            {
                return _globalRotation;
            }
            set
            {
                if (value != _globalRotation)
                {
                    _globalRotation = value;
                    UpdateLocalRotation();
                }
            }
        }

        private int _sValue;
        private int _tValue;

        public int SValue { get => _sValue; set => _sValue = value; }
        public int TValue { get => _tValue; set => _tValue = value; }

        /// <summary>
        /// Empty Instatiator for this transform
        /// </summary>
        public Transform()
        {
            _localPosition = Vector3.Zero;
            _globalPosition = Vector3.Zero;
            _localRotation = Quaternion.Identity;
            _globalRotation = Quaternion.Identity;
            children = new List<Transform>();
            _id = "";
            _parent = null;
            _sValue = default;
            _tValue = default;
        }

        /// <summary>
        /// Transform Instantiator with set ID
        /// </summary>
        /// <param name="id">ID for this Transform</param>
        public Transform(string id)
        {
            _localPosition = Vector3.Zero;
            _globalPosition = Vector3.Zero;
            _localRotation = Quaternion.Identity;
            _globalRotation = Quaternion.Identity;
            children = new List<Transform>();
            _parent = null;
            _id = id;
        }

        /// <summary>
        /// Return String describing the local position of this Transform
        /// </summary>
        /// <returns>String of Local Transform</returns>
        public string LocalToString()
        {
            string str = "Position:\t" + LocalPosition.ToString() + "\tRotation:\t" + LocalRotation.ToString();
            return str;
        }

        /// <summary>
        /// Return String describing the global position of this Transform
        /// </summary>
        /// <returns>String of Global Transform</returns>
        public string GlobalToString()
        {
            string str = "Position:\t" + GlobalPosition.ToString() + "\tRotation:\t" + GlobalRotation.ToString();
            return str;
        }

        /// <summary>   Determines whether the specified object is equal to the current object. </summary>
        ///
        /// <remarks>   Jeffery Ye, 2019/1/26. </remarks>
        ///
        /// <param name="obj">  The object to compare with the current object. </param>
        ///
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Transform);
        }

        public bool Equals(Transform t)
        {
            if (t is null) return false;
            if (ReferenceEquals(this, t)) return true;
            return GlobalPosition == t.GlobalPosition
                && GlobalRotation == t.GlobalRotation;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashBase = 17;
                int hashMultipler = 23;
                int hash = hashBase;
                hash = (hash * hashMultipler) ^ GlobalPosition.GetHashCode();
                hash = (hash * hashMultipler) ^ GlobalRotation.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Transform left, Transform right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Transform left, Transform right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Update the Global position of this transform based on current local position and parent Tree
        /// </summary>
        private void UpdateGlobalPosition()
        {
            _globalPosition = this.GetGlobalPosition().GlobalPosition;
            UpdateChildren();
        }

        /// <summary>
        /// Update the Global Rotation of this transform based on current local rotation and parent tree
        /// </summary>
        private void UpdateGlobalRotation()
        {
            _globalRotation = this.GetGlobalPosition().GlobalRotation;
            UpdateChildren();
        }

        /// <summary>
        /// Update the Local position of this Transform based on the current Global Position
        /// </summary>
        private void UpdateLocalPosition()
        {
            if (_parent == null)
            {
                _localPosition = _globalPosition;
            }
            else
            {
                _localPosition = _parent.InverseTransformPoint(this).GlobalPosition;
            }
            UpdateChildren();
        }

        /// <summary>
        /// Update the Local Rotation of this Transform 
        /// </summary>
        private void UpdateLocalRotation()
        {
            if (_parent == null)
            {
                _localRotation = _globalRotation;
            }
            else
            {
                _localRotation = _parent.InverseTransformPoint(this).GlobalRotation;
            }
            UpdateChildren();
        }

        /// <summary>
        /// Function for updating positions when the parent is modified
        /// </summary>
        private void ParentChanged()
        {
            //When the Parent is Changed the Global Position Remains Constant and the Local needs to be updated
            UpdateLocalPosition();
            UpdateLocalRotation();
        }

        /// <summary>
        /// Function to update all the children of this Transform when it is moved.
        /// </summary>
        private void UpdateChildren()
        {
            foreach (Transform c in children)
            {
                c.ParentMoved();
            }
        }

        /// <summary>
        /// Function which updates the positiong of this Transform when the parent is modified and modifies its children as
        /// </summary>
        public void ParentMoved()
        {
            //When the Parent is moved globally or locally the Global Position of all children must be updated
            //When Parent is moved the Local position will stay the same and the Global must be updated
            UpdateGlobalPosition();
            UpdateGlobalRotation();
            foreach (Transform c in children)
            {
                c.ParentMoved();
            }
        }
    }

    /// <summary>
    /// Extension functions to help Transform class operations.
    /// </summary>
    public static class TransformExtensions
    {

        /// <summary>
        /// Function to calculate the global position of a Transform
        /// </summary>
        /// <param name="t1">Transform to find global position for</param>
        /// <returns>Global Position of this Transform</returns>
        public static Transform GetGlobalPosition(this Transform t1)
        {
            Transform globalPos = new Transform();
            //Transform currentTransform = t1;
            List<Transform> ParentTree = new List<Transform>();



            if (t1.Parent == null)
            {
                //If Parent is null then GlobalPosition = LocalPosition
                globalPos.GlobalPosition = t1.LocalPosition;
                globalPos.GlobalRotation = t1.LocalRotation;
                return globalPos;
            }
            else
            {
                Transform currentTransform = t1;

                //Building the Parent Tree
                while (currentTransform.Parent != null)
                {
                    ParentTree.Add(currentTransform);
                    currentTransform = currentTransform.Parent;
                }
                //Add top Parent Node
                ParentTree.Add(currentTransform);

                //Reversing Parent Tree to start at global origin
                ParentTree.Reverse();

                foreach (Transform t in ParentTree)
                {
                    //If Parent is null add current local position and rotation
                    if (t.Parent == null)
                    {
                        globalPos.GlobalPosition += t.LocalPosition;
                        globalPos.GlobalRotation = globalPos.GlobalRotation * t.LocalRotation;
                    }
                    else
                    {
                        //Calculate GLobal Positional Offset between current Transfrom and Parent taking into account rotation
                        Vector3 offset = Vector3.Transform(t.LocalPosition, Quaternion.Inverse(globalPos.GlobalRotation));
                        globalPos.GlobalPosition += Vector3.Transform(t.LocalPosition, Quaternion.Inverse(globalPos.GlobalRotation));
                        //Add rotation to Global Quaternion
                        globalPos.GlobalRotation = globalPos.GlobalRotation * t.LocalRotation;
                    }
                }

                globalPos.GlobalPosition = globalPos.GlobalPosition.Round();
                //globalPos.rotation = globalPos.rotation.Round();
                return globalPos;
            }
        }

        /// <summary>
        /// Get the Transform describing the offset from T1 to T2
        /// </summary>
        /// <param name="t1">"Origin" Transform to measure from</param>
        /// <param name="t2">Transform to measure to</param>
        /// <returns>Transform describing the distance and rotation from T1 to T2</returns>
        public static Transform InverseTransformPoint(this Transform t1, Transform t2)
        {
            Transform offset = new Transform();
            //Calculate Global Position of t1 and t2 to be compared
            //Transform t1Global = t1.GetGlobalPosition();
            //Transform t2Global = t2.GetGlobalPosition();
            //Transform t1Global = new Transform();
            //t1Global.GlobalPosition = t1.GlobalPosition;
            //Transform t2Global t2.GetGlobalPosition;


            //Calculate the positional offset between each of the positions
            offset.GlobalPosition = Vector3.Transform(t2.GlobalPosition - t1.GlobalPosition, Quaternion.Inverse(t1.GlobalRotation));
            offset.GlobalPosition = offset.GlobalPosition.Round();

            //Calculate the difference in rotation between to two
            offset.GlobalRotation = Quaternion.Divide(t2.GlobalRotation, t1.GlobalRotation);

            return offset;
        }
    }
}
