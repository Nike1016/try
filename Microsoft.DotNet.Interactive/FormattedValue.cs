﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.DotNet.Interactive
{
    public class FormattedValue
    {
        public FormattedValue(string mimeType, object value)
        {
            if (string.IsNullOrWhiteSpace(mimeType))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(mimeType));
            }

            MimeType = mimeType;
            Value = value;
        }

        public string MimeType { get; }

        public object Value { get; }
    }
}