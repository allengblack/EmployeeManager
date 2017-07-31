using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        //[Required]
        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public string sex { get; set; }
        public string MaritalStatus { get; set; }
        public int NumberOfChildren { get; set; }
        public bool IsActive {
            get
            {
                return !(User.LockoutEndDateUtc != null && User.LockoutEndDateUtc.Value > DateTime.Now.ToUniversalTime());
            }
        } 
        public DateTime? DateOfHire { get; set; } = null;

        public string TimeOfService {
            get {
                if (this.DateOfHire != null)
                {
                    return Convert.ToInt32(DateTime.Now.Subtract(this.DateOfHire.Value).TotalDays).ToString();
                }
                else
                {
                    return Convert.ToInt32(TimeSpan.FromDays(0).TotalDays).ToString();
                }
            }
        }

        public string MaxEducation { get; set; }
        public string Position { get; set; }
        public DateTime? DateOfLastEmploment { get; set; } = null;
        public decimal Salary { get; set; }

        public static List<string> EducationLevels
        {
            get
            {
                return new List<string>()
                {
                    "Primary",
                    "Secondary",
                    "Tertiary"
                };
            }
        }

        public static List<string> MaritalStatusList
        {
            get
            {
                return new List<string>()
                {
                    "Single",
                    "Married",
                    "Divorced"
                };
            }
        }

        public enum Positions
        {
            None,
            Entry,
            Intermediate,
            Top
        }

        public enum Gender
        {
            Male,
            Female
        }
    }

    public class EmployeeActiveModel
    {
        public string UserId { get; set; }
        public string Active { get; set; }
        public bool IsActive {
            get
            {
                return Active == "on";
            }
        }
    }
}