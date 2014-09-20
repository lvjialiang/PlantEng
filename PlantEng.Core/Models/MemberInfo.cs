using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PlantEng.Models
{
   /// <summary>
   /// 用户基本信息
   /// </summary>
    public class MemberInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Industry { get; set; }
        public string Position { get; set; }
        public string Fax { get; set; }
        public string Avatar { get; set; }
        /// <summary>
        /// 邮编
        /// 2013-2-4 add
        /// </summary>
        public string ZipCode { get; set; }
        public MemberType Type { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }

        /// <summary>
        /// 2012-12-14 添加
        /// 个人用户所在单位
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 申请杂志类型
        /// 2013-4-3
        /// </summary>
        public string MagType { get; set; }
        /// <summary>
        /// 参与，推荐，批准，采购的产品
        /// 2013-4-3
        /// </summary>
        public string PurchaseProducts { get; set; }

    }
    /// <summary>
    /// 公司用户信息
    /// </summary>
    public class CompanyInfo : MemberInfo {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyIntroduction { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySite { get; set; }
        public string CompanyBanner { get; set; }
        public CompanyStatus CompanyStatus { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyDateTime { get; set; }
        /// <summary>
        /// 最后一次配准时间
        /// </summary>
        public DateTime LastPassDateTime { get; set; }

        /// <summary>
        /// 公司产品类别
        /// 2012-12-14 add
        /// </summary>
        public List<int> Categories { get; set; }

        public CompanyInfo() {
            Categories = new List<int>();
        }
        
    }
    /// <summary>
    /// 公司状态
    /// </summary>
    public enum CompanyStatus { 
        None = 0,
        Applying = 1,
        Pass = 2,
        NoPass = 3
    }
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum MemberType
    {
        None = 0,
        Common = 1,
        Enterprise = 2
    }
}
