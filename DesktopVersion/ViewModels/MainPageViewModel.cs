using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataBaseCore.Models;
using DesktopVersion.Mappers;
using DesktopVersion.Models;
using DesktopVersion.Models.DTO;
using DesktopVersion.Models.ObservableModels;
using DesktopVersion.Services;
using DesktopVersion.Views.Popups;
using Microsoft.Maui.Controls.Compatibility;
using Mopups.Interfaces;
using Mopups.PreBaked.PopupPages.EntryInput;
using Mopups.PreBaked.PopupPages.TextInput;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UraniumUI.Material.Controls;

namespace DesktopVersion.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IDataBaseService _databaseService;
        private readonly IPopupNavigation _popupNavigation;
        private readonly IFolderPickerService _folderPickerService;

        [ObservableProperty] private ObservableCollection<DatabasesTreeViewNode> _databasesTreeViewNodes = new ObservableCollection<DatabasesTreeViewNode>();
        [ObservableProperty] private DatabasesTreeViewNode _selectedNode;

        [ObservableProperty] private bool _isDatabaseInfoVisible;
        [ObservableProperty] private bool _isTableInfoVisible;

        [ObservableProperty] private DatabaseOM _selectedDatabase;
        [ObservableProperty] private TableOM _selectedTable;

        [ObservableProperty] private ObservableCollection<DataGridColumn> _dataGridColumns;
        [ObservableProperty] private DataTemplate _dataTemplate;


        private List<DataBase> _dataBases;

        public Action<ObservableCollection<ColumnOM>> ConstructHeaderAction { get; set; }

        public MainPageViewModel(IDataBaseService databaseService, IPopupNavigation popupNavigation, IFolderPickerService folderPickerService)
        {
            _databaseService = databaseService;
            _popupNavigation = popupNavigation;
            _folderPickerService = folderPickerService;
            _dataBases = _databaseService.GetConnectedDatabases();

            IsDatabaseInfoVisible = false;
            IsTableInfoVisible = false;

            DatabasesTreeViewNodes = new ObservableCollection<DatabasesTreeViewNode>();
        }

        private async Task UpdateTreeView()
        {
            DatabasesTreeViewNodes.Clear();
            foreach(var db in _dataBases)
            {
                var dbNode = new DatabasesTreeViewNode() { Name = db.Name, NodeObject = db, NodeType = NodeType.DataBaseNode };
                foreach(var table in db.Tables)
                {
                    var tbNode = new DatabasesTreeViewNode() { Name = table.Name, parentId = db.Id, NodeObject = table, NodeType = NodeType.TableNode };
                    foreach(var column in table.Columns)
                    {
                        var cl = new DatabasesTreeViewNode() { Name = $"{column.Name} : {column.Type.ToString()}", parentId = table.Id, NodeObject = column, NodeType = NodeType.ColumnNode };
                        tbNode.Children.Add(cl);
                    }
                    dbNode.Children.Add(tbNode);
                }
                DatabasesTreeViewNodes.Add(dbNode);
            }
        }


        [RelayCommand]
        public async Task CreateDatabase()
        {
            var dto = GetService<CreateDatabaseDTO>();
            dto.CreateCallBack = CreateDatabaseCallBackCommand;
            var popup = GetService<CreateDatabasePopup>();
            await _popupNavigation.PushAsync(popup);
        }

        [RelayCommand]
        public async Task CreateDatabaseCallBack(CreateDatabasePopupResult result)
        {
            if(result is not null)
            {
                _databaseService.CreateDatabase(result.Name, result.Path);
                await UpdateTreeView();
            }


        }

        [RelayCommand]
        public async Task OpenDatabase()
        {
            var path = await _folderPickerService.PickFolderAsync();
            if(path is not null)
            {
                _databaseService.OpenDatabase(path);
                await UpdateTreeView();
            }
        }

        [RelayCommand]
        public async Task CreateTable()
        {
            if(SelectedDatabase is null)
            {
                return;
            }

            var db = _dataBases.FirstOrDefault(x => x.Id == SelectedDatabase.Id);

            if(db is null)
            {
                return;
            }

            var res = await EntryInputViewModel.AutoGenerateBasicPopup(Colors.WhiteSmoke, Colors.Red, "Cancel", Colors.WhiteSmoke, Colors.Green, "Submit", Colors.DimGray, string.Empty, "Text input Example");
            if(res is null)
            {
                return;
            }

            _databaseService.CreateTable(db, res);
            await UpdateTreeView();
        }

        [RelayCommand]
        public async Task CreateColumn()
        {
            if(SelectedTable is null)
            {
                return;
            }

            var dto = GetService<CreateColumnDTO>();
            dto.CreateColumnCallback = CreateColumnCallbackCommand;

            var popup = GetService<CreateColumnPopup>();
            await _popupNavigation.PushAsync(popup);

        }

        [RelayCommand]
        public async Task CreateColumnCallback(CreateColumnPopupResult result)
        {
            if(result is null)
            {
                return;
            }

            var db = _dataBases.FirstOrDefault(x => x.Id == SelectedTable.ParentId);

            if(db is null)
            {
                return;
            }

            var table = db.Tables.FirstOrDefault(x => x.Id == SelectedTable.Id);
            if(table is null)
            {
                return;
            }

            var column = new Column() { Name = result.Name, IsPrimaryKey = result.IsPrimaryKey, Nullable = result.Nullable, DefaultValue = result.DefaultValue, Type = result.Type };

            _databaseService.CreateColumn(db, table, column);

            await UpdateTreeView();
            UpdateSelectedTable(table);
        }

        [RelayCommand]
        public async Task DropTable()
        {
            if(SelectedTable is null)
            {
                return;
            }

            var db = _dataBases.FirstOrDefault(x => x.Id == SelectedTable.ParentId);
            if(db is null)
            {
                return;
            }

            var table = db.Tables.FirstOrDefault(x => x.Id == SelectedTable.Id);
            if(table is null)
            {
                return;
            }

            _databaseService.DropTable(db, table);
        }

        partial void OnSelectedNodeChanged(DatabasesTreeViewNode? oldValue, DatabasesTreeViewNode newValue)
        {
            if(newValue is null)
            {
                IsDatabaseInfoVisible = false;
                IsTableInfoVisible = false;
                return;
            }

            switch(newValue.NodeType)
            {
                case NodeType.DataBaseNode:
                IsDatabaseInfoVisible = true;
                IsTableInfoVisible = false;
                SelectedDatabase = new DatabaseOM();
                DatabaseMapper.Map((DataBase)newValue.NodeObject, SelectedDatabase);
                break;
                case NodeType.TableNode:
                IsDatabaseInfoVisible = false;
                IsTableInfoVisible = true;
                UpdateSelectedTable((Table)newValue.NodeObject);
                SelectedTable.ParentId = newValue.parentId;
                break;
            }
        }

        [RelayCommand]
        public async Task AddEmptyRow()
        {
            if(SelectedTable is null)
            {
                return;
            }

            var db = _dataBases.FirstOrDefault(x => x.Id == SelectedTable.ParentId);
            if(db is null)
            {
                return;
            }

            var table = db.Tables.FirstOrDefault(x => x.Id == SelectedTable.Id);
            if(table is null)
            {
                return;
            }

            _databaseService.AddDataRow(db, table);

            UpdateSelectedTable(table);
        }

        [RelayCommand]
        public async Task EditDataRowValue(DataValueOM dataValueOM)
        {
            var dto = GetService<EditDataRowValueDTO>();
            dto.EditDataRowValueCallback = EditDataRowValueCallbackCommand;
            dto.DataValueOM = dataValueOM;

            var popup = GetService<EditDataRowValuePopup>();
            await _popupNavigation.PushAsync(popup);
        }

        [RelayCommand]
        public async Task EditDataRowValueCallback(EditDataRowValuePopupResult result)
        {
            if(result is null)
            {
                return;
            }

            var db = _dataBases.FirstOrDefault(x => x.Id == SelectedTable.ParentId);
            if(db is null)
            {
                return;
            }

            var tb = db.Tables.FirstOrDefault(x => x.Id == SelectedTable.Id);
            if(tb is null)
            {
                return;
            }

            var dr = tb.Data.FirstOrDefault(x => x.Id == result.DataValueOM.ParentId);
            if(dr is null)
            {
                return;
            }

            var dv = dr.Values.FirstOrDefault(x => x.Id == result.DataValueOM.Id);
            if(dv is null)
            {
                return;
            }
            try
            {
                _databaseService.SetDataValue(db, dv, result.NewValue);
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Sad");
            }
            
            UpdateSelectedTable(tb);
        }

        private void UpdateSelectedTable(Table? tb)
        {
            var tbOM = new TableOM();
            TableMapper.Map(tb, tbOM);
            foreach(var data in tbOM.Data)
            {
                data.EditValueCommand = EditDataRowValueCommand;
            }
            SelectedTable = tbOM;
            if(ConstructHeaderAction is not null)
            {
                ConstructHeaderAction.Invoke(SelectedTable.Columns);
            }
        }

        [RelayCommand]
        public async Task DropDatabase()
        {
            if(SelectedDatabase is null)
            {
                return;
            }

            var db = _dataBases.FirstOrDefault(x => x.Id == SelectedDatabase.Id);
            if(db is null)
            {
                return;
            }

            _databaseService.DropDatabase(db);
            await UpdateTreeView();
        }

        [RelayCommand]
        public async Task DeleteColumn()
        {
            var db = _dataBases.FirstOrDefault(x => x.Id == SelectedTable.ParentId);
            if(db is null)
            {
                return;
            }

            var tb = db.Tables.FirstOrDefault(x => x.Id == SelectedTable.Id);
            if(tb is null)
            {
                return;
            }


        }

        [RelayCommand]
        public async Task ColumnSelection()
        {
            if(SelectedTable is null)
            {
                return;
            }

            var dto = GetService<ColumnSelectionDTO>();
            dto.ColumnSelectionCallBack = ColumnSelectionCallbackCommand;
            dto.Table = SelectedTable;

            var popup = GetService<ColumnSelectionPopup>();
            await _popupNavigation.PushAsync(popup);

        }

        [RelayCommand]
        public async Task ColumnSelectionCallback()
        {
            
        }


    }
}
