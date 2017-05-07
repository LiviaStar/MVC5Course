using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MVC5Course.Models.VaildationAttribute
{
    public class 商品名稱必須包含will字串Attribute : DataTypeAttribute
    {
        public 商品名稱必須包含will字串Attribute() : base(DataType.Text)
        {
        }

        public override bool IsValid(object value)
        {
            var str = (string)value;
            return str.Contains("will");
        }

    }
}
