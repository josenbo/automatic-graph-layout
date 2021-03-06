/*
Microsoft Automatic Graph Layout,MSAGL 

Copyright (c) Microsoft Corporation

All rights reserved. 

MIT License 

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Msagl.Core.Geometry;

namespace Microsoft.Msagl.Routing.Visibility {
    /// <summary>
    /// an edge connecting two VisibilityVertices
    /// </summary>
    [DebuggerDisplay("({Source.Point.X} {Source.Point.Y})->({Target.Point.X} {Target.Point.Y}) ({Weight})")]
    public class VisibilityEdge {
        internal double LengthMultiplier = 1;

        internal VisibilityEdge() { }

        internal VisibilityEdge(VisibilityVertex source, VisibilityVertex target, double weight) {
            Debug.Assert(source.Point != target.Point, "Self-edges are not allowed");
            Source = source;
            Target = target;
            Weight = weight;
        }

        internal VisibilityEdge(VisibilityVertex source, VisibilityVertex target) : this (source, target, 1.0) {}

        internal double Weight { get; private set; }

        internal const double DefaultWeight = 1.0;

        /// <summary>
        ///returns true if and only if the edge can be passed
        /// </summary>
        internal Func<bool> IsPassable { get; set; }

    
        /// <summary>
        /// edge source point
        /// </summary>
        public Point SourcePoint {
            get { return Source.Point; }
        }
        /// <summary>
        /// edge target point
        /// </summary>
        public Point TargetPoint {
            get { return Target.Point; }
        }


        internal VisibilityVertex Source { get; set; }

        internal VisibilityVertex Target { get; set; }

        internal double Length {
            get { return (SourcePoint-TargetPoint).Length*LengthMultiplier; }
        }

      


        /// <summary>
        /// Rounded representation; DebuggerDisplay shows the unrounded form.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return String.Format(CultureInfo.InvariantCulture, "{0}->{1} ({2})", Source, Target, Weight.ToString("0.0###", CultureInfo.InvariantCulture));
        }

        internal VisibilityEdge ReversedClone() {
            return new VisibilityEdge(Target, Source);
        }

        internal VisibilityEdge Clone() {
            return new VisibilityEdge(Source, Target);
        }
    }
}