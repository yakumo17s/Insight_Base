//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Insight.Base.Common.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYS_Code_Allot
    {
        public System.Guid ID { get; set; }
        public long SN { get; set; }
        public System.Guid SchemeId { get; set; }
        public System.Guid ModuleId { get; set; }
        public System.Guid OwnerId { get; set; }
        public string AllotNumber { get; set; }
        public Nullable<System.Guid> BusinessId { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<System.Guid> CreatorDeptId { get; set; }
        public System.Guid CreatorUserId { get; set; }
        public System.DateTime CreateTime { get; set; }
    
        public virtual SYS_Organization SYS_Organization { get; set; }
        public virtual SYS_User SYS_User { get; set; }
        public virtual SYS_Module SYS_Module { get; set; }
        public virtual SYS_User SYS_User1 { get; set; }
        public virtual SYS_Code_Scheme SYS_Code_Scheme { get; set; }
    }
}
