﻿using System;
using System.Collections.Generic;
using System.Linq;
using Insight.Base.Common.DTO;
using Insight.Base.Common.Entity;
using Insight.Utils.Common;
using Insight.Utils.Entity;
using Insight.Utils.Redis;

namespace Insight.Base.OAuth
{
    public class Token
    {
        // Token允许的超时毫秒数(300秒)
        private const int timeOut = 300;

        /// <summary>
        /// 访问令牌MD5摘要
        /// </summary>
        public string hash { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string tenantId { get; set; }

        /// <summary>
        /// 租户编码
        /// </summary>
        public string tenantCode { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string tenantName { get; set; }

        /// <summary>
        /// 登录部门ID
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        /// 登录部门编码
        /// </summary>
        public string deptCode { get; set; }

        /// <summary>
        /// 登录部门名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appId { get; set; }

        /// <summary>
        /// 令牌生命周期(秒)
        /// </summary>
        public int life { get; set; }

        /// <summary>
        /// 单点登录
        /// </summary>
        public bool signInOne { get; set; }

        /// <summary>
        /// 服务端自动刷新，客户端无需实现
        /// </summary>
        public bool autoRefresh { get; set; }

        /// <summary>
        /// 租户到期时间
        /// </summary>
        public DateTime expireDate { get; set; }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public DateTime expiryTime { get; set; }

        /// <summary>
        /// Token失效时间
        /// </summary>
        public DateTime failureTime { get; set; }

        /// <summary>
        /// Token验证密钥
        /// </summary>
        public string secretKey { get; set; }

        /// <summary>
        /// Token刷新密钥
        /// </summary>
        public string refreshKey { get; set; }

        /// <summary>
        /// 授权操作码集合
        /// </summary>
        public List<string> permitFuncs { get; set; }

        /// <summary>
        /// 授权数据信息集合
        /// </summary>
        public List<PermitData> permitDatas { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Token()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <param name="appId">应用ID</param>
        public Token(string tenantId, string appId)
        {
            this.tenantId = tenantId;
            this.appId = appId;

            getAppInfo();
            expireDate = getExpireDate(tenantId);
            secretKey = Util.newId("N");
            refreshKey = Util.newId("N");
            expiryTime = DateTime.Now.AddSeconds(life + timeOut);
            failureTime = DateTime.Now.AddSeconds(life * 12 + timeOut);
        }

        /// <summary>
        /// 验证密钥
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="tokenType">令牌类型</param>
        /// <returns>是否通过验证</returns>
        public bool verifyKey(string key, TokenType tokenType)
        {
            var passed = key == (tokenType == TokenType.ACCESS_TOKEN ? secretKey : refreshKey);
            if (passed && tokenType == TokenType.ACCESS_TOKEN && autoRefresh &&
                DateTime.Now.AddSeconds(life / 2 + timeOut) > expiryTime)
            {
                expiryTime = DateTime.Now.AddSeconds(life + timeOut);
                failureTime = DateTime.Now.AddSeconds(life * 12 + timeOut);
            }

            return passed;
        }

        /// <summary>
        /// 刷新令牌关键数据
        /// </summary>
        public void refresh()
        {
            expiryTime = DateTime.Now.AddSeconds(life + timeOut);
            failureTime = DateTime.Now.AddSeconds(life * 12 + timeOut);
            secretKey = Util.newId("N");
        }

        /// <summary>
        /// Token是否过期
        /// </summary>
        /// <param name="isReal">是否实际过期时间</param>
        /// <returns>Token是否过期</returns>
        public bool isExpiry(bool isReal = false)
        {
            var now = DateTime.Now;
            var expiry = expiryTime.AddSeconds(isReal ? -timeOut : 0);
            return now > expiry;
        }

        /// <summary>
        /// Token是否失效
        /// </summary>
        /// <returns>Token是否失效</returns>
        public bool isFailure()
        {
            return DateTime.Now > failureTime;
        }

        /// <summary>
        /// 获取租户的过期时间
        /// </summary>
        /// <param name="tenantId">租户ID</param>
        /// <returns>租户的过期时间</returns>
        private static DateTime getExpireDate(string tenantId)
        {
            using (var context = new Entities())
            {
                return context.tenants.SingleOrDefault(i => i.id == tenantId)?.expireDate ?? DateTime.MinValue;
            }
        }

        /// <summary>
        /// 查询指定ID的应用的令牌生命周期(秒)
        /// </summary>
        /// <returns>应用的令牌生命周期(秒)</returns>
        private void getAppInfo()
        {
            if (string.IsNullOrEmpty(appId)) appId = "Default APP";

            var key = $"App:{appId}";
            var tokenLife = RedisHelper.hashGet(key, "TokenLife");
            var type = RedisHelper.hashGet(key, "SignInOne");
            var auto = RedisHelper.hashGet(key, "AutoRefresh");
            if (!string.IsNullOrEmpty(tokenLife) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(auto))
            {
                life = Convert.ToInt32(tokenLife);
                signInOne = Convert.ToBoolean(type);
                autoRefresh = Convert.ToBoolean(auto);

                return;
            }

            // 从数据库读取应用的令牌生命周期
            using (var context = new Entities())
            {
                var app = context.applications.SingleOrDefault(i => i.id == appId);
                life = app?.tokenLife ?? 1296000;
                RedisHelper.hashSet(key, "TokenLife", life.ToString());

                signInOne = app?.isSigninOne ?? false;
                RedisHelper.hashSet(key, "SignInOne", signInOne.ToString());

                autoRefresh = app?.isAutoRefresh ?? true;
                RedisHelper.hashSet(key, "AutoRefresh", autoRefresh.ToString());
            }
        }
    }
}