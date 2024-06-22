using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebBanHang.Models;

namespace WebBanHang.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Chưa nhập dữ liệu"), StringLength(50)]
        public String Name { get; set; }
        [Range(1, 100), Required(ErrorMessage = "Chưa chọn thuộc tính")]
        public int DisplayOrder { get; set; }

        public int CategoryId { get; set; }
        //khai báo mối kết hợp 1-n
        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; } //khai báo mối kết hợp 1 - nhiều
    }
}
