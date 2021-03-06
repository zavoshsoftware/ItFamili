﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class BaseEntity : object
    {

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public System.Guid Id { get; set; }
        [Display(Name = "IsActive", ResourceType = typeof(Resources.Models.BaseEntity))]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime CreationDate { get; set; }
     
        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime? LastModifiedDate { get; set; }

        [Display(Name = "IsDeleted", ResourceType = typeof(Resources.Models.BaseEntity))]
        [System.ComponentModel.DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "DeletionDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime? DeletionDate { get; set; }
       
        [Display(Name = "Description", ResourceType = typeof(Resources.Models.BaseEntity))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        [NotMapped]
        public string CreationDateStr
        {
            get
            {
                //  return "hi";
                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                string year = pc.GetYear(CreationDate).ToString().PadLeft(4, '0');
                string month = pc.GetMonth(CreationDate).ToString().PadLeft(2, '0');
                string day = pc.GetDayOfMonth(CreationDate).ToString().PadLeft(2, '0');
                return String.Format("{0}/{1}/{2}", year, month, day) ;
            }
        }
    }
}
