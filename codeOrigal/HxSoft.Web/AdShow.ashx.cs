using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using HxSoft.Common;
using HxSoft.Model;
using HxSoft.ClassFactory;

namespace HxSoft.Web
{
    /// <summary>
    /// AdShow 的摘要说明
    /// </summary>
    public class AdShow : IHttpHandler
    {
        //this page add by yang
        public string AdPositionID
        {
            get
            {
                return Config.RequestNumeric(HttpContext.Current.Request.QueryString["AdPositionID"],0).ToString();
            }
        }
        //
        public void ProcessRequest(HttpContext context)
        {
            AdPositionModel adPosModel = new AdPositionModel();
            adPosModel = Factory.AdPosition().GetCacheInfo2(AdPositionID);
            if (adPosModel != null)
            {
                switch (adPosModel.TypeID)
                {
                    case "1":
                        context.Response.Write(Factory.Ad().ShowPicOrFlash(AdPositionID,adPosModel.Width, adPosModel.Height).ToString());
                        break;
                    case "2":
                        context.Response.Write(Factory.Ad().ShowFlashSlide(AdPositionID, adPosModel.Width, adPosModel.Height).ToString());
                        break;
                    case "3":
                        context.Response.Write(Factory.Ad().ShowFloat(AdPositionID, adPosModel.Width, adPosModel.Height).ToString());
                        break;
                    case "4":
                        context.Response.Write(Factory.Ad().ShowDistich(AdPositionID, adPosModel.Width, adPosModel.Height).ToString());
                        break;
                    default:
                        break;
                }
            }
        }

        //
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}