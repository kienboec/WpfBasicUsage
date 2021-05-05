﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfBasicUsage.BL;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.ViewModels {
    public class MediaFolderVM : ViewModelBase {

        private IWpfAppManager mediaManager;
        private MediaItem currentItem;
        private MediaFolder folder;
        private string searchName;

        public ICommand SearchCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public ICommand RandGenItemCommand { get; set; }

        public ICommand RandGenLogCommand { get; set; }

        public ObservableCollection<MediaItem> Items { get; set; }

        public string SearchName {
            get { return searchName; }
            set {
                if (searchName != value) {
                    searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }

        public MediaItem CurrentItem {
            get { return currentItem; }
            set {
                if ((currentItem != value) && (value != null)) {
                    currentItem = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }
            }
        }

        public MediaFolderVM(IWpfAppManager mediaManager) {
            this.mediaManager = mediaManager;
            Items = new ObservableCollection<MediaItem>();
            folder = mediaManager.GetMediaFolder("Get Media Folder From Disk");

            this.SearchCommand = new RelayCommand(o => {
                IEnumerable<MediaItem> items = mediaManager.SearchForItems(SearchName, folder);
                Items.Clear();
                foreach (MediaItem item in items) {
                    Items.Add(item);
                }
            });

            this.ClearCommand = new RelayCommand(o => {
                Items.Clear();
                SearchName = "";

                FillListView();
            });

            this.RandGenItemCommand = new RelayCommand(o => {
                MediaItem genItem = mediaManager.CreateItem(NameGenerator.GenerateName(4), NameGenerator.GenerateName(8), NameGenerator.GenerateName(16), DateTime.Now);
                Items.Add(genItem);
            });

            this.RandGenLogCommand = new RelayCommand(o => {
                MediaLog genLog = mediaManager.CreateItemLog(NameGenerator.GenerateName(45), CurrentItem);
            });

            InitListView();
        }


        public void InitListView() {
            Items = new ObservableCollection<MediaItem>();
            FillListView();
        }

        private void FillListView() {
            foreach (MediaItem item in mediaManager.GetItems(folder)) {
                Items.Add(item);
            }
        }
    }
}
