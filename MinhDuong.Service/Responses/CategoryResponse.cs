using MinhDuong.Common;
using MinhDuong.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhDuong.Service.Responses
{
    public class CategoryResponse
    {
        public CategoryDTO Category { get; set; }
        public bool Success { get; set; }
        public ErrorMessage Error { get; set; }
    }
}