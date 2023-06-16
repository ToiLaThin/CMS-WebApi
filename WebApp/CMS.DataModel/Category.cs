using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.DataModel
{
    [Table("Category", Schema = "CategorySchema")]
    public partial class Category : BaseDataModel<int>
    {
        public Category()
        {
            this.Posts = new List<Post>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
