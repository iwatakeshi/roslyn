// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
namespace Microsoft.CodeAnalysis.CSharp
{
    internal enum ArgumentAnalysisResultKind : byte
    {
        Normal,
        Expanded,
        NoCorrespondingParameter,
        FirstInvalid = NoCorrespondingParameter,
        NoCorrespondingNamedParameter,
        RequiredParameterMissing,
        NameUsedForPositional
    }
}