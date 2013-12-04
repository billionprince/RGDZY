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
 /*   [Table(Name = "DeviceUse")]
    public class DeviceUse
    {
        [Column(IsPrimaryKey = true, Name = "DeviceId", IsDbGenerated = true, UpdateCheck = UpdateCheck.Never)]
        public int deviceId { get; set; }

        [Column(IsPrimaryKey = false, Name = "UserId", UpdateCheck = UpdateCheck.Never)]
        public string UserId { get; set; }

        [Column(IsPrimaryKey = false, Name = "StartDate", UpdateCheck = UpdateCheck.Never)]
        public string startTime { get; set; }

        [Column(IsPrimaryKey = false, Name = "EndDate", UpdateCheck = UpdateCheck.Never)]
        public string endTime { get; set; }

        private EntityRef<Device> _Device;

        [Association(Name = "FK_DeviceUse_Device", Storage = "_Device", ThisKey = "deviceId", IsForeignKey = true)]
        public Device Device
        {
            get { return this._Device.Entity; }
            set { this._Device.Entity=value; }
        }

        public DeviceUse() 
        {
            this._Device = new EntityRef<Device>();
        }
    }
  * */

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.DeviceUse")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class DeviceUse : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _DeviceId;

        private string _UserId;

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
        partial void OnUserIdChanging(string value);
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_UserId", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string UserId
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
                        this._UserId = default(string);
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
	

}