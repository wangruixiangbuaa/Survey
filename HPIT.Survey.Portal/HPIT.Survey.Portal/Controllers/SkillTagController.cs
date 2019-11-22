using HPIT.Data.Core;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Data.Entitys;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class SkillTagController : Controller
    {
        // GET: SkillTag
        public ActionResult Index()
        {
            return View();
        }

        public DeluxeJsonResult QueryPageData(SearchModel<SkillTag> search)
        {
            int total = 0;
            var result = SkillTagDal.Instance.GetPageData(search, out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total, TotalPages = totalPages }, "yyyy-MM-dd HH:mm");
        }
    }
}