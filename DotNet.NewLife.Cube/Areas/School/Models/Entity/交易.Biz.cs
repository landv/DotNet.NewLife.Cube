﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using XCode.Shards;

namespace DotNet.NewLife.Cube.Areas.School.Entity;

public partial class Trade : Entity<Trade>
{
    #region 对象操作
    static Trade()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(nameof(TenantId));

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<TenantModule>();

        // 实体缓存
        // var ec = Meta.Cache;
        // ec.Expire = 60;
    }

    /// <summary>验证并修补数据，返回验证结果，或者通过抛出异常的方式提示验证失败。</summary>
    /// <param name="method">添删改方法</param>
    public override Boolean Valid(DataMethod method)
    {
        //if (method == DataMethod.Delete) return true;
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return true;

        // 建议先调用基类方法，基类方法会做一些统一处理
        if (!base.Valid(method)) return false;

        // 在新插入数据或者修改了指定字段时进行修正

        return true;
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Trade[交易]数据……");

    //    var entity = new Trade();
    //    entity.TenantId = 0;
    //    entity.NodeId = 0;
    //    entity.Tid = "abc";
    //    entity.Status = 0;
    //    entity.PayStatus = 0;
    //    entity.ShipStatus = 0;
    //    entity.CreateIPReceiverPhone = "abc";
    //    entity.ReceiverMobile = "abc";
    //    entity.ReceiverState = "abc";
    //    entity.ReceiverCity = "abc";
    //    entity.ReceiverDistrict = "abc";
    //    entity.ReceiverAddress = "abc";
    //    entity.BuyerName = "abc";
    //    entity.Created = 0;
    //    entity.Modified = 0;
    //    entity.IsSend = 0;
    //    entity.ErrorMsg = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Trade[交易]数据！");
    //}

    ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
    ///// <returns></returns>
    //public override Int32 Insert()
    //{
    //    return base.Insert();
    //}

    ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
    ///// <returns></returns>
    //protected override Int32 OnDelete()
    //{
    //    return base.OnDelete();
    //}
    #endregion

    #region 扩展属性
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="tenantId">租户</param>
    /// <param name="nodeId">节点号</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<Trade> Search(Int32 tenantId, Int32 nodeId, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (tenantId >= 0) exp &= _.TenantId == tenantId;
        if (nodeId >= 0) exp &= _.NodeId == nodeId;
        if (!key.IsNullOrEmpty()) exp &= _.Tid.Contains(key) | _.CreateIPReceiverPhone.Contains(key) | _.ReceiverMobile.Contains(key) | _.ReceiverState.Contains(key) | _.ReceiverCity.Contains(key) | _.ReceiverDistrict.Contains(key) | _.ReceiverAddress.Contains(key) | _.BuyerName.Contains(key) | _.ErrorMsg.Contains(key);

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,Category From Trade Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<Trade> _CategoryCache = new FieldCache<Trade>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    public ITrade ToModel()
    {
        var model = new Trade();
        model.Copy(this);

        return model;
    }

    #endregion
}
