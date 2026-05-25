using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.OffsetsCalculator
{
    public class MatrixComparator : IEqualityComparer<Matrix4x4>
    {
        private readonly float _epsilon;

        public MatrixComparator(float epsilon)
        {
            _epsilon = epsilon;
        }

        public bool Equals(Matrix4x4 x, Matrix4x4 y)
        {
            for (int i = 0; i < 16; i++)
            {
                if (Mathf.Abs(x[i] - y[i]) > _epsilon)
                    return false;
            }

            return true;
        }

        public int GetHashCode(Matrix4x4 obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.m00);
            hashCode.Add(obj.m10);
            hashCode.Add(obj.m20);
            hashCode.Add(obj.m30);
            hashCode.Add(obj.m01);
            hashCode.Add(obj.m11);
            hashCode.Add(obj.m21);
            hashCode.Add(obj.m31);
            hashCode.Add(obj.m02);
            hashCode.Add(obj.m12);
            hashCode.Add(obj.m22);
            hashCode.Add(obj.m32);
            hashCode.Add(obj.m03);
            hashCode.Add(obj.m13);
            hashCode.Add(obj.m23);
            hashCode.Add(obj.m33);
            return hashCode.ToHashCode();
        }
    }
}