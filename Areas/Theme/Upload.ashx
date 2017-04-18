<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Data.SqlClient;

public class Upload : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;
        try
        {
            HttpPostedFile postedFile = context.Request.Files["Filedata"]; // dosyamızı alıyoruz
            string dosya = context.Request["folder"]; // dosyanın yükleneceğin klasör


            postedFile.SaveAs(HttpContext.Current.Server.MapPath(dosya + postedFile.FileName)); // dosyalar yüklenir
        }
        catch
        {
        }
        finally
        {
            context.Response.Write("Dosya yüklendi");
            context.Response.StatusCode = 200;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}