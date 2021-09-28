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
    public class AnswerQuestionTask : IEquatable<AnswerQuestionTask>
    {
        /// <summary>
        /// Gets or Sets TaskId
        /// </summary>
        [Required]
        [DataMember(Name="task_id", EmitDefaultValue=false)]
        public long TaskId { get; set; }

        /// <summary>
        /// Gets or Sets QuestionId
        /// </summary>
        [Required]
        [DataMember(Name="question_id", EmitDefaultValue=false)]
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Gets or Sets RecievedOn
        /// </summary>
        [Required]
        [DataMember(Name="recieved_on", EmitDefaultValue=false)]
        public DateTime RecievedOn { get; set; }

        /// <summary>
        /// Gets or Sets Subject
        /// </summary>
        [Required]
        [DataMember(Name="subject", EmitDefaultValue=false)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or Sets Question
        /// </summary>
        [Required]
        [DataMember(Name="question", EmitDefaultValue=false)]
        public string Question { get; set; }

        /// <summary>
        /// Gets or Sets Sender
        /// </summary>
        [Required]
        [DataMember(Name="sender", EmitDefaultValue=false)]
        public string Sender { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AnswerQuestionTask {\n");
            sb.Append("  TaskId: ").Append(TaskId).Append("\n");
            sb.Append("  QuestionId: ").Append(QuestionId).Append("\n");
            sb.Append("  RecievedOn: ").Append(RecievedOn).Append("\n");
            sb.Append("  Subject: ").Append(Subject).Append("\n");
            sb.Append("  Question: ").Append(Question).Append("\n");
            sb.Append("  Sender: ").Append(Sender).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AnswerQuestionTask)obj);
        }

        /// <summary>
        /// Returns true if AnswerQuestionTask instances are equal
        /// </summary>
        /// <param name="other">Instance of AnswerQuestionTask to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AnswerQuestionTask other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    TaskId == other.TaskId ||
                    
                    TaskId.Equals(other.TaskId)
                ) && 
                (
                    QuestionId == other.QuestionId ||
                    QuestionId != null &&
                    QuestionId.Equals(other.QuestionId)
                ) && 
                (
                    RecievedOn == other.RecievedOn ||
                    RecievedOn != null &&
                    RecievedOn.Equals(other.RecievedOn)
                ) && 
                (
                    Subject == other.Subject ||
                    Subject != null &&
                    Subject.Equals(other.Subject)
                ) && 
                (
                    Question == other.Question ||
                    Question != null &&
                    Question.Equals(other.Question)
                ) && 
                (
                    Sender == other.Sender ||
                    Sender != null &&
                    Sender.Equals(other.Sender)
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
                    
                    hashCode = hashCode * 59 + TaskId.GetHashCode();
                    if (QuestionId != null)
                    hashCode = hashCode * 59 + QuestionId.GetHashCode();
                    if (RecievedOn != null)
                    hashCode = hashCode * 59 + RecievedOn.GetHashCode();
                    if (Subject != null)
                    hashCode = hashCode * 59 + Subject.GetHashCode();
                    if (Question != null)
                    hashCode = hashCode * 59 + Question.GetHashCode();
                    if (Sender != null)
                    hashCode = hashCode * 59 + Sender.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(AnswerQuestionTask left, AnswerQuestionTask right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AnswerQuestionTask left, AnswerQuestionTask right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
