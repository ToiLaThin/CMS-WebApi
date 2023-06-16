namespace CMS.DataModel
{
    public class Post_DTO : BaseDataModel<int>
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public StatusEnum Status { get; set; }

        public int CategoryId { get; set; } // ko co navigation property

    }
}
