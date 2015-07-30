// Copyright (c) 2014-2015 Wolfgang Borgsmüller
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// 1. Redistributions of source code must retain the above copyright 
//    notice, this list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution.
// 
// 3. Neither the name of the copyright holder nor the names of its 
//    contributors may be used to endorse or promote products derived 
//    from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS 
// OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND 
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


public class GetPostDataElementsSignature : Signature {

    public GetPostDataElementsSignature(ISignatureOwner parent, Parser.SignatureData sd, ApiTypeBuilder api)
        : base(parent, sd, api) {
    }

    public override Argument[] ManagedArguments {
        get { return new Argument[] { Arguments[0] }; }
    }

    public override ApiType PublicReturnType {
        get { return new CefStructPtrArrayType(Arguments[2], Arguments[1]); }
    }

    public override string NativeSignature(string functionName) {
        return "static void cfx_post_data_get_elements(cef_post_data_t* self, int elementsCount, cef_post_data_element_t** elements)";
    }

    public override string PInvokeSignature(string functionName) {
        return "void cfx_post_data_get_elements_delegate(IntPtr self, int elementsCount, IntPtr elements)";
    }

    public override void EmitNativeCall(CodeBuilder b, string functionName) {
        b.AppendLine("size_t tmp_elementsCount = (size_t)elementsCount;");
        b.AppendLine("self->get_elements(self, &tmp_elementsCount, elements);");
    }

    public override void EmitPublicCall(CodeBuilder b) {
        var code =
@"int count = CfxApi.cfx_post_data_get_element_count(NativePtr);
if(count == 0) return new CfxPostDataElement[0];
IntPtr[] ptrs = new IntPtr[count];
var ptrs_p = new PinnedObject(ptrs);
CfxApi.cfx_post_data_get_elements(NativePtr, count, ptrs_p.PinnedPtr);
ptrs_p.Free();
CfxPostDataElement[] retval = new CfxPostDataElement[count];
for(int i = 0; i < count; ++i) {
    retval[i] = CfxPostDataElement.Wrap(ptrs[i]);
}
return retval;";

        b.AppendMultiline(code);
    }

    protected override void EmitExecuteInTargetProcess(CodeBuilder b) {
        b.AppendLine("var elements = ((CfxPostData)RemoteProxy.Unwrap(self)).Elements;");
        b.BeginIf("elements != null");
        b.AppendLine("__retval = new IntPtr[elements.Length];");
        b.BeginFor("elements.Length");
        b.AppendLine("__retval[i] = RemoteProxy.Wrap(elements[i]);");
        b.EndBlock();
        b.EndBlock();
    }

    public override void EmitRemoteCall(CodeBuilder b) {
        b.AppendLine("var call = new CfxPostDataGetElementsRenderProcessCall();");
        b.AppendLine("call.self = CfrObject.Unwrap(this);");
        b.AppendLine("call.Execute();");
        b.AppendLine("if(call.__retval == null) return null;");
        b.AppendLine("var retval = new CfrPostDataElement[call.__retval.Length];");
        b.BeginFor("retval.Length");
        b.AppendLine("retval[i] = CfrPostDataElement.Wrap(call.__retval[i]);");
        b.EndBlock();
        b.AppendLine("return retval;");
    }

    public override void DebugPrintUnhandledArrayArguments() {
    }
}