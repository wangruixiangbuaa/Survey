using HPIT.Survey.Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPIT.Survey.Data.ExtEntitys;
namespace HPIT.Survey.Data.Adapter
{
    public class SurveyDal
    {
        public static SurveyDal Instance = new SurveyDal();

        public int Create(SurveyModel survey)
        {
            SurveyContext context = new SurveyContext();
            context.SurveyModel.Add(survey);
            return context.SaveChanges();
        }

        public int Update(SurveyModel survey)
        {
            SurveyContext context = new SurveyContext();
            context.SurveyModel.Add(survey);
            return context.SaveChanges();
        }

        public AbstractFormModel<SurveyModel> StartNewSurvey() {
            SurveyContext context = new SurveyContext();
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = new SurveyModel();
            model.Form.Company = new Company();
            model.Form.Student = new Student();
            model.ExtraDatas["Directions"] = context.Dictionary.Where(r => r.Type == "方向").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Citys"] = context.Dictionary.Where(r => r.Type == "城市").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["ProjectTypes"] = context.Dictionary.Where(r => r.Type == "项目类型").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            return model;
        }

        public AbstractFormModel<SurveyModel> QueryByID(int id)
        {
            SurveyContext context = new SurveyContext();
            var survey = context.SurveyModel.Where(r => r.SurveyID == id).FirstOrDefault();
            AbstractFormModel<SurveyModel> model = new AbstractFormModel<SurveyModel>();
            model.Form = survey;
            model.ExtraDatas["Directions"] = context.Dictionary.Where(r => r.Type == "方向").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["Citys"] = context.Dictionary.Where(r=>r.Type=="城市").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            model.ExtraDatas["ProjectTypes"] = context.Dictionary.Where(r => r.Type == "项目类型").Select(r => new GeneralSelectItem { Text = r.Name, Value = r.Value }).ToList();
            return model;
        }
    }
}
