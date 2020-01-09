using HPIT.Data.Core;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Data.ExtEntitys;
using HPIT.Survey.Data.QueryModel;
using HPIT.Survey.Portal.Common;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class PositionController : Controller
    {
        // GET: Position
        public ActionResult Index()
        {
            ViewData["Source"] = CommonDal.Instance.GetDictionary("来源");
            return View();
        }

        public DeluxeJsonResult QueryPageData(SearchModel<PositionExt> search)
        {
            int total = 0;
            var result = PositionDal.Instance.GetPageData(search, out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total, TotalPages = totalPages });
        }


        public ActionResult PositionStatisticIndex()
        {
            return View();
        }

        public ActionResult PositionMatchIndex()
        {
            return View();
        }


        public DeluxeJsonResult PositionMatchSearch(SearchModel<PositionMatchModel> search)
        {
            search.UserName = DeluxeUser.CurrentMember.RealName;
            int total = 0;
            var eval_match = StudentDal.Instance.GetMatchEval(search.UserName);
            if (eval_match == null)
            {
                return new DeluxeJsonResult(new { Data = "", Total = 0, TotalPages = 0, State = 300, Msg = "请先进行评估才可以进行职位匹配！！！" });
            }
            var result = PositionDal.Instance.GetMatchPageData(search, out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total, TotalPages = totalPages, State = 200 });
        }

        public DeluxeJsonResult GetPositionStatics(string direction)
        {
            var result = PositionDal.Instance.PositionStatistic(direction);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult GetPositionRelateCompany(string position,string direction)
        {
            var result = PositionDal.Instance.GetPositionRelateCompany(position,direction);
            return new DeluxeJsonResult(new { Data = result });
        }

        public DeluxeJsonResult ClearPositionTags(string positionID)
        {
            var result = PositionDal.Instance.ClearPositionTags(positionID);
            return new DeluxeJsonResult(result);
        }

        public DeluxeJsonResult UpdatePosition(Position position)
        {
            var result = PositionDal.Instance.UpdatePosition(position);
            return new DeluxeJsonResult(result);
        }

    }
}