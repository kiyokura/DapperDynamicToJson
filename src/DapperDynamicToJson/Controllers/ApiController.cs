using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Dapper;

namespace DapperDynamicToJson.Controllers
{
  public class ApiController : Controller
  {
    public JsonResult Default()
    {
      var json = new JsonResult()
      {
        Data = GetDynamicRecords(),
        JsonRequestBehavior = JsonRequestBehavior.AllowGet
      };
      return json;
    }

    public JsonResult JsonNet()
    {
      var json = new JsonNetResult()
        {
          Data = GetDynamicRecords(),
          JsonRequestBehavior = JsonRequestBehavior.AllowGet
        };
      return json;
    }

    private IList<dynamic> GetDynamicRecords()
    {
      var conStr = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
      using (var con = new System.Data.SqlClient.SqlConnection(conStr))
      {
        con.Open();
        var sql = @"select 'Taro' as name, 20 as age union select 'Jiro' as name, 25 as age;";
        var records = con.Query<dynamic>(sql);
        return records.ToList();
      }
    }

  }

}