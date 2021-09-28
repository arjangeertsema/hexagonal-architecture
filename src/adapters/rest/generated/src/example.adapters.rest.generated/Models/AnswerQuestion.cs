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
    public class AnswerQuestion : IEquatable<AnswerQuestion>
    {
        /// <summary>
        /// Gets or Sets CommandId
        /// </summary>
        [Required]
        [DataMember(Name="command_id", EmitDefaultValue=false)]
        public Guid CommandId { get; set; }

        /// <summary>
        /// Gets or Sets Answer
        /// </summary>
        [Required]
        [DataMember(Name="answer", EmitDefaultValue=false)]
        public string Answer { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AnswerQuestion {\n");
            sb.Append("  CommandId: ").Append(CommandId).Append("\n");
            sb.Append("  Answer: ").Append(Answer).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AnswerQuestion)obj);
        }

        /// <summary>
        /// Returns true if AnswerQuestion instances are equal
        /// </summary>
        /// <param name="other">Instance of AnswerQuestion to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AnswerQuestion other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    CommandId == other.CommandId ||
                    CommandId != null &&
                    CommandId.Equals(other.CommandId)
                ) && 
                (
                    Answer == other.Answer ||
                    Answer != null &&
                    Answer.Equals(other.Answer)
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
                    if (CommandId != null)
                    hashCode = hashCode * 59 + CommandId.GetHashCode();
                    if (Answer != null)
                    hashCode = hashCode * 59 + Answer.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(AnswerQuestion left, AnswerQuestion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AnswerQuestion left, AnswerQuestion right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
