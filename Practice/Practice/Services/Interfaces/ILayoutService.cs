using Practice.ViewModels.Layout;

namespace Practice.Services.Interfaces
{
    public interface ILayoutService
    {
        void DeleteData(int? id);
        LayoutVM GetSettingDatas();
    }
}
