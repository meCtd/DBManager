﻿using System;
using System.Collections.Generic;

namespace DBManager.Default.DataType
{
    public abstract class DataTypeFactory : IDataTypeFactory
    {
        protected abstract Dictionary<string, TypeDescriptor> TypeDescriptorsMap { get; }

        public TypeDescriptor CreateTypeDescriptor(string typeName)
        {
            if (!TypeDescriptorsMap.TryGetValue(typeName, out var descriptor))
                throw new ArgumentException("Unknown data type");

            return descriptor;
        }
    }
}
