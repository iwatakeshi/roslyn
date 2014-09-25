﻿' Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Roslyn.Test.Utilities

Namespace Microsoft.CodeAnalysis.VisualBasic.UnitTests.Semantics

    Public Class UnstructuredExceptionHandling
        Inherits BasicTestBase

        <Fact()>
        Public Sub TestOnError_Goto0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
        On Error GoTo 0
        System.Console.WriteLine(1)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size       85 (0x55)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.0
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.1
  IL_0008:  call       "Sub System.Console.WriteLine(Integer)"
  IL_000d:  leave.s    IL_004c
  IL_000f:  ldc.i4.m1
  IL_0010:  stloc.1
  IL_0011:  ldloc.0
  IL_0012:  switch    (
  IL_001f,
  IL_001f)
  IL_001f:  leave.s    IL_0041
}
  filter
{
  IL_0021:  isinst     "System.Exception"
  IL_0026:  ldnull
  IL_0027:  cgt.un
  IL_0029:  ldloc.0
  IL_002a:  ldc.i4.0
  IL_002b:  cgt.un
  IL_002d:  and
  IL_002e:  ldloc.1
  IL_002f:  ldc.i4.0
  IL_0030:  ceq
  IL_0032:  and
  IL_0033:  endfilter
}  // end filter
{  // handler
  IL_0035:  castclass  "System.Exception"
  IL_003a:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_003f:  leave.s    IL_000f
}
  IL_0041:  ldc.i4     0x800a0033
  IL_0046:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_004b:  throw
  IL_004c:  ldloc.1
  IL_004d:  brfalse.s  IL_0054
  IL_004f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0054:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size       95 (0x5f)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Boolean V_2)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.0
  IL_0009:  stloc.0
  IL_000a:  ldc.i4.1
  IL_000b:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0010:  nop
  IL_0011:  leave.s    IL_0050
  IL_0013:  ldc.i4.m1
  IL_0014:  stloc.1
  IL_0015:  ldloc.0
  IL_0016:  switch    (
  IL_0023,
  IL_0023)
  IL_0023:  leave.s    IL_0045
}
  filter
{
  IL_0025:  isinst     "System.Exception"
  IL_002a:  ldnull
  IL_002b:  cgt.un
  IL_002d:  ldloc.0
  IL_002e:  ldc.i4.0
  IL_002f:  cgt.un
  IL_0031:  and
  IL_0032:  ldloc.1
  IL_0033:  ldc.i4.0
  IL_0034:  ceq
  IL_0036:  and
  IL_0037:  endfilter
}  // end filter
{  // handler
  IL_0039:  castclass  "System.Exception"
  IL_003e:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0043:  leave.s    IL_0013
}
  IL_0045:  ldc.i4     0x800a0033
  IL_004a:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_004f:  throw
  IL_0050:  ldloc.1
  IL_0051:  ldc.i4.0
  IL_0052:  ceq
  IL_0054:  stloc.2
  IL_0055:  ldloc.2
  IL_0056:  brtrue.s   IL_005e
  IL_0058:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_005d:  nop
  IL_005e:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub TestOnError_GotoM1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
        On Error GoTo -1
        System.Console.WriteLine(1)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size       85 (0x55)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.0
  IL_0006:  stloc.1
  IL_0007:  ldc.i4.1
  IL_0008:  call       "Sub System.Console.WriteLine(Integer)"
  IL_000d:  leave.s    IL_004c
  IL_000f:  ldc.i4.m1
  IL_0010:  stloc.1
  IL_0011:  ldloc.0
  IL_0012:  switch    (
  IL_001f,
  IL_001f)
  IL_001f:  leave.s    IL_0041
}
  filter
{
  IL_0021:  isinst     "System.Exception"
  IL_0026:  ldnull
  IL_0027:  cgt.un
  IL_0029:  ldloc.0
  IL_002a:  ldc.i4.0
  IL_002b:  cgt.un
  IL_002d:  and
  IL_002e:  ldloc.1
  IL_002f:  ldc.i4.0
  IL_0030:  ceq
  IL_0032:  and
  IL_0033:  endfilter
}  // end filter
{  // handler
  IL_0035:  castclass  "System.Exception"
  IL_003a:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_003f:  leave.s    IL_000f
}
  IL_0041:  ldc.i4     0x800a0033
  IL_0046:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_004b:  throw
  IL_004c:  ldloc.1
  IL_004d:  brfalse.s  IL_0054
  IL_004f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0054:  ret
}
]]>)

        End Sub

        <Fact()>
        Public Sub TestOnError_GotoLabel()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
        System.Console.WriteLine(1)
        On Error GoTo OnErrorLabel
        System.Console.WriteLine(2)
OnErrorLabel:
        System.Console.WriteLine(3)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
2
3
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      101 (0x65)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0006:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.0
  IL_000d:  ldc.i4.2
  IL_000e:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0013:  ldc.i4.3
  IL_0014:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0019:  leave.s    IL_005c
  IL_001b:  ldc.i4.m1
  IL_001c:  stloc.1
  IL_001d:  ldloc.0
  IL_001e:  switch    (
  IL_002f,
  IL_002f,
  IL_0013)
  IL_002f:  leave.s    IL_0051
}
  filter
{
  IL_0031:  isinst     "System.Exception"
  IL_0036:  ldnull
  IL_0037:  cgt.un
  IL_0039:  ldloc.0
  IL_003a:  ldc.i4.0
  IL_003b:  cgt.un
  IL_003d:  and
  IL_003e:  ldloc.1
  IL_003f:  ldc.i4.0
  IL_0040:  ceq
  IL_0042:  and
  IL_0043:  endfilter
}  // end filter
{  // handler
  IL_0045:  castclass  "System.Exception"
  IL_004a:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_004f:  leave.s    IL_001b
}
  IL_0051:  ldc.i4     0x800a0033
  IL_0056:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_005b:  throw
  IL_005c:  ldloc.1
  IL_005d:  brfalse.s  IL_0064
  IL_005f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0064:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub TestOnError_ResumeNext()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
        System.Console.WriteLine(1)
        On Error Resume Next
        System.Console.WriteLine(2)
        System.Console.WriteLine(3)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
2
3
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      139 (0x8b)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldc.i4.1
  IL_0003:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0008:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_000d:  ldc.i4.1
  IL_000e:  stloc.0
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  ldc.i4.2
  IL_0012:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0017:  ldc.i4.4
  IL_0018:  stloc.2
  IL_0019:  ldc.i4.3
  IL_001a:  call       "Sub System.Console.WriteLine(Integer)"
  IL_001f:  leave.s    IL_0082
  IL_0021:  ldloc.1
  IL_0022:  ldc.i4.1
  IL_0023:  add
  IL_0024:  ldc.i4.0
  IL_0025:  stloc.1
  IL_0026:  switch    (
  IL_0043,
  IL_0000,
  IL_0008,
  IL_000f,
  IL_0017,
  IL_001f)
  IL_0043:  leave.s    IL_0077
  IL_0045:  ldloc.2
  IL_0046:  stloc.1
  IL_0047:  ldloc.0
  IL_0048:  switch    (
  IL_0055,
  IL_0021)
  IL_0055:  leave.s    IL_0077
}
  filter
{
  IL_0057:  isinst     "System.Exception"
  IL_005c:  ldnull
  IL_005d:  cgt.un
  IL_005f:  ldloc.0
  IL_0060:  ldc.i4.0
  IL_0061:  cgt.un
  IL_0063:  and
  IL_0064:  ldloc.1
  IL_0065:  ldc.i4.0
  IL_0066:  ceq
  IL_0068:  and
  IL_0069:  endfilter
}  // end filter
{  // handler
  IL_006b:  castclass  "System.Exception"
  IL_0070:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0075:  leave.s    IL_0045
}
  IL_0077:  ldc.i4     0x800a0033
  IL_007c:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0081:  throw
  IL_0082:  ldloc.1
  IL_0083:  brfalse.s  IL_008a
  IL_0085:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_008a:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub TestOnError_ResumeNext_2()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
        System.Console.WriteLine(1)
        On Error Resume Next
        System.Console.WriteLine(2)
        On Error Resume Next
        System.Console.WriteLine(3)
        On Error Resume Next
        System.Console.WriteLine(4)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)

            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[
1
2
3
4
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      199 (0xc7)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  ldc.i4.1
  IL_0003:  stloc.2
  IL_0004:  ldc.i4.1
  IL_0005:  call       "Sub System.Console.WriteLine(Integer)"
  IL_000a:  nop
  IL_000b:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0010:  nop
  IL_0011:  ldc.i4.s   -2
  IL_0013:  stloc.0
  IL_0014:  ldc.i4.3
  IL_0015:  stloc.2
  IL_0016:  ldc.i4.2
  IL_0017:  call       "Sub System.Console.WriteLine(Integer)"
  IL_001c:  nop
  IL_001d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0022:  nop
  IL_0023:  ldc.i4.s   -3
  IL_0025:  stloc.0
  IL_0026:  ldc.i4.5
  IL_0027:  stloc.2
  IL_0028:  ldc.i4.3
  IL_0029:  call       "Sub System.Console.WriteLine(Integer)"
  IL_002e:  nop
  IL_002f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0034:  nop
  IL_0035:  ldc.i4.s   -4
  IL_0037:  stloc.0
  IL_0038:  ldc.i4.7
  IL_0039:  stloc.2
  IL_003a:  ldc.i4.4
  IL_003b:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0040:  nop
  IL_0041:  leave.s    IL_00b8
  IL_0043:  ldloc.1
  IL_0044:  ldc.i4.1
  IL_0045:  add
  IL_0046:  ldc.i4.0
  IL_0047:  stloc.1
  IL_0048:  switch    (
  IL_0071,
  IL_0002,
  IL_000b,
  IL_0014,
  IL_001d,
  IL_0026,
  IL_002f,
  IL_0038,
  IL_0041)
  IL_0071:  leave.s    IL_00ad
  IL_0073:  ldloc.2
  IL_0074:  stloc.1
  IL_0075:  ldloc.0
  IL_0076:  ldc.i4.s   -2
  IL_0078:  bgt.s      IL_007d
  IL_007a:  ldc.i4.1
  IL_007b:  br.s       IL_007e
  IL_007d:  ldloc.0
  IL_007e:  switch    (
  IL_008b,
  IL_0043)
  IL_008b:  leave.s    IL_00ad
}
  filter
{
  IL_008d:  isinst     "System.Exception"
  IL_0092:  ldnull
  IL_0093:  cgt.un
  IL_0095:  ldloc.0
  IL_0096:  ldc.i4.0
  IL_0097:  cgt.un
  IL_0099:  and
  IL_009a:  ldloc.1
  IL_009b:  ldc.i4.0
  IL_009c:  ceq
  IL_009e:  and
  IL_009f:  endfilter
}  // end filter
{  // handler
  IL_00a1:  castclass  "System.Exception"
  IL_00a6:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ab:  leave.s    IL_0073
}
  IL_00ad:  ldc.i4     0x800a0033
  IL_00b2:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b7:  throw
  IL_00b8:  ldloc.1
  IL_00b9:  ldc.i4.0
  IL_00ba:  ceq
  IL_00bc:  stloc.3
  IL_00bd:  ldloc.3
  IL_00be:  brtrue.s   IL_00c6
  IL_00c0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c5:  nop
  IL_00c6:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub TestResume()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
        System.Console.WriteLine(1)
	Return
        System.Console.WriteLine(2)
        Resume 
        System.Console.WriteLine(3)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      165 (0xa5)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldc.i4.1
  IL_0003:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0008:  leave      IL_009c
  IL_000d:  ldc.i4.3
  IL_000e:  stloc.2
  IL_000f:  ldc.i4.2
  IL_0010:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0015:  ldc.i4.4
  IL_0016:  stloc.2
  IL_0017:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_001c:  ldloc.1
  IL_001d:  brtrue.s   IL_0034
  IL_001f:  ldc.i4     0x800a0014
  IL_0024:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0029:  throw
  IL_002a:  ldc.i4.5
  IL_002b:  stloc.2
  IL_002c:  ldc.i4.3
  IL_002d:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0032:  leave.s    IL_009c
  IL_0034:  ldloc.1
  IL_0035:  br.s       IL_003a
  IL_0037:  ldloc.1
  IL_0038:  ldc.i4.1
  IL_0039:  add
  IL_003a:  ldc.i4.0
  IL_003b:  stloc.1
  IL_003c:  switch    (
  IL_005d,
  IL_0000,
  IL_0032,
  IL_000d,
  IL_0015,
  IL_002a,
  IL_0032)
  IL_005d:  leave.s    IL_0091
  IL_005f:  ldloc.2
  IL_0060:  stloc.1
  IL_0061:  ldloc.0
  IL_0062:  switch    (
  IL_006f,
  IL_0037)
  IL_006f:  leave.s    IL_0091
}
  filter
{
  IL_0071:  isinst     "System.Exception"
  IL_0076:  ldnull
  IL_0077:  cgt.un
  IL_0079:  ldloc.0
  IL_007a:  ldc.i4.0
  IL_007b:  cgt.un
  IL_007d:  and
  IL_007e:  ldloc.1
  IL_007f:  ldc.i4.0
  IL_0080:  ceq
  IL_0082:  and
  IL_0083:  endfilter
}  // end filter
{  // handler
  IL_0085:  castclass  "System.Exception"
  IL_008a:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_008f:  leave.s    IL_005f
}
  IL_0091:  ldc.i4     0x800a0033
  IL_0096:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_009b:  throw
  IL_009c:  ldloc.1
  IL_009d:  brfalse.s  IL_00a4
  IL_009f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a4:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub TestResumeNext()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
        System.Console.WriteLine(1)
	Return 
        System.Console.WriteLine(2)
        Resume Next
        System.Console.WriteLine(3)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      162 (0xa2)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldc.i4.1
  IL_0003:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0008:  leave      IL_0099
  IL_000d:  ldc.i4.3
  IL_000e:  stloc.2
  IL_000f:  ldc.i4.2
  IL_0010:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0015:  ldc.i4.4
  IL_0016:  stloc.2
  IL_0017:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_001c:  ldloc.1
  IL_001d:  brtrue.s   IL_0034
  IL_001f:  ldc.i4     0x800a0014
  IL_0024:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0029:  throw
  IL_002a:  ldc.i4.5
  IL_002b:  stloc.2
  IL_002c:  ldc.i4.3
  IL_002d:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0032:  leave.s    IL_0099
  IL_0034:  ldloc.1
  IL_0035:  ldc.i4.1
  IL_0036:  add
  IL_0037:  ldc.i4.0
  IL_0038:  stloc.1
  IL_0039:  switch    (
  IL_005a,
  IL_0000,
  IL_0032,
  IL_000d,
  IL_0015,
  IL_002a,
  IL_0032)
  IL_005a:  leave.s    IL_008e
  IL_005c:  ldloc.2
  IL_005d:  stloc.1
  IL_005e:  ldloc.0
  IL_005f:  switch    (
  IL_006c,
  IL_0034)
  IL_006c:  leave.s    IL_008e
}
  filter
{
  IL_006e:  isinst     "System.Exception"
  IL_0073:  ldnull
  IL_0074:  cgt.un
  IL_0076:  ldloc.0
  IL_0077:  ldc.i4.0
  IL_0078:  cgt.un
  IL_007a:  and
  IL_007b:  ldloc.1
  IL_007c:  ldc.i4.0
  IL_007d:  ceq
  IL_007f:  and
  IL_0080:  endfilter
}  // end filter
{  // handler
  IL_0082:  castclass  "System.Exception"
  IL_0087:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_008c:  leave.s    IL_005c
}
  IL_008e:  ldc.i4     0x800a0033
  IL_0093:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0098:  throw
  IL_0099:  ldloc.1
  IL_009a:  brfalse.s  IL_00a1
  IL_009c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a1:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub TestResumeLabel()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
Label:
        System.Console.WriteLine(1)
	    Return 
        System.Console.WriteLine(2)
        Resume Label
        System.Console.WriteLine(3)
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
1
]]>)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size       78 (0x4e)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0006:  leave.s    IL_0045
  IL_0008:  ldc.i4.m1
  IL_0009:  stloc.1
  IL_000a:  ldloc.0
  IL_000b:  switch    (
  IL_0018,
  IL_0018)
  IL_0018:  leave.s    IL_003a
}
  filter
{
  IL_001a:  isinst     "System.Exception"
  IL_001f:  ldnull
  IL_0020:  cgt.un
  IL_0022:  ldloc.0
  IL_0023:  ldc.i4.0
  IL_0024:  cgt.un
  IL_0026:  and
  IL_0027:  ldloc.1
  IL_0028:  ldc.i4.0
  IL_0029:  ceq
  IL_002b:  and
  IL_002c:  endfilter
}  // end filter
{  // handler
  IL_002e:  castclass  "System.Exception"
  IL_0033:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0038:  leave.s    IL_0008
}
  IL_003a:  ldc.i4     0x800a0033
  IL_003f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0044:  throw
  IL_0045:  ldloc.1
  IL_0046:  brfalse.s  IL_004d
  IL_0048:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_004d:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub BaseCtor()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
	Dim x = New Derived(2)
    End Sub
End Module

Class Base
    Sub New()
    End Sub

    Sub New(x as Integer)
	    On Error Goto 0
	    System.Console.WriteLine(2)
    End Sub
End Class

Class Derived
    Inherits Base

    Sub New(x as Integer)
	    MyBase.New(x)
	    On Error Goto 0
	    System.Console.WriteLine(1)
    End Sub

    Sub New(x as String)
	    Me.New(CInt(x))
	    On Error Goto 0
	    System.Console.WriteLine(1)
    End Sub
End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
2
1
]]>)

            compilationVerifier.VerifyIL("Base..ctor(Integer)",
            <![CDATA[
{
  // Code size       91 (0x5b)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  IL_0000:  ldarg.0
  IL_0001:  call       "Sub Object..ctor()"
  .try
{
  IL_0006:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_000b:  ldc.i4.0
  IL_000c:  stloc.0
  IL_000d:  ldc.i4.2
  IL_000e:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0013:  leave.s    IL_0052
  IL_0015:  ldc.i4.m1
  IL_0016:  stloc.1
  IL_0017:  ldloc.0
  IL_0018:  switch    (
  IL_0025,
  IL_0025)
  IL_0025:  leave.s    IL_0047
}
  filter
{
  IL_0027:  isinst     "System.Exception"
  IL_002c:  ldnull
  IL_002d:  cgt.un
  IL_002f:  ldloc.0
  IL_0030:  ldc.i4.0
  IL_0031:  cgt.un
  IL_0033:  and
  IL_0034:  ldloc.1
  IL_0035:  ldc.i4.0
  IL_0036:  ceq
  IL_0038:  and
  IL_0039:  endfilter
}  // end filter
{  // handler
  IL_003b:  castclass  "System.Exception"
  IL_0040:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0045:  leave.s    IL_0015
}
  IL_0047:  ldc.i4     0x800a0033
  IL_004c:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0051:  throw
  IL_0052:  ldloc.1
  IL_0053:  brfalse.s  IL_005a
  IL_0055:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_005a:  ret
}
]]>)

            compilationVerifier.VerifyIL("Derived..ctor(String)",
            <![CDATA[
{
  // Code size       97 (0x61)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  IL_0000:  ldarg.0
  IL_0001:  ldarg.1
  IL_0002:  call       "Function Microsoft.VisualBasic.CompilerServices.Conversions.ToInteger(String) As Integer"
  IL_0007:  call       "Sub Derived..ctor(Integer)"
  .try
{
  IL_000c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0011:  ldc.i4.0
  IL_0012:  stloc.0
  IL_0013:  ldc.i4.1
  IL_0014:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0019:  leave.s    IL_0058
  IL_001b:  ldc.i4.m1
  IL_001c:  stloc.1
  IL_001d:  ldloc.0
  IL_001e:  switch    (
  IL_002b,
  IL_002b)
  IL_002b:  leave.s    IL_004d
}
  filter
{
  IL_002d:  isinst     "System.Exception"
  IL_0032:  ldnull
  IL_0033:  cgt.un
  IL_0035:  ldloc.0
  IL_0036:  ldc.i4.0
  IL_0037:  cgt.un
  IL_0039:  and
  IL_003a:  ldloc.1
  IL_003b:  ldc.i4.0
  IL_003c:  ceq
  IL_003e:  and
  IL_003f:  endfilter
}  // end filter
{  // handler
  IL_0041:  castclass  "System.Exception"
  IL_0046:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_004b:  leave.s    IL_001b
}
  IL_004d:  ldc.i4     0x800a0033
  IL_0052:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0057:  throw
  IL_0058:  ldloc.1
  IL_0059:  brfalse.s  IL_0060
  IL_005b:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0060:  ret
}
]]>)

            compilationVerifier.VerifyIL("Derived..ctor(Integer)",
            <![CDATA[
{
  // Code size       92 (0x5c)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  IL_0000:  ldarg.0
  IL_0001:  ldarg.1
  IL_0002:  call       "Sub Base..ctor(Integer)"
  .try
{
  IL_0007:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_000c:  ldc.i4.0
  IL_000d:  stloc.0
  IL_000e:  ldc.i4.1
  IL_000f:  call       "Sub System.Console.WriteLine(Integer)"
  IL_0014:  leave.s    IL_0053
  IL_0016:  ldc.i4.m1
  IL_0017:  stloc.1
  IL_0018:  ldloc.0
  IL_0019:  switch    (
  IL_0026,
  IL_0026)
  IL_0026:  leave.s    IL_0048
}
  filter
{
  IL_0028:  isinst     "System.Exception"
  IL_002d:  ldnull
  IL_002e:  cgt.un
  IL_0030:  ldloc.0
  IL_0031:  ldc.i4.0
  IL_0032:  cgt.un
  IL_0034:  and
  IL_0035:  ldloc.1
  IL_0036:  ldc.i4.0
  IL_0037:  ceq
  IL_0039:  and
  IL_003a:  endfilter
}  // end filter
{  // handler
  IL_003c:  castclass  "System.Exception"
  IL_0041:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0046:  leave.s    IL_0016
}
  IL_0048:  ldc.i4     0x800a0033
  IL_004d:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0052:  throw
  IL_0053:  ldloc.1
  IL_0054:  brfalse.s  IL_005b
  IL_0056:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_005b:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub ExplicitTry()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Sub Main()
        Try ' 1
            On Error GoTo 0
            Resume
        Catch ex As Exception
        End Try

        On Error GoTo -1
        Resume Next

        Try ' 2
        Catch ex As Exception
        End Try
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC30544: Method cannot contain both a 'Try' statement and an 'On Error' or 'Resume' statement.
        Try ' 1
        ~~~~~~~~
BC30544: Method cannot contain both a 'Try' statement and an 'On Error' or 'Resume' statement.
            On Error GoTo 0
            ~~~~~~~~~~~~~~~
BC30544: Method cannot contain both a 'Try' statement and an 'On Error' or 'Resume' statement.
            Resume
            ~~~~~~
BC30544: Method cannot contain both a 'Try' statement and an 'On Error' or 'Resume' statement.
        On Error GoTo -1
        ~~~~~~~~~~~~~~~~
BC30544: Method cannot contain both a 'Try' statement and an 'On Error' or 'Resume' statement.
        Resume Next
        ~~~~~~~~~~~
BC30544: Method cannot contain both a 'Try' statement and an 'On Error' or 'Resume' statement.
        Try ' 2
        ~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub OnErrorInSyncLockOrUsing()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Sub Main()
        SyncLock New Object()
            On Error GoTo 0 ' 1
            Resume

            Using New Object()
                On Error GoTo -1 ' 2
                Resume Next
            End Using

            On Error GoTo -1 ' 3
            Resume Next

            SyncLock New Object()
                On Error GoTo 0 ' 4
                Resume
            End SyncLock

            On Error GoTo -1 ' 5
            Resume Next
        End SyncLock

        Using New Object()
            On Error GoTo -1 ' 6
            Resume Next

            SyncLock New Object()
                On Error GoTo 0 ' 7
                Resume
            End SyncLock

            On Error GoTo 0 ' 8
            Resume

            Using New Object()
                On Error GoTo -1 ' 9
                Resume Next
            End Using

            On Error GoTo 0 ' 10
            Resume
        End Using
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC30752: 'On Error' statements are not valid within 'SyncLock' statements.
            On Error GoTo 0 ' 1
            ~~~~~~~~~~~~~~~
BC36013: 'On Error' statements are not valid within 'Using' statements.
                On Error GoTo -1 ' 2
                ~~~~~~~~~~~~~~~~
BC30752: 'On Error' statements are not valid within 'SyncLock' statements.
            On Error GoTo -1 ' 3
            ~~~~~~~~~~~~~~~~
BC30752: 'On Error' statements are not valid within 'SyncLock' statements.
                On Error GoTo 0 ' 4
                ~~~~~~~~~~~~~~~
BC30752: 'On Error' statements are not valid within 'SyncLock' statements.
            On Error GoTo -1 ' 5
            ~~~~~~~~~~~~~~~~
BC36013: 'On Error' statements are not valid within 'Using' statements.
            On Error GoTo -1 ' 6
            ~~~~~~~~~~~~~~~~
BC30752: 'On Error' statements are not valid within 'SyncLock' statements.
                On Error GoTo 0 ' 7
                ~~~~~~~~~~~~~~~
BC36013: 'On Error' statements are not valid within 'Using' statements.
            On Error GoTo 0 ' 8
            ~~~~~~~~~~~~~~~
BC36013: 'On Error' statements are not valid within 'Using' statements.
                On Error GoTo -1 ' 9
                ~~~~~~~~~~~~~~~~
BC36013: 'On Error' statements are not valid within 'Using' statements.
            On Error GoTo 0 ' 10
            ~~~~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub InsideLambda()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Sub Main()
        Dim x = Sub()
                    On Error GoTo 0 ' 1
                    Resume
                End Sub

        Dim y = Sub() On Error GoTo -1

        Dim z = Sub() Resume
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC36668: 'On Error' and 'Resume' cannot appear inside a lambda expression.
                    On Error GoTo 0 ' 1
                    ~~~~~~~~~~~~~~~
BC36668: 'On Error' and 'Resume' cannot appear inside a lambda expression.
                    Resume
                    ~~~~~~
BC36668: 'On Error' and 'Resume' cannot appear inside a lambda expression.
        Dim y = Sub() On Error GoTo -1
                      ~~~~~~~~~~~~~~~~
BC36668: 'On Error' and 'Resume' cannot appear inside a lambda expression.
        Dim z = Sub() Resume
                      ~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub BadLabel()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Sub Main()
        On Error GoTo DoesntExist1

        Resume DoesntExist2
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC30132: Label 'DoesntExist1' is not defined.
        On Error GoTo DoesntExist1
                      ~~~~~~~~~~~~
BC30132: Label 'DoesntExist2' is not defined.
        Resume DoesntExist2
               ~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub OnErrorInFunction()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Function Main() As Integer
    	On Error Resume Next
        Return 1
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      112 (0x70)
  .maxstack  3
  .locals init (Integer V_0, //Main
                Integer V_1,
                Integer V_2,
                Integer V_3)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.1
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.3
  IL_0009:  ldc.i4.1
  IL_000a:  stloc.0
  IL_000b:  leave.s    IL_0066
  IL_000d:  ldloc.2
  IL_000e:  ldc.i4.1
  IL_000f:  add
  IL_0010:  ldc.i4.0
  IL_0011:  stloc.2
  IL_0012:  switch    (
  IL_0027,
  IL_0000,
  IL_0007,
  IL_000b)
  IL_0027:  leave.s    IL_005b
  IL_0029:  ldloc.3
  IL_002a:  stloc.2
  IL_002b:  ldloc.1
  IL_002c:  switch    (
  IL_0039,
  IL_000d)
  IL_0039:  leave.s    IL_005b
}
  filter
{
  IL_003b:  isinst     "System.Exception"
  IL_0040:  ldnull
  IL_0041:  cgt.un
  IL_0043:  ldloc.1
  IL_0044:  ldc.i4.0
  IL_0045:  cgt.un
  IL_0047:  and
  IL_0048:  ldloc.2
  IL_0049:  ldc.i4.0
  IL_004a:  ceq
  IL_004c:  and
  IL_004d:  endfilter
}  // end filter
{  // handler
  IL_004f:  castclass  "System.Exception"
  IL_0054:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0059:  leave.s    IL_0029
}
  IL_005b:  ldc.i4     0x800a0033
  IL_0060:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0065:  throw
  IL_0066:  ldloc.2
  IL_0067:  brfalse.s  IL_006e
  IL_0069:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_006e:  ldloc.0
  IL_006f:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub UseSiteErrors()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class Program
    Sub Main()
        On Error GoTo -1
        On Error GoTo 0
        On Error GoTo label
        On Error Resume Next

        Resume 
        Resume Next
        Resume Label
label:
    End Sub
End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlib(source, options:=TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
    Sub Main()
    ~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError' is not defined.
    Sub Main()
    ~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError' is not defined.
    Sub Main()
    ~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
        On Error GoTo -1
        ~~~~~~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
        On Error GoTo 0
        ~~~~~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
        On Error GoTo label
        ~~~~~~~~~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
        On Error Resume Next
        ~~~~~~~~~~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
        Resume 
        ~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError' is not defined.
        Resume 
        ~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
        Resume Next
        ~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError' is not defined.
        Resume Next
        ~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError' is not defined.
        Resume Label
        ~~~~~~~~~~~~
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError' is not defined.
        Resume Label
        ~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_If_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state() As Integer

    Sub Main()
        Dim states()() As Integer = {({1, 1, 1}),
                                     ({1, 2, 1}),
                                     ({0, 2, 1}),
                                     ({2, 1, 1}),
                                     ({2, 2, 1})}

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test1 - {0}", i)
            state = states(i)
            Test1()
        Next

        System.Console.WriteLine()

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test2 - {0}", i)
            state = states(i)
            Test2()
        Next

        states = {({1, 1, 1, 1}),
                  ({1, 2, 1, 1}),
                  ({0, 1, 1, 1}),
                  ({0, 1, 2, 1}),
                  ({2, 1, 1, 1}),
                  ({2, 2, 1, 1})}

        System.Console.WriteLine()

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test3 - {0}", i)
            state = states(i)
            Test3()
        Next

        System.Console.WriteLine()

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test4 - {0}", i)
            state = states(i)
            Test4()
        Next

        states = {({1, 1, 1, 1, 1}),
                  ({1, 2, 1, 1, 1}),
                  ({2, 1, 1, 1, 1}),
                  ({2, 2, 1, 1, 1}),
                  ({0, 1, 1, 1, 1}),
                  ({0, 1, 1, 2, 1}),
                  ({0, 1, 2, 1, 1}),
                  ({0, 1, 2, 2, 1}),
                  ({0, 1, 0, 1, 1})}

        System.Console.WriteLine()

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test5 - {0}", i)
            state = states(i)
            Test5()
        Next

        states = {({1, 1, 1, 1, 1, 1}),
                  ({1, 2, 1, 1, 1, 1}),
                  ({2, 1, 1, 1, 1, 1}),
                  ({2, 2, 1, 1, 1, 1}),
                  ({0, 1, 1, 1, 1, 1}),
                  ({0, 1, 1, 2, 1, 1}),
                  ({0, 1, 2, 1, 1, 1}),
                  ({0, 1, 2, 2, 1, 1}),
                  ({0, 1, 0, 1, 1, 1}),
                  ({0, 1, 0, 1, 2, 1})}

        System.Console.WriteLine()

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test6 - {0}", i)
            state = states(i)
            Test6()
        Next
    End Sub

    Sub Test1()
        On Error Resume Next
        Throw New System.NotSupportedException()
        If M(0) Then
            M(1)
        End If
        M(2)
    End Sub

    Sub Test2()
        On Error Resume Next
        Throw New System.NotSupportedException()
        If M(0) Then M(1)
        M(2)
    End Sub

    Sub Test3()
        On Error Resume Next
        Throw New System.NotSupportedException()
        If M(0) Then
            M(1)
        Else
            M(2)
        End If
        M(3)
    End Sub

    Sub Test4()
        On Error Resume Next
        Throw New System.NotSupportedException()
        If M(0) Then M(1) Else M(2)
        M(3)
    End Sub

    Sub Test5()
        On Error Resume Next
        Throw New System.NotSupportedException()
        If M(0) Then
            M(1)
        ElseIf M(2) Then
            M(3)
        End If
        M(4)
    End Sub

    Sub Test6()
        On Error Resume Next
        Throw New System.NotSupportedException()
        If M(0) Then
            M(1)
        ElseIf M(2) Then
            M(3)
        Else
            M(4)
        End If
        M(5)
    End Sub

    Function M(num As Integer) As Boolean
        System.Console.WriteLine("M{0} - {1}", num, state(num))
        If state(num) = 2 Then
            Throw New System.NotSupportedException()
        End If

        Return state(num) <> 0
    End Function

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
Test1 - 0
M0 - 1
M1 - 1
M2 - 1
Test1 - 1
M0 - 1
M1 - 2
M2 - 1
Test1 - 2
M0 - 0
M2 - 1
Test1 - 3
M0 - 2
M1 - 1
M2 - 1
Test1 - 4
M0 - 2
M1 - 2
M2 - 1

Test2 - 0
M0 - 1
M1 - 1
M2 - 1
Test2 - 1
M0 - 1
M1 - 2
M2 - 1
Test2 - 2
M0 - 0
M2 - 1
Test2 - 3
M0 - 2
M1 - 1
M2 - 1
Test2 - 4
M0 - 2
M1 - 2
M2 - 1

Test3 - 0
M0 - 1
M1 - 1
M3 - 1
Test3 - 1
M0 - 1
M1 - 2
M3 - 1
Test3 - 2
M0 - 0
M2 - 1
M3 - 1
Test3 - 3
M0 - 0
M2 - 2
M3 - 1
Test3 - 4
M0 - 2
M1 - 1
M3 - 1
Test3 - 5
M0 - 2
M1 - 2
M3 - 1

Test4 - 0
M0 - 1
M1 - 1
M3 - 1
Test4 - 1
M0 - 1
M1 - 2
M3 - 1
Test4 - 2
M0 - 0
M2 - 1
M3 - 1
Test4 - 3
M0 - 0
M2 - 2
M3 - 1
Test4 - 4
M0 - 2
M1 - 1
M3 - 1
Test4 - 5
M0 - 2
M1 - 2
M3 - 1

Test5 - 0
M0 - 1
M1 - 1
M4 - 1
Test5 - 1
M0 - 1
M1 - 2
M4 - 1
Test5 - 2
M0 - 2
M1 - 1
M4 - 1
Test5 - 3
M0 - 2
M1 - 2
M4 - 1
Test5 - 4
M0 - 0
M2 - 1
M3 - 1
M4 - 1
Test5 - 5
M0 - 0
M2 - 1
M3 - 2
M4 - 1
Test5 - 6
M0 - 0
M2 - 2
M3 - 1
M4 - 1
Test5 - 7
M0 - 0
M2 - 2
M3 - 2
M4 - 1
Test5 - 8
M0 - 0
M2 - 0
M4 - 1

Test6 - 0
M0 - 1
M1 - 1
M5 - 1
Test6 - 1
M0 - 1
M1 - 2
M5 - 1
Test6 - 2
M0 - 2
M1 - 1
M5 - 1
Test6 - 3
M0 - 2
M1 - 2
M5 - 1
Test6 - 4
M0 - 0
M2 - 1
M3 - 1
M5 - 1
Test6 - 5
M0 - 0
M2 - 1
M3 - 2
M5 - 1
Test6 - 6
M0 - 0
M2 - 2
M3 - 1
M5 - 1
Test6 - 7
M0 - 0
M2 - 2
M3 - 2
M5 - 1
Test6 - 8
M0 - 0
M2 - 0
M4 - 1
M5 - 1
Test6 - 9
M0 - 0
M2 - 0
M4 - 2
M5 - 1
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)
        End Sub


        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_If_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Sub Main()
    End Sub

    Sub Test1()
        On Error Resume Next
        M0()
	If M1() Then
            M2()
        End if
        M3()
    End Sub

    Sub Test2()
        On Error Resume Next
        M0()
	If M1() Then M2()
        M3()
    End Sub

    Sub Test3()
        On Error Resume Next
        M0()
	If M1() Then
            M2()
        Else
            M3()
        End if
        M4()
    End Sub

    Sub Test4()
        On Error Resume Next
        M0()
	If M1() Then M2() Else M3()
        M4()
    End Sub

    Sub Test5()
        On Error Resume Next
        M0()
	If M1() Then
            M2()
        ElseIf M3()
            M4()
        End if
        M5()
    End Sub

    Sub Test6()
        On Error Resume Next
        M0()
	If M1() Then
            M2()
        ElseIf M3()
            M4()
        Else 
            M5()
        End if
        M6()
    End Sub

    Function M0() As Boolean
        Return true
    End Function

    Function M1() As Boolean
        Return true
    End Function

    Function M2() As Boolean
        Return true
    End Function

    Function M3() As Boolean
        Return true
    End Function

    Function M4() As Boolean
        Return true
    End Function

    Function M5() As Boolean
        Return true
    End Function

    Function M6() As Boolean
        Return true
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Test1",
            <![CDATA[
{
  // Code size      152 (0x98)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Function Program.M0() As Boolean"
  IL_000e:  pop
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  call       "Function Program.M1() As Boolean"
  IL_0016:  brfalse.s  IL_0020
  IL_0018:  ldc.i4.4
  IL_0019:  stloc.2
  IL_001a:  call       "Function Program.M2() As Boolean"
  IL_001f:  pop
  IL_0020:  ldc.i4.5
  IL_0021:  stloc.2
  IL_0022:  call       "Function Program.M3() As Boolean"
  IL_0027:  pop
  IL_0028:  leave.s    IL_008f
  IL_002a:  ldloc.1
  IL_002b:  ldc.i4.1
  IL_002c:  add
  IL_002d:  ldc.i4.0
  IL_002e:  stloc.1
  IL_002f:  switch    (
  IL_0050,
  IL_0000,
  IL_0007,
  IL_000f,
  IL_0018,
  IL_0020,
  IL_0028)
  IL_0050:  leave.s    IL_0084
  IL_0052:  ldloc.2
  IL_0053:  stloc.1
  IL_0054:  ldloc.0
  IL_0055:  switch    (
  IL_0062,
  IL_002a)
  IL_0062:  leave.s    IL_0084
}
  filter
{
  IL_0064:  isinst     "System.Exception"
  IL_0069:  ldnull
  IL_006a:  cgt.un
  IL_006c:  ldloc.0
  IL_006d:  ldc.i4.0
  IL_006e:  cgt.un
  IL_0070:  and
  IL_0071:  ldloc.1
  IL_0072:  ldc.i4.0
  IL_0073:  ceq
  IL_0075:  and
  IL_0076:  endfilter
}  // end filter
{  // handler
  IL_0078:  castclass  "System.Exception"
  IL_007d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0082:  leave.s    IL_0052
}
  IL_0084:  ldc.i4     0x800a0033
  IL_0089:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_008e:  throw
  IL_008f:  ldloc.1
  IL_0090:  brfalse.s  IL_0097
  IL_0092:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0097:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test2",
            <![CDATA[
{
  // Code size      152 (0x98)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Function Program.M0() As Boolean"
  IL_000e:  pop
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  call       "Function Program.M1() As Boolean"
  IL_0016:  brfalse.s  IL_0020
  IL_0018:  ldc.i4.4
  IL_0019:  stloc.2
  IL_001a:  call       "Function Program.M2() As Boolean"
  IL_001f:  pop
  IL_0020:  ldc.i4.5
  IL_0021:  stloc.2
  IL_0022:  call       "Function Program.M3() As Boolean"
  IL_0027:  pop
  IL_0028:  leave.s    IL_008f
  IL_002a:  ldloc.1
  IL_002b:  ldc.i4.1
  IL_002c:  add
  IL_002d:  ldc.i4.0
  IL_002e:  stloc.1
  IL_002f:  switch    (
  IL_0050,
  IL_0000,
  IL_0007,
  IL_000f,
  IL_0018,
  IL_0020,
  IL_0028)
  IL_0050:  leave.s    IL_0084
  IL_0052:  ldloc.2
  IL_0053:  stloc.1
  IL_0054:  ldloc.0
  IL_0055:  switch    (
  IL_0062,
  IL_002a)
  IL_0062:  leave.s    IL_0084
}
  filter
{
  IL_0064:  isinst     "System.Exception"
  IL_0069:  ldnull
  IL_006a:  cgt.un
  IL_006c:  ldloc.0
  IL_006d:  ldc.i4.0
  IL_006e:  cgt.un
  IL_0070:  and
  IL_0071:  ldloc.1
  IL_0072:  ldc.i4.0
  IL_0073:  ceq
  IL_0075:  and
  IL_0076:  endfilter
}  // end filter
{  // handler
  IL_0078:  castclass  "System.Exception"
  IL_007d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0082:  leave.s    IL_0052
}
  IL_0084:  ldc.i4     0x800a0033
  IL_0089:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_008e:  throw
  IL_008f:  ldloc.1
  IL_0090:  brfalse.s  IL_0097
  IL_0092:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0097:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test3",
            <![CDATA[
{
  // Code size      170 (0xaa)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Function Program.M0() As Boolean"
  IL_000e:  pop
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  call       "Function Program.M1() As Boolean"
  IL_0016:  brfalse.s  IL_0022
  IL_0018:  ldc.i4.4
  IL_0019:  stloc.2
  IL_001a:  call       "Function Program.M2() As Boolean"
  IL_001f:  pop
  IL_0020:  br.s       IL_002a
  IL_0022:  ldc.i4.6
  IL_0023:  stloc.2
  IL_0024:  call       "Function Program.M3() As Boolean"
  IL_0029:  pop
  IL_002a:  ldc.i4.7
  IL_002b:  stloc.2
  IL_002c:  call       "Function Program.M4() As Boolean"
  IL_0031:  pop
  IL_0032:  leave.s    IL_00a1
  IL_0034:  ldloc.1
  IL_0035:  ldc.i4.1
  IL_0036:  add
  IL_0037:  ldc.i4.0
  IL_0038:  stloc.1
  IL_0039:  switch    (
  IL_0062,
  IL_0000,
  IL_0007,
  IL_000f,
  IL_0018,
  IL_002a,
  IL_0022,
  IL_002a,
  IL_0032)
  IL_0062:  leave.s    IL_0096
  IL_0064:  ldloc.2
  IL_0065:  stloc.1
  IL_0066:  ldloc.0
  IL_0067:  switch    (
  IL_0074,
  IL_0034)
  IL_0074:  leave.s    IL_0096
}
  filter
{
  IL_0076:  isinst     "System.Exception"
  IL_007b:  ldnull
  IL_007c:  cgt.un
  IL_007e:  ldloc.0
  IL_007f:  ldc.i4.0
  IL_0080:  cgt.un
  IL_0082:  and
  IL_0083:  ldloc.1
  IL_0084:  ldc.i4.0
  IL_0085:  ceq
  IL_0087:  and
  IL_0088:  endfilter
}  // end filter
{  // handler
  IL_008a:  castclass  "System.Exception"
  IL_008f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0094:  leave.s    IL_0064
}
  IL_0096:  ldc.i4     0x800a0033
  IL_009b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a0:  throw
  IL_00a1:  ldloc.1
  IL_00a2:  brfalse.s  IL_00a9
  IL_00a4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a9:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test4",
            <![CDATA[
{
  // Code size      170 (0xaa)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Function Program.M0() As Boolean"
  IL_000e:  pop
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  call       "Function Program.M1() As Boolean"
  IL_0016:  brfalse.s  IL_0022
  IL_0018:  ldc.i4.4
  IL_0019:  stloc.2
  IL_001a:  call       "Function Program.M2() As Boolean"
  IL_001f:  pop
  IL_0020:  br.s       IL_002a
  IL_0022:  ldc.i4.6
  IL_0023:  stloc.2
  IL_0024:  call       "Function Program.M3() As Boolean"
  IL_0029:  pop
  IL_002a:  ldc.i4.7
  IL_002b:  stloc.2
  IL_002c:  call       "Function Program.M4() As Boolean"
  IL_0031:  pop
  IL_0032:  leave.s    IL_00a1
  IL_0034:  ldloc.1
  IL_0035:  ldc.i4.1
  IL_0036:  add
  IL_0037:  ldc.i4.0
  IL_0038:  stloc.1
  IL_0039:  switch    (
  IL_0062,
  IL_0000,
  IL_0007,
  IL_000f,
  IL_0018,
  IL_002a,
  IL_0022,
  IL_002a,
  IL_0032)
  IL_0062:  leave.s    IL_0096
  IL_0064:  ldloc.2
  IL_0065:  stloc.1
  IL_0066:  ldloc.0
  IL_0067:  switch    (
  IL_0074,
  IL_0034)
  IL_0074:  leave.s    IL_0096
}
  filter
{
  IL_0076:  isinst     "System.Exception"
  IL_007b:  ldnull
  IL_007c:  cgt.un
  IL_007e:  ldloc.0
  IL_007f:  ldc.i4.0
  IL_0080:  cgt.un
  IL_0082:  and
  IL_0083:  ldloc.1
  IL_0084:  ldc.i4.0
  IL_0085:  ceq
  IL_0087:  and
  IL_0088:  endfilter
}  // end filter
{  // handler
  IL_008a:  castclass  "System.Exception"
  IL_008f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0094:  leave.s    IL_0064
}
  IL_0096:  ldc.i4     0x800a0033
  IL_009b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a0:  throw
  IL_00a1:  ldloc.1
  IL_00a2:  brfalse.s  IL_00a9
  IL_00a4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a9:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test5",
            <![CDATA[
{
  // Code size      183 (0xb7)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Function Program.M0() As Boolean"
  IL_000e:  pop
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  call       "Function Program.M1() As Boolean"
  IL_0016:  brfalse.s  IL_0022
  IL_0018:  ldc.i4.4
  IL_0019:  stloc.2
  IL_001a:  call       "Function Program.M2() As Boolean"
  IL_001f:  pop
  IL_0020:  br.s       IL_0033
  IL_0022:  ldc.i4.6
  IL_0023:  stloc.2
  IL_0024:  call       "Function Program.M3() As Boolean"
  IL_0029:  brfalse.s  IL_0033
  IL_002b:  ldc.i4.7
  IL_002c:  stloc.2
  IL_002d:  call       "Function Program.M4() As Boolean"
  IL_0032:  pop
  IL_0033:  ldc.i4.8
  IL_0034:  stloc.2
  IL_0035:  call       "Function Program.M5() As Boolean"
  IL_003a:  pop
  IL_003b:  leave.s    IL_00ae
  IL_003d:  ldloc.1
  IL_003e:  ldc.i4.1
  IL_003f:  add
  IL_0040:  ldc.i4.0
  IL_0041:  stloc.1
  IL_0042:  switch    (
  IL_006f,
  IL_0000,
  IL_0007,
  IL_000f,
  IL_0018,
  IL_0033,
  IL_0022,
  IL_002b,
  IL_0033,
  IL_003b)
  IL_006f:  leave.s    IL_00a3
  IL_0071:  ldloc.2
  IL_0072:  stloc.1
  IL_0073:  ldloc.0
  IL_0074:  switch    (
  IL_0081,
  IL_003d)
  IL_0081:  leave.s    IL_00a3
}
  filter
{
  IL_0083:  isinst     "System.Exception"
  IL_0088:  ldnull
  IL_0089:  cgt.un
  IL_008b:  ldloc.0
  IL_008c:  ldc.i4.0
  IL_008d:  cgt.un
  IL_008f:  and
  IL_0090:  ldloc.1
  IL_0091:  ldc.i4.0
  IL_0092:  ceq
  IL_0094:  and
  IL_0095:  endfilter
}  // end filter
{  // handler
  IL_0097:  castclass  "System.Exception"
  IL_009c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00a1:  leave.s    IL_0071
}
  IL_00a3:  ldc.i4     0x800a0033
  IL_00a8:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ad:  throw
  IL_00ae:  ldloc.1
  IL_00af:  brfalse.s  IL_00b6
  IL_00b1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b6:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test6",
            <![CDATA[
{
  // Code size      203 (0xcb)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Function Program.M0() As Boolean"
  IL_000e:  pop
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  call       "Function Program.M1() As Boolean"
  IL_0016:  brfalse.s  IL_0022
  IL_0018:  ldc.i4.4
  IL_0019:  stloc.2
  IL_001a:  call       "Function Program.M2() As Boolean"
  IL_001f:  pop
  IL_0020:  br.s       IL_003e
  IL_0022:  ldc.i4.6
  IL_0023:  stloc.2
  IL_0024:  call       "Function Program.M3() As Boolean"
  IL_0029:  brfalse.s  IL_0035
  IL_002b:  ldc.i4.7
  IL_002c:  stloc.2
  IL_002d:  call       "Function Program.M4() As Boolean"
  IL_0032:  pop
  IL_0033:  br.s       IL_003e
  IL_0035:  ldc.i4.s   9
  IL_0037:  stloc.2
  IL_0038:  call       "Function Program.M5() As Boolean"
  IL_003d:  pop
  IL_003e:  ldc.i4.s   10
  IL_0040:  stloc.2
  IL_0041:  call       "Function Program.M6() As Boolean"
  IL_0046:  pop
  IL_0047:  leave.s    IL_00c2
  IL_0049:  ldloc.1
  IL_004a:  ldc.i4.1
  IL_004b:  add
  IL_004c:  ldc.i4.0
  IL_004d:  stloc.1
  IL_004e:  switch    (
  IL_0083,
  IL_0000,
  IL_0007,
  IL_000f,
  IL_0018,
  IL_003e,
  IL_0022,
  IL_002b,
  IL_003e,
  IL_0035,
  IL_003e,
  IL_0047)
  IL_0083:  leave.s    IL_00b7
  IL_0085:  ldloc.2
  IL_0086:  stloc.1
  IL_0087:  ldloc.0
  IL_0088:  switch    (
  IL_0095,
  IL_0049)
  IL_0095:  leave.s    IL_00b7
}
  filter
{
  IL_0097:  isinst     "System.Exception"
  IL_009c:  ldnull
  IL_009d:  cgt.un
  IL_009f:  ldloc.0
  IL_00a0:  ldc.i4.0
  IL_00a1:  cgt.un
  IL_00a3:  and
  IL_00a4:  ldloc.1
  IL_00a5:  ldc.i4.0
  IL_00a6:  ceq
  IL_00a8:  and
  IL_00a9:  endfilter
}  // end filter
{  // handler
  IL_00ab:  castclass  "System.Exception"
  IL_00b0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b5:  leave.s    IL_0085
}
  IL_00b7:  ldc.i4     0x800a0033
  IL_00bc:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c1:  throw
  IL_00c2:  ldloc.1
  IL_00c3:  brfalse.s  IL_00ca
  IL_00c5:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ca:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Test1",
            <![CDATA[
{
  // Code size      181 (0xb5)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Function Program.M0() As Boolean"
  IL_0012:  pop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.M1() As Boolean"
  IL_001a:  ldc.i4.0
  IL_001b:  ceq
  IL_001d:  stloc.3
  IL_001e:  ldloc.3
  IL_001f:  brtrue.s   IL_002a
  IL_0021:  ldc.i4.4
  IL_0022:  stloc.2
  IL_0023:  call       "Function Program.M2() As Boolean"
  IL_0028:  pop
  IL_0029:  nop
  IL_002a:  nop
  IL_002b:  ldc.i4.6
  IL_002c:  stloc.2
  IL_002d:  call       "Function Program.M3() As Boolean"
  IL_0032:  pop
  IL_0033:  leave.s    IL_00a6
  IL_0035:  ldloc.1
  IL_0036:  ldc.i4.1
  IL_0037:  add
  IL_0038:  ldc.i4.0
  IL_0039:  stloc.1
  IL_003a:  switch    (
  IL_005f,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0021,
  IL_0029,
  IL_002b,
  IL_0033)
  IL_005f:  leave.s    IL_009b
  IL_0061:  ldloc.2
  IL_0062:  stloc.1
  IL_0063:  ldloc.0
  IL_0064:  ldc.i4.s   -2
  IL_0066:  bgt.s      IL_006b
  IL_0068:  ldc.i4.1
  IL_0069:  br.s       IL_006c
  IL_006b:  ldloc.0
  IL_006c:  switch    (
  IL_0079,
  IL_0035)
  IL_0079:  leave.s    IL_009b
}
  filter
{
  IL_007b:  isinst     "System.Exception"
  IL_0080:  ldnull
  IL_0081:  cgt.un
  IL_0083:  ldloc.0
  IL_0084:  ldc.i4.0
  IL_0085:  cgt.un
  IL_0087:  and
  IL_0088:  ldloc.1
  IL_0089:  ldc.i4.0
  IL_008a:  ceq
  IL_008c:  and
  IL_008d:  endfilter
}  // end filter
{  // handler
  IL_008f:  castclass  "System.Exception"
  IL_0094:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0099:  leave.s    IL_0061
}
  IL_009b:  ldc.i4     0x800a0033
  IL_00a0:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a5:  throw
  IL_00a6:  ldloc.1
  IL_00a7:  ldc.i4.0
  IL_00a8:  ceq
  IL_00aa:  stloc.3
  IL_00ab:  ldloc.3
  IL_00ac:  brtrue.s   IL_00b4
  IL_00ae:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b3:  nop
  IL_00b4:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test2",
            <![CDATA[
{
  // Code size      175 (0xaf)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Function Program.M0() As Boolean"
  IL_0012:  pop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.M1() As Boolean"
  IL_001a:  ldc.i4.0
  IL_001b:  ceq
  IL_001d:  stloc.3
  IL_001e:  ldloc.3
  IL_001f:  brtrue.s   IL_0029
  IL_0021:  ldc.i4.4
  IL_0022:  stloc.2
  IL_0023:  call       "Function Program.M2() As Boolean"
  IL_0028:  pop
  IL_0029:  ldc.i4.5
  IL_002a:  stloc.2
  IL_002b:  call       "Function Program.M3() As Boolean"
  IL_0030:  pop
  IL_0031:  leave.s    IL_00a0
  IL_0033:  ldloc.1
  IL_0034:  ldc.i4.1
  IL_0035:  add
  IL_0036:  ldc.i4.0
  IL_0037:  stloc.1
  IL_0038:  switch    (
  IL_0059,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0021,
  IL_0029,
  IL_0031)
  IL_0059:  leave.s    IL_0095
  IL_005b:  ldloc.2
  IL_005c:  stloc.1
  IL_005d:  ldloc.0
  IL_005e:  ldc.i4.s   -2
  IL_0060:  bgt.s      IL_0065
  IL_0062:  ldc.i4.1
  IL_0063:  br.s       IL_0066
  IL_0065:  ldloc.0
  IL_0066:  switch    (
  IL_0073,
  IL_0033)
  IL_0073:  leave.s    IL_0095
}
  filter
{
  IL_0075:  isinst     "System.Exception"
  IL_007a:  ldnull
  IL_007b:  cgt.un
  IL_007d:  ldloc.0
  IL_007e:  ldc.i4.0
  IL_007f:  cgt.un
  IL_0081:  and
  IL_0082:  ldloc.1
  IL_0083:  ldc.i4.0
  IL_0084:  ceq
  IL_0086:  and
  IL_0087:  endfilter
}  // end filter
{  // handler
  IL_0089:  castclass  "System.Exception"
  IL_008e:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0093:  leave.s    IL_005b
}
  IL_0095:  ldc.i4     0x800a0033
  IL_009a:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_009f:  throw
  IL_00a0:  ldloc.1
  IL_00a1:  ldc.i4.0
  IL_00a2:  ceq
  IL_00a4:  stloc.3
  IL_00a5:  ldloc.3
  IL_00a6:  brtrue.s   IL_00ae
  IL_00a8:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ad:  nop
  IL_00ae:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test3",
            <![CDATA[
{
  // Code size      200 (0xc8)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Function Program.M0() As Boolean"
  IL_0012:  pop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.M1() As Boolean"
  IL_001a:  ldc.i4.0
  IL_001b:  ceq
  IL_001d:  stloc.3
  IL_001e:  ldloc.3
  IL_001f:  brtrue.s   IL_002c
  IL_0021:  ldc.i4.4
  IL_0022:  stloc.2
  IL_0023:  call       "Function Program.M2() As Boolean"
  IL_0028:  pop
  IL_0029:  nop
  IL_002a:  br.s       IL_0036
  IL_002c:  nop
  IL_002d:  ldc.i4.6
  IL_002e:  stloc.2
  IL_002f:  call       "Function Program.M3() As Boolean"
  IL_0034:  pop
  IL_0035:  nop
  IL_0036:  ldc.i4.8
  IL_0037:  stloc.2
  IL_0038:  call       "Function Program.M4() As Boolean"
  IL_003d:  pop
  IL_003e:  leave.s    IL_00b9
  IL_0040:  ldloc.1
  IL_0041:  ldc.i4.1
  IL_0042:  add
  IL_0043:  ldc.i4.0
  IL_0044:  stloc.1
  IL_0045:  switch    (
  IL_0072,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0021,
  IL_0029,
  IL_002d,
  IL_0035,
  IL_0036,
  IL_003e)
  IL_0072:  leave.s    IL_00ae
  IL_0074:  ldloc.2
  IL_0075:  stloc.1
  IL_0076:  ldloc.0
  IL_0077:  ldc.i4.s   -2
  IL_0079:  bgt.s      IL_007e
  IL_007b:  ldc.i4.1
  IL_007c:  br.s       IL_007f
  IL_007e:  ldloc.0
  IL_007f:  switch    (
  IL_008c,
  IL_0040)
  IL_008c:  leave.s    IL_00ae
}
  filter
{
  IL_008e:  isinst     "System.Exception"
  IL_0093:  ldnull
  IL_0094:  cgt.un
  IL_0096:  ldloc.0
  IL_0097:  ldc.i4.0
  IL_0098:  cgt.un
  IL_009a:  and
  IL_009b:  ldloc.1
  IL_009c:  ldc.i4.0
  IL_009d:  ceq
  IL_009f:  and
  IL_00a0:  endfilter
}  // end filter
{  // handler
  IL_00a2:  castclass  "System.Exception"
  IL_00a7:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ac:  leave.s    IL_0074
}
  IL_00ae:  ldc.i4     0x800a0033
  IL_00b3:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b8:  throw
  IL_00b9:  ldloc.1
  IL_00ba:  ldc.i4.0
  IL_00bb:  ceq
  IL_00bd:  stloc.3
  IL_00be:  ldloc.3
  IL_00bf:  brtrue.s   IL_00c7
  IL_00c1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c6:  nop
  IL_00c7:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test4",
            <![CDATA[
{
  // Code size      194 (0xc2)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Function Program.M0() As Boolean"
  IL_0012:  pop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.M1() As Boolean"
  IL_001a:  ldc.i4.0
  IL_001b:  ceq
  IL_001d:  stloc.3
  IL_001e:  ldloc.3
  IL_001f:  brtrue.s   IL_002b
  IL_0021:  ldc.i4.4
  IL_0022:  stloc.2
  IL_0023:  call       "Function Program.M2() As Boolean"
  IL_0028:  pop
  IL_0029:  br.s       IL_0034
  IL_002b:  nop
  IL_002c:  ldc.i4.6
  IL_002d:  stloc.2
  IL_002e:  call       "Function Program.M3() As Boolean"
  IL_0033:  pop
  IL_0034:  ldc.i4.7
  IL_0035:  stloc.2
  IL_0036:  call       "Function Program.M4() As Boolean"
  IL_003b:  pop
  IL_003c:  leave.s    IL_00b3
  IL_003e:  ldloc.1
  IL_003f:  ldc.i4.1
  IL_0040:  add
  IL_0041:  ldc.i4.0
  IL_0042:  stloc.1
  IL_0043:  switch    (
  IL_006c,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0021,
  IL_0029,
  IL_002c,
  IL_0034,
  IL_003c)
  IL_006c:  leave.s    IL_00a8
  IL_006e:  ldloc.2
  IL_006f:  stloc.1
  IL_0070:  ldloc.0
  IL_0071:  ldc.i4.s   -2
  IL_0073:  bgt.s      IL_0078
  IL_0075:  ldc.i4.1
  IL_0076:  br.s       IL_0079
  IL_0078:  ldloc.0
  IL_0079:  switch    (
  IL_0086,
  IL_003e)
  IL_0086:  leave.s    IL_00a8
}
  filter
{
  IL_0088:  isinst     "System.Exception"
  IL_008d:  ldnull
  IL_008e:  cgt.un
  IL_0090:  ldloc.0
  IL_0091:  ldc.i4.0
  IL_0092:  cgt.un
  IL_0094:  and
  IL_0095:  ldloc.1
  IL_0096:  ldc.i4.0
  IL_0097:  ceq
  IL_0099:  and
  IL_009a:  endfilter
}  // end filter
{  // handler
  IL_009c:  castclass  "System.Exception"
  IL_00a1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00a6:  leave.s    IL_006e
}
  IL_00a8:  ldc.i4     0x800a0033
  IL_00ad:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b2:  throw
  IL_00b3:  ldloc.1
  IL_00b4:  ldc.i4.0
  IL_00b5:  ceq
  IL_00b7:  stloc.3
  IL_00b8:  ldloc.3
  IL_00b9:  brtrue.s   IL_00c1
  IL_00bb:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c0:  nop
  IL_00c1:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test5",
            <![CDATA[
{
  // Code size      218 (0xda)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Function Program.M0() As Boolean"
  IL_0012:  pop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.M1() As Boolean"
  IL_001a:  ldc.i4.0
  IL_001b:  ceq
  IL_001d:  stloc.3
  IL_001e:  ldloc.3
  IL_001f:  brtrue.s   IL_002c
  IL_0021:  ldc.i4.4
  IL_0022:  stloc.2
  IL_0023:  call       "Function Program.M2() As Boolean"
  IL_0028:  pop
  IL_0029:  nop
  IL_002a:  br.s       IL_0043
  IL_002c:  ldc.i4.6
  IL_002d:  stloc.2
  IL_002e:  call       "Function Program.M3() As Boolean"
  IL_0033:  ldc.i4.0
  IL_0034:  ceq
  IL_0036:  stloc.3
  IL_0037:  ldloc.3
  IL_0038:  brtrue.s   IL_0043
  IL_003a:  ldc.i4.7
  IL_003b:  stloc.2
  IL_003c:  call       "Function Program.M4() As Boolean"
  IL_0041:  pop
  IL_0042:  nop
  IL_0043:  ldc.i4.s   9
  IL_0045:  stloc.2
  IL_0046:  call       "Function Program.M5() As Boolean"
  IL_004b:  pop
  IL_004c:  leave.s    IL_00cb
  IL_004e:  ldloc.1
  IL_004f:  ldc.i4.1
  IL_0050:  add
  IL_0051:  ldc.i4.0
  IL_0052:  stloc.1
  IL_0053:  switch    (
  IL_0084,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0021,
  IL_0029,
  IL_002c,
  IL_003a,
  IL_0042,
  IL_0043,
  IL_004c)
  IL_0084:  leave.s    IL_00c0
  IL_0086:  ldloc.2
  IL_0087:  stloc.1
  IL_0088:  ldloc.0
  IL_0089:  ldc.i4.s   -2
  IL_008b:  bgt.s      IL_0090
  IL_008d:  ldc.i4.1
  IL_008e:  br.s       IL_0091
  IL_0090:  ldloc.0
  IL_0091:  switch    (
  IL_009e,
  IL_004e)
  IL_009e:  leave.s    IL_00c0
}
  filter
{
  IL_00a0:  isinst     "System.Exception"
  IL_00a5:  ldnull
  IL_00a6:  cgt.un
  IL_00a8:  ldloc.0
  IL_00a9:  ldc.i4.0
  IL_00aa:  cgt.un
  IL_00ac:  and
  IL_00ad:  ldloc.1
  IL_00ae:  ldc.i4.0
  IL_00af:  ceq
  IL_00b1:  and
  IL_00b2:  endfilter
}  // end filter
{  // handler
  IL_00b4:  castclass  "System.Exception"
  IL_00b9:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00be:  leave.s    IL_0086
}
  IL_00c0:  ldc.i4     0x800a0033
  IL_00c5:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ca:  throw
  IL_00cb:  ldloc.1
  IL_00cc:  ldc.i4.0
  IL_00cd:  ceq
  IL_00cf:  stloc.3
  IL_00d0:  ldloc.3
  IL_00d1:  brtrue.s   IL_00d9
  IL_00d3:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d8:  nop
  IL_00d9:  ret
}
]]>)

            compilationVerifier.VerifyIL("Program.Test6",
            <![CDATA[
{
  // Code size      242 (0xf2)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Function Program.M0() As Boolean"
  IL_0012:  pop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.M1() As Boolean"
  IL_001a:  ldc.i4.0
  IL_001b:  ceq
  IL_001d:  stloc.3
  IL_001e:  ldloc.3
  IL_001f:  brtrue.s   IL_002c
  IL_0021:  ldc.i4.4
  IL_0022:  stloc.2
  IL_0023:  call       "Function Program.M2() As Boolean"
  IL_0028:  pop
  IL_0029:  nop
  IL_002a:  br.s       IL_0050
  IL_002c:  ldc.i4.6
  IL_002d:  stloc.2
  IL_002e:  call       "Function Program.M3() As Boolean"
  IL_0033:  ldc.i4.0
  IL_0034:  ceq
  IL_0036:  stloc.3
  IL_0037:  ldloc.3
  IL_0038:  brtrue.s   IL_0045
  IL_003a:  ldc.i4.7
  IL_003b:  stloc.2
  IL_003c:  call       "Function Program.M4() As Boolean"
  IL_0041:  pop
  IL_0042:  nop
  IL_0043:  br.s       IL_0050
  IL_0045:  nop
  IL_0046:  ldc.i4.s   9
  IL_0048:  stloc.2
  IL_0049:  call       "Function Program.M5() As Boolean"
  IL_004e:  pop
  IL_004f:  nop
  IL_0050:  ldc.i4.s   11
  IL_0052:  stloc.2
  IL_0053:  call       "Function Program.M6() As Boolean"
  IL_0058:  pop
  IL_0059:  leave      IL_00e3
  IL_005e:  ldloc.1
  IL_005f:  ldc.i4.1
  IL_0060:  add
  IL_0061:  ldc.i4.0
  IL_0062:  stloc.1
  IL_0063:  switch    (
  IL_009c,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0021,
  IL_0029,
  IL_002c,
  IL_003a,
  IL_0042,
  IL_0046,
  IL_004f,
  IL_0050,
  IL_0059)
  IL_009c:  leave.s    IL_00d8
  IL_009e:  ldloc.2
  IL_009f:  stloc.1
  IL_00a0:  ldloc.0
  IL_00a1:  ldc.i4.s   -2
  IL_00a3:  bgt.s      IL_00a8
  IL_00a5:  ldc.i4.1
  IL_00a6:  br.s       IL_00a9
  IL_00a8:  ldloc.0
  IL_00a9:  switch    (
  IL_00b6,
  IL_005e)
  IL_00b6:  leave.s    IL_00d8
}
  filter
{
  IL_00b8:  isinst     "System.Exception"
  IL_00bd:  ldnull
  IL_00be:  cgt.un
  IL_00c0:  ldloc.0
  IL_00c1:  ldc.i4.0
  IL_00c2:  cgt.un
  IL_00c4:  and
  IL_00c5:  ldloc.1
  IL_00c6:  ldc.i4.0
  IL_00c7:  ceq
  IL_00c9:  and
  IL_00ca:  endfilter
}  // end filter
{  // handler
  IL_00cc:  castclass  "System.Exception"
  IL_00d1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00d6:  leave.s    IL_009e
}
  IL_00d8:  ldc.i4     0x800a0033
  IL_00dd:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e2:  throw
  IL_00e3:  ldloc.1
  IL_00e4:  ldc.i4.0
  IL_00e5:  ceq
  IL_00e7:  stloc.3
  IL_00e8:  ldloc.3
  IL_00e9:  brtrue.s   IL_00f1
  IL_00eb:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00f0:  nop
  IL_00f1:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Select_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state As Integer()

    Sub Main()
        Dim states = {({-1, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0}),
                      ({0, -1, 0, 0, 0, 0}),
                      ({1, 0, 0, 0, 0, 0}),
                      ({1, 0, -1, 0, 0, 0}),
                      ({2, 0, 0, 0, 0, 0}),
                      ({2, 0, 0, -1, 0, 0}),
                      ({3, 0, 0, 0, 0, 0}),
                      ({3, 0, 0, 0, -1, 0}),
                      ({4, 0, 0, 0, 0, 0}),
                      ({-2, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test - {0}", i)
            state = states(i)
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        Select Case M(0)
            Case 0
                M(1)
            Case 1
                M(2)
            Case 2
                M(3)
            Case 3
                M(4)
        End Select
        M(5)
        Return
OnError:
        System.Console.WriteLine("OnError")
        Resume Next
    End Sub

    Function M(num As Integer) As Integer
        System.Console.WriteLine("M{0} - {1}", num, state(num))
        If state(num) = -1 Then
            Throw New System.NotSupportedException()
        End If

        Return state(num)
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
Test - 0
M0 - -1
OnError
M5 - 0
Test - 1
M0 - 0
M1 - 0
M5 - 0
Test - 2
M0 - 0
M1 - -1
OnError
M5 - 0
Test - 3
M0 - 1
M2 - 0
M5 - 0
Test - 4
M0 - 1
M2 - -1
OnError
M5 - 0
Test - 5
M0 - 2
M3 - 0
M5 - 0
Test - 6
M0 - 2
M3 - -1
OnError
M5 - 0
Test - 7
M0 - 3
M4 - 0
M5 - 0
Test - 8
M0 - 3
M4 - -1
OnError
M5 - 0
Test - 9
M0 - 4
M5 - 0
Test - 10
M0 - -2
M5 - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_Select_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Select Case M1()
            Case 0
                M2()
            Case 1
                M3()
            Case 2
                M4()
            Case 3
                M5()
        End Select
        M6()
    End Sub

    Sub M0()
    End Sub

    Function M1() As Integer
        Return 1
    End Function

    Sub M2()
    End Sub

    Sub M3()
    End Sub

    Sub M4()
    End Sub

    Sub M5()
    End Sub

    Sub M6()
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      237 (0xed)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.M1() As Integer"
  IL_0015:  stloc.3
  IL_0016:  ldloc.3
  IL_0017:  switch    (
  IL_002e,
  IL_0037,
  IL_0040,
  IL_004a)
  IL_002c:  br.s       IL_0052
  IL_002e:  ldc.i4.5
  IL_002f:  stloc.2
  IL_0030:  call       "Sub Program.M2()"
  IL_0035:  br.s       IL_0052
  IL_0037:  ldc.i4.7
  IL_0038:  stloc.2
  IL_0039:  call       "Sub Program.M3()"
  IL_003e:  br.s       IL_0052
  IL_0040:  ldc.i4.s   9
  IL_0042:  stloc.2
  IL_0043:  call       "Sub Program.M4()"
  IL_0048:  br.s       IL_0052
  IL_004a:  ldc.i4.s   11
  IL_004c:  stloc.2
  IL_004d:  call       "Sub Program.M5()"
  IL_0052:  ldc.i4.s   13
  IL_0054:  stloc.2
  IL_0055:  call       "Sub Program.M6()"
  IL_005a:  leave      IL_00e4
  IL_005f:  ldloc.1
  IL_0060:  ldc.i4.1
  IL_0061:  add
  IL_0062:  ldc.i4.0
  IL_0063:  stloc.1
  IL_0064:  switch    (
  IL_00a5,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_0052,
  IL_002e,
  IL_0052,
  IL_0037,
  IL_0052,
  IL_0040,
  IL_0052,
  IL_004a,
  IL_0052,
  IL_0052,
  IL_005a)
  IL_00a5:  leave.s    IL_00d9
  IL_00a7:  ldloc.2
  IL_00a8:  stloc.1
  IL_00a9:  ldloc.0
  IL_00aa:  switch    (
  IL_00b7,
  IL_005f)
  IL_00b7:  leave.s    IL_00d9
}
  filter
{
  IL_00b9:  isinst     "System.Exception"
  IL_00be:  ldnull
  IL_00bf:  cgt.un
  IL_00c1:  ldloc.0
  IL_00c2:  ldc.i4.0
  IL_00c3:  cgt.un
  IL_00c5:  and
  IL_00c6:  ldloc.1
  IL_00c7:  ldc.i4.0
  IL_00c8:  ceq
  IL_00ca:  and
  IL_00cb:  endfilter
}  // end filter
{  // handler
  IL_00cd:  castclass  "System.Exception"
  IL_00d2:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00d7:  leave.s    IL_00a7
}
  IL_00d9:  ldc.i4     0x800a0033
  IL_00de:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e3:  throw
  IL_00e4:  ldloc.1
  IL_00e5:  brfalse.s  IL_00ec
  IL_00e7:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ec:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      271 (0x10f)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3,
  Boolean V_4)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  nop
  IL_0014:  ldc.i4.3
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.M1() As Integer"
  IL_001b:  stloc.3
  IL_001c:  ldloc.3
  IL_001d:  switch    (
  IL_0034,
  IL_003f,
  IL_004a,
  IL_0056)
  IL_0032:  br.s       IL_0062
  IL_0034:  nop
  IL_0035:  ldc.i4.5
  IL_0036:  stloc.2
  IL_0037:  call       "Sub Program.M2()"
  IL_003c:  nop
  IL_003d:  br.s       IL_0062
  IL_003f:  nop
  IL_0040:  ldc.i4.7
  IL_0041:  stloc.2
  IL_0042:  call       "Sub Program.M3()"
  IL_0047:  nop
  IL_0048:  br.s       IL_0062
  IL_004a:  nop
  IL_004b:  ldc.i4.s   9
  IL_004d:  stloc.2
  IL_004e:  call       "Sub Program.M4()"
  IL_0053:  nop
  IL_0054:  br.s       IL_0062
  IL_0056:  nop
  IL_0057:  ldc.i4.s   11
  IL_0059:  stloc.2
  IL_005a:  call       "Sub Program.M5()"
  IL_005f:  nop
  IL_0060:  br.s       IL_0062
  IL_0062:  nop
  IL_0063:  ldc.i4.s   13
  IL_0065:  stloc.2
  IL_0066:  call       "Sub Program.M6()"
  IL_006b:  nop
  IL_006c:  leave      IL_00fe
  IL_0071:  ldloc.1
  IL_0072:  ldc.i4.1
  IL_0073:  add
  IL_0074:  ldc.i4.0
  IL_0075:  stloc.1
  IL_0076:  switch    (
  IL_00b7,
  IL_0002,
  IL_000b,
  IL_0014,
  IL_0062,
  IL_0035,
  IL_003d,
  IL_0040,
  IL_0048,
  IL_004b,
  IL_0054,
  IL_0057,
  IL_0060,
  IL_0063,
  IL_006c)
  IL_00b7:  leave.s    IL_00f3
  IL_00b9:  ldloc.2
  IL_00ba:  stloc.1
  IL_00bb:  ldloc.0
  IL_00bc:  ldc.i4.s   -2
  IL_00be:  bgt.s      IL_00c3
  IL_00c0:  ldc.i4.1
  IL_00c1:  br.s       IL_00c4
  IL_00c3:  ldloc.0
  IL_00c4:  switch    (
  IL_00d1,
  IL_0071)
  IL_00d1:  leave.s    IL_00f3
}
  filter
{
  IL_00d3:  isinst     "System.Exception"
  IL_00d8:  ldnull
  IL_00d9:  cgt.un
  IL_00db:  ldloc.0
  IL_00dc:  ldc.i4.0
  IL_00dd:  cgt.un
  IL_00df:  and
  IL_00e0:  ldloc.1
  IL_00e1:  ldc.i4.0
  IL_00e2:  ceq
  IL_00e4:  and
  IL_00e5:  endfilter
}  // end filter
{  // handler
  IL_00e7:  castclass  "System.Exception"
  IL_00ec:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00f1:  leave.s    IL_00b9
}
  IL_00f3:  ldc.i4     0x800a0033
  IL_00f8:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00fd:  throw
  IL_00fe:  ldloc.1
  IL_00ff:  ldc.i4.0
  IL_0100:  ceq
  IL_0102:  stloc.s    V_4
  IL_0104:  ldloc.s    V_4
  IL_0106:  brtrue.s   IL_010e
  IL_0108:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_010d:  nop
  IL_010e:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Select_2()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state As Integer()

    Sub Main()
        Dim states = {({-1, 0, 0, 0}),
                      ({0, 0, 0, 0}),
                      ({0, -1, 0, 0}),
                      ({1, 0, 0, 0}),
                      ({1, 0, -1, 0}),
                      ({2, 0, 0, 0}),
                      ({-2, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test - {0}", i)
            state = states(i)
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        Select Case M(0)
            Case 0
                M(1)
            Case 1
                M(2)
        End Select
        M(3)
        Return
OnError:
        System.Console.WriteLine("OnError")
        Resume Next
    End Sub

    Function M(num As Integer) As Integer
        System.Console.WriteLine("M{0} - {1}", num, state(num))
        If state(num) = -1 Then
            Throw New System.NotSupportedException()
        End If

        Return state(num)
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
Test - 0
M0 - -1
OnError
M3 - 0
Test - 1
M0 - 0
M1 - 0
M3 - 0
Test - 2
M0 - 0
M1 - -1
OnError
M3 - 0
Test - 3
M0 - 1
M2 - 0
M3 - 0
Test - 4
M0 - 1
M2 - -1
OnError
M3 - 0
Test - 5
M0 - 2
M3 - 0
Test - 6
M0 - -2
M3 - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)
        End Sub

        <Fact()>
        Public Sub Resume_in_Select_3()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Select Case M1()
            Case 0
                M2()
            Case 1
                M3()
        End Select
        M4()
    End Sub

    Sub M0()
    End Sub

    Function M1() As Integer
        Return 1
    End Function

    Sub M2()
    End Sub

    Sub M3()
    End Sub

    Sub M4()
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      183 (0xb7)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                Integer V_3)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.M1() As Integer"
  IL_0015:  stloc.3
  IL_0016:  ldloc.3
  IL_0017:  brfalse.s  IL_001f
  IL_0019:  ldloc.3
  IL_001a:  ldc.i4.1
  IL_001b:  beq.s      IL_0028
  IL_001d:  br.s       IL_002f
  IL_001f:  ldc.i4.5
  IL_0020:  stloc.2
  IL_0021:  call       "Sub Program.M2()"
  IL_0026:  br.s       IL_002f
  IL_0028:  ldc.i4.7
  IL_0029:  stloc.2
  IL_002a:  call       "Sub Program.M3()"
  IL_002f:  ldc.i4.s   9
  IL_0031:  stloc.2
  IL_0032:  call       "Sub Program.M4()"
  IL_0037:  leave.s    IL_00ae
  IL_0039:  ldloc.1
  IL_003a:  ldc.i4.1
  IL_003b:  add
  IL_003c:  ldc.i4.0
  IL_003d:  stloc.1
  IL_003e:  switch    (
  IL_006f,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_002f,
  IL_001f,
  IL_002f,
  IL_0028,
  IL_002f,
  IL_002f,
  IL_0037)
  IL_006f:  leave.s    IL_00a3
  IL_0071:  ldloc.2
  IL_0072:  stloc.1
  IL_0073:  ldloc.0
  IL_0074:  switch    (
  IL_0081,
  IL_0039)
  IL_0081:  leave.s    IL_00a3
}
  filter
{
  IL_0083:  isinst     "System.Exception"
  IL_0088:  ldnull
  IL_0089:  cgt.un
  IL_008b:  ldloc.0
  IL_008c:  ldc.i4.0
  IL_008d:  cgt.un
  IL_008f:  and
  IL_0090:  ldloc.1
  IL_0091:  ldc.i4.0
  IL_0092:  ceq
  IL_0094:  and
  IL_0095:  endfilter
}  // end filter
{  // handler
  IL_0097:  castclass  "System.Exception"
  IL_009c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00a1:  leave.s    IL_0071
}
  IL_00a3:  ldc.i4     0x800a0033
  IL_00a8:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ad:  throw
  IL_00ae:  ldloc.1
  IL_00af:  brfalse.s  IL_00b6
  IL_00b1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b6:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      215 (0xd7)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3,
  Boolean V_4)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  nop
  IL_0014:  ldc.i4.3
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.M1() As Integer"
  IL_001b:  stloc.3
  IL_001c:  ldloc.3
  IL_001d:  brfalse.s  IL_0027
  IL_001f:  br.s       IL_0021
  IL_0021:  ldloc.3
  IL_0022:  ldc.i4.1
  IL_0023:  beq.s      IL_0032
  IL_0025:  br.s       IL_003d
  IL_0027:  nop
  IL_0028:  ldc.i4.5
  IL_0029:  stloc.2
  IL_002a:  call       "Sub Program.M2()"
  IL_002f:  nop
  IL_0030:  br.s       IL_003d
  IL_0032:  nop
  IL_0033:  ldc.i4.7
  IL_0034:  stloc.2
  IL_0035:  call       "Sub Program.M3()"
  IL_003a:  nop
  IL_003b:  br.s       IL_003d
  IL_003d:  nop
  IL_003e:  ldc.i4.s   9
  IL_0040:  stloc.2
  IL_0041:  call       "Sub Program.M4()"
  IL_0046:  nop
  IL_0047:  leave.s    IL_00c6
  IL_0049:  ldloc.1
  IL_004a:  ldc.i4.1
  IL_004b:  add
  IL_004c:  ldc.i4.0
  IL_004d:  stloc.1
  IL_004e:  switch    (
  IL_007f,
  IL_0002,
  IL_000b,
  IL_0014,
  IL_003d,
  IL_0028,
  IL_0030,
  IL_0033,
  IL_003b,
  IL_003e,
  IL_0047)
  IL_007f:  leave.s    IL_00bb
  IL_0081:  ldloc.2
  IL_0082:  stloc.1
  IL_0083:  ldloc.0
  IL_0084:  ldc.i4.s   -2
  IL_0086:  bgt.s      IL_008b
  IL_0088:  ldc.i4.1
  IL_0089:  br.s       IL_008c
  IL_008b:  ldloc.0
  IL_008c:  switch    (
  IL_0099,
  IL_0049)
  IL_0099:  leave.s    IL_00bb
}
  filter
{
  IL_009b:  isinst     "System.Exception"
  IL_00a0:  ldnull
  IL_00a1:  cgt.un
  IL_00a3:  ldloc.0
  IL_00a4:  ldc.i4.0
  IL_00a5:  cgt.un
  IL_00a7:  and
  IL_00a8:  ldloc.1
  IL_00a9:  ldc.i4.0
  IL_00aa:  ceq
  IL_00ac:  and
  IL_00ad:  endfilter
}  // end filter
{  // handler
  IL_00af:  castclass  "System.Exception"
  IL_00b4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b9:  leave.s    IL_0081
}
  IL_00bb:  ldc.i4     0x800a0033
  IL_00c0:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c5:  throw
  IL_00c6:  ldloc.1
  IL_00c7:  ldc.i4.0
  IL_00c8:  ceq
  IL_00ca:  stloc.s    V_4
  IL_00cc:  ldloc.s    V_4
  IL_00ce:  brtrue.s   IL_00d6
  IL_00d0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d5:  nop
  IL_00d6:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Select_4()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state As Integer()

    Sub Main()
        Dim states = {({-1, 0, 0, 0, 0, 0, 0}),
                      ({1, 1, 0, 0, 0, 0, 0}),
                      ({1, 1, -1, 0, 0, 0, 0}),
                      ({1, -1, 0, 0, 0, 0, 0}),
                      ({2, -1, 0, 2, 0, 0, 0}),
                      ({2, 1, 0, 2, 0, 0, 0}),
                      ({2, 1, 0, -1, 0, 0, 0}),
                      ({2, 1, 0, 2, -1, 0, 0}),
                      ({3, 1, 0, 2, 0, 0, 0}),
                      ({3, 1, 0, 2, 0, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test - {0}", i)
            state = states(i)
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        Select Case M(0)
            Case M(1)
                M(2)
            Case M(3)
                M(4)
            Case Else
                M(5)
        End Select
        M(6)
        Return
OnError:
        System.Console.WriteLine("OnError")
        Resume Next
    End Sub

    Function M(num As Integer) As Integer
        System.Console.WriteLine("M{0} - {1}", num, state(num))
        If state(num) = -1 Then
            Throw New System.NotSupportedException()
        End If

        Return state(num)
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
Test - 0
M0 - -1
OnError
M6 - 0
Test - 1
M0 - 1
M1 - 1
M2 - 0
M6 - 0
Test - 2
M0 - 1
M1 - 1
M2 - -1
OnError
M6 - 0
Test - 3
M0 - 1
M1 - -1
OnError
M2 - 0
M6 - 0
Test - 4
M0 - 2
M1 - -1
OnError
M2 - 0
M6 - 0
Test - 5
M0 - 2
M1 - 1
M3 - 2
M4 - 0
M6 - 0
Test - 6
M0 - 2
M1 - 1
M3 - -1
OnError
M4 - 0
M6 - 0
Test - 7
M0 - 2
M1 - 1
M3 - 2
M4 - -1
OnError
M6 - 0
Test - 8
M0 - 3
M1 - 1
M3 - 2
M5 - 0
M6 - 0
Test - 9
M0 - 3
M1 - 1
M3 - 2
M5 - -1
OnError
M6 - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)
        End Sub

        <Fact()>
        Public Sub Resume_in_Select_5()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Select Case M1()
            Case M2()
                M3()
            Case M4()
                M5()
            Case Else
                M6()
        End Select
        M7()
    End Sub

    Sub M0()
    End Sub

    Function M1() As Integer
        Return 1
    End Function

    Function M2() As Integer
        Return 1
    End Function

    Sub M3()
    End Sub

    Function M4() As Integer
        Return 1
    End Function

    Sub M5()
    End Sub

    Sub M6()
    End Sub

    Sub M7()
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      220 (0xdc)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                Integer V_3)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.M1() As Integer"
  IL_0015:  stloc.3
  IL_0016:  ldc.i4.5
  IL_0017:  stloc.2
  IL_0018:  ldloc.3
  IL_0019:  call       "Function Program.M2() As Integer"
  IL_001e:  bne.un.s   IL_0029
  IL_0020:  ldc.i4.6
  IL_0021:  stloc.2
  IL_0022:  call       "Sub Program.M3()"
  IL_0027:  br.s       IL_0045
  IL_0029:  ldc.i4.8
  IL_002a:  stloc.2
  IL_002b:  ldloc.3
  IL_002c:  call       "Function Program.M4() As Integer"
  IL_0031:  bne.un.s   IL_003d
  IL_0033:  ldc.i4.s   9
  IL_0035:  stloc.2
  IL_0036:  call       "Sub Program.M5()"
  IL_003b:  br.s       IL_0045
  IL_003d:  ldc.i4.s   11
  IL_003f:  stloc.2
  IL_0040:  call       "Sub Program.M6()"
  IL_0045:  ldc.i4.s   12
  IL_0047:  stloc.2
  IL_0048:  call       "Sub Program.M7()"
  IL_004d:  leave      IL_00d3
  IL_0052:  ldloc.1
  IL_0053:  ldc.i4.1
  IL_0054:  add
  IL_0055:  ldc.i4.0
  IL_0056:  stloc.1
  IL_0057:  switch    (
  IL_0094,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_0045,
  IL_0016,
  IL_0020,
  IL_0045,
  IL_0029,
  IL_0033,
  IL_0045,
  IL_003d,
  IL_0045,
  IL_004d)
  IL_0094:  leave.s    IL_00c8
  IL_0096:  ldloc.2
  IL_0097:  stloc.1
  IL_0098:  ldloc.0
  IL_0099:  switch    (
  IL_00a6,
  IL_0052)
  IL_00a6:  leave.s    IL_00c8
}
  filter
{
  IL_00a8:  isinst     "System.Exception"
  IL_00ad:  ldnull
  IL_00ae:  cgt.un
  IL_00b0:  ldloc.0
  IL_00b1:  ldc.i4.0
  IL_00b2:  cgt.un
  IL_00b4:  and
  IL_00b5:  ldloc.1
  IL_00b6:  ldc.i4.0
  IL_00b7:  ceq
  IL_00b9:  and
  IL_00ba:  endfilter
}  // end filter
{  // handler
  IL_00bc:  castclass  "System.Exception"
  IL_00c1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00c6:  leave.s    IL_0096
}
  IL_00c8:  ldc.i4     0x800a0033
  IL_00cd:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d2:  throw
  IL_00d3:  ldloc.1
  IL_00d4:  brfalse.s  IL_00db
  IL_00d6:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00db:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      265 (0x109)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3,
  Boolean V_4)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  nop
  IL_0014:  ldc.i4.3
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.M1() As Integer"
  IL_001b:  stloc.3
  IL_001c:  ldc.i4.5
  IL_001d:  stloc.2
  IL_001e:  ldloc.3
  IL_001f:  call       "Function Program.M2() As Integer"
  IL_0024:  ceq
  IL_0026:  ldc.i4.0
  IL_0027:  ceq
  IL_0029:  stloc.s    V_4
  IL_002b:  ldloc.s    V_4
  IL_002d:  brtrue.s   IL_0039
  IL_002f:  ldc.i4.6
  IL_0030:  stloc.2
  IL_0031:  call       "Sub Program.M3()"
  IL_0036:  nop
  IL_0037:  br.s       IL_0060
  IL_0039:  ldc.i4.8
  IL_003a:  stloc.2
  IL_003b:  ldloc.3
  IL_003c:  call       "Function Program.M4() As Integer"
  IL_0041:  ceq
  IL_0043:  ldc.i4.0
  IL_0044:  ceq
  IL_0046:  stloc.s    V_4
  IL_0048:  ldloc.s    V_4
  IL_004a:  brtrue.s   IL_0057
  IL_004c:  ldc.i4.s   9
  IL_004e:  stloc.2
  IL_004f:  call       "Sub Program.M5()"
  IL_0054:  nop
  IL_0055:  br.s       IL_0060
  IL_0057:  ldc.i4.s   11
  IL_0059:  stloc.2
  IL_005a:  call       "Sub Program.M6()"
  IL_005f:  nop
  IL_0060:  nop
  IL_0061:  ldc.i4.s   12
  IL_0063:  stloc.2
  IL_0064:  call       "Sub Program.M7()"
  IL_0069:  nop
  IL_006a:  leave      IL_00f8
  IL_006f:  ldloc.1
  IL_0070:  ldc.i4.1
  IL_0071:  add
  IL_0072:  ldc.i4.0
  IL_0073:  stloc.1
  IL_0074:  switch    (
  IL_00b1,
  IL_0002,
  IL_000b,
  IL_0014,
  IL_0060,
  IL_001c,
  IL_002f,
  IL_0037,
  IL_0039,
  IL_004c,
  IL_0055,
  IL_0057,
  IL_0061,
  IL_006a)
  IL_00b1:  leave.s    IL_00ed
  IL_00b3:  ldloc.2
  IL_00b4:  stloc.1
  IL_00b5:  ldloc.0
  IL_00b6:  ldc.i4.s   -2
  IL_00b8:  bgt.s      IL_00bd
  IL_00ba:  ldc.i4.1
  IL_00bb:  br.s       IL_00be
  IL_00bd:  ldloc.0
  IL_00be:  switch    (
  IL_00cb,
  IL_006f)
  IL_00cb:  leave.s    IL_00ed
}
  filter
{
  IL_00cd:  isinst     "System.Exception"
  IL_00d2:  ldnull
  IL_00d3:  cgt.un
  IL_00d5:  ldloc.0
  IL_00d6:  ldc.i4.0
  IL_00d7:  cgt.un
  IL_00d9:  and
  IL_00da:  ldloc.1
  IL_00db:  ldc.i4.0
  IL_00dc:  ceq
  IL_00de:  and
  IL_00df:  endfilter
}  // end filter
{  // handler
  IL_00e1:  castclass  "System.Exception"
  IL_00e6:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00eb:  leave.s    IL_00b3
}
  IL_00ed:  ldc.i4     0x800a0033
  IL_00f2:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00f7:  throw
  IL_00f8:  ldloc.1
  IL_00f9:  ldc.i4.0
  IL_00fa:  ceq
  IL_00fc:  stloc.s    V_4
  IL_00fe:  ldloc.s    V_4
  IL_0100:  brtrue.s   IL_0108
  IL_0102:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0107:  nop
  IL_0108:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Select_6()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state As Integer()

    Sub Main()
        Dim states = {({-1, -1, 0}),
                      ({0, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test - {0}", i)
            state = states(i)
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        M(0)
        Select Case M(1)
        End Select
        M(2)
        Return
OnError:
        System.Console.WriteLine("OnError")
        Resume Next
    End Sub

    Function M(num As Integer) As Integer
        System.Console.WriteLine("M{0} - {1}", num, state(num))
        If state(num) = -1 Then
            Throw New System.NotSupportedException()
        End If

        Return state(num)
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
Test - 0
M0 - -1
OnError
M1 - -1
OnError
M2 - 0
Test - 1
M0 - 0
M1 - -1
OnError
M2 - 0]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)
        End Sub

        <Fact()>
        Public Sub Resume_in_While_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, 0, 0, 0}),
                      ({1, 0, 0, 0}),
                      ({1, -1, 0, 0}),
                      ({1, -1, -1, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test - {0}", i)
            state = states(i)
            current = 0
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        While M(0) <> 0
            M(1)
        End While
        M(2)
        Return
OnError:
        System.Console.WriteLine("OnError")
        Resume Next
    End Sub

    Function M(num As Integer) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M{0} - {1}", num, val)
        If val = -1 Then
            Throw New System.NotSupportedException()
        End If

        Return val
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      241 (0xf1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  br.s       IL_0016
  IL_000c:  ldc.i4.4
  IL_000d:  stloc.2
  IL_000e:  ldc.i4.1
  IL_000f:  call       "Function Program.M(Integer) As Integer"
  IL_0014:  pop
  IL_0015:  nop
  IL_0016:  ldc.i4.3
  IL_0017:  stloc.2
  IL_0018:  ldc.i4.0
  IL_0019:  call       "Function Program.M(Integer) As Integer"
  IL_001e:  ldc.i4.0
  IL_001f:  cgt.un
  IL_0021:  stloc.3
  IL_0022:  ldloc.3
  IL_0023:  brtrue.s   IL_000c
  IL_0025:  ldc.i4.6
  IL_0026:  stloc.2
  IL_0027:  ldc.i4.2
  IL_0028:  call       "Function Program.M(Integer) As Integer"
  IL_002d:  pop
  IL_002e:  br.s       IL_005c
  IL_0030:  nop
  IL_0031:  ldc.i4.8
  IL_0032:  stloc.2
  IL_0033:  ldstr      "OnError"
  IL_0038:  call       "Sub System.Console.WriteLine(String)"
  IL_003d:  nop
  IL_003e:  ldc.i4.s   9
  IL_0040:  stloc.2
  IL_0041:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0046:  nop
  IL_0047:  ldloc.1
  IL_0048:  ldc.i4.0
  IL_0049:  cgt.un
  IL_004b:  stloc.3
  IL_004c:  ldloc.3
  IL_004d:  brtrue.s   IL_005a
  IL_004f:  ldc.i4     0x800a0014
  IL_0054:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0059:  throw
  IL_005a:  br.s       IL_0061
  IL_005c:  leave      IL_00e2
  IL_0061:  ldloc.1
  IL_0062:  ldc.i4.1
  IL_0063:  add
  IL_0064:  ldc.i4.0
  IL_0065:  stloc.1
  IL_0066:  switch    (
  IL_0097,
  IL_0002,
  IL_000a,
  IL_0016,
  IL_000c,
  IL_0015,
  IL_0025,
  IL_002e,
  IL_0031,
  IL_003e,
  IL_005c)
  IL_0097:  leave.s    IL_00d7
  IL_0099:  ldloc.2
  IL_009a:  stloc.1
  IL_009b:  ldloc.0
  IL_009c:  ldc.i4.s   -2
  IL_009e:  bgt.s      IL_00a3
  IL_00a0:  ldc.i4.1
  IL_00a1:  br.s       IL_00a4
  IL_00a3:  ldloc.0
  IL_00a4:  switch    (
  IL_00b5,
  IL_0061,
  IL_0030)
  IL_00b5:  leave.s    IL_00d7
}
  filter
{
  IL_00b7:  isinst     "System.Exception"
  IL_00bc:  ldnull
  IL_00bd:  cgt.un
  IL_00bf:  ldloc.0
  IL_00c0:  ldc.i4.0
  IL_00c1:  cgt.un
  IL_00c3:  and
  IL_00c4:  ldloc.1
  IL_00c5:  ldc.i4.0
  IL_00c6:  ceq
  IL_00c8:  and
  IL_00c9:  endfilter
}  // end filter
{  // handler
  IL_00cb:  castclass  "System.Exception"
  IL_00d0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00d5:  leave.s    IL_0099
}
  IL_00d7:  ldc.i4     0x800a0033
  IL_00dc:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e1:  throw
  IL_00e2:  ldloc.1
  IL_00e3:  ldc.i4.0
  IL_00e4:  ceq
  IL_00e6:  stloc.3
  IL_00e7:  ldloc.3
  IL_00e8:  brtrue.s   IL_00f0
  IL_00ea:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ef:  nop
  IL_00f0:  ret
}]]>)


            ' Changed from verifying output to checking IL - Bug 717949
            ' Leaving in expected output for  information purpose

            '            Dim expected =
            '            <![CDATA[
            'Test - 0
            'M0 - -1
            'OnError
            'M1 - 0
            'M0 - 0
            'M2 - 0
            'Test - 1
            'M0 - 1
            'M1 - 0
            'M0 - 0
            'M2 - 0
            'Test - 2
            'M0 - 1
            'M1 - -1
            'OnError
            'M0 - 0
            'M2 - 0
            'Test - 3
            'M0 - 1
            'M1 - -1
            'OnError
            'M0 - -1
            'OnError
            'M1 - 0
            'M0 - 0
            'M2 - 0
            ']]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      208 (0xd0)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  br.s       IL_0012
  IL_0009:  ldc.i4.4
  IL_000a:  stloc.2
  IL_000b:  ldc.i4.1
  IL_000c:  call       "Function Program.M(Integer) As Integer"
  IL_0011:  pop
  IL_0012:  ldc.i4.3
  IL_0013:  stloc.2
  IL_0014:  ldc.i4.0
  IL_0015:  call       "Function Program.M(Integer) As Integer"
  IL_001a:  brtrue.s   IL_0009
  IL_001c:  ldc.i4.6
  IL_001d:  stloc.2
  IL_001e:  ldc.i4.2
  IL_001f:  call       "Function Program.M(Integer) As Integer"
  IL_0024:  pop
    IL_0025:  leave      IL_00c7
  IL_002a:  ldc.i4.8
  IL_002b:  stloc.2
  IL_002c:  ldstr      "OnError"
  IL_0031:  call       "Sub System.Console.WriteLine(String)"
  IL_0036:  ldc.i4.s   9
  IL_0038:  stloc.2
  IL_0039:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_003e:  ldloc.1
    IL_003f:  brtrue.s   IL_004e
  IL_0041:  ldc.i4     0x800a0014
  IL_0046:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_004b:  throw
    IL_004c:  leave.s    IL_00c7
    IL_004e:  ldloc.1
    IL_004f:  ldc.i4.1
    IL_0050:  add
    IL_0051:  ldc.i4.0
    IL_0052:  stloc.1
    IL_0053:  switch    (
        IL_0084,
  IL_0000,
  IL_0012,
  IL_0012,
  IL_0009,
  IL_0012,
  IL_001c,
  IL_004c,
  IL_002a,
  IL_0036,
  IL_004c)
    IL_0084:  leave.s    IL_00bc
    IL_0086:  ldloc.2
    IL_0087:  stloc.1
    IL_0088:  ldloc.0
    IL_0089:  switch    (
        IL_009a,
        IL_004e,
  IL_002a)
    IL_009a:  leave.s    IL_00bc
}
  filter
{
    IL_009c:  isinst     "System.Exception"
    IL_00a1:  ldnull
    IL_00a2:  cgt.un
    IL_00a4:  ldloc.0
    IL_00a5:  ldc.i4.0
    IL_00a6:  cgt.un
    IL_00a8:  and
    IL_00a9:  ldloc.1
    IL_00aa:  ldc.i4.0
    IL_00ab:  ceq
    IL_00ad:  and
    IL_00ae:  endfilter
}  // end filter
{  // handler
    IL_00b0:  castclass  "System.Exception"
    IL_00b5:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
    IL_00ba:  leave.s    IL_0086
}
  IL_00bc:  ldc.i4     0x800a0033
  IL_00c1:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c6:  throw
  IL_00c7:  ldloc.1
  IL_00c8:  brfalse.s  IL_00cf
  IL_00ca:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00cf:  ret
}]]>)


        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_While_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        While M1()
            If M4()
                Continue While
            End If
            M2()
ContinueLabel:
        End While
        M3()
    End Sub

    Sub M0()
    End Sub

    Function M1() As Boolean
        Return False
    End Function

    Sub M2()
    End Sub

    Sub M3()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      177 (0xb1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  br.s       IL_0020
  IL_0010:  ldc.i4.5
  IL_0011:  stloc.2
  IL_0012:  call       "Function Program.M4() As Boolean"
  IL_0017:  brtrue.s   IL_0020
  IL_0019:  ldc.i4.7
  IL_001a:  stloc.2
  IL_001b:  call       "Sub Program.M2()"
  IL_0020:  ldc.i4.4
  IL_0021:  stloc.2
  IL_0022:  call       "Function Program.M1() As Boolean"
  IL_0027:  brtrue.s   IL_0010
  IL_0029:  ldc.i4.s   9
  IL_002b:  stloc.2
  IL_002c:  call       "Sub Program.M3()"
  IL_0031:  leave.s    IL_00a8
  IL_0033:  ldloc.1
  IL_0034:  ldc.i4.1
  IL_0035:  add
  IL_0036:  ldc.i4.0
  IL_0037:  stloc.1
  IL_0038:  switch    (
  IL_0069,
  IL_0000,
  IL_0007,
  IL_0020,
  IL_0020,
  IL_0010,
  IL_0020,
  IL_0019,
  IL_0020,
  IL_0029,
  IL_0031)
  IL_0069:  leave.s    IL_009d
  IL_006b:  ldloc.2
  IL_006c:  stloc.1
  IL_006d:  ldloc.0
  IL_006e:  switch    (
  IL_007b,
  IL_0033)
  IL_007b:  leave.s    IL_009d
}
  filter
{
  IL_007d:  isinst     "System.Exception"
  IL_0082:  ldnull
  IL_0083:  cgt.un
  IL_0085:  ldloc.0
  IL_0086:  ldc.i4.0
  IL_0087:  cgt.un
  IL_0089:  and
  IL_008a:  ldloc.1
  IL_008b:  ldc.i4.0
  IL_008c:  ceq
  IL_008e:  and
  IL_008f:  endfilter
}  // end filter
{  // handler
  IL_0091:  castclass  "System.Exception"
  IL_0096:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_009b:  leave.s    IL_006b
}
  IL_009d:  ldc.i4     0x800a0033
  IL_00a2:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a7:  throw
  IL_00a8:  ldloc.1
  IL_00a9:  brfalse.s  IL_00b0
  IL_00ab:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b0:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      218 (0xda)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  br.s       IL_0031
  IL_0015:  ldc.i4.5
  IL_0016:  stloc.2
  IL_0017:  call       "Function Program.M4() As Boolean"
  IL_001c:  ldc.i4.0
  IL_001d:  ceq
  IL_001f:  stloc.3
  IL_0020:  ldloc.3
  IL_0021:  brtrue.s   IL_0026
  IL_0023:  br.s       IL_0031
  IL_0025:  nop
  IL_0026:  nop
  IL_0027:  ldc.i4.8
  IL_0028:  stloc.2
  IL_0029:  call       "Sub Program.M2()"
  IL_002e:  nop
  IL_002f:  nop
  IL_0030:  nop
  IL_0031:  ldc.i4.4
  IL_0032:  stloc.2
  IL_0033:  call       "Function Program.M1() As Boolean"
  IL_0038:  stloc.3
  IL_0039:  ldloc.3
  IL_003a:  brtrue.s   IL_0015
  IL_003c:  ldc.i4.s   10
  IL_003e:  stloc.2
  IL_003f:  call       "Sub Program.M3()"
  IL_0044:  nop
  IL_0045:  leave      IL_00cb
  IL_004a:  ldloc.1
  IL_004b:  ldc.i4.1
  IL_004c:  add
  IL_004d:  ldc.i4.0
  IL_004e:  stloc.1
  IL_004f:  switch    (
  IL_0084,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0031,
  IL_0015,
  IL_0023,
  IL_0025,
  IL_0027,
  IL_0030,
  IL_003c,
  IL_0045)
  IL_0084:  leave.s    IL_00c0
  IL_0086:  ldloc.2
  IL_0087:  stloc.1
  IL_0088:  ldloc.0
  IL_0089:  ldc.i4.s   -2
  IL_008b:  bgt.s      IL_0090
  IL_008d:  ldc.i4.1
  IL_008e:  br.s       IL_0091
  IL_0090:  ldloc.0
  IL_0091:  switch    (
  IL_009e,
  IL_004a)
  IL_009e:  leave.s    IL_00c0
}
  filter
{
  IL_00a0:  isinst     "System.Exception"
  IL_00a5:  ldnull
  IL_00a6:  cgt.un
  IL_00a8:  ldloc.0
  IL_00a9:  ldc.i4.0
  IL_00aa:  cgt.un
  IL_00ac:  and
  IL_00ad:  ldloc.1
  IL_00ae:  ldc.i4.0
  IL_00af:  ceq
  IL_00b1:  and
  IL_00b2:  endfilter
}  // end filter
{  // handler
  IL_00b4:  castclass  "System.Exception"
  IL_00b9:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00be:  leave.s    IL_0086
}
  IL_00c0:  ldc.i4     0x800a0033
  IL_00c5:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ca:  throw
  IL_00cb:  ldloc.1
  IL_00cc:  ldc.i4.0
  IL_00cd:  ceq
  IL_00cf:  stloc.3
  IL_00d0:  ldloc.3
  IL_00d1:  brtrue.s   IL_00d9
  IL_00d3:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d8:  nop
  IL_00d9:  ret
}
]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_Do_While_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Do While M1()
            If M4()
                Continue Do
            End If
            M2()
ContinueLabel:
        Loop
        M3()
    End Sub

    Sub M0()
    End Sub

    Function M1() As Boolean
        Return False
    End Function

    Sub M2()
    End Sub

    Sub M3()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      177 (0xb1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  br.s       IL_0020
  IL_0010:  ldc.i4.5
  IL_0011:  stloc.2
  IL_0012:  call       "Function Program.M4() As Boolean"
  IL_0017:  brtrue.s   IL_0020
  IL_0019:  ldc.i4.7
  IL_001a:  stloc.2
  IL_001b:  call       "Sub Program.M2()"
  IL_0020:  ldc.i4.4
  IL_0021:  stloc.2
  IL_0022:  call       "Function Program.M1() As Boolean"
  IL_0027:  brtrue.s   IL_0010
  IL_0029:  ldc.i4.s   9
  IL_002b:  stloc.2
  IL_002c:  call       "Sub Program.M3()"
  IL_0031:  leave.s    IL_00a8
  IL_0033:  ldloc.1
  IL_0034:  ldc.i4.1
  IL_0035:  add
  IL_0036:  ldc.i4.0
  IL_0037:  stloc.1
  IL_0038:  switch    (
  IL_0069,
  IL_0000,
  IL_0007,
  IL_0020,
  IL_0020,
  IL_0010,
  IL_0020,
  IL_0019,
  IL_0020,
  IL_0029,
  IL_0031)
  IL_0069:  leave.s    IL_009d
  IL_006b:  ldloc.2
  IL_006c:  stloc.1
  IL_006d:  ldloc.0
  IL_006e:  switch    (
  IL_007b,
  IL_0033)
  IL_007b:  leave.s    IL_009d
}
  filter
{
  IL_007d:  isinst     "System.Exception"
  IL_0082:  ldnull
  IL_0083:  cgt.un
  IL_0085:  ldloc.0
  IL_0086:  ldc.i4.0
  IL_0087:  cgt.un
  IL_0089:  and
  IL_008a:  ldloc.1
  IL_008b:  ldc.i4.0
  IL_008c:  ceq
  IL_008e:  and
  IL_008f:  endfilter
}  // end filter
{  // handler
  IL_0091:  castclass  "System.Exception"
  IL_0096:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_009b:  leave.s    IL_006b
}
  IL_009d:  ldc.i4     0x800a0033
  IL_00a2:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a7:  throw
  IL_00a8:  ldloc.1
  IL_00a9:  brfalse.s  IL_00b0
  IL_00ab:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b0:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      218 (0xda)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  br.s       IL_0031
  IL_0015:  ldc.i4.5
  IL_0016:  stloc.2
  IL_0017:  call       "Function Program.M4() As Boolean"
  IL_001c:  ldc.i4.0
  IL_001d:  ceq
  IL_001f:  stloc.3
  IL_0020:  ldloc.3
  IL_0021:  brtrue.s   IL_0026
  IL_0023:  br.s       IL_0031
  IL_0025:  nop
  IL_0026:  nop
  IL_0027:  ldc.i4.8
  IL_0028:  stloc.2
  IL_0029:  call       "Sub Program.M2()"
  IL_002e:  nop
  IL_002f:  nop
  IL_0030:  nop
  IL_0031:  ldc.i4.4
  IL_0032:  stloc.2
  IL_0033:  call       "Function Program.M1() As Boolean"
  IL_0038:  stloc.3
  IL_0039:  ldloc.3
  IL_003a:  brtrue.s   IL_0015
  IL_003c:  ldc.i4.s   10
  IL_003e:  stloc.2
  IL_003f:  call       "Sub Program.M3()"
  IL_0044:  nop
  IL_0045:  leave      IL_00cb
  IL_004a:  ldloc.1
  IL_004b:  ldc.i4.1
  IL_004c:  add
  IL_004d:  ldc.i4.0
  IL_004e:  stloc.1
  IL_004f:  switch    (
  IL_0084,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0031,
  IL_0015,
  IL_0023,
  IL_0025,
  IL_0027,
  IL_0030,
  IL_003c,
  IL_0045)
  IL_0084:  leave.s    IL_00c0
  IL_0086:  ldloc.2
  IL_0087:  stloc.1
  IL_0088:  ldloc.0
  IL_0089:  ldc.i4.s   -2
  IL_008b:  bgt.s      IL_0090
  IL_008d:  ldc.i4.1
  IL_008e:  br.s       IL_0091
  IL_0090:  ldloc.0
  IL_0091:  switch    (
  IL_009e,
  IL_004a)
  IL_009e:  leave.s    IL_00c0
}
  filter
{
  IL_00a0:  isinst     "System.Exception"
  IL_00a5:  ldnull
  IL_00a6:  cgt.un
  IL_00a8:  ldloc.0
  IL_00a9:  ldc.i4.0
  IL_00aa:  cgt.un
  IL_00ac:  and
  IL_00ad:  ldloc.1
  IL_00ae:  ldc.i4.0
  IL_00af:  ceq
  IL_00b1:  and
  IL_00b2:  endfilter
}  // end filter
{  // handler
  IL_00b4:  castclass  "System.Exception"
  IL_00b9:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00be:  leave.s    IL_0086
}
  IL_00c0:  ldc.i4     0x800a0033
  IL_00c5:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ca:  throw
  IL_00cb:  ldloc.1
  IL_00cc:  ldc.i4.0
  IL_00cd:  ceq
  IL_00cf:  stloc.3
  IL_00d0:  ldloc.3
  IL_00d1:  brtrue.s   IL_00d9
  IL_00d3:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d8:  nop
  IL_00d9:  ret
}
]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_Do_Until_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Do Until M1()
            If M4()
                Continue Do
            End If
            M2()
ContinueLabel:
        Loop
        M3()
    End Sub

    Sub M0()
    End Sub

    Function M1() As Boolean
        Return False
    End Function

    Sub M2()
    End Sub

    Sub M3()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      177 (0xb1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  br.s       IL_0020
  IL_0010:  ldc.i4.5
  IL_0011:  stloc.2
  IL_0012:  call       "Function Program.M4() As Boolean"
  IL_0017:  brtrue.s   IL_0020
  IL_0019:  ldc.i4.7
  IL_001a:  stloc.2
  IL_001b:  call       "Sub Program.M2()"
  IL_0020:  ldc.i4.4
  IL_0021:  stloc.2
  IL_0022:  call       "Function Program.M1() As Boolean"
  IL_0027:  brfalse.s  IL_0010
  IL_0029:  ldc.i4.s   9
  IL_002b:  stloc.2
  IL_002c:  call       "Sub Program.M3()"
  IL_0031:  leave.s    IL_00a8
  IL_0033:  ldloc.1
  IL_0034:  ldc.i4.1
  IL_0035:  add
  IL_0036:  ldc.i4.0
  IL_0037:  stloc.1
  IL_0038:  switch    (
  IL_0069,
  IL_0000,
  IL_0007,
  IL_0020,
  IL_0020,
  IL_0010,
  IL_0020,
  IL_0019,
  IL_0020,
  IL_0029,
  IL_0031)
  IL_0069:  leave.s    IL_009d
  IL_006b:  ldloc.2
  IL_006c:  stloc.1
  IL_006d:  ldloc.0
  IL_006e:  switch    (
  IL_007b,
  IL_0033)
  IL_007b:  leave.s    IL_009d
}
  filter
{
  IL_007d:  isinst     "System.Exception"
  IL_0082:  ldnull
  IL_0083:  cgt.un
  IL_0085:  ldloc.0
  IL_0086:  ldc.i4.0
  IL_0087:  cgt.un
  IL_0089:  and
  IL_008a:  ldloc.1
  IL_008b:  ldc.i4.0
  IL_008c:  ceq
  IL_008e:  and
  IL_008f:  endfilter
}  // end filter
{  // handler
  IL_0091:  castclass  "System.Exception"
  IL_0096:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_009b:  leave.s    IL_006b
}
  IL_009d:  ldc.i4     0x800a0033
  IL_00a2:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a7:  throw
  IL_00a8:  ldloc.1
  IL_00a9:  brfalse.s  IL_00b0
  IL_00ab:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b0:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      221 (0xdd)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  br.s       IL_0031
  IL_0015:  ldc.i4.5
  IL_0016:  stloc.2
  IL_0017:  call       "Function Program.M4() As Boolean"
  IL_001c:  ldc.i4.0
  IL_001d:  ceq
  IL_001f:  stloc.3
  IL_0020:  ldloc.3
  IL_0021:  brtrue.s   IL_0026
  IL_0023:  br.s       IL_0031
  IL_0025:  nop
  IL_0026:  nop
  IL_0027:  ldc.i4.8
  IL_0028:  stloc.2
  IL_0029:  call       "Sub Program.M2()"
  IL_002e:  nop
  IL_002f:  nop
  IL_0030:  nop
  IL_0031:  ldc.i4.4
  IL_0032:  stloc.2
  IL_0033:  call       "Function Program.M1() As Boolean"
  IL_0038:  ldc.i4.0
  IL_0039:  ceq
  IL_003b:  stloc.3
  IL_003c:  ldloc.3
  IL_003d:  brtrue.s   IL_0015
  IL_003f:  ldc.i4.s   10
  IL_0041:  stloc.2
  IL_0042:  call       "Sub Program.M3()"
  IL_0047:  nop
  IL_0048:  leave      IL_00ce
  IL_004d:  ldloc.1
  IL_004e:  ldc.i4.1
  IL_004f:  add
  IL_0050:  ldc.i4.0
  IL_0051:  stloc.1
  IL_0052:  switch    (
  IL_0087,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0031,
  IL_0015,
  IL_0023,
  IL_0025,
  IL_0027,
  IL_0030,
  IL_003f,
  IL_0048)
  IL_0087:  leave.s    IL_00c3
  IL_0089:  ldloc.2
  IL_008a:  stloc.1
  IL_008b:  ldloc.0
  IL_008c:  ldc.i4.s   -2
  IL_008e:  bgt.s      IL_0093
  IL_0090:  ldc.i4.1
  IL_0091:  br.s       IL_0094
  IL_0093:  ldloc.0
  IL_0094:  switch    (
  IL_00a1,
  IL_004d)
  IL_00a1:  leave.s    IL_00c3
}
  filter
{
  IL_00a3:  isinst     "System.Exception"
  IL_00a8:  ldnull
  IL_00a9:  cgt.un
  IL_00ab:  ldloc.0
  IL_00ac:  ldc.i4.0
  IL_00ad:  cgt.un
  IL_00af:  and
  IL_00b0:  ldloc.1
  IL_00b1:  ldc.i4.0
  IL_00b2:  ceq
  IL_00b4:  and
  IL_00b5:  endfilter
}  // end filter
{  // handler
  IL_00b7:  castclass  "System.Exception"
  IL_00bc:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00c1:  leave.s    IL_0089
}
  IL_00c3:  ldc.i4     0x800a0033
  IL_00c8:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00cd:  throw
  IL_00ce:  ldloc.1
  IL_00cf:  ldc.i4.0
  IL_00d0:  ceq
  IL_00d2:  stloc.3
  IL_00d3:  ldloc.3
  IL_00d4:  brtrue.s   IL_00dc
  IL_00d6:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00db:  nop
  IL_00dc:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Do_Loop_While_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({0, -1, 0}),
                      ({-1, 0, 0}),
                      ({-1, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test - {0}", i)
            state = states(i)
            current = 0
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        Do
            M(0)
        Loop While M(1) <> 0
        M(2)
        Return
OnError:
        System.Console.WriteLine("OnError")
        Resume Next
    End Sub

    Function M(num As Integer) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M{0} - {1}", num, val)
        If val = -1 Then
            Throw New System.NotSupportedException()
        End If

        Return val
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      232 (0xe8)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  nop
  IL_000b:  ldc.i4.3
  IL_000c:  stloc.2
  IL_000d:  ldc.i4.0
  IL_000e:  call       "Function Program.M(Integer) As Integer"
  IL_0013:  pop
  IL_0014:  nop
  IL_0015:  ldc.i4.4
  IL_0016:  stloc.2
  IL_0017:  ldc.i4.1
  IL_0018:  call       "Function Program.M(Integer) As Integer"
  IL_001d:  ldc.i4.0
  IL_001e:  cgt.un
  IL_0020:  stloc.3
  IL_0021:  ldloc.3
  IL_0022:  brtrue.s   IL_000a
  IL_0024:  ldc.i4.5
  IL_0025:  stloc.2
  IL_0026:  ldc.i4.2
  IL_0027:  call       "Function Program.M(Integer) As Integer"
  IL_002c:  pop
  IL_002d:  br.s       IL_005a
  IL_002f:  nop
  IL_0030:  ldc.i4.7
  IL_0031:  stloc.2
  IL_0032:  ldstr      "OnError"
  IL_0037:  call       "Sub System.Console.WriteLine(String)"
  IL_003c:  nop
  IL_003d:  ldc.i4.8
  IL_003e:  stloc.2
  IL_003f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0044:  nop
  IL_0045:  ldloc.1
  IL_0046:  ldc.i4.0
  IL_0047:  cgt.un
  IL_0049:  stloc.3
  IL_004a:  ldloc.3
  IL_004b:  brtrue.s   IL_0058
  IL_004d:  ldc.i4     0x800a0014
  IL_0052:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0057:  throw
  IL_0058:  br.s       IL_005c
  IL_005a:  leave.s    IL_00d9
  IL_005c:  ldloc.1
  IL_005d:  ldc.i4.1
  IL_005e:  add
  IL_005f:  ldc.i4.0
  IL_0060:  stloc.1
  IL_0061:  switch    (
  IL_008e,
  IL_0002,
  IL_000a,
  IL_000b,
  IL_0015,
  IL_0024,
  IL_002d,
  IL_0030,
  IL_003d,
  IL_005a)
  IL_008e:  leave.s    IL_00ce
  IL_0090:  ldloc.2
  IL_0091:  stloc.1
  IL_0092:  ldloc.0
  IL_0093:  ldc.i4.s   -2
  IL_0095:  bgt.s      IL_009a
  IL_0097:  ldc.i4.1
  IL_0098:  br.s       IL_009b
  IL_009a:  ldloc.0
  IL_009b:  switch    (
  IL_00ac,
  IL_005c,
  IL_002f)
  IL_00ac:  leave.s    IL_00ce
}
  filter
{
  IL_00ae:  isinst     "System.Exception"
  IL_00b3:  ldnull
  IL_00b4:  cgt.un
  IL_00b6:  ldloc.0
  IL_00b7:  ldc.i4.0
  IL_00b8:  cgt.un
  IL_00ba:  and
  IL_00bb:  ldloc.1
  IL_00bc:  ldc.i4.0
  IL_00bd:  ceq
  IL_00bf:  and
  IL_00c0:  endfilter
}  // end filter
{  // handler
  IL_00c2:  castclass  "System.Exception"
  IL_00c7:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00cc:  leave.s    IL_0090
}
  IL_00ce:  ldc.i4     0x800a0033
  IL_00d3:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d8:  throw
  IL_00d9:  ldloc.1
  IL_00da:  ldc.i4.0
  IL_00db:  ceq
  IL_00dd:  stloc.3
  IL_00de:  ldloc.3
  IL_00df:  brtrue.s   IL_00e7
  IL_00e1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00e6:  nop
  IL_00e7:  ret
}]]>)

            ' Changed from verifying output to checking IL - Bug 717949
            ' Leaving in expected output for  information purpose

            '            Dim expected =
            '            <![CDATA[
            'Test - 0
            'M0 - 0
            'M1 - -1
            'OnError
            'M2 - 0
            'Test - 1
            'M0 - -1
            'OnError
            'M1 - 0
            'M2 - 0
            'Test - 2
            'M0 - -1
            'OnError
            'M1 - -1
            'OnError
            'M2 - 0
            ']]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      201 (0xc9)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.3
  IL_0008:  stloc.2
  IL_0009:  ldc.i4.0
  IL_000a:  call       "Function Program.M(Integer) As Integer"
  IL_000f:  pop
  IL_0010:  ldc.i4.4
  IL_0011:  stloc.2
  IL_0012:  ldc.i4.1
  IL_0013:  call       "Function Program.M(Integer) As Integer"
  IL_0018:  brtrue.s   IL_0007
  IL_001a:  ldc.i4.5
  IL_001b:  stloc.2
  IL_001c:  ldc.i4.2
  IL_001d:  call       "Function Program.M(Integer) As Integer"
  IL_0022:  pop
    IL_0023:  leave      IL_00c0
  IL_0028:  ldc.i4.7
  IL_0029:  stloc.2
  IL_002a:  ldstr      "OnError"
  IL_002f:  call       "Sub System.Console.WriteLine(String)"
  IL_0034:  ldc.i4.8
  IL_0035:  stloc.2
  IL_0036:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_003b:  ldloc.1
  IL_003c:  brtrue.s   IL_004b
  IL_003e:  ldc.i4     0x800a0014
  IL_0043:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0048:  throw
    IL_0049:  leave.s    IL_00c0
  IL_004b:  ldloc.1
  IL_004c:  ldc.i4.1
  IL_004d:  add
  IL_004e:  ldc.i4.0
  IL_004f:  stloc.1
  IL_0050:  switch    (
  IL_007d,
  IL_0000,
  IL_0007,
  IL_0007,
  IL_0010,
  IL_001a,
  IL_0049,
  IL_0028,
  IL_0034,
  IL_0049)
    IL_007d:  leave.s    IL_00b5
  IL_007f:  ldloc.2
  IL_0080:  stloc.1
  IL_0081:  ldloc.0
    IL_0082:  switch    (
        IL_0093,
  IL_004b,
  IL_0028)
    IL_0093:  leave.s    IL_00b5
}
  filter
{
    IL_0095:  isinst     "System.Exception"
    IL_009a:  ldnull
    IL_009b:  cgt.un
    IL_009d:  ldloc.0
    IL_009e:  ldc.i4.0
    IL_009f:  cgt.un
    IL_00a1:  and
    IL_00a2:  ldloc.1
    IL_00a3:  ldc.i4.0
    IL_00a4:  ceq
    IL_00a6:  and
    IL_00a7:  endfilter
}  // end filter
{  // handler
    IL_00a9:  castclass  "System.Exception"
    IL_00ae:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
    IL_00b3:  leave.s    IL_007f
}
  IL_00b5:  ldc.i4     0x800a0033
  IL_00ba:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00bf:  throw
  IL_00c0:  ldloc.1
  IL_00c1:  brfalse.s  IL_00c8
  IL_00c3:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c8:  ret
}]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_Do_Loop_While_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Do 
            If M4()
                Continue Do
            End If
            M1()
ContinueLabel:
        Loop While M2()
        M3()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Function M2() As Boolean
        Return False
    End Function

    Sub M3()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      170 (0xaa)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.4
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.M4() As Boolean"
  IL_0015:  brtrue.s   IL_001e
  IL_0017:  ldc.i4.6
  IL_0018:  stloc.2
  IL_0019:  call       "Sub Program.M1()"
  IL_001e:  ldc.i4.7
  IL_001f:  stloc.2
  IL_0020:  call       "Function Program.M2() As Boolean"
  IL_0025:  brtrue.s   IL_000e
  IL_0027:  ldc.i4.8
  IL_0028:  stloc.2
  IL_0029:  call       "Sub Program.M3()"
  IL_002e:  leave.s    IL_00a1
  IL_0030:  ldloc.1
  IL_0031:  ldc.i4.1
  IL_0032:  add
  IL_0033:  ldc.i4.0
  IL_0034:  stloc.1
  IL_0035:  switch    (
  IL_0062,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_000e,
  IL_001e,
  IL_0017,
  IL_001e,
  IL_0027,
  IL_002e)
  IL_0062:  leave.s    IL_0096
  IL_0064:  ldloc.2
  IL_0065:  stloc.1
  IL_0066:  ldloc.0
  IL_0067:  switch    (
  IL_0074,
  IL_0030)
  IL_0074:  leave.s    IL_0096
}
  filter
{
  IL_0076:  isinst     "System.Exception"
  IL_007b:  ldnull
  IL_007c:  cgt.un
  IL_007e:  ldloc.0
  IL_007f:  ldc.i4.0
  IL_0080:  cgt.un
  IL_0082:  and
  IL_0083:  ldloc.1
  IL_0084:  ldc.i4.0
  IL_0085:  ceq
  IL_0087:  and
  IL_0088:  endfilter
}  // end filter
{  // handler
  IL_008a:  castclass  "System.Exception"
  IL_008f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0094:  leave.s    IL_0064
}
  IL_0096:  ldc.i4     0x800a0033
  IL_009b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a0:  throw
  IL_00a1:  ldloc.1
  IL_00a2:  brfalse.s  IL_00a9
  IL_00a4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a9:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      210 (0xd2)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  nop
  IL_0014:  ldc.i4.4
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.M4() As Boolean"
  IL_001b:  ldc.i4.0
  IL_001c:  ceq
  IL_001e:  stloc.3
  IL_001f:  ldloc.3
  IL_0020:  brtrue.s   IL_0025
  IL_0022:  br.s       IL_0030
  IL_0024:  nop
  IL_0025:  nop
  IL_0026:  ldc.i4.7
  IL_0027:  stloc.2
  IL_0028:  call       "Sub Program.M1()"
  IL_002d:  nop
  IL_002e:  nop
  IL_002f:  nop
  IL_0030:  ldc.i4.8
  IL_0031:  stloc.2
  IL_0032:  call       "Function Program.M2() As Boolean"
  IL_0037:  stloc.3
  IL_0038:  ldloc.3
  IL_0039:  brtrue.s   IL_0013
  IL_003b:  ldc.i4.s   9
  IL_003d:  stloc.2
  IL_003e:  call       "Sub Program.M3()"
  IL_0043:  nop
  IL_0044:  leave.s    IL_00c3
  IL_0046:  ldloc.1
  IL_0047:  ldc.i4.1
  IL_0048:  add
  IL_0049:  ldc.i4.0
  IL_004a:  stloc.1
  IL_004b:  switch    (
  IL_007c,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0014,
  IL_0022,
  IL_0024,
  IL_0026,
  IL_0030,
  IL_003b,
  IL_0044)
  IL_007c:  leave.s    IL_00b8
  IL_007e:  ldloc.2
  IL_007f:  stloc.1
  IL_0080:  ldloc.0
  IL_0081:  ldc.i4.s   -2
  IL_0083:  bgt.s      IL_0088
  IL_0085:  ldc.i4.1
  IL_0086:  br.s       IL_0089
  IL_0088:  ldloc.0
  IL_0089:  switch    (
  IL_0096,
  IL_0046)
  IL_0096:  leave.s    IL_00b8
}
  filter
{
  IL_0098:  isinst     "System.Exception"
  IL_009d:  ldnull
  IL_009e:  cgt.un
  IL_00a0:  ldloc.0
  IL_00a1:  ldc.i4.0
  IL_00a2:  cgt.un
  IL_00a4:  and
  IL_00a5:  ldloc.1
  IL_00a6:  ldc.i4.0
  IL_00a7:  ceq
  IL_00a9:  and
  IL_00aa:  endfilter
}  // end filter
{  // handler
  IL_00ac:  castclass  "System.Exception"
  IL_00b1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b6:  leave.s    IL_007e
}
  IL_00b8:  ldc.i4     0x800a0033
  IL_00bd:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c2:  throw
  IL_00c3:  ldloc.1
  IL_00c4:  ldc.i4.0
  IL_00c5:  ceq
  IL_00c7:  stloc.3
  IL_00c8:  ldloc.3
  IL_00c9:  brtrue.s   IL_00d1
  IL_00cb:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d0:  nop
  IL_00d1:  ret
}
]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_Do_Loop_Until_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Do 
            If M4()
                Continue Do
            End If
            M1()
ContinueLabel:
        Loop Until M2()
        M3()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Function M2() As Boolean
        Return False
    End Function

    Sub M3()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      170 (0xaa)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.4
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.M4() As Boolean"
  IL_0015:  brtrue.s   IL_001e
  IL_0017:  ldc.i4.6
  IL_0018:  stloc.2
  IL_0019:  call       "Sub Program.M1()"
  IL_001e:  ldc.i4.7
  IL_001f:  stloc.2
  IL_0020:  call       "Function Program.M2() As Boolean"
  IL_0025:  brfalse.s  IL_000e
  IL_0027:  ldc.i4.8
  IL_0028:  stloc.2
  IL_0029:  call       "Sub Program.M3()"
  IL_002e:  leave.s    IL_00a1
  IL_0030:  ldloc.1
  IL_0031:  ldc.i4.1
  IL_0032:  add
  IL_0033:  ldc.i4.0
  IL_0034:  stloc.1
  IL_0035:  switch    (
  IL_0062,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_000e,
  IL_001e,
  IL_0017,
  IL_001e,
  IL_0027,
  IL_002e)
  IL_0062:  leave.s    IL_0096
  IL_0064:  ldloc.2
  IL_0065:  stloc.1
  IL_0066:  ldloc.0
  IL_0067:  switch    (
  IL_0074,
  IL_0030)
  IL_0074:  leave.s    IL_0096
}
  filter
{
  IL_0076:  isinst     "System.Exception"
  IL_007b:  ldnull
  IL_007c:  cgt.un
  IL_007e:  ldloc.0
  IL_007f:  ldc.i4.0
  IL_0080:  cgt.un
  IL_0082:  and
  IL_0083:  ldloc.1
  IL_0084:  ldc.i4.0
  IL_0085:  ceq
  IL_0087:  and
  IL_0088:  endfilter
}  // end filter
{  // handler
  IL_008a:  castclass  "System.Exception"
  IL_008f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0094:  leave.s    IL_0064
}
  IL_0096:  ldc.i4     0x800a0033
  IL_009b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a0:  throw
  IL_00a1:  ldloc.1
  IL_00a2:  brfalse.s  IL_00a9
  IL_00a4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a9:  ret
}
]]>)
        End Sub

        <Fact>
        Sub Resume_in_Do_Loop_Until_1_Debug()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Do 
            If M4()
                Continue Do
            End If
            M1()
ContinueLabel:
        Loop Until M2()
        M3()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Function M2() As Boolean
        Return False
    End Function

    Sub M3()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      213 (0xd5)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  nop
  IL_0014:  ldc.i4.4
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.M4() As Boolean"
  IL_001b:  ldc.i4.0
  IL_001c:  ceq
  IL_001e:  stloc.3
  IL_001f:  ldloc.3
  IL_0020:  brtrue.s   IL_0025
  IL_0022:  br.s       IL_0030
  IL_0024:  nop
  IL_0025:  nop
  IL_0026:  ldc.i4.7
  IL_0027:  stloc.2
  IL_0028:  call       "Sub Program.M1()"
  IL_002d:  nop
  IL_002e:  nop
  IL_002f:  nop
  IL_0030:  ldc.i4.8
  IL_0031:  stloc.2
  IL_0032:  call       "Function Program.M2() As Boolean"
  IL_0037:  ldc.i4.0
  IL_0038:  ceq
  IL_003a:  stloc.3
  IL_003b:  ldloc.3
  IL_003c:  brtrue.s   IL_0013
  IL_003e:  ldc.i4.s   9
  IL_0040:  stloc.2
  IL_0041:  call       "Sub Program.M3()"
  IL_0046:  nop
  IL_0047:  leave.s    IL_00c6
  IL_0049:  ldloc.1
  IL_004a:  ldc.i4.1
  IL_004b:  add
  IL_004c:  ldc.i4.0
  IL_004d:  stloc.1
  IL_004e:  switch    (
  IL_007f,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0014,
  IL_0022,
  IL_0024,
  IL_0026,
  IL_0030,
  IL_003e,
  IL_0047)
  IL_007f:  leave.s    IL_00bb
  IL_0081:  ldloc.2
  IL_0082:  stloc.1
  IL_0083:  ldloc.0
  IL_0084:  ldc.i4.s   -2
  IL_0086:  bgt.s      IL_008b
  IL_0088:  ldc.i4.1
  IL_0089:  br.s       IL_008c
  IL_008b:  ldloc.0
  IL_008c:  switch    (
  IL_0099,
  IL_0049)
  IL_0099:  leave.s    IL_00bb
}
  filter
{
  IL_009b:  isinst     "System.Exception"
  IL_00a0:  ldnull
  IL_00a1:  cgt.un
  IL_00a3:  ldloc.0
  IL_00a4:  ldc.i4.0
  IL_00a5:  cgt.un
  IL_00a7:  and
  IL_00a8:  ldloc.1
  IL_00a9:  ldc.i4.0
  IL_00aa:  ceq
  IL_00ac:  and
  IL_00ad:  endfilter
}  // end filter
{  // handler
  IL_00af:  castclass  "System.Exception"
  IL_00b4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b9:  leave.s    IL_0081
}
  IL_00bb:  ldc.i4     0x800a0033
  IL_00c0:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c5:  throw
  IL_00c6:  ldloc.1
  IL_00c7:  ldc.i4.0
  IL_00c8:  ceq
  IL_00ca:  stloc.3
  IL_00cb:  ldloc.3
  IL_00cc:  brtrue.s   IL_00d4
  IL_00ce:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d3:  nop
  IL_00d4:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Do_Loop_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({1, -1, 0})}

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("Test - {0}", i)
            state = states(i)
            current = 0
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        Do
            If M(0) = 0 Then
                Return
            End If
            M(1)
        Loop
        Return
OnError:
        System.Console.WriteLine("OnError")
        Resume Next
    End Sub

    Function M(num As Integer) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M{0} - {1}", num, val)
        If val = -1 Then
            Throw New System.NotSupportedException()
        End If

        Return val
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      242 (0xf2)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  nop
  IL_000b:  ldc.i4.3
  IL_000c:  stloc.2
  IL_000d:  ldc.i4.0
  IL_000e:  call       "Function Program.M(Integer) As Integer"
  IL_0013:  ldc.i4.0
  IL_0014:  cgt.un
  IL_0016:  stloc.3
  IL_0017:  ldloc.3
  IL_0018:  brtrue.s   IL_001d
  IL_001a:  br.s       IL_0059
  IL_001c:  nop
  IL_001d:  nop
  IL_001e:  ldc.i4.6
  IL_001f:  stloc.2
  IL_0020:  ldc.i4.1
  IL_0021:  call       "Function Program.M(Integer) As Integer"
  IL_0026:  pop
  IL_0027:  nop
  IL_0028:  br.s       IL_000a
  IL_002a:  br.s       IL_0059
  IL_002c:  nop
  IL_002d:  ldc.i4.s   9
  IL_002f:  stloc.2
  IL_0030:  ldstr      "OnError"
  IL_0035:  call       "Sub System.Console.WriteLine(String)"
  IL_003a:  nop
  IL_003b:  ldc.i4.s   10
  IL_003d:  stloc.2
  IL_003e:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0043:  nop
  IL_0044:  ldloc.1
  IL_0045:  ldc.i4.0
  IL_0046:  cgt.un
  IL_0048:  stloc.3
  IL_0049:  ldloc.3
  IL_004a:  brtrue.s   IL_0057
  IL_004c:  ldc.i4     0x800a0014
  IL_0051:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0056:  throw
  IL_0057:  br.s       IL_005e
  IL_0059:  leave      IL_00e3
  IL_005e:  ldloc.1
  IL_005f:  ldc.i4.1
  IL_0060:  add
  IL_0061:  ldc.i4.0
  IL_0062:  stloc.1
  IL_0063:  switch    (
  IL_0098,
  IL_0002,
  IL_000a,
  IL_000b,
  IL_001a,
  IL_001c,
  IL_001e,
  IL_0027,
  IL_002a,
  IL_002d,
  IL_003b,
  IL_0059)
  IL_0098:  leave.s    IL_00d8
  IL_009a:  ldloc.2
  IL_009b:  stloc.1
  IL_009c:  ldloc.0
  IL_009d:  ldc.i4.s   -2
  IL_009f:  bgt.s      IL_00a4
  IL_00a1:  ldc.i4.1
  IL_00a2:  br.s       IL_00a5
  IL_00a4:  ldloc.0
  IL_00a5:  switch    (
  IL_00b6,
  IL_005e,
  IL_002c)
  IL_00b6:  leave.s    IL_00d8
}
  filter
{
  IL_00b8:  isinst     "System.Exception"
  IL_00bd:  ldnull
  IL_00be:  cgt.un
  IL_00c0:  ldloc.0
  IL_00c1:  ldc.i4.0
  IL_00c2:  cgt.un
  IL_00c4:  and
  IL_00c5:  ldloc.1
  IL_00c6:  ldc.i4.0
  IL_00c7:  ceq
  IL_00c9:  and
  IL_00ca:  endfilter
}  // end filter
{  // handler
  IL_00cc:  castclass  "System.Exception"
  IL_00d1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00d6:  leave.s    IL_009a
}
  IL_00d8:  ldc.i4     0x800a0033
  IL_00dd:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e2:  throw
  IL_00e3:  ldloc.1
  IL_00e4:  ldc.i4.0
  IL_00e5:  ceq
  IL_00e7:  stloc.3
  IL_00e8:  ldloc.3
  IL_00e9:  brtrue.s   IL_00f1
  IL_00eb:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00f0:  nop
  IL_00f1:  ret
}]]>)

            ' Changed from verifying output to checking IL - Bug 717949
            ' Leaving in expected output for  information purpose

            '            Dim expected =
            '            <![CDATA[
            'Test - 0
            'M0 - 1
            'M1 - -1
            'OnError
            'M0 - 0]]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      204 (0xcc)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.3
  IL_0008:  stloc.2
  IL_0009:  ldc.i4.0
  IL_000a:  call       "Function Program.M(Integer) As Integer"
  IL_000f:  brtrue.s   IL_0016
    IL_0011:  leave      IL_00c3
  IL_0016:  ldc.i4.6
  IL_0017:  stloc.2
  IL_0018:  ldc.i4.1
  IL_0019:  call       "Function Program.M(Integer) As Integer"
  IL_001e:  pop
  IL_001f:  br.s       IL_0007
  IL_0021:  ldc.i4.s   9
  IL_0023:  stloc.2
  IL_0024:  ldstr      "OnError"
  IL_0029:  call       "Sub System.Console.WriteLine(String)"
  IL_002e:  ldc.i4.s   10
  IL_0030:  stloc.2
  IL_0031:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0036:  ldloc.1
    IL_0037:  brtrue.s   IL_0046
  IL_0039:  ldc.i4     0x800a0014
  IL_003e:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0043:  throw
    IL_0044:  leave.s    IL_00c3
    IL_0046:  ldloc.1
    IL_0047:  ldc.i4.1
    IL_0048:  add
    IL_0049:  ldc.i4.0
    IL_004a:  stloc.1
    IL_004b:  switch    (
        IL_0080,
  IL_0000,
  IL_0007,
  IL_0007,
  IL_0044,
  IL_0016,
  IL_0016,
  IL_0007,
  IL_0044,
  IL_0021,
  IL_002e,
  IL_0044)
    IL_0080:  leave.s    IL_00b8
    IL_0082:  ldloc.2
    IL_0083:  stloc.1
    IL_0084:  ldloc.0
    IL_0085:  switch    (
        IL_0096,
        IL_0046,
  IL_0021)
    IL_0096:  leave.s    IL_00b8
}
  filter
{
    IL_0098:  isinst     "System.Exception"
    IL_009d:  ldnull
    IL_009e:  cgt.un
    IL_00a0:  ldloc.0
    IL_00a1:  ldc.i4.0
    IL_00a2:  cgt.un
    IL_00a4:  and
    IL_00a5:  ldloc.1
    IL_00a6:  ldc.i4.0
    IL_00a7:  ceq
    IL_00a9:  and
    IL_00aa:  endfilter
}  // end filter
{  // handler
    IL_00ac:  castclass  "System.Exception"
    IL_00b1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
    IL_00b6:  leave.s    IL_0082
}
  IL_00b8:  ldc.i4     0x800a0033
  IL_00bd:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c2:  throw
  IL_00c3:  ldloc.1
  IL_00c4:  brfalse.s  IL_00cb
  IL_00c6:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00cb:  ret
}]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_Do_Loop_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        Do 
            If M4()
                Continue Do
            End If
            M1()
ContinueLabel:
        Loop 
        M2()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Sub M2()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      163 (0xa3)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.4
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.M4() As Boolean"
  IL_0015:  brtrue.s   IL_000e
  IL_0017:  ldc.i4.6
  IL_0018:  stloc.2
  IL_0019:  call       "Sub Program.M1()"
  IL_001e:  br.s       IL_000e
  IL_0020:  ldc.i4.8
  IL_0021:  stloc.2
  IL_0022:  call       "Sub Program.M2()"
  IL_0027:  leave.s    IL_009a
  IL_0029:  ldloc.1
  IL_002a:  ldc.i4.1
  IL_002b:  add
  IL_002c:  ldc.i4.0
  IL_002d:  stloc.1
  IL_002e:  switch    (
  IL_005b,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_000e,
  IL_000e,
  IL_0017,
  IL_000e,
  IL_0020,
  IL_0027)
  IL_005b:  leave.s    IL_008f
  IL_005d:  ldloc.2
  IL_005e:  stloc.1
  IL_005f:  ldloc.0
  IL_0060:  switch    (
  IL_006d,
  IL_0029)
  IL_006d:  leave.s    IL_008f
}
  filter
{
  IL_006f:  isinst     "System.Exception"
  IL_0074:  ldnull
  IL_0075:  cgt.un
  IL_0077:  ldloc.0
  IL_0078:  ldc.i4.0
  IL_0079:  cgt.un
  IL_007b:  and
  IL_007c:  ldloc.1
  IL_007d:  ldc.i4.0
  IL_007e:  ceq
  IL_0080:  and
  IL_0081:  endfilter
}  // end filter
{  // handler
  IL_0083:  castclass  "System.Exception"
  IL_0088:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_008d:  leave.s    IL_005d
}
  IL_008f:  ldc.i4     0x800a0033
  IL_0094:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0099:  throw
  IL_009a:  ldloc.1
  IL_009b:  brfalse.s  IL_00a2
  IL_009d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a2:  ret
}
]]>)


            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      201 (0xc9)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  nop
  IL_0014:  ldc.i4.4
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.M4() As Boolean"
  IL_001b:  ldc.i4.0
  IL_001c:  ceq
  IL_001e:  stloc.3
  IL_001f:  ldloc.3
  IL_0020:  brtrue.s   IL_0025
  IL_0022:  br.s       IL_0030
  IL_0024:  nop
  IL_0025:  nop
  IL_0026:  ldc.i4.7
  IL_0027:  stloc.2
  IL_0028:  call       "Sub Program.M1()"
  IL_002d:  nop
  IL_002e:  nop
  IL_002f:  nop
  IL_0030:  br.s       IL_0013
  IL_0032:  ldc.i4.s   9
  IL_0034:  stloc.2
  IL_0035:  call       "Sub Program.M2()"
  IL_003a:  nop
  IL_003b:  leave.s    IL_00ba
  IL_003d:  ldloc.1
  IL_003e:  ldc.i4.1
  IL_003f:  add
  IL_0040:  ldc.i4.0
  IL_0041:  stloc.1
  IL_0042:  switch    (
  IL_0073,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0014,
  IL_0022,
  IL_0024,
  IL_0026,
  IL_002f,
  IL_0032,
  IL_003b)
  IL_0073:  leave.s    IL_00af
  IL_0075:  ldloc.2
  IL_0076:  stloc.1
  IL_0077:  ldloc.0
  IL_0078:  ldc.i4.s   -2
  IL_007a:  bgt.s      IL_007f
  IL_007c:  ldc.i4.1
  IL_007d:  br.s       IL_0080
  IL_007f:  ldloc.0
  IL_0080:  switch    (
  IL_008d,
  IL_003d)
  IL_008d:  leave.s    IL_00af
}
  filter
{
  IL_008f:  isinst     "System.Exception"
  IL_0094:  ldnull
  IL_0095:  cgt.un
  IL_0097:  ldloc.0
  IL_0098:  ldc.i4.0
  IL_0099:  cgt.un
  IL_009b:  and
  IL_009c:  ldloc.1
  IL_009d:  ldc.i4.0
  IL_009e:  ceq
  IL_00a0:  and
  IL_00a1:  endfilter
}  // end filter
{  // handler
  IL_00a3:  castclass  "System.Exception"
  IL_00a8:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ad:  leave.s    IL_0075
}
  IL_00af:  ldc.i4     0x800a0033
  IL_00b4:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b9:  throw
  IL_00ba:  ldloc.1
  IL_00bb:  ldc.i4.0
  IL_00bc:  ceq
  IL_00be:  stloc.3
  IL_00bf:  ldloc.3
  IL_00c0:  brtrue.s   IL_00c8
  IL_00c2:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c7:  nop
  IL_00c8:  ret
}
]]>)
        End Sub

        <Fact(), WorkItem(737273, "DevDiv")>
        Public Sub Resume_in_ForEach_Enumerable_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System
Imports System.Collections

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({1, 1, 1, 1, 1, -1, -1, 0, 0, 0}),
                      ({1, 1, 1, 1, -1, 0, 0, 0, 0, 0}),
                      ({1, 1, 1, -1, 0, 0, 0, 0, 0}),
                      ({1, 1, -1, 0, 0, 0}),
                      ({1, -1, 1, 1, 1}),
                      ({1, 1, 1, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0}),
                      ({-1, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Test()
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        M("3")
        For Each o In GetEnumerable()
            M("0")
            M("1")
        Next
        M("2")
        Return
OnError:
        System.Console.WriteLine("OnError - {0}", Microsoft.VisualBasic.Information.Err.GetException().GetType())
        Resume Next
    End Sub

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException()
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
    End Class

    Function GetEnumerable() As IEnumerable
        M("GetEnumerable")
        Return New Enumerable()
    End Function

    Class Enumerable
        Implements IEnumerable

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            M("GetEnumerator")
            Return New Enumerator()
        End Function
    End Class

    Class Enumerator
        Implements IEnumerator, IDisposable

        Public ReadOnly Property Current As Object Implements IEnumerator.Current
            Get
                M("Current")
                Return Nothing
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            Return M("MoveNext") <> 0
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            M("Dispose")
        End Sub

    End Class
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            CompileAndVerify(compilation).
                VerifyIL("Program.Test", <![CDATA[
{
  // Code size      315 (0x13b)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                System.Collections.IEnumerator V_3)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "3"
  IL_000e:  call       "Function Program.M(String) As Integer"
  IL_0013:  pop
  IL_0014:  ldc.i4.3
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_001b:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_0020:  stloc.3
  IL_0021:  br.s       IL_004b
  IL_0023:  ldloc.3
  IL_0024:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_0029:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_002e:  pop
  IL_002f:  ldc.i4.4
  IL_0030:  stloc.2
  IL_0031:  ldstr      "0"
  IL_0036:  call       "Function Program.M(String) As Integer"
  IL_003b:  pop
  IL_003c:  ldc.i4.5
  IL_003d:  stloc.2
  IL_003e:  ldstr      "1"
  IL_0043:  call       "Function Program.M(String) As Integer"
  IL_0048:  pop
  IL_0049:  ldc.i4.6
  IL_004a:  stloc.2
  IL_004b:  ldloc.3
  IL_004c:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0051:  brtrue.s   IL_0023
  IL_0053:  ldc.i4.7
  IL_0054:  stloc.2
  IL_0055:  ldloc.3
  IL_0056:  isinst     "System.IDisposable"
  IL_005b:  brfalse.s  IL_0068
  IL_005d:  ldloc.3
  IL_005e:  isinst     "System.IDisposable"
  IL_0063:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0068:  ldc.i4.8
  IL_0069:  stloc.2
  IL_006a:  ldstr      "2"
  IL_006f:  call       "Function Program.M(String) As Integer"
  IL_0074:  pop
  IL_0075:  leave      IL_0132
  IL_007a:  ldc.i4.s   10
  IL_007c:  stloc.2
  IL_007d:  ldstr      "OnError - {0}"
  IL_0082:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_0087:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_008c:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_0091:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_0096:  ldc.i4.s   11
  IL_0098:  stloc.2
  IL_0099:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_009e:  ldloc.1
  IL_009f:  brtrue.s   IL_00b1
  IL_00a1:  ldc.i4     0x800a0014
  IL_00a6:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ab:  throw
  IL_00ac:  leave      IL_0132
  IL_00b1:  ldloc.1
  IL_00b2:  ldc.i4.1
  IL_00b3:  add
  IL_00b4:  ldc.i4.0
  IL_00b5:  stloc.1
  IL_00b6:  switch    (
  IL_00ef,
  IL_0000,
  IL_0007,
  IL_0014,
  IL_002f,
  IL_003c,
  IL_0049,
  IL_0053,
  IL_0068,
  IL_00ac,
  IL_007a,
  IL_0096,
  IL_00ac)
  IL_00ef:  leave.s    IL_0127
  IL_00f1:  ldloc.2
  IL_00f2:  stloc.1
  IL_00f3:  ldloc.0
  IL_00f4:  switch    (
  IL_0105,
  IL_00b1,
  IL_007a)
  IL_0105:  leave.s    IL_0127
}
  filter
{
  IL_0107:  isinst     "System.Exception"
  IL_010c:  ldnull
  IL_010d:  cgt.un
  IL_010f:  ldloc.0
  IL_0110:  ldc.i4.0
  IL_0111:  cgt.un
  IL_0113:  and
  IL_0114:  ldloc.1
  IL_0115:  ldc.i4.0
  IL_0116:  ceq
  IL_0118:  and
  IL_0119:  endfilter
}  // end filter
{  // handler
  IL_011b:  castclass  "System.Exception"
  IL_0120:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0125:  leave.s    IL_00f1
}
  IL_0127:  ldc.i4     0x800a0033
  IL_012c:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0131:  throw
  IL_0132:  ldloc.1
  IL_0133:  brfalse.s  IL_013a
  IL_0135:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_013a:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation).
                VerifyIL("Program.Test", <![CDATA[
{
  // Code size      356 (0x164)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  System.Collections.IEnumerator V_3, //VB$ForEachEnumerator
  Object V_4, //o
  Boolean V_5)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  ldc.i4.2
  IL_000b:  stloc.2
  IL_000c:  ldstr      "3"
  IL_0011:  call       "Function Program.M(String) As Integer"
  IL_0016:  pop
  IL_0017:  ldc.i4.3
  IL_0018:  stloc.2
  IL_0019:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_001e:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_0023:  stloc.3
  IL_0024:  br.s       IL_004f
  IL_0026:  ldloc.3
  IL_0027:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_002c:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_0031:  stloc.s    V_4
  IL_0033:  ldc.i4.4
  IL_0034:  stloc.2
  IL_0035:  ldstr      "0"
  IL_003a:  call       "Function Program.M(String) As Integer"
  IL_003f:  pop
  IL_0040:  ldc.i4.5
  IL_0041:  stloc.2
  IL_0042:  ldstr      "1"
  IL_0047:  call       "Function Program.M(String) As Integer"
  IL_004c:  pop
  IL_004d:  ldc.i4.6
  IL_004e:  stloc.2
  IL_004f:  ldloc.3
  IL_0050:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0055:  stloc.s    V_5
  IL_0057:  ldloc.s    V_5
  IL_0059:  brtrue.s   IL_0026
  IL_005b:  ldc.i4.7
  IL_005c:  stloc.2
  IL_005d:  ldloc.3
  IL_005e:  isinst     "System.IDisposable"
  IL_0063:  ldnull
  IL_0064:  ceq
  IL_0066:  stloc.s    V_5
  IL_0068:  ldloc.s    V_5
  IL_006a:  brtrue.s   IL_0078
  IL_006c:  ldloc.3
  IL_006d:  isinst     "System.IDisposable"
  IL_0072:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0077:  nop
  IL_0078:  ldc.i4.8
  IL_0079:  stloc.2
  IL_007a:  ldstr      "2"
  IL_007f:  call       "Function Program.M(String) As Integer"
  IL_0084:  pop
  IL_0085:  br.s       IL_00c5
  IL_0087:  nop
  IL_0088:  ldc.i4.s   10
  IL_008a:  stloc.2
  IL_008b:  ldstr      "OnError - {0}"
  IL_0090:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_0095:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_009a:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_009f:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_00a4:  nop
  IL_00a5:  ldc.i4.s   11
  IL_00a7:  stloc.2
  IL_00a8:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ad:  nop
  IL_00ae:  ldloc.1
  IL_00af:  ldc.i4.0
  IL_00b0:  cgt.un
  IL_00b2:  stloc.s    V_5
  IL_00b4:  ldloc.s    V_5
  IL_00b6:  brtrue.s   IL_00c3
  IL_00b8:  ldc.i4     0x800a0014
  IL_00bd:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c2:  throw
  IL_00c3:  br.s       IL_00ca
  IL_00c5:  leave      IL_0153
  IL_00ca:  ldloc.1
  IL_00cb:  ldc.i4.1
  IL_00cc:  add
  IL_00cd:  ldc.i4.0
  IL_00ce:  stloc.1
  IL_00cf:  switch    (
  IL_0108,
  IL_0002,
  IL_000a,
  IL_0017,
  IL_0033,
  IL_0040,
  IL_004d,
  IL_005b,
  IL_0078,
  IL_0085,
  IL_0088,
  IL_00a5,
  IL_00c5)
  IL_0108:  leave.s    IL_0148
  IL_010a:  ldloc.2
  IL_010b:  stloc.1
  IL_010c:  ldloc.0
  IL_010d:  ldc.i4.s   -2
  IL_010f:  bgt.s      IL_0114
  IL_0111:  ldc.i4.1
  IL_0112:  br.s       IL_0115
  IL_0114:  ldloc.0
  IL_0115:  switch    (
  IL_0126,
  IL_00ca,
  IL_0087)
  IL_0126:  leave.s    IL_0148
}
  filter
{
  IL_0128:  isinst     "System.Exception"
  IL_012d:  ldnull
  IL_012e:  cgt.un
  IL_0130:  ldloc.0
  IL_0131:  ldc.i4.0
  IL_0132:  cgt.un
  IL_0134:  and
  IL_0135:  ldloc.1
  IL_0136:  ldc.i4.0
  IL_0137:  ceq
  IL_0139:  and
  IL_013a:  endfilter
}  // end filter
{  // handler
  IL_013c:  castclass  "System.Exception"
  IL_0141:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0146:  leave.s    IL_010a
}
  IL_0148:  ldc.i4     0x800a0033
  IL_014d:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0152:  throw
  IL_0153:  ldloc.1
  IL_0154:  ldc.i4.0
  IL_0155:  ceq
  IL_0157:  stloc.s    V_5
  IL_0159:  ldloc.s    V_5
  IL_015b:  brtrue.s   IL_0163
  IL_015d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0162:  nop
  IL_0163:  ret
}
]]>)

        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_ForEach_Enumerable_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        For Each o In GetEnumerable()
            If M4()
                Continue For
            End If
            M1()
ContinueLabel:
        Next
        M2()
    End Sub

    Function GetEnumerable() As System.Collections.IEnumerable
        Return Nothing
    End Function

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Sub M2()
    End Sub

    Function M4() As Boolean
        Return False
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      224 (0xe0)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  System.Collections.IEnumerator V_3) //VB$ForEachEnumerator
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_0015:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_001a:  stloc.3
  IL_001b:  br.s       IL_003b
  IL_001d:  ldloc.3
  IL_001e:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_0023:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_0028:  pop
  IL_0029:  ldc.i4.4
  IL_002a:  stloc.2
  IL_002b:  call       "Function Program.M4() As Boolean"
  IL_0030:  brtrue.s   IL_0039
  IL_0032:  ldc.i4.6
  IL_0033:  stloc.2
  IL_0034:  call       "Sub Program.M1()"
  IL_0039:  ldc.i4.7
  IL_003a:  stloc.2
  IL_003b:  ldloc.3
  IL_003c:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0041:  brtrue.s   IL_001d
  IL_0043:  ldc.i4.8
  IL_0044:  stloc.2
  IL_0045:  ldloc.3
  IL_0046:  isinst     "System.IDisposable"
  IL_004b:  brfalse.s  IL_0058
  IL_004d:  ldloc.3
  IL_004e:  isinst     "System.IDisposable"
  IL_0053:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0058:  ldc.i4.s   9
  IL_005a:  stloc.2
  IL_005b:  call       "Sub Program.M2()"
  IL_0060:  leave.s    IL_00d7
  IL_0062:  ldloc.1
  IL_0063:  ldc.i4.1
  IL_0064:  add
  IL_0065:  ldc.i4.0
  IL_0066:  stloc.1
  IL_0067:  switch    (
  IL_0098,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_0029,
  IL_0039,
  IL_0032,
  IL_0039,
  IL_0043,
  IL_0058,
  IL_0060)
  IL_0098:  leave.s    IL_00cc
  IL_009a:  ldloc.2
  IL_009b:  stloc.1
  IL_009c:  ldloc.0
  IL_009d:  switch    (
  IL_00aa,
  IL_0062)
  IL_00aa:  leave.s    IL_00cc
}
  filter
{
  IL_00ac:  isinst     "System.Exception"
  IL_00b1:  ldnull
  IL_00b2:  cgt.un
  IL_00b4:  ldloc.0
  IL_00b5:  ldc.i4.0
  IL_00b6:  cgt.un
  IL_00b8:  and
  IL_00b9:  ldloc.1
  IL_00ba:  ldc.i4.0
  IL_00bb:  ceq
  IL_00bd:  and
  IL_00be:  endfilter
}  // end filter
{  // handler
  IL_00c0:  castclass  "System.Exception"
  IL_00c5:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ca:  leave.s    IL_009a
}
  IL_00cc:  ldc.i4     0x800a0033
  IL_00d1:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d6:  throw
  IL_00d7:  ldloc.1
  IL_00d8:  brfalse.s  IL_00df
  IL_00da:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00df:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      280 (0x118)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  System.Collections.IEnumerator V_3, //VB$ForEachEnumerator
  Object V_4, //o
  Boolean V_5)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_001a:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_001f:  stloc.3
  IL_0020:  br.s       IL_004e
  IL_0022:  ldloc.3
  IL_0023:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_0028:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_002d:  stloc.s    V_4
  IL_002f:  ldc.i4.4
  IL_0030:  stloc.2
  IL_0031:  call       "Function Program.M4() As Boolean"
  IL_0036:  ldc.i4.0
  IL_0037:  ceq
  IL_0039:  stloc.s    V_5
  IL_003b:  ldloc.s    V_5
  IL_003d:  brtrue.s   IL_0042
  IL_003f:  br.s       IL_004c
  IL_0041:  nop
  IL_0042:  nop
  IL_0043:  ldc.i4.7
  IL_0044:  stloc.2
  IL_0045:  call       "Sub Program.M1()"
  IL_004a:  nop
  IL_004b:  nop
  IL_004c:  ldc.i4.8
  IL_004d:  stloc.2
  IL_004e:  ldloc.3
  IL_004f:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0054:  stloc.s    V_5
  IL_0056:  ldloc.s    V_5
  IL_0058:  brtrue.s   IL_0022
  IL_005a:  ldc.i4.s   9
  IL_005c:  stloc.2
  IL_005d:  ldloc.3
  IL_005e:  isinst     "System.IDisposable"
  IL_0063:  ldnull
  IL_0064:  ceq
  IL_0066:  stloc.s    V_5
  IL_0068:  ldloc.s    V_5
  IL_006a:  brtrue.s   IL_0078
  IL_006c:  ldloc.3
  IL_006d:  isinst     "System.IDisposable"
  IL_0072:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0077:  nop
  IL_0078:  ldc.i4.s   10
  IL_007a:  stloc.2
  IL_007b:  call       "Sub Program.M2()"
  IL_0080:  nop
  IL_0081:  leave      IL_0107
  IL_0086:  ldloc.1
  IL_0087:  ldc.i4.1
  IL_0088:  add
  IL_0089:  ldc.i4.0
  IL_008a:  stloc.1
  IL_008b:  switch    (
  IL_00c0,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_002f,
  IL_003f,
  IL_0041,
  IL_0043,
  IL_004c,
  IL_005a,
  IL_0078,
  IL_0081)
  IL_00c0:  leave.s    IL_00fc
  IL_00c2:  ldloc.2
  IL_00c3:  stloc.1
  IL_00c4:  ldloc.0
  IL_00c5:  ldc.i4.s   -2
  IL_00c7:  bgt.s      IL_00cc
  IL_00c9:  ldc.i4.1
  IL_00ca:  br.s       IL_00cd
  IL_00cc:  ldloc.0
  IL_00cd:  switch    (
  IL_00da,
  IL_0086)
  IL_00da:  leave.s    IL_00fc
}
  filter
{
  IL_00dc:  isinst     "System.Exception"
  IL_00e1:  ldnull
  IL_00e2:  cgt.un
  IL_00e4:  ldloc.0
  IL_00e5:  ldc.i4.0
  IL_00e6:  cgt.un
  IL_00e8:  and
  IL_00e9:  ldloc.1
  IL_00ea:  ldc.i4.0
  IL_00eb:  ceq
  IL_00ed:  and
  IL_00ee:  endfilter
}  // end filter
{  // handler
  IL_00f0:  castclass  "System.Exception"
  IL_00f5:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00fa:  leave.s    IL_00c2
}
  IL_00fc:  ldc.i4     0x800a0033
  IL_0101:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0106:  throw
  IL_0107:  ldloc.1
  IL_0108:  ldc.i4.0
  IL_0109:  ceq
  IL_010b:  stloc.s    V_5
  IL_010d:  ldloc.s    V_5
  IL_010f:  brtrue.s   IL_0117
  IL_0111:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0116:  nop
  IL_0117:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_ForEach_Enumerable_2()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System
Imports System.Collections

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1}),
                      ({1, -1}),
                      ({1, 1, -1}),
                      ({1, 1, 1, -1, 1}),
                      ({1, 1, 1, -1, -1}),
                      ({1, 1, 1, 1, -1, 1}),
                      ({1, 1, 1, 1, 1, -1, 1}),
                      ({1, 1, 1, 0, -1})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        M("3")
        For Each o In GetEnumerable()
            M("0")
            M("1")
        Next
        M("2")
        Return
OnError:
        System.Console.WriteLine("OnError - {0}", Microsoft.VisualBasic.Information.Err.GetException().GetType())
        Resume Next
    End Sub

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException()
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
    End Class

    Function GetEnumerable() As IEnumerable
        M("GetEnumerable")
        Return New Enumerable()
    End Function

    Class Enumerable
        Implements IEnumerable

        Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            M("GetEnumerator")
            Return New Enumerator()
        End Function
    End Class

    Class Enumerator
        Implements IEnumerator, IDisposable

        Public ReadOnly Property Current As Object Implements IEnumerator.Current
            Get
                M("Current")
                Return Nothing
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            Return M("MoveNext") <> 0
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            M("Dispose")
        End Sub

    End Class
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      308 (0x134)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  System.Collections.IEnumerator V_3, //VB$ForEachEnumerator
  Object V_4, //o
  Boolean V_5)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  ldc.i4.1
  IL_0003:  stloc.2
  IL_0004:  ldstr      "3"
  IL_0009:  call       "Function Program.M(String) As Integer"
  IL_000e:  pop
  .try
{
  IL_000f:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_0014:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_0019:  stloc.3
  IL_001a:  br.s       IL_0040
  IL_001c:  ldloc.3
  IL_001d:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_0022:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_0027:  stloc.s    V_4
  IL_0029:  ldstr      "0"
  IL_002e:  call       "Function Program.M(String) As Integer"
  IL_0033:  pop
  IL_0034:  ldstr      "1"
  IL_0039:  call       "Function Program.M(String) As Integer"
  IL_003e:  pop
  IL_003f:  nop
  IL_0040:  ldloc.3
  IL_0041:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0046:  stloc.s    V_5
  IL_0048:  ldloc.s    V_5
  IL_004a:  brtrue.s   IL_001c
  IL_004c:  leave.s    IL_006a
}
  finally
{
  IL_004e:  ldloc.3
  IL_004f:  isinst     "System.IDisposable"
  IL_0054:  ldnull
  IL_0055:  ceq
  IL_0057:  stloc.s    V_5
  IL_0059:  ldloc.s    V_5
  IL_005b:  brtrue.s   IL_0069
  IL_005d:  ldloc.3
  IL_005e:  isinst     "System.IDisposable"
  IL_0063:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0068:  nop
  IL_0069:  endfinally
}
  IL_006a:  ldc.i4.2
  IL_006b:  stloc.2
  IL_006c:  ldstr      "2"
  IL_0071:  call       "Function Program.M(String) As Integer"
  IL_0076:  pop
  IL_0077:  br.s       IL_00b4
  IL_0079:  ldc.i4.4
  IL_007a:  stloc.2
  IL_007b:  ldstr      "OnError - {0}"
  IL_0080:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_0085:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_008a:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_008f:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_0094:  nop
  IL_0095:  ldc.i4.5
  IL_0096:  stloc.2
  IL_0097:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_009c:  nop
  IL_009d:  ldloc.1
  IL_009e:  ldc.i4.0
  IL_009f:  cgt.un
  IL_00a1:  stloc.s    V_5
  IL_00a3:  ldloc.s    V_5
  IL_00a5:  brtrue.s   IL_00b2
  IL_00a7:  ldc.i4     0x800a0014
  IL_00ac:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b1:  throw
  IL_00b2:  br.s       IL_00b6
  IL_00b4:  leave.s    IL_0123
  IL_00b6:  ldloc.1
  IL_00b7:  ldc.i4.1
  IL_00b8:  add
  IL_00b9:  ldc.i4.0
  IL_00ba:  stloc.1
  IL_00bb:  switch    (
  IL_00dc,
  IL_0002,
  IL_006a,
  IL_0077,
  IL_0079,
  IL_0095,
  IL_00b4)
  IL_00dc:  leave.s    IL_0118
  IL_00de:  ldloc.2
  IL_00df:  stloc.1
  IL_00e0:  ldloc.0
  IL_00e1:  ldc.i4.s   -2
  IL_00e3:  bgt.s      IL_00e8
  IL_00e5:  ldc.i4.1
  IL_00e6:  br.s       IL_00e9
  IL_00e8:  ldloc.0
  IL_00e9:  switch    (
  IL_00f6,
  IL_00b6)
  IL_00f6:  leave.s    IL_0118
}
  filter
{
  IL_00f8:  isinst     "System.Exception"
  IL_00fd:  ldnull
  IL_00fe:  cgt.un
  IL_0100:  ldloc.0
  IL_0101:  ldc.i4.0
  IL_0102:  cgt.un
  IL_0104:  and
  IL_0105:  ldloc.1
  IL_0106:  ldc.i4.0
  IL_0107:  ceq
  IL_0109:  and
  IL_010a:  endfilter
}  // end filter
{  // handler
  IL_010c:  castclass  "System.Exception"
  IL_0111:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0116:  leave.s    IL_00de
}
  IL_0118:  ldc.i4     0x800a0033
  IL_011d:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0122:  throw
  IL_0123:  ldloc.1
  IL_0124:  ldc.i4.0
  IL_0125:  ceq
  IL_0127:  stloc.s    V_5
  IL_0129:  ldloc.s    V_5
  IL_012b:  brtrue.s   IL_0133
  IL_012d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0132:  nop
  IL_0133:  ret
}]]>)

            ' Changed from verifying output to checking IL - Bug 717949
            ' Leaving in expected output for  information purpose

            '            Dim expected =
            '            <![CDATA[
            '--- Test - 0
            'M(3) - -1
            'Exception - Program+TestException
            '--- Test - 1
            'M(3) - 1
            'M(GetEnumerable) - -1
            'Exception - Program+TestException
            '--- Test - 2
            'M(3) - 1
            'M(GetEnumerable) - 1
            'M(GetEnumerator) - -1
            'Exception - Program+TestException
            '--- Test - 3
            'M(3) - 1
            'M(GetEnumerable) - 1
            'M(GetEnumerator) - 1
            'M(MoveNext) - -1
            'M(Dispose) - 1
            'Exception - Program+TestException
            '--- Test - 4
            'M(3) - 1
            'M(GetEnumerable) - 1
            'M(GetEnumerator) - 1
            'M(MoveNext) - -1
            'M(Dispose) - -1
            'Exception - Program+TestException
            '--- Test - 5
            'M(3) - 1
            'M(GetEnumerable) - 1
            'M(GetEnumerator) - 1
            'M(MoveNext) - 1
            'M(Current) - -1
            'M(Dispose) - 1
            'Exception - Program+TestException
            '--- Test - 6
            'M(3) - 1
            'M(GetEnumerable) - 1
            'M(GetEnumerator) - 1
            'M(MoveNext) - 1
            'M(Current) - 1
            'M(0) - -1
            'M(Dispose) - 1
            'Exception - Program+TestException
            '--- Test - 7
            'M(3) - 1
            'M(GetEnumerable) - 1
            'M(GetEnumerator) - 1
            'M(MoveNext) - 0
            'M(Dispose) - -1
            'Exception - Program+TestException
            ']]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      268 (0x10c)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                System.Collections.IEnumerator V_3)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldstr      "3"
  IL_0007:  call       "Function Program.M(String) As Integer"
  IL_000c:  pop
  .try
{
  IL_000d:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_0012:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_0017:  stloc.3
  IL_0018:  br.s       IL_003c
  IL_001a:  ldloc.3
  IL_001b:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_0020:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_0025:  pop
  IL_0026:  ldstr      "0"
  IL_002b:  call       "Function Program.M(String) As Integer"
  IL_0030:  pop
  IL_0031:  ldstr      "1"
  IL_0036:  call       "Function Program.M(String) As Integer"
  IL_003b:  pop
  IL_003c:  ldloc.3
  IL_003d:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0042:  brtrue.s   IL_001a
  IL_0044:  leave.s    IL_005a
}
  finally
{
  IL_0046:  ldloc.3
  IL_0047:  isinst     "System.IDisposable"
  IL_004c:  brfalse.s  IL_0059
  IL_004e:  ldloc.3
  IL_004f:  isinst     "System.IDisposable"
  IL_0054:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0059:  endfinally
}
  IL_005a:  ldc.i4.2
  IL_005b:  stloc.2
  IL_005c:  ldstr      "2"
  IL_0061:  call       "Function Program.M(String) As Integer"
  IL_0066:  pop
    IL_0067:  leave      IL_0103
  IL_006c:  ldc.i4.4
  IL_006d:  stloc.2
  IL_006e:  ldstr      "OnError - {0}"
  IL_0073:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_0078:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_007d:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_0082:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_0087:  ldc.i4.5
  IL_0088:  stloc.2
  IL_0089:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_008e:  ldloc.1
  IL_008f:  brtrue.s   IL_009e
  IL_0091:  ldc.i4     0x800a0014
  IL_0096:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_009b:  throw
    IL_009c:  leave.s    IL_0103
  IL_009e:  ldloc.1
  IL_009f:  ldc.i4.1
  IL_00a0:  add
  IL_00a1:  ldc.i4.0
  IL_00a2:  stloc.1
  IL_00a3:  switch    (
  IL_00c4,
  IL_0000,
  IL_005a,
  IL_009c,
  IL_006c,
  IL_0087,
  IL_009c)
    IL_00c4:  leave.s    IL_00f8
  IL_00c6:  ldloc.2
  IL_00c7:  stloc.1
  IL_00c8:  ldloc.0
    IL_00c9:  switch    (
        IL_00d6,
  IL_009e)
    IL_00d6:  leave.s    IL_00f8
}
  filter
{
    IL_00d8:  isinst     "System.Exception"
    IL_00dd:  ldnull
    IL_00de:  cgt.un
    IL_00e0:  ldloc.0
    IL_00e1:  ldc.i4.0
    IL_00e2:  cgt.un
    IL_00e4:  and
    IL_00e5:  ldloc.1
    IL_00e6:  ldc.i4.0
    IL_00e7:  ceq
    IL_00e9:  and
    IL_00ea:  endfilter
}  // end filter
{  // handler
    IL_00ec:  castclass  "System.Exception"
    IL_00f1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
    IL_00f6:  leave.s    IL_00c6
}
  IL_00f8:  ldc.i4     0x800a0033
  IL_00fd:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0102:  throw
  IL_0103:  ldloc.1
  IL_0104:  brfalse.s  IL_010b
  IL_0106:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_010b:  ret
}]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_ForEach_Enumerable_3()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        Resume Next
        M0()
        For Each o In GetEnumerable()
            M1()
        Next
        M2()
    End Sub

    Function GetEnumerable() As System.Collections.IEnumerable
        Return Nothing
    End Function

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Sub M2()
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      199 (0xc7)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                System.Collections.IEnumerator V_3)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  ldloc.1
  IL_0008:  brtrue.s   IL_0061
  IL_000a:  ldc.i4     0x800a0014
  IL_000f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0014:  throw
  IL_0015:  ldc.i4.2
  IL_0016:  stloc.2
  IL_0017:  call       "Sub Program.M0()"
  .try
{
  IL_001c:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_0021:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_0026:  stloc.3
  IL_0027:  br.s       IL_003a
  IL_0029:  ldloc.3
  IL_002a:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_002f:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_0034:  pop
  IL_0035:  call       "Sub Program.M1()"
  IL_003a:  ldloc.3
  IL_003b:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0040:  brtrue.s   IL_0029
  IL_0042:  leave.s    IL_0058
}
  finally
{
  IL_0044:  ldloc.3
  IL_0045:  isinst     "System.IDisposable"
  IL_004a:  brfalse.s  IL_0057
  IL_004c:  ldloc.3
  IL_004d:  isinst     "System.IDisposable"
  IL_0052:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0057:  endfinally
}
  IL_0058:  ldc.i4.3
  IL_0059:  stloc.2
  IL_005a:  call       "Sub Program.M2()"
  IL_005f:  leave.s    IL_00be
  IL_0061:  ldloc.1
  IL_0062:  ldc.i4.1
  IL_0063:  add
  IL_0064:  ldc.i4.0
  IL_0065:  stloc.1
  IL_0066:  switch    (
  IL_007f,
  IL_0000,
  IL_0015,
  IL_0058,
  IL_005f)
  IL_007f:  leave.s    IL_00b3
  IL_0081:  ldloc.2
  IL_0082:  stloc.1
  IL_0083:  ldloc.0
  IL_0084:  switch    (
  IL_0091,
  IL_0061)
  IL_0091:  leave.s    IL_00b3
}
  filter
{
  IL_0093:  isinst     "System.Exception"
  IL_0098:  ldnull
  IL_0099:  cgt.un
  IL_009b:  ldloc.0
  IL_009c:  ldc.i4.0
  IL_009d:  cgt.un
  IL_009f:  and
  IL_00a0:  ldloc.1
  IL_00a1:  ldc.i4.0
  IL_00a2:  ceq
  IL_00a4:  and
  IL_00a5:  endfilter
}  // end filter
{  // handler
  IL_00a7:  castclass  "System.Exception"
  IL_00ac:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b1:  leave.s    IL_0081
}
  IL_00b3:  ldc.i4     0x800a0033
  IL_00b8:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00bd:  throw
  IL_00be:  ldloc.1
  IL_00bf:  brfalse.s  IL_00c6
  IL_00c1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c6:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      241 (0xf1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3,
  System.Collections.IEnumerator V_4, //VB$ForEachEnumerator
  Object V_5) //o
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  ldc.i4.1
  IL_0003:  stloc.2
  IL_0004:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0009:  nop
  IL_000a:  ldloc.1
  IL_000b:  ldc.i4.0
  IL_000c:  cgt.un
  IL_000e:  stloc.3
  IL_000f:  ldloc.3
  IL_0010:  brtrue.s   IL_001d
  IL_0012:  ldc.i4     0x800a0014
  IL_0017:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_001c:  throw
  IL_001d:  br.s       IL_007d
  IL_001f:  ldc.i4.2
  IL_0020:  stloc.2
  IL_0021:  call       "Sub Program.M0()"
  IL_0026:  nop
  .try
{
  IL_0027:  call       "Function Program.GetEnumerable() As System.Collections.IEnumerable"
  IL_002c:  callvirt   "Function System.Collections.IEnumerable.GetEnumerator() As System.Collections.IEnumerator"
  IL_0031:  stloc.s    V_4
  IL_0033:  br.s       IL_004a
  IL_0035:  ldloc.s    V_4
  IL_0037:  callvirt   "Function System.Collections.IEnumerator.get_Current() As Object"
  IL_003c:  call       "Function System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(Object) As Object"
  IL_0041:  stloc.s    V_5
  IL_0043:  call       "Sub Program.M1()"
  IL_0048:  nop
  IL_0049:  nop
  IL_004a:  ldloc.s    V_4
  IL_004c:  callvirt   "Function System.Collections.IEnumerator.MoveNext() As Boolean"
  IL_0051:  stloc.3
  IL_0052:  ldloc.3
  IL_0053:  brtrue.s   IL_0035
  IL_0055:  leave.s    IL_0073
}
  finally
{
  IL_0057:  ldloc.s    V_4
  IL_0059:  isinst     "System.IDisposable"
  IL_005e:  ldnull
  IL_005f:  ceq
  IL_0061:  stloc.3
  IL_0062:  ldloc.3
  IL_0063:  brtrue.s   IL_0072
  IL_0065:  ldloc.s    V_4
  IL_0067:  isinst     "System.IDisposable"
  IL_006c:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_0071:  nop
  IL_0072:  endfinally
}
  IL_0073:  ldc.i4.3
  IL_0074:  stloc.2
  IL_0075:  call       "Sub Program.M2()"
  IL_007a:  nop
  IL_007b:  leave.s    IL_00e2
  IL_007d:  ldloc.1
  IL_007e:  ldc.i4.1
  IL_007f:  add
  IL_0080:  ldc.i4.0
  IL_0081:  stloc.1
  IL_0082:  switch    (
  IL_009b,
  IL_0002,
  IL_001f,
  IL_0073,
  IL_007b)
  IL_009b:  leave.s    IL_00d7
  IL_009d:  ldloc.2
  IL_009e:  stloc.1
  IL_009f:  ldloc.0
  IL_00a0:  ldc.i4.s   -2
  IL_00a2:  bgt.s      IL_00a7
  IL_00a4:  ldc.i4.1
  IL_00a5:  br.s       IL_00a8
  IL_00a7:  ldloc.0
  IL_00a8:  switch    (
  IL_00b5,
  IL_007d)
  IL_00b5:  leave.s    IL_00d7
}
  filter
{
  IL_00b7:  isinst     "System.Exception"
  IL_00bc:  ldnull
  IL_00bd:  cgt.un
  IL_00bf:  ldloc.0
  IL_00c0:  ldc.i4.0
  IL_00c1:  cgt.un
  IL_00c3:  and
  IL_00c4:  ldloc.1
  IL_00c5:  ldc.i4.0
  IL_00c6:  ceq
  IL_00c8:  and
  IL_00c9:  endfilter
}  // end filter
{  // handler
  IL_00cb:  castclass  "System.Exception"
  IL_00d0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00d5:  leave.s    IL_009d
}
  IL_00d7:  ldc.i4     0x800a0033
  IL_00dc:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e1:  throw
  IL_00e2:  ldloc.1
  IL_00e3:  ldc.i4.0
  IL_00e4:  ceq
  IL_00e6:  stloc.3
  IL_00e7:  ldloc.3
  IL_00e8:  brtrue.s   IL_00f0
  IL_00ea:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ef:  nop
  IL_00f0:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_ForTo_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, -1, -1, 0}),
                      ({0, 0, -1, -1, -1, -1, 0}),
                      ({0, 0, 0, -1, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, -1, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, 0, -1, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, -1, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        M("0")
        For i As LoopType = 0 To 1
            M("1")
            M("2")
        Next
        M("3")
        Return
OnError:
        System.Console.WriteLine("OnError - {0}", Microsoft.VisualBasic.Information.Err.GetException().GetType())
        Resume Next
    End Sub

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException()
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
    End Class

    Structure LoopType
        Private m_Val As Integer

        Public Sub New(x As Integer)
            m_Val = x
        End Sub

        Public Shared Widening Operator CType(x As Integer) As LoopType
            M("Operator CType")
            Return New LoopType(x)
        End Operator

        Public Shared Operator -(x As LoopType, y As LoopType) As LoopType
            M("Operator '-'")
            Return New LoopType(x.m_Val - y.m_Val)
        End Operator

        Public Shared Operator +(x As LoopType, y As LoopType) As LoopType
            M("Operator '+'")
            Return New LoopType(x.m_Val + y.m_Val)
        End Operator

        Public Shared Operator >=(x As LoopType, y As LoopType) As Boolean
            M("Operator '>='")
            Return x.m_Val >= y.m_Val
        End Operator

        Public Shared Operator <=(x As LoopType, y As LoopType) As Boolean
            M("Operator '<='")
            Return x.m_Val <= y.m_Val
        End Operator
    End Structure

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      374 (0x176)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Program.LoopType V_3, //VB$LoopObject
  Program.LoopType V_4, //VB$ForLimit
  Program.LoopType V_5, //VB$ForStep
  Boolean V_6, //VB$LoopDirection
  Program.LoopType V_7, //i
  Program.LoopType V_8,
  Boolean V_9)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  ldc.i4.2
  IL_000b:  stloc.2
  IL_000c:  ldstr      "0"
  IL_0011:  call       "Function Program.M(String) As Integer"
  IL_0016:  pop
  IL_0017:  ldc.i4.3
  IL_0018:  stloc.2
  IL_0019:  ldc.i4.0
  IL_001a:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_001f:  stloc.s    V_8
  IL_0021:  ldc.i4.1
  IL_0022:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_0027:  stloc.s    V_4
  IL_0029:  ldc.i4.1
  IL_002a:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_002f:  stloc.s    V_5
  IL_0031:  ldloc.s    V_5
  IL_0033:  ldloc.s    V_5
  IL_0035:  ldloc.s    V_5
  IL_0037:  call       "Function Program.LoopType.op_Subtraction(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_003c:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_0041:  stloc.s    V_6
  IL_0043:  ldloc.s    V_8
  IL_0045:  stloc.s    V_7
  IL_0047:  br.s       IL_0070
  IL_0049:  ldc.i4.4
  IL_004a:  stloc.2
  IL_004b:  ldstr      "1"
  IL_0050:  call       "Function Program.M(String) As Integer"
  IL_0055:  pop
  IL_0056:  ldc.i4.5
  IL_0057:  stloc.2
  IL_0058:  ldstr      "2"
  IL_005d:  call       "Function Program.M(String) As Integer"
  IL_0062:  pop
  IL_0063:  ldc.i4.6
  IL_0064:  stloc.2
  IL_0065:  ldloc.s    V_7
  IL_0067:  ldloc.s    V_5
  IL_0069:  call       "Function Program.LoopType.op_Addition(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_006e:  stloc.s    V_7
  IL_0070:  ldloc.s    V_6
  IL_0072:  brtrue.s   IL_007f
  IL_0074:  ldloc.s    V_7
  IL_0076:  ldloc.s    V_4
  IL_0078:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_007d:  br.s       IL_0088
  IL_007f:  ldloc.s    V_7
  IL_0081:  ldloc.s    V_4
  IL_0083:  call       "Function Program.LoopType.op_LessThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_0088:  stloc.s    V_9
  IL_008a:  ldloc.s    V_9
  IL_008c:  brtrue.s   IL_0049
  IL_008e:  ldc.i4.7
  IL_008f:  stloc.2
  IL_0090:  ldstr      "3"
  IL_0095:  call       "Function Program.M(String) As Integer"
  IL_009a:  pop
  IL_009b:  br.s       IL_00db
  IL_009d:  nop
  IL_009e:  ldc.i4.s   9
  IL_00a0:  stloc.2
  IL_00a1:  ldstr      "OnError - {0}"
  IL_00a6:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_00ab:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_00b0:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_00b5:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_00ba:  nop
  IL_00bb:  ldc.i4.s   10
  IL_00bd:  stloc.2
  IL_00be:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c3:  nop
  IL_00c4:  ldloc.1
  IL_00c5:  ldc.i4.0
  IL_00c6:  cgt.un
  IL_00c8:  stloc.s    V_9
  IL_00ca:  ldloc.s    V_9
  IL_00cc:  brtrue.s   IL_00d9
  IL_00ce:  ldc.i4     0x800a0014
  IL_00d3:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d8:  throw
  IL_00d9:  br.s       IL_00e0
  IL_00db:  leave      IL_0165
  IL_00e0:  ldloc.1
  IL_00e1:  ldc.i4.1
  IL_00e2:  add
  IL_00e3:  ldc.i4.0
  IL_00e4:  stloc.1
  IL_00e5:  switch    (
  IL_011a,
  IL_0002,
  IL_000a,
  IL_0017,
  IL_0049,
  IL_0056,
  IL_0063,
  IL_008e,
  IL_009b,
  IL_009e,
  IL_00bb,
  IL_00db)
  IL_011a:  leave.s    IL_015a
  IL_011c:  ldloc.2
  IL_011d:  stloc.1
  IL_011e:  ldloc.0
  IL_011f:  ldc.i4.s   -2
  IL_0121:  bgt.s      IL_0126
  IL_0123:  ldc.i4.1
  IL_0124:  br.s       IL_0127
  IL_0126:  ldloc.0
  IL_0127:  switch    (
  IL_0138,
  IL_00e0,
  IL_009d)
  IL_0138:  leave.s    IL_015a
}
  filter
{
  IL_013a:  isinst     "System.Exception"
  IL_013f:  ldnull
  IL_0140:  cgt.un
  IL_0142:  ldloc.0
  IL_0143:  ldc.i4.0
  IL_0144:  cgt.un
  IL_0146:  and
  IL_0147:  ldloc.1
  IL_0148:  ldc.i4.0
  IL_0149:  ceq
  IL_014b:  and
  IL_014c:  endfilter
}  // end filter
{  // handler
  IL_014e:  castclass  "System.Exception"
  IL_0153:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0158:  leave.s    IL_011c
}
  IL_015a:  ldc.i4     0x800a0033
  IL_015f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0164:  throw
  IL_0165:  ldloc.1
  IL_0166:  ldc.i4.0
  IL_0167:  ceq
  IL_0169:  stloc.s    V_9
  IL_016b:  ldloc.s    V_9
  IL_016d:  brtrue.s   IL_0175
  IL_016f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0174:  nop
  IL_0175:  ret
}]]>)

            ' Changed from verifying output to checking IL - Bug 717949
            ' Leaving in expected output for  information purpose

            '            Dim expected =
            '            <![CDATA[
            '--- Test - 0
            'M(0) - -1
            'OnError - Program+TestException
            'M(Operator CType) - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 1
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 2
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 3
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 4
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 5
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 6
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 7
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 8
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 9
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(3) - 0
            ']]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)
            compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      374 (0x176)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Program.LoopType V_3, //VB$LoopObject
  Program.LoopType V_4, //VB$ForLimit
  Program.LoopType V_5, //VB$ForStep
  Boolean V_6, //VB$LoopDirection
  Program.LoopType V_7, //i
  Program.LoopType V_8,
  Boolean V_9)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  ldc.i4.2
  IL_000b:  stloc.2
  IL_000c:  ldstr      "0"
  IL_0011:  call       "Function Program.M(String) As Integer"
  IL_0016:  pop
  IL_0017:  ldc.i4.3
  IL_0018:  stloc.2
  IL_0019:  ldc.i4.0
  IL_001a:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_001f:  stloc.s    V_8
  IL_0021:  ldc.i4.1
  IL_0022:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_0027:  stloc.s    V_4
  IL_0029:  ldc.i4.1
  IL_002a:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_002f:  stloc.s    V_5
  IL_0031:  ldloc.s    V_5
  IL_0033:  ldloc.s    V_5
  IL_0035:  ldloc.s    V_5
  IL_0037:  call       "Function Program.LoopType.op_Subtraction(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_003c:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_0041:  stloc.s    V_6
  IL_0043:  ldloc.s    V_8
  IL_0045:  stloc.s    V_7
  IL_0047:  br.s       IL_0070
  IL_0049:  ldc.i4.4
  IL_004a:  stloc.2
  IL_004b:  ldstr      "1"
  IL_0050:  call       "Function Program.M(String) As Integer"
  IL_0055:  pop
  IL_0056:  ldc.i4.5
  IL_0057:  stloc.2
  IL_0058:  ldstr      "2"
  IL_005d:  call       "Function Program.M(String) As Integer"
  IL_0062:  pop
  IL_0063:  ldc.i4.6
  IL_0064:  stloc.2
  IL_0065:  ldloc.s    V_7
  IL_0067:  ldloc.s    V_5
  IL_0069:  call       "Function Program.LoopType.op_Addition(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_006e:  stloc.s    V_7
  IL_0070:  ldloc.s    V_6
  IL_0072:  brtrue.s   IL_007f
  IL_0074:  ldloc.s    V_7
  IL_0076:  ldloc.s    V_4
  IL_0078:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_007d:  br.s       IL_0088
  IL_007f:  ldloc.s    V_7
  IL_0081:  ldloc.s    V_4
  IL_0083:  call       "Function Program.LoopType.op_LessThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_0088:  stloc.s    V_9
  IL_008a:  ldloc.s    V_9
  IL_008c:  brtrue.s   IL_0049
  IL_008e:  ldc.i4.7
  IL_008f:  stloc.2
  IL_0090:  ldstr      "3"
  IL_0095:  call       "Function Program.M(String) As Integer"
  IL_009a:  pop
  IL_009b:  br.s       IL_00db
  IL_009d:  nop
  IL_009e:  ldc.i4.s   9
  IL_00a0:  stloc.2
  IL_00a1:  ldstr      "OnError - {0}"
  IL_00a6:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_00ab:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_00b0:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_00b5:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_00ba:  nop
  IL_00bb:  ldc.i4.s   10
  IL_00bd:  stloc.2
  IL_00be:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c3:  nop
  IL_00c4:  ldloc.1
  IL_00c5:  ldc.i4.0
  IL_00c6:  cgt.un
  IL_00c8:  stloc.s    V_9
  IL_00ca:  ldloc.s    V_9
  IL_00cc:  brtrue.s   IL_00d9
  IL_00ce:  ldc.i4     0x800a0014
  IL_00d3:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d8:  throw
  IL_00d9:  br.s       IL_00e0
  IL_00db:  leave      IL_0165
  IL_00e0:  ldloc.1
  IL_00e1:  ldc.i4.1
  IL_00e2:  add
  IL_00e3:  ldc.i4.0
  IL_00e4:  stloc.1
  IL_00e5:  switch    (
  IL_011a,
  IL_0002,
  IL_000a,
  IL_0017,
  IL_0049,
  IL_0056,
  IL_0063,
  IL_008e,
  IL_009b,
  IL_009e,
  IL_00bb,
  IL_00db)
  IL_011a:  leave.s    IL_015a
  IL_011c:  ldloc.2
  IL_011d:  stloc.1
  IL_011e:  ldloc.0
  IL_011f:  ldc.i4.s   -2
  IL_0121:  bgt.s      IL_0126
  IL_0123:  ldc.i4.1
  IL_0124:  br.s       IL_0127
  IL_0126:  ldloc.0
  IL_0127:  switch    (
  IL_0138,
  IL_00e0,
  IL_009d)
  IL_0138:  leave.s    IL_015a
}
  filter
{
  IL_013a:  isinst     "System.Exception"
  IL_013f:  ldnull
  IL_0140:  cgt.un
  IL_0142:  ldloc.0
  IL_0143:  ldc.i4.0
  IL_0144:  cgt.un
  IL_0146:  and
  IL_0147:  ldloc.1
  IL_0148:  ldc.i4.0
  IL_0149:  ceq
  IL_014b:  and
  IL_014c:  endfilter
}  // end filter
{  // handler
  IL_014e:  castclass  "System.Exception"
  IL_0153:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0158:  leave.s    IL_011c
}
  IL_015a:  ldc.i4     0x800a0033
  IL_015f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0164:  throw
  IL_0165:  ldloc.1
  IL_0166:  ldc.i4.0
  IL_0167:  ceq
  IL_0169:  stloc.s    V_9
  IL_016b:  ldloc.s    V_9
  IL_016d:  brtrue.s   IL_0175
  IL_016f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0174:  nop
  IL_0175:  ret
}]]>)

        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_ForTo_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        For i As LoopType = 0 To 1
            If M4()
                Continue For
            End If
            M1()
ContinueLabel:
        Next
        M2()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Sub M2()
    End Sub

    Function M4() As Boolean
        Return False
    End Function

    Structure LoopType
        Private m_Val As Integer

        Public Sub New(x As Integer)
            m_Val = x
        End Sub

        Public Shared Widening Operator CType(x As Integer) As LoopType
            Return New LoopType(x)
        End Operator

        Public Shared Operator -(x As LoopType, y As LoopType) As LoopType
            Return New LoopType(x.m_Val - y.m_Val)
        End Operator

        Public Shared Operator +(x As LoopType, y As LoopType) As LoopType
            Return New LoopType(x.m_Val + y.m_Val)
        End Operator

        Public Shared Operator >=(x As LoopType, y As LoopType) As Boolean
            Return x.m_Val >= y.m_Val
        End Operator

        Public Shared Operator <=(x As LoopType, y As LoopType) As Boolean
            Return x.m_Val <= y.m_Val
        End Operator
    End Structure

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      242 (0xf2)
  .maxstack  4
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Program.LoopType V_3, //VB$ForLimit
  Program.LoopType V_4, //VB$ForStep
  Boolean V_5, //VB$LoopDirection
  Program.LoopType V_6) //i
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  ldc.i4.0
  IL_0011:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_0016:  ldc.i4.1
  IL_0017:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_001c:  stloc.3
  IL_001d:  ldc.i4.1
  IL_001e:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_0023:  stloc.s    V_4
  IL_0025:  ldloc.s    V_4
  IL_0027:  ldloc.s    V_4
  IL_0029:  dup
  IL_002a:  call       "Function Program.LoopType.op_Subtraction(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_002f:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_0034:  stloc.s    V_5
  IL_0036:  stloc.s    V_6
  IL_0038:  br.s       IL_0057
  IL_003a:  ldc.i4.4
  IL_003b:  stloc.2
  IL_003c:  call       "Function Program.M4() As Boolean"
  IL_0041:  brtrue.s   IL_004a
  IL_0043:  ldc.i4.6
  IL_0044:  stloc.2
  IL_0045:  call       "Sub Program.M1()"
  IL_004a:  ldc.i4.7
  IL_004b:  stloc.2
  IL_004c:  ldloc.s    V_6
  IL_004e:  ldloc.s    V_4
  IL_0050:  call       "Function Program.LoopType.op_Addition(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_0055:  stloc.s    V_6
  IL_0057:  ldloc.s    V_5
  IL_0059:  brtrue.s   IL_0065
  IL_005b:  ldloc.s    V_6
  IL_005d:  ldloc.3
  IL_005e:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_0063:  br.s       IL_006d
  IL_0065:  ldloc.s    V_6
  IL_0067:  ldloc.3
  IL_0068:  call       "Function Program.LoopType.op_LessThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_006d:  brtrue.s   IL_003a
  IL_006f:  ldc.i4.8
  IL_0070:  stloc.2
  IL_0071:  call       "Sub Program.M2()"
  IL_0076:  leave.s    IL_00e9
  IL_0078:  ldloc.1
  IL_0079:  ldc.i4.1
  IL_007a:  add
  IL_007b:  ldc.i4.0
  IL_007c:  stloc.1
  IL_007d:  switch    (
  IL_00aa,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_003a,
  IL_004a,
  IL_0043,
  IL_004a,
  IL_006f,
  IL_0076)
  IL_00aa:  leave.s    IL_00de
  IL_00ac:  ldloc.2
  IL_00ad:  stloc.1
  IL_00ae:  ldloc.0
  IL_00af:  switch    (
  IL_00bc,
  IL_0078)
  IL_00bc:  leave.s    IL_00de
}
  filter
{
  IL_00be:  isinst     "System.Exception"
  IL_00c3:  ldnull
  IL_00c4:  cgt.un
  IL_00c6:  ldloc.0
  IL_00c7:  ldc.i4.0
  IL_00c8:  cgt.un
  IL_00ca:  and
  IL_00cb:  ldloc.1
  IL_00cc:  ldc.i4.0
  IL_00cd:  ceq
  IL_00cf:  and
  IL_00d0:  endfilter
}  // end filter
{  // handler
  IL_00d2:  castclass  "System.Exception"
  IL_00d7:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00dc:  leave.s    IL_00ac
}
  IL_00de:  ldc.i4     0x800a0033
  IL_00e3:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e8:  throw
  IL_00e9:  ldloc.1
  IL_00ea:  brfalse.s  IL_00f1
  IL_00ec:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00f1:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      294 (0x126)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Program.LoopType V_3, //VB$LoopObject
  Program.LoopType V_4, //VB$ForLimit
  Program.LoopType V_5, //VB$ForStep
  Boolean V_6, //VB$LoopDirection
  Program.LoopType V_7, //i
  Program.LoopType V_8,
  Boolean V_9)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  ldc.i4.0
  IL_0016:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_001b:  stloc.s    V_8
  IL_001d:  ldc.i4.1
  IL_001e:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_0023:  stloc.s    V_4
  IL_0025:  ldc.i4.1
  IL_0026:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_002b:  stloc.s    V_5
  IL_002d:  ldloc.s    V_5
  IL_002f:  ldloc.s    V_5
  IL_0031:  ldloc.s    V_5
  IL_0033:  call       "Function Program.LoopType.op_Subtraction(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_0038:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_003d:  stloc.s    V_6
  IL_003f:  ldloc.s    V_8
  IL_0041:  stloc.s    V_7
  IL_0043:  br.s       IL_006f
  IL_0045:  ldc.i4.4
  IL_0046:  stloc.2
  IL_0047:  call       "Function Program.M4() As Boolean"
  IL_004c:  ldc.i4.0
  IL_004d:  ceq
  IL_004f:  stloc.s    V_9
  IL_0051:  ldloc.s    V_9
  IL_0053:  brtrue.s   IL_0058
  IL_0055:  br.s       IL_0062
  IL_0057:  nop
  IL_0058:  nop
  IL_0059:  ldc.i4.7
  IL_005a:  stloc.2
  IL_005b:  call       "Sub Program.M1()"
  IL_0060:  nop
  IL_0061:  nop
  IL_0062:  ldc.i4.8
  IL_0063:  stloc.2
  IL_0064:  ldloc.s    V_7
  IL_0066:  ldloc.s    V_5
  IL_0068:  call       "Function Program.LoopType.op_Addition(Program.LoopType, Program.LoopType) As Program.LoopType"
  IL_006d:  stloc.s    V_7
  IL_006f:  ldloc.s    V_6
  IL_0071:  brtrue.s   IL_007e
  IL_0073:  ldloc.s    V_7
  IL_0075:  ldloc.s    V_4
  IL_0077:  call       "Function Program.LoopType.op_GreaterThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_007c:  br.s       IL_0087
  IL_007e:  ldloc.s    V_7
  IL_0080:  ldloc.s    V_4
  IL_0082:  call       "Function Program.LoopType.op_LessThanOrEqual(Program.LoopType, Program.LoopType) As Boolean"
  IL_0087:  stloc.s    V_9
  IL_0089:  ldloc.s    V_9
  IL_008b:  brtrue.s   IL_0045
  IL_008d:  ldc.i4.s   9
  IL_008f:  stloc.2
  IL_0090:  call       "Sub Program.M2()"
  IL_0095:  nop
  IL_0096:  leave.s    IL_0115
  IL_0098:  ldloc.1
  IL_0099:  ldc.i4.1
  IL_009a:  add
  IL_009b:  ldc.i4.0
  IL_009c:  stloc.1
  IL_009d:  switch    (
  IL_00ce,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0045,
  IL_0055,
  IL_0057,
  IL_0059,
  IL_0062,
  IL_008d,
  IL_0096)
  IL_00ce:  leave.s    IL_010a
  IL_00d0:  ldloc.2
  IL_00d1:  stloc.1
  IL_00d2:  ldloc.0
  IL_00d3:  ldc.i4.s   -2
  IL_00d5:  bgt.s      IL_00da
  IL_00d7:  ldc.i4.1
  IL_00d8:  br.s       IL_00db
  IL_00da:  ldloc.0
  IL_00db:  switch    (
  IL_00e8,
  IL_0098)
  IL_00e8:  leave.s    IL_010a
}
  filter
{
  IL_00ea:  isinst     "System.Exception"
  IL_00ef:  ldnull
  IL_00f0:  cgt.un
  IL_00f2:  ldloc.0
  IL_00f3:  ldc.i4.0
  IL_00f4:  cgt.un
  IL_00f6:  and
  IL_00f7:  ldloc.1
  IL_00f8:  ldc.i4.0
  IL_00f9:  ceq
  IL_00fb:  and
  IL_00fc:  endfilter
}  // end filter
{  // handler
  IL_00fe:  castclass  "System.Exception"
  IL_0103:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0108:  leave.s    IL_00d0
}
  IL_010a:  ldc.i4     0x800a0033
  IL_010f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0114:  throw
  IL_0115:  ldloc.1
  IL_0116:  ldc.i4.0
  IL_0117:  ceq
  IL_0119:  stloc.s    V_9
  IL_011b:  ldloc.s    V_9
  IL_011d:  brtrue.s   IL_0125
  IL_011f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0124:  nop
  IL_0125:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_ForTo_2()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, -1, 0}),
                      ({0, -1, -1, -1, 0}),
                      ({0, 0, -1, -1, -1, 0}),
                      ({0, 0, 0, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, 0, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        M("0")
        For i As Object = CType(0, LoopType) To 1
            M("1")
            M("2")
        Next
        M("3")
        Return
OnError:
        System.Console.WriteLine("OnError - {0}", Microsoft.VisualBasic.Information.Err.GetException().GetType())
        Resume Next
    End Sub

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException()
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
    End Class

    Structure LoopType
        Private m_Val As Integer

        Public Sub New(x As Integer)
            m_Val = x
        End Sub

        Public Shared Widening Operator CType(x As Integer) As LoopType
            M("Operator CType")
            Return New LoopType(x)
        End Operator

        Public Shared Operator -(x As LoopType, y As LoopType) As LoopType
            M("Operator '-'")
            Return New LoopType(x.m_Val - y.m_Val)
        End Operator

        Public Shared Operator +(x As LoopType, y As LoopType) As LoopType
            M("Operator '+'")
            Return New LoopType(x.m_Val + y.m_Val)
        End Operator

        Public Shared Operator >=(x As LoopType, y As LoopType) As Boolean
            M("Operator '>='")
            Return x.m_Val >= y.m_Val
        End Operator

        Public Shared Operator <=(x As LoopType, y As LoopType) As Boolean
            M("Operator '<='")
            Return x.m_Val <= y.m_Val
        End Operator
    End Structure

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      344 (0x158)
  .maxstack  6
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Object V_3, //VB$LoopObject
  Object V_4, //i
  Boolean V_5)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  ldc.i4.2
  IL_000b:  stloc.2
  IL_000c:  ldstr      "0"
  IL_0011:  call       "Function Program.M(String) As Integer"
  IL_0016:  pop
  IL_0017:  ldc.i4.3
  IL_0018:  stloc.2
  IL_0019:  ldloc.s    V_4
  IL_001b:  ldc.i4.0
  IL_001c:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_0021:  box        "Program.LoopType"
  IL_0026:  ldc.i4.1
  IL_0027:  box        "Integer"
  IL_002c:  ldc.i4.1
  IL_002d:  box        "Integer"
  IL_0032:  ldloca.s   V_3
  IL_0034:  ldloca.s   V_4
  IL_0036:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForLoopInitObj(Object, Object, Object, Object, ByRef Object, ByRef Object) As Boolean"
  IL_003b:  ldc.i4.0
  IL_003c:  ceq
  IL_003e:  stloc.s    V_5
  IL_0040:  ldloc.s    V_5
  IL_0042:  brtrue.s   IL_0070
  IL_0044:  ldc.i4.4
  IL_0045:  stloc.2
  IL_0046:  ldstr      "1"
  IL_004b:  call       "Function Program.M(String) As Integer"
  IL_0050:  pop
  IL_0051:  ldc.i4.5
  IL_0052:  stloc.2
  IL_0053:  ldstr      "2"
  IL_0058:  call       "Function Program.M(String) As Integer"
  IL_005d:  pop
  IL_005e:  ldc.i4.6
  IL_005f:  stloc.2
  IL_0060:  ldloc.s    V_4
  IL_0062:  ldloc.3
  IL_0063:  ldloca.s   V_4
  IL_0065:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForNextCheckObj(Object, Object, ByRef Object) As Boolean"
  IL_006a:  stloc.s    V_5
  IL_006c:  ldloc.s    V_5
  IL_006e:  brtrue.s   IL_0044
  IL_0070:  ldc.i4.7
  IL_0071:  stloc.2
  IL_0072:  ldstr      "3"
  IL_0077:  call       "Function Program.M(String) As Integer"
  IL_007c:  pop
  IL_007d:  br.s       IL_00bd
  IL_007f:  nop
  IL_0080:  ldc.i4.s   9
  IL_0082:  stloc.2
  IL_0083:  ldstr      "OnError - {0}"
  IL_0088:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_008d:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_0092:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_0097:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_009c:  nop
  IL_009d:  ldc.i4.s   10
  IL_009f:  stloc.2
  IL_00a0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a5:  nop
  IL_00a6:  ldloc.1
  IL_00a7:  ldc.i4.0
  IL_00a8:  cgt.un
  IL_00aa:  stloc.s    V_5
  IL_00ac:  ldloc.s    V_5
  IL_00ae:  brtrue.s   IL_00bb
  IL_00b0:  ldc.i4     0x800a0014
  IL_00b5:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ba:  throw
  IL_00bb:  br.s       IL_00c2
  IL_00bd:  leave      IL_0147
  IL_00c2:  ldloc.1
  IL_00c3:  ldc.i4.1
  IL_00c4:  add
  IL_00c5:  ldc.i4.0
  IL_00c6:  stloc.1
  IL_00c7:  switch    (
  IL_00fc,
  IL_0002,
  IL_000a,
  IL_0017,
  IL_0044,
  IL_0051,
  IL_005e,
  IL_0070,
  IL_007d,
  IL_0080,
  IL_009d,
  IL_00bd)
  IL_00fc:  leave.s    IL_013c
  IL_00fe:  ldloc.2
  IL_00ff:  stloc.1
  IL_0100:  ldloc.0
  IL_0101:  ldc.i4.s   -2
  IL_0103:  bgt.s      IL_0108
  IL_0105:  ldc.i4.1
  IL_0106:  br.s       IL_0109
  IL_0108:  ldloc.0
  IL_0109:  switch    (
  IL_011a,
  IL_00c2,
  IL_007f)
  IL_011a:  leave.s    IL_013c
}
  filter
{
  IL_011c:  isinst     "System.Exception"
  IL_0121:  ldnull
  IL_0122:  cgt.un
  IL_0124:  ldloc.0
  IL_0125:  ldc.i4.0
  IL_0126:  cgt.un
  IL_0128:  and
  IL_0129:  ldloc.1
  IL_012a:  ldc.i4.0
  IL_012b:  ceq
  IL_012d:  and
  IL_012e:  endfilter
}  // end filter
{  // handler
  IL_0130:  castclass  "System.Exception"
  IL_0135:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_013a:  leave.s    IL_00fe
}
  IL_013c:  ldc.i4     0x800a0033
  IL_0141:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0146:  throw
  IL_0147:  ldloc.1
  IL_0148:  ldc.i4.0
  IL_0149:  ceq
  IL_014b:  stloc.s    V_5
  IL_014d:  ldloc.s    V_5
  IL_014f:  brtrue.s   IL_0157
  IL_0151:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0156:  nop
  IL_0157:  ret
}]]>)

            ' Changed from verifying output to checking IL - Bug 717949
            ' Leaving in expected output for  information purpose

            '            Dim expected =
            '            <![CDATA[
            '--- Test - 0
            'M(0) - -1
            'OnError - Program+TestException
            'M(Operator CType) - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'OnError - System.Exception
            'M(3) - 0
            '--- Test - 1
            'M(0) - 0
            'M(Operator CType) - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'OnError - System.Exception
            'M(3) - 0
            '--- Test - 2
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - -1
            'OnError - System.ArgumentException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'OnError - System.Exception
            'M(3) - 0
            '--- Test - 3
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - -1
            'OnError - System.ArgumentException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'OnError - System.Exception
            'M(3) - 0
            '--- Test - 4
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'OnError - System.Exception
            'M(3) - 0
            '--- Test - 5
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'OnError - System.Exception
            'M(3) - 0
            '--- Test - 6
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - -1
            'OnError - Program+TestException
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(3) - 0
            '--- Test - 7
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - -1
            'OnError - Program+TestException
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(3) - 0
            '--- Test - 8
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 9
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - -1
            'OnError - Program+TestException
            'M(3) - 0
            '--- Test - 10
            'M(0) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator CType) - 0
            'M(Operator '-') - 0
            'M(Operator '>=') - 0
            'M(Operator '<=') - 0
            'M(1) - 0
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - 0
            'M(Operator '+') - 0
            'M(Operator '<=') - 0
            'M(3) - 0
            ']]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      302 (0x12e)
  .maxstack  6
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                Object V_3,
                Object V_4) //i
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "0"
  IL_000e:  call       "Function Program.M(String) As Integer"
  IL_0013:  pop
  IL_0014:  ldc.i4.3
  IL_0015:  stloc.2
  IL_0016:  ldloc.s    V_4
  IL_0018:  ldc.i4.0
  IL_0019:  call       "Function Program.LoopType.op_Implicit(Integer) As Program.LoopType"
  IL_001e:  box        "Program.LoopType"
  IL_0023:  ldc.i4.1
  IL_0024:  box        "Integer"
  IL_0029:  ldc.i4.1
  IL_002a:  box        "Integer"
  IL_002f:  ldloca.s   V_3
  IL_0031:  ldloca.s   V_4
  IL_0033:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForLoopInitObj(Object, Object, Object, Object, ByRef Object, ByRef Object) As Boolean"
  IL_0038:  brfalse.s  IL_0062
  IL_003a:  ldc.i4.4
  IL_003b:  stloc.2
  IL_003c:  ldstr      "1"
  IL_0041:  call       "Function Program.M(String) As Integer"
  IL_0046:  pop
  IL_0047:  ldc.i4.5
  IL_0048:  stloc.2
  IL_0049:  ldstr      "2"
  IL_004e:  call       "Function Program.M(String) As Integer"
  IL_0053:  pop
  IL_0054:  ldc.i4.6
  IL_0055:  stloc.2
  IL_0056:  ldloc.s    V_4
  IL_0058:  ldloc.3
  IL_0059:  ldloca.s   V_4
  IL_005b:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForNextCheckObj(Object, Object, ByRef Object) As Boolean"
  IL_0060:  brtrue.s   IL_003a
  IL_0062:  ldc.i4.7
  IL_0063:  stloc.2
  IL_0064:  ldstr      "3"
  IL_0069:  call       "Function Program.M(String) As Integer"
  IL_006e:  pop
    IL_006f:  leave      IL_0125
  IL_0074:  ldc.i4.s   9
  IL_0076:  stloc.2
  IL_0077:  ldstr      "OnError - {0}"
  IL_007c:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_0081:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_0086:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_008b:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_0090:  ldc.i4.s   10
  IL_0092:  stloc.2
  IL_0093:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0098:  ldloc.1
    IL_0099:  brtrue.s   IL_00a8
  IL_009b:  ldc.i4     0x800a0014
  IL_00a0:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00a5:  throw
    IL_00a6:  leave.s    IL_0125
    IL_00a8:  ldloc.1
    IL_00a9:  ldc.i4.1
    IL_00aa:  add
    IL_00ab:  ldc.i4.0
    IL_00ac:  stloc.1
    IL_00ad:  switch    (
        IL_00e2,
  IL_0000,
  IL_0007,
  IL_0014,
  IL_003a,
  IL_0047,
  IL_0054,
  IL_0062,
  IL_00a6,
  IL_0074,
  IL_0090,
  IL_00a6)
    IL_00e2:  leave.s    IL_011a
    IL_00e4:  ldloc.2
    IL_00e5:  stloc.1
    IL_00e6:  ldloc.0
    IL_00e7:  switch    (
        IL_00f8,
        IL_00a8,
  IL_0074)
    IL_00f8:  leave.s    IL_011a
}
  filter
{
    IL_00fa:  isinst     "System.Exception"
    IL_00ff:  ldnull
    IL_0100:  cgt.un
    IL_0102:  ldloc.0
    IL_0103:  ldc.i4.0
    IL_0104:  cgt.un
    IL_0106:  and
    IL_0107:  ldloc.1
    IL_0108:  ldc.i4.0
    IL_0109:  ceq
    IL_010b:  and
    IL_010c:  endfilter
}  // end filter
{  // handler
    IL_010e:  castclass  "System.Exception"
    IL_0113:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
    IL_0118:  leave.s    IL_00e4
}
  IL_011a:  ldc.i4     0x800a0033
  IL_011f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0124:  throw
  IL_0125:  ldloc.1
  IL_0126:  brfalse.s  IL_012d
  IL_0128:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_012d:  ret
}]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_ForTo_3()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        For i As Object = New LoopType(0) To 1
            If M4()
                Continue For
            End If
            M1()
ContinueLabel:
        Next
        M2()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Sub M2()
    End Sub

    Function M4() As Boolean
        Return False
    End Function

    Structure LoopType
        Private m_Val As Integer

        Public Sub New(x As Integer)
            m_Val = x
        End Sub

        Public Shared Widening Operator CType(x As Integer) As LoopType
            Return New LoopType(x)
        End Operator

        Public Shared Operator -(x As LoopType, y As LoopType) As LoopType
            Return New LoopType(x.m_Val - y.m_Val)
        End Operator

        Public Shared Operator +(x As LoopType, y As LoopType) As LoopType
            Return New LoopType(x.m_Val + y.m_Val)
        End Operator

        Public Shared Operator >=(x As LoopType, y As LoopType) As Boolean
            Return x.m_Val >= y.m_Val
        End Operator

        Public Shared Operator <=(x As LoopType, y As LoopType) As Boolean
            Return x.m_Val <= y.m_Val
        End Operator
    End Structure

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      213 (0xd5)
  .maxstack  6
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Object V_3, //VB$LoopObject
  Object V_4) //i
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  ldloc.s    V_4
  IL_0012:  ldc.i4.0
  IL_0013:  newobj     "Sub Program.LoopType..ctor(Integer)"
  IL_0018:  box        "Program.LoopType"
  IL_001d:  ldc.i4.1
  IL_001e:  box        "Integer"
  IL_0023:  ldc.i4.1
  IL_0024:  box        "Integer"
  IL_0029:  ldloca.s   V_3
  IL_002b:  ldloca.s   V_4
  IL_002d:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForLoopInitObj(Object, Object, Object, Object, ByRef Object, ByRef Object) As Boolean"
  IL_0032:  brfalse.s  IL_0052
  IL_0034:  ldc.i4.4
  IL_0035:  stloc.2
  IL_0036:  call       "Function Program.M4() As Boolean"
  IL_003b:  brtrue.s   IL_0044
  IL_003d:  ldc.i4.6
  IL_003e:  stloc.2
  IL_003f:  call       "Sub Program.M1()"
  IL_0044:  ldc.i4.7
  IL_0045:  stloc.2
  IL_0046:  ldloc.s    V_4
  IL_0048:  ldloc.3
  IL_0049:  ldloca.s   V_4
  IL_004b:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForNextCheckObj(Object, Object, ByRef Object) As Boolean"
  IL_0050:  brtrue.s   IL_0034
  IL_0052:  ldc.i4.8
  IL_0053:  stloc.2
  IL_0054:  call       "Sub Program.M2()"
  IL_0059:  leave.s    IL_00cc
  IL_005b:  ldloc.1
  IL_005c:  ldc.i4.1
  IL_005d:  add
  IL_005e:  ldc.i4.0
  IL_005f:  stloc.1
  IL_0060:  switch    (
  IL_008d,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_0034,
  IL_0044,
  IL_003d,
  IL_0044,
  IL_0052,
  IL_0059)
  IL_008d:  leave.s    IL_00c1
  IL_008f:  ldloc.2
  IL_0090:  stloc.1
  IL_0091:  ldloc.0
  IL_0092:  switch    (
  IL_009f,
  IL_005b)
  IL_009f:  leave.s    IL_00c1
}
  filter
{
  IL_00a1:  isinst     "System.Exception"
  IL_00a6:  ldnull
  IL_00a7:  cgt.un
  IL_00a9:  ldloc.0
  IL_00aa:  ldc.i4.0
  IL_00ab:  cgt.un
  IL_00ad:  and
  IL_00ae:  ldloc.1
  IL_00af:  ldc.i4.0
  IL_00b0:  ceq
  IL_00b2:  and
  IL_00b3:  endfilter
}  // end filter
{  // handler
  IL_00b5:  castclass  "System.Exception"
  IL_00ba:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00bf:  leave.s    IL_008f
}
  IL_00c1:  ldc.i4     0x800a0033
  IL_00c6:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00cb:  throw
  IL_00cc:  ldloc.1
  IL_00cd:  brfalse.s  IL_00d4
  IL_00cf:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d4:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      264 (0x108)
  .maxstack  6
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Object V_3, //VB$LoopObject
  Object V_4, //i
  Boolean V_5)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  ldloc.s    V_4
  IL_0017:  ldc.i4.0
  IL_0018:  newobj     "Sub Program.LoopType..ctor(Integer)"
  IL_001d:  box        "Program.LoopType"
  IL_0022:  ldc.i4.1
  IL_0023:  box        "Integer"
  IL_0028:  ldc.i4.1
  IL_0029:  box        "Integer"
  IL_002e:  ldloca.s   V_3
  IL_0030:  ldloca.s   V_4
  IL_0032:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForLoopInitObj(Object, Object, Object, Object, ByRef Object, ByRef Object) As Boolean"
  IL_0037:  ldc.i4.0
  IL_0038:  ceq
  IL_003a:  stloc.s    V_5
  IL_003c:  ldloc.s    V_5
  IL_003e:  brtrue.s   IL_006f
  IL_0040:  ldc.i4.4
  IL_0041:  stloc.2
  IL_0042:  call       "Function Program.M4() As Boolean"
  IL_0047:  ldc.i4.0
  IL_0048:  ceq
  IL_004a:  stloc.s    V_5
  IL_004c:  ldloc.s    V_5
  IL_004e:  brtrue.s   IL_0053
  IL_0050:  br.s       IL_005d
  IL_0052:  nop
  IL_0053:  nop
  IL_0054:  ldc.i4.7
  IL_0055:  stloc.2
  IL_0056:  call       "Sub Program.M1()"
  IL_005b:  nop
  IL_005c:  nop
  IL_005d:  ldc.i4.8
  IL_005e:  stloc.2
  IL_005f:  ldloc.s    V_4
  IL_0061:  ldloc.3
  IL_0062:  ldloca.s   V_4
  IL_0064:  call       "Function Microsoft.VisualBasic.CompilerServices.ObjectFlowControl.ForLoopControl.ForNextCheckObj(Object, Object, ByRef Object) As Boolean"
  IL_0069:  stloc.s    V_5
  IL_006b:  ldloc.s    V_5
  IL_006d:  brtrue.s   IL_0040
  IL_006f:  ldc.i4.s   9
  IL_0071:  stloc.2
  IL_0072:  call       "Sub Program.M2()"
  IL_0077:  nop
  IL_0078:  leave.s    IL_00f7
  IL_007a:  ldloc.1
  IL_007b:  ldc.i4.1
  IL_007c:  add
  IL_007d:  ldc.i4.0
  IL_007e:  stloc.1
  IL_007f:  switch    (
  IL_00b0,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0040,
  IL_0050,
  IL_0052,
  IL_0054,
  IL_005d,
  IL_006f,
  IL_0078)
  IL_00b0:  leave.s    IL_00ec
  IL_00b2:  ldloc.2
  IL_00b3:  stloc.1
  IL_00b4:  ldloc.0
  IL_00b5:  ldc.i4.s   -2
  IL_00b7:  bgt.s      IL_00bc
  IL_00b9:  ldc.i4.1
  IL_00ba:  br.s       IL_00bd
  IL_00bc:  ldloc.0
  IL_00bd:  switch    (
  IL_00ca,
  IL_007a)
  IL_00ca:  leave.s    IL_00ec
}
  filter
{
  IL_00cc:  isinst     "System.Exception"
  IL_00d1:  ldnull
  IL_00d2:  cgt.un
  IL_00d4:  ldloc.0
  IL_00d5:  ldc.i4.0
  IL_00d6:  cgt.un
  IL_00d8:  and
  IL_00d9:  ldloc.1
  IL_00da:  ldc.i4.0
  IL_00db:  ceq
  IL_00dd:  and
  IL_00de:  endfilter
}  // end filter
{  // handler
  IL_00e0:  castclass  "System.Exception"
  IL_00e5:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ea:  leave.s    IL_00b2
}
  IL_00ec:  ldc.i4     0x800a0033
  IL_00f1:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00f6:  throw
  IL_00f7:  ldloc.1
  IL_00f8:  ldc.i4.0
  IL_00f9:  ceq
  IL_00fb:  stloc.s    V_5
  IL_00fd:  ldloc.s    V_5
  IL_00ff:  brtrue.s   IL_0107
  IL_0101:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0106:  nop
  IL_0107:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_ForEach_String_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
            Dim states = {({-1, -1, -1, -1, 0}),
                      ({0, -1, 0, 0, 0}),
                      ({0, 0, -1, -1, -1, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        M("0")
        For Each c In GetString()
            System.Console.WriteLine("c={0}", Microsoft.VisualBasic.Strings.AscW(c))
            M("1")
            M("2")
        Next
        M("3")
        Return
OnError:
        System.Console.WriteLine("OnError - {0}", Microsoft.VisualBasic.Information.Err.GetException().GetType())
        Resume Next
    End Sub

    Function GetString() As String
        M("GetString")
        Return "ab"
    End Function

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException()
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
    End Class

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      352 (0x160)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  String V_3, //VB$ForEachArray
  Integer V_4, //VB$ForEachArrayIndex
  Char V_5, //c
  Boolean V_6)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  ldc.i4.2
  IL_000b:  stloc.2
  IL_000c:  ldstr      "0"
  IL_0011:  call       "Function Program.M(String) As Integer"
  IL_0016:  pop
  IL_0017:  ldc.i4.3
  IL_0018:  stloc.2
  IL_0019:  call       "Function Program.GetString() As String"
  IL_001e:  stloc.3
  IL_001f:  ldc.i4.0
  IL_0020:  stloc.s    V_4
  IL_0022:  br.s       IL_0064
  IL_0024:  ldloc.3
  IL_0025:  ldloc.s    V_4
  IL_0027:  callvirt   "Function String.get_Chars(Integer) As Char"
  IL_002c:  stloc.s    V_5
  IL_002e:  ldc.i4.4
  IL_002f:  stloc.2
  IL_0030:  ldstr      "c={0}"
  IL_0035:  ldloc.s    V_5
  IL_0037:  box        "Integer"
  IL_003c:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_0041:  nop
  IL_0042:  ldc.i4.5
  IL_0043:  stloc.2
  IL_0044:  ldstr      "1"
  IL_0049:  call       "Function Program.M(String) As Integer"
  IL_004e:  pop
  IL_004f:  ldc.i4.6
  IL_0050:  stloc.2
  IL_0051:  ldstr      "2"
  IL_0056:  call       "Function Program.M(String) As Integer"
  IL_005b:  pop
  IL_005c:  ldc.i4.7
  IL_005d:  stloc.2
  IL_005e:  ldloc.s    V_4
  IL_0060:  ldc.i4.1
  IL_0061:  add.ovf
  IL_0062:  stloc.s    V_4
  IL_0064:  ldloc.s    V_4
  IL_0066:  ldloc.3
  IL_0067:  callvirt   "Function String.get_Length() As Integer"
  IL_006c:  clt
  IL_006e:  stloc.s    V_6
  IL_0070:  ldloc.s    V_6
  IL_0072:  brtrue.s   IL_0024
  IL_0074:  ldc.i4.8
  IL_0075:  stloc.2
  IL_0076:  ldstr      "3"
  IL_007b:  call       "Function Program.M(String) As Integer"
  IL_0080:  pop
  IL_0081:  br.s       IL_00c1
  IL_0083:  nop
  IL_0084:  ldc.i4.s   10
  IL_0086:  stloc.2
  IL_0087:  ldstr      "OnError - {0}"
  IL_008c:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_0091:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_0096:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_009b:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_00a0:  nop
  IL_00a1:  ldc.i4.s   11
  IL_00a3:  stloc.2
  IL_00a4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a9:  nop
  IL_00aa:  ldloc.1
  IL_00ab:  ldc.i4.0
  IL_00ac:  cgt.un
  IL_00ae:  stloc.s    V_6
  IL_00b0:  ldloc.s    V_6
  IL_00b2:  brtrue.s   IL_00bf
  IL_00b4:  ldc.i4     0x800a0014
  IL_00b9:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00be:  throw
  IL_00bf:  br.s       IL_00c6
  IL_00c1:  leave      IL_014f
  IL_00c6:  ldloc.1
  IL_00c7:  ldc.i4.1
  IL_00c8:  add
  IL_00c9:  ldc.i4.0
  IL_00ca:  stloc.1
  IL_00cb:  switch    (
  IL_0104,
  IL_0002,
  IL_000a,
  IL_0017,
  IL_002e,
  IL_0042,
  IL_004f,
  IL_005c,
  IL_0074,
  IL_0081,
  IL_0084,
  IL_00a1,
  IL_00c1)
  IL_0104:  leave.s    IL_0144
  IL_0106:  ldloc.2
  IL_0107:  stloc.1
  IL_0108:  ldloc.0
  IL_0109:  ldc.i4.s   -2
  IL_010b:  bgt.s      IL_0110
  IL_010d:  ldc.i4.1
  IL_010e:  br.s       IL_0111
  IL_0110:  ldloc.0
  IL_0111:  switch    (
  IL_0122,
  IL_00c6,
  IL_0083)
  IL_0122:  leave.s    IL_0144
}
  filter
{
  IL_0124:  isinst     "System.Exception"
  IL_0129:  ldnull
  IL_012a:  cgt.un
  IL_012c:  ldloc.0
  IL_012d:  ldc.i4.0
  IL_012e:  cgt.un
  IL_0130:  and
  IL_0131:  ldloc.1
  IL_0132:  ldc.i4.0
  IL_0133:  ceq
  IL_0135:  and
  IL_0136:  endfilter
}  // end filter
{  // handler
  IL_0138:  castclass  "System.Exception"
  IL_013d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0142:  leave.s    IL_0106
}
  IL_0144:  ldc.i4     0x800a0033
  IL_0149:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_014e:  throw
  IL_014f:  ldloc.1
  IL_0150:  ldc.i4.0
  IL_0151:  ceq
  IL_0153:  stloc.s    V_6
  IL_0155:  ldloc.s    V_6
  IL_0157:  brtrue.s   IL_015f
  IL_0159:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_015e:  nop
  IL_015f:  ret
}]]>)

            '            Dim expected =
            '            <![CDATA[
            '--- Test - 0
            'M(0) - -1
            'OnError - Program+TestException
            'M(GetString) - -1
            'OnError - Program+TestException
            'c=0
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'OnError - System.NullReferenceException
            'M(3) - 0
            '--- Test - 1
            'M(0) - 0
            'M(GetString) - -1
            'OnError - Program+TestException
            'c=0
            'M(1) - 0
            'M(2) - 0
            'OnError - System.NullReferenceException
            'M(3) - 0
            '--- Test - 2
            'M(0) - 0
            'M(GetString) - 0
            'c=97
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'c=98
            'M(1) - -1
            'OnError - Program+TestException
            'M(2) - -1
            'OnError - Program+TestException
            'M(3) - 0
            ']]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Program.Test",
            <![CDATA[{
  // Code size      317 (0x13d)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                String V_3,
                Integer V_4,
                Char V_5) //c
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "0"
  IL_000e:  call       "Function Program.M(String) As Integer"
  IL_0013:  pop
  IL_0014:  ldc.i4.3
  IL_0015:  stloc.2
  IL_0016:  call       "Function Program.GetString() As String"
  IL_001b:  stloc.3
  IL_001c:  ldc.i4.0
  IL_001d:  stloc.s    V_4
  IL_001f:  br.s       IL_0060
  IL_0021:  ldloc.3
  IL_0022:  ldloc.s    V_4
  IL_0024:  callvirt   "Function String.get_Chars(Integer) As Char"
  IL_0029:  stloc.s    V_5
  IL_002b:  ldc.i4.4
  IL_002c:  stloc.2
  IL_002d:  ldstr      "c={0}"
  IL_0032:  ldloc.s    V_5
  IL_0034:  box        "Integer"
  IL_0039:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_003e:  ldc.i4.5
  IL_003f:  stloc.2
  IL_0040:  ldstr      "1"
  IL_0045:  call       "Function Program.M(String) As Integer"
  IL_004a:  pop
  IL_004b:  ldc.i4.6
  IL_004c:  stloc.2
  IL_004d:  ldstr      "2"
  IL_0052:  call       "Function Program.M(String) As Integer"
  IL_0057:  pop
  IL_0058:  ldc.i4.7
  IL_0059:  stloc.2
  IL_005a:  ldloc.s    V_4
  IL_005c:  ldc.i4.1
  IL_005d:  add.ovf
  IL_005e:  stloc.s    V_4
  IL_0060:  ldloc.s    V_4
  IL_0062:  ldloc.3
  IL_0063:  callvirt   "Function String.get_Length() As Integer"
  IL_0068:  blt.s      IL_0021
  IL_006a:  ldc.i4.8
  IL_006b:  stloc.2
  IL_006c:  ldstr      "3"
  IL_0071:  call       "Function Program.M(String) As Integer"
  IL_0076:  pop
    IL_0077:  leave      IL_0134
  IL_007c:  ldc.i4.s   10
  IL_007e:  stloc.2
  IL_007f:  ldstr      "OnError - {0}"
  IL_0084:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_0089:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_008e:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_0093:  call       "Sub System.Console.WriteLine(String, Object)"
  IL_0098:  ldc.i4.s   11
  IL_009a:  stloc.2
  IL_009b:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a0:  ldloc.1
  IL_00a1:  brtrue.s   IL_00b3
  IL_00a3:  ldc.i4     0x800a0014
  IL_00a8:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ad:  throw
    IL_00ae:  leave      IL_0134
  IL_00b3:  ldloc.1
  IL_00b4:  ldc.i4.1
  IL_00b5:  add
  IL_00b6:  ldc.i4.0
  IL_00b7:  stloc.1
  IL_00b8:  switch    (
  IL_00f1,
  IL_0000,
  IL_0007,
  IL_0014,
  IL_002b,
  IL_003e,
  IL_004b,
  IL_0058,
  IL_006a,
  IL_00ae,
  IL_007c,
  IL_0098,
  IL_00ae)
    IL_00f1:  leave.s    IL_0129
  IL_00f3:  ldloc.2
  IL_00f4:  stloc.1
  IL_00f5:  ldloc.0
    IL_00f6:  switch    (
        IL_0107,
  IL_00b3,
  IL_007c)
    IL_0107:  leave.s    IL_0129
}
  filter
{
    IL_0109:  isinst     "System.Exception"
    IL_010e:  ldnull
    IL_010f:  cgt.un
    IL_0111:  ldloc.0
    IL_0112:  ldc.i4.0
    IL_0113:  cgt.un
    IL_0115:  and
    IL_0116:  ldloc.1
    IL_0117:  ldc.i4.0
    IL_0118:  ceq
    IL_011a:  and
    IL_011b:  endfilter
}  // end filter
{  // handler
    IL_011d:  castclass  "System.Exception"
    IL_0122:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
    IL_0127:  leave.s    IL_00f3
}
  IL_0129:  ldc.i4     0x800a0033
  IL_012e:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0133:  throw
  IL_0134:  ldloc.1
  IL_0135:  brfalse.s  IL_013c
  IL_0137:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_013c:  ret
}]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_ForEach_String_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        For Each c In GetString()
            If M4()
                Continue For
            End If
            M1()
ContinueLabel:
        Next
        M2()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Sub M2()
    End Sub

    Function M4() As Boolean
        Return False
    End Function

    Function GetString() As String
        Return "ab"
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      201 (0xc9)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  String V_3, //VB$ForEachArray
  Integer V_4) //VB$ForEachArrayIndex
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.GetString() As String"
  IL_0015:  stloc.3
  IL_0016:  ldc.i4.0
  IL_0017:  stloc.s    V_4
  IL_0019:  br.s       IL_003c
  IL_001b:  ldloc.3
  IL_001c:  ldloc.s    V_4
  IL_001e:  callvirt   "Function String.get_Chars(Integer) As Char"
  IL_0023:  pop
  IL_0024:  ldc.i4.4
  IL_0025:  stloc.2
  IL_0026:  call       "Function Program.M4() As Boolean"
  IL_002b:  brtrue.s   IL_0034
  IL_002d:  ldc.i4.6
  IL_002e:  stloc.2
  IL_002f:  call       "Sub Program.M1()"
  IL_0034:  ldc.i4.7
  IL_0035:  stloc.2
  IL_0036:  ldloc.s    V_4
  IL_0038:  ldc.i4.1
  IL_0039:  add.ovf
  IL_003a:  stloc.s    V_4
  IL_003c:  ldloc.s    V_4
  IL_003e:  ldloc.3
  IL_003f:  callvirt   "Function String.get_Length() As Integer"
  IL_0044:  blt.s      IL_001b
  IL_0046:  ldc.i4.8
  IL_0047:  stloc.2
  IL_0048:  call       "Sub Program.M2()"
  IL_004d:  leave.s    IL_00c0
  IL_004f:  ldloc.1
  IL_0050:  ldc.i4.1
  IL_0051:  add
  IL_0052:  ldc.i4.0
  IL_0053:  stloc.1
  IL_0054:  switch    (
  IL_0081,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_0024,
  IL_0034,
  IL_002d,
  IL_0034,
  IL_0046,
  IL_004d)
  IL_0081:  leave.s    IL_00b5
  IL_0083:  ldloc.2
  IL_0084:  stloc.1
  IL_0085:  ldloc.0
  IL_0086:  switch    (
  IL_0093,
  IL_004f)
  IL_0093:  leave.s    IL_00b5
}
  filter
{
  IL_0095:  isinst     "System.Exception"
  IL_009a:  ldnull
  IL_009b:  cgt.un
  IL_009d:  ldloc.0
  IL_009e:  ldc.i4.0
  IL_009f:  cgt.un
  IL_00a1:  and
  IL_00a2:  ldloc.1
  IL_00a3:  ldc.i4.0
  IL_00a4:  ceq
  IL_00a6:  and
  IL_00a7:  endfilter
}  // end filter
{  // handler
  IL_00a9:  castclass  "System.Exception"
  IL_00ae:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b3:  leave.s    IL_0083
}
  IL_00b5:  ldc.i4     0x800a0033
  IL_00ba:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00bf:  throw
  IL_00c0:  ldloc.1
  IL_00c1:  brfalse.s  IL_00c8
  IL_00c3:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c8:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      248 (0xf8)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  String V_3, //VB$ForEachArray
  Integer V_4, //VB$ForEachArrayIndex
  Char V_5, //c
  Boolean V_6)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.GetString() As String"
  IL_001a:  stloc.3
  IL_001b:  ldc.i4.0
  IL_001c:  stloc.s    V_4
  IL_001e:  br.s       IL_004f
  IL_0020:  ldloc.3
  IL_0021:  ldloc.s    V_4
  IL_0023:  callvirt   "Function String.get_Chars(Integer) As Char"
  IL_0028:  stloc.s    V_5
  IL_002a:  ldc.i4.4
  IL_002b:  stloc.2
  IL_002c:  call       "Function Program.M4() As Boolean"
  IL_0031:  ldc.i4.0
  IL_0032:  ceq
  IL_0034:  stloc.s    V_6
  IL_0036:  ldloc.s    V_6
  IL_0038:  brtrue.s   IL_003d
  IL_003a:  br.s       IL_0047
  IL_003c:  nop
  IL_003d:  nop
  IL_003e:  ldc.i4.7
  IL_003f:  stloc.2
  IL_0040:  call       "Sub Program.M1()"
  IL_0045:  nop
  IL_0046:  nop
  IL_0047:  ldc.i4.8
  IL_0048:  stloc.2
  IL_0049:  ldloc.s    V_4
  IL_004b:  ldc.i4.1
  IL_004c:  add.ovf
  IL_004d:  stloc.s    V_4
  IL_004f:  ldloc.s    V_4
  IL_0051:  ldloc.3
  IL_0052:  callvirt   "Function String.get_Length() As Integer"
  IL_0057:  clt
  IL_0059:  stloc.s    V_6
  IL_005b:  ldloc.s    V_6
  IL_005d:  brtrue.s   IL_0020
  IL_005f:  ldc.i4.s   9
  IL_0061:  stloc.2
  IL_0062:  call       "Sub Program.M2()"
  IL_0067:  nop
  IL_0068:  leave.s    IL_00e7
  IL_006a:  ldloc.1
  IL_006b:  ldc.i4.1
  IL_006c:  add
  IL_006d:  ldc.i4.0
  IL_006e:  stloc.1
  IL_006f:  switch    (
  IL_00a0,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_002a,
  IL_003a,
  IL_003c,
  IL_003e,
  IL_0047,
  IL_005f,
  IL_0068)
  IL_00a0:  leave.s    IL_00dc
  IL_00a2:  ldloc.2
  IL_00a3:  stloc.1
  IL_00a4:  ldloc.0
  IL_00a5:  ldc.i4.s   -2
  IL_00a7:  bgt.s      IL_00ac
  IL_00a9:  ldc.i4.1
  IL_00aa:  br.s       IL_00ad
  IL_00ac:  ldloc.0
  IL_00ad:  switch    (
  IL_00ba,
  IL_006a)
  IL_00ba:  leave.s    IL_00dc
}
  filter
{
  IL_00bc:  isinst     "System.Exception"
  IL_00c1:  ldnull
  IL_00c2:  cgt.un
  IL_00c4:  ldloc.0
  IL_00c5:  ldc.i4.0
  IL_00c6:  cgt.un
  IL_00c8:  and
  IL_00c9:  ldloc.1
  IL_00ca:  ldc.i4.0
  IL_00cb:  ceq
  IL_00cd:  and
  IL_00ce:  endfilter
}  // end filter
{  // handler
  IL_00d0:  castclass  "System.Exception"
  IL_00d5:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00da:  leave.s    IL_00a2
}
  IL_00dc:  ldc.i4     0x800a0033
  IL_00e1:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e6:  throw
  IL_00e7:  ldloc.1
  IL_00e8:  ldc.i4.0
  IL_00e9:  ceq
  IL_00eb:  stloc.s    V_6
  IL_00ed:  ldloc.s    V_6
  IL_00ef:  brtrue.s   IL_00f7
  IL_00f1:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00f6:  nop
  IL_00f7:  ret
}
]]>)
        End Sub

        <Fact(), WorkItem(737273, "DevDiv")>
        Public Sub Resume_in_ForEach_Array_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, -1, 0}),
                      ({0, -1, 0, 0, 0}),
                      ({0, 0, -1, -1, -1, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError
        M("0")
        For Each c In GetArray()
            System.Console.WriteLine("c={0}", Microsoft.VisualBasic.Strings.AscW(c))
            M("1")
            M("2")
        Next
        M("3")
        Return
OnError:
        System.Console.WriteLine("OnError - {0}", Microsoft.VisualBasic.Information.Err.GetException().GetType())
        Resume Next
    End Sub

    Function GetArray() As Char()
        M("GetArray")
        Return "ab"
    End Function

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException()
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
    End Class

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            CompileAndVerify(compilation)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation)

        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Public Sub Resume_in_ForEach_Array_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Module Program

    Sub Main()
        On Error Resume Next
        M0()
        For Each c In GetArray()
            If M4()
                Continue For
            End If
            M1()
ContinueLabel:
        Next
        M2()
    End Sub

    Sub M0()
    End Sub

    Sub M1()
    End Sub

    Sub M2()
    End Sub

    Function M4() As Boolean
        Return False
    End Function

    Function GetArray() As Char()
        Return "ab"
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      194 (0xc2)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Char() V_3, //VB$ForEachArray
  Integer V_4) //VB$ForEachArrayIndex
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  call       "Sub Program.M0()"
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  call       "Function Program.GetArray() As Char()"
  IL_0015:  stloc.3
  IL_0016:  ldc.i4.0
  IL_0017:  stloc.s    V_4
  IL_0019:  br.s       IL_0038
  IL_001b:  ldloc.3
  IL_001c:  ldloc.s    V_4
  IL_001e:  ldelem.u2
  IL_001f:  pop
  IL_0020:  ldc.i4.4
  IL_0021:  stloc.2
  IL_0022:  call       "Function Program.M4() As Boolean"
  IL_0027:  brtrue.s   IL_0030
  IL_0029:  ldc.i4.6
  IL_002a:  stloc.2
  IL_002b:  call       "Sub Program.M1()"
  IL_0030:  ldc.i4.7
  IL_0031:  stloc.2
  IL_0032:  ldloc.s    V_4
  IL_0034:  ldc.i4.1
  IL_0035:  add.ovf
  IL_0036:  stloc.s    V_4
  IL_0038:  ldloc.s    V_4
  IL_003a:  ldloc.3
  IL_003b:  ldlen
  IL_003c:  conv.i4
  IL_003d:  blt.s      IL_001b
  IL_003f:  ldc.i4.8
  IL_0040:  stloc.2
  IL_0041:  call       "Sub Program.M2()"
  IL_0046:  leave.s    IL_00b9
  IL_0048:  ldloc.1
  IL_0049:  ldc.i4.1
  IL_004a:  add
  IL_004b:  ldc.i4.0
  IL_004c:  stloc.1
  IL_004d:  switch    (
  IL_007a,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_0020,
  IL_0030,
  IL_0029,
  IL_0030,
  IL_003f,
  IL_0046)
  IL_007a:  leave.s    IL_00ae
  IL_007c:  ldloc.2
  IL_007d:  stloc.1
  IL_007e:  ldloc.0
  IL_007f:  switch    (
  IL_008c,
  IL_0048)
  IL_008c:  leave.s    IL_00ae
}
  filter
{
  IL_008e:  isinst     "System.Exception"
  IL_0093:  ldnull
  IL_0094:  cgt.un
  IL_0096:  ldloc.0
  IL_0097:  ldc.i4.0
  IL_0098:  cgt.un
  IL_009a:  and
  IL_009b:  ldloc.1
  IL_009c:  ldc.i4.0
  IL_009d:  ceq
  IL_009f:  and
  IL_00a0:  endfilter
}  // end filter
{  // handler
  IL_00a2:  castclass  "System.Exception"
  IL_00a7:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ac:  leave.s    IL_007c
}
  IL_00ae:  ldc.i4     0x800a0033
  IL_00b3:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b8:  throw
  IL_00b9:  ldloc.1
  IL_00ba:  brfalse.s  IL_00c1
  IL_00bc:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c1:  ret
}
]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.Main",
            <![CDATA[
{
  // Code size      241 (0xf1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Char() V_3, //VB$ForEachArray
  Integer V_4, //VB$ForEachArrayIndex
  Char V_5, //c
  Boolean V_6)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.s   -2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.2
  IL_000c:  stloc.2
  IL_000d:  call       "Sub Program.M0()"
  IL_0012:  nop
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Program.GetArray() As Char()"
  IL_001a:  stloc.3
  IL_001b:  ldc.i4.0
  IL_001c:  stloc.s    V_4
  IL_001e:  br.s       IL_004b
  IL_0020:  ldloc.3
  IL_0021:  ldloc.s    V_4
  IL_0023:  ldelem.u2
  IL_0024:  stloc.s    V_5
  IL_0026:  ldc.i4.4
  IL_0027:  stloc.2
  IL_0028:  call       "Function Program.M4() As Boolean"
  IL_002d:  ldc.i4.0
  IL_002e:  ceq
  IL_0030:  stloc.s    V_6
  IL_0032:  ldloc.s    V_6
  IL_0034:  brtrue.s   IL_0039
  IL_0036:  br.s       IL_0043
  IL_0038:  nop
  IL_0039:  nop
  IL_003a:  ldc.i4.7
  IL_003b:  stloc.2
  IL_003c:  call       "Sub Program.M1()"
  IL_0041:  nop
  IL_0042:  nop
  IL_0043:  ldc.i4.8
  IL_0044:  stloc.2
  IL_0045:  ldloc.s    V_4
  IL_0047:  ldc.i4.1
  IL_0048:  add.ovf
  IL_0049:  stloc.s    V_4
  IL_004b:  ldloc.s    V_4
  IL_004d:  ldloc.3
  IL_004e:  ldlen
  IL_004f:  conv.i4
  IL_0050:  clt
  IL_0052:  stloc.s    V_6
  IL_0054:  ldloc.s    V_6
  IL_0056:  brtrue.s   IL_0020
  IL_0058:  ldc.i4.s   9
  IL_005a:  stloc.2
  IL_005b:  call       "Sub Program.M2()"
  IL_0060:  nop
  IL_0061:  leave.s    IL_00e0
  IL_0063:  ldloc.1
  IL_0064:  ldc.i4.1
  IL_0065:  add
  IL_0066:  ldc.i4.0
  IL_0067:  stloc.1
  IL_0068:  switch    (
  IL_0099,
  IL_0002,
  IL_000b,
  IL_0013,
  IL_0026,
  IL_0036,
  IL_0038,
  IL_003a,
  IL_0043,
  IL_0058,
  IL_0061)
  IL_0099:  leave.s    IL_00d5
  IL_009b:  ldloc.2
  IL_009c:  stloc.1
  IL_009d:  ldloc.0
  IL_009e:  ldc.i4.s   -2
  IL_00a0:  bgt.s      IL_00a5
  IL_00a2:  ldc.i4.1
  IL_00a3:  br.s       IL_00a6
  IL_00a5:  ldloc.0
  IL_00a6:  switch    (
  IL_00b3,
  IL_0063)
  IL_00b3:  leave.s    IL_00d5
}
  filter
{
  IL_00b5:  isinst     "System.Exception"
  IL_00ba:  ldnull
  IL_00bb:  cgt.un
  IL_00bd:  ldloc.0
  IL_00be:  ldc.i4.0
  IL_00bf:  cgt.un
  IL_00c1:  and
  IL_00c2:  ldloc.1
  IL_00c3:  ldc.i4.0
  IL_00c4:  ceq
  IL_00c6:  and
  IL_00c7:  endfilter
}  // end filter
{  // handler
  IL_00c9:  castclass  "System.Exception"
  IL_00ce:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00d3:  leave.s    IL_009b
}
  IL_00d5:  ldc.i4     0x800a0033
  IL_00da:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00df:  throw
  IL_00e0:  ldloc.1
  IL_00e1:  ldc.i4.0
  IL_00e2:  ceq
  IL_00e4:  stloc.s    V_6
  IL_00e6:  ldloc.s    V_6
  IL_00e8:  brtrue.s   IL_00f0
  IL_00ea:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ef:  nop
  IL_00f0:  ret
}
]]>)
        End Sub

        <Fact()>
        Public Sub Resume_in_SyncLock_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, 0}),
                      ({0, 0, -1, 0}),
                      ({0, 0, 0, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError

        M("0")
        SyncLock CObj(GetLock())
            M("1")
            M("2")
        End SyncLock
        M("3")

        Return
OnError:
        System.Console.WriteLine("OnError - {0}", Microsoft.VisualBasic.Information.Err.GetException().GetType())
        Resume Next
    End Sub

    Function GetLock() As Object
        M("GetLock")
        Return New Object()
    End Function

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException()
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
    End Class

End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException
M(GetLock) - -1
OnError - Program+TestException
M(3) - 0
--- Test - 1
M(0) - 0
M(GetLock) - 0
M(1) - -1
OnError - Program+TestException
M(3) - 0
--- Test - 2
M(0) - 0
M(GetLock) - 0
M(1) - 0
M(2) - -1
OnError - Program+TestException
M(3) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_Using_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, 0}),
                      ({0, 0, -1, 0, 0}),
                      ({0, 0, 0, -1, 0, 0}),
                      ({0, 0, -1, -1, 0}),
                      ({0, 0, 0, 0, -1, 0}),
                      ({0, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError

        M("0")
        Using x = GetDisposable()
            M("1")
            M("2")
        End Using
        M("3")

        Return
OnError:
        Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
        System.Console.WriteLine("OnError - {0}{1}",
                                 ex.GetType(),
                                 If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
        Resume Next
    End Sub

    Function GetDisposable() As IDisposable
        M("GetDisposable")
        Return New Disposable()
    End Function

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class

    Class Disposable
        Implements IDisposable

        Public Sub Dispose() Implements IDisposable.Dispose
            M("Dispose")
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(GetDisposable) - -1
OnError - Program+TestException - GetDisposable
M(3) - 0
--- Test - 1
M(0) - 0
M(GetDisposable) - 0
M(1) - -1
M(Dispose) - 0
OnError - Program+TestException - 1
M(3) - 0
--- Test - 2
M(0) - 0
M(GetDisposable) - 0
M(1) - 0
M(2) - -1
M(Dispose) - 0
OnError - Program+TestException - 2
M(3) - 0
--- Test - 3
M(0) - 0
M(GetDisposable) - 0
M(1) - -1
M(Dispose) - -1
OnError - Program+TestException - Dispose
M(3) - 0
--- Test - 4
M(0) - 0
M(GetDisposable) - 0
M(1) - 0
M(2) - 0
M(Dispose) - -1
OnError - Program+TestException - Dispose
M(3) - 0
--- Test - 5
M(0) - 0
M(GetDisposable) - 0
M(1) - 0
M(2) - 0
M(Dispose) - 0
M(3) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_Using_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, 0}),
                      ({0, 0, -1, 0, 0}),
                      ({0, 0, 0, -1, 0, 0, 0}),
                      ({0, 0, -1, -1, 0, 0}),
                      ({0, 0, 0, 0, -1, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, -1, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Sub Test()
        On Error GoTo OnError

        M("0")
        Using x = GetDisposable(1), y = GetDisposable(2)
            M("1")
            M("2")
        End Using
        M("3")

        Return
OnError:
        Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
        System.Console.WriteLine("OnError - {0}{1}",
                                 ex.GetType(),
                                 If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
        Resume Next
    End Sub

    Function GetDisposable(x As Integer) As IDisposable
        M("GetDisposable " & x)
        Return New Disposable(x)
    End Function

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class

    Class Disposable
        Implements IDisposable

        Private tag As Integer

        Sub New(x As Integer)
            tag = x
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            M("Dispose " & tag)
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(GetDisposable 1) - -1
OnError - Program+TestException - GetDisposable 1
M(3) - 0
--- Test - 1
M(0) - 0
M(GetDisposable 1) - 0
M(GetDisposable 2) - -1
M(Dispose 1) - 0
OnError - Program+TestException - GetDisposable 2
M(3) - 0
--- Test - 2
M(0) - 0
M(GetDisposable 1) - 0
M(GetDisposable 2) - 0
M(1) - -1
M(Dispose 2) - 0
M(Dispose 1) - 0
OnError - Program+TestException - 1
M(3) - 0
--- Test - 3
M(0) - 0
M(GetDisposable 1) - 0
M(GetDisposable 2) - -1
M(Dispose 1) - -1
OnError - Program+TestException - Dispose 1
M(3) - 0
--- Test - 4
M(0) - 0
M(GetDisposable 1) - 0
M(GetDisposable 2) - 0
M(1) - 0
M(2) - -1
M(Dispose 2) - 0
M(Dispose 1) - 0
OnError - Program+TestException - 2
M(3) - 0
--- Test - 5
M(0) - 0
M(GetDisposable 1) - 0
M(GetDisposable 2) - 0
M(1) - 0
M(2) - 0
M(Dispose 2) - -1
M(Dispose 1) - 0
OnError - Program+TestException - Dispose 2
M(3) - 0
--- Test - 6
M(0) - 0
M(GetDisposable 1) - 0
M(GetDisposable 2) - 0
M(1) - 0
M(2) - 0
M(Dispose 2) - 0
M(Dispose 1) - -1
OnError - Program+TestException - Dispose 1
M(3) - 0
--- Test - 7
M(0) - 0
M(GetDisposable 1) - 0
M(GetDisposable 2) - 0
M(1) - 0
M(2) - 0
M(Dispose 2) - 0
M(Dispose 1) - 0
M(3) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_VariableDeclaration_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0}),
                      ({0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, -1, 0}),
                      ({0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, -1, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            M("0")
            Dim x1 As Integer = M("1"), y1 As Integer, z1 As Integer = M("2")
            M("3")
            Static x2 As Integer = M("4"), y2 As Integer, z2 As Integer = M("5")
            M("6")
            Dim x3, z3 As New TestException(M("7"))
            M("8")
            Static x4, z4 As New TestException(M("9"))
            M("10")
            Dim z5 As New TestException(M("11"))
            M("12")
            Static z6 As New TestException(M("13"))
            M("14")

            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - -1
OnError - Program+TestException - 2
M(3) - -1
OnError - Program+TestException - 3
M(4) - -1
OnError - Program+TestException - 4
M(5) - -1
OnError - Program+TestException - 5
M(6) - -1
OnError - Program+TestException - 6
M(7) - -1
OnError - Program+TestException - 7
M(7) - -1
OnError - Program+TestException - 7
M(8) - -1
OnError - Program+TestException - 8
M(9) - -1
OnError - Program+TestException - 9
M(9) - -1
OnError - Program+TestException - 9
M(10) - -1
OnError - Program+TestException - 10
M(11) - -1
OnError - Program+TestException - 11
M(12) - -1
OnError - Program+TestException - 12
M(13) - -1
OnError - Program+TestException - 13
M(14) - 0
--- Test - 1
M(0) - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - 0
M(3) - 0
M(4) - -1
OnError - Program+TestException - 4
M(5) - 0
M(6) - 0
M(7) - -1
OnError - Program+TestException - 7
M(7) - 0
M(8) - 0
M(9) - -1
OnError - Program+TestException - 9
M(9) - 0
M(10) - 0
M(11) - -1
OnError - Program+TestException - 11
M(12) - 0
M(13) - -1
OnError - Program+TestException - 13
M(14) - 0
--- Test - 2
M(0) - 0
M(1) - 0
M(2) - -1
OnError - Program+TestException - 2
M(3) - 0
M(4) - 0
M(5) - -1
OnError - Program+TestException - 5
M(6) - 0
M(7) - 0
M(7) - -1
OnError - Program+TestException - 7
M(8) - 0
M(9) - 0
M(9) - -1
OnError - Program+TestException - 9
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_With_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, -1, 0}),
                      ({0, -1, 0, 0, 0}),
                      ({0, 0, -1, 0, 0}),
                      ({0, 0, 0, -1, 0}),
                      ({0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            M("0")
            With M("1")
                M("2")
                M("3")
            End With
            M("4")

            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - -1
OnError - Program+TestException - 2
M(3) - -1
OnError - Program+TestException - 3
M(4) - 0
--- Test - 1
M(0) - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - 0
M(3) - 0
M(4) - 0
--- Test - 2
M(0) - 0
M(1) - 0
M(2) - -1
OnError - Program+TestException - 2
M(3) - 0
M(4) - 0
--- Test - 3
M(0) - 0
M(1) - 0
M(2) - 0
M(3) - -1
OnError - Program+TestException - 3
M(4) - 0
--- Test - 4
M(0) - 0
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_ReDim_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, 0, -1, 0}),
                      ({0, 2, -1, -1, 0}),
                      ({0, 2, -1, 2, 0}),
                      ({0, 2, 0, 2, 0}),
                      ({0, -1, 0, 2, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            Dim x = {1}
            Dim saveX = x

            Dim y = {({1})}
            Dim saveY = y(0)

            M("0")
            ReDim Preserve x(M("1")), y(M("2"))(M("3"))
            M("4")

            System.Console.WriteLine("x is {0}Changed = {1}; Content is {2}preserved",
                                     If(x Is saveX, "not ", ""),
                                     If(x Is Nothing, "NOTHING", x),
                                     If(x Is Nothing OrElse x(0) <> saveX(0), "NOT ", ""))

            System.Console.WriteLine("y(0) is {0}Changed = {1}; Content is {2}preserved",
                                     If(y(0) Is saveY, "not ", ""),
                                     If(y(0) Is Nothing, "NOTHING", y(0)),
                                     If(y(0) Is Nothing OrElse y(0)(0) <> saveY(0), "NOT ", ""))

            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)


            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - 0
M(3) - -1
OnError - Program+TestException - 3
M(4) - 0
x is not Changed = System.Int32[]; Content is preserved
y(0) is not Changed = System.Int32[]; Content is preserved
--- Test - 1
M(0) - 0
M(1) - 2
M(2) - -1
OnError - Program+TestException - 2
M(4) - -1
OnError - Program+TestException - 4
x is Changed = System.Int32[]; Content is preserved
y(0) is not Changed = System.Int32[]; Content is preserved
--- Test - 2
M(0) - 0
M(1) - 2
M(2) - -1
OnError - Program+TestException - 2
M(4) - 2
x is Changed = System.Int32[]; Content is preserved
y(0) is not Changed = System.Int32[]; Content is preserved
--- Test - 3
M(0) - 0
M(1) - 2
M(2) - 0
M(3) - 2
M(4) - 0
x is Changed = System.Int32[]; Content is preserved
y(0) is Changed = System.Int32[]; Content is preserved
--- Test - 4
M(0) - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - 0
M(3) - 2
M(4) - 0
x is not Changed = System.Int32[]; Content is preserved
y(0) is Changed = System.Int32[]; Content is preserved]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_ReDim_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, 0}),
                      ({0, -1, -1, 0}),
                      ({0, -1, 0, 0}),
                      ({0, 0, -1, 0}),
                      ({0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            Dim y = {({1})}
            Dim saveY = y(0)

            M("0")
            ReDim y(M("1"))(M("2"))
            M("3")

            System.Console.WriteLine("y(0) is {0}Changed = {1}",
                                     If(y(0) Is saveY, "not ", ""),
                                     If(y(0) Is Nothing, "NOTHING", y(0)))

            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(1) - -1
OnError - Program+TestException - 1
M(3) - -1
OnError - Program+TestException - 3
y(0) is not Changed = System.Int32[]
--- Test - 1
M(0) - 0
M(1) - -1
OnError - Program+TestException - 1
M(3) - -1
OnError - Program+TestException - 3
y(0) is not Changed = System.Int32[]
--- Test - 2
M(0) - 0
M(1) - -1
OnError - Program+TestException - 1
M(3) - 0
y(0) is not Changed = System.Int32[]
--- Test - 3
M(0) - 0
M(1) - 0
M(2) - -1
OnError - Program+TestException - 2
M(3) - 0
y(0) is not Changed = System.Int32[]
--- Test - 4
M(0) - 0
M(1) - 0
M(2) - 0
M(3) - 0
y(0) is Changed = System.Int32[]
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_Erase_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, 0}),
                      ({0, 1, -1, 0}),
                      ({0, 1, 1, 0}),
                      ({0, 1, 0, 0}),
                      ({0, 0, -1, 0}),
                      ({0, 0, 1, 0}),
                      ({0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            Dim x = {({1})}

            Dim y = {({1})}

            M("0")
            Erase x(M("1")), y(M("2"))
            M("3")

            System.Console.WriteLine("x(0) is {0}Nothing", If(x(0) IsNot Nothing, "Not ", ""))
            System.Console.WriteLine("y(0) is {0}Nothing", If(y(0) IsNot Nothing, "Not ", ""))

            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - -1
OnError - Program+TestException - 2
M(3) - 0
x(0) is Not Nothing
y(0) is Not Nothing
--- Test - 1
M(0) - 0
M(1) - 1
OnError - System.IndexOutOfRangeException
M(2) - -1
OnError - Program+TestException - 2
M(3) - 0
x(0) is Not Nothing
y(0) is Not Nothing
--- Test - 2
M(0) - 0
M(1) - 1
OnError - System.IndexOutOfRangeException
M(2) - 1
OnError - System.IndexOutOfRangeException
M(3) - 0
x(0) is Not Nothing
y(0) is Not Nothing
--- Test - 3
M(0) - 0
M(1) - 1
OnError - System.IndexOutOfRangeException
M(2) - 0
M(3) - 0
x(0) is Not Nothing
y(0) is Nothing
--- Test - 4
M(0) - 0
M(1) - 0
M(2) - -1
OnError - Program+TestException - 2
M(3) - 0
x(0) is Nothing
y(0) is Not Nothing
--- Test - 5
M(0) - 0
M(1) - 0
M(2) - 1
OnError - System.IndexOutOfRangeException
M(3) - 0
x(0) is Nothing
y(0) is Not Nothing
--- Test - 6
M(0) - 0
M(1) - 0
M(2) - 0
M(3) - 0
x(0) is Nothing
y(0) is Nothing
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_Erase_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, 0}),
                      ({0, 1, 0}),
                      ({0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            Dim x = {({1})}

            M("0")
            Erase x(M("1"))
            M("2")

            System.Console.WriteLine("x(0) is {0}Nothing", If(x(0) IsNot Nothing, "Not ", ""))

            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(1) - -1
OnError - Program+TestException - 1
M(2) - 0
x(0) is Not Nothing
--- Test - 1
M(0) - 0
M(1) - 1
OnError - System.IndexOutOfRangeException
M(2) - 0
x(0) is Not Nothing
--- Test - 2
M(0) - 0
M(1) - 0
M(2) - 0
x(0) is Nothing
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_Goto_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({1, -1, 0}),
                      ({1, 1, 0}),
                      ({0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            If M("0") <> 0 Then
                M("1")
                GoTo Label
            End If

            M("2")
Label:
            M("3")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - 1
M(1) - -1
OnError - Program+TestException - 1
M(3) - 0
--- Test - 1
M(0) - 1
M(1) - 1
M(3) - 0
--- Test - 2
M(0) - 0
M(2) - 0
M(3) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_ExitSelect_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({0, 1, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            Select Case M("0")
                Case 0
                    If M("1") <> 0 Then
                        M("2")
                        Exit Select
                    End If

                    M("3")
            End Select

            M("4")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - 0
M(1) - 1
M(2) - -1
OnError - Program+TestException - 2
M(4) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_ContinueWhile_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({1, 1, -1, 0, 0}),
                      ({1, 1, -1, -1, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance
        Sub Test()
            On Error GoTo OnError

            While M("0") <> 0
                If M("1") <> 0 Then
                    M("2")
                    Continue While
                End If

                M("3")
            End While

            M("4")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.DebugExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.TestInstance.Test",
            <![CDATA[{
  // Code size      375 (0x177)
  .maxstack  4
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  System.Exception V_3, //ex
  Boolean V_4)
  IL_0000:  nop
  .try
{
  IL_0001:  nop
  IL_0002:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0007:  nop
  IL_0008:  ldc.i4.2
  IL_0009:  stloc.0
  IL_000a:  br.s       IL_0040
  IL_000c:  ldc.i4.4
  IL_000d:  stloc.2
  IL_000e:  ldstr      "1"
  IL_0013:  call       "Function Program.M(String) As Integer"
  IL_0018:  ldc.i4.0
  IL_0019:  ceq
  IL_001b:  stloc.s    V_4
  IL_001d:  ldloc.s    V_4
  IL_001f:  brtrue.s   IL_0031
  IL_0021:  ldc.i4.5
  IL_0022:  stloc.2
  IL_0023:  ldstr      "2"
  IL_0028:  call       "Function Program.M(String) As Integer"
  IL_002d:  pop
  IL_002e:  br.s       IL_0040
  IL_0030:  nop
  IL_0031:  nop
  IL_0032:  ldc.i4.8
  IL_0033:  stloc.2
  IL_0034:  ldstr      "3"
  IL_0039:  call       "Function Program.M(String) As Integer"
  IL_003e:  pop
  IL_003f:  nop
  IL_0040:  ldc.i4.3
  IL_0041:  stloc.2
  IL_0042:  ldstr      "0"
  IL_0047:  call       "Function Program.M(String) As Integer"
  IL_004c:  ldc.i4.0
  IL_004d:  cgt.un
  IL_004f:  stloc.s    V_4
  IL_0051:  ldloc.s    V_4
  IL_0053:  brtrue.s   IL_000c
  IL_0055:  ldc.i4.s   10
  IL_0057:  stloc.2
  IL_0058:  ldstr      "4"
  IL_005d:  call       "Function Program.M(String) As Integer"
  IL_0062:  pop
  IL_0063:  br.s       IL_00cc
  IL_0065:  nop
  IL_0066:  ldc.i4.s   12
  IL_0068:  stloc.2
  IL_0069:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_006e:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_0073:  stloc.3
  IL_0074:  ldc.i4.s   13
  IL_0076:  stloc.2
  IL_0077:  ldstr      "OnError - {0}{1}"
  IL_007c:  ldloc.3
  IL_007d:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_0082:  ldloc.3
  IL_0083:  isinst     "Program.TestException"
  IL_0088:  brtrue.s   IL_0091
  IL_008a:  ldsfld     "String.Empty As String"
  IL_008f:  br.s       IL_00a6
  IL_0091:  ldstr      " - {0}"
  IL_0096:  ldloc.3
  IL_0097:  castclass  "Program.TestException"
  IL_009c:  ldfld      "Program.TestException.Value As String"
  IL_00a1:  call       "Function String.Format(String, Object) As String"
  IL_00a6:  call       "Sub System.Console.WriteLine(String, Object, Object)"
  IL_00ab:  nop
  IL_00ac:  ldc.i4.s   14
  IL_00ae:  stloc.2
  IL_00af:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b4:  nop
  IL_00b5:  ldloc.1
  IL_00b6:  ldc.i4.0
  IL_00b7:  cgt.un
  IL_00b9:  stloc.s    V_4
  IL_00bb:  ldloc.s    V_4
  IL_00bd:  brtrue.s   IL_00ca
  IL_00bf:  ldc.i4     0x800a0014
  IL_00c4:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c9:  throw
  IL_00ca:  br.s       IL_00d1
  IL_00cc:  leave      IL_0166
  IL_00d1:  ldloc.1
  IL_00d2:  ldc.i4.1
  IL_00d3:  add
  IL_00d4:  ldc.i4.0
  IL_00d5:  stloc.1
  IL_00d6:  switch    (
  IL_011b,
  IL_0002,
  IL_000a,
  IL_0040,
  IL_000c,
  IL_0021,
  IL_002e,
  IL_0030,
  IL_0032,
  IL_003f,
  IL_0055,
  IL_0063,
  IL_0066,
  IL_0074,
  IL_00ac,
  IL_00cc)
  IL_011b:  leave.s    IL_015b
  IL_011d:  ldloc.2
  IL_011e:  stloc.1
  IL_011f:  ldloc.0
  IL_0120:  ldc.i4.s   -2
  IL_0122:  bgt.s      IL_0127
  IL_0124:  ldc.i4.1
  IL_0125:  br.s       IL_0128
  IL_0127:  ldloc.0
  IL_0128:  switch    (
  IL_0139,
  IL_00d1,
  IL_0065)
  IL_0139:  leave.s    IL_015b
}
  filter
{
  IL_013b:  isinst     "System.Exception"
  IL_0140:  ldnull
  IL_0141:  cgt.un
  IL_0143:  ldloc.0
  IL_0144:  ldc.i4.0
  IL_0145:  cgt.un
  IL_0147:  and
  IL_0148:  ldloc.1
  IL_0149:  ldc.i4.0
  IL_014a:  ceq
  IL_014c:  and
  IL_014d:  endfilter
}  // end filter
{  // handler
  IL_014f:  castclass  "System.Exception"
  IL_0154:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0159:  leave.s    IL_011d
}
  IL_015b:  ldc.i4     0x800a0033
  IL_0160:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0165:  throw
  IL_0166:  ldloc.1
  IL_0167:  ldc.i4.0
  IL_0168:  ceq
  IL_016a:  stloc.s    V_4
  IL_016c:  ldloc.s    V_4
  IL_016e:  brtrue.s   IL_0176
  IL_0170:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0175:  nop
  IL_0176:  ret
}]]>)




            '            Dim expected =
            '            <![CDATA[
            '--- Test - 0
            'M(0) - 1
            'M(1) - 1
            'M(2) - -1
            'OnError - Program+TestException - 2
            'M(0) - 0
            'M(4) - 0
            '--- Test - 1
            'M(0) - 1
            'M(1) - 1
            'M(2) - -1
            'OnError - Program+TestException - 2
            'M(0) - -1
            'OnError - Program+TestException - 0
            'M(1) - 0
            'M(3) - 0
            'M(0) - 0
            'M(4) - 0
            ']]>

            compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Program.TestInstance.Test",
            <![CDATA[{
  // Code size      330 (0x14a)
  .maxstack  4
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                System.Exception V_3) //ex
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  br.s       IL_0033
  IL_0009:  ldc.i4.4
  IL_000a:  stloc.2
  IL_000b:  ldstr      "1"
  IL_0010:  call       "Function Program.M(String) As Integer"
  IL_0015:  brfalse.s  IL_0026
  IL_0017:  ldc.i4.5
  IL_0018:  stloc.2
  IL_0019:  ldstr      "2"
  IL_001e:  call       "Function Program.M(String) As Integer"
  IL_0023:  pop
  IL_0024:  br.s       IL_0033
  IL_0026:  ldc.i4.8
  IL_0027:  stloc.2
  IL_0028:  ldstr      "3"
  IL_002d:  call       "Function Program.M(String) As Integer"
  IL_0032:  pop
  IL_0033:  ldc.i4.3
  IL_0034:  stloc.2
  IL_0035:  ldstr      "0"
  IL_003a:  call       "Function Program.M(String) As Integer"
  IL_003f:  brtrue.s   IL_0009
  IL_0041:  ldc.i4.s   10
  IL_0043:  stloc.2
  IL_0044:  ldstr      "4"
  IL_0049:  call       "Function Program.M(String) As Integer"
  IL_004e:  pop
    IL_004f:  leave      IL_0141
  IL_0054:  ldc.i4.s   12
  IL_0056:  stloc.2
  IL_0057:  call       "Function Microsoft.VisualBasic.Information.Err() As Microsoft.VisualBasic.ErrObject"
  IL_005c:  callvirt   "Function Microsoft.VisualBasic.ErrObject.GetException() As System.Exception"
  IL_0061:  stloc.3
  IL_0062:  ldc.i4.s   13
  IL_0064:  stloc.2
  IL_0065:  ldstr      "OnError - {0}{1}"
  IL_006a:  ldloc.3
  IL_006b:  callvirt   "Function System.Exception.GetType() As System.Type"
  IL_0070:  ldloc.3
  IL_0071:  isinst     "Program.TestException"
  IL_0076:  brtrue.s   IL_007f
  IL_0078:  ldsfld     "String.Empty As String"
  IL_007d:  br.s       IL_0094
  IL_007f:  ldstr      " - {0}"
  IL_0084:  ldloc.3
  IL_0085:  castclass  "Program.TestException"
  IL_008a:  ldfld      "Program.TestException.Value As String"
  IL_008f:  call       "Function String.Format(String, Object) As String"
  IL_0094:  call       "Sub System.Console.WriteLine(String, Object, Object)"
  IL_0099:  ldc.i4.s   14
  IL_009b:  stloc.2
  IL_009c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a1:  ldloc.1
  IL_00a2:  brtrue.s   IL_00b4
  IL_00a4:  ldc.i4     0x800a0014
  IL_00a9:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ae:  throw
    IL_00af:  leave      IL_0141
  IL_00b4:  ldloc.1
  IL_00b5:  ldc.i4.1
  IL_00b6:  add
  IL_00b7:  ldc.i4.0
  IL_00b8:  stloc.1
  IL_00b9:  switch    (
  IL_00fe,
  IL_0000,
  IL_0033,
  IL_0033,
  IL_0009,
  IL_0017,
  IL_0033,
  IL_0026,
  IL_0026,
  IL_0033,
  IL_0041,
  IL_00af,
  IL_0054,
  IL_0062,
  IL_0099,
  IL_00af)
    IL_00fe:  leave.s    IL_0136
  IL_0100:  ldloc.2
  IL_0101:  stloc.1
  IL_0102:  ldloc.0
    IL_0103:  switch    (
        IL_0114,
  IL_00b4,
  IL_0054)
    IL_0114:  leave.s    IL_0136
}
  filter
{
    IL_0116:  isinst     "System.Exception"
    IL_011b:  ldnull
    IL_011c:  cgt.un
    IL_011e:  ldloc.0
    IL_011f:  ldc.i4.0
    IL_0120:  cgt.un
    IL_0122:  and
    IL_0123:  ldloc.1
    IL_0124:  ldc.i4.0
    IL_0125:  ceq
    IL_0127:  and
    IL_0128:  endfilter
}  // end filter
{  // handler
    IL_012a:  castclass  "System.Exception"
    IL_012f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
    IL_0134:  leave.s    IL_0100
}
  IL_0136:  ldc.i4     0x800a0033
  IL_013b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0140:  throw
  IL_0141:  ldloc.1
  IL_0142:  brfalse.s  IL_0149
  IL_0144:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0149:  ret
}]]>)


        End Sub

        <Fact()>
        Public Sub Resume_in_AddRaiseRemove_Event_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({-1, -1, -1, -1, -1, 0}),
                      ({0, -1, -1, -1, -1, 0}),
                      ({0, 0, -1, -1, -1, -1, 0}),
                      ({0, 0, 0, -1, -1, -1, 0}),
                      ({0, 0, 0, 0, -1, -1, 0}),
                      ({0, 0, 0, 0, 0, -1, 0}),
                      ({0, 0, 0, 0, 0, 0, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}", e.GetType())
            End Try
        Next

    End Sub

    Class TestInstance

        Private Event m_Event As Action

        Sub Test()
            On Error GoTo OnError

            M("0")
            AddHandler m_Event, GetDelegate("1")
            M("2")
            RaiseEvent m_Event()
            M("4")
            RemoveHandler m_Event, GetDelegate("5")
            M("6")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function GetDelegate(tag As String) As Action
        M("GetDelegate " & tag)
        Return AddressOf EventSub
    End Function

    Sub EventSub()
        M("Event Sub")
    End Sub

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(0) - -1
OnError - Program+TestException - 0
M(GetDelegate 1) - -1
OnError - Program+TestException - GetDelegate 1
M(2) - -1
OnError - Program+TestException - 2
M(4) - -1
OnError - Program+TestException - 4
M(GetDelegate 5) - -1
OnError - Program+TestException - GetDelegate 5
M(6) - 0
--- Test - 1
M(0) - 0
M(GetDelegate 1) - -1
OnError - Program+TestException - GetDelegate 1
M(2) - -1
OnError - Program+TestException - 2
M(4) - -1
OnError - Program+TestException - 4
M(GetDelegate 5) - -1
OnError - Program+TestException - GetDelegate 5
M(6) - 0
--- Test - 2
M(0) - 0
M(GetDelegate 1) - 0
M(2) - -1
OnError - Program+TestException - 2
M(Event Sub) - -1
OnError - Program+TestException - Event Sub
M(4) - -1
OnError - Program+TestException - 4
M(GetDelegate 5) - -1
OnError - Program+TestException - GetDelegate 5
M(6) - 0
--- Test - 3
M(0) - 0
M(GetDelegate 1) - 0
M(2) - 0
M(Event Sub) - -1
OnError - Program+TestException - Event Sub
M(4) - -1
OnError - Program+TestException - 4
M(GetDelegate 5) - -1
OnError - Program+TestException - GetDelegate 5
M(6) - 0
--- Test - 4
M(0) - 0
M(GetDelegate 1) - 0
M(2) - 0
M(Event Sub) - 0
M(4) - -1
OnError - Program+TestException - 4
M(GetDelegate 5) - -1
OnError - Program+TestException - GetDelegate 5
M(6) - 0
--- Test - 5
M(0) - 0
M(GetDelegate 1) - 0
M(2) - 0
M(Event Sub) - 0
M(4) - 0
M(GetDelegate 5) - -1
OnError - Program+TestException - GetDelegate 5
M(6) - 0
--- Test - 6
M(0) - 0
M(GetDelegate 1) - 0
M(2) - 0
M(Event Sub) - 0
M(4) - 0
M(GetDelegate 5) - 0
M(6) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_ObjectConstruction_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({0, 0, 0, 0, 0, 0, 0}),
                      ({-1}),
                      ({0, -1}),
                      ({0, 0, -1}),
                      ({0, 0, 0, -1, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Dim x = New TestInstance(0)
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}{1}", e.GetType(),
                                         If(TypeOf e Is TestException, String.Format(" - {0}", DirectCast(e, TestException).Value), String.Empty))
            End Try
        Next

    End Sub

    Class TestInstanceBase
        Sub New()
            M("TestInstanceBase.New")
        End Sub
    End Class

    Class TestInstance
        Inherits TestInstanceBase

        Dim m_x As Integer = M("X")
        Dim m_y As Integer = M("Y")

        Sub New()
            MyBase.New()
            On Error GoTo OnError
            M("TestInstance.New 1")
            M("TestInstance.New 2")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("TestInstance.New OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub

        Sub New(x As Integer)
            Me.New()
            On Error GoTo OnError

            M("TestInstance.New(x As Integer) 1")
            M("TestInstance.New(x As Integer) 2")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("TestInstance.New(x As Integer) OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(TestInstanceBase.New) - 0
M(X) - 0
M(Y) - 0
M(TestInstance.New 1) - 0
M(TestInstance.New 2) - 0
M(TestInstance.New(x As Integer) 1) - 0
M(TestInstance.New(x As Integer) 2) - 0
--- Test - 1
M(TestInstanceBase.New) - -1
Exception - Program+TestException - TestInstanceBase.New
--- Test - 2
M(TestInstanceBase.New) - 0
M(X) - -1
Exception - Program+TestException - X
--- Test - 3
M(TestInstanceBase.New) - 0
M(X) - 0
M(Y) - -1
Exception - Program+TestException - Y
--- Test - 4
M(TestInstanceBase.New) - 0
M(X) - 0
M(Y) - 0
M(TestInstance.New 1) - -1
TestInstance.New OnError - Program+TestException - TestInstance.New 1
M(TestInstance.New 2) - 0
M(TestInstance.New(x As Integer) 1) - 0
M(TestInstance.New(x As Integer) 2) - 0
--- Test - 5
M(TestInstanceBase.New) - 0
M(X) - 0
M(Y) - 0
M(TestInstance.New 1) - 0
M(TestInstance.New 2) - 0
M(TestInstance.New(x As Integer) 1) - -1
TestInstance.New(x As Integer) OnError - Program+TestException - TestInstance.New(x As Integer) 1
M(TestInstance.New(x As Integer) 2) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_ObjectConstruction_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({0, 0, 0, 0, 0}),
                      ({-1}),
                      ({0, -1}),
                      ({0, 0, -1}),
                      ({0, 0, 0, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Dim x = New TestInstance()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}{1}", e.GetType(),
                                         If(TypeOf e Is TestException, String.Format(" - {0}", DirectCast(e, TestException).Value), String.Empty))
            End Try
        Next

    End Sub

    Class TestInstanceBase
        Sub New()
            M("TestInstanceBase.New")
        End Sub
    End Class

    Class TestInstance
        Inherits TestInstanceBase

        Dim m_x As Integer = M("X")
        Dim m_y As Integer = M("Y")

        Sub New()
            On Error GoTo OnError
            M("TestInstance.New 1")
            M("TestInstance.New 2")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("TestInstance.New OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(TestInstanceBase.New) - 0
M(X) - 0
M(Y) - 0
M(TestInstance.New 1) - 0
M(TestInstance.New 2) - 0
--- Test - 1
M(TestInstanceBase.New) - -1
Exception - Program+TestException - TestInstanceBase.New
--- Test - 2
M(TestInstanceBase.New) - 0
M(X) - -1
Exception - Program+TestException - X
--- Test - 3
M(TestInstanceBase.New) - 0
M(X) - 0
M(Y) - -1
Exception - Program+TestException - Y
--- Test - 4
M(TestInstanceBase.New) - 0
M(X) - 0
M(Y) - 0
M(TestInstance.New 1) - -1
TestInstance.New OnError - Program+TestException - TestInstance.New 1
M(TestInstance.New 2) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_TypeInitialization_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({0, 0, -1, 0, -1})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Dim x As New TestInstance()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}{1}", e.GetType(),
                                         If(TypeOf e Is TestException, String.Format(" - {0}", DirectCast(e, TestException).Value), String.Empty))
            End Try
        Next

    End Sub

    Class TestInstanceBase
        Shared m_v As Integer = M("V")
        Shared m_w As Integer = M("W")

        Shared Sub New()
            On Error GoTo OnError
            M("TestInstanceBase.New 1")
            M("TestInstanceBase.New 2")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("TestInstanceBase.New OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Class TestInstance
        Inherits TestInstanceBase

        Shared m_x As Integer = M("X")
        Shared m_y As Integer = M("Y")

        Shared Sub New()
            On Error GoTo OnError
            M("TestInstance.New 1")
            M("TestInstance.New 2")
            Return
OnError:
            Dim ex = Microsoft.VisualBasic.Information.Err.GetException()
            System.Console.WriteLine("TestInstance.New OnError - {0}{1}",
                                     ex.GetType(),
                                     If(TypeOf ex Is TestException, String.Format(" - {0}", DirectCast(ex, TestException).Value), String.Empty))
            Resume Next
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(X) - 0
M(Y) - 0
M(TestInstance.New 1) - -1
TestInstance.New OnError - Program+TestException - TestInstance.New 1
M(TestInstance.New 2) - 0
M(V) - -1
Exception - System.TypeInitializationException
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Program
    Private state As Integer()
    Private current As Integer

    Sub Main()
        Dim states = {({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}),
                      ({-1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0}),
                      ({0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0})
                     }

        For i As Integer = 0 To states.Length - 1
            System.Console.WriteLine("--- Test - {0}", i)
            state = states(i)
            current = 0
            Try
                Call (New TestInstance()).Test()
            Catch e As Exception
                System.Console.WriteLine("Exception - {0}{1}", e.GetType(),
                                         If(TypeOf e Is TestException, String.Format(" - {0}", DirectCast(e, TestException).Value), String.Empty))
            End Try
        Next

    End Sub


    Class TestInstance

        Sub Test()
            On Error Resume Next
            M("1")
            Dim x = Function() M("6")
            M("2")
            Dim y = Function()
                        M("8")
                        Return M("9")
                    End Function
            M("3")
            Dim v = Sub() M("11")
            M("4")
            Dim w = Sub()
                        M("13")
                        M("14")
                    End Sub
            M("5")

            If x IsNot Nothing Then
                x()
            End If
            M("7")
            If y IsNot Nothing Then
                y()
            End If
            M("10")
            If v IsNot Nothing Then
                v()
            End If
            M("12")
            If w IsNot Nothing Then
                w()
            End If
            M("15")
        End Sub
    End Class

    Function M(tag As String) As Integer
        If current >= state.Length Then
            System.Console.WriteLine("Test issue.")
            Return 0
        End If

        Dim val = state(current)
        current += 1
        System.Console.WriteLine("M({0}) - {1}", tag, val)
        If val = -1 Then
            Throw New TestException(tag)
        End If

        Return val
    End Function

    Class TestException
        Inherits Exception
        Public ReadOnly Value As String

        Sub New(x As String)
            Value = x
        End Sub
    End Class
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
--- Test - 0
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 1
M(1) - -1
M(2) - -1
M(3) - -1
M(4) - -1
M(5) - -1
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 2
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - -1
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 3
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - -1
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 4
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - -1
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 5
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - -1
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 6
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - -1
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 7
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - -1
M(12) - 0
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 8
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - 0
M(12) - -1
M(13) - 0
M(14) - 0
M(15) - 0
--- Test - 9
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - -1
M(15) - 0
--- Test - 10
M(1) - 0
M(2) - 0
M(3) - 0
M(4) - 0
M(5) - 0
M(6) - 0
M(7) - 0
M(8) - 0
M(9) - 0
M(10) - 0
M(11) - 0
M(12) - 0
M(13) - 0
M(14) - -1
M(15) - 0
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

            compilation = compilation.WithOptions(TestOptions.DebugExe)

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Sub test1()
        Resume ' 1

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test1()
        Dim x4 = Sub()
                     test1()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test1
Label:
    End Sub

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC36595: Method cannot contain both a 'Resume' statement and a definition of a variable that is used in a lambda or query expression.
        Resume ' 1
        ~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_2()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Sub test2()
        Resume Next ' 2

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test2()
        Dim x4 = Sub()
                     test2()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test2
Label:
    End Sub

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC36595: Method cannot contain both a 'Resume Next' statement and a definition of a variable that is used in a lambda or query expression.
        Resume Next ' 2
        ~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_3()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Sub test3()
        On Error Resume Next ' 3

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test3()
        Dim x4 = Sub()
                     test3()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test3
Label:
    End Sub

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC36595: Method cannot contain both a 'On Error Resume Next' statement and a definition of a variable that is used in a lambda or query expression.
        On Error Resume Next ' 3
        ~~~~~~~~~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_4()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Sub test4()
        On Error Goto Label ' 4

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test4()
        Dim x4 = Sub()
                     test4()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test4
Label:
    End Sub

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC36597: 'On Error Goto Label' is not valid because 'Label' is inside a scope that defines a variable that is used in a lambda or query expression.
        On Error Goto Label ' 4
        ~~~~~~~~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_5()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    SHared Sub test0()
        Resume
        Resume Next
        Resume Label
        On Error Resume Next
        On Error GoTo 0
        On Error GoTo -1
        On Error Goto Label

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test0()
        Dim x4 = Sub()
                     test0()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test0
Label:
    End Sub

    Sub test5()
        On Error Goto 0 ' 5

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test5()
        Dim x4 = Sub()
                     test5()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test5
Label:
    End Sub

    Sub test6()
        On Error Goto -1 ' 6

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test6()
        Dim x4 = Sub()
                     test6()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test6
Label:
    End Sub

    Sub test7()
        Resume Label ' 7

        Dim x1 = Function() 1
        Dim x2 = Function()
                     Return 1
                 End Function
        Dim x3 = Sub() test7()
        Dim x4 = Sub()
                     test7()
                 End Sub

        Dim x5 As System.Action(Of Integer) = AddressOf test7
Label:
    End Sub

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_6()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Sub test0()
        On Error Resume Next

        If Expr1() <> 0 Then
            On Error GoTo 0
            Dim x As Integer = 9 ' System.NullReferenceException - closure is not initialized.
            Dim y = Function() x
            Dim z = y()
        End If
    End Sub

    Function Expr1() As Integer
        Throw New NotImplementedException()
    End Function

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC36595: Method cannot contain both a 'On Error Resume Next' statement and a definition of a variable that is used in a lambda or query expression.
        On Error Resume Next
        ~~~~~~~~~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_7()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Shared Sub Main()
        On Error Goto Label

        If Expr1() <> 0 Then
            Dim x As Integer = 9 
            Dim y = Function() x
            System.Console.WriteLine(y())
        End If

Label:
    End Sub

    Shared Function Expr1() As Integer
        Throw New NotImplementedException()
    End Function

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_8()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Shared Sub Main()
        On Error Goto Label

        If Expr1() <> 0 Then
Label2:
            Dim x As Integer = 9 
            Dim y = Function() x
            System.Console.WriteLine(y())
        End If

Label:
        Resume Label2
    End Sub

    Shared Function Expr1() As Integer
        Throw New NotImplementedException()
    End Function

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC36597: 'Resume Label2' is not valid because 'Label2' is inside a scope that defines a variable that is used in a lambda or query expression.
        Resume Label2
        ~~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub Resume_in_Lambda_9()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1

    Shared Sub Main()
        On Error Goto Label2

        If Expr1() <> 0 Then
Label2:
            Dim x As Integer = 9 
            Dim y = Function() x
            System.Console.WriteLine(y())
        End If
    End Sub

    Shared Function Expr1() As Integer
        Throw New NotImplementedException()
    End Function

End Class
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC36597: 'On Error Goto Label2' is not valid because 'Label2' is inside a scope that defines a variable that is used in a lambda or query expression.
        On Error Goto Label2
        ~~~~~~~~~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub ErrorStatement_0()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System
Imports Microsoft.VisualBasic.Constants

Module Program
    Sub Main()
        Try
            Error 20
        Catch ex As Exception
            System.Console.WriteLine(ex.ToString().Split(vbCr & vbLf)(0))
        End Try
    End Sub
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
System.InvalidOperationException: Resume without error.
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub

        <Fact()>
        Public Sub ErrorStatement_1()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System
Imports Microsoft.VisualBasic.Constants

Module Program
    Sub Main()
        Error New NotImplementedException()
    End Sub
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC30311: Value of type 'NotImplementedException' cannot be converted to 'Integer'.
        Error New NotImplementedException()
              ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub ErrorStatement_2()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class Program
    Shared Sub Main()
        Error 20
    End Sub
End Class
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlib(source, options:=TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC35000: Requested operation is not available because the runtime library function 'Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError' is not defined.
        Error 20
        ~~~~~~~~
</expected>)
        End Sub

        <Fact()>
        Public Sub UnusedLocal()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1


    Sub test1()
        Dim x As Object ' 1
        Static y As Object ' 1
        Const z As String = "a" ' 1

        Return
Label:
    End Sub

    Sub test2()
        Dim x As Object ' 2
        Static y As Object ' 2
        Const z As String = "a" ' 2

        On Error GoTo 0
        Return
Label:
    End Sub

    Sub test3()
        Dim x As Object ' 3
        Static y As Object ' 3
        Const z As String = "a" ' 3

        On Error GoTo -1
        Return
Label:
    End Sub

    Sub test4()
        Dim x As Object ' 4
        Static y As Object ' 4
        Const z As String = "a" ' 4

        On Error GoTo Label
        Return
Label:
    End Sub

    Sub test5()
        Dim x As Object ' 5
        Static y As Object ' 5
        Const z As String = "a" ' 5

        On Error Resume Next
        Return
Label:
    End Sub

    Sub test6()
        Dim x As Object ' 6
        Static y As Object ' 6
        Const z As String = "a" ' 6

        Resume
        Return
Label:
    End Sub

    Sub test7()
        Dim x As Object ' 7
        Static y As Object ' 7
        Const z As String = "a" ' 7

        Resume Next
        Return
Label:
    End Sub

    Sub test8()
        Dim x As Object ' 8
        Static y As Object ' 8
        Const z As String = "a" ' 8

        Resume Label
        Return
Label:
    End Sub
End Class
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC42024: Unused local variable: 'x'.
        Dim x As Object ' 1
            ~
BC42024: Unused local variable: 'y'.
        Static y As Object ' 1
               ~
BC42099: Unused local constant: 'z'.
        Const z As String = "a" ' 1
              ~
</expected>)
        End Sub

        <Fact()>
        Public Sub MissingReturn()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Class TestOnError1


    Function test1() As Object
Label:
    End Function ' 1

    Function test2() As Object

        On Error GoTo 0
Label:
    End Function ' 2

    Function test3() As Object

        On Error GoTo -1
Label:
    End Function ' 3

    Function test4() As Object

        On Error GoTo Label
Label:
    End Function ' 4

    Function test5() As Object

        On Error Resume Next
Label:
    End Function ' 5

    Function test6() As Object

        Resume
Label:
    End Function ' 6

    Function test7() As Object

        Resume Next
Label:
    End Function ' 7

    Function test8() As Object

        Resume Label
Label:
    End Function ' 8
End Class
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseDll)

            AssertTheseDiagnostics(compilation,
<expected>
BC42105: Function 'test1' doesn't return a value on all code paths. A null reference exception could occur at run time when the result is used.
    End Function ' 1
    ~~~~~~~~~~~~
</expected>)
        End Sub

        <Fact(), WorkItem(547095, "DevDiv")>
        Public Sub Bug17937()
            Dim source =
<compilation name="AscW">
    <file name="a.vb">
        <![CDATA[
Imports System
Module Program
    Sub Main()
        Dim x As New Object
        On Error GoTo trap

        Throw New Exception
        Console.WriteLine("after throw")
        Exit Sub
trap:
        SyncLock x
            Console.WriteLine("hello")
            Resume Next
        End SyncLock
    End Sub
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim expected =
            <![CDATA[
hello
after throw
]]>

            CompileAndVerify(compilation, expectedOutput:=expected)

        End Sub


        <Fact()>
        Public Sub SingleLabelErrorHandler()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Module1
    Sub Main()
        Dim sPath As String = ""
        sPath = "Test2"
        On Error GoTo foo
        Error 5
        Console.WriteLine(sPath)
        Exit Sub
fooReturn:
        sPath &= "fooReturn"
        Console.WriteLine(sPath)
        Exit Sub
foo:
        sPath &= "foo"
        Resume Next 'Resume Next
    End Sub
End Module

        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="Test2foo")

            compilation = compilation.WithOptions(TestOptions.DebugExe)
            CompileAndVerify(compilation, expectedOutput:="Test2foo")
        End Sub

        <Fact()>
        Public Sub MultipleLabelErrorHandler()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Module1
    Sub Main()
        Dim sPath As String = ""
        sPath = "Test3"
        On Error GoTo foo
        Error 5
        Exit Sub
fooReturn:
        sPath &= "fooReturn"
        Console.WriteLine(sPath)
        Exit Sub
foo:
        sPath &= "foo"
        Resume fooReturn 'Resume with Label
    End Sub

End Module

        ]]>
    </file>
</compilation>

            'Just to verify with/without optimizations
            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="Test3foofooReturn")

            compilation = compilation.WithOptions(TestOptions.DebugExe)
            CompileAndVerify(compilation, expectedOutput:="Test3foofooReturn")
        End Sub

        <Fact()>
        Public Sub OnError_In_Single_Method_With_Resume_Next()
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Module1
    Sub Main()
        Dim sPath As String = ""
        sPath = "Start"
        On Error Resume Next
        Error 5
        Console.WriteLine(sPath)
        Console.WriteLine("End")
        Exit Sub
fooReturn:
        sPath &= "fooReturn"
        Console.WriteLine(sPath)
        Exit Sub
foo:
        sPath &= "foo"
        Resume fooReturn 'Resume with Line    
    End Sub
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[Start
End]]>)

            compilation = compilation.WithOptions(TestOptions.DebugExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[Start
End]]>)
        End Sub

        <Fact()>
        Public Sub OnError_In_Single_Method_With_Goto_0()
            'IL Baseline check only as it will fail with an unhandled exception at runtime
            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
Imports System

Module Module1
    Sub Main()
        Dim sPath As String = ""
        sPath = "Test4"
        On Error GoTo 0
        Error 5 '<- Will error here on undhandled exception

        Console.WriteLine(sPath)
        Exit Sub
fooReturn:
        sPath &= "fooReturn"
        Console.WriteLine(sPath)
        Exit Sub
foo:
        sPath &= "foo"
        Resume fooReturn 'Resume with Line    
    End Sub
End Module
        ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size       87 (0x57)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                String V_2) //sPath
  .try
{
  IL_0000:  ldstr      ""
  IL_0005:  stloc.2
  IL_0006:  ldstr      "Test4"
  IL_000b:  stloc.2
  IL_000c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0011:  ldc.i4.0
  IL_0012:  stloc.0
  IL_0013:  ldc.i4.5
  IL_0014:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0019:  throw
  IL_001a:  ldc.i4.m1
  IL_001b:  stloc.1
  IL_001c:  ldloc.0
  IL_001d:  switch    (
  IL_002a,
  IL_002a)
  IL_002a:  leave.s    IL_004c
}
  filter
{
  IL_002c:  isinst     "System.Exception"
  IL_0031:  ldnull
  IL_0032:  cgt.un
  IL_0034:  ldloc.0
  IL_0035:  ldc.i4.0
  IL_0036:  cgt.un
  IL_0038:  and
  IL_0039:  ldloc.1
  IL_003a:  ldc.i4.0
  IL_003b:  ceq
  IL_003d:  and
  IL_003e:  endfilter
}  // end filter
{  // handler
  IL_0040:  castclass  "System.Exception"
  IL_0045:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_004a:  leave.s    IL_001a
}
  IL_004c:  ldc.i4     0x800a0033
  IL_0051:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0056:  throw
}]]>)
        End Sub

        <Fact()>
        Public Sub OnError_In_Single_Method_With_Goto_Minus1()
            'IL Baseline check only as it will fail with an unhandled exception at runtime

            Dim source =
<compilation name="ErrorHandling">
    <file name="a.vb">
        <![CDATA[
        Imports System

Module Module1
    Public Sub Main()
        Dim sPath As String = ""
        sPath = "Test4"
        On Error GoTo -1
        Error 5 '<- will error here as unhandled exception
        Console.WriteLine(sPath)
        Exit Sub
fooReturn:
        sPath &= "fooReturn"
        Console.WriteLine(sPath)
        Exit Sub
foo:
        sPath &= "foo"
        Resume fooReturn 'Resume with Line    
    End Sub
End Module
                ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size       87 (0x57)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                String V_2) //sPath
  .try
{
  IL_0000:  ldstr      ""
  IL_0005:  stloc.2
  IL_0006:  ldstr      "Test4"
  IL_000b:  stloc.2
  IL_000c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0011:  ldc.i4.0
  IL_0012:  stloc.1
  IL_0013:  ldc.i4.5
  IL_0014:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0019:  throw
  IL_001a:  ldc.i4.m1
  IL_001b:  stloc.1
  IL_001c:  ldloc.0
  IL_001d:  switch    (
  IL_002a,
  IL_002a)
  IL_002a:  leave.s    IL_004c
}
  filter
{
  IL_002c:  isinst     "System.Exception"
  IL_0031:  ldnull
  IL_0032:  cgt.un
  IL_0034:  ldloc.0
  IL_0035:  ldc.i4.0
  IL_0036:  cgt.un
  IL_0038:  and
  IL_0039:  ldloc.1
  IL_003a:  ldc.i4.0
  IL_003b:  ceq
  IL_003d:  and
  IL_003e:  endfilter
}  // end filter
{  // handler
  IL_0040:  castclass  "System.Exception"
  IL_0045:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_004a:  leave.s    IL_001a
}
  IL_004c:  ldc.i4     0x800a0033
  IL_0051:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0056:  throw
}]]>)
        End Sub

        <Fact()>
        Sub Multi_OnError_In_Single_Method_1()
            'This will work because of the correct reset of event handler and using Throw rather than Error 
            'statement to trigger the Errors
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        Dim sb As String = ""

        On Error GoTo foo

        Throw New Exception()
        Exit Sub
foo:
        On Error GoTo -1
        Console.WriteLine("foo")
        On Error GoTo bar
        Throw New Exception()
        GoTo EndSection
        Exit Sub
bar:
        On Error GoTo -1
        Console.WriteLine("bar")
        On Error GoTo zoo
        Throw New Exception()
        GoTo EndSection
        Exit Sub
zoo:
        On Error GoTo -1
        Console.WriteLine("zoo")        
EndSection:
        Console.WriteLine("EndSection")        
        Exit Sub
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[foo
bar
zoo
EndSection]]>)
        End Sub

        <Fact()>
        Sub Multi_OnError_In_Single_Method_2()
            'This will fail at 2nd call as handler has not been reset correctly
            ' So we will only IL Baseline this 
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        On Error GoTo foo
        Throw New Exception()
        Exit Sub
foo:
        On Error GoTo 0
        Console.Write("foo")
        On Error GoTo bar
        Throw New Exception() '<- Unhandled Exception Here
        GoTo EndSection
        Exit Sub
bar:
        On Error GoTo 0
        Console.Write("bar")
        On Error GoTo zoo
        Throw New Exception()
        GoTo EndSection
        Exit Sub
zoo:
        On Error GoTo 0
        Console.Write("zoo")
EndSection:
        Console.Write("EndSection")
        Exit Sub
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size      184 (0xb8)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  newobj     "Sub System.Exception..ctor()"
  IL_000c:  throw
  IL_000d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0012:  ldc.i4.0
  IL_0013:  stloc.0
  IL_0014:  ldstr      "foo"
  IL_0019:  call       "Sub System.Console.Write(String)"
  IL_001e:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0023:  ldc.i4.3
  IL_0024:  stloc.0
  IL_0025:  newobj     "Sub System.Exception..ctor()"
  IL_002a:  throw
  IL_002b:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0030:  ldc.i4.0
  IL_0031:  stloc.0
  IL_0032:  ldstr      "bar"
  IL_0037:  call       "Sub System.Console.Write(String)"
  IL_003c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0041:  ldc.i4.4
  IL_0042:  stloc.0
  IL_0043:  newobj     "Sub System.Exception..ctor()"
  IL_0048:  throw
  IL_0049:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_004e:  ldc.i4.0
  IL_004f:  stloc.0
  IL_0050:  ldstr      "zoo"
  IL_0055:  call       "Sub System.Console.Write(String)"
  IL_005a:  ldstr      "EndSection"
  IL_005f:  call       "Sub System.Console.Write(String)"
  IL_0064:  leave.s    IL_00af
  IL_0066:  ldc.i4.m1
  IL_0067:  stloc.1
  IL_0068:  ldloc.0
  IL_0069:  switch    (
  IL_0082,
  IL_0082,
  IL_000d,
  IL_002b,
  IL_0049)
  IL_0082:  leave.s    IL_00a4
}
  filter
{
  IL_0084:  isinst     "System.Exception"
  IL_0089:  ldnull
  IL_008a:  cgt.un
  IL_008c:  ldloc.0
  IL_008d:  ldc.i4.0
  IL_008e:  cgt.un
  IL_0090:  and
  IL_0091:  ldloc.1
  IL_0092:  ldc.i4.0
  IL_0093:  ceq
  IL_0095:  and
  IL_0096:  endfilter
}  // end filter
{  // handler
  IL_0098:  castclass  "System.Exception"
  IL_009d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00a2:  leave.s    IL_0066
}
  IL_00a4:  ldc.i4     0x800a0033
  IL_00a9:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00ae:  throw
  IL_00af:  ldloc.1
  IL_00b0:  brfalse.s  IL_00b7
  IL_00b2:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00b7:  ret
}]]>)
        End Sub

        <Fact()>
        Sub Multi_OnError_In_Single_Method_3()
            'This will fail at 2nd call as handler has not been reset correctly
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        On Error GoTo foo
        Throw New Exception()
        Exit Sub
foo:
        Console.Write("foo")
        On Error GoTo bar
        Throw New Exception() '<- Unhandled Exception Here
        GoTo EndSection
        Exit Sub
bar:
        Console.Write("bar")
        On Error GoTo zoo
        Throw New Exception()
        GoTo EndSection
        Exit Sub
zoo:
        Console.Write("zoo")
EndSection:
        Console.Write("EndSection")
        Exit Sub
    End Sub
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation)
            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size      163 (0xa3)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  newobj     "Sub System.Exception..ctor()"
  IL_000c:  throw
  IL_000d:  ldstr      "foo"
  IL_0012:  call       "Sub System.Console.Write(String)"
  IL_0017:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_001c:  ldc.i4.3
  IL_001d:  stloc.0
  IL_001e:  newobj     "Sub System.Exception..ctor()"
  IL_0023:  throw
  IL_0024:  ldstr      "bar"
  IL_0029:  call       "Sub System.Console.Write(String)"
  IL_002e:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0033:  ldc.i4.4
  IL_0034:  stloc.0
  IL_0035:  newobj     "Sub System.Exception..ctor()"
  IL_003a:  throw
  IL_003b:  ldstr      "zoo"
  IL_0040:  call       "Sub System.Console.Write(String)"
  IL_0045:  ldstr      "EndSection"
  IL_004a:  call       "Sub System.Console.Write(String)"
  IL_004f:  leave.s    IL_009a
  IL_0051:  ldc.i4.m1
  IL_0052:  stloc.1
  IL_0053:  ldloc.0
  IL_0054:  switch    (
  IL_006d,
  IL_006d,
  IL_000d,
  IL_0024,
  IL_003b)
  IL_006d:  leave.s    IL_008f
}
  filter
{
  IL_006f:  isinst     "System.Exception"
  IL_0074:  ldnull
  IL_0075:  cgt.un
  IL_0077:  ldloc.0
  IL_0078:  ldc.i4.0
  IL_0079:  cgt.un
  IL_007b:  and
  IL_007c:  ldloc.1
  IL_007d:  ldc.i4.0
  IL_007e:  ceq
  IL_0080:  and
  IL_0081:  endfilter
}  // end filter
{  // handler
  IL_0083:  castclass  "System.Exception"
  IL_0088:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_008d:  leave.s    IL_0051
}
  IL_008f:  ldc.i4     0x800a0033
  IL_0094:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0099:  throw
  IL_009a:  ldloc.1
  IL_009b:  brfalse.s  IL_00a2
  IL_009d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a2:  ret
}]]>)

        End Sub

        <Fact()>
        Sub OnError_With_Explicit_Throw()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
    On Error GoTo Handler
    Console.Write("Start")
    Throw New DivideByZeroException()
    exit Sub
Handler:
    Console.Write("Handler")
    End Sub
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="StartHandler")
        End Sub

        <Fact()>
        Sub OnError_With_Explicit_Error()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
    On Error GoTo Handler
    Console.Write("Start")
    Error 5
    exit Sub
Handler:
    Console.Write("Handler")
    End Sub
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="StartHandler")
        End Sub

        <Fact()>
        Sub OnError_Resume_Next_With_Explicit_Error()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
    On Error Resume Next
    Console.Write("Start")
    Error 5
    exit Sub
Handler:
    Console.Write("Handler")
    End Sub
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="Start")
        End Sub

        <Fact()>
        Sub OnError_Resume_Next_With_Explicit_Exception()
            'This ensures that the Handler is not called
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
    On Error Resume Next
    Console.Write("Start")
    Throw New DivideByZeroException()
    exit Sub
Handler:
    Console.Write("Handler")
    End Sub
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Start]]>)
            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size      151 (0x97)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "Start"
  IL_000e:  call       "Sub System.Console.Write(String)"
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  newobj     "Sub System.DivideByZeroException..ctor()"
  IL_001a:  throw
  IL_001b:  ldc.i4.5
  IL_001c:  stloc.2
  IL_001d:  ldstr      "Handler"
  IL_0022:  call       "Sub System.Console.Write(String)"
  IL_0027:  leave.s    IL_008e
  IL_0029:  ldloc.1
  IL_002a:  ldc.i4.1
  IL_002b:  add
  IL_002c:  ldc.i4.0
  IL_002d:  stloc.1
  IL_002e:  switch    (
  IL_004f,
  IL_0000,
  IL_0007,
  IL_0013,
  IL_0027,
  IL_001b,
  IL_0027)
  IL_004f:  leave.s    IL_0083
  IL_0051:  ldloc.2
  IL_0052:  stloc.1
  IL_0053:  ldloc.0
  IL_0054:  switch    (
  IL_0061,
  IL_0029)
  IL_0061:  leave.s    IL_0083
}
  filter
{
  IL_0063:  isinst     "System.Exception"
  IL_0068:  ldnull
  IL_0069:  cgt.un
  IL_006b:  ldloc.0
  IL_006c:  ldc.i4.0
  IL_006d:  cgt.un
  IL_006f:  and
  IL_0070:  ldloc.1
  IL_0071:  ldc.i4.0
  IL_0072:  ceq
  IL_0074:  and
  IL_0075:  endfilter
}  // end filter
{  // handler
  IL_0077:  castclass  "System.Exception"
  IL_007c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0081:  leave.s    IL_0051
}
  IL_0083:  ldc.i4     0x800a0033
  IL_0088:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_008d:  throw
  IL_008e:  ldloc.1
  IL_008f:  brfalse.s  IL_0096
  IL_0091:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0096:  ret
}]]>)
        End Sub


        <Fact()>
        Sub OnError_Resume_Next_With_Explicit_Exception_AndLabel_Next()
            'This ensures that the Handler is not called
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
    On Error Resume Next
    Console.Write("Start")
    Throw New DivideByZeroException()
    exit Sub
[Next]:
    Console.Write("Handler")
    End Sub
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Start]]>)
            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size      151 (0x97)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "Start"
  IL_000e:  call       "Sub System.Console.Write(String)"
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  newobj     "Sub System.DivideByZeroException..ctor()"
  IL_001a:  throw
  IL_001b:  ldc.i4.5
  IL_001c:  stloc.2
  IL_001d:  ldstr      "Handler"
  IL_0022:  call       "Sub System.Console.Write(String)"
  IL_0027:  leave.s    IL_008e
  IL_0029:  ldloc.1
  IL_002a:  ldc.i4.1
  IL_002b:  add
  IL_002c:  ldc.i4.0
  IL_002d:  stloc.1
  IL_002e:  switch    (
  IL_004f,
  IL_0000,
  IL_0007,
  IL_0013,
  IL_0027,
  IL_001b,
  IL_0027)
  IL_004f:  leave.s    IL_0083
  IL_0051:  ldloc.2
  IL_0052:  stloc.1
  IL_0053:  ldloc.0
  IL_0054:  switch    (
  IL_0061,
  IL_0029)
  IL_0061:  leave.s    IL_0083
}
  filter
{
  IL_0063:  isinst     "System.Exception"
  IL_0068:  ldnull
  IL_0069:  cgt.un
  IL_006b:  ldloc.0
  IL_006c:  ldc.i4.0
  IL_006d:  cgt.un
  IL_006f:  and
  IL_0070:  ldloc.1
  IL_0071:  ldc.i4.0
  IL_0072:  ceq
  IL_0074:  and
  IL_0075:  endfilter
}  // end filter
{  // handler
  IL_0077:  castclass  "System.Exception"
  IL_007c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0081:  leave.s    IL_0051
}
  IL_0083:  ldc.i4     0x800a0033
  IL_0088:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_008d:  throw
  IL_008e:  ldloc.1
  IL_008f:  brfalse.s  IL_0096
  IL_0091:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0096:  ret
}
]]>)
        End Sub

        <Fact()>
        Sub OnError_Resume_Next_With_Explicit_Exception_2()
            'This ensures that the Handler is not called as the current handler should be the last one
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        On Error GoTo Handler
        On Error Resume Next

        Console.Write("Start")
        Throw New DivideByZeroException()
        Exit Sub
Handler:
        Console.Write("Handler")
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Start]]>)
            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size      166 (0xa6)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_000c:  ldc.i4.1
  IL_000d:  stloc.0
  IL_000e:  ldc.i4.3
  IL_000f:  stloc.2
  IL_0010:  ldstr      "Start"
  IL_0015:  call       "Sub System.Console.Write(String)"
  IL_001a:  ldc.i4.4
  IL_001b:  stloc.2
  IL_001c:  newobj     "Sub System.DivideByZeroException..ctor()"
  IL_0021:  throw
  IL_0022:  ldc.i4.6
  IL_0023:  stloc.2
  IL_0024:  ldstr      "Handler"
  IL_0029:  call       "Sub System.Console.Write(String)"
  IL_002e:  leave.s    IL_009d
  IL_0030:  ldloc.1
  IL_0031:  ldc.i4.1
  IL_0032:  add
  IL_0033:  ldc.i4.0
  IL_0034:  stloc.1
  IL_0035:  switch    (
  IL_005a,
  IL_0000,
  IL_0007,
  IL_000e,
  IL_001a,
  IL_002e,
  IL_0022,
  IL_002e)
  IL_005a:  leave.s    IL_0092
  IL_005c:  ldloc.2
  IL_005d:  stloc.1
  IL_005e:  ldloc.0
  IL_005f:  switch    (
  IL_0070,
  IL_0030,
  IL_0022)
  IL_0070:  leave.s    IL_0092
}
  filter
{
  IL_0072:  isinst     "System.Exception"
  IL_0077:  ldnull
  IL_0078:  cgt.un
  IL_007a:  ldloc.0
  IL_007b:  ldc.i4.0
  IL_007c:  cgt.un
  IL_007e:  and
  IL_007f:  ldloc.1
  IL_0080:  ldc.i4.0
  IL_0081:  ceq
  IL_0083:  and
  IL_0084:  endfilter
}  // end filter
{  // handler
  IL_0086:  castclass  "System.Exception"
  IL_008b:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0090:  leave.s    IL_005c
}
  IL_0092:  ldc.i4     0x800a0033
  IL_0097:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_009c:  throw
  IL_009d:  ldloc.1
  IL_009e:  brfalse.s  IL_00a5
  IL_00a0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00a5:  ret
}]]>)
        End Sub

        <WorkItem(737273, "DevDiv")>
        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Sub OnError_WithLoopingConstructs()
            'Various Looping constructs with Errors and capturing the behaviour of Resume Next as going into the loop rather
            ' then skipping to the next statement outstide of the loop
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System
Imports System.Collections
Imports System.Collections.Generic

Module Module1
    Sub Main()
        ForEach()
        ForNext()
        DoWhile()
        ForNextStepError()
    End Sub

    Sub ForEach()
        'The error will cause it to resume in the block
        On Error GoTo errhandler

        Dim x = {}
        For Each i In xCollection()
            Console.Write("in block")
        Next

        Console.WriteLine("End")
errhandler:
        Resume Next
    End Sub


    Sub ForNext()
        'The error will cause it to resume in the block
        On Error GoTo errhandler

        Dim x = {}
        For i = 0 To xCollection.Count - 1
            Console.Write("in block")
        Next

        Console.WriteLine("End")
errhandler:
        Resume Next
    End Sub

    Sub DoWhile()
        'The error will cause it to resume in the block
        On Error GoTo errhandler
        Dim iIndex As Integer


        Do While iIndex <= xCollection.Count - 1
            Console.Write("in block")
            iIndex += 1
        Loop

        Console.WriteLine("End")
errhandler:
        If iIndex >= 3 Then
            Exit Sub
        End If
        Resume Next
    End Sub

    Function xCollection() As System.Collections.Generic.List(Of Integer)
        Throw New Exception() 'Used instead of Err.Raise or Error
        Return Nothing
    End Function


    'If error in the step it will still enter to loop
    ' Need count condition to ensure I can exit otherwise
    'becomes and infinite loop
    Public Sub ForNextStepError()
        On Error Resume Next
        Dim iCount As Integer = 0

        For i = 0 To 10 Step StepFoo(-1)
            Console.WriteLine("In Loop" & i)
            iCount += 1
            If iCount >= 3 Then
                Exit For
            End If
        Next
    End Sub

    Function StepFoo(i As Integer) As Integer
        If i < 0 Then
            Error 5
        Else
            Return i
        End If
    End Function

End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation)

            'Verify the IL for each loop construct
            compilationVerifier.VerifyIL("Module1.ForEach", <![CDATA[
{
  // Code size      238 (0xee)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  System.Collections.Generic.List(Of Integer).Enumerator V_3) //VB$ForEachEnumerator
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldc.i4.0
  IL_000a:  newarr     "Object"
  IL_000f:  pop
  IL_0010:  ldc.i4.3
  IL_0011:  stloc.2
  IL_0012:  call       "Function Module1.xCollection() As System.Collections.Generic.List(Of Integer)"
  IL_0017:  callvirt   "Function System.Collections.Generic.List(Of Integer).GetEnumerator() As System.Collections.Generic.List(Of Integer).Enumerator"
  IL_001c:  stloc.3
  IL_001d:  br.s       IL_0035
  IL_001f:  ldloca.s   V_3
  IL_0021:  call       "Function System.Collections.Generic.List(Of Integer).Enumerator.get_Current() As Integer"
  IL_0026:  pop
  IL_0027:  ldc.i4.4
  IL_0028:  stloc.2
  IL_0029:  ldstr      "in block"
  IL_002e:  call       "Sub System.Console.Write(String)"
  IL_0033:  ldc.i4.5
  IL_0034:  stloc.2
  IL_0035:  ldloca.s   V_3
  IL_0037:  call       "Function System.Collections.Generic.List(Of Integer).Enumerator.MoveNext() As Boolean"
  IL_003c:  brtrue.s   IL_001f
  IL_003e:  ldc.i4.6
  IL_003f:  stloc.2
  IL_0040:  ldloca.s   V_3
  IL_0042:  constrained. "System.Collections.Generic.List(Of Integer).Enumerator"
  IL_0048:  callvirt   "Sub System.IDisposable.Dispose()"
  IL_004d:  ldc.i4.7
  IL_004e:  stloc.2
  IL_004f:  ldstr      "End"
  IL_0054:  call       "Sub System.Console.WriteLine(String)"
  IL_0059:  ldc.i4.8
  IL_005a:  stloc.2
  IL_005b:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0060:  ldloc.1
  IL_0061:  brtrue.s   IL_0070
  IL_0063:  ldc.i4     0x800a0014
  IL_0068:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_006d:  throw
  IL_006e:  leave.s    IL_00e5
  IL_0070:  ldloc.1
  IL_0071:  ldc.i4.1
  IL_0072:  add
  IL_0073:  ldc.i4.0
  IL_0074:  stloc.1
  IL_0075:  switch    (
  IL_00a2,
  IL_0000,
  IL_0007,
  IL_0010,
  IL_0027,
  IL_0033,
  IL_003e,
  IL_004d,
  IL_0059,
  IL_006e)
  IL_00a2:  leave.s    IL_00da
  IL_00a4:  ldloc.2
  IL_00a5:  stloc.1
  IL_00a6:  ldloc.0
  IL_00a7:  switch    (
  IL_00b8,
  IL_0070,
  IL_0059)
  IL_00b8:  leave.s    IL_00da
}
  filter
{
  IL_00ba:  isinst     "System.Exception"
  IL_00bf:  ldnull
  IL_00c0:  cgt.un
  IL_00c2:  ldloc.0
  IL_00c3:  ldc.i4.0
  IL_00c4:  cgt.un
  IL_00c6:  and
  IL_00c7:  ldloc.1
  IL_00c8:  ldc.i4.0
  IL_00c9:  ceq
  IL_00cb:  and
  IL_00cc:  endfilter
}  // end filter
{  // handler
  IL_00ce:  castclass  "System.Exception"
  IL_00d3:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00d8:  leave.s    IL_00a4
}
  IL_00da:  ldc.i4     0x800a0033
  IL_00df:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e4:  throw
  IL_00e5:  ldloc.1
  IL_00e6:  brfalse.s  IL_00ed
  IL_00e8:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ed:  ret
}
]]>)

            compilationVerifier.VerifyIL("Module1.ForNext", <![CDATA[
{
  // Code size      218 (0xda)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3, //VB$ForLimit
  Integer V_4) //i
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldc.i4.0
  IL_000a:  newarr     "Object"
  IL_000f:  pop
  IL_0010:  ldc.i4.3
  IL_0011:  stloc.2
  IL_0012:  call       "Function Module1.xCollection() As System.Collections.Generic.List(Of Integer)"
  IL_0017:  callvirt   "Function System.Collections.Generic.List(Of Integer).get_Count() As Integer"
  IL_001c:  ldc.i4.1
  IL_001d:  sub.ovf
  IL_001e:  stloc.3
  IL_001f:  ldc.i4.0
  IL_0020:  stloc.s    V_4
  IL_0022:  br.s       IL_0038
  IL_0024:  ldc.i4.4
  IL_0025:  stloc.2
  IL_0026:  ldstr      "in block"
  IL_002b:  call       "Sub System.Console.Write(String)"
  IL_0030:  ldc.i4.5
  IL_0031:  stloc.2
  IL_0032:  ldloc.s    V_4
  IL_0034:  ldc.i4.1
  IL_0035:  add.ovf
  IL_0036:  stloc.s    V_4
  IL_0038:  ldloc.s    V_4
  IL_003a:  ldloc.3
  IL_003b:  ble.s      IL_0024
  IL_003d:  ldc.i4.6
  IL_003e:  stloc.2
  IL_003f:  ldstr      "End"
  IL_0044:  call       "Sub System.Console.WriteLine(String)"
  IL_0049:  ldc.i4.7
  IL_004a:  stloc.2
  IL_004b:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0050:  ldloc.1
  IL_0051:  brtrue.s   IL_0060
  IL_0053:  ldc.i4     0x800a0014
  IL_0058:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_005d:  throw
  IL_005e:  leave.s    IL_00d1
  IL_0060:  ldloc.1
  IL_0061:  ldc.i4.1
  IL_0062:  add
  IL_0063:  ldc.i4.0
  IL_0064:  stloc.1
  IL_0065:  switch    (
  IL_008e,
  IL_0000,
  IL_0007,
  IL_0010,
  IL_0024,
  IL_0030,
  IL_003d,
  IL_0049,
  IL_005e)
  IL_008e:  leave.s    IL_00c6
  IL_0090:  ldloc.2
  IL_0091:  stloc.1
  IL_0092:  ldloc.0
  IL_0093:  switch    (
  IL_00a4,
  IL_0060,
  IL_0049)
  IL_00a4:  leave.s    IL_00c6
}
  filter
{
  IL_00a6:  isinst     "System.Exception"
  IL_00ab:  ldnull
  IL_00ac:  cgt.un
  IL_00ae:  ldloc.0
  IL_00af:  ldc.i4.0
  IL_00b0:  cgt.un
  IL_00b2:  and
  IL_00b3:  ldloc.1
  IL_00b4:  ldc.i4.0
  IL_00b5:  ceq
  IL_00b7:  and
  IL_00b8:  endfilter
}  // end filter
{  // handler
  IL_00ba:  castclass  "System.Exception"
  IL_00bf:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00c4:  leave.s    IL_0090
}
  IL_00c6:  ldc.i4     0x800a0033
  IL_00cb:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d0:  throw
  IL_00d1:  ldloc.1
  IL_00d2:  brfalse.s  IL_00d9
  IL_00d4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d9:  ret
}
]]>)

            compilationVerifier.VerifyIL("Module1.DoWhile", <![CDATA[{
  // Code size      225 (0xe1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3) //iIndex
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  br.s       IL_001b
  IL_0009:  ldc.i4.4
  IL_000a:  stloc.2
  IL_000b:  ldstr      "in block"
  IL_0010:  call       "Sub System.Console.Write(String)"
  IL_0015:  ldc.i4.5
  IL_0016:  stloc.2
  IL_0017:  ldloc.3
  IL_0018:  ldc.i4.1
  IL_0019:  add.ovf
  IL_001a:  stloc.3
  IL_001b:  ldc.i4.3
  IL_001c:  stloc.2
  IL_001d:  ldloc.3
  IL_001e:  call       "Function Module1.xCollection() As System.Collections.Generic.List(Of Integer)"
  IL_0023:  callvirt   "Function System.Collections.Generic.List(Of Integer).get_Count() As Integer"
  IL_0028:  ldc.i4.1
  IL_0029:  sub.ovf
  IL_002a:  ble.s      IL_0009
  IL_002c:  ldc.i4.7
  IL_002d:  stloc.2
  IL_002e:  ldstr      "End"
  IL_0033:  call       "Sub System.Console.WriteLine(String)"
  IL_0038:  ldc.i4.8
  IL_0039:  stloc.2
  IL_003a:  ldloc.3
  IL_003b:  ldc.i4.3
  IL_003c:  blt.s      IL_0043
  IL_003e:  leave      IL_00d8
  IL_0043:  ldc.i4.s   10
  IL_0045:  stloc.2
  IL_0046:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_004b:  ldloc.1
  IL_004c:  brtrue.s   IL_005b
  IL_004e:  ldc.i4     0x800a0014
  IL_0053:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0058:  throw
  IL_0059:  leave.s    IL_00d8
  IL_005b:  ldloc.1
  IL_005c:  ldc.i4.1
  IL_005d:  add
  IL_005e:  ldc.i4.0
  IL_005f:  stloc.1
  IL_0060:  switch    (
  IL_0095,
  IL_0000,
  IL_001b,
  IL_001b,
  IL_0009,
  IL_0015,
  IL_001b,
  IL_002c,
  IL_0038,
  IL_0059,
  IL_0043,
  IL_0059)
  IL_0095:  leave.s    IL_00cd
  IL_0097:  ldloc.2
  IL_0098:  stloc.1
  IL_0099:  ldloc.0
  IL_009a:  switch    (
  IL_00ab,
  IL_005b,
  IL_0038)
  IL_00ab:  leave.s    IL_00cd
}
  filter
{
  IL_00ad:  isinst     "System.Exception"
  IL_00b2:  ldnull
  IL_00b3:  cgt.un
  IL_00b5:  ldloc.0
  IL_00b6:  ldc.i4.0
  IL_00b7:  cgt.un
  IL_00b9:  and
  IL_00ba:  ldloc.1
  IL_00bb:  ldc.i4.0
  IL_00bc:  ceq
  IL_00be:  and
  IL_00bf:  endfilter
}  // end filter
{  // handler
  IL_00c1:  castclass  "System.Exception"
  IL_00c6:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00cb:  leave.s    IL_0097
}
  IL_00cd:  ldc.i4     0x800a0033
  IL_00d2:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d7:  throw
  IL_00d8:  ldloc.1
  IL_00d9:  brfalse.s  IL_00e0
  IL_00db:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00e0:  ret
}]]>)

            compilationVerifier.VerifyIL("Module1.ForNextStepError", <![CDATA[
{
  // Code size      218 (0xda)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3, //iCount
  Integer V_4, //VB$ForStep
  Integer V_5) //i
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldc.i4.0
  IL_000a:  stloc.3
  IL_000b:  ldc.i4.3
  IL_000c:  stloc.2
  IL_000d:  ldc.i4.m1
  IL_000e:  call       "Function Module1.StepFoo(Integer) As Integer"
  IL_0013:  stloc.s    V_4
  IL_0015:  ldc.i4.0
  IL_0016:  stloc.s    V_5
  IL_0018:  br.s       IL_004c
  IL_001a:  ldc.i4.4
  IL_001b:  stloc.2
  IL_001c:  ldstr      "In Loop"
  IL_0021:  ldloc.s    V_5
  IL_0023:  call       "Function Microsoft.VisualBasic.CompilerServices.Conversions.ToString(Integer) As String"
  IL_0028:  call       "Function String.Concat(String, String) As String"
  IL_002d:  call       "Sub System.Console.WriteLine(String)"
  IL_0032:  ldc.i4.5
  IL_0033:  stloc.2
  IL_0034:  ldloc.3
  IL_0035:  ldc.i4.1
  IL_0036:  add.ovf
  IL_0037:  stloc.3
  IL_0038:  ldc.i4.6
  IL_0039:  stloc.2
  IL_003a:  ldloc.3
  IL_003b:  ldc.i4.3
  IL_003c:  blt.s      IL_0043
  IL_003e:  leave      IL_00d1
  IL_0043:  ldc.i4.8
  IL_0044:  stloc.2
  IL_0045:  ldloc.s    V_5
  IL_0047:  ldloc.s    V_4
  IL_0049:  add.ovf
  IL_004a:  stloc.s    V_5
  IL_004c:  ldloc.s    V_4
  IL_004e:  ldc.i4.s   31
  IL_0050:  shr
  IL_0051:  ldloc.s    V_5
  IL_0053:  xor
  IL_0054:  ldloc.s    V_4
  IL_0056:  ldc.i4.s   31
  IL_0058:  shr
  IL_0059:  ldc.i4.s   10
  IL_005b:  xor
  IL_005c:  ble.s      IL_001a
  IL_005e:  leave.s    IL_00d1
  IL_0060:  ldloc.1
  IL_0061:  ldc.i4.1
  IL_0062:  add
  IL_0063:  ldc.i4.0
  IL_0064:  stloc.1
  IL_0065:  switch    (
  IL_0092,
  IL_0000,
  IL_0007,
  IL_000b,
  IL_001a,
  IL_0032,
  IL_0038,
  IL_005e,
  IL_0043,
  IL_005e)
  IL_0092:  leave.s    IL_00c6
  IL_0094:  ldloc.2
  IL_0095:  stloc.1
  IL_0096:  ldloc.0
  IL_0097:  switch    (
  IL_00a4,
  IL_0060)
  IL_00a4:  leave.s    IL_00c6
}
  filter
{
  IL_00a6:  isinst     "System.Exception"
  IL_00ab:  ldnull
  IL_00ac:  cgt.un
  IL_00ae:  ldloc.0
  IL_00af:  ldc.i4.0
  IL_00b0:  cgt.un
  IL_00b2:  and
  IL_00b3:  ldloc.1
  IL_00b4:  ldc.i4.0
  IL_00b5:  ceq
  IL_00b7:  and
  IL_00b8:  endfilter
}  // end filter
{  // handler
  IL_00ba:  castclass  "System.Exception"
  IL_00bf:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00c4:  leave.s    IL_0094
}
  IL_00c6:  ldc.i4     0x800a0033
  IL_00cb:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00d0:  throw
  IL_00d1:  ldloc.1
  IL_00d2:  brfalse.s  IL_00d9
  IL_00d4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00d9:  ret
}
]]>)

        End Sub

        <Fact()>
        Sub OnError_GotoZeroBaselineInMainBlock()
            'This will reset the handler and the 2nd error will result in
            'an unhandled exception.   Will IL Baseline  the test
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        GotoMinus0()
    End Sub

    Sub GotoMinus0()
        On Error GoTo foo
        Error 1
        On Error GoTo 0 'This should reset the error handler to nothing
        Error 2 '< It will fail here with unhandled exception
        Exit Sub
foo:
        Console.WriteLine("In Handler")
        Resume Next
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Module1.GotoMinus0", <![CDATA[{
  // Code size      189 (0xbd)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldc.i4.1
  IL_000a:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_000f:  throw
  IL_0010:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0015:  ldc.i4.0
  IL_0016:  stloc.0
  IL_0017:  ldc.i4.4
  IL_0018:  stloc.2
  IL_0019:  ldc.i4.2
  IL_001a:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_001f:  throw
  IL_0020:  ldc.i4.6
  IL_0021:  stloc.2
  IL_0022:  ldstr      "In Handler"
  IL_0027:  call       "Sub System.Console.WriteLine(String)"
  IL_002c:  ldc.i4.7
  IL_002d:  stloc.2
  IL_002e:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0033:  ldloc.1
  IL_0034:  brtrue.s   IL_0043
  IL_0036:  ldc.i4     0x800a0014
  IL_003b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0040:  throw
  IL_0041:  leave.s    IL_00b4
  IL_0043:  ldloc.1
  IL_0044:  ldc.i4.1
  IL_0045:  add
  IL_0046:  ldc.i4.0
  IL_0047:  stloc.1
  IL_0048:  switch    (
  IL_0071,
  IL_0000,
  IL_0007,
  IL_0010,
  IL_0017,
  IL_0041,
  IL_0020,
  IL_002c,
  IL_0041)
  IL_0071:  leave.s    IL_00a9
  IL_0073:  ldloc.2
  IL_0074:  stloc.1
  IL_0075:  ldloc.0
  IL_0076:  switch    (
  IL_0087,
  IL_0043,
  IL_0020)
  IL_0087:  leave.s    IL_00a9
}
  filter
{
  IL_0089:  isinst     "System.Exception"
  IL_008e:  ldnull
  IL_008f:  cgt.un
  IL_0091:  ldloc.0
  IL_0092:  ldc.i4.0
  IL_0093:  cgt.un
  IL_0095:  and
  IL_0096:  ldloc.1
  IL_0097:  ldc.i4.0
  IL_0098:  ceq
  IL_009a:  and
  IL_009b:  endfilter
}  // end filter
{  // handler
  IL_009d:  castclass  "System.Exception"
  IL_00a2:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00a7:  leave.s    IL_0073
}
  IL_00a9:  ldc.i4     0x800a0033
  IL_00ae:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b3:  throw
  IL_00b4:  ldloc.1
  IL_00b5:  brfalse.s  IL_00bc
  IL_00b7:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00bc:  ret
}
]]>)
        End Sub

        <Fact()>
        Sub OnError_GotoZeroBaselineInHandler()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        GotoMinus0
    End Sub

    Sub GotoMinus0()        
        On Error GoTo foo
        Throw New Exception()

        Throw New Exception() '< It will fail here with unhandled exception
        Exit Sub
foo:
        Console.WriteLine("In Handler")
        On Error GoTo 0 'This should reset the error handler to nothing
        Resume Next
    End Sub

End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Module1.GotoMinus0", <![CDATA[{
  // Code size      187 (0xbb)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  newobj     "Sub System.Exception..ctor()"
  IL_000e:  throw
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  newobj     "Sub System.Exception..ctor()"
  IL_0016:  throw
  IL_0017:  ldc.i4.5
  IL_0018:  stloc.2
  IL_0019:  ldstr      "In Handler"
  IL_001e:  call       "Sub System.Console.WriteLine(String)"
  IL_0023:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0028:  ldc.i4.0
  IL_0029:  stloc.0
  IL_002a:  ldc.i4.7
  IL_002b:  stloc.2
  IL_002c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0031:  ldloc.1
  IL_0032:  brtrue.s   IL_0041
  IL_0034:  ldc.i4     0x800a0014
  IL_0039:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_003e:  throw
  IL_003f:  leave.s    IL_00b2
  IL_0041:  ldloc.1
  IL_0042:  ldc.i4.1
  IL_0043:  add
  IL_0044:  ldc.i4.0
  IL_0045:  stloc.1
  IL_0046:  switch    (
  IL_006f,
  IL_0000,
  IL_0007,
  IL_000f,
  IL_003f,
  IL_0017,
  IL_0023,
  IL_002a,
  IL_003f)
  IL_006f:  leave.s    IL_00a7
  IL_0071:  ldloc.2
  IL_0072:  stloc.1
  IL_0073:  ldloc.0
  IL_0074:  switch    (
  IL_0085,
  IL_0041,
  IL_0017)
  IL_0085:  leave.s    IL_00a7
}
  filter
{
  IL_0087:  isinst     "System.Exception"
  IL_008c:  ldnull
  IL_008d:  cgt.un
  IL_008f:  ldloc.0
  IL_0090:  ldc.i4.0
  IL_0091:  cgt.un
  IL_0093:  and
  IL_0094:  ldloc.1
  IL_0095:  ldc.i4.0
  IL_0096:  ceq
  IL_0098:  and
  IL_0099:  endfilter
}  // end filter
{  // handler
  IL_009b:  castclass  "System.Exception"
  IL_00a0:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00a5:  leave.s    IL_0071
}
  IL_00a7:  ldc.i4     0x800a0033
  IL_00ac:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00b1:  throw
  IL_00b2:  ldloc.1
  IL_00b3:  brfalse.s  IL_00ba
  IL_00b5:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00ba:  ret
}]]>)
        End Sub

        <Fact()>
        Sub OnError_GotoMinusBaselineInhandler()
            'The difference on the resetting the error in the handler and the resume is important as this one
            'will resume in the Handler and hence the 2nd error will not be generated
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        GotoMinus1()
    End Sub

    Sub GotoMinus1()
        'Resume Next takes place in Foo: Block
        'and hence will result in infinite recursion without
        'the Iindex count exit condition

        ' This is interesting because of the Error Number 20 being raised for Resume without Error
        ' and is documented in spec

        Dim IiNDEX As Integer = 0

        On Error GoTo foo
        Console.WriteLine("Before 1st Error")
        Error 1
        Console.WriteLine("Before 2nd Error")
        Error 2
        Exit Sub
foo:
        Console.WriteLine("Foo")
        IiNDEX += 1
        If IiNDEX >= 3 Then Exit Sub
        Console.WriteLine("In Foo Before Reset")
        On Error GoTo -1 'This should reset the error
        Console.WriteLine("In Foo After Reset")

        Resume Next
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Before 1st Error
Foo
In Foo Before Reset
In Foo After Reset
Foo
In Foo Before Reset
In Foo After Reset
Foo
]]>)

            compilationVerifier.VerifyIL("Module1.GotoMinus1", <![CDATA[{
  // Code size      298 (0x12a)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                Integer V_3) //IiNDEX
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldc.i4.0
  IL_0003:  stloc.3
  IL_0004:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0009:  ldc.i4.2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.3
  IL_000c:  stloc.2
  IL_000d:  ldstr      "Before 1st Error"
  IL_0012:  call       "Sub System.Console.WriteLine(String)"
  IL_0017:  ldc.i4.4
  IL_0018:  stloc.2
  IL_0019:  ldc.i4.1
  IL_001a:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_001f:  throw
  IL_0020:  ldc.i4.5
  IL_0021:  stloc.2
  IL_0022:  ldstr      "Before 2nd Error"
  IL_0027:  call       "Sub System.Console.WriteLine(String)"
  IL_002c:  ldc.i4.6
  IL_002d:  stloc.2
  IL_002e:  ldc.i4.2
  IL_002f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0034:  throw
  IL_0035:  ldc.i4.8
  IL_0036:  stloc.2
  IL_0037:  ldstr      "Foo"
  IL_003c:  call       "Sub System.Console.WriteLine(String)"
  IL_0041:  ldc.i4.s   9
  IL_0043:  stloc.2
  IL_0044:  ldloc.3
  IL_0045:  ldc.i4.1
  IL_0046:  add.ovf
  IL_0047:  stloc.3
  IL_0048:  ldc.i4.s   10
  IL_004a:  stloc.2
  IL_004b:  ldloc.3
  IL_004c:  ldc.i4.3
  IL_004d:  blt.s      IL_0054
  IL_004f:  leave      IL_0121
  IL_0054:  ldc.i4.s   12
  IL_0056:  stloc.2
  IL_0057:  ldstr      "In Foo Before Reset"
  IL_005c:  call       "Sub System.Console.WriteLine(String)"
  IL_0061:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0066:  ldc.i4.0
  IL_0067:  stloc.1
  IL_0068:  ldc.i4.s   14
  IL_006a:  stloc.2
  IL_006b:  ldstr      "In Foo After Reset"
  IL_0070:  call       "Sub System.Console.WriteLine(String)"
  IL_0075:  ldc.i4.s   15
  IL_0077:  stloc.2
  IL_0078:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_007d:  ldloc.1
  IL_007e:  brtrue.s   IL_0090
  IL_0080:  ldc.i4     0x800a0014
  IL_0085:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_008a:  throw
  IL_008b:  leave      IL_0121
  IL_0090:  ldloc.1
  IL_0091:  ldc.i4.1
  IL_0092:  add
  IL_0093:  ldc.i4.0
  IL_0094:  stloc.1
  IL_0095:  switch    (
  IL_00de,
  IL_0000,
  IL_0004,
  IL_000b,
  IL_0017,
  IL_0020,
  IL_002c,
  IL_008b,
  IL_0035,
  IL_0041,
  IL_0048,
  IL_008b,
  IL_0054,
  IL_0061,
  IL_0068,
  IL_0075,
  IL_008b)
  IL_00de:  leave.s    IL_0116
  IL_00e0:  ldloc.2
  IL_00e1:  stloc.1
  IL_00e2:  ldloc.0
  IL_00e3:  switch    (
  IL_00f4,
  IL_0090,
  IL_0035)
  IL_00f4:  leave.s    IL_0116
}
  filter
{
  IL_00f6:  isinst     "System.Exception"
  IL_00fb:  ldnull
  IL_00fc:  cgt.un
  IL_00fe:  ldloc.0
  IL_00ff:  ldc.i4.0
  IL_0100:  cgt.un
  IL_0102:  and
  IL_0103:  ldloc.1
  IL_0104:  ldc.i4.0
  IL_0105:  ceq
  IL_0107:  and
  IL_0108:  endfilter
}  // end filter
{  // handler
  IL_010a:  castclass  "System.Exception"
  IL_010f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0114:  leave.s    IL_00e0
}
  IL_0116:  ldc.i4     0x800a0033
  IL_011b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0120:  throw
  IL_0121:  ldloc.1
  IL_0122:  brfalse.s  IL_0129
  IL_0124:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0129:  ret
}]]>)
        End Sub

        <Fact()>
        Sub OnError_GotoMinusBaselineInMainBlock()
            'The difference on the resetting the error in the handler and the resume is important as this one
            'will resume in the Handler and hence the 2nd error will not be generated
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        GotoMinus1()
    End Sub

    Sub GotoMinus1()
        'Resume Next takes place in Foo: Block
        'and hence will result in infinite recursion without
        'the Iindex count exit condition

        ' This is interesting because of the Error Number 20 being raised for Resume without Error
        ' and is documented in spec

        Dim IiNDEX As Integer = 0

        On Error GoTo foo
        Console.WriteLine("Before 1st Error")
        Error 1

        Console.WriteLine("In Main Block Before Reset")
        On Error GoTo -1 'This should reset the error
        Console.WriteLine("In Main Block After Reset")

        Console.WriteLine("Before 2nd Error")
        Error 2
        Exit Sub
foo:
        Console.WriteLine("Foo")
        IiNDEX += 1
        If IiNDEX >= 3 Then Exit Sub
        
        Resume Next
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Before 1st Error
Foo
In Main Block Before Reset
In Main Block After Reset
Before 2nd Error
Foo]]>)

            compilationVerifier.VerifyIL("Module1.GotoMinus1", <![CDATA[
{
  // Code size      298 (0x12a)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                Integer V_3) //IiNDEX
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldc.i4.0
  IL_0003:  stloc.3
  IL_0004:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0009:  ldc.i4.2
  IL_000a:  stloc.0
  IL_000b:  ldc.i4.3
  IL_000c:  stloc.2
  IL_000d:  ldstr      "Before 1st Error"
  IL_0012:  call       "Sub System.Console.WriteLine(String)"
  IL_0017:  ldc.i4.4
  IL_0018:  stloc.2
  IL_0019:  ldc.i4.1
  IL_001a:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_001f:  throw
  IL_0020:  ldc.i4.5
  IL_0021:  stloc.2
  IL_0022:  ldstr      "In Main Block Before Reset"
  IL_0027:  call       "Sub System.Console.WriteLine(String)"
  IL_002c:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0031:  ldc.i4.0
  IL_0032:  stloc.1
  IL_0033:  ldc.i4.7
  IL_0034:  stloc.2
  IL_0035:  ldstr      "In Main Block After Reset"
  IL_003a:  call       "Sub System.Console.WriteLine(String)"
  IL_003f:  ldc.i4.8
  IL_0040:  stloc.2
  IL_0041:  ldstr      "Before 2nd Error"
  IL_0046:  call       "Sub System.Console.WriteLine(String)"
  IL_004b:  ldc.i4.s   9
  IL_004d:  stloc.2
  IL_004e:  ldc.i4.2
  IL_004f:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0054:  throw
  IL_0055:  ldc.i4.s   11
  IL_0057:  stloc.2
  IL_0058:  ldstr      "Foo"
  IL_005d:  call       "Sub System.Console.WriteLine(String)"
  IL_0062:  ldc.i4.s   12
  IL_0064:  stloc.2
  IL_0065:  ldloc.3
  IL_0066:  ldc.i4.1
  IL_0067:  add.ovf
  IL_0068:  stloc.3
  IL_0069:  ldc.i4.s   13
  IL_006b:  stloc.2
  IL_006c:  ldloc.3
  IL_006d:  ldc.i4.3
  IL_006e:  blt.s      IL_0075
  IL_0070:  leave      IL_0121
  IL_0075:  ldc.i4.s   15
  IL_0077:  stloc.2
  IL_0078:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_007d:  ldloc.1
  IL_007e:  brtrue.s   IL_0090
  IL_0080:  ldc.i4     0x800a0014
  IL_0085:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_008a:  throw
  IL_008b:  leave      IL_0121
  IL_0090:  ldloc.1
  IL_0091:  ldc.i4.1
  IL_0092:  add
  IL_0093:  ldc.i4.0
  IL_0094:  stloc.1
  IL_0095:  switch    (
  IL_00de,
  IL_0000,
  IL_0004,
  IL_000b,
  IL_0017,
  IL_0020,
  IL_002c,
  IL_0033,
  IL_003f,
  IL_004b,
  IL_008b,
  IL_0055,
  IL_0062,
  IL_0069,
  IL_008b,
  IL_0075,
  IL_008b)
  IL_00de:  leave.s    IL_0116
  IL_00e0:  ldloc.2
  IL_00e1:  stloc.1
  IL_00e2:  ldloc.0
  IL_00e3:  switch    (
  IL_00f4,
  IL_0090,
  IL_0055)
  IL_00f4:  leave.s    IL_0116
}
  filter
{
  IL_00f6:  isinst     "System.Exception"
  IL_00fb:  ldnull
  IL_00fc:  cgt.un
  IL_00fe:  ldloc.0
  IL_00ff:  ldc.i4.0
  IL_0100:  cgt.un
  IL_0102:  and
  IL_0103:  ldloc.1
  IL_0104:  ldc.i4.0
  IL_0105:  ceq
  IL_0107:  and
  IL_0108:  endfilter
}  // end filter
{  // handler
  IL_010a:  castclass  "System.Exception"
  IL_010f:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0114:  leave.s    IL_00e0
}
  IL_0116:  ldc.i4     0x800a0033
  IL_011b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0120:  throw
  IL_0121:  ldloc.1
  IL_0122:  brfalse.s  IL_0129
  IL_0124:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0129:  ret
}]]>)
        End Sub

        <Fact()>
        Sub OnError_GotoMinusBaselineInMainBlock_ThrowException()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        GotoMinus1()
    End Sub

        Sub GotoMinus1()
        'Resume next will result in going to the next statement from initial error and 
        'handler is reset here - so need to have extra condition check.

        On Error GoTo foo
        Console.WriteLine("Before 1 Exception")
        Throw New Exception()
        Console.WriteLine("After 1 Exception")

        On Error GoTo -1 'This should reset the error
        Console.WriteLine("Before 2 Exception")        
        Throw New Exception()
        Console.WriteLine("After 2 Exception")        
        Exit Sub
foo:
        Console.WriteLine("Foo")        
        Resume Next
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Before 1 Exception
Foo
After 1 Exception
Before 2 Exception
Foo
After 2 Exception]]>)


            compilationVerifier.VerifyIL("Module1.GotoMinus1", <![CDATA[{
  // Code size      261 (0x105)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "Before 1 Exception"
  IL_000e:  call       "Sub System.Console.WriteLine(String)"
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  newobj     "Sub System.Exception..ctor()"
  IL_001a:  throw
  IL_001b:  ldc.i4.4
  IL_001c:  stloc.2
  IL_001d:  ldstr      "After 1 Exception"
  IL_0022:  call       "Sub System.Console.WriteLine(String)"
  IL_0027:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_002c:  ldc.i4.0
  IL_002d:  stloc.1
  IL_002e:  ldc.i4.6
  IL_002f:  stloc.2
  IL_0030:  ldstr      "Before 2 Exception"
  IL_0035:  call       "Sub System.Console.WriteLine(String)"
  IL_003a:  ldc.i4.7
  IL_003b:  stloc.2
  IL_003c:  newobj     "Sub System.Exception..ctor()"
  IL_0041:  throw
  IL_0042:  ldc.i4.8
  IL_0043:  stloc.2
  IL_0044:  ldstr      "After 2 Exception"
  IL_0049:  call       "Sub System.Console.WriteLine(String)"
  IL_004e:  leave      IL_00fc
  IL_0053:  ldc.i4.s   10
  IL_0055:  stloc.2
  IL_0056:  ldstr      "Foo"
  IL_005b:  call       "Sub System.Console.WriteLine(String)"
  IL_0060:  ldc.i4.s   11
  IL_0062:  stloc.2
  IL_0063:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0068:  ldloc.1
  IL_0069:  brtrue.s   IL_007b
  IL_006b:  ldc.i4     0x800a0014
  IL_0070:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0075:  throw
  IL_0076:  leave      IL_00fc
  IL_007b:  ldloc.1
  IL_007c:  ldc.i4.1
  IL_007d:  add
  IL_007e:  ldc.i4.0
  IL_007f:  stloc.1
  IL_0080:  switch    (
  IL_00b9,
  IL_0000,
  IL_0007,
  IL_0013,
  IL_001b,
  IL_0027,
  IL_002e,
  IL_003a,
  IL_0042,
  IL_0076,
  IL_0053,
  IL_0060,
  IL_0076)
  IL_00b9:  leave.s    IL_00f1
  IL_00bb:  ldloc.2
  IL_00bc:  stloc.1
  IL_00bd:  ldloc.0
  IL_00be:  switch    (
  IL_00cf,
  IL_007b,
  IL_0053)
  IL_00cf:  leave.s    IL_00f1
}
  filter
{
  IL_00d1:  isinst     "System.Exception"
  IL_00d6:  ldnull
  IL_00d7:  cgt.un
  IL_00d9:  ldloc.0
  IL_00da:  ldc.i4.0
  IL_00db:  cgt.un
  IL_00dd:  and
  IL_00de:  ldloc.1
  IL_00df:  ldc.i4.0
  IL_00e0:  ceq
  IL_00e2:  and
  IL_00e3:  endfilter
}  // end filter
{  // handler
  IL_00e5:  castclass  "System.Exception"
  IL_00ea:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ef:  leave.s    IL_00bb
}
  IL_00f1:  ldc.i4     0x800a0033
  IL_00f6:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00fb:  throw
  IL_00fc:  ldloc.1
  IL_00fd:  brfalse.s  IL_0104
  IL_00ff:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0104:  ret
}]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Sub OnError_WithSyncLock_1()
            'This is the typical scenario documented in the spec to ensure that infinite
            'recursion does not occur
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Class LockClass
End Class

Module Module1
    Sub Main()
        Dim firsttime As Boolean = True
        Dim lock As New LockClass

        On Error GoTo handler

        SyncLock lock
            Console.WriteLine("Before Exception")
            Throw New Exception()
            Console.WriteLine("AfterException")
        End SyncLock
        Exit Sub
handler:
        If firsttime Then
            firsttime = False
            Resume
        Else
            Resume Next
        End If
    End Sub
End Module   
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation, expectedOutput:="Before Exception" & Environment.NewLine & "Before Exception")


            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[
{
  // Code size      260 (0x104)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Boolean V_3, //firsttime
  LockClass V_4, //lock
  Object V_5, //VB$Lock
  Boolean V_6) //VB$LockTaken
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldc.i4.1
  IL_0003:  stloc.3
  IL_0004:  ldc.i4.2
  IL_0005:  stloc.2
  IL_0006:  newobj     "Sub LockClass..ctor()"
  IL_000b:  stloc.s    V_4
  IL_000d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0012:  ldc.i4.2
  IL_0013:  stloc.0
  IL_0014:  ldc.i4.4
  IL_0015:  stloc.2
  IL_0016:  ldloc.s    V_4
  IL_0018:  stloc.s    V_5
  IL_001a:  ldc.i4.0
  IL_001b:  stloc.s    V_6
  .try
{
  IL_001d:  ldloc.s    V_5
  IL_001f:  ldloca.s   V_6
  IL_0021:  call       "Sub System.Threading.Monitor.Enter(Object, ByRef Boolean)"
  IL_0026:  ldstr      "Before Exception"
  IL_002b:  call       "Sub System.Console.WriteLine(String)"
  IL_0030:  newobj     "Sub System.Exception..ctor()"
  IL_0035:  throw
}
  finally
{
  IL_0036:  ldloc.s    V_6
  IL_0038:  brfalse.s  IL_0041
  IL_003a:  ldloc.s    V_5
  IL_003c:  call       "Sub System.Threading.Monitor.Exit(Object)"
  IL_0041:  endfinally
}
  IL_0042:  ldc.i4.6
  IL_0043:  stloc.2
  IL_0044:  ldloc.3
  IL_0045:  brfalse.s  IL_0060
  IL_0047:  ldc.i4.7
  IL_0048:  stloc.2
  IL_0049:  ldc.i4.0
  IL_004a:  stloc.3
  IL_004b:  ldc.i4.8
  IL_004c:  stloc.2
  IL_004d:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0052:  ldloc.1
  IL_0053:  brtrue.s   IL_007b
  IL_0055:  ldc.i4     0x800a0014
  IL_005a:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_005f:  throw
  IL_0060:  ldc.i4.s   10
  IL_0062:  stloc.2
  IL_0063:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0068:  ldloc.1
  IL_0069:  brtrue.s   IL_007e
  IL_006b:  ldc.i4     0x800a0014
  IL_0070:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0075:  throw
  IL_0076:  leave      IL_00fb
  IL_007b:  ldloc.1
  IL_007c:  br.s       IL_0081
  IL_007e:  ldloc.1
  IL_007f:  ldc.i4.1
  IL_0080:  add
  IL_0081:  ldc.i4.0
  IL_0082:  stloc.1
  IL_0083:  switch    (
  IL_00b8,
  IL_0000,
  IL_0004,
  IL_000d,
  IL_0014,
  IL_0076,
  IL_0042,
  IL_0047,
  IL_004b,
  IL_0076,
  IL_0060,
  IL_0076)
  IL_00b8:  leave.s    IL_00f0
  IL_00ba:  ldloc.2
  IL_00bb:  stloc.1
  IL_00bc:  ldloc.0
  IL_00bd:  switch    (
  IL_00ce,
  IL_007e,
  IL_0042)
  IL_00ce:  leave.s    IL_00f0
}
  filter
{
  IL_00d0:  isinst     "System.Exception"
  IL_00d5:  ldnull
  IL_00d6:  cgt.un
  IL_00d8:  ldloc.0
  IL_00d9:  ldc.i4.0
  IL_00da:  cgt.un
  IL_00dc:  and
  IL_00dd:  ldloc.1
  IL_00de:  ldc.i4.0
  IL_00df:  ceq
  IL_00e1:  and
  IL_00e2:  endfilter
}  // end filter
{  // handler
  IL_00e4:  castclass  "System.Exception"
  IL_00e9:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ee:  leave.s    IL_00ba
}
  IL_00f0:  ldc.i4     0x800a0033
  IL_00f5:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00fa:  throw
  IL_00fb:  ldloc.1
  IL_00fc:  brfalse.s  IL_0103
  IL_00fe:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0103:  ret
}
]]>)
        End Sub


        <Fact()>
        Sub OnError_WithSyncLock_2()
            'This needs to baseline only because of infinite recursion issue
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Class LockClass
End Class

Module Module1
        Sub Main()
        Dim firsttime As Boolean = True
        Dim lock As New LockClass

        On Error GoTo handler

        SyncLock lock
            Console.WriteLine("Before Exception")
            Throw New Exception()
            Console.WriteLine("AfterException")
        End SyncLock
        Exit Sub
handler:
        Resume  'result in infinite recursion
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim compilationVerifier = CompileAndVerify(compilation)

            compilationVerifier.VerifyIL("Module1.Main", <![CDATA[
{
  // Code size      206 (0xce)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                LockClass V_3, //lock
                Object V_4,
                Boolean V_5)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  ldc.i4.2
  IL_0003:  stloc.2
  IL_0004:  newobj     "Sub LockClass..ctor()"
  IL_0009:  stloc.3
  IL_000a:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_000f:  ldc.i4.2
  IL_0010:  stloc.0
  IL_0011:  ldc.i4.4
  IL_0012:  stloc.2
  IL_0013:  ldloc.3
  IL_0014:  stloc.s    V_4
  IL_0016:  ldc.i4.0
  IL_0017:  stloc.s    V_5
  .try
{
  IL_0019:  ldloc.s    V_4
  IL_001b:  ldloca.s   V_5
  IL_001d:  call       "Sub System.Threading.Monitor.Enter(Object, ByRef Boolean)"
  IL_0022:  ldstr      "Before Exception"
  IL_0027:  call       "Sub System.Console.WriteLine(String)"
  IL_002c:  newobj     "Sub System.Exception..ctor()"
  IL_0031:  throw
}
  finally
{
  IL_0032:  ldloc.s    V_5
  IL_0034:  brfalse.s  IL_003d
  IL_0036:  ldloc.s    V_4
  IL_0038:  call       "Sub System.Threading.Monitor.Exit(Object)"
  IL_003d:  endfinally
}
  IL_003e:  ldc.i4.6
  IL_003f:  stloc.2
  IL_0040:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0045:  ldloc.1
  IL_0046:  brtrue.s   IL_0055
  IL_0048:  ldc.i4     0x800a0014
  IL_004d:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0052:  throw
  IL_0053:  leave.s    IL_00c5
  IL_0055:  ldloc.1
  IL_0056:  br.s       IL_005b
  IL_0058:  ldloc.1
  IL_0059:  ldc.i4.1
  IL_005a:  add
  IL_005b:  ldc.i4.0
  IL_005c:  stloc.1
  IL_005d:  switch    (
  IL_0082,
  IL_0000,
  IL_0002,
  IL_000a,
  IL_0011,
  IL_0053,
  IL_003e,
  IL_0053)
  IL_0082:  leave.s    IL_00ba
  IL_0084:  ldloc.2
  IL_0085:  stloc.1
  IL_0086:  ldloc.0
  IL_0087:  switch    (
  IL_0098,
  IL_0058,
  IL_003e)
  IL_0098:  leave.s    IL_00ba
}
  filter
{
  IL_009a:  isinst     "System.Exception"
  IL_009f:  ldnull
  IL_00a0:  cgt.un
  IL_00a2:  ldloc.0
  IL_00a3:  ldc.i4.0
  IL_00a4:  cgt.un
  IL_00a6:  and
  IL_00a7:  ldloc.1
  IL_00a8:  ldc.i4.0
  IL_00a9:  ceq
  IL_00ab:  and
  IL_00ac:  endfilter
}  // end filter
{  // handler
  IL_00ae:  castclass  "System.Exception"
  IL_00b3:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b8:  leave.s    IL_0084
}
  IL_00ba:  ldc.i4     0x800a0033
  IL_00bf:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c4:  throw
  IL_00c5:  ldloc.1
  IL_00c6:  brfalse.s  IL_00cd
  IL_00c8:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00cd:  ret
}
]]>)
        End Sub

        <Fact()>
        Sub OnError_WithSyncLock_3()
            'This verifies that resume next will resume outside the sync block
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Class LockClass
End Class

Module Module1
    Sub Main()
        'This will not resume after the exception but rather outside of the syncLock block
        Dim lock As New LockClass

        On Error GoTo handler

        SyncLock lock
            Console.WriteLine("Before Exception")
            Throw New Exception()
            Console.WriteLine("AfterException")
        End SyncLock
        Exit Sub
handler:
        Resume Next
    End Sub
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim CompilationVerifier = CompileAndVerify(compilation, expectedOutput:="Before Exception")

            CompilationVerifier.VerifyIL("Module1.Main", <![CDATA[
{
  // Code size      197 (0xc5)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                LockClass V_3, //lock
                Object V_4,
                Boolean V_5)
  .try
{
  IL_0000:  ldc.i4.1
  IL_0001:  stloc.2
  IL_0002:  newobj     "Sub LockClass..ctor()"
  IL_0007:  stloc.3
  IL_0008:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_000d:  ldc.i4.2
  IL_000e:  stloc.0
  IL_000f:  ldc.i4.3
  IL_0010:  stloc.2
  IL_0011:  ldloc.3
  IL_0012:  stloc.s    V_4
  IL_0014:  ldc.i4.0
  IL_0015:  stloc.s    V_5
  .try
{
  IL_0017:  ldloc.s    V_4
  IL_0019:  ldloca.s   V_5
  IL_001b:  call       "Sub System.Threading.Monitor.Enter(Object, ByRef Boolean)"
  IL_0020:  ldstr      "Before Exception"
  IL_0025:  call       "Sub System.Console.WriteLine(String)"
  IL_002a:  newobj     "Sub System.Exception..ctor()"
  IL_002f:  throw
}
  finally
{
  IL_0030:  ldloc.s    V_5
  IL_0032:  brfalse.s  IL_003b
  IL_0034:  ldloc.s    V_4
  IL_0036:  call       "Sub System.Threading.Monitor.Exit(Object)"
  IL_003b:  endfinally
}
  IL_003c:  ldc.i4.5
  IL_003d:  stloc.2
  IL_003e:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0043:  ldloc.1
  IL_0044:  brtrue.s   IL_0053
  IL_0046:  ldc.i4     0x800a0014
  IL_004b:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0050:  throw
  IL_0051:  leave.s    IL_00bc
  IL_0053:  ldloc.1
  IL_0054:  ldc.i4.1
  IL_0055:  add
  IL_0056:  ldc.i4.0
  IL_0057:  stloc.1
  IL_0058:  switch    (
  IL_0079,
  IL_0000,
  IL_0008,
  IL_000f,
  IL_0051,
  IL_003c,
  IL_0051)
  IL_0079:  leave.s    IL_00b1
  IL_007b:  ldloc.2
  IL_007c:  stloc.1
  IL_007d:  ldloc.0
  IL_007e:  switch    (
  IL_008f,
  IL_0053,
  IL_003c)
  IL_008f:  leave.s    IL_00b1
}
  filter
{
  IL_0091:  isinst     "System.Exception"
  IL_0096:  ldnull
  IL_0097:  cgt.un
  IL_0099:  ldloc.0
  IL_009a:  ldc.i4.0
  IL_009b:  cgt.un
  IL_009d:  and
  IL_009e:  ldloc.1
  IL_009f:  ldc.i4.0
  IL_00a0:  ceq
  IL_00a2:  and
  IL_00a3:  endfilter
}  // end filter
{  // handler
  IL_00a5:  castclass  "System.Exception"
  IL_00aa:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00af:  leave.s    IL_007b
}
  IL_00b1:  ldc.i4     0x800a0033
  IL_00b6:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00bb:  throw
  IL_00bc:  ldloc.1
  IL_00bd:  brfalse.s  IL_00c4
  IL_00bf:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c4:  ret
}
]]>)
        End Sub

        <Fact(Skip:="1005639"), WorkItem(1005639)>
        Sub OnError_ResumeWithConditionBlocks()
            'This verifies that resume next will resume inside the IF block when an error occurs in the
            'IF statement / ELSEIF and also and multiple condition IF With ANDALSO
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        IFBlock_1()
        IFBlock()
        ElseiFBlock_With_ANDALSO()
    End Sub

    Public Sub IFBlock_1()
        On Error Resume Next
        Console.WriteLine("*********************************************")
        Console.WriteLine("IF Block(1) Test")
        If Foo_IF() Then
            Console.WriteLine("If Block")
        Else
            Console.WriteLine("Else Block")
        End If
        Console.WriteLine("End")
    End Sub

    Function Foo_IF() As Boolean
        Error 5
    End Function

    Public Sub IFBlock()
        'Will it go if block or somewhere else.
        On Error Resume Next
        Console.WriteLine("*********************************************")
        Console.WriteLine("IF Block Test")
        If FooIF(0) Then
            Console.WriteLine("If Block")
        ElseIf FooIF(2) Then 'Cause and Error
            Console.WriteLine("ElseIf Block")
        Else
            Console.WriteLine("Else Block")
        End If
        Console.WriteLine("End")
    End Sub

    Function FooIF(i As Integer) As Boolean
        If i = 0 Then
            Return False
        ElseIf i = 1 Then
            Return True
        Else
            Error 5
        End If
    End Function

    Public Sub ElseiFBlock_With_ANDALSO()
        On Error Resume Next

        Dim a = 1
        Console.WriteLine("*********************************************")
        Console.WriteLine("ELSEIF Block Test")
        If a = 1 AndAlso FooELSEIF(0) Then
            Console.WriteLine("If Block")
        ElseIf a = 1 AndAlso FooELSEIF(2) Then 'Cause and Error
            Console.WriteLine("ElseIf Block")
        Else
            Console.WriteLine("Else Block")
        End If
        Console.WriteLine("End")
    End Sub

    Function FooELSEIF(i As Integer) As Boolean
        If i = 0 Then
            Return False
        ElseIf i = 1 Then
            Return True
        Else
            Error 5
        End If
    End Function
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim CompilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[*********************************************
IF Block(1) Test
If Block
End
*********************************************
IF Block Test
ElseIf Block
End
*********************************************
ELSEIF Block Test
ElseIf Block
End]]>)

            'Check IF For the resume points
            CompilationVerifier.VerifyIL("Module1.IFBlock_1", <![CDATA[{
  // Code size      202 (0xca)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "*********************************************"
  IL_000e:  call       "Sub System.Console.WriteLine(String)"
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  ldstr      "IF Block(1) Test"
  IL_001a:  call       "Sub System.Console.WriteLine(String)"
  IL_001f:  ldc.i4.4
  IL_0020:  stloc.2
  IL_0021:  call       "Function Module1.Foo_IF() As Boolean"
  IL_0026:  brfalse.s  IL_0036
  IL_0028:  ldc.i4.5
  IL_0029:  stloc.2
  IL_002a:  ldstr      "If Block"
  IL_002f:  call       "Sub System.Console.WriteLine(String)"
  IL_0034:  br.s       IL_0042
  IL_0036:  ldc.i4.7
  IL_0037:  stloc.2
  IL_0038:  ldstr      "Else Block"
  IL_003d:  call       "Sub System.Console.WriteLine(String)"
  IL_0042:  ldc.i4.8
  IL_0043:  stloc.2
  IL_0044:  ldstr      "End"
  IL_0049:  call       "Sub System.Console.WriteLine(String)"
  IL_004e:  leave.s    IL_00c1
  IL_0050:  ldloc.1
  IL_0051:  ldc.i4.1
  IL_0052:  add
  IL_0053:  ldc.i4.0
  IL_0054:  stloc.1
  IL_0055:  switch    (
  IL_0082,
  IL_0000,
  IL_0007,
  IL_0013,
  IL_001f,
  IL_0028,
  IL_0042,
  IL_0036,
  IL_0042,
  IL_004e)
  IL_0082:  leave.s    IL_00b6
  IL_0084:  ldloc.2
  IL_0085:  stloc.1
  IL_0086:  ldloc.0
  IL_0087:  switch    (
  IL_0094,
  IL_0050)
  IL_0094:  leave.s    IL_00b6
}
  filter
{
  IL_0096:  isinst     "System.Exception"
  IL_009b:  ldnull
  IL_009c:  cgt.un
  IL_009e:  ldloc.0
  IL_009f:  ldc.i4.0
  IL_00a0:  cgt.un
  IL_00a2:  and
  IL_00a3:  ldloc.1
  IL_00a4:  ldc.i4.0
  IL_00a5:  ceq
  IL_00a7:  and
  IL_00a8:  endfilter
}  // end filter
{  // handler
  IL_00aa:  castclass  "System.Exception"
  IL_00af:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00b4:  leave.s    IL_0084
}
  IL_00b6:  ldc.i4     0x800a0033
  IL_00bb:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00c0:  throw
  IL_00c1:  ldloc.1
  IL_00c2:  brfalse.s  IL_00c9
  IL_00c4:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00c9:  ret
}]]>)
            CompilationVerifier.VerifyIL("Module1.IFBlock", <![CDATA[{
  // Code size      241 (0xf1)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2) //VB$CurrentStatement
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "*********************************************"
  IL_000e:  call       "Sub System.Console.WriteLine(String)"
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  ldstr      "IF Block Test"
  IL_001a:  call       "Sub System.Console.WriteLine(String)"
  IL_001f:  ldc.i4.4
  IL_0020:  stloc.2
  IL_0021:  ldc.i4.0
  IL_0022:  call       "Function Module1.FooIF(Integer) As Boolean"
  IL_0027:  brfalse.s  IL_0037
  IL_0029:  ldc.i4.5
  IL_002a:  stloc.2
  IL_002b:  ldstr      "If Block"
  IL_0030:  call       "Sub System.Console.WriteLine(String)"
  IL_0035:  br.s       IL_005c
  IL_0037:  ldc.i4.7
  IL_0038:  stloc.2
  IL_0039:  ldc.i4.2
  IL_003a:  call       "Function Module1.FooIF(Integer) As Boolean"
  IL_003f:  brfalse.s  IL_004f
  IL_0041:  ldc.i4.8
  IL_0042:  stloc.2
  IL_0043:  ldstr      "ElseIf Block"
  IL_0048:  call       "Sub System.Console.WriteLine(String)"
  IL_004d:  br.s       IL_005c
  IL_004f:  ldc.i4.s   10
  IL_0051:  stloc.2
  IL_0052:  ldstr      "Else Block"
  IL_0057:  call       "Sub System.Console.WriteLine(String)"
  IL_005c:  ldc.i4.s   11
  IL_005e:  stloc.2
  IL_005f:  ldstr      "End"
  IL_0064:  call       "Sub System.Console.WriteLine(String)"
  IL_0069:  leave.s    IL_00e8
  IL_006b:  ldloc.1
  IL_006c:  ldc.i4.1
  IL_006d:  add
  IL_006e:  ldc.i4.0
  IL_006f:  stloc.1
  IL_0070:  switch    (
  IL_00a9,
  IL_0000,
  IL_0007,
  IL_0013,
  IL_001f,
  IL_0029,
  IL_005c,
  IL_0037,
  IL_0041,
  IL_005c,
  IL_004f,
  IL_005c,
  IL_0069)
  IL_00a9:  leave.s    IL_00dd
  IL_00ab:  ldloc.2
  IL_00ac:  stloc.1
  IL_00ad:  ldloc.0
  IL_00ae:  switch    (
  IL_00bb,
  IL_006b)
  IL_00bb:  leave.s    IL_00dd
}
  filter
{
  IL_00bd:  isinst     "System.Exception"
  IL_00c2:  ldnull
  IL_00c3:  cgt.un
  IL_00c5:  ldloc.0
  IL_00c6:  ldc.i4.0
  IL_00c7:  cgt.un
  IL_00c9:  and
  IL_00ca:  ldloc.1
  IL_00cb:  ldc.i4.0
  IL_00cc:  ceq
  IL_00ce:  and
  IL_00cf:  endfilter
}  // end filter
{  // handler
  IL_00d1:  castclass  "System.Exception"
  IL_00d6:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00db:  leave.s    IL_00ab
}
  IL_00dd:  ldc.i4     0x800a0033
  IL_00e2:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00e7:  throw
  IL_00e8:  ldloc.1
  IL_00e9:  brfalse.s  IL_00f0
  IL_00eb:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_00f0:  ret
}]]>)
            CompilationVerifier.VerifyIL("Module1.ElseiFBlock_With_ANDALSO", <![CDATA[{
  // Code size      261 (0x105)
  .maxstack  3
  .locals init (Integer V_0, //VB$ActiveHandler
  Integer V_1, //VB$ResumeTarget
  Integer V_2, //VB$CurrentStatement
  Integer V_3) //a
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.1
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldc.i4.1
  IL_000a:  stloc.3
  IL_000b:  ldc.i4.3
  IL_000c:  stloc.2
  IL_000d:  ldstr      "*********************************************"
  IL_0012:  call       "Sub System.Console.WriteLine(String)"
  IL_0017:  ldc.i4.4
  IL_0018:  stloc.2
  IL_0019:  ldstr      "ELSEIF Block Test"
  IL_001e:  call       "Sub System.Console.WriteLine(String)"
  IL_0023:  ldc.i4.5
  IL_0024:  stloc.2
  IL_0025:  ldloc.3
  IL_0026:  ldc.i4.1
  IL_0027:  bne.un.s   IL_003f
  IL_0029:  ldc.i4.0
  IL_002a:  call       "Function Module1.FooELSEIF(Integer) As Boolean"
  IL_002f:  brfalse.s  IL_003f
  IL_0031:  ldc.i4.6
  IL_0032:  stloc.2
  IL_0033:  ldstr      "If Block"
  IL_0038:  call       "Sub System.Console.WriteLine(String)"
  IL_003d:  br.s       IL_0069
  IL_003f:  ldc.i4.8
  IL_0040:  stloc.2
  IL_0041:  ldloc.3
  IL_0042:  ldc.i4.1
  IL_0043:  bne.un.s   IL_005c
  IL_0045:  ldc.i4.2
  IL_0046:  call       "Function Module1.FooELSEIF(Integer) As Boolean"
  IL_004b:  brfalse.s  IL_005c
  IL_004d:  ldc.i4.s   9
  IL_004f:  stloc.2
  IL_0050:  ldstr      "ElseIf Block"
  IL_0055:  call       "Sub System.Console.WriteLine(String)"
  IL_005a:  br.s       IL_0069
  IL_005c:  ldc.i4.s   11
  IL_005e:  stloc.2
  IL_005f:  ldstr      "Else Block"
  IL_0064:  call       "Sub System.Console.WriteLine(String)"
  IL_0069:  ldc.i4.s   12
  IL_006b:  stloc.2
  IL_006c:  ldstr      "End"
  IL_0071:  call       "Sub System.Console.WriteLine(String)"
  IL_0076:  leave      IL_00fc
  IL_007b:  ldloc.1
  IL_007c:  ldc.i4.1
  IL_007d:  add
  IL_007e:  ldc.i4.0
  IL_007f:  stloc.1
  IL_0080:  switch    (
  IL_00bd,
  IL_0000,
  IL_0007,
  IL_000b,
  IL_0017,
  IL_0023,
  IL_0031,
  IL_0069,
  IL_003f,
  IL_004d,
  IL_0069,
  IL_005c,
  IL_0069,
  IL_0076)
  IL_00bd:  leave.s    IL_00f1
  IL_00bf:  ldloc.2
  IL_00c0:  stloc.1
  IL_00c1:  ldloc.0
  IL_00c2:  switch    (
  IL_00cf,
  IL_007b)
  IL_00cf:  leave.s    IL_00f1
}
  filter
{
  IL_00d1:  isinst     "System.Exception"
  IL_00d6:  ldnull
  IL_00d7:  cgt.un
  IL_00d9:  ldloc.0
  IL_00da:  ldc.i4.0
  IL_00db:  cgt.un
  IL_00dd:  and
  IL_00de:  ldloc.1
  IL_00df:  ldc.i4.0
  IL_00e0:  ceq
  IL_00e2:  and
  IL_00e3:  endfilter
}  // end filter
{  // handler
  IL_00e5:  castclass  "System.Exception"
  IL_00ea:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_00ef:  leave.s    IL_00bf
}
  IL_00f1:  ldc.i4     0x800a0033
  IL_00f6:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_00fb:  throw
  IL_00fc:  ldloc.1
  IL_00fd:  brfalse.s  IL_0104
  IL_00ff:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0104:  ret
}]]>)
        End Sub

        <Fact()>
        Sub OnError_ResumeWithSelectCase()
            'This verifies that resume next will resumes outside the select caser block rather than at the next case statement or within the block            
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        On Error GoTo handler
        Console.WriteLine("Before Select")
        Select Case foo
            Case 1
                Console.WriteLine("1")
            Case 2
                Console.WriteLine("2")
            Case Else
                Console.WriteLine("else")
        End Select
        Console.WriteLine("After Case")
        Exit Sub
handler:
        Console.WriteLine("In Handler")
        Resume Next
    End Sub

    Function foo()
        Error 1
    End Function
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim CompilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Before Select
In Handler
After Case]]>)

            'Check IF For the resume points
            CompilationVerifier.VerifyIL("Module1.Main", <![CDATA[{
  // Code size      315 (0x13b)
  .maxstack  3
  .locals init (Integer V_0,
                Integer V_1,
                Integer V_2,
                Object V_3)
  .try
{
  IL_0000:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_0005:  ldc.i4.2
  IL_0006:  stloc.0
  IL_0007:  ldc.i4.2
  IL_0008:  stloc.2
  IL_0009:  ldstr      "Before Select"
  IL_000e:  call       "Sub System.Console.WriteLine(String)"
  IL_0013:  ldc.i4.3
  IL_0014:  stloc.2
  IL_0015:  call       "Function Module1.foo() As Object"
  IL_001a:  stloc.3
  IL_001b:  ldc.i4.5
  IL_001c:  stloc.2
  IL_001d:  ldloc.3
  IL_001e:  ldc.i4.1
  IL_001f:  box        "Integer"
  IL_0024:  ldc.i4.0
  IL_0025:  call       "Function Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(Object, Object, Boolean) As Boolean"
  IL_002a:  brfalse.s  IL_003a
  IL_002c:  ldc.i4.6
  IL_002d:  stloc.2
  IL_002e:  ldstr      "1"
  IL_0033:  call       "Sub System.Console.WriteLine(String)"
  IL_0038:  br.s       IL_0067
  IL_003a:  ldc.i4.8
  IL_003b:  stloc.2
  IL_003c:  ldloc.3
  IL_003d:  ldc.i4.2
  IL_003e:  box        "Integer"
  IL_0043:  ldc.i4.0
  IL_0044:  call       "Function Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(Object, Object, Boolean) As Boolean"
  IL_0049:  brfalse.s  IL_005a
  IL_004b:  ldc.i4.s   9
  IL_004d:  stloc.2
  IL_004e:  ldstr      "2"
  IL_0053:  call       "Sub System.Console.WriteLine(String)"
  IL_0058:  br.s       IL_0067
  IL_005a:  ldc.i4.s   11
  IL_005c:  stloc.2
  IL_005d:  ldstr      "else"
  IL_0062:  call       "Sub System.Console.WriteLine(String)"
  IL_0067:  ldc.i4.s   12
  IL_0069:  stloc.2
  IL_006a:  ldstr      "After Case"
  IL_006f:  call       "Sub System.Console.WriteLine(String)"
  IL_0074:  leave      IL_0132
  IL_0079:  ldc.i4.s   14
  IL_007b:  stloc.2
  IL_007c:  ldstr      "In Handler"
  IL_0081:  call       "Sub System.Console.WriteLine(String)"
  IL_0086:  ldc.i4.s   15
  IL_0088:  stloc.2
  IL_0089:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_008e:  ldloc.1
  IL_008f:  brtrue.s   IL_00a1
  IL_0091:  ldc.i4     0x800a0014
  IL_0096:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_009b:  throw
  IL_009c:  leave      IL_0132
  IL_00a1:  ldloc.1
  IL_00a2:  ldc.i4.1
  IL_00a3:  add
  IL_00a4:  ldc.i4.0
  IL_00a5:  stloc.1
  IL_00a6:  switch    (
  IL_00ef,
  IL_0000,
  IL_0007,
  IL_0013,
  IL_0067,
  IL_001b,
  IL_002c,
  IL_0067,
  IL_003a,
  IL_004b,
  IL_0067,
  IL_005a,
  IL_0067,
  IL_009c,
  IL_0079,
  IL_0086,
  IL_009c)
  IL_00ef:  leave.s    IL_0127
  IL_00f1:  ldloc.2
  IL_00f2:  stloc.1
  IL_00f3:  ldloc.0
  IL_00f4:  switch    (
  IL_0105,
  IL_00a1,
  IL_0079)
  IL_0105:  leave.s    IL_0127
}
  filter
{
  IL_0107:  isinst     "System.Exception"
  IL_010c:  ldnull
  IL_010d:  cgt.un
  IL_010f:  ldloc.0
  IL_0110:  ldc.i4.0
  IL_0111:  cgt.un
  IL_0113:  and
  IL_0114:  ldloc.1
  IL_0115:  ldc.i4.0
  IL_0116:  ceq
  IL_0118:  and
  IL_0119:  endfilter
}  // end filter
{  // handler
  IL_011b:  castclass  "System.Exception"
  IL_0120:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.SetProjectError(System.Exception)"
  IL_0125:  leave.s    IL_00f1
}
  IL_0127:  ldc.i4     0x800a0033
  IL_012c:  call       "Function Microsoft.VisualBasic.CompilerServices.ProjectData.CreateProjectError(Integer) As System.Exception"
  IL_0131:  throw
  IL_0132:  ldloc.1
  IL_0133:  brfalse.s  IL_013a
  IL_0135:  call       "Sub Microsoft.VisualBasic.CompilerServices.ProjectData.ClearProjectError()"
  IL_013a:  ret
}]]>)

        End Sub

        <Fact()>
        Sub OnError_ResumeWithSelectCase_Error_On_Case()
            'This verifies that resume next will resumes 
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[Imports System

        Module Module1
            Sub Main()
                'Will Go In Block, even though error occurred in case statement 
                On Error GoTo handler
                Dim i = 1
                Console.WriteLine("Before Select")
                Select Case i
                    Case foo()
                        Console.WriteLine("1")
                    Case 2
                        Console.WriteLine("2")
                    Case Else
                        Console.WriteLine("else")
                End Select
                Console.WriteLine("After Case")
                Exit Sub
handler:
                Console.WriteLine("In Handler")
                Resume Next
            End Sub

            Function foo()
                Error 1
            End Function
        End Module


        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim CompilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Before Select
In Handler
1
After Case]]>)

        End Sub

        <Fact()>
        Sub OnError_ResumeWithSelectCase_Error_On_Case_Condition()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[Imports System

        Module Module1
            Sub Main()
                'Will Go In Block, even though error occurred in case statement 
                On Error GoTo handler
                Dim i = 1
                Console.WriteLine("Before Select")
                Select Case i
                    Case foo()
                        Console.WriteLine("1")
                    Case 2
                        Console.WriteLine("2")
                    Case Else
                        Console.WriteLine("else")
                End Select
                Console.WriteLine("After Case")
                Exit Sub
handler:
                Console.WriteLine("In Handler")
                Resume Next
            End Sub

            Function foo()
                Error 1
            End Function
        End Module


        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim CompilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Before Select
In Handler
1
After Case]]>)
        End Sub

        <Fact()>
        Sub OnError_ResumeWithSelectCase_Error_On_Case_Condition_Multiple()

            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    'Will Go In Block, even though error occurred 
    'It Will not try and evaluate additional items in case
    Sub Main()
        On Error GoTo handler
        Dim i = 1
        Console.WriteLine("Before Select")
        Select Case i
            Case i = foo(), 1
                Console.WriteLine("1")
            Case 2
                Console.WriteLine("2")
            Case Else
                Console.WriteLine("else")
        End Select
        Console.WriteLine("After Case")
        Exit Sub
handler:
        Console.WriteLine("In Handler")
        Resume Next
    End Sub

    Function foo()
        Error 1
    End Function
End Module
        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            Dim CompilationVerifier = CompileAndVerify(compilation, expectedOutput:=<![CDATA[Before Select
In Handler
1
After Case]]>)

        End Sub

        <Fact()>
        Sub ErrorOject()
            'We are causing the error by throwing an exception or by using the Error or Err.Raise
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System
Imports Microsoft.VisualBasic

Module Module1
    Sub Main()
        ByException()
        ByError()
        ByErrorRaise()
    End Sub

    Sub ByException()
        On Error GoTo Handler
        Console.Write("Start")
        Throw New DivideByZeroException()
        Exit Sub
Handler:
        Console.Write("Handler")
        Console.Write(Err.Number)
        Console.Write(Err.Description)
    End Sub

    Sub ByError()
        On Error GoTo Handler
        Console.Write("Start")
        Error 11
        Exit Sub
Handler:
        Console.Write("Handler")
        Console.Write(Err.Number)
        Console.Write(Err.Description)
    End Sub

Sub ByErrorRaise()
        On Error GoTo Handler
        Console.Write("Start")
        Err.Raise(12)
        Exit Sub
Handler:
        Console.Write("Handler")
        Console.Write(Err.Number)
        Console.Write(Err.Description)
    End Sub
End Module

        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[StartHandler11Attempted to divide by zero.StartHandler11Division by zero.StartHandler12Application-defined or object-defined error.]]>)
        End Sub

        <Fact()>
        Sub ErrorHandler_ResumeNext_ErrorObjectFunctionality()
            Dim compilationDef =
    <compilation name="ErrorHandlerTest">
        <file name="a.vb">
Imports System
Imports Microsoft.VisualBasic

Module Module1
    Sub Main()
        Dim Msg As String
        ' If an error occurs, construct an error message.
        On Error Resume Next   ' Defer error handling.

        Err.Clear()

        Err.Raise(6)   ' Generate an "Overflow" error.
        ' Check for error, then show message.

        Msg = "Error # " &amp; Str(Err.Number) &amp; " was generated by " &amp; Err.Source &amp; ControlChars.CrLf &amp; Err.Description
        Console.WriteLine(Msg)

        Err.Clear()
        Msg = "Error # " &amp; Str(Err.Number) &amp; " was generated by " &amp; Err.Source &amp; ControlChars.CrLf &amp; Err.Description
        Console.WriteLine(Msg)

        Err.Raise(5)   ' Generate an "Overflow" error.
        Msg = "Error # " &amp; Str(Err.Number) &amp; " was generated by " &amp; Err.Source &amp; ControlChars.CrLf &amp; Err.Description
        Console.WriteLine(Msg)
    End Sub
End Module
</file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(compilationDef, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[Error #  6 was generated by ErrorHandlerTest
Overflow.
Error #  0 was generated by 

Error #  5 was generated by ErrorHandlerTest
Procedure call or argument is not valid.
]]>)
        End Sub

        <Fact()>
        Sub ErrorHandler_InCollectionInitializer()
            'As we are handling the error in the Add, we should handle two items to the collection
            'In other collection initializer we would result in all or nothing behaviour
            Dim compilationDef =
    <compilation name="ErrorHandlerTest">
        <file name="a.vb">
Imports System
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

'Used my own attribute for Extesnion attribute based upon necessary signature rather than adding a specific reference to 
'System.Core which contains this normally

Namespace System.Runtime.CompilerServices
    &lt;AttributeUsage(AttributeTargets.Assembly Or AttributeTargets.Class Or AttributeTargets.Method, AllowMultiple:=False, Inherited:=False)&gt; Class ExtensionAttribute
        Inherits Attribute
    End Class
End Namespace

Module Module1
    Sub Main()
        Dim x As New System.Collections.Generic.Stack(Of Integer) From {1, 2, 3}
        Console.WriteLine(x.Count)
    End Sub

    &lt;System.Runtime.CompilerServices.Extension&gt;
    Sub Add(ByRef x As System.Collections.Generic.Stack(Of Integer), y As Integer)
        On Error GoTo handler
        If y = 2 Then
            Error 1
        End If
        x.Push(y)
        Exit Sub
handler:
        Exit Sub
    End Sub
End Module
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(compilationDef, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="2")
        End Sub

        <Fact()>
        Sub ErrorHandler_InCollectionInitializer_2()
            'As we are handling the error in the Add, we should handle two items to the collection
            'This should result in Nothing - All or nothing 
            Dim compilationDef =
    <compilation name="ErrorHandlerTest">
        <file name="a.vb">
Imports System
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic


Module Module1
    Sub Main()
        On Error Resume Next
        Dim lval = Long.MaxValue
        Dim x As New List(Of Integer) From {1, 2, lval}
        Console.WriteLine(If(x Is Nothing, "Nothing", x.Count.ToString))
    End Sub
End Module
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(compilationDef, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="Nothing")
        End Sub

        <Fact()>
        Sub ErrorHandler_InCollectionInitializer_3()
            'As we are handling the error in the Add, we should handle two items to the collection
            'This should result in Nothing - All or nothing 
            Dim compilationDef =
    <compilation name="ErrorHandlerTest">
        <file name="a.vb">
Imports System
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Module Module1
    Sub Main()
        On Error Goto Handler
        Dim lval = Long.MaxValue
        Dim x As New List(Of Integer) From {1, 2, lval}
        Console.WriteLine(If(x Is Nothing, "Nothing", x.Count.ToString))
        exit sub
Handler:
    Resume Next
    End Sub
End Module
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(compilationDef, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:="Nothing")
        End Sub

        'As we are handling the error in the Add, we should handle two items to the collection
        'This should result in Nothing - All or nothing 
        Sub ErrorObjectResetAfterExitXXX_statement()
            'Error Object is reset after Exit Sub/Exit Function/Exit Property or Resume Next in the handler
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System

Module Module1
    Sub Main()
        Console.WriteLine("Method Resume Next")
        Err.Clear()
        Console.WriteLine("main:(Before)" & Err.Number)
        ErrorMethod()
        Console.WriteLine("main:(After)" & Err.Number)

        Console.WriteLine("Method Goto")
        Err.Clear()
        Console.WriteLine("main:(Before)" & Err.Number)
        Console.WriteLine(Err.Number)
        ErrorMethod_2()
        Console.WriteLine("main:(After)" & Err.Number)
        Console.WriteLine(Err.Number)

        Console.WriteLine("Method Resume Next")
        Err.Clear()
        Console.WriteLine("main:(Before)" & Err.Number)
        Console.WriteLine(Err.Number)
        ErrorMethod_3()
        Console.WriteLine("main:(After)" & Err.Number)
        Console.WriteLine(Err.Number)

        Console.WriteLine("Property Resume Next")
        Err.Clear()
        Console.WriteLine("main:(Before)" & Err.Number)
        Dim x = Prop_1
        Console.WriteLine("main:(After)" & Err.Number)

        Console.WriteLine("Property Goto")
        Err.Clear()
        Console.WriteLine("main:(Before)" & Err.Number)
        Console.WriteLine(Err.Number)
        x = Prop_2
        Console.WriteLine("main:(After)" & Err.Number)
        Console.WriteLine(Err.Number)

        Console.WriteLine("Property Resume Next")
        Err.Clear()
        Console.WriteLine("main:(Before)" & Err.Number)
        Console.WriteLine(Err.Number)
        x = Prop_2
        Console.WriteLine("main:(After)" & Err.Number)
        Console.WriteLine(Err.Number)
    End Sub

    Sub ErrorMethod()
        On Error Resume Next

        Err.Raise(5)
        Console.WriteLine("Resume Next:" & Err.Number)
    End Sub

    Sub ErrorMethod_2()
        On Error GoTo Handler
        Err.Raise(5)
        Exit Sub
Handler:
        Console.WriteLine("In Handler:" & Err.Number)
        Exit Sub
    End Sub

    Sub ErrorMethod_3()
        On Error GoTo Handler
        Err.Raise(5)
        Exit Sub
Handler:
        Console.WriteLine("In Handler:" & Err.Number)
        Resume Next
    End Sub

    Public ReadOnly Property Prop_1 As Integer
        Get
            On Error Resume Next

            Err.Raise(5)
            Console.WriteLine("Resume Next:" & Err.Number)
            Return 1
        End Get
    End Property

    Public ReadOnly Property Prop_2 As Integer
        Get
            On Error GoTo Handler
            Err.Raise(5)
            Return 1
            Exit Property
Handler:
            Console.WriteLine("In Handler:" & Err.Number)
            Exit Property
        End Get
    End Property

    Public ReadOnly Property Prop_3 As Integer
        Get
            On Error GoTo Handler
            Err.Raise(5)
            Return 1
            Exit Property
Handler:
            Console.WriteLine("In Handler:" & Err.Number)
            Resume Next
        End Get
    End Property
End Module        ]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[*****************************************
Before Error
Number:0
Source:
Description:
Erl:0GetException:Nothing
*****************************************
In Handler
Number:5
Source:ErrorHandling
Description:Procedure call or argument is not valid.
Erl:0
GetException:System.ArgumentException: Procedure call or argument is not valid.
   at Module1.Main()
*****************************************
After Clear
In Handler
Number:0
Source:
Description:
Erl:0
GetException:Nothing]]>)
        End Sub

        <Fact()>
        Sub ErrorObject_Properties()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System
Imports  Microsoft.VisualBasic

Module Module1
    Sub Main()
        On Error GoTo handler
        Console.WriteLine("*****************************************")
        Console.WriteLine("Before Error")
        Console.WriteLine("Number:" & Err.Number)
        Console.WriteLine("Source:" & Err.Source)
        Console.WriteLine("Description:" & Err.Description)
        Console.WriteLine("Erl:" & Err.Erl)        
        Console.WriteLine("GetException:" & If(Err.GetException Is Nothing, "Nothing", Err.GetException.ToString))


        Error 5

        Exit Sub
handler:
        Console.WriteLine("*****************************************")
        Console.WriteLine("In Handler")
        Console.WriteLine("Number:" & Err.Number)
        Console.WriteLine("Source:" & Err.Source)
        Console.WriteLine("Description:" & Err.Description)
        Console.WriteLine("Erl:" & Err.Erl)        
        Console.WriteLine("GetException:" & If(Err.GetException Is Nothing, "Nothing", Err.GetException.ToString))

        Console.WriteLine("*****************************************")
        Console.WriteLine("After Clear")
        Err.Clear()
        Console.WriteLine("In Handler")
        Console.WriteLine("Number:" & Err.Number)
        Console.WriteLine("Source:" & Err.Source)
        Console.WriteLine("Description:" & Err.Description)
        Console.WriteLine("Erl:" & Err.Erl)        
        Console.WriteLine("GetException:" & If(Err.GetException Is Nothing, "Nothing", Err.GetException.ToString))
        Resume Next
    End Sub
End Module]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[*****************************************
Before Error
Number:0
Source:
Description:
Erl:0
GetException:Nothing
*****************************************
In Handler
Number:5
Source:ErrorHandling
Description:Procedure call or argument is not valid.
Erl:0
GetException:System.ArgumentException: Procedure call or argument is not valid.
   at Module1.Main()
*****************************************
After Clear
In Handler
Number:0
Source:
Description:
Erl:0
GetException:Nothing]]>)
        End Sub

        <Fact()>
        Sub ErrorObject_Properties_AfterGeneratedInDifferentWays()
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System
Imports  Microsoft.VisualBasic

Module Module1
    Sub Main()
        On Error GoTo handler
        Console.WriteLine("*****************************************")
        Console.WriteLine("Before Error")
        Console.WriteLine("Number:" & Err.Number)
        Console.WriteLine("Source:" & Err.Source)
        Console.WriteLine("Description:" & Err.Description)
        Console.WriteLine("Erl:" & Err.Erl)        
        Console.WriteLine("GetException:" & If(Err.GetException Is Nothing, "Nothing", Err.GetException.ToString))

        Console.WriteLine("WITH EXCEPTION")
        Throw New System.IO.FileNotFoundException
        Err.Clear()

        Console.WriteLine("WITH ERROR")
        Error 53
        Err.Clear()

        Console.WriteLine("WITH ERR.RAISE")
        Err.Raise(53)
        Exit Sub
handler:
        Console.WriteLine("*****************************************")
        Console.WriteLine("In Handler")
        Console.WriteLine("Number:" & Err.Number)
        Console.WriteLine("Source:" & Err.Source)
        Console.WriteLine("Description:" & Err.Description)
        Console.WriteLine("Erl:" & Err.Erl)        
        Console.WriteLine("GetException:" & If(Err.GetException Is Nothing, "Nothing", Err.GetException.ToString))

        Console.WriteLine("*****************************************")
        Console.WriteLine("After Clear")
        Err.Clear()
        Console.WriteLine("In Handler")
        Console.WriteLine("Number:" & Err.Number)
        Console.WriteLine("Source:" & Err.Source)
        Console.WriteLine("Description:" & Err.Description)
        Console.WriteLine("Erl:" & Err.Erl)    
        Console.WriteLine("GetException:" & If(Err.GetException Is Nothing, "Nothing", Err.GetException.ToString))
        Resume Next
    End Sub
End Module]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[   *****************************************
Before Error
Number:0
Source:
Description:
Erl:0
GetException:Nothing
WITH EXCEPTION
*****************************************
In Handler
Number:53
Source:ErrorHandling
Description:Unable to find the specified file.
Erl:0
GetException:System.IO.FileNotFoundException: Unable to find the specified file.
   at Module1.Main()
*****************************************
After Clear
In Handler
Number:0
Source:
Description:
Erl:0
GetException:Nothing
WITH ERROR
*****************************************
In Handler
Number:53
Source:ErrorHandling
Description:File not found.
Erl:0
GetException:System.IO.FileNotFoundException: File not found.
   at Module1.Main()
*****************************************
After Clear
In Handler
Number:0
Source:
Description:
Erl:0
GetException:Nothing
WITH ERR.RAISE
*****************************************
In Handler
Number:53
Source:ErrorHandling
Description:File not found.
Erl:0
GetException:System.IO.FileNotFoundException: File not found.
   at Microsoft.VisualBasic.ErrObject.Raise(Int32 Number, Object Source, Object Description, Object HelpFile, Object HelpContext)
   at Module1.Main()
*****************************************
After Clear
In Handler
Number:0
Source:
Description:
Erl:0
GetException:Nothing]]>)
        End Sub

        <Fact()>
        Sub ErrorObject_Properties__DLLLastError()
            'The Err.LastDllError is reset without having to call the Err.Clear
            'Unsure why...
            Dim source =
    <compilation name="ErrorHandling">
        <file name="a.vb">
            <![CDATA[
Imports System
Imports  Microsoft.VisualBasic

Module Module1
  Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer

    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Const ERROR_INVALID_WINDOW_HANDLE As Long = 1400
    Const ERROR_INVALID_WINDOW_HANDLE_DESCR As String = "Invalid window handle."

    Sub Main()
        ' Prints left, right, top, and bottom positions of a window in pixels.

        Dim rectWindow As RECT
        Dim HWND = 0 '//Intentional erorr for incorrect windows handle

        ' Pass in window handle and empty the data structure.
        ' If function returns 0, an error occurred.
        If GetWindowRect(hwnd, rectWindow) = 0 Then
            ' Check LastDllError and display a dialog box if the error
            ' occurred because an invalid handle was passed.
            Dim x As Integer = Err.LastDllError
            Console.WriteLine(x)
            If x = ERROR_INVALID_WINDOW_HANDLE Then
                Console.WriteLine(ERROR_INVALID_WINDOW_HANDLE_DESCR & "    Error!")
            Else
                Console.WriteLine(ERROR_INVALID_WINDOW_HANDLE_DESCR & "    Incorrect Behaviour!")
            End If
            Console.WriteLine(Err.LastDllError)
        End If
    End Sub
End Module]]>
        </file>
    </compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)
            CompileAndVerify(compilation, expectedOutput:=<![CDATA[1400
Invalid window handle.    Error!
1400]]>)
        End Sub

        <Fact>
        Public Sub ERL_01()
            Dim source =
<compilation>
    <file name="a.vb">
        <![CDATA[
Imports System
Imports Microsoft.VisualBasic

Module Program
    Sub Main()
        Dim x = Sub()
                    Try
100:
                        Throw New NotSupportedException()

                    Catch ex As Exception
                        System.Console.WriteLine(Err.Erl)
                    End Try
                End Sub

        x()

        Try
200:
            Throw New NotSupportedException()

        Catch ex As Exception
            System.Console.WriteLine(Err.Erl)
        End Try

    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
0
200
]]>)
        End Sub

        <Fact>
        Public Sub ERL_02()
            Dim source =
<compilation>
    <file name="a.vb">
        <![CDATA[
Imports System
Imports Microsoft.VisualBasic

Module Program
    Sub Main()
        On Error GoTo Handler

100:
        Throw New NotSupportedException()
200:
        Throw New NotSupportedException()
300:
        Throw New NotSupportedException()

400:
L500:   System.Console.WriteLine("L500")
        Throw New NotSupportedException()

        Return

Handler:
        System.Console.WriteLine(Err.Erl)
        Resume Next
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
100
200
300
L500
400
]]>)
        End Sub

        <Fact>
        Public Sub ERL_03()
            Dim source =
<compilation>
    <file name="a.vb">
        <![CDATA[
Imports System
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Module Program
    Sub Main()
        For Each x In Test2()
        Next
    End Sub

    Iterator Function Test2() As IEnumerable(Of Integer)
        Try
100:
            Throw New NotImplementedException()

        Catch ex As Exception
            System.Console.WriteLine(Err.Erl)
        End Try
    End Function
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
100
]]>)
        End Sub

        <Fact>
        Public Sub ERL_04()
            Dim source =
<compilation>
    <file name="a.vb">
        <![CDATA[
Imports System
Imports Microsoft.VisualBasic

Module Program
    Sub Main()
        On Error GoTo Handler

100:
        Throw New NotSupportedException()
4000000000000000000:
        Throw New NotSupportedException()
300:
        Throw New NotSupportedException()

40000000000000:
L500:   System.Console.WriteLine("L500")
        Throw New NotSupportedException()

        Return

Handler:
        System.Console.WriteLine(Err.Erl)
        Resume Next
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            Dim compilationVerifier = CompileAndVerify(compilation,
                         expectedOutput:=
            <![CDATA[
100
0
300
L500
0
]]>)
        End Sub

        <Fact>
        Public Sub ERL_05()
            Dim source =
<compilation>
    <file name="a.vb">
        <![CDATA[
Imports System
Imports Microsoft.VisualBasic

Module Program
    Sub Main()
        On Error GoTo Handler

40000000000000000000:
        Throw New NotSupportedException()
        Return

Handler:
        System.Console.WriteLine(Err.Erl)
        Resume Next
    End Sub
End Module
    ]]>
    </file>
</compilation>

            Dim compilation = CreateCompilationWithMscorlibAndVBRuntime(source, TestOptions.ReleaseExe)

            AssertTheseDiagnostics(compilation,
<expected>
BC30036: Overflow.
40000000000000000000:
~~~~~~~~~~~~~~~~~~~~
</expected>)
        End Sub

    End Class
End Namespace
