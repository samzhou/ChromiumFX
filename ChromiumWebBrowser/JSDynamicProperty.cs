﻿// Copyright (c) 2014-2015 Wolfgang Borgsmüller
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chromium.Remote;
using Chromium.Remote.Event;

namespace Chromium.WebBrowser {

    /// <summary>
    /// Represents a dynamic javascript property in the render process to be added to
    /// a browser frame's global object or to a JSObject.
    /// </summary>
    public class JSDynamicProperty : JSProperty {


        /// <summary>
        /// Called if a script attempts to get the value of
        /// this dynamic property. It's up to the
        /// application to decide how to handle the request. See also
        /// description of CfrV8AccessorGetEventArgs.
        /// If the application does not subscribe to this event, the 
        /// default action will be to return «undefined».
        /// </summary>
        public event CfrV8AccessorGetEventHandler PropertyGet;

        /// <summary>
        /// Called if a script attempts to set the value of
        /// this dynamic property. It's up to the 
        /// application to decide how to handle the request. See also
        /// description of CfrV8AccessorSetEventArgs.
        /// If the application does not subscribe to this event, the 
        /// default action will be to return an exception with
        /// the message "Property is readonly" to the script.
        /// </summary>
        public event CfrV8AccessorSetEventHandler PropertySet;

        /// <summary>
        /// If true, then the PropertyGet and PropertySet events 
        /// are executed on the thread that owns the browser's 
        /// underlying window handle. Preserves affinity to the render thread.
        /// </summary>
        public bool InvokeOnBrowser { get; private set; }

        /// <summary>
        /// Creates a new dynamic javascript property to be added 
        /// to a browser frame's global object or to another JSObject.
        /// </summary>
        public JSDynamicProperty()
            : base(JSPropertyType.Dynamic) {
        }

        /// <summary>
        /// Creates a new dynamic javascript property to be added
        /// to a browser frame's global object or to another JSObject.
        /// If invokeOnBrowser is true, then the PropertyGet and PropertySet 
        /// events are executed on the thread that owns the browser's 
        /// underlying window handle. Preserves affinity to the render thread.
        /// </summary>
        public JSDynamicProperty(bool invokeOnBrowser)
            : base(JSPropertyType.Dynamic) {
            this.InvokeOnBrowser = invokeOnBrowser;
        }

        internal void RaisePropertySet(CfrV8AccessorSetEventArgs e) {
            var h = PropertySet;
            if(h == null) {
                e.Exception = "Property is readonly.";
                e.SetReturnValue(true);
            } else {
                if(InvokeOnBrowser) {
                    Browser.RenderThreadInvoke((MethodInvoker)(() => h.Invoke(this, e)));
                } else {
                    h.Invoke(this, e);
                }
            }
        }

        internal void RaisePropertyGet(CfrV8AccessorGetEventArgs e) {
            var h = PropertyGet;
            if(h == null) {
                e.Retval = CfrV8Value.CreateUndefined(e.RemoteRuntime);
                e.SetReturnValue(true);
            } else {
                if(InvokeOnBrowser) {
                    Browser.RenderThreadInvoke((MethodInvoker)(() => h.Invoke(this, e)));
                } else {
                    h.Invoke(this, e);
                }
            }
        }

        internal override CfrV8Value CreateV8Value() {
            throw new NotImplementedException();
        }
    }
}