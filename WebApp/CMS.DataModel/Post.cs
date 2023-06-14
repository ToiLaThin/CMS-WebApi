using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.DataModel
{
    public enum StatusEnum
    {
        Draft,
        Published,
        Archived
    }

    [Table("Post", Schema = "Post")]
    public partial class Post : BaseDataModel<int>
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public StatusEnum Status { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } //nullable de khi post swagger ko phai nhap
    }
}
