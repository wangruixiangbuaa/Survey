using HPIT.Data.Core;
using HPIT.Survey.Data.Adapter;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPIT.Survey.Portal.Controllers
{
    public class DictionaryController : Controller
    {
        // GET: Dictionary
        public ActionResult Index()
        {
            return View();
        }

        public DeluxeJsonResult QueryPageData(SearchModel<HPIT.Survey.Data.Entitys.Dictionary> search)
        {
            int total = 0;
            var result = DictionaryDal.Instance.GetPageData(search, out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total, TotalPages = totalPages });
        }


        public DeluxeJsonResult DeleteDic(int id)
        {
            int result = DictionaryDal.Instance.DeleteDic(id);
            return new DeluxeJsonResult(new { data = result,state= result});
        }

        [HttpPost]
        public DeluxeJsonResult UpdateDic(HPIT.Survey.Data.Entitys.Dictionary dictionary)
        {
            int result = DictionaryDal.Instance.UpdateDic(dictionary);
            return new DeluxeJsonResult(new { data = result,state = result});
        }
    }
} 