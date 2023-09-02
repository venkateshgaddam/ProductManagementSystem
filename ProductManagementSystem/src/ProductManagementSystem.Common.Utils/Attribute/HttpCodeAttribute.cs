﻿using System;

namespace ProductManagementSystem.Common.Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class HttpCodeAttribute : Attribute, ICcpAttribute<int>
    {
        public HttpCodeAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}