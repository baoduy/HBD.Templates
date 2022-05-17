using HBD.Web.Models;

namespace TEMP.AppServices.Abstracts
{
    public abstract class ModelBase : Model
    {
        #region Properties
        
        // public override Guid? Id
        // {
        //     get => base.Id;
        //     set => base.Id = value;
        // }

        public string UserId { get; internal set; }

        #endregion Properties
    }
}