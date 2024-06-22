using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using WebBanHang.Models;

namespace WebBanHang.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public int CompanyId { get; set; }
        //khai báo mối kết hợp 1-n
        [ForeignKey("CompanyId")]
        public virtual Company Company { set; get; } //khai báo mối kết hợp 1 - nhiều
    }
}
