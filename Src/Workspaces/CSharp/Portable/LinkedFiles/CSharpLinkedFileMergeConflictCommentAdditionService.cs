﻿// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Composition;
using Microsoft.CodeAnalysis.Host.Mef;

namespace Microsoft.CodeAnalysis.CSharp
{
    [ExportLanguageService(typeof(ILinkedFileMergeConflictCommentAdditionService), LanguageNames.CSharp), Shared]
    internal sealed class CSharpLinkedFileMergeConflictCommentAdditionService : AbstractLinkedFileMergeConflictCommentAdditionService
    {
        internal override string GetConflictCommentText(string header, string beforeString, string afterString)
        {
            if (beforeString == null && afterString == null)
            {
                // Whitespace only
                return null;
            }
            else if (beforeString == null)
            {
                // New code
                return string.Format(@"
/* {0}
{1}
{2}
*/
",
                header,
                WorkspacesResources.AddedHeader,
                afterString);
            }
            else if (afterString == null)
            {
                // Removed code
                return string.Format(@"
/* {0}
{1}
{2}
*/
",
                header,
                WorkspacesResources.RemovedHeader,
                beforeString);
            }
            else
            {
                // Changed code
                return string.Format(@"
/* {0}
{1}
{2}
{3}
{4}
*/
",
                header,
                WorkspacesResources.BeforeHeader,
                beforeString,
                WorkspacesResources.AfterHeader,
                afterString);
            }
        }
    }
}