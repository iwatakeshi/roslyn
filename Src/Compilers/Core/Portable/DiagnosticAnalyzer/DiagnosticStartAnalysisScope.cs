﻿// Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Microsoft.CodeAnalysis.Diagnostics
{
    /// <summary>
    /// Scope for setting up analyzers for an entire session.
    /// </summary>
    public abstract class SessionStartAnalysisScope
    {
        public abstract void RegisterSessionAnalyzer(DiagnosticAnalyzer analyzer);

        public abstract void RegisterCompilationStartAction(Action<CompilationStartAnalysisContext> action);
        public abstract void RegisterCompilationEndAction(Action<CompilationEndAnalysisContext> action);

        public abstract void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action);
        public abstract void RegisterSymbolAction(Action<SymbolAnalysisContext> action, params SymbolKind[] symbolKinds);

        public abstract void RegisterCodeBlockStartAction<TSyntaxKind>(Action<CodeBlockStartAnalysisContext<TSyntaxKind>> action);
        public abstract void RegisterCodeBlockEndAction<TSyntaxKind>(Action<CodeBlockEndAnalysisContext> action);

        public abstract void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action);
        public abstract void RegisterSyntaxNodeAction<TSyntaxKind>(Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds);
    }

    /// <summary>
    /// Scope for setting up analyzers for a compilation.
    /// </summary>
    public abstract class CompilationStartAnalysisScope
    {
        public abstract void RegisterCompilationEndAction(Action<CompilationEndAnalysisContext> action);

        public abstract void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action);
        public abstract void RegisterSymbolAction(Action<SymbolAnalysisContext> action, params SymbolKind[] symbolKinds);

        public abstract void RegisterCodeBlockStartAction<TSyntaxKind>(Action<CodeBlockStartAnalysisContext<TSyntaxKind>> action);
        public abstract void RegisterCodeBlockEndAction<TSyntaxKind>(Action<CodeBlockEndAnalysisContext> action);

        public abstract void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action);
        public abstract void RegisterSyntaxNodeAction<TSyntaxKind>(Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds);
    }

    /// <summary>
    /// Scope for setting up analyzers for a code block.
    /// </summary>
    public abstract class CodeBlockStartAnalysisScope<TSyntaxKind>
    {
        public abstract void RegisterCodeBlockEndAction(Action<CodeBlockEndAnalysisContext> action);

        public abstract void RegisterSyntaxNodeAction(Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds);
    }

    namespace Internal
    {
        // ToDo: Figure out how to make everything in this namespace internal.

        /// <summary>
        /// Scope for setting up analyzers for an entire session, automatically associating actions with analyzers.
        /// </summary>
        public sealed class AnalyzerSessionStartAnalysisScope : SessionStartAnalysisScope
        {
            private readonly DiagnosticAnalyzer analyzer;
            private readonly HostSessionStartAnalysisScope scope;

            public AnalyzerSessionStartAnalysisScope(DiagnosticAnalyzer analyzer, HostSessionStartAnalysisScope scope)
            {
                this.analyzer = analyzer;
                this.scope = scope;
            }

            public override void RegisterSessionAnalyzer(DiagnosticAnalyzer analyzer)
            {
                this.scope.RegisterSessionAnalyzer(analyzer);
            }

            public override void RegisterCompilationStartAction(Action<CompilationStartAnalysisContext> action)
            {
                this.scope.RegisterCompilationStartAction(this.analyzer, action);
            }

            public override void RegisterCompilationEndAction(Action<CompilationEndAnalysisContext> action)
            {
                this.scope.RegisterCompilationEndAction(this.analyzer, action);
            }

            public override void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action)
            {
                this.scope.RegisterSyntaxTreeAction(this.analyzer, action);
            }

            public override void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action)
            {
                this.scope.RegisterSemanticModelAction(this.analyzer, action);
            }

            public override void RegisterSymbolAction(Action<SymbolAnalysisContext> action, params SymbolKind[] symbolKinds)
            {
                this.scope.RegisterSymbolAction(this.analyzer, action, symbolKinds);
            }

            public override void RegisterCodeBlockStartAction<TSyntaxKind>(Action<CodeBlockStartAnalysisContext<TSyntaxKind>> action)
            {
                this.scope.RegisterCodeBlockStartAction<TSyntaxKind>(this.analyzer, action);
            }

            public override void RegisterCodeBlockEndAction<TSyntaxKind>(Action<CodeBlockEndAnalysisContext> action)
            {
                this.scope.RegisterCodeBlockEndAction<TSyntaxKind>(this.analyzer, action);
            }

            public override void RegisterSyntaxNodeAction<TSyntaxKind>(Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds)
            {
                this.scope.RegisterSyntaxNodeAction<TSyntaxKind>(this.analyzer, action, syntaxKinds);
            }
        }

        /// <summary>
        /// Scope for setting up analyzers for a compilation, automatically associating actions with analyzers.
        /// </summary>
        public sealed class AnalyzerCompilationStartAnalysisScope : CompilationStartAnalysisScope
        {
            private readonly DiagnosticAnalyzer analyzer;
            private readonly HostCompilationStartAnalysisScope scope;

            public AnalyzerCompilationStartAnalysisScope(DiagnosticAnalyzer analyzer, HostCompilationStartAnalysisScope scope)
            {
                this.analyzer = analyzer;
                this.scope = scope;
            }

            public override void RegisterCompilationEndAction(Action<CompilationEndAnalysisContext> action)
            {
                this.scope.RegisterCompilationEndAction(this.analyzer, action);
            }

            public override void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action)
            {
                this.scope.RegisterSyntaxTreeAction(this.analyzer, action);
            }

            public override void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action)
            {
                this.scope.RegisterSemanticModelAction(this.analyzer, action);
            }

            public override void RegisterSymbolAction(Action<SymbolAnalysisContext> action, params SymbolKind[] symbolKinds)
            {
                this.scope.RegisterSymbolAction(this.analyzer, action, symbolKinds);
            }

            public override void RegisterCodeBlockStartAction<TSyntaxKind>(Action<CodeBlockStartAnalysisContext<TSyntaxKind>> action)
            {
                this.scope.RegisterCodeBlockStartAction<TSyntaxKind>(this.analyzer, action);
            }

            public override void RegisterCodeBlockEndAction<TSyntaxKind>(Action<CodeBlockEndAnalysisContext> action)
            {
                this.scope.RegisterCodeBlockEndAction<TSyntaxKind>(this.analyzer, action);
            }

            public override void RegisterSyntaxNodeAction<TSyntaxKind>(Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds)
            {
                this.scope.RegisterSyntaxNodeAction<TSyntaxKind>(this.analyzer, action, syntaxKinds);
            }
        }

        /// <summary>
        /// Scope for setting up analyzers for a code block, automatically associating actions with analyzers.
        /// </summary>
        public sealed class AnalyzerCodeBlockStartAnalysisScope<TSyntaxKind> : CodeBlockStartAnalysisScope<TSyntaxKind>
        {
            private readonly DiagnosticAnalyzer analyzer;
            private readonly HostCodeBlockStartAnalysisScope<TSyntaxKind> scope;

            internal AnalyzerCodeBlockStartAnalysisScope(DiagnosticAnalyzer analyzer, HostCodeBlockStartAnalysisScope<TSyntaxKind> scope)
            {
                this.analyzer = analyzer;
                this.scope = scope;
            }

            public override void RegisterCodeBlockEndAction(Action<CodeBlockEndAnalysisContext> action)
            {
                this.scope.RegisterCodeBlockEndAction(this.analyzer, action);
            }

            public override void RegisterSyntaxNodeAction(Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds)
            {
                this.scope.RegisterSyntaxNodeAction(this.analyzer, action, syntaxKinds);
            }
        }

        /// <summary>
        /// Scope for setting up analyzers for an entire session, capable of retrieving the actions.
        /// </summary>
        public sealed class HostSessionStartAnalysisScope : HostAnalysisScope
        {
            private ImmutableArray<DiagnosticAnalyzer> sessionAnalyzers = ImmutableArray<DiagnosticAnalyzer>.Empty;
            private ImmutableArray<CompilationStartAnalyzerAction> compilationStartActions = ImmutableArray<CompilationStartAnalyzerAction>.Empty;

            public ImmutableArray<DiagnosticAnalyzer> SessionAnalyzers
            {
                get { return this.sessionAnalyzers; }
            }

            public ImmutableArray<CompilationStartAnalyzerAction> CompilationStartActions
            {
                get { return this.compilationStartActions; }
            }

            public void RegisterSessionAnalyzer(DiagnosticAnalyzer analyzer)
            {
                this.sessionAnalyzers = this.sessionAnalyzers.Add(analyzer);
            }

            public void RegisterCompilationStartAction(DiagnosticAnalyzer analyzer, Action<CompilationStartAnalysisContext> action)
            {
                CompilationStartAnalyzerAction analyzerAction = new CompilationStartAnalyzerAction(action, analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddCompilationStartAction(analyzerAction);
                this.compilationStartActions = this.compilationStartActions.Add(analyzerAction);
            }
        }

        /// <summary>
        /// Scope for setting up analyzers for a compilation, capable of retrieving the actions.
        /// </summary>
        public sealed class HostCompilationStartAnalysisScope : HostAnalysisScope
        {
            private readonly HostSessionStartAnalysisScope sessionScope;

            public HostSessionStartAnalysisScope SessionScope
            {
                get { return this.sessionScope; }
            }

            public override ImmutableArray<CompilationEndAnalyzerAction> CompilationEndActions
            {
                get { return base.CompilationEndActions.Concat(this.sessionScope.CompilationEndActions); }
            }

            public override ImmutableArray<SemanticModelAnalyzerAction> SemanticModelActions
            {
                get { return base.SemanticModelActions.Concat(this.sessionScope.SemanticModelActions); }
            }

            public override ImmutableArray<SyntaxTreeAnalyzerAction> SyntaxTreeActions
            {
                get { return base.SyntaxTreeActions.Concat(this.sessionScope.SyntaxTreeActions); }
            }

            public override ImmutableArray<SymbolAnalyzerAction> SymbolActions
            {
                get { return base.SymbolActions.Concat(this.sessionScope.SymbolActions); }
            }

            public HostCompilationStartAnalysisScope(HostSessionStartAnalysisScope sessionScope)
            {
                this.sessionScope = sessionScope;
            }

            public override bool HasCodeBlockStartActions<TSyntaxKind>()
            {
                return
                    base.HasCodeBlockStartActions<TSyntaxKind>() ||
                    this.sessionScope.HasCodeBlockStartActions<TSyntaxKind>();
            }

            public override ImmutableArray<CodeBlockStartAnalyzerAction<TSyntaxKind>> GetCodeBlockStartActions<TSyntaxKind>()
            {
                return base.GetCodeBlockStartActions<TSyntaxKind>().Concat(this.sessionScope.GetCodeBlockStartActions<TSyntaxKind>());
            }

            public override bool HasCodeBlockEndActions<TSyntaxKind>()
            {
                return
                    base.HasCodeBlockEndActions<TSyntaxKind>() ||
                    this.sessionScope.HasCodeBlockEndActions<TSyntaxKind>();
            }

            public override ImmutableArray<CodeBlockEndAnalyzerAction<TSyntaxKind>> GetCodeBlockEndActions<TSyntaxKind>()
            {
                return base.GetCodeBlockEndActions<TSyntaxKind>().Concat(this.sessionScope.GetCodeBlockEndActions<TSyntaxKind>());
            }

            public override ImmutableArray<SyntaxNodeAnalyzerAction<TSyntaxKind>> GetSyntaxNodeActions<TSyntaxKind>()
            {
                return base.GetSyntaxNodeActions<TSyntaxKind>().Concat(this.sessionScope.GetSyntaxNodeActions<TSyntaxKind>());
            }

            public override AnalyzerActions GetAnalyzerActions(DiagnosticAnalyzer analyzer)
            {
                AnalyzerActions compilationActions = base.GetAnalyzerActions(analyzer);
                AnalyzerActions sessionActions = this.sessionScope.GetAnalyzerActions(analyzer);

                if (sessionActions == null)
                {
                    return compilationActions;
                }

                if (compilationActions == null)
                {
                    return sessionActions;
                }

                return compilationActions.Append(sessionActions);
            }
        }

        /// <summary>
        /// Scope for setting up analyzers for a code block, capable of retrieving the actions.
        /// </summary>
        public sealed class HostCodeBlockStartAnalysisScope<TSyntaxKind>
        {
            private ImmutableArray<CodeBlockEndAnalyzerAction<TSyntaxKind>> codeBlockEndActions = ImmutableArray<CodeBlockEndAnalyzerAction<TSyntaxKind>>.Empty;
            private ImmutableArray<SyntaxNodeAnalyzerAction<TSyntaxKind>> syntaxNodeActions = ImmutableArray<SyntaxNodeAnalyzerAction<TSyntaxKind>>.Empty;

            public ImmutableArray<CodeBlockEndAnalyzerAction<TSyntaxKind>> CodeBlockEndActions
            {
                get { return this.codeBlockEndActions; }
            }

            public ImmutableArray<SyntaxNodeAnalyzerAction<TSyntaxKind>> SyntaxNodeActions
            {
                get { return this.syntaxNodeActions; }
            }

            internal HostCodeBlockStartAnalysisScope()
            {
            }

            public void RegisterCodeBlockEndAction(DiagnosticAnalyzer analyzer, Action<CodeBlockEndAnalysisContext> action)
            {
                this.codeBlockEndActions = this.codeBlockEndActions.Add(new CodeBlockEndAnalyzerAction<TSyntaxKind>(action, analyzer));
            }

            public void RegisterSyntaxNodeAction(DiagnosticAnalyzer analyzer, Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds)
            {
                this.syntaxNodeActions = this.syntaxNodeActions.Add(new SyntaxNodeAnalyzerAction<TSyntaxKind>(action, ImmutableArray.Create<TSyntaxKind>(syntaxKinds), analyzer));
            }
        }

        public abstract class HostAnalysisScope
        {
            private ImmutableArray<CompilationEndAnalyzerAction> compilationEndActions = ImmutableArray<CompilationEndAnalyzerAction>.Empty;
            private ImmutableArray<SemanticModelAnalyzerAction> semanticModelActions = ImmutableArray<SemanticModelAnalyzerAction>.Empty;
            private ImmutableArray<SyntaxTreeAnalyzerAction> syntaxTreeActions = ImmutableArray<SyntaxTreeAnalyzerAction>.Empty;
            private ImmutableArray<SymbolAnalyzerAction> symbolActions = ImmutableArray<SymbolAnalyzerAction>.Empty;
            private ImmutableArray<AnalyzerAction> codeBlockStartActions = ImmutableArray<AnalyzerAction>.Empty;
            private ImmutableArray<AnalyzerAction> codeBlockEndActions = ImmutableArray<AnalyzerAction>.Empty;
            private ImmutableArray<AnalyzerAction> syntaxNodeActions = ImmutableArray<AnalyzerAction>.Empty;
            private readonly Dictionary<DiagnosticAnalyzer, AnalyzerActions> analyzerActions = new Dictionary<DiagnosticAnalyzer, AnalyzerActions>();

            public virtual ImmutableArray<CompilationEndAnalyzerAction> CompilationEndActions
            {
                get { return this.compilationEndActions; }
            }

            public virtual ImmutableArray<SemanticModelAnalyzerAction> SemanticModelActions
            {
                get { return this.semanticModelActions; }
            }

            public virtual ImmutableArray<SyntaxTreeAnalyzerAction> SyntaxTreeActions
            {
                get { return this.syntaxTreeActions; }
            }

            public virtual ImmutableArray<SymbolAnalyzerAction> SymbolActions
            {
                get { return this.symbolActions; }
            }

            public virtual bool HasCodeBlockStartActions<TSyntaxKind>()
            {
                return this.codeBlockStartActions.OfType<CodeBlockStartAnalyzerAction<TSyntaxKind>>().Any();
            }

            public virtual ImmutableArray<CodeBlockStartAnalyzerAction<TSyntaxKind>> GetCodeBlockStartActions<TSyntaxKind>()
            {
                return this.codeBlockStartActions.OfType<CodeBlockStartAnalyzerAction<TSyntaxKind>>().AsImmutable();
            }

            public virtual bool HasCodeBlockEndActions<TSyntaxKind>()
            {
                return this.codeBlockEndActions.OfType<CodeBlockEndAnalyzerAction<TSyntaxKind>>().Any();
            }

            public virtual ImmutableArray<CodeBlockEndAnalyzerAction<TSyntaxKind>> GetCodeBlockEndActions<TSyntaxKind>()
            {
                return this.codeBlockEndActions.OfType<CodeBlockEndAnalyzerAction<TSyntaxKind>>().AsImmutable();
            }

            public virtual ImmutableArray<SyntaxNodeAnalyzerAction<TSyntaxKind>> GetSyntaxNodeActions<TSyntaxKind>()
            {
                return this.syntaxNodeActions.OfType<SyntaxNodeAnalyzerAction<TSyntaxKind>>().AsImmutable();
            }

            public virtual AnalyzerActions GetAnalyzerActions(DiagnosticAnalyzer analyzer)
            {
                AnalyzerActions actions;
                this.analyzerActions.TryGetValue(analyzer, out actions);
                return actions;
            }

            public void RegisterCompilationEndAction(DiagnosticAnalyzer analyzer, Action<CompilationEndAnalysisContext> action)
            {
                CompilationEndAnalyzerAction analyzerAction = new CompilationEndAnalyzerAction(action, analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddCompilationEndAction(analyzerAction);
                this.compilationEndActions = this.compilationEndActions.Add(analyzerAction);
            }

            public void RegisterSemanticModelAction(DiagnosticAnalyzer analyzer, Action<SemanticModelAnalysisContext> action)
            {
                SemanticModelAnalyzerAction analyzerAction = new SemanticModelAnalyzerAction(action, analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddSemanticModelAction(analyzerAction);
                this.semanticModelActions = this.semanticModelActions.Add(analyzerAction);
            }

            public void RegisterSyntaxTreeAction(DiagnosticAnalyzer analyzer, Action<SyntaxTreeAnalysisContext> action)
            {
                SyntaxTreeAnalyzerAction analyzerAction = new SyntaxTreeAnalyzerAction(action, analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddSyntaxTreeAction(analyzerAction);
                this.syntaxTreeActions = this.syntaxTreeActions.Add(analyzerAction);
            }

            public void RegisterSymbolAction(DiagnosticAnalyzer analyzer, Action<SymbolAnalysisContext> action, params SymbolKind[] symbolKinds)
            {
                SymbolAnalyzerAction analyzerAction = new SymbolAnalyzerAction(action, ImmutableArray.Create<SymbolKind>(symbolKinds), analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddSymbolAction(analyzerAction);
                this.symbolActions = this.symbolActions.Add(analyzerAction);
            }

            public void RegisterCodeBlockStartAction<TSyntaxKind>(DiagnosticAnalyzer analyzer, Action<CodeBlockStartAnalysisContext<TSyntaxKind>> action)
            {
                CodeBlockStartAnalyzerAction<TSyntaxKind> analyzerAction = new CodeBlockStartAnalyzerAction<TSyntaxKind>(action, analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddCodeBlockStartAction(analyzerAction);
                this.codeBlockStartActions = this.codeBlockStartActions.Add(analyzerAction);
            }

            public void RegisterCodeBlockEndAction<TSyntaxKind>(DiagnosticAnalyzer analyzer, Action<CodeBlockEndAnalysisContext> action)
            {
                CodeBlockEndAnalyzerAction<TSyntaxKind> analyzerAction = new CodeBlockEndAnalyzerAction<TSyntaxKind>(action, analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddCodeBlockEndAction(analyzerAction);
                this.codeBlockEndActions = this.codeBlockEndActions.Add(analyzerAction);
            }

            public void RegisterSyntaxNodeAction<TSyntaxKind>(DiagnosticAnalyzer analyzer, Action<SyntaxNodeAnalysisContext> action, params TSyntaxKind[] syntaxKinds)
            {
                SyntaxNodeAnalyzerAction<TSyntaxKind> analyzerAction = new SyntaxNodeAnalyzerAction<TSyntaxKind>(action, ImmutableArray.Create<TSyntaxKind>(syntaxKinds), analyzer);
                this.GetOrCreateAnalyzerActions(analyzer).AddSyntaxNodeAction(analyzerAction);
                this.syntaxNodeActions = this.syntaxNodeActions.Add(analyzerAction);
            }

            protected AnalyzerActions GetOrCreateAnalyzerActions(DiagnosticAnalyzer analyzer)
            {
                AnalyzerActions actions;
                if (!this.analyzerActions.TryGetValue(analyzer, out actions))
                {
                    actions = new AnalyzerActions();
                    this.analyzerActions[analyzer] = actions;
                }

                return actions;
            }
        }

        // ToDo: AnalyzerActions, and all of the mechanism around it, can be eliminated if the IDE diagnostic analyzer driver
        // moves from an analyzer-centric model to an action-centric model. For example, the driver would need to stop asking
        // if a particular analyzer can analyze syntax trees, and instead ask if any syntax tree actions are present. Also,
        // the driver needs to apply all relevant actions rather then applying the actions of individual analyzers.
        /// <summary>
        /// Actions registered by a particular analyzer.
        /// </summary>
        public sealed class AnalyzerActions
        {
            private ImmutableArray<CompilationStartAnalyzerAction> compilationStartActions = ImmutableArray<CompilationStartAnalyzerAction>.Empty;
            private ImmutableArray<CompilationEndAnalyzerAction> compilationEndActions = ImmutableArray<CompilationEndAnalyzerAction>.Empty;
            private ImmutableArray<SyntaxTreeAnalyzerAction> syntaxTreeActions = ImmutableArray<SyntaxTreeAnalyzerAction>.Empty;
            private ImmutableArray<SemanticModelAnalyzerAction> semanticModelActions = ImmutableArray<SemanticModelAnalyzerAction>.Empty;
            private ImmutableArray<SymbolAnalyzerAction> symbolActions = ImmutableArray<SymbolAnalyzerAction>.Empty;
            private ImmutableArray<AnalyzerAction> codeBlockStartActions = ImmutableArray<AnalyzerAction>.Empty;
            private ImmutableArray<AnalyzerAction> codeBlockEndActions = ImmutableArray<AnalyzerAction>.Empty;
            private ImmutableArray<AnalyzerAction> syntaxNodeActions = ImmutableArray<AnalyzerAction>.Empty;

            public ImmutableArray<CompilationStartAnalyzerAction> CompilationStartActions
            {
                get { return this.compilationStartActions; }
            }

            public ImmutableArray<CompilationEndAnalyzerAction> CompilationEndActions
            {
                get { return this.compilationEndActions; }
            }

            public ImmutableArray<SyntaxTreeAnalyzerAction> SyntaxTreeActions
            {
                get { return this.syntaxTreeActions; }
            }

            public ImmutableArray<SemanticModelAnalyzerAction> SemanticModelActions
            {
                get { return this.semanticModelActions; }
            }

            public ImmutableArray<SymbolAnalyzerAction> SymbolActions
            {
                get { return this.symbolActions; }
            }

            public ImmutableArray<AnalyzerAction> CodeBlockStartActions
            {
                get { return this.codeBlockStartActions; }
            }

            public ImmutableArray<AnalyzerAction> CodeBlockEndActions
            {
                get { return this.codeBlockEndActions; }
            }

            public ImmutableArray<AnalyzerAction> SyntaxNodeActions
            {
                get { return this.syntaxNodeActions; }
            }

            internal AnalyzerActions()
            {
            }

            public void AddCompilationStartAction(CompilationStartAnalyzerAction action)
            {
                this.compilationStartActions = this.compilationStartActions.Add(action);
            }

            public void AddCompilationEndAction(CompilationEndAnalyzerAction action)
            {
                this.compilationEndActions = this.compilationEndActions.Add(action);
            }

            public void AddSyntaxTreeAction(SyntaxTreeAnalyzerAction action)
            {
                this.syntaxTreeActions = this.syntaxTreeActions.Add(action);
            }

            public void AddSemanticModelAction(SemanticModelAnalyzerAction action)
            {
                this.semanticModelActions = this.semanticModelActions.Add(action);
            }

            public void AddSymbolAction(SymbolAnalyzerAction action)
            {
                this.symbolActions = this.symbolActions.Add(action);
            }

            public void AddCodeBlockStartAction<TSyntaxKind>(CodeBlockStartAnalyzerAction<TSyntaxKind> action)
            {
                this.codeBlockStartActions = this.codeBlockStartActions.Add(action);
            }

            public void AddCodeBlockEndAction<TSyntaxKind>(CodeBlockEndAnalyzerAction<TSyntaxKind> action)
            {
                this.codeBlockEndActions = this.codeBlockEndActions.Add(action);
            }

            public void AddSyntaxNodeAction<TSyntaxKind>(SyntaxNodeAnalyzerAction<TSyntaxKind> action)
            {
                this.syntaxNodeActions = this.syntaxNodeActions.Add(action);
            }

            public ImmutableArray<CodeBlockStartAnalyzerAction<TSyntaxKind>> GetCodeBlockStartActions<TSyntaxKind>()
            {
                return this.codeBlockStartActions.OfType<CodeBlockStartAnalyzerAction<TSyntaxKind>>().ToImmutableArray();
            }

            public ImmutableArray<CodeBlockEndAnalyzerAction<TSyntaxKind>> GetCodeBlockEndActions<TSyntaxKind>()
            {
                return this.codeBlockEndActions.OfType<CodeBlockEndAnalyzerAction<TSyntaxKind>>().ToImmutableArray();
            }

            public ImmutableArray<SyntaxNodeAnalyzerAction<TSyntaxKind>> GetSyntaxNodeActions<TSyntaxKind>()
            {
                return this.syntaxNodeActions.OfType<SyntaxNodeAnalyzerAction<TSyntaxKind>>().ToImmutableArray();
            }

            public AnalyzerActions Append(AnalyzerActions otherActions)
            {
                AnalyzerActions actions = new AnalyzerActions();
                actions.compilationStartActions = this.compilationStartActions.AddRange(otherActions.compilationStartActions);
                actions.compilationEndActions = this.compilationEndActions.AddRange(otherActions.compilationEndActions);
                actions.syntaxTreeActions = this.syntaxTreeActions.AddRange(otherActions.syntaxTreeActions);
                actions.semanticModelActions = this.semanticModelActions.AddRange(otherActions.semanticModelActions);
                actions.symbolActions = this.symbolActions.AddRange(otherActions.symbolActions);
                actions.codeBlockStartActions = this.codeBlockStartActions.AddRange(otherActions.codeBlockStartActions);
                actions.codeBlockEndActions = this.codeBlockEndActions.AddRange(otherActions.codeBlockEndActions);
                actions.syntaxNodeActions = this.syntaxNodeActions.AddRange(otherActions.syntaxNodeActions);

                return actions;
            }
        }
    }
}
