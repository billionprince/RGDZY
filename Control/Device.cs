using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ComponentModel;
using System;

namespace RGDZY.control
{
/*    public class SimpleDevice
    {
        public int id { get; set; }
        public string assetNum { get; set; }

        public EntitySet<DeviceUse> DeviceUse;

        public SimpleDevice()
        {
            this.DeviceUse = new EntitySet<DeviceUse>();
        }
    }
    [Table(Name="Device")]
    public class Device
    {
        [Column(IsPrimaryKey = true, Name = "Id", IsDbGenerated=true, UpdateCheck = UpdateCheck.Never)]
        public int id { get; set; }

        [Column(IsPrimaryKey = false, Name = "AssetNum", UpdateCheck = UpdateCheck.Never)]
        public string  assetNum { get; set; }

        [Column(IsPrimaryKey = false, Name = "Type", UpdateCheck = UpdateCheck.Never)]
        public string type { get; set; }

        [Column(IsPrimaryKey = false, Name = "Version", UpdateCheck = UpdateCheck.Never)]
        public string version { get; set; }

        [Column(IsPrimaryKey = false, Name = "Cpu", UpdateCheck = UpdateCheck.Never)]
        public string cpu { get; set; }

        [Column(IsPrimaryKey = false, Name = "Memory", UpdateCheck = UpdateCheck.Never)]
        public string memory { get; set; }

        [Column(IsPrimaryKey = false, Name = "Disk", UpdateCheck = UpdateCheck.Never)]
        public string disk { get; set; }

        [Column(IsPrimaryKey = false, Name = "PurchaseDate", UpdateCheck = UpdateCheck.Never)]
        public DateTime purchaseDate { get; set; }

        [Column(IsPrimaryKey = false, Name = "Remark", UpdateCheck = UpdateCheck.Never)]
        public string remark { get; set; }

        private EntitySet<DeviceUse> _DeviceUse;

        [Association(Name = "FK_DeviceUse_Device", Storage = "_DeviceUse", ThisKey = "id", OtherKey = "deviceId")]
        public EntitySet<DeviceUse> DeviceUse
        {
            get { return this._DeviceUse; }
            set { this._DeviceUse.Assign(value); }
        }

        public Device()
        {
            this._DeviceUse = new EntitySet<DeviceUse>();
        }

    }*/


    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Device")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class Device : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _AssetNum;

        private string _Type;

        private string _Version;

        private string _Cpu;

        private string _Memory;

        private string _Disk;

        private System.Nullable<System.DateTime> _PurchaseDate;

        private string _Remark;

        private EntityRef<DeviceUse> _DeviceUse;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnAssetNumChanging(string value);
        partial void OnAssetNumChanged();
        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();
        partial void OnVersionChanging(string value);
        partial void OnVersionChanged();
        partial void OnCpuChanging(string value);
        partial void OnCpuChanged();
        partial void OnMemoryChanging(string value);
        partial void OnMemoryChanged();
        partial void OnDiskChanging(string value);
        partial void OnDiskChanged();
        partial void OnPurchaseDateChanging(System.Nullable<System.DateTime> value);
        partial void OnPurchaseDateChanged();
        partial void OnRemarkChanging(string value);
        partial void OnRemarkChanged();
        #endregion

        public Device()
        {
            this.Initialize();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetNum", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string AssetNum
        {
            get
            {
                return this._AssetNum;
            }
            set
            {
                if ((this._AssetNum != value))
                {
                    this.OnAssetNumChanging(value);
                    this.SendPropertyChanging();
                    this._AssetNum = value;
                    this.SendPropertyChanged("AssetNum");
                    this.OnAssetNumChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this.OnTypeChanging(value);
                    this.SendPropertyChanging();
                    this._Type = value;
                    this.SendPropertyChanged("Type");
                    this.OnTypeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Version", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string Version
        {
            get
            {
                return this._Version;
            }
            set
            {
                if ((this._Version != value))
                {
                    this.OnVersionChanging(value);
                    this.SendPropertyChanging();
                    this._Version = value;
                    this.SendPropertyChanged("Version");
                    this.OnVersionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Cpu", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string Cpu
        {
            get
            {
                return this._Cpu;
            }
            set
            {
                if ((this._Cpu != value))
                {
                    this.OnCpuChanging(value);
                    this.SendPropertyChanging();
                    this._Cpu = value;
                    this.SendPropertyChanged("Cpu");
                    this.OnCpuChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Memory", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string Memory
        {
            get
            {
                return this._Memory;
            }
            set
            {
                if ((this._Memory != value))
                {
                    this.OnMemoryChanging(value);
                    this.SendPropertyChanging();
                    this._Memory = value;
                    this.SendPropertyChanged("Memory");
                    this.OnMemoryChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Disk", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string Disk
        {
            get
            {
                return this._Disk;
            }
            set
            {
                if ((this._Disk != value))
                {
                    this.OnDiskChanging(value);
                    this.SendPropertyChanging();
                    this._Disk = value;
                    this.SendPropertyChanged("Disk");
                    this.OnDiskChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PurchaseDate", DbType = "Date")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public System.Nullable<System.DateTime> PurchaseDate
        {
            get
            {
                return this._PurchaseDate;
            }
            set
            {
                if ((this._PurchaseDate != value))
                {
                    this.OnPurchaseDateChanging(value);
                    this.SendPropertyChanging();
                    this._PurchaseDate = value;
                    this.SendPropertyChanged("PurchaseDate");
                    this.OnPurchaseDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Remark", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                if ((this._Remark != value))
                {
                    this.OnRemarkChanging(value);
                    this.SendPropertyChanging();
                    this._Remark = value;
                    this.SendPropertyChanged("Remark");
                    this.OnRemarkChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_DeviceUse_Device", Storage = "_DeviceUse", ThisKey = "Id", OtherKey = "DeviceId", IsUnique = true, IsForeignKey = false, DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10, EmitDefaultValue = false)]
        public DeviceUse DeviceUse
        {
            get
            {
                if ((this.serializing
                            && (this._DeviceUse.HasLoadedOrAssignedValue == false)))
                {
                    return null;
                }
                return this._DeviceUse.Entity;
            }
            set
            {
                DeviceUse previousValue = this._DeviceUse.Entity;
                if (((previousValue != value)
                            || (this._DeviceUse.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._DeviceUse.Entity = null;
                        previousValue.Device = null;
                    }
                    this._DeviceUse.Entity = value;
                    if ((value != null))
                    {
                        value.Device = this;
                    }
                    this.SendPropertyChanged("DeviceUse");
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Initialize()
        {
            this._DeviceUse = default(EntityRef<DeviceUse>);
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }

        [global::System.Runtime.Serialization.OnSerializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnSerializing(StreamingContext context)
        {
            this.serializing = true;
        }

        [global::System.Runtime.Serialization.OnSerializedAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnSerialized(StreamingContext context)
        {
            this.serializing = false;
        }
    }


}