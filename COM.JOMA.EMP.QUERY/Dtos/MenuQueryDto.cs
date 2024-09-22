using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class MenuQueryDto
    {
        public int Id { get; set; }
        public int? MenuPadreId { get; set; }
        public int IdUario { get; set; }
        public int IdRol { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public List<MenuQueryDto> Children { get; set; }
        public MenuQueryDto()
        {
            Children = new List<MenuQueryDto>();
        }
    }
}
