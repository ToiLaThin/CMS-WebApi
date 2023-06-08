namespace CMS.DataModel
{
    public interface IEntity<TType>
    {
        TType Id { get; set; }
    }

    public interface IBaseDataModel
    {
        DateTime CreateDate { get; set; }

        short CreateBy { get; set; }

        DateTime? ModifiedDate { get; set; }

        short? ModifiedBy { get; set; }
    }

    public class BaseDataModel<TType> : IBaseDataModel, IEntity<TType>
    {
        public TType Id { get; set; }
        public DateTime CreateDate { get; set; }
        public short CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public short? ModifiedBy { get; set; }
    }
}
