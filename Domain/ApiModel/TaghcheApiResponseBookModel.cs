using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ApiModel
{
    public class TaghcheApiResponseBookModel
    {
        public TaghcheApiResponseBookDetailModel Book { get; set; } = new TaghcheApiResponseBookDetailModel();
    }

    public class TaghcheApiResponseBookDetailModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int numberOfPages { get; set; }
    }
}
