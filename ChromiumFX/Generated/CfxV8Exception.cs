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

// Generated file. Do not edit.


using System;

namespace Chromium {
    /// <summary>
    /// Structure representing a V8 exception. The functions of this structure may be
    /// called on any render process thread.
    /// </summary>
    /// <remarks>
    /// See also the original CEF documentation in
    /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
    /// </remarks>
    public class CfxV8Exception : CfxBase {

        static CfxV8Exception () {
            CfxApi.cfx_v8exception_get_message = (CfxApi.cfx_v8exception_get_message_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_message", typeof(CfxApi.cfx_v8exception_get_message_delegate));
            CfxApi.cfx_v8exception_get_source_line = (CfxApi.cfx_v8exception_get_source_line_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_source_line", typeof(CfxApi.cfx_v8exception_get_source_line_delegate));
            CfxApi.cfx_v8exception_get_script_resource_name = (CfxApi.cfx_v8exception_get_script_resource_name_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_script_resource_name", typeof(CfxApi.cfx_v8exception_get_script_resource_name_delegate));
            CfxApi.cfx_v8exception_get_line_number = (CfxApi.cfx_v8exception_get_line_number_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_line_number", typeof(CfxApi.cfx_v8exception_get_line_number_delegate));
            CfxApi.cfx_v8exception_get_start_position = (CfxApi.cfx_v8exception_get_start_position_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_start_position", typeof(CfxApi.cfx_v8exception_get_start_position_delegate));
            CfxApi.cfx_v8exception_get_end_position = (CfxApi.cfx_v8exception_get_end_position_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_end_position", typeof(CfxApi.cfx_v8exception_get_end_position_delegate));
            CfxApi.cfx_v8exception_get_start_column = (CfxApi.cfx_v8exception_get_start_column_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_start_column", typeof(CfxApi.cfx_v8exception_get_start_column_delegate));
            CfxApi.cfx_v8exception_get_end_column = (CfxApi.cfx_v8exception_get_end_column_delegate)CfxApi.GetDelegate(CfxApi.libcfxPtr, "cfx_v8exception_get_end_column", typeof(CfxApi.cfx_v8exception_get_end_column_delegate));
        }

        private static readonly WeakCache weakCache = new WeakCache();

        internal static CfxV8Exception Wrap(IntPtr nativePtr) {
            if(nativePtr == IntPtr.Zero) return null;
            lock(weakCache) {
                var wrapper = (CfxV8Exception)weakCache.Get(nativePtr);
                if(wrapper == null) {
                    wrapper = new CfxV8Exception(nativePtr);
                    weakCache.Add(wrapper);
                } else {
                    CfxApi.cfx_release(nativePtr);
                }
                return wrapper;
            }
        }


        internal CfxV8Exception(IntPtr nativePtr) : base(nativePtr) {}

        /// <summary>
        /// Returns the exception message.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public String Message {
            get {
                return StringFunctions.ConvertStringUserfree(CfxApi.cfx_v8exception_get_message(NativePtr));
            }
        }

        /// <summary>
        /// Returns the line of source code that the exception occurred within.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public String SourceLine {
            get {
                return StringFunctions.ConvertStringUserfree(CfxApi.cfx_v8exception_get_source_line(NativePtr));
            }
        }

        /// <summary>
        /// Returns the resource name for the script from where the function causing
        /// the error originates.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public String ScriptResourceName {
            get {
                return StringFunctions.ConvertStringUserfree(CfxApi.cfx_v8exception_get_script_resource_name(NativePtr));
            }
        }

        /// <summary>
        /// Returns the 1-based number of the line where the error occurred or 0 if the
        /// line number is unknown.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int LineNumber {
            get {
                return CfxApi.cfx_v8exception_get_line_number(NativePtr);
            }
        }

        /// <summary>
        /// Returns the index within the script of the first character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int StartPosition {
            get {
                return CfxApi.cfx_v8exception_get_start_position(NativePtr);
            }
        }

        /// <summary>
        /// Returns the index within the script of the last character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int EndPosition {
            get {
                return CfxApi.cfx_v8exception_get_end_position(NativePtr);
            }
        }

        /// <summary>
        /// Returns the index within the line of the first character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int StartColumn {
            get {
                return CfxApi.cfx_v8exception_get_start_column(NativePtr);
            }
        }

        /// <summary>
        /// Returns the index within the line of the last character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/wborgsm/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int EndColumn {
            get {
                return CfxApi.cfx_v8exception_get_end_column(NativePtr);
            }
        }

        internal override void OnDispose(IntPtr nativePtr) {
            weakCache.Remove(nativePtr);
            base.OnDispose(nativePtr);
        }
    }
}
