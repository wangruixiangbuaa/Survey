using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIT.Survey.Data.ExtEntitys
{

    /// <summary>
    /// 抽象类方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AbstractFormModel<T> where T:class
    {
        public T Form { get; set; }

        public Dictionary<string, object> ExtraDatas { get; set; } = new Dictionary<string, object>();
    }
}
