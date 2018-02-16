﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insight.Base.Common.Entity
{
    [Table("ucs_application")]
    public class Application
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 应用别名
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string host { get; set; }

        /// <summary>
        /// 令牌生存周期(小时)
        /// </summary>
        [Column("token_life")]
        public int tokenLife { get; set; }

        /// <summary>
        /// 图标url
        /// </summary>
        public string iconurl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public byte[] icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Column("creator_id")]
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_time")]
        public DateTime createTime { get; set; }
    }

    [Table("ucs_navigator")]
    public class Navigator
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 上级导航ID
        /// </summary>
        [Column("parent_id")]
        public string parentId { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [Column("app_id")]
        public string appId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 导航名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 模块url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 控制器命名空间
        /// </summary>
        [Column("class_name")]
        public string className { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [Column("file_path")]
        public string filePath { get; set; }

        /// <summary>
        /// 图标url
        /// </summary>
        public string iconurl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public byte[] icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 是否默认启动：0、否；1、是
        /// </summary>
        [Column("is_default")]
        public bool isDefault { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Column("creator_id")]
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_time")]
        public DateTime createTime { get; set; }
    }

    [Table("ucs_function")]
    public class Function
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 导航ID
        /// </summary>
        [Column("navigator_id")]
        public string navigatorId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string alias { get; set; }

        /// <summary>
        /// 接口路由
        /// </summary>
        public string routes { get; set; }

        /// <summary>
        /// 功能url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 图标url
        /// </summary>
        public string iconurl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public byte[] icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 是否开始分组：0、否；1、是
        /// </summary>
        [Column("is_begin")]
        public bool isBegin { get; set; }

        /// <summary>
        /// 是否隐藏文字：0、显示；1、隐藏
        /// </summary>
        [Column("is_hide_text")]
        public bool isHideText { get; set; }

        /// <summary>
        /// 是否可见：0、不可见；1、可见
        /// </summary>
        [Column("is_visible")]
        public bool isVisible { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Column("creator_id")]
        public string creatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_time")]
        public DateTime createTime { get; set; }
    }
}
