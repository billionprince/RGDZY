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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AssetNum", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.DeviceUse")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class DeviceUse : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _DeviceId;

        private int _UserId;

        private System.Nullable<System.DateTime> _StartDate;

        private System.Nullable<System.DateTime> _EndDate;

        private EntityRef<Device> _Device;

        private EntityRef<User> _User;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnDeviceIdChanging(int value);
        partial void OnDeviceIdChanged();
        partial void OnUserIdChanging(int value);
        partial void OnUserIdChanged();
        partial void OnStartDateChanging(System.Nullable<System.DateTime> value);
        partial void OnStartDateChanged();
        partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnEndDateChanged();
        #endregion

        public DeviceUse()
        {
            this.Initialize();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DeviceId", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int DeviceId
        {
            get
            {
                return this._DeviceId;
            }
            set
            {
                if ((this._DeviceId != value))
                {
                    if (this._Device.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnDeviceIdChanging(value);
                    this.SendPropertyChanging();
                    this._DeviceId = value;
                    this.SendPropertyChanged("DeviceId");
                    this.OnDeviceIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "Int NOT NULL")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public int UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                if ((this._UserId != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnUserIdChanging(value);
                    this.SendPropertyChanging();
                    this._UserId = value;
                    this.SendPropertyChanged("UserId");
                    this.OnUserIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "Date")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<System.DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this.OnStartDateChanging(value);
                    this.SendPropertyChanging();
                    this._StartDate = value;
                    this.SendPropertyChanged("StartDate");
                    this.OnStartDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndDate", DbType = "Date")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<System.DateTime> EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this.OnEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._EndDate = value;
                    this.SendPropertyChanged("EndDate");
                    this.OnEndDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_DeviceUse_Device", Storage = "_Device", ThisKey = "DeviceId", OtherKey = "Id", IsForeignKey = true)]
        public Device Device
        {
            get
            {
                return this._Device.Entity;
            }
            set
            {
                Device previousValue = this._Device.Entity;
                if (((previousValue != value)
                            || (this._Device.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Device.Entity = null;
                        previousValue.DeviceUse = null;
                    }
                    this._Device.Entity = value;
                    if ((value != null))
                    {
                        value.DeviceUse = this;
                        this._DeviceId = value.Id;
                    }
                    else
                    {
                        this._DeviceId = default(int);
                    }
                    this.SendPropertyChanged("Device");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_DeviceUse_User", Storage = "_User", ThisKey = "UserId", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.DeviceUse.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.DeviceUse.Add(this);
                        this._UserId = value.Id;
                    }
                    else
                    {
                        this._UserId = default(int);
                    }
                    this.SendPropertyChanged("User");
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
            this._Device = default(EntityRef<Device>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Event")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class Event : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.Nullable<System.TimeSpan> _StartTime;

        private System.Nullable<System.TimeSpan> _EndTime;

        private System.Nullable<int> _Repeat;

        private System.Nullable<System.DateTime> _StartDate;

        private System.Nullable<System.DateTime> _EndDate;

        private EntitySet<UserEvent> _UserEvent;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnStartTimeChanging(System.Nullable<System.TimeSpan> value);
        partial void OnStartTimeChanged();
        partial void OnEndTimeChanging(System.Nullable<System.TimeSpan> value);
        partial void OnEndTimeChanged();
        partial void OnRepeatChanging(System.Nullable<int> value);
        partial void OnRepeatChanged();
        partial void OnStartDateChanging(System.Nullable<System.DateTime> value);
        partial void OnStartDateChanged();
        partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnEndDateChanged();
        #endregion

        public Event()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartTime", DbType = "Time(7)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<System.TimeSpan> StartTime
        {
            get
            {
                return this._StartTime;
            }
            set
            {
                if ((this._StartTime != value))
                {
                    this.OnStartTimeChanging(value);
                    this.SendPropertyChanging();
                    this._StartTime = value;
                    this.SendPropertyChanged("StartTime");
                    this.OnStartTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndTime", DbType = "Time(7)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<System.TimeSpan> EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                if ((this._EndTime != value))
                {
                    this.OnEndTimeChanging(value);
                    this.SendPropertyChanging();
                    this._EndTime = value;
                    this.SendPropertyChanged("EndTime");
                    this.OnEndTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Repeat", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> Repeat
        {
            get
            {
                return this._Repeat;
            }
            set
            {
                if ((this._Repeat != value))
                {
                    this.OnRepeatChanging(value);
                    this.SendPropertyChanging();
                    this._Repeat = value;
                    this.SendPropertyChanged("Repeat");
                    this.OnRepeatChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartDate", DbType = "Date")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public System.Nullable<System.DateTime> StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this.OnStartDateChanging(value);
                    this.SendPropertyChanging();
                    this._StartDate = value;
                    this.SendPropertyChanged("StartDate");
                    this.OnStartDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndDate", DbType = "Date")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public System.Nullable<System.DateTime> EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this.OnEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._EndDate = value;
                    this.SendPropertyChanged("EndDate");
                    this.OnEndDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserEvent_Event", Storage = "_UserEvent", ThisKey = "Id", OtherKey = "EventId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8, EmitDefaultValue = false)]
        public EntitySet<UserEvent> UserEvent
        {
            get
            {
                if ((this.serializing
                            && (this._UserEvent.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._UserEvent;
            }
            set
            {
                this._UserEvent.Assign(value);
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

        private void attach_UserEvent(UserEvent entity)
        {
            this.SendPropertyChanging();
            entity.Event = this;
        }

        private void detach_UserEvent(UserEvent entity)
        {
            this.SendPropertyChanging();
            entity.Event = null;
        }

        private void Initialize()
        {
            this._UserEvent = new EntitySet<UserEvent>(new Action<UserEvent>(this.attach_UserEvent), new Action<UserEvent>(this.detach_UserEvent));
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Job")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class Job : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private string _Detail;

        private System.Nullable<System.DateTime> _StartTime;

        private System.Nullable<System.DateTime> _EndTime;

        private System.Nullable<int> _TaskId;

        private System.Nullable<int> _UserId;

        private EntityRef<Task> _Task;

        private EntityRef<User> _User;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDetailChanging(string value);
        partial void OnDetailChanged();
        partial void OnStartTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnStartTimeChanged();
        partial void OnEndTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnEndTimeChanged();
        partial void OnTaskIdChanging(System.Nullable<int> value);
        partial void OnTaskIdChanged();
        partial void OnUserIdChanging(System.Nullable<int> value);
        partial void OnUserIdChanged();
        #endregion

        public Job()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Detail", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string Detail
        {
            get
            {
                return this._Detail;
            }
            set
            {
                if ((this._Detail != value))
                {
                    this.OnDetailChanging(value);
                    this.SendPropertyChanging();
                    this._Detail = value;
                    this.SendPropertyChanged("Detail");
                    this.OnDetailChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StartTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<System.DateTime> StartTime
        {
            get
            {
                return this._StartTime;
            }
            set
            {
                if ((this._StartTime != value))
                {
                    this.OnStartTimeChanging(value);
                    this.SendPropertyChanging();
                    this._StartTime = value;
                    this.SendPropertyChanged("StartTime");
                    this.OnStartTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EndTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<System.DateTime> EndTime
        {
            get
            {
                return this._EndTime;
            }
            set
            {
                if ((this._EndTime != value))
                {
                    this.OnEndTimeChanging(value);
                    this.SendPropertyChanging();
                    this._EndTime = value;
                    this.SendPropertyChanged("EndTime");
                    this.OnEndTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TaskId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public System.Nullable<int> TaskId
        {
            get
            {
                return this._TaskId;
            }
            set
            {
                if ((this._TaskId != value))
                {
                    if (this._Task.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTaskIdChanging(value);
                    this.SendPropertyChanging();
                    this._TaskId = value;
                    this.SendPropertyChanged("TaskId");
                    this.OnTaskIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public System.Nullable<int> UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                if ((this._UserId != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnUserIdChanging(value);
                    this.SendPropertyChanging();
                    this._UserId = value;
                    this.SendPropertyChanged("UserId");
                    this.OnUserIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Job_Task", Storage = "_Task", ThisKey = "TaskId", OtherKey = "Id", IsForeignKey = true)]
        public Task Task
        {
            get
            {
                return this._Task.Entity;
            }
            set
            {
                Task previousValue = this._Task.Entity;
                if (((previousValue != value)
                            || (this._Task.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Task.Entity = null;
                        previousValue.Job.Remove(this);
                    }
                    this._Task.Entity = value;
                    if ((value != null))
                    {
                        value.Job.Add(this);
                        this._TaskId = value.Id;
                    }
                    else
                    {
                        this._TaskId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Task");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Job_User", Storage = "_User", ThisKey = "UserId", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.Job.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.Job.Add(this);
                        this._UserId = value.Id;
                    }
                    else
                    {
                        this._UserId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("User");
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
            this._Task = default(EntityRef<Task>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Message")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class Message : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Content;

        private System.Nullable<System.DateTime> _Timestamp;

        private System.Nullable<int> _From;

        private System.Nullable<int> _To;

        private System.Nullable<int> _HaveRead;

        private EntityRef<User> _User;

        private EntityRef<User> _ToUser;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnContentChanging(string value);
        partial void OnContentChanged();
        partial void OnTimestampChanging(System.Nullable<System.DateTime> value);
        partial void OnTimestampChanged();
        partial void OnFromChanging(System.Nullable<int> value);
        partial void OnFromChanged();
        partial void OnToChanging(System.Nullable<int> value);
        partial void OnToChanged();
        partial void OnHaveReadChanging(System.Nullable<int> value);
        partial void OnHaveReadChanged();
        #endregion

        public Message()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Content", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                if ((this._Content != value))
                {
                    this.OnContentChanging(value);
                    this.SendPropertyChanging();
                    this._Content = value;
                    this.SendPropertyChanged("Content");
                    this.OnContentChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Timestamp", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<System.DateTime> Timestamp
        {
            get
            {
                return this._Timestamp;
            }
            set
            {
                if ((this._Timestamp != value))
                {
                    this.OnTimestampChanging(value);
                    this.SendPropertyChanging();
                    this._Timestamp = value;
                    this.SendPropertyChanged("Timestamp");
                    this.OnTimestampChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_From", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<int> From
        {
            get
            {
                return this._From;
            }
            set
            {
                if ((this._From != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnFromChanging(value);
                    this.SendPropertyChanging();
                    this._From = value;
                    this.SendPropertyChanged("From");
                    this.OnFromChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_To", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> To
        {
            get
            {
                return this._To;
            }
            set
            {
                if ((this._To != value))
                {
                    if (this._ToUser.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnToChanging(value);
                    this.SendPropertyChanging();
                    this._To = value;
                    this.SendPropertyChanged("To");
                    this.OnToChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HaveRead", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public System.Nullable<int> HaveRead
        {
            get
            {
                return this._HaveRead;
            }
            set
            {
                if ((this._HaveRead != value))
                {
                    this.OnHaveReadChanging(value);
                    this.SendPropertyChanging();
                    this._HaveRead = value;
                    this.SendPropertyChanged("HaveRead");
                    this.OnHaveReadChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Message_FromUser", Storage = "_User", ThisKey = "From", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.Message.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.Message.Add(this);
                        this._From = value.Id;
                    }
                    else
                    {
                        this._From = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("User");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Message_ToUser", Storage = "_ToUser", ThisKey = "To", OtherKey = "Id", IsForeignKey = true)]
        public User ToUser
        {
            get
            {
                return this._ToUser.Entity;
            }
            set
            {
                User previousValue = this._ToUser.Entity;
                if (((previousValue != value)
                            || (this._ToUser.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._ToUser.Entity = null;
                        previousValue.Message_ToUser.Remove(this);
                    }
                    this._ToUser.Entity = value;
                    if ((value != null))
                    {
                        value.Message_ToUser.Add(this);
                        this._To = value.Id;
                    }
                    else
                    {
                        this._To = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("ToUser");
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
            this._User = default(EntityRef<User>);
            this._ToUser = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Project")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class Project : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private System.Nullable<int> _Advisor;

        private string _Description;

        private EntityRef<User> _User;

        private EntitySet<Task> _Task;

        private EntitySet<UserProject> _UserProject;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnAdvisorChanging(System.Nullable<int> value);
        partial void OnAdvisorChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        #endregion

        public Project()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Advisor", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<int> Advisor
        {
            get
            {
                return this._Advisor;
            }
            set
            {
                if ((this._Advisor != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnAdvisorChanging(value);
                    this.SendPropertyChanging();
                    this._Advisor = value;
                    this.SendPropertyChanged("Advisor");
                    this.OnAdvisorChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Project_User", Storage = "_User", ThisKey = "Advisor", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.Project.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.Project.Add(this);
                        this._Advisor = value.Id;
                    }
                    else
                    {
                        this._Advisor = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("User");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Task_Project", Storage = "_Task", ThisKey = "Id", OtherKey = "ProjectId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5, EmitDefaultValue = false)]
        public EntitySet<Task> Task
        {
            get
            {
                if ((this.serializing
                            && (this._Task.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._Task;
            }
            set
            {
                this._Task.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserProject_Project", Storage = "_UserProject", ThisKey = "Id", OtherKey = "ProjectId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6, EmitDefaultValue = false)]
        public EntitySet<UserProject> UserProject
        {
            get
            {
                if ((this.serializing
                            && (this._UserProject.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._UserProject;
            }
            set
            {
                this._UserProject.Assign(value);
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

        private void attach_Task(Task entity)
        {
            this.SendPropertyChanging();
            entity.Project = this;
        }

        private void detach_Task(Task entity)
        {
            this.SendPropertyChanging();
            entity.Project = null;
        }

        private void attach_UserProject(UserProject entity)
        {
            this.SendPropertyChanging();
            entity.Project = this;
        }

        private void detach_UserProject(UserProject entity)
        {
            this.SendPropertyChanging();
            entity.Project = null;
        }

        private void Initialize()
        {
            this._User = default(EntityRef<User>);
            this._Task = new EntitySet<Task>(new Action<Task>(this.attach_Task), new Action<Task>(this.detach_Task));
            this._UserProject = new EntitySet<UserProject>(new Action<UserProject>(this.attach_UserProject), new Action<UserProject>(this.detach_UserProject));
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Task")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class Task : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private string _Detail;

        private System.Nullable<int> _ProjectId;

        private EntitySet<Job> _Job;

        private EntityRef<Project> _Project;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDetailChanging(string value);
        partial void OnDetailChanged();
        partial void OnProjectIdChanging(System.Nullable<int> value);
        partial void OnProjectIdChanged();
        #endregion

        public Task()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Detail", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string Detail
        {
            get
            {
                return this._Detail;
            }
            set
            {
                if ((this._Detail != value))
                {
                    this.OnDetailChanging(value);
                    this.SendPropertyChanging();
                    this._Detail = value;
                    this.SendPropertyChanged("Detail");
                    this.OnDetailChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProjectId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<int> ProjectId
        {
            get
            {
                return this._ProjectId;
            }
            set
            {
                if ((this._ProjectId != value))
                {
                    if (this._Project.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnProjectIdChanging(value);
                    this.SendPropertyChanging();
                    this._ProjectId = value;
                    this.SendPropertyChanged("ProjectId");
                    this.OnProjectIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Job_Task", Storage = "_Job", ThisKey = "Id", OtherKey = "TaskId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5, EmitDefaultValue = false)]
        public EntitySet<Job> Job
        {
            get
            {
                if ((this.serializing
                            && (this._Job.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._Job;
            }
            set
            {
                this._Job.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Task_Project", Storage = "_Project", ThisKey = "ProjectId", OtherKey = "Id", IsForeignKey = true)]
        public Project Project
        {
            get
            {
                return this._Project.Entity;
            }
            set
            {
                Project previousValue = this._Project.Entity;
                if (((previousValue != value)
                            || (this._Project.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Project.Entity = null;
                        previousValue.Task.Remove(this);
                    }
                    this._Project.Entity = value;
                    if ((value != null))
                    {
                        value.Task.Add(this);
                        this._ProjectId = value.Id;
                    }
                    else
                    {
                        this._ProjectId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Project");
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

        private void attach_Job(Job entity)
        {
            this.SendPropertyChanging();
            entity.Task = this;
        }

        private void detach_Job(Job entity)
        {
            this.SendPropertyChanging();
            entity.Task = null;
        }

        private void Initialize()
        {
            this._Job = new EntitySet<Job>(new Action<Job>(this.attach_Job), new Action<Job>(this.detach_Job));
            this._Project = default(EntityRef<Project>);
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.User")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private string _Name;

        private string _StudentId;

        private int _Authority;

        private string _Password;

        private EntitySet<DeviceUse> _DeviceUse;

        private EntitySet<Job> _Job;

        private EntitySet<Message> _Message;

        private EntitySet<Message> _Message_ToUser;

        private EntitySet<Project> _Project;

        private EntitySet<UserEvent> _UserEvent;

        private EntitySet<UserProject> _UserProject;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnStudentIdChanging(string value);
        partial void OnStudentIdChanged();
        partial void OnAuthorityChanging(int value);
        partial void OnAuthorityChanged();
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
        #endregion

        public User()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StudentId", DbType = "NVarChar(MAX)", UpdateCheck = UpdateCheck.Never)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string StudentId
        {
            get
            {
                return this._StudentId;
            }
            set
            {
                if ((this._StudentId != value))
                {
                    this.OnStudentIdChanging(value);
                    this.SendPropertyChanging();
                    this._StudentId = value;
                    this.SendPropertyChanged("StudentId");
                    this.OnStudentIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Authority", DbType = "Int NOT NULL")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public int Authority
        {
            get
            {
                return this._Authority;
            }
            set
            {
                if ((this._Authority != value))
                {
                    this.OnAuthorityChanging(value);
                    this.SendPropertyChanging();
                    this._Authority = value;
                    this.SendPropertyChanged("Authority");
                    this.OnAuthorityChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Password", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this.OnPasswordChanging(value);
                    this.SendPropertyChanging();
                    this._Password = value;
                    this.SendPropertyChanged("Password");
                    this.OnPasswordChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_DeviceUse_User", Storage = "_DeviceUse", ThisKey = "Id", OtherKey = "UserId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6, EmitDefaultValue = false)]
        public EntitySet<DeviceUse> DeviceUse
        {
            get
            {
                if ((this.serializing
                            && (this._DeviceUse.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._DeviceUse;
            }
            set
            {
                this._DeviceUse.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Job_User", Storage = "_Job", ThisKey = "Id", OtherKey = "UserId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7, EmitDefaultValue = false)]
        public EntitySet<Job> Job
        {
            get
            {
                if ((this.serializing
                            && (this._Job.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._Job;
            }
            set
            {
                this._Job.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Message_FromUser", Storage = "_Message", ThisKey = "Id", OtherKey = "From", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8, EmitDefaultValue = false)]
        public EntitySet<Message> Message
        {
            get
            {
                if ((this.serializing
                            && (this._Message.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._Message;
            }
            set
            {
                this._Message.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Message_ToUser", Storage = "_Message_ToUser", ThisKey = "Id", OtherKey = "To", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9, EmitDefaultValue = false)]
        public EntitySet<Message> Message_ToUser
        {
            get
            {
                if ((this.serializing
                            && (this._Message_ToUser.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._Message_ToUser;
            }
            set
            {
                this._Message_ToUser.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_Project_User", Storage = "_Project", ThisKey = "Id", OtherKey = "Advisor", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10, EmitDefaultValue = false)]
        public EntitySet<Project> Project
        {
            get
            {
                if ((this.serializing
                            && (this._Project.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._Project;
            }
            set
            {
                this._Project.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserEvent_User", Storage = "_UserEvent", ThisKey = "Id", OtherKey = "UserId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11, EmitDefaultValue = false)]
        public EntitySet<UserEvent> UserEvent
        {
            get
            {
                if ((this.serializing
                            && (this._UserEvent.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._UserEvent;
            }
            set
            {
                this._UserEvent.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserProject_User", Storage = "_UserProject", ThisKey = "Id", OtherKey = "UserId", DeleteRule = "NO ACTION")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 12, EmitDefaultValue = false)]
        public EntitySet<UserProject> UserProject
        {
            get
            {
                if ((this.serializing
                            && (this._UserProject.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._UserProject;
            }
            set
            {
                this._UserProject.Assign(value);
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

        private void attach_DeviceUse(DeviceUse entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_DeviceUse(DeviceUse entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_Job(Job entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_Job(Job entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_Message(Message entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_Message(Message entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_Message_ToUser(Message entity)
        {
            this.SendPropertyChanging();
            entity.ToUser = this;
        }

        private void detach_Message_ToUser(Message entity)
        {
            this.SendPropertyChanging();
            entity.ToUser = null;
        }

        private void attach_Project(Project entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_Project(Project entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_UserEvent(UserEvent entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_UserEvent(UserEvent entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void attach_UserProject(UserProject entity)
        {
            this.SendPropertyChanging();
            entity.User = this;
        }

        private void detach_UserProject(UserProject entity)
        {
            this.SendPropertyChanging();
            entity.User = null;
        }

        private void Initialize()
        {
            this._DeviceUse = new EntitySet<DeviceUse>(new Action<DeviceUse>(this.attach_DeviceUse), new Action<DeviceUse>(this.detach_DeviceUse));
            this._Job = new EntitySet<Job>(new Action<Job>(this.attach_Job), new Action<Job>(this.detach_Job));
            this._Message = new EntitySet<Message>(new Action<Message>(this.attach_Message), new Action<Message>(this.detach_Message));
            this._Message_ToUser = new EntitySet<Message>(new Action<Message>(this.attach_Message_ToUser), new Action<Message>(this.detach_Message_ToUser));
            this._Project = new EntitySet<Project>(new Action<Project>(this.attach_Project), new Action<Project>(this.detach_Project));
            this._UserEvent = new EntitySet<UserEvent>(new Action<UserEvent>(this.attach_UserEvent), new Action<UserEvent>(this.detach_UserEvent));
            this._UserProject = new EntitySet<UserProject>(new Action<UserProject>(this.attach_UserProject), new Action<UserProject>(this.detach_UserProject));
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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserEvent")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class UserEvent : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.Nullable<int> _UserId;

        private System.Nullable<int> _EventId;

        private EntityRef<Event> _Event;

        private EntityRef<User> _User;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnUserIdChanging(System.Nullable<int> value);
        partial void OnUserIdChanged();
        partial void OnEventIdChanging(System.Nullable<int> value);
        partial void OnEventIdChanged();
        #endregion

        public UserEvent()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<int> UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                if ((this._UserId != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnUserIdChanging(value);
                    this.SendPropertyChanging();
                    this._UserId = value;
                    this.SendPropertyChanged("UserId");
                    this.OnUserIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EventId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<int> EventId
        {
            get
            {
                return this._EventId;
            }
            set
            {
                if ((this._EventId != value))
                {
                    if (this._Event.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnEventIdChanging(value);
                    this.SendPropertyChanging();
                    this._EventId = value;
                    this.SendPropertyChanged("EventId");
                    this.OnEventIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserEvent_Event", Storage = "_Event", ThisKey = "EventId", OtherKey = "Id", IsForeignKey = true)]
        public Event Event
        {
            get
            {
                return this._Event.Entity;
            }
            set
            {
                Event previousValue = this._Event.Entity;
                if (((previousValue != value)
                            || (this._Event.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Event.Entity = null;
                        previousValue.UserEvent.Remove(this);
                    }
                    this._Event.Entity = value;
                    if ((value != null))
                    {
                        value.UserEvent.Add(this);
                        this._EventId = value.Id;
                    }
                    else
                    {
                        this._EventId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Event");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserEvent_User", Storage = "_User", ThisKey = "UserId", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.UserEvent.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.UserEvent.Add(this);
                        this._UserId = value.Id;
                    }
                    else
                    {
                        this._UserId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("User");
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
            this._Event = default(EntityRef<Event>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.UserProject")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class UserProject : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Id;

        private System.Nullable<int> _UserId;

        private System.Nullable<int> _ProjectId;

        private EntityRef<Project> _Project;

        private EntityRef<User> _User;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        partial void OnUserIdChanging(System.Nullable<int> value);
        partial void OnUserIdChanged();
        partial void OnProjectIdChanging(System.Nullable<int> value);
        partial void OnProjectIdChanged();
        #endregion

        public UserProject()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<int> UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                if ((this._UserId != value))
                {
                    if (this._User.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnUserIdChanging(value);
                    this.SendPropertyChanging();
                    this._UserId = value;
                    this.SendPropertyChanged("UserId");
                    this.OnUserIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProjectId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<int> ProjectId
        {
            get
            {
                return this._ProjectId;
            }
            set
            {
                if ((this._ProjectId != value))
                {
                    if (this._Project.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnProjectIdChanging(value);
                    this.SendPropertyChanging();
                    this._ProjectId = value;
                    this.SendPropertyChanged("ProjectId");
                    this.OnProjectIdChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserProject_Project", Storage = "_Project", ThisKey = "ProjectId", OtherKey = "Id", IsForeignKey = true)]
        public Project Project
        {
            get
            {
                return this._Project.Entity;
            }
            set
            {
                Project previousValue = this._Project.Entity;
                if (((previousValue != value)
                            || (this._Project.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Project.Entity = null;
                        previousValue.UserProject.Remove(this);
                    }
                    this._Project.Entity = value;
                    if ((value != null))
                    {
                        value.UserProject.Add(this);
                        this._ProjectId = value.Id;
                    }
                    else
                    {
                        this._ProjectId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("Project");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "FK_UserProject_User", Storage = "_User", ThisKey = "UserId", OtherKey = "Id", IsForeignKey = true)]
        public User User
        {
            get
            {
                return this._User.Entity;
            }
            set
            {
                User previousValue = this._User.Entity;
                if (((previousValue != value)
                            || (this._User.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._User.Entity = null;
                        previousValue.UserProject.Remove(this);
                    }
                    this._User.Entity = value;
                    if ((value != null))
                    {
                        value.UserProject.Add(this);
                        this._UserId = value.Id;
                    }
                    else
                    {
                        this._UserId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("User");
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
            this._Project = default(EntityRef<Project>);
            this._User = default(EntityRef<User>);
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }
    }
}