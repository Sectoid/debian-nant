// NAnt - A .NET build tool
// Copyright (C) 2001-2005 Gerry Shaw
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Gert Driesen (driesen@users.sourceforge.net)

using System;
using System.ComponentModel;
using System.Globalization;

namespace NAnt.DotNet.Types {
    /// <summary>
    /// Specifies the type of debugging information generated by the compiler.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///   For backward compatibility, the following string values can also be
    ///   used in build files:
    ///   </para>
    ///   <list type="table">
    ///     <listheader>
    ///       <term>Value</term>
    ///       <description>Corresponding field</description>
    ///     </listheader>
    ///     <item>
    ///       <term>&quot;true&quot;</term>
    ///       <description><see cref="Enable" /></description>
    ///     </item>
    ///     <item>
    ///       <term>&quot;false&quot;</term>
    ///       <description><see cref="None" /></description>
    ///     </item>
    ///   </list>
    ///   <para>
    ///   When set to <see langword="Enabled" /> then the following conditional 
    ///   compilation symbols will also be defined:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>
    ///       <description>DEBUG</description>
    ///     </item>
    ///     <item>
    ///       <description>TRACE</description>
    ///     </item>
    ///   </list>
    /// </remarks>
    [TypeConverter(typeof(DebugOutputConverter))]
    public enum DebugOutput {
        /// <summary>
        /// Create no debug information.
        /// </summary>
        None = 0,

        /// <summary>
        /// Enable attaching a debugger to the running program.
        /// </summary>
        Enable = 1,

        /// <summary>
        /// Enable attaching a debugger to the running program.
        /// </summary>
        Full = 2,

        /// <summary>
        /// Only display assembler when the running program is attached to the 
        /// debugger.
        /// </summary>
        PdbOnly = 3
    }

    /// <summary>
    /// Specialized <see cref="EnumConverter" /> that also supports 
    /// case-insensitive conversion of &quot;true&quot; to 
    /// <see cref="DebugOutput.Enable" /> and &quot;false&quot; to
    /// <see cref="DebugOutput.None" />.
    /// </summary>
    public class DebugOutputConverter : EnumConverter {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugOutputConverter" />
        /// class.
        /// </summary>
        public DebugOutputConverter() : base(typeof(DebugOutput)) {
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the 
        /// specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="CultureInfo"/> object. If a <see langword="null"/> is passed, the current culture is assumed.</param>
        /// <param name="value">The <see cref="Object"/> to convert.</param>
        /// <returns>
        /// An <see cref="Object"/> that represents the converted value.
        /// </returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            string stringValue = value as string;
            if (stringValue != null) {
                if (string.Compare(stringValue, "true", true, culture) == 0)
                    return DebugOutput.Enable;
                if (string.Compare(stringValue, "false", true, culture) == 0)
                    return DebugOutput.None;
                return Enum.Parse(typeof(DebugOutput), stringValue, true);
            }

            // default to EnumConverter behavior
            return base.ConvertFrom(context, culture, value);
        }
    }
}
