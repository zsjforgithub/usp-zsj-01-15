//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template made by Louis-Guillaume Morand.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace USP.Models.ViewModel
{
    
    
    public class SysPrivilegeMetaData
    {
        public virtual long ID
        {
            get;
            set;
        }
        [Required] 
        public virtual long Parent
        {
            get;
            set;
        }
        [Required] 
        public virtual long Menu
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string Name
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string Clazz
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string Area
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string Controller
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string Method
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string Parameter
        {
            get;
            set;
        }
    	
        [StringLength(100, ErrorMessage="最多可输入100个字符")]
        [Required] 
        public virtual string Url
        {
            get;
            set;
        }
    	
        [StringLength(50, ErrorMessage="最多可输入50个字符")]
        public virtual string Reserve
        {
            get;
            set;
        }
    	
        [StringLength(250, ErrorMessage="最多可输入250个字符")]
        public virtual string Remark
        {
            get;
            set;
        }
        [Required] 
        public virtual long Creator
        {
            get;
            set;
        }
        [Required] 
        public virtual System.DateTime CreateTime
        {
            get;
            set;
        }
        public virtual Nullable<long> Auditor
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> AuditTime
        {
            get;
            set;
        }
        public virtual Nullable<long> Canceler
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> CancelTime
        {
            get;
            set;
        }
    }
}