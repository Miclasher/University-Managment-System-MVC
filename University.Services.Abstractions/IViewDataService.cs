using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace University.Services.Abstractions
{
    public interface IViewDataService
    {
        Task LoadViewDataForStudents(ViewDataDictionary viewData);
        Task LoadViewDataForGroups(ViewDataDictionary viewData);
    }
}
