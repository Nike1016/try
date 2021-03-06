﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DotNet.Interactive.Events
{
    public class NuGetPackageAdded : KernelEventBase
    {
        public NuGetPackageAdded(NugetPackageReference packageReference)
        {
            PackageReference = packageReference;
        }

        public NugetPackageReference PackageReference { get; }
    }

}