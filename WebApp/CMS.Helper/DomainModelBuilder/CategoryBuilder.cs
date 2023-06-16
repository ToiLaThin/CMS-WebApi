namespace CMS.Helper
{
    using CMS.DataModel;
    public class CategoryBuilder
    {
        private Category _category;

        public CategoryBuilder() {
            this._category = new Category() { };
        }

        public CategoryBuilder(Category category) { 
            this._category = category;
        }

        public CategoryBuilder SetCategoryName(string name)
        {
            this._category.Name = name;
            return this;
        }

        public CategoryBuilder SetCategoryDescription(string description)
        {
            this._category.Description = description;
            return this;
        }

        public CategoryBuilder SetCategoryWithOutId(Category category)
        {
            CategoryBuilder result = this.SetCategoryName(category.Name)
                             .SetCategoryDescription(category.Description);
            return result;
        }   

        public Category Build() {
            return this._category;
        }
    }
}
