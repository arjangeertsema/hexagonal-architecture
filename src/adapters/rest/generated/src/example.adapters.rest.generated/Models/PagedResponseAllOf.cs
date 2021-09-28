/*
 * Contact center service
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 0.0.1
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using example.adapters.rest.generated.Converters;

namespace example.adapters.rest.generated.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PagedResponseAllOf : IEquatable<PagedResponseAllOf>
    {
        /// <summary>
        /// Gets or Sets Self
        /// </summary>
        [DataMember(Name="self", EmitDefaultValue=false)]
        public string Self { get; set; }

        /// <summary>
        /// Gets or Sets First
        /// </summary>
        [DataMember(Name="first", EmitDefaultValue=false)]
        public string First { get; set; }

        /// <summary>
        /// Gets or Sets Prev
        /// </summary>
        [DataMember(Name="prev", EmitDefaultValue=false)]
        public string Prev { get; set; }

        /// <summary>
        /// Gets or Sets Next
        /// </summary>
        [Required]
        [DataMember(Name="next", EmitDefaultValue=false)]
        public string Next { get; set; }

        /// <summary>
        /// Gets or Sets Last
        /// </summary>
        [DataMember(Name="last", EmitDefaultValue=false)]
        public string Last { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PagedResponseAllOf {\n");
            sb.Append("  Self: ").Append(Self).Append("\n");
            sb.Append("  First: ").Append(First).Append("\n");
            sb.Append("  Prev: ").Append(Prev).Append("\n");
            sb.Append("  Next: ").Append(Next).Append("\n");
            sb.Append("  Last: ").Append(Last).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PagedResponseAllOf)obj);
        }

        /// <summary>
        /// Returns true if PagedResponseAllOf instances are equal
        /// </summary>
        /// <param name="other">Instance of PagedResponseAllOf to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PagedResponseAllOf other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Self == other.Self ||
                    Self != null &&
                    Self.Equals(other.Self)
                ) && 
                (
                    First == other.First ||
                    First != null &&
                    First.Equals(other.First)
                ) && 
                (
                    Prev == other.Prev ||
                    Prev != null &&
                    Prev.Equals(other.Prev)
                ) && 
                (
                    Next == other.Next ||
                    Next != null &&
                    Next.Equals(other.Next)
                ) && 
                (
                    Last == other.Last ||
                    Last != null &&
                    Last.Equals(other.Last)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Self != null)
                    hashCode = hashCode * 59 + Self.GetHashCode();
                    if (First != null)
                    hashCode = hashCode * 59 + First.GetHashCode();
                    if (Prev != null)
                    hashCode = hashCode * 59 + Prev.GetHashCode();
                    if (Next != null)
                    hashCode = hashCode * 59 + Next.GetHashCode();
                    if (Last != null)
                    hashCode = hashCode * 59 + Last.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(PagedResponseAllOf left, PagedResponseAllOf right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PagedResponseAllOf left, PagedResponseAllOf right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
