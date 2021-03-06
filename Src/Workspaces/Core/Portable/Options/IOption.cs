﻿// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;

namespace Microsoft.CodeAnalysis.Options
{
    public interface IOption
    {
        string Feature { get; }
        string Name { get; }
        Type Type { get; }
        object DefaultValue { get; }
        bool IsPerLanguage { get; }
    }
}
