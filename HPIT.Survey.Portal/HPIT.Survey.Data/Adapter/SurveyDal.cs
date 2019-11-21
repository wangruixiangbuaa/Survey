using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPIT.Survey.Data.ExtEntitys;
using System.Data.Entity;
using HPIT.Data.Core;

namespace HPIT.Survey.Data.Adapter
{
    public class SurveyDal
    {
        public static SurveyDal Instance = new SurveyDal();

        public SurveyContext context { get; set; }
        public SurveyDal() {
            this.context = new SurveyContext();
        }

        public int Create(SurveyModel survey)
        {
            SurveyContext context = new SurveyContext();
            context.SurveyModel.Add(survey);
            return context.SaveChanges();
        }

        public int Update(SurveyModel survey)
        {
            context.Entry(survey).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public AbstractFormModel<SurveyModel> StartNewSurvey()
        {
            SurveyContext context = new SurveyContext();
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = new SurveyModel();
            model.Form.Company = new Company();
            model.Form.Student = new Student();
            model.ExtraDatas["Directions"] = context.Dictionary.Where(r => r.Type == "方向").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Citys"] = context.Dictionary.Where(r => r.Type == "城市").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["ProjectTypes"] = context.Dictionary.Where(r => r.Type == "项目类型").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Source"] = context.Dictionary.Where(r => r.Type == "来源").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Count"] = CommonDal.Instance.GetCount();
            model.ExtraDatas["Years"] = CommonDal.Instance.GetYears();
            return model;
        }

        public AbstractFormModel<SurveyModel> QueryByID(int id)
        {
            var survey = context.SurveyModel.Where(r => r.SurveyID == id).FirstOrDefault();
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = survey;
            model.ExtraDatas["Directions"] = context.Dictionary.Where(r => r.Type == "方向").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Citys"] = context.Dictionary.Where(r=>r.Type=="城市").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["ProjectTypes"] = context.Dictionary.Where(r => r.Type == "项目类型").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Count"] = CommonDal.Instance.GetCount();
            model.ExtraDatas["Source"] = context.Dictionary.Where(r => r.Type == "来源").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Years"] = CommonDal.Instance.GetYears();
            return model;
        }


        public object GetPageData(SearchModel<SurveyModel> search,out int count)
        {
            GetPageListParameter<SurveyModel, int> parameter = new GetPageListParameter<SurveyModel, int>();
            parameter.isAsc = true;
            parameter.orderByLambda = t => t.SurveyID;
            parameter.pageIndex = search.PageIndex;
            parameter.pageSize = search.PageSize;
            parameter.whereLambda = t => t.SurveyID > 0;
            DBBaseService baseService = new DBBaseService(SurveyContext.Instance);
            List<SurveyModel> list = baseService.GetSimplePagedData<SurveyModel, int>(parameter, out count);
            return list;
        }
    }
}
