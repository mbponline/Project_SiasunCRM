﻿using Kingdee.BOS.Core.Bill.PlugIn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Orm.DataEntity;

namespace KEEPER.K3.SIASUN.CRM.ClueBusinessPlugIn
{
    [Description("线索界面插件")]
    public class ClueEditUI:AbstractBillPlugIn
    {
        

        public override void BeforeF7Select(BeforeF7SelectEventArgs e)
        {
            base.BeforeF7Select(e);
            if (e.FieldKey.ToUpper().Equals("F_PEJK_PROPROCESS"))
            {
                //项目进展按照产品进行过滤
                DynamicObject proCode = this.Model.GetValue("F_PEJK_CRMProCode", e.Row) as DynamicObject;
                if (proCode ==  null)
                {
                    return;
                }
                DynamicObjectCollection ProProcess = proCode["PEJK_Cust_CRMPROENTRY"] as DynamicObjectCollection;
                if (ProProcess == null ||ProProcess.Count() == 0)
                {
                    return;
                }
                List<long> ProprocessIds = new List<long>();
                foreach (var item in ProProcess)
                {
                    ProprocessIds.Add(Convert.ToInt64(item["F_PEJK_ProProcess_Id"]));
                }
                string str = string.Join(",", ProprocessIds);
                string filter = string.Format(" FID IN ({0})", str);
                if (string.IsNullOrEmpty(e.ListFilterParameter.Filter))
                {
                    e.ListFilterParameter.Filter = filter;
                }
                else
                {
                    filter = " And " + filter;
                    e.ListFilterParameter.Filter += filter;
                }
            }
            
        }
    }
}