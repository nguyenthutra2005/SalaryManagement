using System;

namespace SalaryManagement.Domain.Common
{
    public abstract class ThucTheCoBan
    {
        public int Id { get; set; }
        public DateTime NgayTao { get; set; }  
        public DateTime? NgayCapNhat { get; set; }
        public bool DaXoa { get; set; } = false;
    }
}