namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using VaildationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        public int 訂單數量
        {
            get
            {
                //return this.OrderLine.Count;

                //return this.OrderLine.Where(p => p.Qty > 400).Count(); // 會找出全部再count
                //return this.OrderLine.Where(p => p.Qty > 400).ToList().Count();
                return this.OrderLine.Count(p => p.Qty > 400); //效能最好 select count(*) ...where p.Qty > 400
                //Entity Framework 效能不好可能是語法用錯
            }
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [Required(ErrorMessage = "請輸入商品名稱")]
        //[MinLength(3), MaxLength(30)]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
        [DisplayName("商品名稱")]
        [商品名稱必須包含will字串(ErrorMessage = "商品名稱必須包含will字串")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "請設定正確的商品價格範圍")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString = "{0:0}")]

        [DisplayName("商品價格")]
        public Nullable<decimal> Price { get; set; }

        [DisplayName("是否上架")]
        public Nullable<bool> Active { get; set; }
        [Required(ErrorMessage = "請輸入庫存量")]

        [DisplayName("商品庫存")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
