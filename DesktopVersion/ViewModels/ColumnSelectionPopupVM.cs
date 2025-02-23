using CommunityToolkit.Mvvm.ComponentModel;
using DesktopVersion.Models;
using DesktopVersion.Models.DTO;
using DesktopVersion.Models.ObservableModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopVersion.ViewModels
{
    public partial class ColumnSelectionPopupVM : BaseViewModel
    {
        private readonly ColumnSelectionDTO _dto;


        [ObservableProperty] public ObservableCollection<ColumnSelectionOM> _columnCollection;


        public ColumnSelectionPopupVM(ColumnSelectionDTO dto)
        {
            _dto = dto;

            InitColumnCollection();

        }

        private void InitColumnCollection()
        {
            ColumnCollection = new ObservableCollection<ColumnSelectionOM>();
            foreach(var column in _dto.Table.Columns)
            {
                ColumnCollection.Add(new ColumnSelectionOM
                {
                    Column = column,
                    IsSelected = true,
                });
            }
        }


        public async Task Apply()
        {
            var result = new ColumnSelectionPopupResult();
            result.Columns = ColumnCollection.Where(x => x.IsSelected).Select(x => x.Column).ToList();

            if(_dto.ColumnSelectionCallBack is not null)
            {
                _dto.ColumnSelectionCallBack.Execute(result);
            }
    }
}
